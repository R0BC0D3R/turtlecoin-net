// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(uint) <= sizeof(type), "Size of " #type " must be at least that of uint"); inline uint hash_value(const type &_v) { return reinterpret_cast<const uint &>(_v); } } namespace std { template<> struct hash<Crypto::type> { uint operator()(const Crypto::type &_v) const { return reinterpret_cast<const uint &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

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


namespace CryptoNote
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
public interface IObservable <T>
{
  void addObserver(T observer);
  void removeObserver(T observer);
}

}

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



namespace CryptoNote
{

public interface IStreamSerializable
{
  void save(std::ostream os);
  void load(std::istream in);
}

}

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



namespace CryptoNote
{

public class TransactionInformation
{
  // transaction info
  public Crypto.Hash transactionHash = new Crypto.Hash();
  public Crypto.PublicKey publicKey = new Crypto.PublicKey();
  public uint blockHeight;
  public ulong timestamp;
  public ulong unlockTime;
  public ulong totalAmountIn;
  public ulong totalAmountOut;
  public List<byte> extra = new List<byte>();
  public Crypto.Hash paymentId = new Crypto.Hash();
}


public class TransactionOutputInformation
{
  // output info
  public TransactionTypes.OutputType type;
  public ulong amount;
  public uint globalOutputIndex;
  public uint outputInTransaction;

  // transaction info
  public Crypto.Hash transactionHash = new Crypto.Hash();
  public Crypto.PublicKey transactionPublicKey = new Crypto.PublicKey();

  public Crypto.PublicKey outputKey = new Crypto.PublicKey();
}

public class TransactionSpentOutputInformation: TransactionOutputInformation
{
  public uint spendingBlockHeight;
  public ulong timestamp;
  public Crypto.Hash spendingTransactionHash = new Crypto.Hash();
  public Crypto.KeyImage keyImage = new Crypto.KeyImage(); //!< \attention Used only for TransactionTypes::OutputType::Key
  public uint inputInTransaction;
}

public interface ITransfersContainer : IStreamSerializable
{
//C++ TO C# CONVERTER TODO TASK: Enums within interfaces are not allowed in C#. Move this enum elsewhere:
  public enum Flags : uint
  {
	// state
	IncludeStateUnlocked = 0x01,
	IncludeStateLocked = 0x02,
	IncludeStateSoftLocked = 0x04,
	IncludeStateSpent = 0x08,
	// output type
	IncludeTypeKey = 0x100,
	// combinations
	IncludeStateAll = 0xff,
	IncludeTypeAll = 0xff00,

	IncludeKeyUnlocked = IncludeTypeKey | IncludeStateUnlocked,
	IncludeKeyNotUnlocked = IncludeTypeKey | IncludeStateLocked | IncludeStateSoftLocked,

	IncludeAllLocked = IncludeTypeAll | IncludeStateLocked | IncludeStateSoftLocked,
	IncludeAllUnlocked = IncludeTypeAll | IncludeStateUnlocked,
	IncludeAll = IncludeTypeAll | IncludeStateAll,

	IncludeDefault = IncludeKeyUnlocked
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint transfersCount() const = 0;
  uint transfersCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint transactionsCount() const = 0;
  uint transactionsCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong balance(uint flags = IncludeDefault) const = 0;
  ulong balance(uint flags = IncludeDefault);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getOutputs(ClassicVector<TransactionOutputInformation>& transfers, uint flags = IncludeDefault) const = 0;
  void getOutputs(List<TransactionOutputInformation> transfers, uint flags = IncludeDefault);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionInformation(const Crypto::Hash& transactionHash, TransactionInformation& info, ulong* amountIn = null, ulong* amountOut = null) const = 0;
  bool getTransactionInformation(Crypto.Hash transactionHash, TransactionInformation info, ref Nullable<ulong> amountIn, ref Nullable<ulong> amountOut);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionOutputInformation> getTransactionOutputs(const Crypto::Hash& transactionHash, uint flags = IncludeDefault) const = 0;
  List<TransactionOutputInformation> getTransactionOutputs(Crypto.Hash transactionHash, uint flags = IncludeDefault);
  //only type flags are feasible for this function
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionOutputInformation> getTransactionInputs(const Crypto::Hash& transactionHash, uint flags) const = 0;
  List<TransactionOutputInformation> getTransactionInputs(Crypto.Hash transactionHash, uint flags);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getUnconfirmedTransactions(ClassicVector<Crypto::Hash>& transactions) const = 0;
  void getUnconfirmedTransactions(List<Crypto.Hash> transactions);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionSpentOutputInformation> getSpentOutputs() const = 0;
  List<TransactionSpentOutputInformation> getSpentOutputs();
}

}


namespace CryptoNote
{

public class SynchronizationStart
{
  public ulong timestamp;
  public ulong height;
}

public class AccountSubscription
{
  public AccountKeys keys = new AccountKeys();
  public SynchronizationStart syncStart = new SynchronizationStart();
  public uint transactionSpendableAge;
}

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ITransfersSubscription;

public class ITransfersObserver
{
  public virtual void onError(ITransfersSubscription @object, uint height, std::error_code ec)
  {
  }

