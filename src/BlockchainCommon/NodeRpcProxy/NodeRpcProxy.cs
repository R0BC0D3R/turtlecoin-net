// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using Crypto;
using Common;
using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class ContextGroup;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class Dispatcher;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class Event;
}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class HttpClient;

public class INodeRpcProxyObserver : System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public virtual void connectionStatusUpdated(bool connected)
  {
  }
}

public class NodeRpcProxy : CryptoNote.INode
{
  public NodeRpcProxy(string nodeHost, ushort nodePort, Logging.ILogger logger)
  {
	  this.m_logger = new Logging.LoggerRef(logger, "NodeRpcProxy");
	  this.m_rpcTimeout = 10000;
	  this.m_pullInterval = 5000;
	  this.m_nodeHost = nodeHost;
	  this.m_nodePort = nodePort;
	  this.m_connected = true;
	  this.m_peerCount = 0;
	  this.m_networkHeight = 0;
	  this.m_nodeHeight = 0;
	resetInternalState();
  }
  public override void Dispose()
  {
	try
	{
	  shutdown();
	}
	catch (System.Exception)
	{
	}
	  base.Dispose();
  }

  public override bool addObserver(INodeObserver observer)
  {
	return m_observerManager.add(observer);
  }
  public override bool removeObserver(INodeObserver observer)
  {
	return m_observerManager.remove(observer);
  }

  public virtual bool addObserver(CryptoNote.INodeRpcProxyObserver observer)
  {
	return m_rpcProxyObserverManager.add(observer);
  }
  public virtual bool removeObserver(CryptoNote.INodeRpcProxyObserver observer)
  {
	return m_rpcProxyObserverManager.remove(observer);
  }

