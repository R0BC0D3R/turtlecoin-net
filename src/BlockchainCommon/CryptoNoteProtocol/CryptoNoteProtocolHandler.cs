// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl



using Logging;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

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



namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class Dispatcher;
}

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class Currency;

  public class CryptoNoteProtocolHandler : ICryptoNoteProtocolHandler
  {

	public CryptoNoteProtocolHandler(Currency currency, System.Dispatcher dispatcher, ICore rcore, IP2pEndpoint p_net_layout, Logging.ILogger log)
	{
		this.m_dispatcher = new System.Dispatcher(dispatcher);
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
		this.m_currency = new CryptoNote.Currency(currency);
		this.m_core = new CryptoNote.ICore(rcore);
		this.m_p2p = p_net_layout;
		this.m_synchronized = false;
		this.m_stop = false;
		this.m_observedHeight = 0;
		this.m_blockchainHeight = 0;
		this.m_peersCount = 0;
		this.logger = new Logging.LoggerRef(log, "protocol");

	  if (m_p2p == null)
	  {
		m_p2p = m_p2p_stub;
	  }
	}

	public override bool addObserver(ICryptoNoteProtocolObserver observer)
	{
	  return m_observerManager.add(observer);
	}
	public override bool removeObserver(ICryptoNoteProtocolObserver observer)
	{
	  return m_observerManager.remove(observer);
	}

	public void set_p2p_endpoint(IP2pEndpoint p2p)
	{
	  if (p2p != null)
	  {
		m_p2p = p2p;
	  }
	  else
	  {
		m_p2p = m_p2p_stub;
	  }
	}
	// ICore& get_core() { return m_core; }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isSynchronized() const override
	public override bool isSynchronized()
	{
		return m_synchronized;
	}
	public void log_connections()
	{
	  std::stringstream ss = new std::stringstream();

	  ss << std::setw(25) << std::left << "Remote Host" << std::setw(20) << "Peer ID" << std::setw(25) << "Recv/Sent (inactive,sec)" << std::setw(25) << "State" << std::setw(20) << "Lifetime(seconds)" << std::endl;

	  m_p2p.for_each_connection((CryptoNoteConnectionContext cntxt, ulong peer_id) =>
	  {
		ss << std::setw(25) << std::left << (string)(cntxt.m_is_income ? "[INCOMING]" : "[OUTGOING]") + Common.ipAddressToString(cntxt.m_remote_ip) + ":" + Convert.ToString(cntxt.m_remote_port) << std::setw(20) << std::hex << peer_id << std::setw(25) << get_protocol_state_string(cntxt.m_state) << std::setw(20) << Convert.ToString(time(null) - cntxt.m_started) << std::endl;
	  });
	  logger.functorMethod(INFO) << "Connections: " << std::endl << ss.str();
	}

	// Interface t_payload_net_handler, where t_payload_net_handler is template argument of nodetool::node_server
	public void stop()
	{
	  m_stop = true;
	}
	public bool start_sync(CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "Starting synchronization";

	  if (context.m_state == CryptoNoteConnectionContext.state_synchronizing)
	  {
		Debug.Assert(context.m_needed_objects.Count == 0);
		Debug.Assert(context.m_requested_objects.Count == 0);

		NOTIFY_REQUEST_CHAIN.request r = boost::value_initialized<NOTIFY_REQUEST_CHAIN.request>();
		r.block_ids = new List<Crypto.Hash>(m_core.BuildSparseChain());
		logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_REQUEST_CHAIN: m_block_ids.size()=" << r.block_ids.Count;
		GlobalMembers.post_notify<NOTIFY_REQUEST_CHAIN>(m_p2p, r, context);
	  }

	  return true;
	}
	public void onConnectionOpened(CryptoNoteConnectionContext context)
	{
	}
	public void onConnectionClosed(CryptoNoteConnectionContext context)
	{
	  bool updated = false;
	  lock (m_observedHeightMutex)
	  {
		ulong prevHeight = m_observedHeight;
		recalculateMaxObservedHeight(context);
		if (prevHeight != m_observedHeight)
		{
		  updated = true;
		}
	  }

	  if (updated)
	  {
		logger.functorMethod(TRACE) << "Observed height updated: " << m_observedHeight;
		m_observerManager.notify(ICryptoNoteProtocolObserver.lastKnownBlockHeightUpdated, m_observedHeight);
	  }

	  if (context.m_state != CryptoNoteConnectionContext.state_befor_handshake)
	  {
		m_peersCount--;
		m_observerManager.notify(ICryptoNoteProtocolObserver.peerCountUpdated, m_peersCount.load());
	  }
	}
	public CoreStatistics getStatistics()
	{
	  return m_core.GetCoreStatistics();
	}
	public bool get_payload_sync_data(CORE_SYNC_DATA hshd)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: hshd.top_id = m_core.getTopBlockHash();
	  hshd.top_id.CopyFrom(m_core.GetTopBlockHash());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: hshd.current_height = m_core.getTopBlockIndex() + 1;
	  hshd.current_height.CopyFrom(m_core.GetTopBlockIndex() + 1);
	  return true;
	}
	public bool process_payload_sync_data(CORE_SYNC_DATA hshd, CryptoNoteConnectionContext context, bool is_initial)
	{
	  if (context.m_state == CryptoNoteConnectionContext.state_befor_handshake && !is_initial)
	  {
		return true;
	  }

	  if (context.m_state == CryptoNoteConnectionContext.state_synchronizing)
	  {
	  }
	  else if (m_core.HasBlock(hshd.top_id))
	  {
		if (is_initial)
		{
		  on_connection_synchronized();
		  context.m_state = CryptoNoteConnectionContext.state_pool_sync_required;
		}
		else
		{
		  context.m_state = CryptoNoteConnectionContext.state_normal;
		}
	  }
	  else
	  {
		ulong currentHeight = get_current_blockchain_height();

		ulong remoteHeight = hshd.current_height;

		/* Find the difference between the remote and the local height */
		long diff = (long)remoteHeight - (long)currentHeight;

		/* Find out how many days behind/ahead we are from the remote height */
		ulong days = Math.Abs(diff) / (24 * 60 * 60 / m_currency.difficultyTarget());

		std::stringstream ss = new std::stringstream();

		ss << "Your " << CRYPTONOTE_NAME << " node is syncing with the network ";

		/* We're behind the remote node */
		if (diff >= 0)
		{
			ss << "(" << Common.get_sync_percentage(currentHeight, remoteHeight) << "% complete) ";

			ss << "You are " << diff << " blocks (" << days << " days) behind ";
		}
		/* We're ahead of the remote node, no need to print percentages */
		else
		{
			ss << "You are " << Math.Abs(diff) << " blocks (" << days << " days) ahead ";
		}

		ss << "the current peer you're connected to. Slow and steady wins the race! ";

		var logLevel = Logging.Level.TRACE;
		/* Log at different levels depending upon if we're ahead, behind, and if it's
		  a newly formed connection */
		if (diff >= 0)
		{
			if (is_initial)
			{
				logLevel = Logging.Level.INFO;
			}
			else
			{
				logLevel = Logging.Level.DEBUGGING;
			}
		}
		logger.functorMethod(logLevel, Logging.BRIGHT_GREEN) << context << ss.str();

		logger.functorMethod(Logging.Level.DEBUGGING) << "Remote top block height: " << hshd.current_height << ", id: " << hshd.top_id;
		//let the socket to send response to handshake, but request callback, to let send request data after response
		logger.functorMethod(Logging.Level.TRACE) << context << "requesting synchronization";
		context.m_state = CryptoNoteConnectionContext.state_sync_required;
	  }

	  updateObservedHeight(new uint(hshd.current_height), context);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: context.m_remote_blockchain_height = hshd.current_height;
	  context.m_remote_blockchain_height.CopyFrom(hshd.current_height);

	  if (is_initial)
	  {
		m_peersCount++;
		m_observerManager.notify(ICryptoNoteProtocolObserver.peerCountUpdated, m_peersCount.load());
	  }

	  return true;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handleCommand(bool is_notify, int command, BinaryArray in_buff, BinaryArray buff_out, CryptoNoteConnectionContext context, ref bool handled);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPeerCount() const override
	public override uint getPeerCount()
	{
	  return m_peersCount;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getObservedHeight() const override
	public override uint getObservedHeight()
	{
	  lock (m_observedHeightMutex)
	  {
		  return m_observedHeight;
	  }
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockchainHeight() const override
	public override uint getBlockchainHeight()
	{
	  lock (m_blockchainHeightMutex)
	  {
		  return m_blockchainHeight;
	  }
	}
	public void requestMissingPoolTransactions(CryptoNoteConnectionContext context)
	{
	  if (context.version < 1)
	  {
		return;
	  }

	  NOTIFY_REQUEST_TX_POOL.request notification = new NOTIFY_REQUEST_TX_POOL.request();
	  notification.txs = m_core.GetPoolTransactionHashes();

	  bool ok = GlobalMembers.post_notify<NOTIFY_REQUEST_TX_POOL>(m_p2p, notification, context);
	  if (!ok)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Failed to post notification NOTIFY_REQUEST_TX_POOL to " << context.m_connection_id;
	  }
	}

	//----------------- commands handlers ----------------------------------------------
	private int handle_notify_new_block(int command, NOTIFY_NEW_BLOCK.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_NEW_BLOCK (hop " << arg.hop << ")";
	  updateObservedHeight(arg.current_blockchain_height, context);
	  context.m_remote_blockchain_height = arg.current_blockchain_height;
	  if (context.m_state != CryptoNoteConnectionContext.state_normal)
	  {
		return 1;
	  }

	  var result = m_core.AddBlock(new RawBlock({arg.b.block, arg.b.transactions}));
	  if (result == error.AddBlockErrorCondition.BLOCK_ADDED)
	  {
		if (result == error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED)
		{
		  ++arg.hop;
		  //TODO: Add here announce protocol usage
		  GlobalMembers.relay_post_notify<NOTIFY_NEW_BLOCK>(m_p2p, arg, context.m_connection_id);
		  // relay_block(arg, context);
		  requestMissingPoolTransactions(context);
		}
		else if (result == error.AddBlockErrorCode.ADDED_TO_MAIN)
		{
		  ++arg.hop;
		  //TODO: Add here announce protocol usage
		  GlobalMembers.relay_post_notify<NOTIFY_NEW_BLOCK>(m_p2p, arg, context.m_connection_id);
		  // relay_block(arg, context);
		}
		else if (result == error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE)
		{
		  logger.functorMethod(Logging.Level.TRACE) << context << "Block added as alternative";
		}
		else
		{
		  logger.functorMethod(Logging.Level.TRACE) << context << "Block already exists";
		}
	  }
	  else if (result == error.AddBlockErrorCondition.BLOCK_REJECTED)
	  {
		context.m_state = CryptoNoteConnectionContext.state_synchronizing;
		NOTIFY_REQUEST_CHAIN.request r = boost::value_initialized<NOTIFY_REQUEST_CHAIN.request>();
		r.block_ids = new List<Crypto.Hash>(m_core.BuildSparseChain());
		logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_REQUEST_CHAIN: m_block_ids.size()=" << r.block_ids.Count;
		GlobalMembers.post_notify<NOTIFY_REQUEST_CHAIN>(m_p2p, r, context);
	  }
	  else
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "Block verification failed, dropping connection: " << result.message();
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
	  }

	  return 1;
	}
	private int handle_notify_new_transactions(int command, NOTIFY_NEW_TRANSACTIONS.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_NEW_TRANSACTIONS";

	  if (context.m_state != CryptoNoteConnectionContext.state_normal)
	  {
		return 1;
	  }

	  for (var tx_blob_it = arg.txs.begin(); tx_blob_it != arg.txs.end();)
	  {
		if (!m_core.addTransactionToPool(*tx_blob_it))
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << context << "Tx verification failed";
		  tx_blob_it = arg.txs.erase(tx_blob_it);
		}
		else
		{
		  ++tx_blob_it;
		}
	  }

	  if (arg.txs.size())
	  {
		//TODO: add announce usage here
		GlobalMembers.relay_post_notify<NOTIFY_NEW_TRANSACTIONS>(m_p2p, arg, context.m_connection_id);
	  }

	  return true;
	}
	private int handle_request_get_objects(int command, NOTIFY_REQUEST_GET_OBJECTS.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_REQUEST_GET_OBJECTS";
	  NOTIFY_RESPONSE_GET_OBJECTS.request rsp = new NOTIFY_RESPONSE_GET_OBJECTS.request();
	  //if (!m_core.handle_get_objects(arg, rsp)) {
	  //  logger(Logging::ERROR) << context << "failed to handle request NOTIFY_REQUEST_GET_OBJECTS, dropping connection";
	  //  context.m_state = CryptoNoteConnectionContext::state_shutdown;
	  //}

	  rsp.current_blockchain_height = m_core.GetTopBlockIndex() + 1;
	  List<RawBlock> rawBlocks = new List<RawBlock>();
	  m_core.getBlocks(arg.blocks, rawBlocks, rsp.missed_ids);
	  if (!arg.txs.empty())
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << context << "NOTIFY_RESPONSE_GET_OBJECTS: request.txs.empty() != true";
	  }

	  rsp.blocks = GlobalMembers.convertRawBlocksToRawBlocksLegacy(rawBlocks);

	  logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_RESPONSE_GET_OBJECTS: blocks.size()=" << rsp.blocks.size() << ", txs.size()=" << rsp.txs.size() << ", rsp.m_current_blockchain_height=" << rsp.current_blockchain_height << ", missed_ids.size()=" << rsp.missed_ids.size();
	  GlobalMembers.post_notify<NOTIFY_RESPONSE_GET_OBJECTS>(m_p2p, rsp, context);
	  return 1;
	}
	private int handle_response_get_objects(int command, NOTIFY_RESPONSE_GET_OBJECTS.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_RESPONSE_GET_OBJECTS";

	  if (context.m_last_response_height > arg.current_blockchain_height)
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "sent wrong NOTIFY_HAVE_OBJECTS: arg.m_current_blockchain_height=" << arg.current_blockchain_height << " < m_last_response_height=" << context.m_last_response_height << ", dropping connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  updateObservedHeight(arg.current_blockchain_height, context);
	  context.m_remote_blockchain_height = arg.current_blockchain_height;
	  List<BlockTemplate> blockTemplates = new List<BlockTemplate>();
	  List<CachedBlock> cachedBlocks = new List<CachedBlock>();
	  blockTemplates.Resize(arg.blocks.size());
	  cachedBlocks.Capacity = arg.blocks.size();

	  List<RawBlock> rawBlocks = GlobalMembers.convertRawBlocksLegacyToRawBlocks(arg.blocks);

	  for (uint index = 0; index < rawBlocks.Count; ++index)
	  {
		if (!CryptoNote.GlobalMembers.fromBinaryArray(ref blockTemplates[index], rawBlocks[index].block))
		{
		  logger.functorMethod(Logging.Level.ERROR) << context << "sent wrong block: failed to parse and validate block: \r\n" << toHex(rawBlocks[index].block) << "\r\n dropping connection";
		  context.m_state = CryptoNoteConnectionContext.state_shutdown;
		  return 1;
		}

		cachedBlocks.emplace_back(blockTemplates[index]);
		if (index == 1)
		{
		  if (m_core.HasBlock(cachedBlocks[cachedBlocks.Count - 1].getBlockHash()))
		  { //TODO
			context.m_state = CryptoNoteConnectionContext.state_idle;
			context.m_needed_objects.Clear();
			context.m_requested_objects.Clear();
			logger.functorMethod(Logging.Level.DEBUGGING) << context << "Connection set to idle state.";
			return 1;
		  }
		}

		var req_it = context.m_requested_objects.find(cachedBlocks[cachedBlocks.Count - 1].getBlockHash());
		if (req_it == context.m_requested_objects.end())
		{
		  logger.functorMethod(Logging.Level.ERROR) << context << "sent wrong NOTIFY_RESPONSE_GET_OBJECTS: block with id=" << Common.GlobalMembers.podToHex(cachedBlocks[cachedBlocks.Count - 1].getBlockHash()) << " wasn't requested, dropping connection";
		  context.m_state = CryptoNoteConnectionContext.state_shutdown;
		  return 1;
		}

		if (cachedBlocks[cachedBlocks.Count - 1].getBlock().transactionHashes.size() != rawBlocks[index].transactions.Count)
		{
		  logger.functorMethod(Logging.Level.ERROR) << context << "sent wrong NOTIFY_RESPONSE_GET_OBJECTS: block with id=" << Common.GlobalMembers.podToHex(cachedBlocks[cachedBlocks.Count - 1].getBlockHash()) << ", transactionHashes.size()=" << cachedBlocks[cachedBlocks.Count - 1].getBlock().transactionHashes.size() << " mismatch with block_complete_entry.m_txs.size()=" << rawBlocks[index].transactions.Count << ", dropping connection";
		  context.m_state = CryptoNoteConnectionContext.state_shutdown;
		  return 1;
		}

		context.m_requested_objects.erase(req_it);
	  }

	  if (context.m_requested_objects.Count != 0)
	  {
		logger.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << context << "returned not all requested objects (context.m_requested_objects.size()=" << context.m_requested_objects.Count << "), dropping connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  {
		int result = processObjects(context, std::move(rawBlocks), cachedBlocks);
		if (result != 0)
		{
		  return result;
		}
	  }

	  logger.functorMethod(DEBUGGING, BRIGHT_GREEN) << "Local blockchain updated, new index = " << m_core.GetTopBlockIndex();
	  if (m_stop == null && context.m_state == CryptoNoteConnectionContext.state_synchronizing)
	  {
		request_missing_objects(context, true);
	  }

	  return 1;
	}
	private int handle_request_chain(int command, NOTIFY_REQUEST_CHAIN.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_REQUEST_CHAIN: m_block_ids.size()=" << arg.block_ids.Count;

	  if (arg.block_ids.Count == 0)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING, Logging.BRIGHT_RED) << context << "Failed to handle NOTIFY_REQUEST_CHAIN. block_ids is empty";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  if (arg.block_ids[arg.block_ids.Count - 1] != m_core.GetBlockHashByIndex(0))
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "Failed to handle NOTIFY_REQUEST_CHAIN. block_ids doesn't end with genesis block ID";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  NOTIFY_RESPONSE_CHAIN_ENTRY.request r = new NOTIFY_RESPONSE_CHAIN_ENTRY.request();
	  r.m_block_ids = m_core.findBlockchainSupplement(arg.block_ids, BLOCKS_IDS_SYNCHRONIZING_DEFAULT_COUNT, r.total_height, r.start_height);

	  logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_RESPONSE_CHAIN_ENTRY: m_start_height=" << r.start_height << ", m_total_height=" << r.total_height << ", m_block_ids.size()=" << r.m_block_ids.size();
	  GlobalMembers.post_notify<NOTIFY_RESPONSE_CHAIN_ENTRY>(m_p2p, r, context);
	  return 1;
	}
	private int handle_response_chain_entry(int command, NOTIFY_RESPONSE_CHAIN_ENTRY.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_RESPONSE_CHAIN_ENTRY: m_block_ids.size()=" << arg.m_block_ids.size() << ", m_start_height=" << arg.start_height << ", m_total_height=" << arg.total_height;

	  if (!arg.m_block_ids.size())
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "sent empty m_block_ids, dropping connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  if (!m_core.hasBlock(arg.m_block_ids.front()))
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "sent m_block_ids starting from unknown id: " << Common.GlobalMembers.podToHex(arg.m_block_ids.front()) << " , dropping connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  context.m_remote_blockchain_height = arg.total_height;
	  context.m_last_response_height = arg.start_height + (uint)arg.m_block_ids.size() - 1;

	  if (context.m_last_response_height > context.m_remote_blockchain_height)
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "sent wrong NOTIFY_RESPONSE_CHAIN_ENTRY, with \r\nm_total_height=" << arg.total_height << "\r\nm_start_height=" << arg.start_height << "\r\nm_block_ids.size()=" << arg.m_block_ids.size();
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
	  }

	  bool allBlocksKnown = true;
	  foreach (var bl_id in arg.m_block_ids)
	  {
		if (allBlocksKnown)
		{
		  if (!m_core.hasBlock(bl_id))
		  {
			context.m_needed_objects.AddLast(bl_id);
			allBlocksKnown = false;
		  }
		}
		else
		{
		  context.m_needed_objects.AddLast(bl_id);
		}
	  }

	  request_missing_objects(context, false);
	  return 1;
	}
	private int handleRequestTxPool(int command, NOTIFY_REQUEST_TX_POOL.request arg, CryptoNoteConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "NOTIFY_REQUEST_TX_POOL: txs.size() = " << arg.txs.size();
	  NOTIFY_NEW_TRANSACTIONS.request notification = new NOTIFY_NEW_TRANSACTIONS.request();
	  List<Crypto.Hash> deletedTransactions = new List<Crypto.Hash>();
	  m_core.getPoolChanges(m_core.GetTopBlockHash(), arg.txs, notification.txs, deletedTransactions);
	  if (!notification.txs.empty())
	  {
		bool ok = GlobalMembers.post_notify<NOTIFY_NEW_TRANSACTIONS>(m_p2p, notification, context);
		if (!ok)
		{
		  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Failed to post notification NOTIFY_NEW_TRANSACTIONS to " << context.m_connection_id;
		}
	  }

	  return 1;
	}

	//----------------- i_cryptonote_protocol ----------------------------------
	private override void relayBlock(NOTIFY_NEW_BLOCK.request arg)
	{
	  var buf = LevinProtocol.encode(arg);
	  m_p2p.externalRelayNotifyToAll(NOTIFY_NEW_BLOCK.ID, buf, null);
	}
	private override void relayTransactions(List<List<ushort>> transactions)
	{
	  var buf = LevinProtocol.encode(new NOTIFY_NEW_TRANSACTIONS.request({transactions}));
	  m_p2p.externalRelayNotifyToAll(NOTIFY_NEW_TRANSACTIONS.ID, buf, null);
	}

	//----------------------------------------------------------------------------------
	private uint get_current_blockchain_height()
	{
	  return m_core.GetTopBlockIndex() + 1;
	}
	private bool request_missing_objects(CryptoNoteConnectionContext context, bool check_having_blocks)
	{
	  if (context.m_needed_objects.Count != 0)
	  {
		//we know objects that we need, request this objects
		NOTIFY_REQUEST_GET_OBJECTS.request req = new NOTIFY_REQUEST_GET_OBJECTS.request();
		uint count = 0;
		var it = context.m_needed_objects.GetEnumerator();

		while (it != context.m_needed_objects.end() && count < BLOCKS_SYNCHRONIZING_DEFAULT_COUNT)
		{
		  if (!(check_having_blocks && m_core.hasBlock(*it)))
		  {
			req.blocks.push_back(*it);
			++count;
			context.m_requested_objects.Add(*it);
		  }
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
		  it = context.m_needed_objects.erase(it);
		}
		logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_REQUEST_GET_OBJECTS: blocks.size()=" << req.blocks.size() << ", txs.size()=" << req.txs.size();
		GlobalMembers.post_notify<NOTIFY_REQUEST_GET_OBJECTS>(m_p2p, req, context);
	  }
	  else if (context.m_last_response_height < context.m_remote_blockchain_height - 1)
	  { //we have to fetch more objects ids, request blockchain entry

		NOTIFY_REQUEST_CHAIN.request r = boost::value_initialized<NOTIFY_REQUEST_CHAIN.request>();
		r.block_ids = new List<Crypto.Hash>(m_core.BuildSparseChain());
		logger.functorMethod(Logging.Level.TRACE) << context << "-->>NOTIFY_REQUEST_CHAIN: m_block_ids.size()=" << r.block_ids.Count;
		GlobalMembers.post_notify<NOTIFY_REQUEST_CHAIN>(m_p2p, r, context);
	  }
	  else
	  {
		if (!(context.m_last_response_height == context.m_remote_blockchain_height - 1 && !context.m_needed_objects.Count && !context.m_requested_objects.Count))
		{
		  logger.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << "request_missing_blocks final condition failed!" << "\r\nm_last_response_height=" << context.m_last_response_height << "\r\nm_remote_blockchain_height=" << context.m_remote_blockchain_height << "\r\nm_needed_objects.size()=" << context.m_needed_objects.Count << "\r\nm_requested_objects.size()=" << context.m_requested_objects.Count << "\r\non connection [" << context << "]";
		  return false;
		}

		requestMissingPoolTransactions(context);

		context.m_state = CryptoNoteConnectionContext.state_normal;
		logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_GREEN) << context << "Successfully synchronized with the TurtleCoin Network.";
		on_connection_synchronized();
	  }
	  return true;
	}
	private bool on_connection_synchronized()
	{
	  bool val_expected = false;
	  if (m_synchronized.compare_exchange_strong(val_expected, true))
	  {
		logger.functorMethod(Logging.Level.INFO) << std::endl;
		  logger.functorMethod(INFO, BRIGHT_MAGENTA) << "===[ " + (string)CryptoNote.CRYPTONOTE_NAME + " Tip! ]=============================" << std::endl;
		  logger.functorMethod(INFO, WHITE) << " Always exit " + WalletConfig.daemonName + " and " + WalletConfig.walletName + " with the \"exit\" command to preserve your chain and wallet data." << std::endl;
		  logger.functorMethod(INFO, WHITE) << " Use the \"help\" command to see a list of available commands." << std::endl;
		  logger.functorMethod(INFO, WHITE) << " Use the \"backup\" command in " + WalletConfig.walletName + " to display your keys/seed for restoring a corrupted wallet." << std::endl;
		  logger.functorMethod(INFO, WHITE) << " If you need more assistance, you can contact us for support at " + WalletConfig.contactLink << std::endl;
		  logger.functorMethod(INFO, BRIGHT_MAGENTA) << "===================================================" << std::endl << std::endl;

		  logger.functorMethod(INFO, BRIGHT_GREEN) << asciiArt << std::endl;

		m_observerManager.notify(ICryptoNoteProtocolObserver.blockchainSynchronized, m_core.GetTopBlockIndex());
	  }
	  return true;
	}
	private void updateObservedHeight(uint peerHeight, CryptoNoteConnectionContext context)
	{
	  bool updated = false;
	  lock (m_observedHeightMutex)
	  {

		uint height = new uint(m_observedHeight);
		if (context.m_remote_blockchain_height != 0 && context.m_last_response_height <= context.m_remote_blockchain_height - 1)
		{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_observedHeight = context.m_remote_blockchain_height - 1;
		  m_observedHeight.CopyFrom(context.m_remote_blockchain_height - 1);
		  if (m_observedHeight != height)
		  {
			updated = true;
		  }
		}
		else if (peerHeight > context.m_remote_blockchain_height)
		{
		  m_observedHeight = Math.Max(m_observedHeight, peerHeight);
		  if (m_observedHeight != height)
		  {
			updated = true;
		  }
		}
		else if (peerHeight != context.m_remote_blockchain_height && context.m_remote_blockchain_height == m_observedHeight)
		{
		  //the client switched to alternative chain and had maximum observed height. need to recalculate max height
		  recalculateMaxObservedHeight(context);
		  if (m_observedHeight != height)
		  {
			updated = true;
		  }
		}
	  }

	  lock (m_blockchainHeightMutex)
	  {
		if (peerHeight > m_blockchainHeight)
		{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_blockchainHeight = peerHeight;
		  m_blockchainHeight.CopyFrom(peerHeight);
		  logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_GREEN) << "New Top Block Detected: " << peerHeight;
		}
	  }


	  if (updated)
	  {
		logger.functorMethod(TRACE) << "Observed height updated: " << m_observedHeight;
		m_observerManager.notify(ICryptoNoteProtocolObserver.lastKnownBlockHeightUpdated, m_observedHeight);
	  }
	}
	private void recalculateMaxObservedHeight(CryptoNoteConnectionContext context)
	{
	  //should be locked outside
	  uint peerHeight = 0;
	  m_p2p.for_each_connection((CryptoNoteConnectionContext ctx, ulong peerId) =>
	  {
		if (ctx.m_connection_id != context.m_connection_id)
		{
		  peerHeight = Math.Max(peerHeight, ctx.m_remote_blockchain_height);
		}
	  });

	  m_observedHeight = Math.Max(peerHeight, m_core.GetTopBlockIndex() + 1);
		if (context.m_state == CryptoNoteConnectionContext.state_normal)
		{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_observedHeight = m_core.getTopBlockIndex();
		  m_observedHeight.CopyFrom(m_core.GetTopBlockIndex());
		}
	}
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	private int processObjects(CryptoNoteConnectionContext context, List<RawBlock>&& rawBlocks, List<CachedBlock> cachedBlocks)
	{
	  Debug.Assert(rawBlocks.size() == cachedBlocks.Count);
	  for (uint index = 0; index < rawBlocks.size(); ++index)
	  {
		if (m_stop != null)
		{
		  break;
		}

		var addResult = m_core.addBlock(cachedBlocks[index], std::move(rawBlocks[index]));
		if (addResult == error.AddBlockErrorCondition.BLOCK_VALIDATION_FAILED || addResult == error.AddBlockErrorCondition.TRANSACTION_VALIDATION_FAILED || addResult == error.AddBlockErrorCondition.DESERIALIZATION_FAILED)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << context << "Block verification failed, dropping connection: " << addResult.message();
		  context.m_state = CryptoNoteConnectionContext.state_shutdown;
		  return 1;
		}
		else if (addResult == error.AddBlockErrorCondition.BLOCK_REJECTED)
		{
		  logger.functorMethod(Logging.Level.INFO) << context << "Block received at sync phase was marked as orphaned, dropping connection: " << addResult.message();
		  context.m_state = CryptoNoteConnectionContext.state_shutdown;
		  return 1;
		}
		else if (addResult == error.AddBlockErrorCode.ALREADY_EXISTS)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << context << "Block already exists, switching to idle state: " << addResult.message();
		  context.m_state = CryptoNoteConnectionContext.state_idle;
		  context.m_needed_objects.Clear();
		  context.m_requested_objects.Clear();
		  return 1;
		}

		m_dispatcher.yield();
	  }

	  return 0;
	}
	private Logging.LoggerRef logger = new Logging.LoggerRef();


	private System.Dispatcher m_dispatcher;
	private ICore m_core;
	private readonly Currency m_currency;

	private p2p_endpoint_stub m_p2p_stub = new p2p_endpoint_stub();
	private IP2pEndpoint m_p2p;
	private std::atomic<bool> m_synchronized = new std::atomic<bool>();
	private std::atomic<bool> m_stop = new std::atomic<bool>();

	private object m_observedHeightMutex = new object();
	private uint m_observedHeight = new uint();

	private object m_blockchainHeightMutex = new object();
	private uint m_blockchainHeight = new uint();

	private std::atomic<uint> m_peersCount = new std::atomic<uint>();
	private Tools.ObserverManager<ICryptoNoteProtocolObserver> m_observerManager = new Tools.ObserverManager<ICryptoNoteProtocolObserver>();
  }
}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class t_parametr>

// Changed std::bind -> lambda, for better debugging, remove it ASAP
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define HANDLE_NOTIFY(CMD, Handler) case CMD::ID: { ret = notifyAdaptor<CMD>(in, ctx, [this](int a1, CMD::request& a2, CryptoNoteConnectionContext& a3) { return Handler(a1, a2, a3); }); break; }



}
