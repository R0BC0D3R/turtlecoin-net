// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
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
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{

public partial class WalletSerializerV2
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  WalletSerializerV2(ITransfersObserver transfersObserver, Crypto::PublicKey viewPublicKey, Crypto::SecretKey viewSecretKey, ref ulong actualBalance, ref ulong pendingBalance, WalletsContainer walletsContainer, TransfersSyncronizer synchronizer, UnlockTransactionJobs unlockTransactions, WalletTransactions transactions, WalletTransfers transfers, UncommitedTransactions uncommitedTransactions, string extra, uint transactionSoftLockTime);

  public void load(Common.IInputStream source, byte version)
  {
	CryptoNote.BinaryInputStreamSerializer s = new CryptoNote.BinaryInputStreamSerializer(source);

	byte saveLevelValue;
	s.functorMethod(saveLevelValue, "saveLevel");
	WalletSaveLevel saveLevel = (WalletSaveLevel)saveLevelValue;

	loadKeyListAndBalances(s.functorMethod, saveLevel == WalletSaveLevel.SAVE_ALL);

	if (saveLevel == WalletSaveLevel.SAVE_KEYS_AND_TRANSACTIONS || saveLevel == WalletSaveLevel.SAVE_ALL)
	{
	  loadTransactions(s.functorMethod);
	  loadTransfers(s.functorMethod);
	}

	if (saveLevel == WalletSaveLevel.SAVE_ALL)
	{
	  loadTransfersSynchronizer(s.functorMethod);
	  loadUnlockTransactionsJobs(s.functorMethod);
	  s.functorMethod(m_uncommitedTransactions, "uncommitedTransactions");
	}

	s.functorMethod(m_extra, "extra");
  }
  public void save(Common.IOutputStream destination, WalletSaveLevel saveLevel)
  {
	CryptoNote.BinaryOutputStreamSerializer s = new CryptoNote.BinaryOutputStreamSerializer(destination);

	byte saveLevelValue = (byte)saveLevel;
	s.functorMethod(saveLevelValue, "saveLevel");

	saveKeyListAndBalances(s.functorMethod, saveLevel == WalletSaveLevel.SAVE_ALL);

	if (saveLevel == WalletSaveLevel.SAVE_KEYS_AND_TRANSACTIONS || saveLevel == WalletSaveLevel.SAVE_ALL)
	{
	  saveTransactions(s.functorMethod);
	  saveTransfers(s.functorMethod);
	}

	if (saveLevel == WalletSaveLevel.SAVE_ALL)
	{
	  saveTransfersSynchronizer(s.functorMethod);
	  saveUnlockTransactionsJobs(s.functorMethod);
	  s.functorMethod(m_uncommitedTransactions, "uncommitedTransactions");
	}

	s.functorMethod(m_extra, "extra");
  }

  public HashSet<Crypto.PublicKey> addedKeys()
  {
	return m_addedKeys;
  }
  public HashSet<Crypto.PublicKey> deletedKeys()
  {
	return m_deletedKeys;
  }

  public const byte MIN_VERSION = 6;
  public const byte SERIALIZATION_VERSION = 6;

  private void loadKeyListAndBalances(CryptoNote.ISerializer serializer, bool saveCache)
  {
	ulong walletCount;
	serializer.functorMethod(walletCount, "walletCount");

	m_actualBalance = 0;
	m_pendingBalance = 0;
	m_deletedKeys.Clear();

	HashSet<Crypto.PublicKey> cachedKeySet = new HashSet<Crypto.PublicKey>();
	auto index = m_walletsContainer.get<KeysIndex>();
	for (uint i = 0; i < walletCount; ++i)
	{
	  Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();
	  ulong actualBalance;
	  ulong pendingBalance;
	  serializer.functorMethod(spendPublicKey, "spendPublicKey");

	  if (saveCache)
	  {
		serializer.functorMethod(actualBalance, "actualBalance");
		serializer.functorMethod(pendingBalance, "pendingBalance");
	  }

	  cachedKeySet.Add(spendPublicKey);

	  var it = index.find(spendPublicKey);
	  if (it == index.end())
	  {
		m_deletedKeys.emplace(std::move(spendPublicKey));
	  }
	  else if (saveCache)
	  {
		m_actualBalance += actualBalance;
		m_pendingBalance += pendingBalance;

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: index.modify(it, [actualBalance, pendingBalance](WalletRecord& wallet)
		index.modify(it, (WalletRecord wallet) =>
		{
		  wallet.actualBalance = actualBalance;
		  wallet.pendingBalance = pendingBalance;
		});
	  }
	}

	foreach (var wallet in index)
	{
	  if (cachedKeySet.count(wallet.spendPublicKey) == 0)
	  {
		m_addedKeys.Add(wallet.spendPublicKey);
	  }
	}
  }
  private void saveKeyListAndBalances(CryptoNote.ISerializer serializer, bool saveCache)
  {
	ulong walletCount = m_walletsContainer.get<RandomAccessIndex>().size();
	serializer.functorMethod(walletCount, "walletCount");
	foreach (var wallet in m_walletsContainer.get<RandomAccessIndex>())
	{
	  serializer.functorMethod(wallet.spendPublicKey, "spendPublicKey");

	  if (saveCache)
	  {
		serializer.functorMethod(wallet.actualBalance, "actualBalance");
		serializer.functorMethod(wallet.pendingBalance, "pendingBalance");
	  }
	}
  }

  private void loadTransactions(CryptoNote.ISerializer serializer)
  {
	ulong count = 0;
	serializer.functorMethod(count, "transactionCount");

	m_transactions.get<RandomAccessIndex>().reserve(count);

	for (ulong i = 0; i < count; ++i)
	{
	  WalletTransactionDtoV2 dto = new WalletTransactionDtoV2();
	  serializer.functorMethod(dto, "transaction");

	  WalletTransaction tx = new WalletTransaction();
	  tx.state = dto.state;
	  tx.timestamp = dto.timestamp;
	  tx.blockHeight = dto.blockHeight;
	  tx.hash = dto.hash;
	  tx.totalAmount = dto.totalAmount;
	  tx.fee = dto.fee;
	  tx.creationTime = dto.creationTime;
	  tx.unlockTime = dto.unlockTime;
	  tx.extra = dto.extra;
	  tx.isBase = dto.isBase;

	  m_transactions.get<RandomAccessIndex>().push_back(std::move(tx));
	}
  }
  private void saveTransactions(CryptoNote.ISerializer serializer)
  {
	ulong count = m_transactions.size();
	serializer.functorMethod(count, "transactionCount");

	foreach (var tx in m_transactions)
	{
	  WalletTransactionDtoV2 dto = new WalletTransactionDtoV2(tx);
	  serializer.functorMethod(dto, "transaction");
	}
  }

  private void loadTransfers(CryptoNote.ISerializer serializer)
  {
	ulong count = 0;
	serializer.functorMethod(count, "transferCount");

	m_transfers.reserve(count);

	for (ulong i = 0; i < count; ++i)
	{
	  ulong txId = 0;
	  serializer.functorMethod(txId, "transactionId");

	  WalletTransferDtoV2 dto = new WalletTransferDtoV2();
	  serializer.functorMethod(dto, "transfer");

	  WalletTransfer tr = new WalletTransfer();
	  tr.address = dto.address;
	  tr.amount = dto.amount;
	  tr.type = (WalletTransferType)dto.type;

	  m_transfers.emplace_back(std::piecewise_construct, std::forward_as_tuple(txId), std::forward_as_tuple(std::move(tr)));
	}
  }
  private void saveTransfers(CryptoNote.ISerializer serializer)
  {
	ulong count = m_transfers.size();
	serializer.functorMethod(count, "transferCount");

	foreach (var kv in m_transfers)
	{
	  ulong txId = kv.first;

	  WalletTransferDtoV2 tr = new WalletTransferDtoV2(kv.second);

	  serializer.functorMethod(txId, "transactionId");
	  serializer.functorMethod(tr, "transfer");
	}
  }

  private void loadTransfersSynchronizer(CryptoNote.ISerializer serializer)
  {
	string transfersSynchronizerData;
	serializer.functorMethod(transfersSynchronizerData, "transfersSynchronizer");

	std::stringstream stream = new std::stringstream(transfersSynchronizerData);
	m_synchronizer.load(stream);
  }
  private void saveTransfersSynchronizer(CryptoNote.ISerializer serializer)
  {
	std::stringstream stream = new std::stringstream();
	m_synchronizer.save(stream);
	stream.flush();

	string transfersSynchronizerData = stream.str();
	serializer.functorMethod(transfersSynchronizerData, "transfersSynchronizer");
  }

  private void loadUnlockTransactionsJobs(CryptoNote.ISerializer serializer)
  {
	auto index = m_unlockTransactions.get<TransactionHashIndex>();
	auto walletsIndex = m_walletsContainer.get<KeysIndex>();

	ulong jobsCount = 0;
	serializer.functorMethod(jobsCount, "unlockTransactionsJobsCount");

	for (ulong i = 0; i < jobsCount; ++i)
	{
	  UnlockTransactionJobDtoV2 dto = new UnlockTransactionJobDtoV2();
	  serializer.functorMethod(dto, "unlockTransactionsJob");

	  var walletIt = walletsIndex.find(dto.walletSpendPublicKey);
	  if (walletIt != walletsIndex.end())
	  {
		UnlockTransactionJob job = new UnlockTransactionJob();
		job.blockHeight = dto.blockHeight;
		job.transactionHash = dto.transactionHash;
		job.container = walletIt.container;

		index.insert(std::move(job));
	  }
	}
  }
  private void saveUnlockTransactionsJobs(CryptoNote.ISerializer serializer)
  {
	auto index = m_unlockTransactions.get<TransactionHashIndex>();
	auto wallets = m_walletsContainer.get<TransfersContainerIndex>();

	ulong jobsCount = index.size();
	serializer.functorMethod(jobsCount, "unlockTransactionsJobsCount");

	foreach (var j in index)
	{
	  var containerIt = wallets.find(j.container);
	  Debug.Assert(containerIt != wallets.end());

	  var keyIt = m_walletsContainer.project<KeysIndex>(containerIt);
	  Debug.Assert(keyIt != m_walletsContainer.get<KeysIndex>().end());

	  UnlockTransactionJobDtoV2 dto = new UnlockTransactionJobDtoV2();
	  dto.blockHeight = j.blockHeight;
	  dto.transactionHash = j.transactionHash;
	  dto.walletSpendPublicKey = keyIt.spendPublicKey;

	  serializer.functorMethod(dto, "unlockTransactionsJob");
	}
  }

//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to value types:
//ORIGINAL LINE: ulong& m_actualBalance;
  private ulong m_actualBalance;
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to value types:
//ORIGINAL LINE: ulong& m_pendingBalance;
  private ulong m_pendingBalance;
  private WalletsContainer m_walletsContainer;
  private TransfersSyncronizer m_synchronizer;
  private UnlockTransactionJobs m_unlockTransactions;
  private WalletTransactions m_transactions;
  private WalletTransfers m_transfers;
  private UncommitedTransactions m_uncommitedTransactions;
  private string m_extra;

  private HashSet<Crypto.PublicKey> m_addedKeys = new HashSet<Crypto.PublicKey>();
  private HashSet<Crypto.PublicKey> m_deletedKeys = new HashSet<Crypto.PublicKey>();
}

} //namespace CryptoNote

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//DO NOT CHANGE IT
public class UnlockTransactionJobDtoV2
{
  public uint blockHeight;
  public Hash transactionHash = new Hash();
  public Crypto.PublicKey walletSpendPublicKey = new Crypto.PublicKey();
}

