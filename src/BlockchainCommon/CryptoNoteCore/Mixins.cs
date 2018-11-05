// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace CryptoNote
{
  public class Mixins
  {

	  /* This method is used to get the minimum and maximum mixin permitted for the
	     requested height */
	  public static Tuple<ulong, ulong> getMixinAllowableRange(uint height)
	  {
		ulong minMixin = 0;
		ulong maxMixin = ulong.MaxValue;

		/* We now limit the mixin allowed in a transaction. However, there have been
		   some transactions outside these limits in the past, so we need to only
		   enforce this on new blocks, otherwise wouldn't be able to sync the chain */

		/* We also need to ensure that the mixin enforced is for the limit that
		   was correct when the block was formed - i.e. if 0 mixin was allowed at
		   block 100, but is no longer allowed - we should still validate block 100 */

		if (height >= CryptoNote.parameters.MIXIN_LIMITS_V3_HEIGHT)
		{
		  minMixin = CryptoNote.parameters.MINIMUM_MIXIN_V3;
		  maxMixin = CryptoNote.parameters.MAXIMUM_MIXIN_V3;
		}
		else if (height >= CryptoNote.parameters.MIXIN_LIMITS_V2_HEIGHT)
		{
		  minMixin = CryptoNote.parameters.MINIMUM_MIXIN_V2;
		  maxMixin = CryptoNote.parameters.MAXIMUM_MIXIN_V2;
		}
		else if (height >= CryptoNote.parameters.MIXIN_LIMITS_V1_HEIGHT)
		{
		  minMixin = CryptoNote.parameters.MINIMUM_MIXIN_V1;
		  maxMixin = CryptoNote.parameters.MAXIMUM_MIXIN_V1;
		}

		return new Tuple<ulong, ulong>(minMixin, maxMixin);
	  }

	  /* This method is used by WalletService to determine if the mixin amount is correct
	     for the current block height */
	  public static Tuple<bool, string, std::error_code> validate(uint mixin, uint height)
	  {
		var (minMixin, maxMixin) = getMixinAllowableRange(height);

		std::stringstream str = new std::stringstream();

		if (mixin < minMixin)
		{
		  str << "Mixin of " << (int)mixin << " under minimum mixin threshold of " << minMixin;
		  return new Tuple<bool, string, std::error_code>(false, str.str(), GlobalMembers.make_error_code(CryptoNote.error.MIXIN_BELOW_THRESHOLD));
		}
		else if (mixin > maxMixin)
		{
		  str << "Mixin of " << (int)mixin << " above maximum mixin threshold of " << maxMixin;
		  return new Tuple<bool, string, std::error_code>(false, str.str(), GlobalMembers.make_error_code(CryptoNote.error.MIXIN_ABOVE_THRESHOLD));
		}

		return new Tuple<bool, string, std::error_code>(true, string(), std::error_code());
	  }

	  /* This method is commonly used by the node to determine if the transactions in the vector have
	     the correct mixin (anonymity) as defined by the current rules */
	  public static Tuple<bool, string> validate(List<CachedTransaction> transactions, uint height)
	  {
		var (minMixin, maxMixin) = getMixinAllowableRange(height);

		foreach (var transaction in transactions)
		{
			var (success, error) = validate(transaction, minMixin, maxMixin);

			if (!success)
			{
				return new Tuple<bool, string>(false, error);
			}
		}

		return new Tuple<bool, string>(true, string());
	  }

	  /* This method is commonly used by the node to determine if the transaction has
	     the correct mixin (anonymity) as defined by the current rules */
	  public static Tuple<bool, string> validate(CachedTransaction transaction, ulong minMixin, ulong maxMixin)
	  {
		ulong ringSize = 1;

		var tx = createTransaction(transaction.getTransaction());

		for (uint i = 0; i < tx.getInputCount(); ++i)
		{
		  if (tx.getInputType(i) != TransactionTypes.InputType.Key)
		  {
			continue;
		  }

		  KeyInput input = new KeyInput();
		  tx.getInput(i, input);
		  ulong currentRingSize = input.outputIndexes.Count;
		  if (currentRingSize > ringSize)
		  {
			  ringSize = currentRingSize;
		  }
		}

		/* Ring size = mixin + 1 - your transaction plus the others you mix with */
		ulong mixin = ringSize - 1;

		std::stringstream str = new std::stringstream();

		if (mixin > maxMixin)
		{
		  str << "Transaction " << transaction.getTransactionHash() << " is not valid. Reason: transaction mixin is too large (" << (int)mixin << "). Maximum mixin allowed is " << (int)maxMixin;

		  return new Tuple<bool, string>(false, str.str());
		}
		else if (mixin < minMixin)
		{
		  str << "Transaction " << transaction.getTransactionHash() << " is not valid. Reason: transaction mixin is too small (" << (int)mixin << "). Minimum mixin allowed is " << (int)minMixin;

		  return new Tuple<bool, string>(false, str.str());
		}

		return new Tuple<bool, string>(true, string());
	  }
  }
}