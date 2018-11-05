// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


namespace CryptoNote
{

public class INodeObserver : System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public virtual void peerCountUpdated(uint count)
  {
  }
  public virtual void localBlockchainUpdated(uint height)
  {
  }
  public virtual void lastKnownBlockHeightUpdated(uint height)
  {
  }
  public virtual void poolChanged()
  {
  }
  public virtual void blockchainSynchronized(uint topHeight)
  {
  }
  public virtual void chainSwitched(uint newTopIndex, uint commonRoot, List<Crypto.Hash> hashes)
  {
  }
}

public class OutEntry
{
  public uint outGlobalIndex;
  public Crypto.PublicKey outKey = new Crypto.PublicKey();
}

public class OutsForAmount
{
  public ulong amount;
  public List<OutEntry> outs = new List<OutEntry>();
}

public class TransactionShortInfo
{
  public Crypto.Hash txId = new Crypto.Hash();
  public TransactionPrefix txPrefix = new TransactionPrefix();
}

public class BlockShortEntry
{
  public Crypto.Hash blockHash = new Crypto.Hash();
  public bool hasBlock;
  public CryptoNote.BlockTemplate block = new CryptoNote.BlockTemplate();
  public List<TransactionShortInfo> txsShortInfo = new List<TransactionShortInfo>();
}

public class BlockHeaderInfo
{
  public uint index;
  public byte majorVersion;
  public byte minorVersion;
  public ulong timestamp;
  public Crypto.Hash hash = new Crypto.Hash();
  public Crypto.Hash prevHash = new Crypto.Hash();
  public uint nonce;
  public bool isAlternative;
  public uint depth; // last block index = current block index + depth
  public Difficulty difficulty = new Difficulty();
  public ulong reward;
}

public abstract class INode : System.IDisposable
{
  public delegate void Callback(std::error_code UnnamedParameter);

  public virtual void Dispose()
  {
  }
  public abstract bool addObserver(INodeObserver observer);
  public abstract bool removeObserver(INodeObserver observer);

  //precondition: must be called in dispatcher's thread
  public abstract void init(Callback callback);
  //precondition: must be called in dispatcher's thread
  public abstract bool shutdown();

  //precondition: all of following methods must not be invoked in dispatcher's thread
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPeerCount() const = 0;
  public abstract uint getPeerCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastLocalBlockHeight() const = 0;
  public abstract uint getLastLocalBlockHeight();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastKnownBlockHeight() const = 0;
  public abstract uint getLastKnownBlockHeight();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLocalBlockCount() const = 0;
  public abstract uint getLocalBlockCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getKnownBlockCount() const = 0;
  public abstract uint getKnownBlockCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getLastLocalBlockTimestamp() const = 0;
  public abstract ulong getLastLocalBlockTimestamp();

  public abstract void getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount, List<Crypto.Hash> blockHashes, Callback callback);
  public abstract void getTransactionHashesByPaymentId(Crypto.Hash paymentId, List<Crypto.Hash> transactionHashes, Callback callback);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockHeaderInfo getLastLocalBlockHeaderInfo() const = 0;
  public abstract BlockHeaderInfo getLastLocalBlockHeaderInfo();

  public abstract void relayTransaction(Transaction transaction, Callback callback);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract void getRandomOutsByAmounts(List<ulong>&& amounts, ushort outsCount, List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> result, Callback callback);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract void getNewBlocks(List<Crypto.Hash>&& knownBlockIds, List<RawBlock> newBlocks, ref uint startHeight, Callback callback);
  public abstract void getTransactionOutsGlobalIndices(Crypto.Hash transactionHash, List<uint> outsGlobalIndices, Callback callback);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract void queryBlocks(List<Crypto.Hash>&& knownBlockIds, ulong timestamp, List<BlockShortEntry> newBlocks, ref uint startHeight, Callback callback);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract void getPoolSymmetricDifference(List<Crypto.Hash>&& knownPoolTxIds, Crypto.Hash knownBlockId, ref bool isBcActual, List<std::unique_ptr<ITransactionReader>> newTxs, List<Crypto.Hash> deletedTxIds, Callback callback);

  public abstract void getBlocks(List<uint> blockHeights, List<List<BlockDetails>> blocks, Callback callback);
  public abstract void getBlocks(List<Crypto.Hash> blockHashes, List<BlockDetails> blocks, Callback callback);
  public abstract void getTransactions(List<Crypto.Hash> transactionHashes, List<TransactionDetails> transactions, Callback callback);
  public abstract void isSynchronized(ref bool syncStatus, Callback callback);
}

}