  public virtual void onTransactionUpdated(ITransfersSubscription @object, Crypto.Hash transactionHash)
  {
  }

  /**
   * \note The sender must guarantee that onTransactionDeleted() is called only after onTransactionUpdated() is called
   * for the same \a transactionHash.
   */
  public virtual void onTransactionDeleted(ITransfersSubscription @object, Crypto.Hash transactionHash)
  {
  }
}

public abstract class ITransfersSubscription : IObservable < ITransfersObserver >, System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract AccountPublicAddress getAddress();
  public abstract ITransfersContainer getContainer();
}

public class ITransfersSynchronizerObserver
{
  public virtual void onBlocksAdded(Crypto.PublicKey viewPublicKey, List<Crypto.Hash> blockHashes)
  {
  }
  public virtual void onBlockchainDetach(Crypto.PublicKey viewPublicKey, uint blockIndex)
  {
  }
  public virtual void onTransactionDeleteBegin(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash)
  {
  }
  public virtual void onTransactionDeleteEnd(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash)
  {
  }
  public virtual void onTransactionUpdated(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash, List<ITransfersContainer> containers)
  {
  }
}

public abstract class ITransfersSynchronizer : IStreamSerializable, System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract ITransfersSubscription addSubscription(AccountSubscription acc);
  public abstract bool removeSubscription(AccountPublicAddress acc);
  public abstract void getSubscriptions(List<AccountPublicAddress> subscriptions);
  // returns nullptr if address is not found
  public abstract ITransfersSubscription getSubscription(AccountPublicAddress acc);
  public abstract List<Crypto.Hash> getViewKeyKnownBlocks(Crypto.PublicKey publicViewKey);
}

}


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct CompleteBlock;

public class IBlockchainSynchronizerObserver
{
  public virtual void synchronizationProgressUpdated(uint processedBlockCount, uint totalBlockCount)
  {
  }
  public virtual void synchronizationCompleted(std::error_code result)
  {
  }
}

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IBlockchainConsumerObserver;

public abstract class IBlockchainConsumer : IObservable<IBlockchainConsumerObserver>, System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public abstract SynchronizationStart getSyncStart();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const ClassicUnorderedSet<Crypto::Hash>& getKnownPoolTxIds() const = 0;
  public abstract HashSet<Crypto.Hash> getKnownPoolTxIds();
  public abstract void onBlockchainDetach(uint height);
  public abstract uint onNewBlocks(CompleteBlock blocks, uint startHeight, uint count);
  public abstract std::error_code onPoolUpdated(List<std::unique_ptr<ITransactionReader>> addedTransactions, List<Crypto.Hash> deletedTransactions);

  public abstract std::error_code addUnconfirmedTransaction(ITransactionReader transaction);
  public abstract void removeUnconfirmedTransaction(Crypto.Hash transactionHash);
}

public class IBlockchainConsumerObserver
{
  public virtual void onBlocksAdded(IBlockchainConsumer consumer, List<Crypto.Hash> blockHashes)
  {
  }
  public virtual void onBlockchainDetach(IBlockchainConsumer consumer, uint blockIndex)
  {
  }
  public virtual void onTransactionDeleteBegin(IBlockchainConsumer consumer, Crypto.Hash transactionHash)
  {
  }
  public virtual void onTransactionDeleteEnd(IBlockchainConsumer consumer, Crypto.Hash transactionHash)
  {
  }
  public virtual void onTransactionUpdated(IBlockchainConsumer consumer, Crypto.Hash transactionHash, List<ITransfersContainer> containers)
  {
  }
}

public interface IBlockchainSynchronizer : IObservable<IBlockchainSynchronizerObserver>, IStreamSerializable
{
  void addConsumer(IBlockchainConsumer consumer);
  bool removeConsumer(IBlockchainConsumer consumer);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IStreamSerializable* getConsumerState(IBlockchainConsumer* consumer) const = 0;
  IStreamSerializable getConsumerState(IBlockchainConsumer consumer);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getConsumerKnownBlocks(IBlockchainConsumer& consumer) const = 0;
  List<Crypto.Hash> getConsumerKnownBlocks(IBlockchainConsumer consumer);

  std::future<std::error_code> addUnconfirmedTransaction(ITransactionReader transaction);
  std::future removeUnconfirmedTransaction(Crypto.Hash transactionHash);

  void start();
  void stop();
}

}