//DO NOT CHANGE IT
public class WalletTransactionDtoV2
{
  public WalletTransactionDtoV2()
  {
  }

  public WalletTransactionDtoV2(CryptoNote.WalletTransaction wallet)
  {
	state = wallet.state;
	timestamp = wallet.timestamp;
	blockHeight = wallet.blockHeight;
	hash = wallet.hash;
	totalAmount = wallet.totalAmount;
	fee = wallet.fee;
	creationTime = wallet.creationTime;
	unlockTime = wallet.unlockTime;
	extra = wallet.extra;
	isBase = wallet.isBase;
  }

  public CryptoNote.WalletTransactionState state;
  public ulong timestamp;
  public uint blockHeight;
  public Hash hash = new Hash();
  public long totalAmount;
  public ulong fee;
  public ulong creationTime;
  public ulong unlockTime;
  public string extra;
  public bool isBase;
}

//DO NOT CHANGE IT
public class WalletTransferDtoV2
{
  public WalletTransferDtoV2()
  {
  }

  public WalletTransferDtoV2(CryptoNote.WalletTransfer tr)
  {
	address = tr.address;
	amount = tr.amount;
	type = (byte)tr.type;
  }

  public string address;
  public ulong amount;
  public byte type;
}




namespace CryptoNote
{
public partial class WalletSerializerV2
{
	public WalletSerializerV2(ITransfersObserver transfersObserver, Crypto.PublicKey viewPublicKey, Crypto.SecretKey viewSecretKey, ref ulong actualBalance, ref ulong pendingBalance, WalletsContainer walletsContainer, TransfersSyncronizer synchronizer, UnlockTransactionJobs unlockTransactions, boost::multi_index_container < CryptoNote.WalletTransaction, boost::multi_index.indexed_by < boost::multi_index.random_access < boost::multi_index.tag <RandomAccessIndex>>, boost::multi_index.hashed_unique < boost::multi_index.tag <TransactionIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, Crypto.Hash, CryptoNote.WalletTransaction.hash >>, boost::multi_index.ordered_non_unique < boost::multi_index.tag <BlockHeightIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, uint, CryptoNote.WalletTransaction.blockHeight >> >>& transactions, List<Tuple<ulong, CryptoNote.WalletTransfer>> transfers, SortedDictionary<ulong, CryptoNote.Transaction> uncommitedTransactions, string extra, uint transactionSoftLockTime)
	{
		this.m_actualBalance = actualBalance;
		this.m_pendingBalance = pendingBalance;
		this.m_walletsContainer = walletsContainer;
		this.m_synchronizer = synchronizer;
		this.m_unlockTransactions = unlockTransactions;
		this.m_transactions = transactions;
		this.m_transfers = transfers;
		this.m_uncommitedTransactions = uncommitedTransactions;
		this.m_extra = extra;
	}
}
}