namespace PaymentService
{

public class NodeFactory : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static CryptoNote::INode createNode(string daemonAddress, ushort daemonPort, Logging::ILogger logger);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static CryptoNote::INode createNodeStub();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  NodeFactory();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  CryptoNote::INode getNode(string daemonAddress, ushort daemonPort);

  private static NodeFactory factory = new NodeFactory();
}

} //namespace PaymentService

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.


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

public enum WalletTransactionState : byte
{
  SUCCEEDED = 0,
  FAILED,
  CANCELLED,
  CREATED,
  DELETED
}

public enum WalletEventType
{
  TRANSACTION_CREATED,
  TRANSACTION_UPDATED,
  BALANCE_UNLOCKED,
  SYNC_PROGRESS_UPDATED,
  SYNC_COMPLETED,
}

public enum WalletSaveLevel : byte
{
  SAVE_KEYS_ONLY,
  SAVE_KEYS_AND_TRANSACTIONS,
  SAVE_ALL
}

public class WalletTransactionCreatedData
{
  public uint transactionIndex;
}

public class WalletTransactionUpdatedData
{
  public uint transactionIndex;
}

public class WalletSynchronizationProgressUpdated
{
  public uint processedBlockCount;
  public uint totalBlockCount;
}

public class WalletEvent
{
  public WalletEventType type;
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	WalletTransactionCreatedData transactionCreated;
//	WalletTransactionUpdatedData transactionUpdated;
//	WalletSynchronizationProgressUpdated synchronizationProgressUpdated;
//  };
}

public class WalletTransaction
{
  public WalletTransactionState state;
  public ulong timestamp;
  public uint blockHeight;
  public Crypto.Hash hash = new Crypto.Hash();
  public long totalAmount;
  public ulong fee;
  public ulong creationTime;
  public ulong unlockTime;
  public string extra;
  public bool isBase;
}

public enum WalletTransferType : byte
{
  USUAL = 0,
  DONATION,
  CHANGE
}

public class WalletOrder
{
  public string address;
  public ulong amount;
}

public class WalletTransfer
{
  public WalletTransferType type;
  public string address;
  public long amount;
}

public class DonationSettings
{
  public string address;
  public ulong threshold = 0;
}

public class TransactionParameters
{
  public List<string> sourceAddresses = new List<string>();
  public List<WalletOrder> destinations = new List<WalletOrder>();
  public ulong fee = 0;
  public ushort mixIn = 0;
  public string extra;
  public ulong unlockTimestamp = 0;
  public DonationSettings donation = new DonationSettings();
  public string changeDestination;
}

public class WalletTransactionWithTransfers
{
  public WalletTransaction transaction = new WalletTransaction();
  public List<WalletTransfer> transfers = new List<WalletTransfer>();
}

public class TransactionsInBlockInfo
{
  public Crypto.Hash blockHash = new Crypto.Hash();
  public List<WalletTransactionWithTransfers> transactions = new List<WalletTransactionWithTransfers>();
}

