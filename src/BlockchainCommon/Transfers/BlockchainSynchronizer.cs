// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using Crypto;
using Logging;
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

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public class BlockchainSynchronizer : INodeObserver, IObservableImpl<IBlockchainSynchronizerObserver, IBlockchainSynchronizer>
{

  public BlockchainSynchronizer(INode node, Logging.ILogger logger, Hash genesisBlockHash)
  {
	  this.m_logger = new Logging.LoggerRef(logger, "BlockchainSynchronizer");
	  this.m_node = new CryptoNote.INode(node);
	  this.m_genesisBlockHash = new Crypto.Hash(genesisBlockHash);
	  this.m_currentState = new CryptoNote.BlockchainSynchronizer.State.stopped;
	  this.m_futureState = new CryptoNote.BlockchainSynchronizer.State.stopped;
  }
  public new void Dispose()
  {
	stop();
	  base.Dispose();
  }

  // IBlockchainSynchronizer
  public override void addConsumer(IBlockchainConsumer consumer)
  {
	Debug.Assert(consumer != null);
	Debug.Assert(m_consumers.count(consumer) == 0);

	if (!(checkIfStopped() && checkIfShouldStop()))
	{
	  var message = "Failed to add consumer: not stopped";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", consumer " << consumer;
	  throw new System.Exception(message);
	}

	m_consumers.Add(consumer, new SynchronizationState(m_genesisBlockHash));
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Consumer added, consumer " << consumer << ", count " << m_consumers.Count;
  }
  public override bool removeConsumer(IBlockchainConsumer consumer)
  {
	Debug.Assert(consumer != null);

	if (!(checkIfStopped() && checkIfShouldStop()))
	{
	  var message = "Failed to remove consumer: not stopped";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", consumer " << consumer;
	  throw new System.Exception(message);
	}

	bool result = m_consumers.Remove(consumer) > 0;
	if (result)
	{
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Consumer removed, consumer " << consumer << ", count " << m_consumers.Count;
	}
	else
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to remove consumer: not found, consumer " << consumer;
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IStreamSerializable* getConsumerState(IBlockchainConsumer* consumer) const override
  public override IStreamSerializable getConsumerState(IBlockchainConsumer consumer)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);
	return getConsumerSynchronizationState(consumer);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getConsumerKnownBlocks(IBlockchainConsumer& consumer) const override
  public override List<Crypto.Hash> getConsumerKnownBlocks(IBlockchainConsumer consumer)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);

	var state = getConsumerSynchronizationState(consumer);
	if (state == null)
	{
	  var message = "Failed to get consumer known blocks: not found";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", consumer " << consumer;
	  throw new System.ArgumentException(message);
	}

	return state.getKnownBlockHashes();
  }

  public override std::future<std::error_code> addUnconfirmedTransaction(ITransactionReader transaction)
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Adding unconfirmed transaction, hash " << transaction.getTransactionHash();

	std::unique_lock<object> @lock = new std::unique_lock<object>(m_stateMutex);

	if (m_currentState == State.stopped || m_futureState == State.stopped)
	{
	  var message = "Failed to add unconfirmed transaction: not stopped";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", hash " << transaction.getTransactionHash();
	  throw new System.Exception(message);
	}

	std::promise<std::error_code> promise = new std::promise<std::error_code>();
	var future = promise.get_future();
	m_addTransactionTasks.emplace_back(transaction, std::move(promise));
	m_hasWork.notify_one();

	return future;
  }
  public override std::future removeUnconfirmedTransaction(Crypto.Hash transactionHash)
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Removing unconfirmed transaction, hash " << transactionHash;

	std::unique_lock<object> @lock = new std::unique_lock<object>(m_stateMutex);

	if (m_currentState == State.stopped || m_futureState == State.stopped)
	{
	  var message = "Failed to remove unconfirmed transaction: not stopped";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", hash " << transactionHash;
	  throw new System.Exception(message);
	}

	std::promise promise = new std::promise();
	var future = promise.get_future();
	m_removeTransactionTasks.emplace_back(transactionHash, std::move(promise));
	m_hasWork.notify_one();

	return future;
  }

  public override void start()
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Starting...";

	if (m_consumers.Count == 0)
	{
	  var message = "Failed to start: no consumers";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
	  throw new System.Exception(message);
	}

	State nextState;
	if (!wasStarted)
	{
	  nextState = State.deleteOldTxs;
	  wasStarted = true;
	}
	else
	{
	  nextState = State.blockchainSync;
	}

	if (!setFutureStateIf(nextState, () =>
	{
		//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
		return m_currentState == State.stopped && m_futureState == State.stopped;
	}))
	{
	  var message = "Failed to start: already started";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
	  throw new System.Exception(message);
	}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: workingThread.reset(new std::thread([this]
	workingThread.reset(new std::thread(() =>
	{
		workingProcedure();
	}));
  }
  public override void stop()
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Stopping...";
	setFutureState.stopped;

	// wait for previous processing to end
	if (workingThread.get() != null && workingThread.joinable())
	{
	  workingThread.join();
	}

	workingThread.reset();
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Stopped";
  }

  // IStreamSerializable
  public override void save(std::ostream os)
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Saving...";
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	os.write(reinterpret_cast<const char>(m_genesisBlockHash), sizeof(Crypto.Hash));
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Saved";
  }
  public override void load(std::istream in)
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Loading...";
	Hash genesisBlockHash = new Hash();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	in.read(reinterpret_cast<char>(genesisBlockHash), sizeof(Hash));
	if (genesisBlockHash != m_genesisBlockHash)
	{
	  var message = "Failed to load: genesis block hash does not match stored state";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", read " << genesisBlockHash << ", expected " << m_genesisBlockHash;
	  throw new System.Exception(message);
	}

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Loaded";
  }

  // INodeObserver
  public override void localBlockchainUpdated(uint height)
  {
	m_logger.functorMethod(DEBUGGING) << "Event: localBlockchainUpdated " << height;
	setFutureState.blockchainSync;
  }
  public override void lastKnownBlockHeightUpdated(uint height)
  {
	m_logger.functorMethod(DEBUGGING) << "Event: lastKnownBlockHeightUpdated " << height;
	setFutureState.blockchainSync;
  }
  public override void poolChanged()
  {
	m_logger.functorMethod(DEBUGGING) << "Event: poolChanged";
	setFutureState.poolSync;
  }


  private class GetBlocksResponse
  {
	public uint startHeight = new uint();
	public List<BlockShortEntry> newBlocks = new List<BlockShortEntry>();
  }

  private class GetBlocksRequest
  {
	public GetBlocksRequest()
	{
	  syncStart.timestamp = 0;
	  syncStart.height = 0;
	}
	public SynchronizationStart syncStart = new SynchronizationStart();
	public List<Crypto.Hash> knownBlocks = new List<Crypto.Hash>();
  }

  private class GetPoolResponse
  {
	public bool isLastKnownBlockActual;
	public List<std::unique_ptr<ITransactionReader>> newTxs = new List<std::unique_ptr<ITransactionReader>>();
	public List<Crypto.Hash> deletedTxIds = new List<Crypto.Hash>();
  }

  private class GetPoolRequest
  {
	public List<Crypto.Hash> knownTxIds = new List<Crypto.Hash>();
	public Crypto.Hash lastKnownBlock = new Crypto.Hash();
  }

  private enum State
  { //prioritized finite states
	idle = 0, //DO
	poolSync = 1, //NOT
	blockchainSync = 2, //REORDER
	deleteOldTxs = 3, //!!!
	stopped = 4 //!!!
  }

  private enum UpdateConsumersResult
  {
	nothingChanged = 0,
	addedNewBlocks = 1,
	errorOccurred = 2
  }

  //void startSync();
  private void removeOutdatedTransactions()
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Removing outdated pool transactions...";

	HashSet<Crypto.Hash> unionPoolHistory = new HashSet<Crypto.Hash>();
	HashSet<Crypto.Hash> ignored = new HashSet<Crypto.Hash>();
	getPoolUnionAndIntersection(ref unionPoolHistory, ref ignored);

	GetPoolRequest request = new GetPoolRequest();
	request.knownTxIds.assign(unionPoolHistory.GetEnumerator(), unionPoolHistory.end());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: request.lastKnownBlock = lastBlockId;
	request.lastKnownBlock.CopyFrom(lastBlockId);

	GetPoolResponse response = new GetPoolResponse();
	response.isLastKnownBlockActual = false;

	std::error_code ec = getPoolSymmetricDifferenceSync(std::move(request), response);

	if (ec == null)
	{
	  m_logger.functorMethod(DEBUGGING) << "Outdated pool transactions received, " << response.deletedTxIds.Count << ':' << Common.GlobalMembers.makeContainerFormatter(response.deletedTxIds);

	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_consumersMutex);
	  foreach (var consumer in m_consumers)
	  {
		ec = consumer.first.onPoolUpdated({}, response.deletedTxIds);
		if (ec != null)
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process outdated pool transactions: " << ec << ", " << ec.message() << ", consumer " << consumer.first;
		  break;
		}
	  }
	}
	else
	{
	  m_logger.functorMethod(DEBUGGING, BRIGHT_RED) << "Failed to query outdated pool transaction: " << ec << ", " << ec.message();
	}

	if (ec == null)
	{
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Outdated pool transactions processed";
	}
	else
	{
	  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, ec);

	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Retry in " << GlobalMembers.RETRY_TIMEOUT << " seconds...";
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_stateMutex);
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: bool stopped = m_hasWork.wait_for(lock, std::chrono::seconds(RETRY_TIMEOUT), [this]
	  bool stopped = m_hasWork.wait_for(@lock, std::chrono.seconds(GlobalMembers.RETRY_TIMEOUT), () =>
	  {
		return m_futureState == State.stopped;
	  });

	  if (!stopped)
	  {
		m_futureState = State.deleteOldTxs;
	  }
	}
  }
  private void startPoolSync()
  {
	m_logger.functorMethod(DEBUGGING) << "Starting pool synchronization...";

	HashSet<Crypto.Hash> unionPoolHistory = new HashSet<Crypto.Hash>();
	HashSet<Crypto.Hash> intersectedPoolHistory = new HashSet<Crypto.Hash>();
	getPoolUnionAndIntersection(ref unionPoolHistory, ref intersectedPoolHistory);

	GetPoolRequest unionRequest = new GetPoolRequest();
	unionRequest.knownTxIds.assign(unionPoolHistory.GetEnumerator(), unionPoolHistory.end());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: unionRequest.lastKnownBlock = lastBlockId;
	unionRequest.lastKnownBlock.CopyFrom(lastBlockId);

	GetPoolResponse unionResponse = new GetPoolResponse();
	unionResponse.isLastKnownBlockActual = false;

	std::error_code ec = getPoolSymmetricDifferenceSync(std::move(unionRequest), unionResponse);

	if (ec != null)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to query transaction pool changes: " << ec << ", " << ec.message();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: setFutureStateIf(State::idle, [this]
	  setFutureStateIf(State.idle, () =>
	  {
		  return m_futureState != State.stopped;
	  });
	  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, ec);
	}
	else
	{ //get union ok
	  if (!unionResponse.isLastKnownBlockActual)
	  { //bc outdated
		m_logger.functorMethod(DEBUGGING) << "Transaction pool changes received, but blockchain has been changed";
		setFutureState.blockchainSync;
	  }
	  else
	  {
		m_logger.functorMethod(DEBUGGING) << "Transaction pool changes received, added " << unionResponse.newTxs.Count << ", deleted " << unionResponse.deletedTxIds.Count;

		if (unionPoolHistory == intersectedPoolHistory)
		{ //usual case, start pool processing
		  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, processPoolTxs(unionResponse));
		}
		else
		{
		  GetPoolRequest intersectionRequest = new GetPoolRequest();
		  intersectionRequest.knownTxIds.assign(intersectedPoolHistory.GetEnumerator(), intersectedPoolHistory.end());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: intersectionRequest.lastKnownBlock = lastBlockId;
		  intersectionRequest.lastKnownBlock.CopyFrom(lastBlockId);

		  GetPoolResponse intersectionResponse = new GetPoolResponse();
		  intersectionResponse.isLastKnownBlockActual = false;

		  std::error_code ec2 = getPoolSymmetricDifferenceSync(std::move(intersectionRequest), intersectionResponse);

		  if (ec2 != null)
		  {
			m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to query transaction pool changes, stage 2: " << ec << ", " << ec.message();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: setFutureStateIf(State::idle, [this]
			setFutureStateIf(State.idle, () =>
			{
				return m_futureState != State.stopped;
			});
			m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, ec2);
		  }
		  else
		  { //get intersection ok
			if (!intersectionResponse.isLastKnownBlockActual)
			{ //bc outdated
			  m_logger.functorMethod(DEBUGGING) << "Transaction pool changes at stage 2 received, but blockchain has been changed";
			  setFutureState.blockchainSync;
			}
			else
			{
			  m_logger.functorMethod(DEBUGGING) << "Transaction pool changes at stage 2 received, added " << intersectionResponse.newTxs.Count << ", deleted " << intersectionResponse.deletedTxIds.Count;
			  intersectionResponse.deletedTxIds.assign(unionResponse.deletedTxIds.GetEnumerator(), unionResponse.deletedTxIds.end());
			  std::error_code ec3 = processPoolTxs(intersectionResponse);

			  //notify about error, or success
			  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, ec3);
			}
		  }
		}
	  }
	}
  }
  private void startBlockchainSync()
  {
	m_logger.functorMethod(DEBUGGING) << "Starting blockchain synchronization...";

	GetBlocksResponse response = new GetBlocksResponse();
	GetBlocksRequest req = getCommonHistory();

	try
	{
	  if (req.knownBlocks.Count > 0)
	  {
		var queryBlocksCompleted = std::promise<std::error_code>();
		var queryBlocksWaitFuture = queryBlocksCompleted.get_future();

		m_node.queryBlocks(std::move(req.knownBlocks), new ulong(req.syncStart.timestamp), response.newBlocks, response.startHeight, (std::error_code ec) =>
		{
			var detachedPromise = std::move(queryBlocksCompleted);
			detachedPromise.set_value(ec);
		});

		std::error_code ec = queryBlocksWaitFuture.get();

		if (ec != null)
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to query blocks: " << ec << ", " << ec.message();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: setFutureStateIf(State::idle, [this]
		  setFutureStateIf(State.idle, () =>
		  {
			  return m_futureState != State.stopped;
		  });
		  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, ec);
		}
		else
		{
		  m_logger.functorMethod(DEBUGGING) << "Blocks received, start index " << response.startHeight << ", count " << response.newBlocks.Count;
		  processBlocks(response);
		}
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to query and process blocks: " << e.Message;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: setFutureStateIf(State::idle, [this]
	  setFutureStateIf(State.idle, () =>
	  {
		  return m_futureState != State.stopped;
	  });
	  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, std::make_error_code(std::errc.invalid_argument));
	}
  }

  private void processBlocks(GetBlocksResponse response)
  {
	m_logger.functorMethod(DEBUGGING) << "Process blocks, start index " << response.startHeight << ", count " << response.newBlocks.Count;

	BlockchainInterval interval = new BlockchainInterval();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: interval.startHeight = response.startHeight;
	interval.startHeight.CopyFrom(response.startHeight);
	List<CompleteBlock> blocks = new List<CompleteBlock>();

	foreach (var block in response.newBlocks)
	{
	  if (checkIfShouldStop())
	  {
		break;
	  }

	  CompleteBlock completeBlock = new CompleteBlock();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: completeBlock.blockHash = block.blockHash;
	  completeBlock.blockHash.CopyFrom(block.blockHash);
	  if (block.hasBlock)
	  {
		completeBlock.block = std::move(block.block);
		completeBlock.transactions.AddLast(createTransactionPrefix(completeBlock.block.baseTransaction));

		try
		{
		  foreach (var txShortInfo in block.txsShortInfo)
		  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
			completeBlock.transactions.AddLast(createTransactionPrefix(txShortInfo.txPrefix, reinterpret_cast<const Hash&>(txShortInfo.txId)));
		  }
		}
		catch (System.Exception e)
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process blocks: " << e.Message;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: setFutureStateIf(State::idle, [this]
		  setFutureStateIf(State.idle, () =>
		  {
			  return m_futureState != State.stopped;
		  });
		  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, std::make_error_code(std::errc.invalid_argument));
		  return;
		}
	  }

	  interval.blocks.Add(completeBlock.blockHash);
	  blocks.Add(std::move(completeBlock));
	}

	uint processedBlockCount = response.startHeight + (uint)response.newBlocks.Count;
	if (!checkIfShouldStop())
	{
	  response.newBlocks.Clear();
	  std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);
	  var result = updateConsumers(interval, blocks);
	  lk.unlock();

	  switch (result)
	  {
	  case UpdateConsumersResult.errorOccurred:
		if (setFutureStateIf(State.idle, () =>
		{
			//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
			return m_futureState != State.stopped;
		}))
		{
		  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, std::make_error_code(std::errc.invalid_argument));
		}
		break;

	  case UpdateConsumersResult.nothingChanged:
		if (m_node.getKnownBlockCount() != m_node.getLocalBlockCount())
		{
		  m_logger.functorMethod(DEBUGGING) << "Blockchain updated, resume blockchain synchronization";
		  std::this_thread.sleep_for(std::chrono.milliseconds(100));
		}
		else
		{
		  break;
		}

	  case UpdateConsumersResult.addedNewBlocks:
		setFutureState.blockchainSync;
		m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationProgressUpdated, processedBlockCount, Math.Max(m_node.getKnownBlockCount(), m_node.getLocalBlockCount()));
		break;
	  }
	}

	if (checkIfShouldStop())
	{ //Sic!
	  m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Block processing is interrupted";
	  m_observerManager.notify(IBlockchainSynchronizerObserver.synchronizationCompleted, std::make_error_code(std::errc.interrupted));
	}
  }

  /// \pre m_consumersMutex is locked
  private BlockchainSynchronizer.UpdateConsumersResult updateConsumers(BlockchainInterval interval, List<CompleteBlock> blocks)
  {
	Debug.Assert(interval.blocks.Count == blocks.Count);

	bool smthChanged = false;
	bool hasErrors = false;

	uint lastBlockIndex = uint.MaxValue;
	foreach (var kv in m_consumers)
	{
	  var result = kv.second.checkInterval(interval);

	  if (result.detachRequired)
	  {
		m_logger.functorMethod(DEBUGGING) << "Detach consumer, consumer " << kv.first << ", block index " << result.detachHeight;
		kv.first.onBlockchainDetach(result.detachHeight);
		kv.second.detach(result.detachHeight);
	  }

	  if (result.newBlockHeight == 1)
	  {
		result.newBlockHeight = 0;
	  }
	  if (result.hasNewBlocks)
	  {
		uint startOffset = result.newBlockHeight - interval.startHeight;
	  if (result.newBlockHeight == 0)
	  {
		startOffset = 0;
	  }
		uint blockCount = (uint)blocks.Count - startOffset;
		// update consumer
		m_logger.functorMethod(DEBUGGING) << "Adding blocks to consumer, consumer " << kv.first << ", start index " << result.newBlockHeight << ", count " << blockCount;
		uint addedCount = kv.first.onNewBlocks(blocks.data() + startOffset, result.newBlockHeight, blockCount);
		if (addedCount > 0)
		{
		  if (addedCount < blockCount)
		  {
			m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add " << (blockCount - addedCount) << " blocks of " << blockCount << " to consumer, consumer " << kv.first;
			hasErrors = true;
		  }

		  // update state if consumer succeeded
		  kv.second.addBlocks(interval.blocks.data() + startOffset, result.newBlockHeight, addedCount);
		  smthChanged = true;
		}
		else
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add blocks to consumer, consumer " << kv.first;
		  hasErrors = true;
		}

		if (addedCount > 0)
		{
		  lastBlockIndex = Math.Min(lastBlockIndex, startOffset + addedCount - 1);
		}
	  }
	}

	if (lastBlockIndex != uint.MaxValue)
	{
	  Debug.Assert(lastBlockIndex < blocks.Count);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastBlockId = blocks[lastBlockIndex].blockHash;
	  lastBlockId.CopyFrom(blocks[lastBlockIndex].blockHash);
	  m_logger.functorMethod(DEBUGGING) << "Last block hash " << lastBlockId << ", index " << (interval.startHeight + lastBlockIndex);
	}

	if (hasErrors)
	{
	  m_logger.functorMethod(DEBUGGING) << "Not all blocks were added to consumers, there were errors";
	  return UpdateConsumersResult.errorOccurred;
	}
	else if (smthChanged)
	{
	  m_logger.functorMethod(DEBUGGING) << "Blocks added to consumers";
	  return UpdateConsumersResult.addedNewBlocks;
	}
	else
	{
	  m_logger.functorMethod(DEBUGGING) << "No new blocks received. Consumers not updated";
	  return UpdateConsumersResult.nothingChanged;
	}
  }
  private std::error_code processPoolTxs(GetPoolResponse response)
  {
	m_logger.functorMethod(DEBUGGING) << "Starting to process pool transactions, added " << response.newTxs.Count << ':' << new TransactionReaderListFormatter(response.newTxs) << ", deleted " << response.deletedTxIds.Count << ':' << Common.GlobalMembers.makeContainerFormatter(response.deletedTxIds);

	std::error_code error = new std::error_code();
	{
	  std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);
	  foreach (var consumer in m_consumers)
	  {
		if (checkIfShouldStop())
		{ //if stop, return immediately, without notification
		  m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Pool transactions processing is interrupted";
		  return std::make_error_code(std::errc.interrupted);
		}

		error = consumer.first.onPoolUpdated(response.newTxs, response.deletedTxIds);
		if (error != null)
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to process pool transactions: " << error << ", " << error.message() << ", consumer " << consumer.first;
		  break;
		}
	  }
	}

	if (error == null)
	{
	  m_logger.functorMethod(DEBUGGING) << "Pool changes processed";
	}

	return error;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private std::error_code getPoolSymmetricDifferenceSync(GetPoolRequest && request, GetPoolResponse response)
  {
	var promise = std::promise<std::error_code>();
	var future = promise.get_future();

	m_node.getPoolSymmetricDifference(std::move(request.knownTxIds), std::move(request.lastKnownBlock), ref response.isLastKnownBlockActual, response.newTxs, response.deletedTxIds, (std::error_code ec) =>
	{
		var detachedPromise = std::move(promise);
		detachedPromise.set_value(ec);
	});

	return future.get();
  }
  private std::error_code doAddUnconfirmedTransaction(ITransactionReader transaction)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);

	std::error_code ec = new std::error_code();
	var addIt = m_consumers.GetEnumerator();
	while (addIt.MoveNext())
	{
	  ec = addIt.Current.Key.addUnconfirmedTransaction(transaction);
	  if (ec != null)
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add unconfirmed transaction to consumer: " << ec << ", " << ec.message() << ", consumer " << addIt.Current.Key << ", hash " << transaction.getTransactionHash();
		break;
	  }
	}

	if (ec != null)
	{
	  var transactionHash = transaction.getTransactionHash();
	  for (var rollbackIt = m_consumers.GetEnumerator(); rollbackIt != addIt; ++rollbackIt)
	  {
		rollbackIt.first.removeUnconfirmedTransaction(transactionHash);
	  }
	}
	else
	{
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Unconfirmed transaction added, hash " << transaction.getTransactionHash();
	}

	return ec;
  }
  private void doRemoveUnconfirmedTransaction(Crypto.Hash transactionHash)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);

	foreach (var consumer in m_consumers)
	{
	  consumer.first.removeUnconfirmedTransaction(transactionHash);
	}

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Unconfirmed transaction removed, hash " << transactionHash;
  }

  ///second parameter is used only in case of errors returned into callback from INode, such as aborted or connection lost

  //--------------------------- FSM ------------------------------------

  private bool setFutureState(State s)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: return setFutureStateIf(s, [this, s]
	return setFutureStateIf(s, () =>
	{
		return s > m_futureState;
	});
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private bool setFutureStateIf(State s, Func<bool>&& pred)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_stateMutex);
	if (pred())
	{
	  m_futureState = s;
	  m_hasWork.notify_one();
	  return true;
	}

	return false;
  }

  private void actualizeFutureState()
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_stateMutex);
	if (m_currentState == State.stopped && (m_futureState == State.deleteOldTxs || m_futureState == State.blockchainSync))
	{ // start(), immideately attach observer
	  m_node.addObserver(this);
	}

	if (m_futureState == State.stopped && m_currentState != State.stopped)
	{ // stop(), immideately detach observer
	  m_node.removeObserver(this);
	}

	while (m_removeTransactionTasks.Count > 0)
	{
	  auto task = m_removeTransactionTasks.First.Value;
	  Crypto.Hash transactionHash = task.first;
	  var detachedPromise = std::move(task.second);
	  m_removeTransactionTasks.RemoveFirst();

	  try
	  {
		doRemoveUnconfirmedTransaction(transactionHash);
		detachedPromise.set_value();
	  }
	  catch
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to remove unconfirmed transaction, hash " << transactionHash;
		detachedPromise.set_exception(std::current_exception());
	  }
	}

	while (m_addTransactionTasks.Count > 0)
	{
	  auto task = m_addTransactionTasks.First.Value;
	  ITransactionReader transaction = task.first;
	  var detachedPromise = std::move(task.second);
	  m_addTransactionTasks.RemoveFirst();

	  try
	  {
		var ec = doAddUnconfirmedTransaction(transaction);
		detachedPromise.set_value(ec);
	  }
	  catch
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add unconfirmed transaction, hash " << transaction.getTransactionHash();
		detachedPromise.set_exception(std::current_exception());
	  }
	}

	m_currentState = m_futureState;
	switch (m_futureState)
	{
	case State.stopped:
	  break;
	case State.deleteOldTxs:
	  m_futureState = State.blockchainSync;
	  lk.unlock();
	  removeOutdatedTransactions();
	  break;
	case State.blockchainSync:
	  m_futureState = State.poolSync;
	  lk.unlock();
	  startBlockchainSync();
	  break;
	case State.poolSync:
	  m_futureState = State.idle;
	  lk.unlock();
	  startPoolSync();
	  break;
	case State.idle:
	  m_logger.functorMethod(DEBUGGING) << "Idle";
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_hasWork.wait(lk, [this]
	  m_hasWork.wait(lk, () =>
	  {
		return m_futureState != State.idle || m_removeTransactionTasks.Count > 0 || m_addTransactionTasks.Count > 0;
	  });
	  m_logger.functorMethod(DEBUGGING) << "Resume";
	  lk.unlock();
	  break;
	default:
	  break;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfShouldStop() const
  private bool checkIfShouldStop()
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_stateMutex);
	return m_futureState == State.stopped;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfStopped() const
  private bool checkIfStopped()
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_stateMutex);
	return m_currentState == State.stopped;
  }

  private void workingProcedure()
  {
	m_logger.functorMethod(DEBUGGING) << "Working thread started";

	while (!checkIfShouldStop())
	{
	  actualizeFutureState();
	}

	actualizeFutureState();

	m_logger.functorMethod(DEBUGGING) << "Working thread stopped";
  }

  private BlockchainSynchronizer.GetBlocksRequest getCommonHistory()
  {
	GetBlocksRequest request = new GetBlocksRequest();
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);
	if (m_consumers.Count == 0)
	{
	  return request;
	}

	var shortest = m_consumers.GetEnumerator();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	var syncStart = shortest.first.getSyncStart();
	var it = shortest;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	++it;
	while (it.MoveNext())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (it.Current.Value.getHeight() < shortest.second.getHeight())
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: shortest = it;
		shortest.CopyFrom(it);
	  }

	  var consumerStart = it.Current.Key.getSyncStart();
	  syncStart.timestamp = Math.Min(syncStart.timestamp, consumerStart.timestamp);
	  syncStart.height = Math.Min(syncStart.height, consumerStart.height);
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	m_logger.functorMethod(DEBUGGING) << "Shortest chain size " << shortest.second.getHeight();

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	request.knownBlocks = shortest.second.getShortHistory(m_node.getLastLocalBlockHeight());
	request.syncStart = syncStart;

	m_logger.functorMethod(DEBUGGING) << "Common history: start block index " << request.syncStart.height << ", sparse chain size " << request.knownBlocks.Count;

	return request;
  }
  //--------------------------- FSM END ------------------------------------

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void getPoolUnionAndIntersection(ClassicUnorderedSet<Crypto::Hash>& poolUnion, ClassicUnorderedSet<Crypto::Hash>& poolIntersection) const
  private void getPoolUnionAndIntersection(ref HashSet<Crypto.Hash> poolUnion, ref HashSet<Crypto.Hash> poolIntersection)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_consumersMutex);

	var itConsumers = m_consumers.GetEnumerator();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	poolUnion = itConsumers.first.getKnownPoolTxIds();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	poolIntersection = itConsumers.first.getKnownPoolTxIds();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	++itConsumers;

	while (itConsumers.MoveNext())
	{
	  HashSet<Crypto.Hash> consumerKnownIds = itConsumers.Current.Key.getKnownPoolTxIds();

	  poolUnion.insert(consumerKnownIds.GetEnumerator(), consumerKnownIds.end());

	  for (var itIntersection = poolIntersection.GetEnumerator(); itIntersection != poolIntersection.end();)
	  {
		if (consumerKnownIds.count(*itIntersection) == 0)
		{
		  itIntersection = poolIntersection.erase(itIntersection);
		}
		else
		{
		  ++itIntersection;
		}
	  }
	}

	m_logger.functorMethod(DEBUGGING) << "Pool union size " << poolUnion.Count << ", intersection size " << poolIntersection.Count;
  }

  ///pre: m_consumersMutex is locked
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SynchronizationState* getConsumerSynchronizationState(IBlockchainConsumer* consumer) const
  private SynchronizationState getConsumerSynchronizationState(IBlockchainConsumer consumer)
  {
	Debug.Assert(consumer != null);

	if (!(checkIfStopped() && checkIfShouldStop()))
	{
	  var message = "Failed to get consumer state: not stopped";
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ", consumer " << consumer;
	  throw new System.Exception(message);
	}

	var it = m_consumers.find(consumer);
	if (it == m_consumers.end())
	{
	  return null;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return it.second.get();
  }


  private Logging.LoggerRef m_logger = new Logging.LoggerRef();
  private SortedDictionary<IBlockchainConsumer, SynchronizationState> m_consumers = new SortedDictionary<IBlockchainConsumer, SynchronizationState>();
  private INode m_node;
  private readonly Crypto.Hash m_genesisBlockHash = new Crypto.Hash();

  private Crypto.Hash lastBlockId = new Crypto.Hash();

  private State m_currentState;
  private State m_futureState;
  private std::unique_ptr<std::thread> workingThread = new std::unique_ptr<std::thread>();
  private readonly LinkedList<Tuple<ITransactionReader, std::promise<std::error_code>>> m_addTransactionTasks = new LinkedList<Tuple<ITransactionReader, std::promise<std::error_code>>>();
  private readonly LinkedList<Tuple<Crypto.Hash, std::promise>> m_removeTransactionTasks = new LinkedList<Tuple<Crypto.Hash, std::promise>>();

  private object m_consumersMutex = new object();
  private object m_stateMutex = new object();
  private std::condition_variable m_hasWork = new std::condition_variable();

  private bool wasStarted = false;
}

}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class TransactionReaderListFormatter
{
  public TransactionReaderListFormatter(List<std::unique_ptr<CryptoNote.ITransactionReader>> transactionList)
  {
	  this.m_transactionList = transactionList;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void print(std::ostream& os) const
  public void print(std::ostream os)
  {
	os << '{';

	if (m_transactionList.Count > 0)
	{
	  os << m_transactionList[0].getTransactionHash();
	  for (var it = std::next(m_transactionList.GetEnumerator()); it != m_transactionList.end(); ++it)
	  {
		os << ", " << it.getTransactionHash();
	  }
	}

	os << '}';
  }

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& os, const TransactionReaderListFormatter& formatter)
  public static std::ostream operator << (std::ostream os, TransactionReaderListFormatter formatter)
  {
	formatter.print(os);
	return os;
  }

  private readonly List<std::unique_ptr<CryptoNote.ITransactionReader>> m_transactionList;
}


