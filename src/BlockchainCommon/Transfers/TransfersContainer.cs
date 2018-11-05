using Common;
using Crypto;
using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.




//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(size_t) <= sizeof(type), "Size of " #type " must be at least that of size_t"); inline size_t hash_value(const type &_v) { return reinterpret_cast<const size_t &>(_v); } } namespace std { template<> struct hash<Crypto::type> { size_t operator()(const Crypto::type &_v) const { return reinterpret_cast<const size_t &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct TransactionOutputInformationIn;

public class SpentOutputDescriptor
{
  public SpentOutputDescriptor()
  {
	  this.m_type = new CryptoNote.TransactionTypes.OutputType(TransactionTypes.OutputType.Invalid);
  }
  public SpentOutputDescriptor(TransactionOutputInformationIn transactionInfo)
  {
	  this.m_type = new CryptoNote.TransactionTypes.OutputType(transactionInfo.type);
	  this.m_amount = 0;
	  this.m_globalOutputIndex = 0;
	if (m_type == TransactionTypes.OutputType.Key)
	{
	  m_keyImage = transactionInfo.keyImage;
	}
	else
	{
	  Debug.Assert(false);
	}
  }
  public SpentOutputDescriptor(KeyImage keyImage)
  {
	assign(keyImage);
  }