public abstract class IWallet : System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract void initialize(string path, string password);
  public abstract void initializeWithViewKey(string path, string password, Crypto.SecretKey viewSecretKey);
  public abstract void load(string path, string password, string extra);
  public abstract void load(string path, string password);
  public abstract void shutdown();

  public abstract void changePassword(string oldPassword, string newPassword);
  public abstract void save(WalletSaveLevel saveLevel = WalletSaveLevel.SAVE_ALL, string extra = "");
  public abstract void exportWallet(string path, bool encrypt = true, WalletSaveLevel saveLevel = WalletSaveLevel.SAVE_ALL, string extra = "");

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getAddressCount() const = 0;
  public abstract uint getAddressCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual string getAddress(uint index) const = 0;
  public abstract string getAddress(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getAddressSpendKey(uint index) const = 0;
  public abstract KeyPair getAddressSpendKey(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getAddressSpendKey(const string& address) const = 0;
  public abstract KeyPair getAddressSpendKey(string address);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getViewKey() const = 0;
  public abstract KeyPair getViewKey();
  public abstract string createAddress();
  public abstract string createAddress(Crypto.SecretKey spendSecretKey);
  public abstract string createAddress(Crypto.PublicKey spendPublicKey);
  public abstract List<string> createAddressList(List<Crypto.SecretKey> spendSecretKeys);
  public abstract void deleteAddress(string address);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getActualBalance() const = 0;
  public abstract ulong getActualBalance();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getActualBalance(const string& address) const = 0;
  public abstract ulong getActualBalance(string address);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getPendingBalance() const = 0;
  public abstract ulong getPendingBalance();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getPendingBalance(const string& address) const = 0;
  public abstract ulong getPendingBalance(string address);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionCount() const = 0;
  public abstract uint getTransactionCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransaction getTransaction(uint transactionIndex) const = 0;
  public abstract WalletTransaction getTransaction(uint transactionIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionTransferCount(uint transactionIndex) const = 0;
  public abstract uint getTransactionTransferCount(uint transactionIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransfer getTransactionTransfer(uint transactionIndex, uint transferIndex) const = 0;
  public abstract WalletTransfer getTransactionTransfer(uint transactionIndex, uint transferIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransactionWithTransfers getTransaction(const Crypto::Hash& transactionHash) const = 0;
  public abstract WalletTransactionWithTransfers getTransaction(Crypto.Hash transactionHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionsInBlockInfo> getTransactions(const Crypto::Hash& blockHash, uint count) const = 0;
  public abstract List<TransactionsInBlockInfo> getTransactions(Crypto.Hash blockHash, uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionsInBlockInfo> getTransactions(uint blockIndex, uint count) const = 0;
  public abstract List<TransactionsInBlockInfo> getTransactions(uint blockIndex, uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashes(uint blockIndex, uint count) const = 0;
  public abstract List<Crypto.Hash> getBlockHashes(uint blockIndex, uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockCount() const = 0;
  public abstract uint getBlockCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<WalletTransactionWithTransfers> getUnconfirmedTransactions() const = 0;
  public abstract List<WalletTransactionWithTransfers> getUnconfirmedTransactions();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint> getDelayedTransactionIds() const = 0;
  public abstract List<uint> getDelayedTransactionIds();

  public abstract uint transfer(TransactionParameters sendingTransaction);

  public abstract uint makeTransaction(TransactionParameters sendingTransaction);
  public abstract void commitTransaction(uint transactionId);
  public abstract void rollbackUncommitedTransaction(uint transactionId);

  public abstract void start();
  public abstract void stop();

  //blocks until an event occurred
  public abstract WalletEvent getEvent();
}

}

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




namespace PaymentService
{

/* Forward declaration to avoid circular dependency from including "WalletService.h" */
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class WalletService;

public class RequestSerializationError: System.Exception
{

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* what() const throw() override
  public override string what()
  {
	  return "Request error";
  }
}

public class Save
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class Export
{
  public class Request
  {
	public string fileName;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class Reset
{
  public class Request
  {
	public string viewSecretKey;

	public ulong scanHeight = 0;

	public bool newAddress = false;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetViewKey
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string viewSecretKey;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetMnemonicSeed
{
  public class Request
  {
	public string address;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string mnemonicSeed;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetStatus
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public uint blockCount;
	public uint knownBlockCount;
	public ulong localDaemonBlockCount;
	public string lastBlockHash;
	public uint peerCount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetAddresses
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<string> addresses = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class CreateAddress
{
  public class Request
  {
	public string spendSecretKey;
	public string spendPublicKey;

	public ulong scanHeight = 0;

	public bool newAddress = false;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string address;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class CreateAddressList
{
  public class Request
  {
	public List<string> spendSecretKeys = new List<string>();

	public ulong scanHeight = 0;

	public bool newAddress = false;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<string> addresses = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class DeleteAddress
{
  public class Request
  {
	public string address;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetSpendKeys
{
  public class Request
  {
	public string address;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string spendSecretKey;
	public string spendPublicKey;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetBalance
{
  public class Request
  {
	public string address;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public ulong availableBalance;
	public ulong lockedAmount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetBlockHashes
{
  public class Request
  {
	public uint firstBlockIndex;
	public uint blockCount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<string> blockHashes = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class TransactionHashesInBlockRpcInfo
{
  public string blockHash;
  public List<string> transactionHashes = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer serializer);
}

public class GetTransactionHashes
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public string blockHash;
	public uint firstBlockIndex = uint.MaxValue;
	public uint blockCount;
	public string paymentId;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<TransactionHashesInBlockRpcInfo> items = new List<TransactionHashesInBlockRpcInfo>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class TransferRpcInfo
{
  public byte type;
  public string address;
  public long amount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer serializer);
}

public class TransactionRpcInfo
{
  public byte state;
  public string transactionHash;
  public uint blockIndex;
  public ulong timestamp;
  public bool isBase;
  public ulong unlockTime;
  public long amount;
  public ulong fee;
  public List<TransferRpcInfo> transfers = new List<TransferRpcInfo>();
  public string extra;
  public string paymentId;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer serializer);
}

public class GetTransaction
{
  public class Request
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public TransactionRpcInfo transaction = new TransactionRpcInfo();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class TransactionsInBlockRpcInfo
{
  public string blockHash;
  public List<TransactionRpcInfo> transactions = new List<TransactionRpcInfo>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer serializer);
}

public class GetTransactions
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public string blockHash;
	public uint firstBlockIndex = uint.MaxValue;
	public uint blockCount;
	public string paymentId;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<TransactionsInBlockRpcInfo> items = new List<TransactionsInBlockRpcInfo>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetUnconfirmedTransactionHashes
{
  public class Request
  {
	public List<string> addresses = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<string> transactionHashes = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class WalletRpcOrder
{
  public string address;
  public ulong amount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer serializer);
}

public class SendTransaction
{
  public class Request
  {
	public List<string> sourceAddresses = new List<string>();
	public List<WalletRpcOrder> transfers = new List<WalletRpcOrder>();
	public string changeAddress;
	public ulong fee = 0;
	public ulong anonymity;
	public string extra;
	public string paymentId;
	public ulong unlockTime = 0;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer, WalletService service);
  }

  public class Response
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class CreateDelayedTransaction
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public List<WalletRpcOrder> transfers = new List<WalletRpcOrder>();
	public string changeAddress;
	public ulong fee = 0;
	public ulong anonymity;
	public string extra;
	public string paymentId;
	public ulong unlockTime = 0;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer, WalletService service);
  }

  public class Response
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class GetDelayedTransactionHashes
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public List<string> transactionHashes = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class DeleteDelayedTransaction
{
  public class Request
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class SendDelayedTransaction
{
  public class Request
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class SendFusionTransaction
{
  public class Request
  {
	public ulong threshold;
	public ulong anonymity;
	public List<string> addresses = new List<string>();
	public string destinationAddress;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer, WalletService service);
  }

  public class Response
  {
	public string transactionHash;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class EstimateFusion
{
  public class Request
  {
	public ulong threshold;
	public List<string> addresses = new List<string>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public uint fusionReadyCount;
	public uint totalOutputCount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class CreateIntegratedAddress
{
  public class Request
  {
	public string address;
	public string paymentId;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string integratedAddress;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

public class NodeFeeInfo
{
  public class Request
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }

  public class Response
  {
	public string address;
	public uint amount;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(CryptoNote::ISerializer serializer);
  }
}

} //namespace PaymentService



namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IFusionManager;
}

namespace PaymentService
{

public class WalletConfiguration
{
  public string walletFile;
  public string walletPassword;
  public bool syncFromZero;
  public string secretViewKey;
  public string secretSpendKey;
  public string mnemonicSeed;
  public ulong scanHeight;
}

public class WalletService : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  WalletService(CryptoNote::Currency currency, System::Dispatcher sys, CryptoNote::INode node, CryptoNote::IWallet wallet, CryptoNote::IFusionManager fusionManager, WalletConfiguration conf, Logging::ILogger logger);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void init();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void saveWallet();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code saveWalletNoThrow();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code exportWallet(string fileName);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code resetWallet(ulong scanHeight);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createAddress(string spendSecretKeyText, ulong scanHeight, bool newAddress, string address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createAddressList(ClassicVector<string> spendSecretKeysText, ulong scanHeight, bool newAddress, ClassicVector<string> addresses);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createAddress(string address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createTrackingAddress(string spendPublicKeyText, ulong scanHeight, bool newAddress, string address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code deleteAddress(string address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getSpendkeys(string address, string publicSpendKeyText, string secretSpendKeyText);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getBalance(string address, ref ulong availableBalance, ref ulong lockedAmount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getBalance(ref ulong availableBalance, ref ulong lockedAmount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getBlockHashes(uint firstBlockIndex, uint blockCount, ClassicVector<string> blockHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getViewKey(string viewSecretKey);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getMnemonicSeed(string address, string mnemonicSeed);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getTransactionHashes(ClassicVector<string> addresses, string blockHash, uint blockCount, string paymentId, ClassicVector<TransactionHashesInBlockRpcInfo> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getTransactionHashes(ClassicVector<string> addresses, uint firstBlockIndex, uint blockCount, string paymentId, ClassicVector<TransactionHashesInBlockRpcInfo> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getTransactions(ClassicVector<string> addresses, string blockHash, uint blockCount, string paymentId, ClassicVector<TransactionsInBlockRpcInfo> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getTransactions(ClassicVector<string> addresses, uint firstBlockIndex, uint blockCount, string paymentId, ClassicVector<TransactionsInBlockRpcInfo> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getTransaction(string transactionHash, TransactionRpcInfo transaction);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getAddresses(ClassicVector<string> addresses);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code sendTransaction(SendTransaction::Request request, string transactionHash);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createDelayedTransaction(CreateDelayedTransaction::Request request, string transactionHash);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getDelayedTransactionHashes(ClassicVector<string> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code deleteDelayedTransaction(string transactionHash);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code sendDelayedTransaction(string transactionHash);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getUnconfirmedTransactionHashes(ClassicVector<string> addresses, ClassicVector<string> transactionHashes);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getStatus(ref uint blockCount, ref uint knownBlockCount, ref ulong localDaemonBlockCount, string lastBlockHash, ref uint peerCount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code sendFusionTransaction(ulong threshold, uint anonymity, ClassicVector<string> addresses, string destinationAddress, string transactionHash);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code estimateFusion(ulong threshold, ClassicVector<string> addresses, ref uint fusionReadyCount, ref uint totalOutputCount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code createIntegratedAddress(string address, string paymentId, string integratedAddress);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::error_code getFeeInfo(string address, ref uint amount);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getDefaultMixin() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ulong getDefaultMixin();


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void refresh();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void reset(ulong scanHeight);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void loadWallet();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void loadTransactionIdIndex();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void getNodeFee();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(const Crypto::Hash& blockHash, uint blockCount) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(Crypto::Hash blockHash, uint blockCount);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(uint firstBlockIndex, uint blockCount) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(uint firstBlockIndex, uint blockCount);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(const Crypto::Hash& blockHash, uint blockCount, const TransactionsInBlockInfoFilter& filter) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(Crypto::Hash blockHash, uint blockCount, TransactionsInBlockInfoFilter filter);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(uint firstBlockIndex, uint blockCount, const TransactionsInBlockInfoFilter& filter) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(uint firstBlockIndex, uint blockCount, TransactionsInBlockInfoFilter filter);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(const Crypto::Hash& blockHash, uint blockCount, const TransactionsInBlockInfoFilter& filter) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(Crypto::Hash blockHash, uint blockCount, TransactionsInBlockInfoFilter filter);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(uint firstBlockIndex, uint blockCount, const TransactionsInBlockInfoFilter& filter) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(uint firstBlockIndex, uint blockCount, TransactionsInBlockInfoFilter filter);

  private readonly CryptoNote.Currency currency;
  private CryptoNote.IWallet wallet;
  private CryptoNote.IFusionManager fusionManager;
  private CryptoNote.INode node;
  private readonly WalletConfiguration config;
  private bool inited;
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private System.Dispatcher dispatcher;
  private System.Event readyEvent = new System.Event();
  private System.ContextGroup refreshContext = new System.ContextGroup();
  private string m_node_address;
  private uint m_node_fee;

  private SortedDictionary<string, uint> transactionIdIndex = new SortedDictionary<string, uint>();
}

} //namespace PaymentService


public class PaymentGateService
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  PaymentGateService();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool init(int argc, string[] argv);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const PaymentService::ConfigurationManager& getConfig() const
  public PaymentService.ConfigurationManager getConfig()
  {
	  return config;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: PaymentService::WalletConfiguration getWalletConfig() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  PaymentService::WalletConfiguration getWalletConfig();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  CryptoNote::Currency getCurrency();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void run();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void stop();

  public Logging.ILogger getLogger()
  {
	  return logger;
  }


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void runInProcess(Logging::LoggerRef log);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void runRpcProxy(Logging::LoggerRef log);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void runWalletService(CryptoNote::Currency currency, CryptoNote::INode node);

  private System.Dispatcher dispatcher;
  private System.Event stopEvent;
  private PaymentService.ConfigurationManager config = new PaymentService.ConfigurationManager();
  private PaymentService.WalletService service;
  private CryptoNote.CurrencyBuilder currencyBuilder = new CryptoNote.CurrencyBuilder();

  private Logging.LoggerGroup logger = new Logging.LoggerGroup();
  private std::ofstream fileStream = new std::ofstream();
  private Logging.StreamLogger fileLogger = new Logging.StreamLogger();
  private Logging.ConsoleLogger consoleLogger = new Logging.ConsoleLogger();
}


#if WIN32
#else
#endif