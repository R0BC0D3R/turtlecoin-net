// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Crypto;
using Logging;
using System.Collections.Generic;

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

public class TransfersSubscription : IObservableImpl < ITransfersObserver, ITransfersSubscription >
{
  public TransfersSubscription(CryptoNote.Currency currency, Logging.ILogger logger, AccountSubscription sub)
  {
	  this.subscription = new CryptoNote.AccountSubscription(sub);
	  this.logger = new Logging.LoggerRef(logger, "TransfersSubscription");
	  this.transfers = new CryptoNote.TransfersContainer(currency, logger, sub.transactionSpendableAge);
	  this.m_address = currency.accountAddressAsString(sub.keys.address);
  }

  public SynchronizationStart getSyncStart()
  {
	return subscription.syncStart;
  }
  public void onBlockchainDetach(uint height)
  {
	List<Hash> deletedTransactions = transfers.detach(height);
	foreach (var hash in deletedTransactions)
	{
	  logger.functorMethod(TRACE) << "Transaction deleted from wallet " << m_address << ", hash " << hash;
	  m_observerManager.notify(ITransfersObserver.onTransactionDeleted, this, hash);
	}
  }
  public void onError(std::error_code ec, uint height)
  {
	if (height != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
	transfers.detach(height);
	}
	m_observerManager.notify(ITransfersObserver.onError, this, height, ec);
  }
  public bool advanceHeight(uint height)
  {
	return transfers.advanceHeight(height);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const AccountKeys& getKeys() const
  public AccountKeys getKeys()
  {
	return subscription.keys;
  }
  public bool addTransaction(TransactionBlockInfo blockInfo, ITransactionReader tx, List<TransactionOutputInformationIn> transfersList)
  {
	bool added = transfers.addTransaction(blockInfo, tx, transfersList);
	if (added)
	{
	  logger.functorMethod(TRACE) << "Transaction updates balance of wallet " << m_address << ", hash " << tx.getTransactionHash();
	  m_observerManager.notify(ITransfersObserver.onTransactionUpdated, this, tx.getTransactionHash());
	}

	return added;
  }

  public void deleteUnconfirmedTransaction(Hash transactionHash)
  {
	if (transfers.deleteUnconfirmedTransaction(transactionHash))
	{
	  logger.functorMethod(TRACE) << "Transaction deleted from wallet " << m_address << ", hash " << transactionHash;
	  m_observerManager.notify(ITransfersObserver.onTransactionDeleted, this, transactionHash);
	}
  }
  public void markTransactionConfirmed(TransactionBlockInfo block, Hash transactionHash, List<uint> globalIndices)
  {
	transfers.markTransactionConfirmed(block, transactionHash, globalIndices);
	m_observerManager.notify(ITransfersObserver.onTransactionUpdated, this, transactionHash);
  }

  // ITransfersSubscription
  public override AccountPublicAddress getAddress()
  {
	return subscription.keys.address;
  }
  public override ITransfersContainer getContainer()
  {
	return transfers;
  }

  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private TransfersContainer transfers = new TransfersContainer();
  private AccountSubscription subscription = new AccountSubscription();
  private string m_address;
}

}

