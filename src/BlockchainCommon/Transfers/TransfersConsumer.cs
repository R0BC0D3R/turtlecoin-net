// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The BBSCoin Developers
// Copyright (c) 2018, The Karbo Developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using Crypto;
using Logging;
using Common;

using CryptoNote;
using System;
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
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl




namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class INode;

public class TransfersConsumer: IObservableImpl<IBlockchainConsumerObserver, IBlockchainConsumer>
{

  public TransfersConsumer(CryptoNote.Currency currency, INode node, Logging.ILogger logger, SecretKey viewSecret)
  {
	  this.m_node = new CryptoNote.INode(node);
	  this.m_viewSecret = new Crypto.SecretKey(viewSecret);
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_logger = new Logging.LoggerRef(logger, "TransfersConsumer");
	updateSyncStart();
  }

  public ITransfersSubscription addSubscription(AccountSubscription subscription)
  {
	if (subscription.keys.viewSecretKey != m_viewSecret)
	{
	  throw new System.Exception("TransfersConsumer: view secret key mismatch");
	}

	auto res = m_subscriptions[subscription.keys.address.spendPublicKey];

	if (res.get() == null)
	{
	  res.reset(new TransfersSubscription(m_currency, m_logger.getLogger(), subscription));
	  m_spendKeys.Add(subscription.keys.address.spendPublicKey);

	  if (m_subscriptions.Count == 1)
	  {
		m_syncStart = res.getSyncStart();
	  }
	  else
	  {
		var subStart = res.getSyncStart();
		m_syncStart.height = Math.Min(m_syncStart.height, subStart.height);
		m_syncStart.timestamp = Math.Min(m_syncStart.timestamp, subStart.timestamp);
	  }
	}

	return *res;
  }
  // returns true if no subscribers left
  public bool removeSubscription(AccountPublicAddress address)
  {
	m_subscriptions.Remove(address.spendPublicKey);
	m_spendKeys.erase(address.spendPublicKey);
	updateSyncStart();
	return m_subscriptions.Count == 0;
  }
  public ITransfersSubscription getSubscription(AccountPublicAddress acc)
  {
	var it = m_subscriptions.find(acc.spendPublicKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return it == m_subscriptions.end() ? null : it.second.get();
  }
  public void getSubscriptions(List<AccountPublicAddress> subscriptions)
  {
	foreach (var kv in m_subscriptions)
	{
	  subscriptions.Add(kv.second.getAddress());
	}
  }

  public void initTransactionPool(HashSet<Crypto.Hash> uncommitedTransactions)
  {
	for (var itSubscriptions = m_subscriptions.GetEnumerator(); itSubscriptions != m_subscriptions.end(); ++itSubscriptions)
	{
	  List<Crypto.Hash> unconfirmedTransactions = new List<Crypto.Hash>();
	  itSubscriptions.second.getContainer().getUnconfirmedTransactions(unconfirmedTransactions);

	  foreach (Crypto.Hash itTransactions in unconfirmedTransactions)
	  {
		if (uncommitedTransactions.countitTransactions == 0)
		{
		  m_poolTxs.emplaceitTransactions;
		}
	  }
	}
  }
  public void addPublicKeysSeen(Crypto.Hash transactionHash, Crypto.PublicKey outputKey)
  {
	  lock (seen_mutex)
	  {
		  GlobalMembers.transactions_hash_seen.Add(transactionHash);
	  }
	  GlobalMembers.public_keys_seen.Add(outputKey);
  }

  // IBlockchainConsumer
  public override SynchronizationStart getSyncStart()
  {
	return m_syncStart;
  }
  public override void onBlockchainDetach(uint height)
  {
	m_observerManager.notify(IBlockchainConsumerObserver.onBlockchainDetach, this, height);

	foreach (var kv in m_subscriptions)
	{
	  kv.second.onBlockchainDetach(height);
	}
  }
  public override uint onNewBlocks(CompleteBlock[] blocks, uint startHeight, uint count)
  {
	Debug.Assert(blocks);
	Debug.Assert(count > 0);

//C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
//	struct Tx
//	{
//	  TransactionBlockInfo blockInfo;
//	  const ITransactionReader* tx;
//	  bool isLastTransactionInBlock;
//	};

//C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
//	struct PreprocessedTx : Tx, PreprocessInfo
//	{
//	};

	List<PreprocessedTx> preprocessedTransactions = new List<PreprocessedTx>();
	object preprocessedTransactionsMutex = new object();

	uint workers = std::thread.hardware_concurrency();
	if (workers == 0)
	{
	  workers = 2;
	}

	BlockingQueue<Tx> inputQueue = new BlockingQueue<Tx>(workers * 2);

	std::atomic<bool> stopProcessing = new std::atomic<bool>(false);
	std::atomic<uint> emptyBlockCount = new std::atomic<uint>(0);

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var pushingThread = std::async(std::launch.async, () =>
	{
	  for (uint i = 0; i < count && stopProcessing == null; ++i)
	  {
		auto block = blocks[i].block;

		if (!block.is_initialized())
		{
		  ++emptyBlockCount;
		  continue;
		}

		// filter by syncStartTimestamp
		if (m_syncStart.timestamp != 0 && block.timestamp < m_syncStart.timestamp)
		{
		  ++emptyBlockCount;
		  continue;
		}

		TransactionBlockInfo blockInfo = new TransactionBlockInfo();
		blockInfo.height = startHeight + i;
		blockInfo.timestamp = block.timestamp;
		blockInfo.transactionIndex = 0; // position in block

		foreach (var tx in blocks[i].transactions)
		{
		  var pubKey = tx.getTransactionPublicKey();
		  if (pubKey == NULL_PUBLIC_KEY)
		  {
			++blockInfo.transactionIndex;
			continue;
		  }

		  bool isLastTransactionInBlock = blockInfo.transactionIndex + 1 == blocks[i].transactions.size();
		  Tx item = new Tx(blockInfo, tx.get(), isLastTransactionInBlock);
		  inputQueue.push(new Tx(item));
		  ++blockInfo.transactionIndex;
		}
	  }

	  inputQueue.close();
	});

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var processingFunction = () =>
	{
	  Tx item = new Tx();
	  std::error_code ec = new std::error_code();
	  while (stopProcessing == null && inputQueue.pop(ref item))
	  {
		PreprocessedTx output = new PreprocessedTx();
		(Tx)output = item;

		ec.CopyFrom(preprocessOutputs(item.blockInfo, *item.tx, output));
		if (ec != null)
		{
		  stopProcessing = true;
		  break;
		}

		lock (preprocessedTransactionsMutex)
		{
			preprocessedTransactions.Add(std::move(output));
		}
	  }
	  return ec;
	};

	List<std::future<std::error_code>> processingThreads = new List<std::future<std::error_code>>();
	for (uint i = 0; i < workers; ++i)
	{
	  processingThreads.Add(std::async(std::launch.async, processingFunction));
	}

	std::error_code processingError = new std::error_code();
	foreach (var f in processingThreads)
	{
	  try
	  {
		std::error_code ec = f.get();
		if (processingError == null && ec != null)
		{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: processingError = ec;
		  processingError.CopyFrom(ec);
		}
	  }
	  catch (std::system_error e)
	  {
		processingError = e.code();
	  }
	  catch (System.Exception)
	  {
		processingError = std::make_error_code(std::errc.operation_canceled);
	  }
	}

	if (processingError != null)
	{
	  forEachSubscription((TransfersSubscription sub) =>
	  {
		sub.onError(processingError, startHeight);
	  });

	  return 0;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> blockHashes = getBlockHashes(blocks, count);
	List<Crypto.Hash> blockHashes = GlobalMembers.getBlockHashes(new CryptoNote.CompleteBlock(blocks), count);
	m_observerManager.notify(IBlockchainConsumerObserver.onBlocksAdded, this, blockHashes);

	// sort by block height and transaction index in block
//C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
//ORIGINAL LINE: std::sort(preprocessedTransactions.begin(), preprocessedTransactions.end(), [](const PreprocessedTx& a, const PreprocessedTx& b)
	preprocessedTransactions.Sort((PreprocessedTx a, PreprocessedTx b) =>
	{
	  return std::tie(a.blockInfo.height, a.blockInfo.transactionIndex) < std::tie(b.blockInfo.height, b.blockInfo.transactionIndex);
	});

	uint processedBlockCount = (uint)emptyBlockCount;
	try
	{
	  foreach (var tx in preprocessedTransactions)
	  {
		processTransaction(tx.blockInfo, *tx.tx, tx);

		if (tx.isLastTransactionInBlock)
		{
		  ++processedBlockCount;
		  m_logger.functorMethod(TRACE) << "Processed block " << (int)processedBlockCount << " of " << (int)count << ", last processed block index " << tx.blockInfo.height << ", hash " << blocks[processedBlockCount - 1].blockHash;

		  var newHeight = startHeight + processedBlockCount - 1;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: forEachSubscription([newHeight](TransfersSubscription& sub)
		  forEachSubscription((TransfersSubscription sub) =>
		  {
			  sub.advanceHeight(newHeight);
		  });
		}
	  }
	}
	catch (MarkTransactionConfirmedException e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process block transactions: failed to confirm transaction " << e.getTxHash() << ", remove this transaction from all containers and transaction pool";
	  forEachSubscription((TransfersSubscription sub) =>
	  {
		sub.deleteUnconfirmedTransaction(e.getTxHash());
	  });

	  m_poolTxs.erase(e.getTxHash());
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process block transactions, exception: " << e.Message;
	}
	catch
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process block transactions, unknown exception";
	}

	if (processedBlockCount < count)
	{
	  uint detachIndex = startHeight + processedBlockCount;
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Not all block transactions are processed, fully processed block count: " << (int)processedBlockCount << " of " << (int)count << ", last processed block hash " << (processedBlockCount > 0 ? blocks[processedBlockCount - 1].blockHash : GlobalMembers.NULL_HASH) << ", detach block index " << (int)detachIndex << " to remove partially processed block";
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: forEachSubscription([detachIndex](TransfersSubscription& sub)
	  forEachSubscription((TransfersSubscription sub) =>
	  {
		  sub.onBlockchainDetach(detachIndex);
	  });
	}

	return processedBlockCount;
  }
  public override std::error_code onPoolUpdated(List<std::unique_ptr<ITransactionReader>> addedTransactions, List<Hash> deletedTransactions)
  {
	TransactionBlockInfo unconfirmedBlockInfo = new TransactionBlockInfo();
	unconfirmedBlockInfo.timestamp = 0;
	unconfirmedBlockInfo.height = GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;

	std::error_code processingError = new std::error_code();
	foreach (var cryptonoteTransaction in addedTransactions)
	{
	  m_poolTxs.emplace(cryptonoteTransaction.getTransactionHash());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: processingError = processTransaction(unconfirmedBlockInfo, *cryptonoteTransaction.get());
	  processingError.CopyFrom(processTransaction(unconfirmedBlockInfo, *cryptonoteTransaction.get()));
	  if (processingError != null)
	  {
		foreach (var sub in m_subscriptions)
		{
		  sub.second.onError(processingError, GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
		}

		return processingError;
	  }
	}

	foreach (var deletedTxHash in deletedTransactions)
	{
	  m_poolTxs.erase(deletedTxHash);

	  m_observerManager.notify(IBlockchainConsumerObserver.onTransactionDeleteBegin, this, deletedTxHash);
	  foreach (var sub in m_subscriptions)
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		sub.second.deleteUnconfirmedTransaction(*reinterpret_cast<const Hash>(deletedTxHash));
	  }

	  m_observerManager.notify(IBlockchainConsumerObserver.onTransactionDeleteEnd, this, deletedTxHash);
	}

	return std::error_code();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const ClassicUnorderedSet<Crypto::Hash>& getKnownPoolTxIds() const override
  public override HashSet<Crypto.Hash> getKnownPoolTxIds()
  {
	return m_poolTxs;
  }

  public override std::error_code addUnconfirmedTransaction(ITransactionReader transaction)
  {
	TransactionBlockInfo unconfirmedBlockInfo = new TransactionBlockInfo();
	unconfirmedBlockInfo.height = GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
	unconfirmedBlockInfo.timestamp = 0;
	unconfirmedBlockInfo.transactionIndex = 0;

	return processTransaction(unconfirmedBlockInfo, transaction);
  }
  public override void removeUnconfirmedTransaction(Crypto.Hash transactionHash)
  {
	m_observerManager.notify(IBlockchainConsumerObserver.onTransactionDeleteBegin, this, transactionHash);
	foreach (var subscription in m_subscriptions)
	{
	  subscription.second.deleteUnconfirmedTransaction(transactionHash);
	}
	m_observerManager.notify(IBlockchainConsumerObserver.onTransactionDeleteEnd, this, transactionHash);
  }


//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename F>
  private void forEachSubscription<F>(F action)
  {
	foreach (var kv in m_subscriptions)
	{
	  action(*kv.second);
	}
  }

  private class PreprocessInfo
  {
	public Dictionary<Crypto.PublicKey, List<TransactionOutputInformationIn>> outputs = new Dictionary<Crypto.PublicKey, List<TransactionOutputInformationIn>>();
	public List<uint> globalIdxs = new List<uint>();
  }

  private std::error_code preprocessOutputs(TransactionBlockInfo blockInfo, ITransactionReader tx, PreprocessInfo info)
  {
	Dictionary<PublicKey, List<uint>> outputs = new Dictionary<PublicKey, List<uint>>();
	try
	{
	  GlobalMembers.findMyOutputs(tx, m_viewSecret, m_spendKeys, outputs);
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(WARNING, BRIGHT_RED) << "Failed to process transaction: " << e.Message << ", transaction hash " << Common.GlobalMembers.podToHex(tx.getTransactionHash());
	  return std::error_code();
	}

	if (outputs.Count == 0)
	{
	  return std::error_code();
	}

	std::error_code errorCode = new std::error_code();
	var txHash = tx.getTransactionHash();
	if (blockInfo.height != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: errorCode = getGlobalIndices(reinterpret_cast<const Hash&>(txHash), info.globalIdxs);
	  errorCode.CopyFrom(getGlobalIndices(reinterpret_cast<const Hash&>(txHash), info.globalIdxs));
	  if (errorCode != null)
	  {
		return errorCode;
	  }
	}

	foreach (var kv in outputs)
	{
	  var it = m_subscriptions.find(kv.first);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (it != m_subscriptions.end())
	  {
		auto transfers = info.outputs[kv.first];
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: errorCode = createTransfers(it->second->getKeys(), blockInfo, tx, kv.second, info.globalIdxs, transfers, m_logger);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		errorCode.CopyFrom(CryptoNote.GlobalMembers.createTransfers(it.second.getKeys(), blockInfo, tx, kv.second, info.globalIdxs, transfers, m_logger.functorMethod));
		if (errorCode != null)
		{
		  return errorCode;
		}
	  }
	}

	return std::error_code();
  }
  private std::error_code processTransaction(TransactionBlockInfo blockInfo, ITransactionReader tx)
  {
	PreprocessInfo info = new PreprocessInfo();
	var ec = preprocessOutputs(blockInfo, tx, info);
	if (ec != null)
	{
	  return ec;
	}

	processTransaction(blockInfo, tx, info);
	return std::error_code();
  }
  private void processTransaction(TransactionBlockInfo blockInfo, ITransactionReader tx, PreprocessInfo info)
  {
	List<TransactionOutputInformationIn> emptyOutputs = new List<TransactionOutputInformationIn>();
	List<ITransfersContainer> transactionContainers = new List<ITransfersContainer>();

	m_logger.functorMethod(TRACE) << "Process transaction, block " << (int)blockInfo.height << ", transaction index " << (int)blockInfo.transactionIndex << ", hash " << tx.getTransactionHash();
	bool someContainerUpdated = false;
	foreach (var kv in m_subscriptions)
	{
	  var it = info.outputs.find(kv.first);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  auto subscriptionOutputs = (it == info.outputs.end()) ? emptyOutputs : it.second;

	  bool containerContainsTx;
	  bool containerUpdated;
	  processOutputs(blockInfo, *kv.second, tx, subscriptionOutputs, info.globalIdxs, ref containerContainsTx, ref containerUpdated);
	  someContainerUpdated = someContainerUpdated || containerUpdated;
	  if (containerContainsTx)
	  {
		transactionContainers.emplace_back(kv.second.getContainer());
	  }
	}

	if (someContainerUpdated)
	{
	  m_logger.functorMethod(TRACE) << "Transaction updated some containers, hash " << tx.getTransactionHash();
	  m_observerManager.notify(IBlockchainConsumerObserver.onTransactionUpdated, this, tx.getTransactionHash(), transactionContainers);
	}
	else
	{
	  m_logger.functorMethod(TRACE) << "Transaction doesn't updated any container, hash " << tx.getTransactionHash();
	}
  }
  private void processOutputs(TransactionBlockInfo blockInfo, TransfersSubscription sub, ITransactionReader tx, List<TransactionOutputInformationIn> transfers, List<uint> globalIdxs, ref bool contains, ref bool updated)
  {

	TransactionInformation subscribtionTxInfo = new TransactionInformation();
	contains = sub.getContainer().getTransactionInformation(tx.getTransactionHash(), subscribtionTxInfo);
	updated = false;

	if (contains)
	{
	  if (subscribtionTxInfo.blockHeight == GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT && blockInfo.height != GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		try
		{
		  // pool->blockchain
		  sub.markTransactionConfirmed(blockInfo, tx.getTransactionHash(), globalIdxs);
		  updated = true;
		}
		catch
		{
			m_logger.functorMethod(ERROR, BRIGHT_RED) << "markTransactionConfirmed failed, throw MarkTransactionConfirmedException";
			throw new MarkTransactionConfirmedException(tx.getTransactionHash());
		}
	  }
	  else
	  {
		Debug.Assert(subscribtionTxInfo.blockHeight == blockInfo.height);
	  }
	}
	else
	{
	  updated = sub.addTransaction(blockInfo, tx, transfers);
	  contains = updated;
	}
  }

  private std::error_code getGlobalIndices(Hash transactionHash, List<uint> outsGlobalIndices)
  {
	std::promise<std::error_code> prom = new std::promise<std::error_code>();
	std::future<std::error_code> f = prom.get_future();

	INode.Callback cb = (std::error_code ec) =>
	{
	  std::promise<std::error_code> p = new std::promise<std::error_code>(std::move(prom));
	  p.set_value(ec);
	};

	outsGlobalIndices.Clear();
	m_node.getTransactionOutsGlobalIndices(transactionHash, outsGlobalIndices, cb);

	return f.get();
  }

  private void updateSyncStart()
  {
	SynchronizationStart start = new SynchronizationStart();

	start.height = ulong.MaxValue;
	start.timestamp = ulong.MaxValue;

	foreach (var kv in m_subscriptions)
	{
	  var subStart = kv.second.getSyncStart();
	  start.height = Math.Min(start.height, subStart.height);
	  start.timestamp = Math.Min(start.timestamp, subStart.timestamp);
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_syncStart = start;
	m_syncStart.CopyFrom(start);
  }

  private SynchronizationStart m_syncStart = new SynchronizationStart();
  private readonly Crypto.SecretKey m_viewSecret = new Crypto.SecretKey();
  // map { spend public key -> subscription }
  private Dictionary<Crypto.PublicKey, std::unique_ptr<TransfersSubscription>> m_subscriptions = new Dictionary<Crypto.PublicKey, std::unique_ptr<TransfersSubscription>>();
  private HashSet<Crypto.PublicKey> m_spendKeys = new HashSet<Crypto.PublicKey>();
  private HashSet<Crypto.Hash> m_poolTxs = new HashSet<Crypto.Hash>();

  private INode m_node;
  private readonly CryptoNote.Currency m_currency;
  private Logging.LoggerRef m_logger = new Logging.LoggerRef();
}

}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class MarkTransactionConfirmedException : System.Exception
{
	public MarkTransactionConfirmedException(Crypto.Hash txHash)
	{
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Hash& getTxHash() const
	public Hash getTxHash()
	{
		return m_txHash;
	}

	private Crypto.Hash m_txHash = new Crypto.Hash();
}