  public override void init(INode.Callback callback)
  {
	lock (m_mutex)
	{
    
		if (m_state != State.STATE_NOT_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.ALREADY_INITIALIZED));
		  return;
		}
	}

	m_state = State.STATE_INITIALIZING;
	resetInternalState();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_workerThread = std::thread([this, callback]
	m_workerThread = std::thread(() =>
	{
	  workerThread(callback);
	});
  }
  public override bool shutdown()
  {
	if (m_workerThread.joinable())
	{
	  m_workerThread.detach();
	}

	m_state = State.STATE_NOT_INITIALIZED;

	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPeerCount() const override
  public override uint getPeerCount()
  {
	return m_peerCount.load(std::memory_order_relaxed);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastLocalBlockHeight() const override
  public override uint getLastLocalBlockHeight()
  {
	lock (m_mutex)
	{
		return lastLocalBlockHeaderInfo.index;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastKnownBlockHeight() const override
  public override uint getLastKnownBlockHeight()
  {
	return m_networkHeight.load(std::memory_order_relaxed);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLocalBlockCount() const override
  public override uint getLocalBlockCount()
  {
	lock (m_mutex)
	{
		return lastLocalBlockHeaderInfo.index + 1;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getKnownBlockCount() const override
  public override uint getKnownBlockCount()
  {
	return m_networkHeight.load(std::memory_order_relaxed) + 1;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getLastLocalBlockTimestamp() const override
  public override ulong getLastLocalBlockTimestamp()
  {
	lock (m_mutex)
	{
		return lastLocalBlockHeaderInfo.timestamp;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getNodeHeight() const override
  public override ulong getNodeHeight()
  {
	return m_nodeHeight.load(std::memory_order_relaxed);
  }

  public override string getInfo()
  {
	CryptoNote.COMMAND_RPC_GET_INFO.request ireq = new CryptoNote.COMMAND_RPC_GET_INFO.request();
	CryptoNote.COMMAND_RPC_GET_INFO.response iresp = new CryptoNote.COMMAND_RPC_GET_INFO.response();

	std::error_code ec = jsonCommand("/getinfo", ireq, iresp);

	if (ec != null || iresp.status != DefineConstants.CORE_RPC_STATUS_OK)
	{
	  return "Problem retrieving information from RPC server.";
	}

	return Common.get_status_string(iresp);
  }
  public override void getFeeInfo()
  {
	CryptoNote.COMMAND_RPC_GET_FEE_ADDRESS.request ireq = boost::value_initialized<decltype(ireq)>();
	CryptoNote.COMMAND_RPC_GET_FEE_ADDRESS.response iresp = boost::value_initialized<decltype(iresp)>();

	std::error_code ec = jsonCommand("/feeinfo", ireq, iresp);

	if (ec != null || iresp.status != DefineConstants.CORE_RPC_STATUS_OK)
	{
	  return;
	}
	m_fee_address = iresp.address;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_fee_amount = iresp.amount;
	m_fee_amount.CopyFrom(iresp.amount);

	return;
  }

  public override void getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount, List<Crypto.Hash> blockHashes, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetBlockHashesByTimestamps, this, timestampBegin, secondsCount, std::@ref(blockHashes)), callback);
  }
  public override void getTransactionHashesByPaymentId(Crypto.Hash paymentId, List<Crypto.Hash> transactionHashes, INode.Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetTransactionHashesByPaymentId, this, std::cref(paymentId), std::@ref(transactionHashes)), callback);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockHeaderInfo getLastLocalBlockHeaderInfo() const override
  public override BlockHeaderInfo getLastLocalBlockHeaderInfo()
  {
	lock (m_mutex)
	{
		return lastLocalBlockHeaderInfo;
	}
  }

  public override void relayTransaction(CryptoNote.Transaction transaction, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doRelayTransaction, this, transaction), callback);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getRandomOutsByAmounts(List<ulong>&& amounts, ushort outsCount, List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> outs, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetRandomOutsByAmounts, this, std::move(amounts), outsCount, std::@ref(outs)), callback);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getNewBlocks(List<Crypto.Hash>&& knownBlockIds, List<CryptoNote.RawBlock> newBlocks, uint startHeight, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetNewBlocks, this, std::move(knownBlockIds), std::@ref(newBlocks), std::@ref(startHeight)), callback);
  }
  public override void getTransactionOutsGlobalIndices(Crypto.Hash transactionHash, List<uint> outsGlobalIndices, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetTransactionOutsGlobalIndices, this, transactionHash, std::@ref(outsGlobalIndices)), callback);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void queryBlocks(List<Crypto.Hash>&& knownBlockIds, ulong timestamp, List<BlockShortEntry> newBlocks, uint startHeight, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doQueryBlocksLite, this, std::move(knownBlockIds), timestamp, std::@ref(newBlocks), std::@ref(startHeight)), callback);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getPoolSymmetricDifference(List<Crypto.Hash>&& knownPoolTxIds, Crypto.Hash knownBlockId, ref bool isBcActual, List<std::unique_ptr<ITransactionReader>> newTxs, List<Crypto.Hash> deletedTxIds, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest([this, knownPoolTxIds, knownBlockId, &isBcActual, newTxs, deletedTxIds] mutable.std::error_code
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: return this->doGetPoolSymmetricDifference(std::move(knownPoolTxIds), knownBlockId, isBcActual, newTxs, deletedTxIds);
	  return this.doGetPoolSymmetricDifference(std::move(knownPoolTxIds), new Crypto.Hash(knownBlockId), ref isBcActual, newTxs, ref deletedTxIds);
	}
   , callback);
  }
  public override void getBlocks(List<uint> blockHeights, List<List<BlockDetails>> blocks, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetBlocksByHeight, this, std::cref(blockHeights), std::@ref(blocks)), callback);
  }
  public override void getBlocks(List<Crypto.Hash> blockHashes, List<BlockDetails> blocks, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetBlocksByHash, this, std::cref(blockHashes), std::@ref(blocks)), callback);
  }
  public override void getBlock(uint blockHeight, BlockDetails block, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetBlock, this, blockHeight, std::@ref(block)), callback);
  }
  public override void getTransactions(List<Crypto.Hash> transactionHashes, List<TransactionDetails> transactions, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	scheduleRequest(std::bind(this.doGetTransactions, this, std::cref(transactionHashes), std::@ref(transactions)), callback);
  }
  public override void isSynchronized(ref bool syncStatus, Callback callback)
  {
	lock (m_mutex)
	{
		if (m_state != State.STATE_INITIALIZED)
		{
		  callback(GlobalMembers.make_error_code(error.NOT_INITIALIZED));
		  return;
		}
	}

	// TODO NOT IMPLEMENTED
	callback(std::error_code());
  }
  public override string feeAddress()
  {
	return m_fee_address;
  }
  public override uint feeAmount()
  {
	return m_fee_amount;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint rpcTimeout() const
  public uint rpcTimeout()
  {
	  return m_rpcTimeout;
  }
  public void rpcTimeout(uint val)
  {
	  m_rpcTimeout = val;
  }

  private void resetInternalState()
  {
	m_stop = false;
	m_peerCount.store(0, std::memory_order_relaxed);
	m_networkHeight.store(0, std::memory_order_relaxed);
	lastLocalBlockHeaderInfo.index = 0;
	lastLocalBlockHeaderInfo.majorVersion = 0;
	lastLocalBlockHeaderInfo.minorVersion = 0;
	lastLocalBlockHeaderInfo.timestamp = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastLocalBlockHeaderInfo.hash = CryptoNote::NULL_HASH;
	lastLocalBlockHeaderInfo.hash.CopyFrom(CryptoNote.GlobalMembers.NULL_HASH);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastLocalBlockHeaderInfo.prevHash = CryptoNote::NULL_HASH;
	lastLocalBlockHeaderInfo.prevHash.CopyFrom(CryptoNote.GlobalMembers.NULL_HASH);
	lastLocalBlockHeaderInfo.nonce = 0;
	lastLocalBlockHeaderInfo.isAlternative = false;
	lastLocalBlockHeaderInfo.depth = 0;
	lastLocalBlockHeaderInfo.difficulty = 0;
	lastLocalBlockHeaderInfo.reward = 0;
	m_knownTxs.Clear();
  }
  private void workerThread(INode.Callback initialized_callback)
  {
	try
	{
	  Dispatcher dispatcher = new Dispatcher();
	  m_dispatcher = dispatcher;
	  ContextGroup contextGroup = new ContextGroup(dispatcher);
	  m_context_group = contextGroup;
	  HttpClient httpClient = new HttpClient(dispatcher, m_nodeHost, m_nodePort);
	  m_httpClient = httpClient;
	  Event httpEvent = new Event(dispatcher);
	  m_httpEvent = httpEvent;
	  m_httpEvent.set();

	  lock (m_mutex)
	  {
		Debug.Assert(m_state == State.STATE_INITIALIZING);
		m_state = State.STATE_INITIALIZED;
		m_cv_initialized.notify_all();
	  }

	  getFeeInfo();

	  updateNodeStatus();

	  initialized_callback(std::error_code());

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: contextGroup.spawn([this]()
	  contextGroup.spawn(() =>
	  {
		Timer pullTimer = new Timer(m_dispatcher);
		while (!m_stop)
		{
		  updateNodeStatus();
		  if (!m_stop)
		  {
			pullTimer.sleep(std::chrono.milliseconds(m_pullInterval));
		  }
		}
	  });

	  contextGroup.wait();
	  // Make sure all remote spawns are executed
	  m_dispatcher.yield();
	}
	catch (System.Exception)
	{
	}

	m_dispatcher = null;
	m_context_group = null;
	m_httpClient = null;
	m_httpEvent = null;
	m_connected = false;
	m_rpcProxyObserverManager.notify(INodeRpcProxyObserver.connectionStatusUpdated, m_connected);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> getKnownTxsVector() const
  private List<Crypto.Hash> getKnownTxsVector()
  {
	return new List<Crypto.Hash>(m_knownTxs.GetEnumerator(), m_knownTxs.end());
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void pullNodeStatusAndScheduleTheNext();
  private void updateNodeStatus()
  {
	bool updateBlockchain = true;
	while (updateBlockchain)
	{
	  updateBlockchainStatus();
	  updateBlockchain = !updatePoolStatus();
	}
  }
  private void updateBlockchainStatus()
  {
	CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.request req = boost::value_initialized<decltype(req)>();
	CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.response rsp = boost::value_initialized<decltype(rsp)>();

	std::error_code ec = jsonRpcCommand("getlastblockheader", req, rsp);

	if (ec == null)
	{
	  Crypto.Hash blockHash = new Crypto.Hash();
	  Crypto.Hash prevBlockHash = new Crypto.Hash();
	  if (!parse_hash256(rsp.block_header.hash, blockHash) || !parse_hash256(rsp.block_header.prev_hash, prevBlockHash))
	  {
		return;
	  }

	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);
	  uint blockIndex = rsp.block_header.height;
	  if (blockHash != lastLocalBlockHeaderInfo.hash)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastLocalBlockHeaderInfo.index = blockIndex;
		lastLocalBlockHeaderInfo.index.CopyFrom(blockIndex);
		lastLocalBlockHeaderInfo.majorVersion = rsp.block_header.major_version;
		lastLocalBlockHeaderInfo.minorVersion = rsp.block_header.minor_version;
		lastLocalBlockHeaderInfo.timestamp = rsp.block_header.timestamp;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastLocalBlockHeaderInfo.hash = blockHash;
		lastLocalBlockHeaderInfo.hash.CopyFrom(blockHash);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastLocalBlockHeaderInfo.prevHash = prevBlockHash;
		lastLocalBlockHeaderInfo.prevHash.CopyFrom(prevBlockHash);
		lastLocalBlockHeaderInfo.nonce = rsp.block_header.nonce;
		lastLocalBlockHeaderInfo.isAlternative = rsp.block_header.orphan_status;
		lastLocalBlockHeaderInfo.depth = rsp.block_header.depth;
		lastLocalBlockHeaderInfo.difficulty = rsp.block_header.difficulty;
		lastLocalBlockHeaderInfo.reward = rsp.block_header.reward;
		@lock.unlock();
		m_observerManager.notify(INodeObserver.localBlockchainUpdated, blockIndex);
	  }
	}

	CryptoNote.COMMAND_RPC_GET_INFO.request getInfoReq = boost::value_initialized<decltype(getInfoReq)>();
	CryptoNote.COMMAND_RPC_GET_INFO.response getInfoResp = boost::value_initialized<decltype(getInfoResp)>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = jsonCommand("/getinfo", getInfoReq, getInfoResp);
	ec.CopyFrom(jsonCommand("/getinfo", getInfoReq, getInfoResp));
	if (ec == null)
	{
	  //a quirk to let wallets work with previous versions daemons.
	  //Previous daemons didn't have the 'last_known_block_index' parameter in RPC so it may have zero value.
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);
	  var lastKnownBlockIndex = Math.Max(getInfoResp.last_known_block_index, lastLocalBlockHeaderInfo.index);
	  @lock.unlock();
	  if (m_networkHeight.load(std::memory_order_relaxed) != lastKnownBlockIndex)
	  {
		m_networkHeight.store(lastKnownBlockIndex, std::memory_order_relaxed);
		m_observerManager.notify(INodeObserver.lastKnownBlockHeightUpdated, m_networkHeight.load(std::memory_order_relaxed));
	  }

	  m_nodeHeight.store(getInfoResp.height, std::memory_order_relaxed);

	  updatePeerCount(getInfoResp.incoming_connections_count + getInfoResp.outgoing_connections_count);
	}

	if (m_connected != m_httpClient.isConnected())
	{
	  m_connected = m_httpClient.isConnected();
	  m_rpcProxyObserverManager.notify(INodeRpcProxyObserver.connectionStatusUpdated, m_connected);
	}
  }
  private bool updatePoolStatus()
  {
	List<Crypto.Hash> knownTxs = getKnownTxsVector();
	Crypto.Hash tailBlock = lastLocalBlockHeaderInfo.hash;

	bool isBcActual = false;
	List<std::unique_ptr<ITransactionReader>> addedTxs = new List<std::unique_ptr<ITransactionReader>>();
	List<Crypto.Hash> deletedTxsIds = new List<Crypto.Hash>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: std::error_code ec = doGetPoolSymmetricDifference(std::move(knownTxs), tailBlock, isBcActual, addedTxs, deletedTxsIds);
	std::error_code ec = doGetPoolSymmetricDifference(std::move(knownTxs), new Crypto.Hash(tailBlock), ref isBcActual, addedTxs, ref deletedTxsIds);
	if (ec != null)
	{
	  return true;
	}

	if (!isBcActual)
	{
	  return false;
	}

	if (addedTxs.Count > 0 || deletedTxsIds.Count > 0)
	{
	  updatePoolState(addedTxs, deletedTxsIds);
	  m_observerManager.notify(INodeObserver.poolChanged);
	}

	return true;
  }
  private void updatePeerCount(uint peerCount)
  {
	if (peerCount != m_peerCount)
	{
	  m_peerCount = peerCount;
	  m_observerManager.notify(INodeObserver.peerCountUpdated, m_peerCount.load(std::memory_order_relaxed));
	}
  }
  private void updatePoolState(List<std::unique_ptr<ITransactionReader>> addedTxs, List<Crypto.Hash> deletedTxsIds)
  {
	foreach (var hash in deletedTxsIds)
	{
	  m_knownTxs.erase(hash);
	}

	foreach (var tx in addedTxs)
	{
	  Hash hash = tx.getTransactionHash();
	  m_knownTxs.emplace(std::move(hash));
	}
  }

  private std::error_code doGetBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount, ref List<Crypto.Hash> blockHashes)
  {
	COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS.response rsp = boost::value_initialized<decltype(rsp)>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.timestampBegin = timestampBegin;
	req.timestampBegin.CopyFrom(timestampBegin);
	req.secondsCount = secondsCount;

	std::error_code ec = jsonCommand("/get_blocks_hashes_by_timestamps", req, rsp);
	if (ec == null)
	{
	  blockHashes = std::move(rsp.blockHashes);
	}

	return ec;
  }
  private std::error_code doRelayTransaction(CryptoNote.Transaction transaction)
  {
	COMMAND_RPC_SEND_RAW_TX.request req = new COMMAND_RPC_SEND_RAW_TX.request();
	COMMAND_RPC_SEND_RAW_TX.response rsp = new COMMAND_RPC_SEND_RAW_TX.response();
	req.tx_as_hex = toHex(CryptoNote.GlobalMembers.toBinaryArray(transaction));
	m_logger.functorMethod(TRACE) << "NodeRpcProxy::doRelayTransaction, tx hex " << req.tx_as_hex;
	return jsonCommand("/sendrawtransaction", req, rsp);
  }
  private std::error_code doGetRandomOutsByAmounts(List<ulong> amounts, ushort outsCount, ref List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> outs)
  {
	COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.response rsp = boost::value_initialized<decltype(rsp)>();
	req.amounts = std::move(amounts);
	req.outs_count = outsCount;

	m_logger.functorMethod(TRACE) << "Send getrandom_outs request";
	std::error_code ec = jsonCommand("/getrandom_outs", req, rsp);
	if (ec == null)
	{
	  m_logger.functorMethod(TRACE) << "getrandom_outs complete";
	  outs = std::move(rsp.outs);
	}
	else
	{
	  m_logger.functorMethod(TRACE) << "getrandom_outs failed: " << ec << ", " << ec.message();
	}

	return ec;
  }
  private std::error_code doGetNewBlocks(List<Crypto.Hash> knownBlockIds, ref List<CryptoNote.RawBlock> newBlocks, ref uint startHeight)
  {
	CryptoNote.COMMAND_RPC_GET_BLOCKS_FAST.request req = boost::value_initialized<decltype(req)>();
	CryptoNote.COMMAND_RPC_GET_BLOCKS_FAST.response rsp = boost::value_initialized<decltype(rsp)>();
	req.block_ids = std::move(knownBlockIds);

	m_logger.functorMethod(TRACE) << "Send getblocks request";
	std::error_code ec = jsonCommand("/getblocks", req, rsp);
	if (ec == null)
	{
	  m_logger.functorMethod(TRACE) << "getblocks complete, start_height " << rsp.start_height << ", block count " << rsp.blocks.Count;
	  newBlocks = std::move(rsp.blocks);
	  startHeight = (uint)rsp.start_height;
	}
	else
	{
	  m_logger.functorMethod(TRACE) << "getblocks failed: " << ec << ", " << ec.message();
	}

	return ec;
  }
  private std::error_code doGetTransactionOutsGlobalIndices(Crypto.Hash transactionHash, List<uint> outsGlobalIndices)
  {
	CryptoNote.COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES.request req = boost::value_initialized<decltype(req)>();
	CryptoNote.COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES.response rsp = boost::value_initialized<decltype(rsp)>();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.txid = transactionHash;
	req.txid.CopyFrom(transactionHash);

	m_logger.functorMethod(TRACE) << "Send get_o_indexes request, transaction " << req.txid;
	std::error_code ec = jsonCommand("/get_o_indexes", req, rsp);
	if (ec == null)
	{
	  m_logger.functorMethod(TRACE) << "get_o_indexes complete";
	  outsGlobalIndices.Clear();
	  foreach (var idx in rsp.o_indexes)
	  {
		outsGlobalIndices.Add((uint)idx);
	  }
	}
	else
	{
	  m_logger.functorMethod(TRACE) << "get_o_indexes failed: " << ec << ", " << ec.message();
	}

	return ec;
  }
  private std::error_code doQueryBlocksLite(List<Crypto.Hash> knownBlockIds, ulong timestamp, List<CryptoNote.BlockShortEntry> newBlocks, ref uint startHeight)
  {
	CryptoNote.COMMAND_RPC_QUERY_BLOCKS_LITE.request req = boost::value_initialized<decltype(req)>();
	CryptoNote.COMMAND_RPC_QUERY_BLOCKS_LITE.response rsp = boost::value_initialized<decltype(rsp)>();

	req.blockIds = new List<Crypto.Hash>(knownBlockIds);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.timestamp = timestamp;
	req.timestamp.CopyFrom(timestamp);

	m_logger.functorMethod(TRACE) << "Send queryblockslite request, timestamp " << req.timestamp;
	std::error_code ec = jsonCommand("/queryblockslite", req, rsp);
	if (ec != null)
	{
	  m_logger.functorMethod(TRACE) << "queryblockslite failed: " << ec << ", " << ec.message();
	  return ec;
	}

	m_logger.functorMethod(TRACE) << "queryblockslite complete, startHeight " << rsp.startHeight << ", block count " << rsp.items.Count;
	startHeight = (uint)rsp.startHeight;

	foreach (var item in rsp.items)
	{
	  BlockShortEntry bse = new BlockShortEntry();
	  bse.hasBlock = false;

	  bse.blockHash = std::move(item.blockId);
	  if (!item.block.empty())
	  {
		if (!CryptoNote.GlobalMembers.fromBinaryArray(ref bse.block, item.block))
		{
		  return std::make_error_code(std::errc.invalid_argument);
		}

		bse.hasBlock = true;
	  }

	  foreach (var txp in item.txPrefixes)
	  {
		TransactionShortInfo tsi = new TransactionShortInfo();
		tsi.txId = txp.txHash;
		tsi.txPrefix = txp.txPrefix;
		bse.txsShortInfo.Add(std::move(tsi));
	  }

	  newBlocks.Add(std::move(bse));
	}

	return std::error_code();
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private std::error_code doGetPoolSymmetricDifference(List<Crypto.Hash>&& knownPoolTxIds, Crypto.Hash knownBlockId, ref bool isBcActual, List<std::unique_ptr<ITransactionReader>> newTxs, ref List<Crypto.Hash> deletedTxIds)
  {
	CryptoNote.COMMAND_RPC_GET_POOL_CHANGES_LITE.request req = boost::value_initialized<decltype(req)>();
	CryptoNote.COMMAND_RPC_GET_POOL_CHANGES_LITE.response rsp = boost::value_initialized<decltype(rsp)>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.tailBlockId = knownBlockId;
	req.tailBlockId.CopyFrom(knownBlockId);
	req.knownTxsIds = knownPoolTxIds;

	m_logger.functorMethod(TRACE) << "Send get_pool_changes_lite request, tailBlockId " << req.tailBlockId;
	std::error_code ec = jsonCommand("/get_pool_changes_lite", req, rsp);

	if (ec != null)
	{
	  m_logger.functorMethod(TRACE) << "get_pool_changes_lite failed: " << ec << ", " << ec.message();
	  return ec;
	}

	m_logger.functorMethod(TRACE) << "get_pool_changes_lite complete, isTailBlockActual " << rsp.isTailBlockActual;
	isBcActual = rsp.isTailBlockActual;

	deletedTxIds = std::move(rsp.deletedTxsIds);

	foreach (var tpi in rsp.addedTxs)
	{
	  newTxs.Add(createTransactionPrefix(tpi.txPrefix, tpi.txHash));
	}

	return ec;
  }
  private std::error_code doGetBlocksByHeight(List<uint> blockHeights, List<List<BlockDetails>> blocks)
  {
	COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS.response resp = boost::value_initialized<decltype(resp)>();

	req.blockHeights = new List<uint>(blockHeights);

	std::error_code ec = jsonCommand("/get_blocks_details_by_heights", req, resp);
	if (ec != null)
	{
	  return ec;
	}

	var tmp = std::move(resp.blocks);
	blocks.Add(tmp);

	return ec;
  }
  private std::error_code doGetBlocksByHash(List<Crypto.Hash> blockHashes, ref List<BlockDetails> blocks)
  {
	COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES.response resp = boost::value_initialized<decltype(resp)>();

	req.blockHashes = new List<Crypto.Hash>(blockHashes);

	std::error_code ec = jsonCommand("/get_blocks_details_by_hashes", req, resp);
	if (ec != null)
	{
	  return ec;
	}

	blocks = std::move(resp.blocks);
	return ec;
  }
  private std::error_code doGetBlock(uint blockHeight, ref BlockDetails block)
  {
	COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT.response resp = boost::value_initialized<decltype(resp)>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.blockHeight = blockHeight;
	req.blockHeight.CopyFrom(blockHeight);

	std::error_code ec = jsonCommand("/get_block_details_by_height", req, resp);

	if (ec != null)
	{
	  return ec;
	}

	block = std::move(resp.block);

	return ec;
  }
  private std::error_code doGetTransactionHashesByPaymentId(Crypto.Hash paymentId, ref List<Crypto.Hash> transactionHashes)
  {
	COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID.response resp = boost::value_initialized<decltype(resp)>();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.paymentId = paymentId;
	req.paymentId.CopyFrom(paymentId);
	std::error_code ec = jsonCommand("/get_transaction_hashes_by_payment_id", req, resp);
	if (ec != null)
	{
	  return ec;
	}

	transactionHashes = std::move(resp.transactionHashes);
	return ec;
  }
  private std::error_code doGetTransactions(List<Crypto.Hash> transactionHashes, ref List<TransactionDetails> transactions)
  {
	COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES.request req = boost::value_initialized<decltype(req)>();
	COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES.response resp = boost::value_initialized<decltype(resp)>();

	req.transactionHashes = new List<Crypto.Hash>(transactionHashes);
	std::error_code ec = jsonCommand("/get_transaction_details_by_hashes", req, resp);
	if (ec != null)
	{
	  return ec;
	}

	transactions = std::move(resp.transactions);
	return ec;
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private void scheduleRequest(Func<std::error_code>&& procedure, Callback callback)
  {
	// callback is located on stack, so copy it inside binder
//C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
//	class Wrapper
//	{
//	public:
//	  Wrapper(System.Action<System.Func<std::error_code>&, Callback&>&& _func, System.Func<std::error_code>&& _procedure, const Callback& _callback) : func(std::move(_func)), procedure(std::move(_procedure)), callback(std::move(_callback))
//	  {
//	  }
//	  Wrapper(const Wrapper& other) : func(other.func), procedure(other.procedure), callback(other.callback)
//	  {
//	  }
//	  Wrapper(Wrapper&& other) : func(std::move(other.func)), procedure(std::move(other.procedure)), callback(std::move(other.callback))
//	  {
//	  }
//	  void operator ()()
//	  {
//		func(procedure, callback);
//	  }
//	private:
//	  System.Action<System.Func<std::error_code>&, Callback&> func;
//	  System.Func<std::error_code> procedure;
//	  Callback callback;
//	};
	Debug.Assert(m_dispatcher != null && m_context_group != null);
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher->remoteSpawn(Wrapper([this](System.Func<std::error_code>& procedure, Callback& callback)
	m_dispatcher.remoteSpawn(Wrapper((Func<std::error_code> procedure, Callback callback) =>
	{
	  m_context_group.spawn(Wrapper((Func<std::error_code> procedure, Callback callback) =>
	  {
		  if (m_stop)
		  {
			callback(std::make_error_code(std::errc.operation_canceled));
		  }
		  else
		  {
			std::error_code ec = procedure();
			if (m_connected != m_httpClient.isConnected())
			{
			  m_connected = m_httpClient.isConnected();
			  m_rpcProxyObserverManager.notify(INodeRpcProxyObserver.connectionStatusUpdated, m_connected);
			}
			callback(m_stop ? std::make_error_code(std::errc.operation_canceled) : ec);
		  }
	  }, std::move(procedure), std::move(callback)));
	}, std::move(procedure), callback));
  }
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>
  private std::error_code binaryCommand<Request, Response>(string url, Request req, Response res)
  {
	std::error_code ec = new std::error_code();

	try
	{
	  EventLock eventLock = new EventLock(m_httpEvent);
	  CryptoNote.GlobalMembers.invokeBinaryCommand(m_httpClient, url, req, res);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = interpretResponseStatus(res.status);
	  ec.CopyFrom(GlobalMembers.interpretResponseStatus(res.status));
	}
	catch (ConnectException)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::CONNECT_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.CONNECT_ERROR));
	}
	catch (System.Exception)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::NETWORK_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.NETWORK_ERROR));
	}

	return ec;
  }
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>
  private std::error_code jsonCommand<Request, Response>(string url, Request req, Response res)
  {
	std::error_code ec = new std::error_code();

	try
	{
	  m_logger.functorMethod(TRACE) << "Send " << url << " JSON request";
	  EventLock eventLock = new EventLock(m_httpEvent);
	  CryptoNote.GlobalMembers.invokeJsonCommand(m_httpClient, url, req, res);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = interpretResponseStatus(res.status);
	  ec.CopyFrom(GlobalMembers.interpretResponseStatus(res.status));
	}
	catch (ConnectException)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::CONNECT_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.CONNECT_ERROR));
	}
	catch (System.Exception)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::NETWORK_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.NETWORK_ERROR));
	}

	if (ec != null)
	{
	  m_logger.functorMethod(TRACE) << url << " JSON request failed: " << ec << ", " << ec.message();
	}
	else
	{
	  m_logger.functorMethod(TRACE) << url << " JSON request compete";
	}

	return ec;
  }
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>
  private std::error_code jsonRpcCommand<Request, Response>(string method, Request req, Response res)
  {
	std::error_code ec = GlobalMembers.make_error_code(error.INTERNAL_NODE_ERROR);

	try
	{
	  m_logger.functorMethod(TRACE) << "Send " << method << " JSON RPC request";
	  EventLock eventLock = new EventLock(m_httpEvent);

	  JsonRpc.JsonRpcRequest jsReq = new JsonRpc.JsonRpcRequest();

	  jsReq.setMethod(method);
	  jsReq.setParams(req);

	  HttpRequest httpReq = new HttpRequest();
	  HttpResponse httpRes = new HttpResponse();

	  httpReq.addHeader("Content-Type", "application/json");
	  httpReq.setUrl("/json_rpc");
	  httpReq.setBody(jsReq.getBody());

	  m_httpClient.request(httpReq, httpRes);

	  JsonRpc.JsonRpcResponse jsRes = new JsonRpc.JsonRpcResponse();

	  if (httpRes.getStatus() == HttpResponse.STATUS_200)
	  {
		jsRes.parse(httpRes.getBody());
		if (jsRes.getResult(res))
		{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = interpretResponseStatus(res.status);
		  ec.CopyFrom(GlobalMembers.interpretResponseStatus(res.status));
		}
	  }
	}
	catch (ConnectException)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::CONNECT_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.CONNECT_ERROR));
	}
	catch (System.Exception)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ec = make_error_code(error::NETWORK_ERROR);
	  ec.CopyFrom(GlobalMembers.make_error_code(error.NETWORK_ERROR));
	}

	if (ec != null)
	{
	  m_logger.functorMethod(TRACE) << method << " JSON RPC request failed: " << ec << ", " << ec.message();
	}
	else
	{
	  m_logger.functorMethod(TRACE) << method << " JSON RPC request compete";
	}

	return ec;
  }

  private enum State
  {
	STATE_NOT_INITIALIZED,
	STATE_INITIALIZING,
	STATE_INITIALIZED
  }

  private Logging.LoggerRef m_logger = new Logging.LoggerRef();
  private State m_state = State.STATE_NOT_INITIALIZED;
  private object m_mutex = new object();
  private std::condition_variable m_cv_initialized = new std::condition_variable();
  private std::thread m_workerThread = new std::thread();
  private System.Dispatcher m_dispatcher = null;
  private System.ContextGroup m_context_group = null;
  private Tools.ObserverManager<CryptoNote.INodeObserver> m_observerManager = new Tools.ObserverManager<CryptoNote.INodeObserver>();
  private Tools.ObserverManager<CryptoNote.INodeRpcProxyObserver> m_rpcProxyObserverManager = new Tools.ObserverManager<CryptoNote.INodeRpcProxyObserver>();

  private readonly string m_nodeHost;
  private readonly ushort m_nodePort;
  private uint m_rpcTimeout;
  private HttpClient m_httpClient = null;
  private System.Event m_httpEvent = null;

  private ulong m_pullInterval = new ulong();

  // Internal state
  private bool m_stop = false;
  private std::atomic<uint> m_peerCount = new std::atomic<uint>();
  private std::atomic<uint> m_networkHeight = new std::atomic<uint>();
  private std::atomic<ulong> m_nodeHeight = new std::atomic<ulong>();

  private BlockHeaderInfo lastLocalBlockHeaderInfo = new BlockHeaderInfo();
  //protect it with mutex if decided to add worker threads
  private HashSet<Crypto.Hash> m_knownTxs = new HashSet<Crypto.Hash>();

  private bool m_connected;
  private string m_fee_address;
  private uint m_fee_amount = new uint();
}
}





#if ! AUTO_VAL_INIT
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define AUTO_VAL_INIT(n) boost::value_initialized<decltype(n)>()
#define AUTO_VAL_INIT
#endif

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

}
