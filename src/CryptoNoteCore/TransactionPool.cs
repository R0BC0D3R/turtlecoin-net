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
	var pendingTx = new PendingTransactionInfo({(uint64_t)time(null), std::move(transaction)});

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
//ORIGINAL LINE: virtual size_t getTransactionCount() const override
  public size_t getTransactionCount()
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
//ORIGINAL LINE: virtual uint64_t getTransactionReceiveTime(const Crypto::Hash& hash) const override
  public uint64_t getTransactionReceiveTime(Crypto.Hash hash)
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
	public uint64_t receiveTime = new uint64_t();
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
	  uint64_t lhs_hi = new uint64_t();
	  uint64_t lhs_lo = GlobalMembers.mul128(left.getTransactionFee(), right.getTransactionBinaryArray().size(), lhs_hi);
	  uint64_t rhs_hi = new uint64_t();
	  uint64_t rhs_lo = GlobalMembers.mul128(right.getTransactionFee(), left.getTransactionBinaryArray().size(), rhs_hi);

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
//ORIGINAL LINE: size_t operator ()(const boost::optional<Crypto::Hash>& paymentId) const
	public static size_t functorMethod(boost.optional<Crypto.Hash> paymentId)
	{
	  if (!paymentId)
	  {
		return size_t.MaxValue;
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
//ORIGINAL LINE: #define IDENT32(x) ((uint32_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT64(x) ((uint64_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32(x) ((((uint32_t) (x) & 0x000000ff) << 24) | (((uint32_t) (x) & 0x0000ff00) << 8) | (((uint32_t) (x) & 0x00ff0000) >> 8) | (((uint32_t) (x) & 0xff000000) >> 24))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64(x) ((((uint64_t) (x) & 0x00000000000000ff) << 56) | (((uint64_t) (x) & 0x000000000000ff00) << 40) | (((uint64_t) (x) & 0x0000000000ff0000) << 24) | (((uint64_t) (x) & 0x00000000ff000000) << 8) | (((uint64_t) (x) & 0x000000ff00000000) >> 8) | (((uint64_t) (x) & 0x0000ff0000000000) >> 24) | (((uint64_t) (x) & 0x00ff000000000000) >> 40) | (((uint64_t) (x) & 0xff00000000000000) >> 56))
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