  public void assign(KeyImage keyImage)
  {
	m_type = TransactionTypes.OutputType.Key;
	m_keyImage = keyImage;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isValid() const
  public bool isValid()
  {
	return m_type != TransactionTypes.OutputType.Invalid;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const SpentOutputDescriptor& other) const
  public static bool operator == (SpentOutputDescriptor ImpliedObject, SpentOutputDescriptor other)
  {
	if (ImpliedObject.m_type == TransactionTypes.OutputType.Key)
	{
	  return other.m_type == ImpliedObject.m_type && *other.m_keyImage == *m_keyImage;
	}
	else
	{
	  Debug.Assert(false);
	  return false;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint hash() const
  public uint hash()
  {
	if (m_type == TransactionTypes.OutputType.Key)
	{
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
  //	static_assert(sizeof(uint) < sizeof(*m_keyImage), "sizeof(size_t) < sizeof(*m_keyImage)");
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return *reinterpret_cast<const uint>(m_keyImage.data);
	}
	else
	{
	  Debug.Assert(false);
	  return 0;
	}
  }

  private TransactionTypes.OutputType m_type;
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	const Crypto::KeyImage* m_keyImage;
//	struct
//	{
//	  ulong m_amount;
//	  uint m_globalOutputIndex;
//	};
//  };
}

public class SpentOutputDescriptorHasher
{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint operator ()(const SpentOutputDescriptor& descriptor) const
  public static uint functorMethod(SpentOutputDescriptor descriptor)
  {
	return new descriptor.hash();
  }
}

public class TransactionOutputInformationIn : TransactionOutputInformation
{
  public Crypto.KeyImage keyImage = new Crypto.KeyImage(); //!< \attention Used only for TransactionTypes::OutputType::Key
}

public class TransactionOutputInformationEx : TransactionOutputInformationIn
{
  public ulong unlockTime;
  public uint blockHeight;
  public uint transactionIndex;
  public bool visible;

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SpentOutputDescriptor getSpentOutputDescriptor() const
  public SpentOutputDescriptor getSpentOutputDescriptor()
  {
	  return new SpentOutputDescriptor(this);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTransactionHash() const
  public Crypto.Hash getTransactionHash()
  {
	  return transactionHash;
  }

  public void serialize(CryptoNote.ISerializer s)
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	s.functorMethod(reinterpret_cast<byte&>(type), "type");
	s.functorMethod(amount, "");
	serializeGlobalOutputIndex(s.functorMethod, globalOutputIndex, "");
	s.functorMethod(outputInTransaction, "");
	s.functorMethod(transactionPublicKey, "");
	s.functorMethod(keyImage, "");
	s.functorMethod(unlockTime, "");
	serializeBlockHeight(s.functorMethod, blockHeight, "");
	s.functorMethod(transactionIndex, "");
	s.functorMethod(transactionHash, "");
	s.functorMethod(visible, "");

	if (type == TransactionTypes.OutputType.Key)
	{
	  s.functorMethod(outputKey, "");
	}
  }

}

public class TransactionBlockInfo
{
  public uint height;
  public ulong timestamp;
  public uint transactionIndex;

  public void serialize(ISerializer s)
  {
	serializeBlockHeight(s.functorMethod, height, "height");
	s.functorMethod(timestamp, "timestamp");
	s.functorMethod(transactionIndex, "transactionIndex");
  }
}

public class SpentTransactionOutput : TransactionOutputInformationEx
{
  public TransactionBlockInfo spendingBlock = new TransactionBlockInfo();
  public Crypto.Hash spendingTransactionHash = new Crypto.Hash();
  public uint inputInTransaction;

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getSpendingTransactionHash() const
  public Crypto.Hash getSpendingTransactionHash()
  {
	return spendingTransactionHash;
  }

  public new void serialize(ISerializer s)
  {
	base.serialize(s.functorMethod);
	s.functorMethod(spendingBlock, "spendingBlock");
	s.functorMethod(spendingTransactionHash, "spendingTransactionHash");
	s.functorMethod(inputInTransaction, "inputInTransaction");
  }
}

public enum KeyImageState
{
  Unconfirmed,
  Confirmed,
  Spent
}

public class KeyOutputInfo
{
  public KeyImageState state;
  public uint count;
}

public class TransfersContainer : ITransfersContainer
{
  public TransfersContainer(Currency currency, Logging.ILogger logger, uint transactionSpendableAge)
  {
	  this.m_currentHeight = 0;
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_logger = new Logging.LoggerRef(logger, "TransfersContainer");
	  this.m_transactionSpendableAge = transactionSpendableAge;
  }

  public bool addTransaction(TransactionBlockInfo block, ITransactionReader tx, List<TransactionOutputInformationIn> transfers)
  {

	try
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);

	  if (block.height < m_currentHeight)
	  {
		var message = "Failed to add transaction: block index < m_currentHeight";
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", block " << (int)block.height << ", m_currentHeight " << (int)m_currentHeight;
		throw new System.ArgumentException(message);
	  }

	  if (m_transactions.count(tx.getTransactionHash()) > 0)
	  {
		var message = "Transaction is already added";
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", hash " << tx.getTransactionHash();
		throw new System.ArgumentException(message);
	  }

	  bool added = addTransactionOutputs(block, tx, transfers);
	  added |= addTransactionInputs(block, tx);

	  if (added)
	  {
		addTransaction(block, tx);
	  }

	  if (block.height != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		m_currentHeight = block.height;
	  }

	  return added;
	}
	catch
	{
	  if (m_transactions.count(tx.getTransactionHash()) == 0)
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add transaction, remove transaction transfers, block " << (int)block.height << ", transaction hash " << tx.getTransactionHash();
		deleteTransactionTransfers(tx.getTransactionHash());
	  }

	  throw;
	}
  }
  public bool deleteUnconfirmedTransaction(Hash transactionHash)
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);

	var it = m_transactions.find(transactionHash);
	if (it == m_transactions.end())
	{
	  return false;
	}
	else if (it.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
	  return false;
	}
	else
	{
	  deleteTransactionTransfers(it.transactionHash);
	  m_transactions.erase(it);
	  return true;
	}
  }
  public bool markTransactionConfirmed(TransactionBlockInfo block, Hash transactionHash, List<uint> globalIndices)
  {
	if (block.height == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
	  var message = "Failed to confirm transaction: block height is unconfirmed";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", transaction hash " << transactionHash;
	  throw new System.ArgumentException(message);
	}

	std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);

	var transactionIt = m_transactions.find(transactionHash);
	if (transactionIt == m_transactions.end())
	{
	  return false;
	}

	if (transactionIt.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
	  return false;
	}

	try
	{
	  var txInfo = *transactionIt;
	  txInfo.blockHeight = block.height;
	  txInfo.timestamp = block.timestamp;
	  m_transactions.replace(transactionIt, txInfo);

	  var availableRange = m_unconfirmedTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	  for (var transferIt = availableRange.first; transferIt != availableRange.second;)
	  {
		var transfer = transferIt;
		Debug.Assert(transfer.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
		Debug.Assert(transfer.globalOutputIndex == GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);
		if (transfer.outputInTransaction >= globalIndices.Count)
		{
		  var message = "Failed to confirm transaction: not enough elements in globalIndices";
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", globalIndices.size() " << globalIndices.Count << ", output index " << transfer.outputInTransaction;
		  throw new System.ArgumentException(message);
		}

		transfer.blockHeight = block.height;
		transfer.transactionIndex = block.transactionIndex;
		transfer.globalOutputIndex = globalIndices[transfer.outputInTransaction];

		var result = m_availableTransfers.insert(std::move(transfer));
		result; // Disable unused warning
		Debug.Assert(result.second);

		transferIt = m_unconfirmedTransfers.get<ContainingTransactionIndex>().erase(transferIt);

		if (transfer.type == TransactionTypes.OutputType.Key)
		{
		  updateTransfersVisibility(transfer.keyImage);
		}
	  }

	  auto spendingTransactionIndex = m_spentTransfers.get<SpendingTransactionIndex>();
	  var spentRange = spendingTransactionIndex.equal_range(transactionHash);
	  for (var transferIt = spentRange.first; transferIt != spentRange.second; ++transferIt)
	  {
		var transfer = transferIt;
		Debug.Assert(transfer.spendingBlock.height == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);

		transfer.spendingBlock = block;
		spendingTransactionIndex.replace(transferIt, transfer);
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "markTransactionConfirmed failed: " << e.Message << ", rollback changes, block index " << (int)block.height << ", tx " << transactionHash;

	  var txInfo = *transactionIt;
	  txInfo.blockHeight = GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
	  txInfo.timestamp = 0;
	  m_transactions.replace(transactionIt, txInfo);

	  var availableRange = m_availableTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	  for (var transferIt = availableRange.first; transferIt != availableRange.second;)
	  {
		TransactionOutputInformationEx unconfirmedTransfer = transferIt;
		Debug.Assert(unconfirmedTransfer.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
		Debug.Assert(unconfirmedTransfer.globalOutputIndex != GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);
		unconfirmedTransfer.blockHeight = GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		unconfirmedTransfer.transactionIndex = 0;
		unconfirmedTransfer.globalOutputIndex = GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX;

		var result = m_unconfirmedTransfers.insert(std::move(unconfirmedTransfer));
		result; // Disable unused warning
		Debug.Assert(result.second);

		transferIt = m_availableTransfers.get<ContainingTransactionIndex>().erase(transferIt);

		if (unconfirmedTransfer.type == TransactionTypes.OutputType.Key)
		{
		  updateTransfersVisibility(unconfirmedTransfer.keyImage);
		}
	  }

	  auto spendingTransactionIndex = m_spentTransfers.get<SpendingTransactionIndex>();
	  var spentRange = spendingTransactionIndex.equal_range(transactionHash);
	  for (var transferIt = spentRange.first; transferIt != spentRange.second; ++transferIt)
	  {
		var spentTransfer = transferIt;
		spentTransfer.spendingBlock.height = GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		spentTransfer.spendingBlock.timestamp = 0;
		spentTransfer.spendingBlock.transactionIndex = 0;

		spendingTransactionIndex.replace(transferIt, spentTransfer);
	  }

	  throw;
	}

	return true;
  }

  public List<Hash> detach(uint height)
  {
	// This method expects that WALLET_UNCONFIRMED_TRANSACTION_HEIGHT is a big positive number
	Debug.Assert(height < GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);

	lock (m_mutex)
	{
    
		List<Hash> deletedTransactions = new List<Hash>();
	}
	auto spendingTransactionIndex = m_spentTransfers.get<SpendingTransactionIndex>();
	auto blockHeightIndex = m_transactions.get<1>();
	var it = blockHeightIndex.end();
	while (it != blockHeightIndex.begin())
	{
	  --it;

	  bool doDelete = false;
	  if (it.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		var range = spendingTransactionIndex.equal_range(it.transactionHash);
		for (var spentTransferIt = range.first; spentTransferIt != range.second; ++spentTransferIt)
		{
		  if (spentTransferIt.blockHeight >= height)
		  {
			doDelete = true;
			break;
		  }
		}
	  }
	  else if (it.blockHeight >= height)
	  {
		doDelete = true;
	  }
	  else
	  {
		break;
	  }

	  if (doDelete)
	  {
		deleteTransactionTransfers(it.transactionHash);
		deletedTransactions.emplace_back(it.transactionHash);
		it = blockHeightIndex.erase(it);
	  }
	}

	// TODO: notification on detach
	m_currentHeight = height == 0 ? 0 : height - 1;

	return deletedTransactions;
  }
  public bool advanceHeight(uint height)
  {
	lock (m_mutex)
	{
    
		if (m_currentHeight <= height)
		{
		  m_currentHeight = height;
		  return true;
		}
	}

	return false;
  }

  // ITransfersContainer
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint transfersCount() const override
  public uint transfersCount()
  {
	lock (m_mutex)
	{
		return m_unconfirmedTransfers.size() + m_availableTransfers.size() + m_spentTransfers.size();
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint transactionsCount() const override
  public uint transactionsCount()
  {
	lock (m_mutex)
	{
		return m_transactions.size();
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong balance(uint flags) const override
  public ulong balance(uint flags)
  {
	lock (m_mutex)
	{
		ulong amount = 0;
	}

	foreach (var t in m_availableTransfers)
	{
	  if (t.visible && isIncluded(t, flags))
	  {
		amount += t.amount;
	  }
	}

	if ((flags & IncludeStateLocked) != 0)
	{
	  foreach (var t in m_unconfirmedTransfers)
	  {
		if (t.visible && isIncluded(t.type, IncludeStateLocked, flags))
		{
		  amount += t.amount;
		}
	  }
	}

	return amount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getOutputs(ClassicVector<TransactionOutputInformation>& transfers, uint flags) const override
  public void getOutputs(List<TransactionOutputInformation> transfers, uint flags)
  {
	lock (m_mutex)
	{
		foreach (var t in m_availableTransfers)
		{
		  if (t.visible && isIncluded(t, flags))
		  {
			transfers.Add(t);
		  }
		}
	}

	if ((flags & IncludeStateLocked) != 0)
	{
	  foreach (var t in m_unconfirmedTransfers)
	  {
		if (t.visible && isIncluded(t.type, IncludeStateLocked, flags))
		{
		  transfers.Add(t);
		}
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionInformation(const Hash& transactionHash, TransactionInformation& info, ulong* amountIn = null, ulong* amountOut = null) const override
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on the parameter 'amountIn', so pointers on this parameter are left unchanged:
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on the parameter 'amountOut', so pointers on this parameter are left unchanged:
  public bool getTransactionInformation(Hash transactionHash, ref TransactionInformation info, Nullable<ulong>* amountIn = null, Nullable<ulong>* amountOut = null)
  {
	lock (m_mutex)
	{
		var it = m_transactions.find(transactionHash);
	}
	if (it == m_transactions.end())
	{
	  return false;
	}

	info = it;

	if (amountOut != null)
	{
	  *amountOut = 0;

	  if (info.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		var unconfirmedOutputsRange = m_unconfirmedTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
		for (var it = unconfirmedOutputsRange.first; it != unconfirmedOutputsRange.second; ++it)
		{
		  *amountOut += it.amount;
		}
	  }
	  else
	  {
		var availableOutputsRange = m_availableTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
		for (var it = availableOutputsRange.first; it != availableOutputsRange.second; ++it)
		{
		  *amountOut += it.amount;
		}

		var spentOutputsRange = m_spentTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
		for (var it = spentOutputsRange.first; it != spentOutputsRange.second; ++it)
		{
		  *amountOut += it.amount;
		}
	  }
	}

	if (amountIn != null)
	{
	  *amountIn = 0;
	  var rangeInputs = m_spentTransfers.get<SpendingTransactionIndex>().equal_range(transactionHash);
	  for (var it = rangeInputs.first; it != rangeInputs.second; ++it)
	  {
		*amountIn += it.amount;
	  }
	}

	return true;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionOutputInformation> getTransactionOutputs(const Hash& transactionHash, uint flags) const override
  public List<TransactionOutputInformation> getTransactionOutputs(Hash transactionHash, uint flags)
  {
	lock (m_mutex)
	{
    
		List<TransactionOutputInformation> result = new List<TransactionOutputInformation>();
	}

	var availableRange = m_availableTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	for (var i = availableRange.first; i != availableRange.second; ++i)
	{
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to variables:
//ORIGINAL LINE: const auto& t = *i;
	  auto t = i;
	  if (isIncluded(t, flags))
	  {
		result.push_back(t);
	  }
	}

	if ((flags & IncludeStateLocked) != 0)
	{
	  var unconfirmedRange = m_unconfirmedTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	  for (var i = unconfirmedRange.first; i != unconfirmedRange.second; ++i)
	  {
		if (isIncluded(i.type, IncludeStateLocked, flags))
		{
		  result.push_back(*i);
		}
	  }
	}

	if ((flags & IncludeStateSpent) != 0)
	{
	  var spentRange = m_spentTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	  for (var i = spentRange.first; i != spentRange.second; ++i)
	  {
		if (isIncluded(i.type, IncludeStateAll, flags))
		{
		  result.push_back(*i);
		}
	  }
	}

	return result;
  }
  //only type flags are feasible for this function
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionOutputInformation> getTransactionInputs(const Hash& transactionHash, uint flags) const override
  public List<TransactionOutputInformation> getTransactionInputs(Hash transactionHash, uint flags)
  {
	//only type flags are feasible
	Debug.Assert((flags & IncludeStateAll) == 0);
	flags |= IncludeStateUnlocked;

	lock (m_mutex)
	{
    
		List<TransactionOutputInformation> result = new List<TransactionOutputInformation>();
	}
	var transactionInputsRange = m_spentTransfers.get<SpendingTransactionIndex>().equal_range(transactionHash);
	for (var it = transactionInputsRange.first; it != transactionInputsRange.second; ++it)
	{
	  if (isIncluded(it.type, IncludeStateUnlocked, flags))
	  {
		result.push_back(*it);
	  }
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getUnconfirmedTransactions(ClassicVector<Crypto::Hash>& transactions) const override
  public void getUnconfirmedTransactions(List<Crypto.Hash> transactions)
  {
	lock (m_mutex)
	{
		transactions.Clear();
	}
	foreach (var element in m_transactions)
	{
	  if (element.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		transactions.Add(*reinterpret_cast<const Crypto.Hash>(element.transactionHash));
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionSpentOutputInformation> getSpentOutputs() const override
  public List<TransactionSpentOutputInformation> getSpentOutputs()
  {
	lock (m_mutex)
	{
    
		List<TransactionSpentOutputInformation> spentOutputs = new List<TransactionSpentOutputInformation>();
	}

	spentOutputs.reserve(m_spentTransfers.size());

	foreach (var o in m_spentTransfers)
	{
	  TransactionSpentOutputInformation spentOutput = new TransactionSpentOutputInformation();
	  (TransactionOutputInformation)spentOutput = o;

	  spentOutput.spendingBlockHeight = o.spendingBlock.height;
	  spentOutput.timestamp = o.spendingBlock.timestamp;
	  spentOutput.spendingTransactionHash = o.spendingTransactionHash;
	  spentOutput.keyImage = o.keyImage;
	  spentOutput.inputInTransaction = o.inputInTransaction;

	  spentOutputs.push_back(spentOutput);
	}

	return spentOutputs;
  }

  // IStreamSerializable
  public override void save(std::ostream os)
  {
	lock (m_mutex)
	{
		StdOutputStream stream = new StdOutputStream(os);
	}
	CryptoNote.BinaryOutputStreamSerializer s = new CryptoNote.BinaryOutputStreamSerializer(stream);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	s.functorMethod(const_cast<uint&>(GlobalMembers.TRANSFERS_CONTAINER_STORAGE_VERSION), "version");

	s.functorMethod(m_currentHeight, "height");
	CryptoNote.GlobalMembers.writeSequence<TransactionInformation>(m_transactions.begin(), m_transactions.end(), "transactions", s.functorMethod);
	CryptoNote.GlobalMembers.writeSequence<TransactionOutputInformationEx>(m_unconfirmedTransfers.begin(), m_unconfirmedTransfers.end(), "unconfirmedTransfers", s.functorMethod);
	CryptoNote.GlobalMembers.writeSequence<TransactionOutputInformationEx>(m_availableTransfers.begin(), m_availableTransfers.end(), "availableTransfers", s.functorMethod);
	CryptoNote.GlobalMembers.writeSequence<SpentTransactionOutput>(m_spentTransfers.begin(), m_spentTransfers.end(), "spentTransfers", s.functorMethod);
  }
  public override void load(std::istream in)
  {
	lock (m_mutex)
	{
		StdInputStream stream = new StdInputStream(in);
	}
	CryptoNote.BinaryInputStreamSerializer s = new CryptoNote.BinaryInputStreamSerializer(stream);

	uint version = 0;
	s.functorMethod(version, "version");

	if (version > GlobalMembers.TRANSFERS_CONTAINER_STORAGE_VERSION)
	{
	  var message = "Failed to load: unsupported version";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", version " << (int)version << ", supported version " << (int)GlobalMembers.TRANSFERS_CONTAINER_STORAGE_VERSION;
	  throw new System.Exception(message);
	}

	uint currentHeight = 0;
	TransactionMultiIndex transactions = new TransactionMultiIndex();
	UnconfirmedTransfersMultiIndex unconfirmedTransfers = new UnconfirmedTransfersMultiIndex();
	AvailableTransfersMultiIndex availableTransfers = new AvailableTransfersMultiIndex();
	SpentTransfersMultiIndex spentTransfers = new SpentTransfersMultiIndex();

	s.functorMethod(currentHeight, "height");
	CryptoNote.GlobalMembers.readSequence<TransactionInformation>(std::inserter(transactions, transactions.end()), "transactions", s.functorMethod);
	CryptoNote.GlobalMembers.readSequence<TransactionOutputInformationEx>(std::inserter(unconfirmedTransfers, unconfirmedTransfers.end()), "unconfirmedTransfers", s.functorMethod);
	CryptoNote.GlobalMembers.readSequence<TransactionOutputInformationEx>(std::inserter(availableTransfers, availableTransfers.end()), "availableTransfers", s.functorMethod);
	CryptoNote.GlobalMembers.readSequence<SpentTransactionOutput>(std::inserter(spentTransfers, spentTransfers.end()), "spentTransfers", s.functorMethod);

	m_currentHeight = currentHeight;
	m_transactions = std::move(transactions);
	m_unconfirmedTransfers = std::move(unconfirmedTransfers);
	m_availableTransfers = std::move(availableTransfers);
	m_spentTransfers = std::move(spentTransfers);

	// Repair the container if it was broken while handling addTransaction() in previous version of the code
	// Hope it isn't necessary anymore
	//repair();
  }

  private class ContainingTransactionIndex
  {
  }
  private class SpendingTransactionIndex
  {
  }
  private class SpentOutputDescriptorIndex
  {
  }

  private typedef boost::multi_index_container< TransactionInformation, boost::multi_index.indexed_by< boost::multi_index.hashed_unique<BOOST_MULTI_INDEX_MEMBER(TransactionInformation, Crypto.Hash, transactionHash)>, boost::multi_index.ordered_non_unique<BOOST_MULTI_INDEX_MEMBER(TransactionInformation, uint, blockHeight)>> > TransactionMultiIndex = new typedef();





  /**
   * \pre m_mutex is locked.
   */
  private void addTransaction(TransactionBlockInfo block, ITransactionReader tx)
  {
	var txHash = tx.getTransactionHash();

	TransactionInformation txInfo = new TransactionInformation();
	txInfo.blockHeight = block.height;
	txInfo.timestamp = block.timestamp;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: txInfo.transactionHash = txHash;
	txInfo.transactionHash.CopyFrom(txHash);
	txInfo.unlockTime = tx.getUnlockTime();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: txInfo.publicKey = tx.getTransactionPublicKey();
	txInfo.publicKey.CopyFrom(tx.getTransactionPublicKey());
	txInfo.totalAmountIn = tx.getInputTotalAmount();
	txInfo.totalAmountOut = tx.getOutputTotalAmount();
	txInfo.extra = tx.getExtra();

	if (!tx.getPaymentId(txInfo.paymentId))
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: txInfo.paymentId = NULL_HASH;
	  txInfo.paymentId.CopyFrom(GlobalMembers.NULL_HASH);
	}

	var result = m_transactions.insert(std::move(txInfo));
	result; // Disable unused warning
	Debug.Assert(result.second);
  }

  /**
   * \pre m_mutex is locked.
   */
  private bool addTransactionOutputs(TransactionBlockInfo block, ITransactionReader tx, List<TransactionOutputInformationIn> transfers)
  {
	bool outputsAdded = false;

	var txHash = tx.getTransactionHash();
	bool transactionIsUnconfimed = (block.height == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
	foreach (var transfer in transfers)
	{
	  Debug.Assert(transfer.outputInTransaction < tx.getOutputCount());
	  Debug.Assert(transfer.type == tx.getOutputType(transfer.outputInTransaction));
	  Debug.Assert(transfer.amount > 0);

	  bool transferIsUnconfirmed = (transfer.globalOutputIndex == GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);
	  if (transactionIsUnconfimed != transferIsUnconfirmed)
	  {
		var message = "Failed to add transaction output: globalOutputIndex is invalid";
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", globalOutputIndex " << (int)transfer.globalOutputIndex << ", transaction is confirmed " << transferIsUnconfirmed;
		throw new System.ArgumentException(message);
	  }

	  TransactionOutputInformationEx info = new TransactionOutputInformationEx();
	  (TransactionOutputInformationIn)info = transfer;
	  info.blockHeight = block.height;
	  info.transactionIndex = block.transactionIndex;
	  info.unlockTime = tx.getUnlockTime();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: info.transactionHash = txHash;
	  info.transactionHash.CopyFrom(txHash);
	  info.visible = true;

	  if (transferIsUnconfirmed)
	  {
		var result = m_unconfirmedTransfers.insert(std::move(info));
		result; // Disable unused warning
		Debug.Assert(result.second);
	  }
	  else
	  {
		if (info.type == TransactionTypes.OutputType.Key)
		{
		  bool duplicate = false;
		  SpentOutputDescriptor descriptor = new SpentOutputDescriptor(transfer);

		  var availableRange = m_availableTransfers.get<SpentOutputDescriptorIndex>().equal_range(descriptor);
		  for (var it = availableRange.first; !duplicate && it != availableRange.second; ++it)
		  {
			if (it.transactionHash == info.transactionHash && it.outputInTransaction == info.outputInTransaction)
			{
			  duplicate = true;
			}
		  }

		  var spentRange = m_spentTransfers.get<SpentOutputDescriptorIndex>().equal_range(descriptor);
		  for (var it = spentRange.first; !duplicate && it != spentRange.second; ++it)
		  {
			if (it.transactionHash == info.transactionHash && it.outputInTransaction == info.outputInTransaction)
			{
			  duplicate = true;
			}
		  }

		  if (duplicate)
		  {
			var message = "Failed to add transaction output: key output already exists";
			m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", transaction hash " << info.transactionHash << ", output index " << (int)info.outputInTransaction << ", key image " << info.keyImage;
			throw new System.Exception(message);
		  }
		}

		var result = m_availableTransfers.insert(std::move(info));
		result; // Disable unused warning
		Debug.Assert(result.second);
	  }

	  if (info.type == TransactionTypes.OutputType.Key)
	  {
		updateTransfersVisibility(info.keyImage);
	  }

	  outputsAdded = true;
	}

	return outputsAdded;
  }

  /**
   * \pre m_mutex is locked.
   */
  private bool addTransactionInputs(TransactionBlockInfo block, ITransactionReader tx)
  {
	bool inputsAdded = false;

	for (uint i = 0; i < tx.getInputCount(); ++i)
	{
	  var inputType = tx.getInputType(i);

	  if (inputType == TransactionTypes.InputType.Key)
	  {
		KeyInput input = new KeyInput();
		tx.getInput(i, input);

		SpentOutputDescriptor descriptor = new SpentOutputDescriptor(input.keyImage);
		var spentRange = m_spentTransfers.get<SpentOutputDescriptorIndex>().equal_range(descriptor);
		if (std::distance(spentRange.first, spentRange.second) > 0)
		{
		  Debug.Assert(std::distance(spentRange.first, spentRange.second) == 1);
		  auto spentOutput = spentRange.first;
		  var message = "Failed add key input: key image already spent";
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", key image " << input.keyImage << '\n' << "    rejected transaction" << ": hash " << tx.getTransactionHash() << ", block " << (int)block.height << ", transaction index " << (int)block.transactionIndex << ", input " << (int)i << '\n' << "    spending transaction" << ": hash " << spentOutput.spendingTransactionHash << ", block " << spentOutput.spendingBlock.height << ", input " << spentOutput.inputInTransaction << '\n' << "    spent output        " << ": hash " << spentOutput.transactionHash << ", block " << spentOutput.blockHeight << ", transaction index " << spentOutput.transactionIndex << ", output " << spentOutput.outputInTransaction << ", amount " << m_currency.formatAmount(spentOutput.amount);
		  throw new System.Exception(message);
		}

		var availableRange = m_availableTransfers.get<SpentOutputDescriptorIndex>().equal_range(descriptor);
		var unconfirmedRange = m_unconfirmedTransfers.get<SpentOutputDescriptorIndex>().equal_range(descriptor);
		uint availableCount = std::distance(availableRange.first, availableRange.second);
		uint unconfirmedCount = std::distance(unconfirmedRange.first, unconfirmedRange.second);

		if (availableCount == 0)
		{
		  if (unconfirmedCount > 0)
		  {
			var message = "Failed to add key input: spend output of unconfirmed transaction";
			m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", key image " << input.keyImage;
			throw new System.Exception(message);
		  }
		  else
		  {
			// This input doesn't spend any transfer from this container
			continue;
		  }
		}

		auto outputDescriptorIndex = m_availableTransfers.get<SpentOutputDescriptorIndex>();
		var availableOutputsRange = outputDescriptorIndex.equal_range(new SpentOutputDescriptor(input.keyImage));

		var iteratorList = GlobalMembers.createTransferIteratorList(availableOutputsRange);
		iteratorList.sort();
		var spendingTransferIt = iteratorList.findFirstByAmount(input.amount);

		if (spendingTransferIt == availableOutputsRange.second)
		{
		  var message = "Failed to add key input: invalid amount";
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", key image " << input.keyImage << ", amount " << m_currency.formatAmount(input.amount);
		  throw new System.Exception(message);
		}

		Debug.Assert(spendingTransferIt.keyImage == input.keyImage);
		copyToSpent(block, tx, i, *spendingTransferIt);
		// erase from available outputs
		outputDescriptorIndex.erase(spendingTransferIt);
		updateTransfersVisibility(input.keyImage);

		inputsAdded = true;
	  }
	  else
	  {
		Debug.Assert(inputType == TransactionTypes.InputType.Generating);
	  }
	}

	return inputsAdded;
  }

  /**
   * \pre m_mutex is locked.
   */
  private void deleteTransactionTransfers(Hash transactionHash)
  {
	auto spendingTransactionIndex = m_spentTransfers.get<SpendingTransactionIndex>();
	var spentTransfersRange = spendingTransactionIndex.equal_range(transactionHash);
	for (var it = spentTransfersRange.first; it != spentTransfersRange.second;)
	{
	  Debug.Assert(it.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
	  Debug.Assert(it.globalOutputIndex != GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);

	  var result = m_availableTransfers.insert((TransactionOutputInformationEx)(*it));
	  Debug.Assert(result.second);
	  it = spendingTransactionIndex.erase(it);

	  if (result.first.type == TransactionTypes.OutputType.Key)
	  {
		updateTransfersVisibility(result.first.keyImage);
	  }
	}

	var unconfirmedTransfersRange = m_unconfirmedTransfers.get<ContainingTransactionIndex>().equal_range(transactionHash);
	for (var it = unconfirmedTransfersRange.first; it != unconfirmedTransfersRange.second;)
	{
	  if (it.type == TransactionTypes.OutputType.Key)
	  {
		KeyImage keyImage = it.keyImage;
		it = m_unconfirmedTransfers.get<ContainingTransactionIndex>().erase(it);
		updateTransfersVisibility(keyImage);
	  }
	  else
	  {
		it = m_unconfirmedTransfers.get<ContainingTransactionIndex>().erase(it);
	  }
	}

	auto transactionTransfersIndex = m_availableTransfers.get<ContainingTransactionIndex>();
	var transactionTransfersRange = transactionTransfersIndex.equal_range(transactionHash);
	for (var it = transactionTransfersRange.first; it != transactionTransfersRange.second;)
	{
	  if (it.type == TransactionTypes.OutputType.Key)
	  {
		KeyImage keyImage = it.keyImage;
		it = transactionTransfersIndex.erase(it);
		updateTransfersVisibility(keyImage);
	  }
	  else
	  {
		it = transactionTransfersIndex.erase(it);
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isSpendTimeUnlocked(ulong unlockTime) const
  private bool isSpendTimeUnlocked(ulong unlockTime)
  {
	if (unlockTime < m_currency.maxBlockHeight())
	{
	  // interpret as block index
	  return m_currentHeight + m_currency.lockedTxAllowedDeltaBlocks() >= unlockTime;
	}
	else
	{
	  //interpret as time
	  ulong current_time = (ulong)time(null);
	  return current_time + m_currency.lockedTxAllowedDeltaSeconds() >= unlockTime;
	}

	return false;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isIncluded(const TransactionOutputInformationEx& info, uint flags) const
  private bool isIncluded(TransactionOutputInformationEx info, uint flags)
  {
	uint state;
	if (info.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT || !isSpendTimeUnlocked(info.unlockTime))
	{
	  state = IncludeStateLocked;
	}
	else if (m_currentHeight < info.blockHeight + m_transactionSpendableAge)
	{
	  state = IncludeStateSoftLocked;
	}
	else
	{
	  state = IncludeStateUnlocked;
	}

	return isIncluded(info.type, state, flags);
  }
  private static bool isIncluded(TransactionTypes.OutputType type, uint state, uint flags)
  {
	return (((flags & IncludeTypeKey) != 0 && type == TransactionTypes.OutputType.Key)) && ((flags & state) != 0);
  }

  /**
   * \pre m_mutex is locked.
   */
  private void updateTransfersVisibility(KeyImage keyImage)
  {
	auto unconfirmedIndex = m_unconfirmedTransfers.get<SpentOutputDescriptorIndex>();
	auto availableIndex = m_availableTransfers.get<SpentOutputDescriptorIndex>();
	auto spentIndex = m_spentTransfers.get<SpentOutputDescriptorIndex>();

	SpentOutputDescriptor descriptor = new SpentOutputDescriptor(keyImage);
	var unconfirmedRange = unconfirmedIndex.equal_range(descriptor);
	var availableRange = availableIndex.equal_range(descriptor);
	var spentRange = spentIndex.equal_range(descriptor);

	uint unconfirmedCount = std::distance(unconfirmedRange.first, unconfirmedRange.second);
	uint availableCount = std::distance(availableRange.first, availableRange.second);
	uint spentCount = std::distance(spentRange.first, spentRange.second);
	Debug.Assert(spentCount == 0 || spentCount == 1);

	if (spentCount > 0)
	{
	  GlobalMembers.updateVisibility(unconfirmedIndex, unconfirmedRange, false);
	  GlobalMembers.updateVisibility(availableIndex, availableRange, false);
	  GlobalMembers.updateVisibility(spentIndex, spentRange, true);
	}
	else if (availableCount > 0)
	{
	  GlobalMembers.updateVisibility(unconfirmedIndex, unconfirmedRange, false);
	  GlobalMembers.updateVisibility(availableIndex, availableRange, false);

	  var iteratorList = GlobalMembers.createTransferIteratorList(availableRange);
	  var earliestTransferIt = iteratorList.minElement();
	  Debug.Assert(earliestTransferIt != availableRange.second);

	  var earliestTransfer = *earliestTransferIt;
	  earliestTransfer.visible = true;
	  availableIndex.replace(earliestTransferIt, earliestTransfer);
	}
	else
	{
	  GlobalMembers.updateVisibility(unconfirmedIndex, unconfirmedRange, unconfirmedCount == 1);
	}
  }


  /**
   * \pre m_mutex is locked.
   */
  private void copyToSpent(TransactionBlockInfo block, ITransactionReader tx, uint inputIndex, TransactionOutputInformationEx output)
  {
	Debug.Assert(output.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
	Debug.Assert(output.globalOutputIndex != GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);

	SpentTransactionOutput spentOutput = new SpentTransactionOutput();
	(TransactionOutputInformationEx)spentOutput = output;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: spentOutput.spendingBlock = block;
	spentOutput.spendingBlock.CopyFrom(block);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: spentOutput.spendingTransactionHash = tx.getTransactionHash();
	spentOutput.spendingTransactionHash.CopyFrom(tx.getTransactionHash());
	spentOutput.inputInTransaction = (uint)inputIndex;
	var result = m_spentTransfers.insert(std::move(spentOutput));
	result; // Disable unused warning
	Debug.Assert(result.second);
  }
  private void repair()
  {
	uint deletedInputCount = 0;
	for (var it = m_spentTransfers.begin(); it != m_spentTransfers.end();)
	{
	  Debug.Assert(it.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
	  Debug.Assert(it.globalOutputIndex != GlobalMembers.UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX);

	  if (m_transactions.count(it.spendingTransactionHash) == 0)
	  {
		bool isInputConfirmed = it.spendingBlock.height != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Orphan input found, remove it and return output spent by them to available outputs:\n" << "    input       " << ": block " << std::setw(7) << (isInputConfirmed ? (int)it.spendingBlock.height : -1) << ", transaction index " << std::setw(2) << (isInputConfirmed ? (int)it.spendingBlock.transactionIndex : -1) << ", transaction hash " << it.spendingTransactionHash << ", input " << std::setw(3) << it.inputInTransaction << '\n' << "    spent output" << ": block " << std::setw(7) << it.blockHeight << ", transaction index " << std::setw(2) << it.transactionIndex << ", transaction hash " << it.transactionHash << ", output " << std::setw(2) << it.outputInTransaction;

		var result = m_availableTransfers.insert((TransactionOutputInformationEx)(*it));
		Debug.Assert(result.second);
		it = m_spentTransfers.erase(it);

		if (result.first.type == TransactionTypes.OutputType.Key)
		{
		  updateTransfersVisibility(result.first.keyImage);
		}

		++deletedInputCount;
	  }
	  else
	  {
		++it;
	  }
	}

	uint deletedUnconfirmedOutputCount = 0;
	for (var it = m_unconfirmedTransfers.begin(); it != m_unconfirmedTransfers.end();)
	{
	  Debug.Assert(it.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);

	  if (m_transactions.count(it.transactionHash) == 0)
	  {
		m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Orphan unconfirmed output found, remove it" << ", transaction hash " << it.transactionHash << ", output " << std::setw(2) << it.outputInTransaction << ", amount " << m_currency.formatAmount(it.amount);

		if (it.type == TransactionTypes.OutputType.Key)
		{
		  KeyImage keyImage = it.keyImage;
		  it = m_unconfirmedTransfers.erase(it);
		  updateTransfersVisibility(keyImage);
		}
		else
		{
		  it = m_unconfirmedTransfers.erase(it);
		}

		++deletedUnconfirmedOutputCount;
	  }
	  else
	  {
		++it;
	  }
	}

	uint deletedAvailableOutputCount = 0;
	for (var it = m_availableTransfers.begin(); it != m_availableTransfers.end();)
	{
	  Debug.Assert(it.blockHeight != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);

	  if (m_transactions.count(it.transactionHash) == 0)
	  {
		m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Orphan output found, remove it" << ", block " << std::setw(7) << it.blockHeight << ", transaction index " << std::setw(2) << it.transactionIndex << ", transaction hash " << it.transactionHash << ", output " << std::setw(2) << it.outputInTransaction << ", amount " << m_currency.formatAmount(it.amount);

		if (it.type == TransactionTypes.OutputType.Key)
		{
		  KeyImage keyImage = it.keyImage;
		  it = m_availableTransfers.erase(it);
		  updateTransfersVisibility(keyImage);
		}
		else
		{
		  it = m_availableTransfers.erase(it);
		}

		++deletedAvailableOutputCount;
	  }
	  else
	  {
		++it;
	  }
	}

	if (deletedInputCount + deletedUnconfirmedOutputCount + deletedAvailableOutputCount > 0)
	{
	  m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Repair finished:\n" << "    Deleted inputs " << (int)deletedInputCount << ", total inputs " << m_spentTransfers.size() << '\n' << "    Deleted unconfirmed outputs " << (int)deletedUnconfirmedOutputCount << ", total unconfirmed outputs " << m_unconfirmedTransfers.size() << '\n' << "    Deleted available outputs " << (int)deletedAvailableOutputCount << ", total available outputs " << m_availableTransfers.size();
	}
	else
	{
	  m_logger.functorMethod(DEBUGGING) << "Repair finished";
	}
  }

  private TransactionMultiIndex m_transactions = new TransactionMultiIndex();
  private readonly boost::multi_index_container<TransactionOutputInformationEx, boost::multi_index.indexed_by<boost::multi_index.hashed_non_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, TransactionOutputInformationEx.getTransactionHash>>>> m_unconfirmedTransfers = new boost::multi_index_container<TransactionOutputInformationEx, boost::multi_index.indexed_by<boost::multi_index.hashed_non_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, TransactionOutputInformationEx.getTransactionHash>>>>();
  private readonly boost::multi_index_container<TransactionOutputInformationEx, boost::multi_index.indexed_by<boost::multi_index.hashed_non_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, TransactionOutputInformationEx.getTransactionHash>>>> m_availableTransfers = new boost::multi_index_container<TransactionOutputInformationEx, boost::multi_index.indexed_by<boost::multi_index.hashed_non_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, TransactionOutputInformationEx.getTransactionHash>>>>();
  private readonly boost::multi_index_container<SpentTransactionOutput, boost::multi_index.indexed_by<boost::multi_index.hashed_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, SpentTransactionOutput.getTransactionHash>>, boost::multi_index.hashed_non_unique <boost::multi_index.tag<SpendingTransactionIndex>, boost::multi_index.const_mem_fun <SpentTransactionOutput, const Crypto.Hash, SpentTransactionOutput.getSpendingTransactionHash>>>> m_spentTransfers = new boost::multi_index_container<SpentTransactionOutput, boost::multi_index.indexed_by<boost::multi_index.hashed_unique< boost::multi_index.tag<SpentOutputDescriptorIndex>, boost::multi_index.const_mem_fun< TransactionOutputInformationEx, SpentOutputDescriptor, TransactionOutputInformationEx.getSpentOutputDescriptor>, SpentOutputDescriptorHasher >, boost::multi_index.hashed_non_unique<boost::multi_index.tag<ContainingTransactionIndex>, boost::multi_index.const_mem_fun<TransactionOutputInformationEx, Crypto.Hash, SpentTransactionOutput.getTransactionHash>>, boost::multi_index.hashed_non_unique <boost::multi_index.tag<SpendingTransactionIndex>, boost::multi_index.const_mem_fun <SpentTransactionOutput, const Crypto.Hash, SpentTransactionOutput.getSpendingTransactionHash>>>>();

  private uint m_currentHeight; // current height is needed to check if a transfer is unlocked
  private uint m_transactionSpendableAge;
  private readonly CryptoNote.Currency m_currency;
  private object m_mutex = new object();
  private Logging.LoggerRef m_logger = new Logging.LoggerRef();
}

}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename TIterator>
  public class TransferIteratorList <TIterator>
  {
	public TransferIteratorList(TIterator begin, TIterator end)
	{
		this.m_end = end;
	  for (var it = begin; it != end; ++it)
	  {
		m_list.emplace_back(it);
	  }
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public TransferIteratorList(TransferIteratorList<TIterator>&& other)
	{
	  m_list = std::move(other.m_list);
	  m_end = std::move(other.m_end);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
	public static TransferIteratorList operator = (TransferIteratorList<TIterator>&& other)
	{
	  m_list = std::move(other.m_list);
	  m_end = std::move(other.m_end);
	  return this;
	}

	public void sort()
	{
//C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
//ORIGINAL LINE: std::sort(m_list.begin(), m_list.end(), &TransferIteratorList::lessTIterator);
	  m_list.Sort(TransferIteratorList.lessTIterator);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TIterator findFirstByAmount(ulong amount) const
	public TIterator findFirstByAmount(ulong amount)
	{
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: auto listIt = std::find_if(m_list.begin(), m_list.end(), [amount](const TIterator& it)
	  var listIt = std::find_if(m_list.GetEnumerator(), m_list.end(), (TIterator it) =>
	  {
		return it.amount == amount;
	  });
	  return listIt == m_list.end() ? m_end : *listIt;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TIterator minElement() const
	public TIterator minElement()
	{
	  var listIt = std::min_element(m_list.GetEnumerator(), m_list.end(), TransferIteratorList.lessTIterator);
	  return listIt == m_list.end() ? m_end : *listIt;
	}

	private static bool lessTIterator(TIterator it1, TIterator it2)
	{
	  return (it1.blockHeight < it2.blockHeight) || (it1.blockHeight == it2.blockHeight && it1.transactionIndex < it2.transactionIndex);
	}

	private List<TIterator> m_list = new List<TIterator>();
	private TIterator m_end = new TIterator();
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename TIterator>

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename C, typename T>

}
