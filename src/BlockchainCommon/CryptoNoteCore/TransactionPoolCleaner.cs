// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;
using System.Diagnostics;

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

public class TransactionPoolCleanWrapper: ITransactionPoolCleanWrapper
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public TransactionPoolCleanWrapper(std::unique_ptr<ITransactionPool>&& transactionPool, std::unique_ptr<ITimeProvider>&& timeProvider, Logging.ILogger logger, ulong timeout)
  {
	  this.transactionPool = std::move(transactionPool);
	  this.timeProvider = std::move(timeProvider);
	  this.logger = new Logging.LoggerRef(logger, "TransactionPoolCleanWrapper");
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.timeout = timeout;
	  this.timeout.CopyFrom(timeout);

	Debug.Assert(this.timeProvider);
  }

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TransactionPoolCleanWrapper(const TransactionPoolCleanWrapper&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TransactionPoolCleanWrapper(TransactionPoolCleanWrapper&& other) = delete;

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TransactionPoolCleanWrapper& operator =(const TransactionPoolCleanWrapper&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TransactionPoolCleanWrapper& operator =(TransactionPoolCleanWrapper&&) = delete;

  public override void Dispose()
  {
	  base.Dispose();
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override bool pushTransaction(CachedTransaction && tx, TransactionValidatorState && transactionState)
  {
	return !isTransactionRecentlyDeleted(tx.getTransactionHash()) && transactionPool.pushTransaction(std::move(tx), std::move(transactionState));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const CachedTransaction& getTransaction(const Crypto::Hash& hash) const override
  public override CachedTransaction getTransaction(Crypto.Hash hash)
  {
	return transactionPool.getTransaction(hash);
  }
  public override bool removeTransaction(Crypto.Hash hash)
  {
	return transactionPool.removeTransaction(hash);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getTransactionCount() const override
  public override size_t getTransactionCount()
  {
	return transactionPool.getTransactionCount();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const override
  public override List<Crypto.Hash> getTransactionHashes()
  {
	return transactionPool.getTransactionHashes();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool checkIfTransactionPresent(const Crypto::Hash& hash) const override
  public override bool checkIfTransactionPresent(Crypto.Hash hash)
  {
	return transactionPool.checkIfTransactionPresent(hash);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const TransactionValidatorState& getPoolTransactionValidationState() const override
  public override TransactionValidatorState getPoolTransactionValidationState()
  {
	return transactionPool.getPoolTransactionValidationState();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<CachedTransaction> getPoolTransactions() const override
  public override List<CachedTransaction> getPoolTransactions()
  {
	return transactionPool.getPoolTransactions();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getTransactionReceiveTime(const Crypto::Hash& hash) const override
  public override ulong getTransactionReceiveTime(Crypto.Hash hash)
  {
	return transactionPool.getTransactionReceiveTime(hash);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const override
  public override List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId)
  {
	return transactionPool.getTransactionHashesByPaymentId(paymentId);
  }

  public override List<Crypto.Hash> clean(uint height)
  {
	try
	{
	  ulong currentTime = timeProvider.now();
	  var transactionHashes = transactionPool.getTransactionHashes();

	  List<Crypto.Hash> deletedTransactions = new List<Crypto.Hash>();
	  foreach (var hash in transactionHashes)
	  {
		ulong transactionAge = currentTime - transactionPool.getTransactionReceiveTime(hash);
		if (transactionAge >= timeout)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << "Deleting transaction " << Common.GlobalMembers.podToHex(hash) << " from pool";
		  recentlyDeletedTransactions.Add(hash, currentTime);
		  transactionPool.removeTransaction(hash);
		  deletedTransactions.emplace_back(std::move(hash));
		}

		CachedTransaction transaction = transactionPool.getTransaction(hash);
		List<CachedTransaction> transactions = new List<CachedTransaction>();
		transactions.emplace_back(transaction);

		var (success, error) = Mixins.validate(new List<CachedTransaction>(transactions), new uint(height));

		if (!success)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << "Deleting invalid transaction " << Common.GlobalMembers.podToHex(hash) << " from pool." << error;
		  recentlyDeletedTransactions.Add(hash, currentTime);
		  transactionPool.removeTransaction(hash);
		  deletedTransactions.emplace_back(std::move(hash));
		}
	  }

	  cleanRecentlyDeletedTransactions(new ulong(currentTime));
	  return deletedTransactions;
	}
	catch (System.InterruptedException)
	{
	  throw;
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Caught an exception: " << e.Message << ", stopping cleaning procedure cycle";
	  throw;
	}
  }

  private std::unique_ptr<ITransactionPool> transactionPool = new std::unique_ptr<ITransactionPool>();
  private std::unique_ptr<ITimeProvider> timeProvider = new std::unique_ptr<ITimeProvider>();
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private Dictionary<Crypto.Hash, ulong> recentlyDeletedTransactions = new Dictionary<Crypto.Hash, ulong>();
  private ulong timeout = new ulong();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTransactionRecentlyDeleted(const Crypto::Hash& hash) const
  private bool isTransactionRecentlyDeleted(Crypto.Hash hash)
  {
	var it = recentlyDeletedTransactions.find(hash);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return it != recentlyDeletedTransactions.end() && it.second >= timeout;
  }
  private void cleanRecentlyDeletedTransactions(ulong currentTime)
  {
	for (var it = recentlyDeletedTransactions.GetEnumerator(); it != recentlyDeletedTransactions.end();)
	{
	  if (currentTime - it.second >= timeout != null)
	  {
		it = recentlyDeletedTransactions.Remove(it);
	  }
	  else
	  {
		++it;
	  }
	}
  }
}

} //namespace CryptoNote

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);



