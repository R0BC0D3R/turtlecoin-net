// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;
using System.Diagnostics;

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



namespace CryptoNote
{

public class TransactionPool : ITransactionPool
{
  public TransactionPool(Logging.ILogger logger)
  {
	  this.transactionHashIndex = transactions.get<TransactionHashTag>();
	  this.transactionCostIndex = transactions.get<TransactionCostTag>();
	  this.paymentIdIndex = transactions.get<PaymentIdTag>();
	  this.logger = new Logging.LoggerRef(logger, "TransactionPool");
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public bool pushTransaction(CachedTransaction && transaction, TransactionValidatorState && transactionState)
  {
	var pendingTx = new PendingTransactionInfo({(ulong)time(null), std::move(transaction)});

	Crypto.Hash paymentId = new Crypto.Hash();
	if (getPaymentIdFromTxExtra(pendingTx.cachedTransaction.getTransaction().extra, paymentId))
	{
	  pendingTx.paymentId = paymentId;
	}

	if (transactionHashIndex.count(pendingTx.getTransactionHash()) > 0)
	{
	  logger(Logging.DEBUGGING) << "pushTransaction: transaction hash already present in index";
	  return false;
	}

	if (hasIntersections(poolState, transactionState))
	{
	  logger(Logging.DEBUGGING) << "pushTransaction: failed to merge states, some keys already used";
	  return false;
	}

	mergeStates(poolState, transactionState);

	logger(Logging.DEBUGGING) << "pushed transaction " << pendingTx.getTransactionHash() << " to pool";
	return transactionHashIndex.insert(std::move(pendingTx)).second;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const CachedTransaction& getTransaction(const Crypto::Hash& hash) const override
  public CachedTransaction getTransaction(Crypto.Hash hash)
  {
	var it = transactionHashIndex.find(hash);
	Debug.Assert(it != transactionHashIndex.end());

	return it.cachedTransaction;
  }
  public bool removeTransaction(Crypto.Hash hash)
  {
	var it = transactionHashIndex.find(hash);
	if (it == transactionHashIndex.end())
	{
	  logger(Logging.DEBUGGING) << "removeTransaction: transaction not found";
	  return false;
	}

	excludeFromState(poolState, it.cachedTransaction);
	transactionHashIndex.erase(it);

	logger(Logging.DEBUGGING) << "transaction " << hash << " removed from pool";
	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionCount() const override
  public uint getTransactionCount()
  {
	return transactionHashIndex.size();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const override
  public List<Crypto.Hash> getTransactionHashes()
  {
	List<Crypto.Hash> hashes = new List<Crypto.Hash>();
	for (var it = transactionCostIndex.begin(); it != transactionCostIndex.end(); ++it)
	{
	  hashes.Add(it.getTransactionHash());
	}

	return hashes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool checkIfTransactionPresent(const Crypto::Hash& hash) const override
  public bool checkIfTransactionPresent(Crypto.Hash hash)
  {
	return transactionHashIndex.find(hash) != transactionHashIndex.end();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const TransactionValidatorState& getPoolTransactionValidationState() const override
  public TransactionValidatorState getPoolTransactionValidationState()
  {
	return poolState;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<CachedTransaction> getPoolTransactions() const override
  public List<CachedTransaction> getPoolTransactions()
  {
	List<CachedTransaction> result = new List<CachedTransaction>();
	result.Capacity = transactionCostIndex.size();

	foreach (var transactionItem in transactionCostIndex)
	{
	  result.emplace_back(transactionItem.cachedTransaction);
	}

	return result;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getTransactionReceiveTime(const Crypto::Hash& hash) const override
  public ulong getTransactionReceiveTime(Crypto.Hash hash)
  {
	var it = transactionHashIndex.find(hash);
	Debug.Assert(it != transactionHashIndex.end());

	return it.receiveTime;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const override
  public List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId)
  {
	boost.optional<Crypto.Hash> p = new boost.optional<Crypto.Hash>(paymentId);

	var range = paymentIdIndex.equal_range(p);
	List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
	transactionHashes.Capacity = std::distance(range.first, range.second);
	for (var it = range.first; it != range.second; ++it)
	{
	  transactionHashes.Add(it.getTransactionHash());
	}

	return transactionHashes;
  }
  private TransactionValidatorState poolState = new TransactionValidatorState();

  private class PendingTransactionInfo
  {
	public ulong receiveTime = new ulong();
	public CachedTransaction cachedTransaction = new CachedTransaction();
	public boost.optional<Crypto.Hash> paymentId;

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTransactionHash() const
	public Crypto.Hash getTransactionHash()
	{
	  return cachedTransaction.getTransactionHash();
	}
  }

  private class TransactionPriorityComparator
  {
	// lhs > hrs

	// lhs > hrs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ()(const PendingTransactionInfo& lhs, const PendingTransactionInfo& rhs) const
	public static bool functorMethod(PendingTransactionInfo lhs, PendingTransactionInfo rhs)
	{
	  CachedTransaction left = lhs.cachedTransaction;
	  CachedTransaction right = rhs.cachedTransaction;

	  // price(lhs) = lhs.fee / lhs.blobSize
	  // price(lhs) > price(rhs) -->
	  // lhs.fee / lhs.blobSize > rhs.fee / rhs.blobSize -->
	  // lhs.fee * rhs.blobSize > rhs.fee * lhs.blobSize
	  ulong lhs_hi = new ulong();
	  ulong lhs_lo = GlobalMembers.mul128(left.getTransactionFee(), right.getTransactionBinaryArray().size(), lhs_hi);
	  ulong rhs_hi = new ulong();
	  ulong rhs_lo = GlobalMembers.mul128(right.getTransactionFee(), left.getTransactionBinaryArray().size(), rhs_hi);

	  return (lhs_hi > rhs_hi) || (lhs_hi == rhs_hi && lhs_lo > rhs_lo) || (lhs_hi == rhs_hi && lhs_lo == rhs_lo && left.getTransactionBinaryArray().size() < right.getTransactionBinaryArray().size()) || (lhs_hi == rhs_hi && lhs_lo == rhs_lo && left.getTransactionBinaryArray().size() == right.getTransactionBinaryArray().size() && lhs.receiveTime < rhs.receiveTime);
	}
  }

  private class TransactionHashTag
  {
  }
  private class TransactionCostTag
  {
  }
  private class PaymentIdTag
  {
  }



  private class PaymentIdHasher
  {
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint operator ()(const boost::optional<Crypto::Hash>& paymentId) const
	public static uint functorMethod(boost.optional<Crypto.Hash> paymentId)
	{
	  if (!paymentId)
	  {
		return uint.MaxValue;
	  }

	  return new std::hash<Crypto.Hash>({})(*paymentId);
	}
  }

  private typedef boost::multi_index.hashed_non_unique< boost::multi_index.tag<PaymentIdTag>, BOOST_MULTI_INDEX_MEMBER(PendingTransactionInfo, boost.optional<Crypto.Hash>, paymentId), PaymentIdHasher > PaymentIdIndex = new typedef();


  private readonly boost::multi_index_container<PendingTransactionInfo, boost::multi_index.indexed_by<boost::multi_index.hashed_unique<boost::multi_index.tag<TransactionHashTag>, boost::multi_index.const_mem_fun<PendingTransactionInfo, Crypto.Hash, PendingTransactionInfo.getTransactionHash>>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TransactionCostTag>, boost::multi_index.identity<PendingTransactionInfo>, TransactionPriorityComparator >, PaymentIdIndex>> transactions = new boost::multi_index_container<PendingTransactionInfo, boost::multi_index.indexed_by<boost::multi_index.hashed_unique<boost::multi_index.tag<TransactionHashTag>, boost::multi_index.const_mem_fun<PendingTransactionInfo, Crypto.Hash, PendingTransactionInfo.getTransactionHash>>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TransactionCostTag>, boost::multi_index.identity<PendingTransactionInfo>, TransactionPriorityComparator >, PaymentIdIndex>>();
  private readonly boost::multi_index_container<PendingTransactionInfo, boost::multi_index.indexed_by<boost::multi_index.hashed_unique<boost::multi_index.tag<TransactionHashTag>, boost::multi_index.const_mem_fun<PendingTransactionInfo, Crypto.Hash&, &PendingTransactionInfo.getTransactionHash>>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TransactionCostTag>, boost::multi_index.identity<PendingTransactionInfo>, TransactionPriorityComparator >, PaymentIdIndex>>.index<TransactionHashTag>.type transactionHashIndex;
  private readonly boost::multi_index_container<PendingTransactionInfo, boost::multi_index.indexed_by<boost::multi_index.hashed_unique<boost::multi_index.tag<TransactionHashTag>, boost::multi_index.const_mem_fun<PendingTransactionInfo, Crypto.Hash&, &PendingTransactionInfo.getTransactionHash>>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TransactionCostTag>, boost::multi_index.identity<PendingTransactionInfo>, TransactionPriorityComparator >, PaymentIdIndex>>.index<TransactionCostTag>.type transactionCostIndex;
  private readonly boost::multi_index_container<PendingTransactionInfo, boost::multi_index.indexed_by<boost::multi_index.hashed_unique<boost::multi_index.tag<TransactionHashTag>, boost::multi_index.const_mem_fun<PendingTransactionInfo, Crypto.Hash&, &PendingTransactionInfo.getTransactionHash>>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TransactionCostTag>, boost::multi_index.identity<PendingTransactionInfo>, TransactionPriorityComparator >, PaymentIdIndex>>.index<PaymentIdTag>.type paymentIdIndex;

  private Logging.LoggerRef logger = new Logging.LoggerRef();
}

}


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define inline __inline
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT32(x) ((uint) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT64(x) ((ulong) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32(x) ((((uint) (x) & 0x000000ff) << 24) | (((uint) (x) & 0x0000ff00) << 8) | (((uint) (x) & 0x00ff0000) >> 8) | (((uint) (x) & 0xff000000) >> 24))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64(x) ((((ulong) (x) & 0x00000000000000ff) << 56) | (((ulong) (x) & 0x000000000000ff00) << 40) | (((ulong) (x) & 0x0000000000ff0000) << 24) | (((ulong) (x) & 0x00000000ff000000) << 8) | (((ulong) (x) & 0x000000ff00000000) >> 8) | (((ulong) (x) & 0x0000ff0000000000) >> 24) | (((ulong) (x) & 0x00ff000000000000) >> 40) | (((ulong) (x) & 0xff00000000000000) >> 56))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_swap64

