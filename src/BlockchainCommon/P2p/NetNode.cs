// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH



using Common;
using Logging;
using CryptoNote;
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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl


namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class LevinProtocol;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class ISerializer;

  public class P2pMessage
  {
	public enum Type
	{
	  COMMAND,
	  REPLY,
	  NOTIFY
	}

	public P2pMessage(Type type, uint command, BinaryArray buffer, int returnCode = 0)
	{
		this.type = new CryptoNote.P2pMessage.Type(type);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.command = command;
		this.command.CopyFrom(command);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.buffer = buffer;
		this.buffer.CopyFrom(buffer);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.returnCode = returnCode;
		this.returnCode.CopyFrom(returnCode);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pMessage(P2pMessage && msg)
	{
		this.type = new CryptoNote.P2pMessage.Type(msg.type);
		this.command = msg.command;
		this.buffer = std::move(msg.buffer);
		this.returnCode = msg.returnCode;
	}

	public uint size()
	{
	  return buffer.size();
	}

	public Type type;
	public uint command = new uint();
	public readonly BinaryArray buffer = new BinaryArray();
	public int returnCode = new int();
  }

  public class P2pConnectionContext : CryptoNoteConnectionContext
  {

	public System.Context context;
	public ulong peerId = new ulong();
	public System.TcpConnection connection = new System.TcpConnection();

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pConnectionContext(System.Dispatcher dispatcher, Logging.ILogger log, System.TcpConnection && conn)
	{
		this.context = null;
		this.peerId = 0;
		this.connection = new System.TcpConnection(std::move(conn));
		this.logger = new Logging.LoggerRef(log, "node_server");
		this.queueEvent = new System.Event(dispatcher);
		this.stopped = false;
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pConnectionContext(P2pConnectionContext && ctx) : base(std::move(ctx))
	{
		this.context = ctx.context;
		this.peerId = ctx.peerId;
		this.connection = new System.TcpConnection(std::move(ctx.connection));
		this.logger = new Logging.LoggerRef(ctx.logger.getLogger(), "node_server");
		this.queueEvent = new System.Event(std::move(ctx.queueEvent));
		this.stopped = std::move(ctx.stopped);
	}


	//-----------------------------------------------------------------------------------
	// P2pConnectionContext implementation
	//-----------------------------------------------------------------------------------

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public bool pushMessage(P2pMessage && msg)
	{
	  writeQueueSize += msg.size();

	  if (writeQueueSize > P2P_CONNECTION_MAX_WRITE_BUFFER_SIZE)
	  {
		logger.functorMethod(DEBUGGING) << this << "Write queue overflows. Interrupt connection";
		interrupt();
		return false;
	  }

	  writeQueue.Add(std::move(msg));
	  queueEvent.set();
	  return true;
	}
	public List<P2pMessage> popBuffer()
	{
	  writeOperationStartTime = TimePoint();

	  while (writeQueue.Count == 0 && !stopped)
	  {
		queueEvent.wait();
	  }

	  List<P2pMessage> msgs = new List<P2pMessage>(std::move(writeQueue));
	  writeQueue.Clear();
	  writeQueueSize = 0;
	  writeOperationStartTime = Clock.now();
	  queueEvent.clear();
	  return msgs;
	}
	public void interrupt()
	{
	  logger.functorMethod(DEBUGGING) << this << "Interrupt connection";
	  Debug.Assert(context != null);
	  stopped = true;
	  queueEvent.set();
	  context.interrupt();
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong writeDuration(std::chrono::steady_clock::time_point now) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	ulong writeDuration(std::chrono::steady_clock::time_point now);

	private Logging.LoggerRef logger = new Logging.LoggerRef();
	private std::chrono.steady_clock.time_point writeOperationStartTime = new std::chrono.steady_clock.time_point();
	private System.Event queueEvent = new System.Event();
	private List<P2pMessage> writeQueue = new List<P2pMessage>();
	private uint writeQueueSize = 0;
	private bool stopped;
  }

  public class NodeServer : IP2pEndpoint
  {


	//-----------------------------------------------------------------------------------

	public static void init_options(boost::program_options.options_description desc)
	{
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_bind_ip);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_bind_port);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_external_port);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_allow_local_ip);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_add_peer);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_add_priority_node);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_add_exclusive_node);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_seed_node);
	  command_line.GlobalMembers.add_arg(desc, GlobalMembers.arg_p2p_hide_my_port);
	}

	public NodeServer(System.Dispatcher dispatcher, CryptoNote.CryptoNoteProtocolHandler payload_handler, Logging.ILogger log)
	{
		this.m_dispatcher = new System.Dispatcher(dispatcher);
		this.m_workingContextGroup = dispatcher;
		this.m_payload_handler = new CryptoNote.CryptoNoteProtocolHandler(payload_handler);
		this.m_allow_local_ip = false;
		this.m_hide_my_port = false;
		this.m_network_id = CryptoNote.CRYPTONOTE_NETWORK;
		this.logger = new Logging.LoggerRef(log, "node_server");
		this.m_stopEvent = new System.Event(m_dispatcher);
		this.m_idleTimer = m_dispatcher;
		this.m_timedSyncTimer = m_dispatcher;
		this.m_timeoutTimer = m_dispatcher;
		this.m_stop = false;
		this.m_connections_maker_interval = new CryptoNote.OnceInInterval(1);
		this.m_peerlist_store_interval = new CryptoNote.OnceInInterval(60 * 30, false);
	}

	//-----------------------------------------------------------------------------------

	public bool run()
	{
	  logger.functorMethod(INFO) << "Starting node_server";

	  m_workingContextGroup.spawn(std::bind(this.acceptLoop, this));
	  m_workingContextGroup.spawn(std::bind(this.onIdle, this));
	  m_workingContextGroup.spawn(std::bind(this.timedSyncLoop, this));
	  m_workingContextGroup.spawn(std::bind(this.timeoutLoop, this));

	  m_stopEvent.wait();

	  logger.functorMethod(INFO) << "Stopping NodeServer and its " << m_connections.Count << " connections...";
	  safeInterrupt(m_workingContextGroup);
	  m_workingContextGroup.wait();

	  logger.functorMethod(INFO) << "NodeServer loop stopped";
	  return true;
	}

	//-----------------------------------------------------------------------------------

	public bool init(NetNodeConfig config)
	{
	  if (!config.getTestnet())
	  {
		foreach (var seed in CryptoNote.SEED_NODES)
		{
		  append_net_address(m_seed_nodes, seed);
		}
	  }
	  else
	  {
		m_network_id.data[0] += 1;
	  }

	  if (!handleConfig(config))
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to handle command line";
		return false;
	  }
	  m_config_folder = config.getConfigFolder();
	  m_p2p_state_filename = config.getP2pStateFilename();

	  if (!init_config())
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to init config.";
		return false;
	  }

	  if (!m_peerlist.init(m_allow_local_ip))
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to init peerlist.";
		return false;
	  }

	  foreach (var p in m_command_line_peers)
	  {
		m_peerlist.append_with_peer_white(p);
	  }

	  //only in case if we really sure that we have external visible ip
	  m_have_address = true;
	  m_ip_address = 0;

#if ALLOW_DEBUG_COMMANDS
	  m_last_stat_request_time = 0;
#endif

	  //configure self
	  // m_net_server.get_config_object().m_pcommands_handler = this;
	  // m_net_server.get_config_object().m_invoke_timeout = CryptoNote::P2P_DEFAULT_INVOKE_TIMEOUT;

	  //try to bind
	  logger.functorMethod(INFO) << "Binding on " << m_bind_ip << ":" << m_port;
	  m_listeningPort = Common.GlobalMembers.fromString<ushort>(m_port);

	  m_listener = System.TcpListener(m_dispatcher, System.Ipv4Address(m_bind_ip), (ushort)m_listeningPort);

	  logger.functorMethod(INFO) << "Net service binded on " << m_bind_ip << ":" << m_listeningPort;

	  if (m_external_port != null)
	  {
		logger.functorMethod(INFO) << "External port defined as " << m_external_port;
	  }

	  GlobalMembers.addPortMapping(logger.functorMethod, new uint(m_listeningPort));

	  return true;
	}
	//-----------------------------------------------------------------------------------

	public bool deinit()
	{
	  return store_config();
	}
	//-----------------------------------------------------------------------------------

	public bool sendStopSignal()
	{
	  m_stop = true;

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([this]
	  m_dispatcher.remoteSpawn(() =>
	  {
		m_stopEvent.set();
		m_payload_handler.stop();
	  });

	  logger.functorMethod(INFO, BRIGHT_YELLOW) << "Stop signal sent, please only EXIT or CTRL+C one time to avoid stalling the shutdown process.";
	  return true;
	}
	public uint get_this_peer_port()
	{
		return m_listeningPort;
	}
	//-----------------------------------------------------------------------------------

	public CryptoNote.CryptoNoteProtocolHandler get_payload_object()
	{
	  return m_payload_handler;
	}

	public void serialize(ISerializer s)
	{
	  ushort version = 1;
	  s.functorMethod(version, "version");

	  if (version != 1)
	  {
		throw new System.Exception("Unsupported version");
	  }

	  s.functorMethod(m_peerlist, "peerlist");
	  s.functorMethod(m_config.m_peer_id, "peer_id");
	}

	// debug functions
	//-----------------------------------------------------------------------------------

	public bool log_peerlist()
	{
	  LinkedList<PeerlistEntry> pl_wite = new LinkedList<PeerlistEntry>();
	  LinkedList<PeerlistEntry> pl_gray = new LinkedList<PeerlistEntry>();
	  m_peerlist.get_peerlist_full(pl_gray, pl_wite);
	  logger.functorMethod(INFO) << std::endl << "Peerlist white:" << std::endl << GlobalMembers.print_peerlist_to_string(pl_wite) << std::endl << "Peerlist gray:" << std::endl << GlobalMembers.print_peerlist_to_string(pl_gray);
	  return true;
	}
	//-----------------------------------------------------------------------------------

	public bool log_connections()
	{
	  logger.functorMethod(INFO) << "Connections: \r\n" << print_connections_container();
	  return true;
	}

	//-----------------------------------------------------------------------------------

	public ulong get_connections_count()
	{
	  return m_connections.Count;
	}

	//-----------------------------------------------------------------------------------
	public uint get_outgoing_connections_count()
	{
	  uint count = 0;
	  foreach (var cntxt in m_connections)
	  {
		if (!cntxt.second.m_is_income)
		{
		  ++count;
		}
	  }
	  return count;
	}

	public PeerlistManager getPeerlistManager()
	{
		return m_peerlist;
	}


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handleCommand(LevinProtocol::Command cmd, BinaryArray buff_out, P2pConnectionContext context, ref bool handled);

	//----------------- commands handlers ----------------------------------------------
	//-----------------------------------------------------------------------------------

	private int handle_handshake(int command, COMMAND_HANDSHAKE.request arg, COMMAND_HANDSHAKE.response rsp, P2pConnectionContext context)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: context.version = arg.node_data.version;
	  context.version.CopyFrom(arg.node_data.version);

	  if (arg.node_data.network_id != m_network_id)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "WRONG NETWORK AGENT CONNECTED! id=" << arg.node_data.network_id;
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  if (arg.node_data.version < CryptoNote.P2P_MINIMUM_VERSION)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "UNSUPPORTED NETWORK AGENT VERSION CONNECTED! version=" << Convert.ToString(arg.node_data.version);
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }
	  else if (arg.node_data.version > CryptoNote.P2P_CURRENT_VERSION)
	  {
		logger.functorMethod(Logging.Level.WARNING) << context << "Our software may be out of date. Please visit: " << CryptoNote.LATEST_VERSION_URL << " for the latest version.";
	  }

	  if (!context.m_is_income)
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "COMMAND_HANDSHAKE came not from incoming connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  if (context.peerId != null)
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "COMMAND_HANDSHAKE came, but seems that connection already have associated peer_id (double COMMAND_HANDSHAKE?)";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  if (!m_payload_handler.process_payload_sync_data(arg.payload_data, context, true))
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "COMMAND_HANDSHAKE came, but process_payload_sync_data returned false, dropping connection.";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }
	  //associate peer_id with this connection
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: context.peerId = arg.node_data.peer_id;
	  context.peerId.CopyFrom(arg.node_data.peer_id);

	  if (arg.node_data.peer_id != m_config.m_peer_id && arg.node_data.my_port != null)
	  {
		ulong peer_id_l = new ulong(arg.node_data.peer_id);
		uint port_l = new uint(arg.node_data.my_port);

		if (try_ping(arg.node_data, context))
		{
			//called only(!) if success pinged, update local peerlist
			PeerlistEntry pe = new PeerlistEntry();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pe.adr.ip = context.m_remote_ip;
			pe.adr.ip.CopyFrom(context.m_remote_ip);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pe.adr.port = port_l;
			pe.adr.port.CopyFrom(port_l);
			pe.last_seen = time(null);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pe.id = peer_id_l;
			pe.id.CopyFrom(peer_id_l);
			m_peerlist.append_with_peer_white(pe);

			logger.functorMethod(Logging.Level.TRACE) << context << "BACK PING SUCCESS, " << Common.ipAddressToString(context.m_remote_ip) << ":" << port_l << " added to whitelist";
		}
	  }

	  //fill response
	  m_peerlist.get_peerlist_head(rsp.local_peerlist);
	  get_local_node_data(rsp.node_data);
	  m_payload_handler.get_payload_sync_data(rsp.payload_data);

	  logger.functorMethod(Logging.Level.DEBUGGING, Logging.BRIGHT_GREEN) << "COMMAND_HANDSHAKE";
	  return 1;
	}

	//-----------------------------------------------------------------------------------
	private int handle_timed_sync(int command, COMMAND_TIMED_SYNC.request arg, COMMAND_TIMED_SYNC.response rsp, P2pConnectionContext context)
	{
	  if (!m_payload_handler.process_payload_sync_data(arg.payload_data, context, false))
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "Failed to process_payload_sync_data(), dropping connection";
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  //fill response
	  rsp.local_time = time(null);
	  m_peerlist.get_peerlist_head(rsp.local_peerlist);
	  m_payload_handler.get_payload_sync_data(rsp.payload_data);
	  logger.functorMethod(Logging.Level.TRACE) << context << "COMMAND_TIMED_SYNC";
	  return 1;
	}
	//-----------------------------------------------------------------------------------

	private int handle_ping(int command, COMMAND_PING.request arg, COMMAND_PING.response rsp, P2pConnectionContext context)
	{
	  logger.functorMethod(Logging.Level.TRACE) << context << "COMMAND_PING";
	  rsp.status = PING_OK_RESPONSE_STATUS_TEXT;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.peer_id = m_config.m_peer_id;
	  rsp.peer_id.CopyFrom(m_config.m_peer_id);
	  return 1;
	}
	//-----------------------------------------------------------------------------------

#if ALLOW_DEBUG_COMMANDS
	private int handle_get_stat_info(int command, COMMAND_REQUEST_STAT_INFO.request arg, COMMAND_REQUEST_STAT_INFO.response rsp, P2pConnectionContext context)
	{
	  if (!check_trust(arg.tr))
	  {
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.connections_count = get_connections_count();
	  rsp.connections_count.CopyFrom(get_connections_count());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.incoming_connections_count = rsp.connections_count - get_outgoing_connections_count();
	  rsp.incoming_connections_count.CopyFrom(rsp.connections_count - get_outgoing_connections_count());
	  rsp.version = PROJECT_VERSION_LONG;
	  rsp.os_version = Tools.get_os_version_string();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.payload_info = m_payload_handler.getStatistics();
	  rsp.payload_info.CopyFrom(m_payload_handler.getStatistics());
	  return 1;
	}
	//-----------------------------------------------------------------------------------

	private int handle_get_network_state(int command, COMMAND_REQUEST_NETWORK_STATE.request arg, COMMAND_REQUEST_NETWORK_STATE.response rsp, P2pConnectionContext context)
	{
	  if (!check_trust(arg.tr))
	  {
		context.m_state = CryptoNoteConnectionContext.state_shutdown;
		return 1;
	  }

	  foreach (var cntxt in m_connections)
	  {
		connection_entry ce = new connection_entry();
		ce.adr.ip = cntxt.second.m_remote_ip;
		ce.adr.port = cntxt.second.m_remote_port;
		ce.id = cntxt.second.peerId;
		ce.is_income = cntxt.second.m_is_income;
		rsp.connections_list.AddLast(ce);
	  }

	  m_peerlist.get_peerlist_full(rsp.local_peerlist_gray, rsp.local_peerlist_white);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.my_id = m_config.m_peer_id;
	  rsp.my_id.CopyFrom(m_config.m_peer_id);
	  rsp.local_time = time(null);
	  return 1;
	}
	//-----------------------------------------------------------------------------------

	private int handle_get_peer_id(int command, COMMAND_REQUEST_PEER_ID.request arg, COMMAND_REQUEST_PEER_ID.response rsp, P2pConnectionContext context)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.my_id = m_config.m_peer_id;
	  rsp.my_id.CopyFrom(m_config.m_peer_id);
	  return 1;
	}
#if ALLOW_DEBUG_COMMANDS
	private bool check_trust(proof_of_trust tr)
	{
	  ulong local_time = time(null);
	  ulong time_delata = local_time > tr.time != null ? local_time - tr.time : tr.time - local_time;

	  if (time_delata > 24 * 60 * 60)
	  {
		logger.functorMethod(ERROR) << "check_trust failed to check time conditions, local_time=" << local_time << ", proof_time=" << tr.time;
		return false;
	  }

	  if (m_last_stat_request_time >= tr.time)
	  {
		logger.functorMethod(ERROR) << "check_trust failed to check time conditions, last_stat_request_time=" << m_last_stat_request_time << ", proof_time=" << tr.time;
		return false;
	  }

	  if (m_config.m_peer_id != tr.peer_id)
	  {
		logger.functorMethod(ERROR) << "check_trust failed: peer_id mismatch (passed " << tr.peer_id << ", expected " << m_config.m_peer_id << ")";
		return false;
	  }

	  Crypto.PublicKey pk = new Crypto.PublicKey();
	  Common.GlobalMembers.podFromHex(CryptoNote.P2P_STAT_TRUSTED_PUB_KEY, pk);
	  Crypto.Hash h = CryptoNote.GlobalMembers.get_proof_of_trust_hash(tr);
	  if (!Crypto.check_signature(h, pk, tr.sign))
	  {
		logger.functorMethod(ERROR) << "check_trust failed: sign check failed";
		return false;
	  }

	  //update last request time
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_last_stat_request_time = tr.time;
	  m_last_stat_request_time.CopyFrom(tr.time);
	  return true;
	}
#endif
#endif

	//-----------------------------------------------------------------------------------

	private bool init_config()
	{
	  try
	  {
		string state_file_path = m_config_folder + "/" + m_p2p_state_filename;
		bool loaded = false;

		try
		{
		  std::ifstream p2p_data = new std::ifstream();
		  p2p_data.open(state_file_path, std::ios_base.binary | std::ios_base.in);

		  if (!p2p_data.fail())
		  {
			StdInputStream inputStream = new StdInputStream(p2p_data);
			BinaryInputStreamSerializer a = new BinaryInputStreamSerializer(inputStream);
			CryptoNote.GlobalMembers.serialize(this, a.functorMethod);
			loaded = true;
		  }
		}
		catch (System.Exception e)
		{
		  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to load config from file '" << state_file_path << "': " << e.Message;
		}

		if (!loaded)
		{
		  make_default_config();
		}

		//at this moment we have hardcoded config
		m_config.m_net_config.handshake_interval = CryptoNote.P2P_DEFAULT_HANDSHAKE_INTERVAL;
		m_config.m_net_config.connections_count = CryptoNote.P2P_DEFAULT_CONNECTIONS_COUNT;
		m_config.m_net_config.packet_max_size = CryptoNote.P2P_DEFAULT_PACKET_MAX_SIZE; //20 MB limit
		m_config.m_net_config.config_id = 0; // initial config
		m_config.m_net_config.connection_timeout = CryptoNote.P2P_DEFAULT_CONNECTION_TIMEOUT;
		m_config.m_net_config.ping_connection_timeout = CryptoNote.P2P_DEFAULT_PING_CONNECTION_TIMEOUT;
		m_config.m_net_config.send_peerlist_sz = CryptoNote.P2P_DEFAULT_PEERS_IN_HANDSHAKE;

		m_first_connection_maker_call = true;
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "init_config failed: " << e.Message;
		return false;
	  }
	  return true;
	}

	//-----------------------------------------------------------------------------------
	private bool make_default_config()
	{
	  m_config.m_peer_id = Crypto.GlobalMembers.rand<ulong>();
	  logger.functorMethod(INFO, BRIGHT_WHITE) << "Generated new peer ID: " << m_config.m_peer_id;
	  return true;
	}

	//-----------------------------------------------------------------------------------

	private bool store_config()
	{
	  try
	  {
		if (!Tools.create_directories_if_necessary(m_config_folder))
		{
		  logger.functorMethod(INFO) << "Failed to create data directory: " << m_config_folder;
		  return false;
		}

		string state_file_path = m_config_folder + "/" + m_p2p_state_filename;
		std::ofstream p2p_data = new std::ofstream();
		p2p_data.open(state_file_path, std::ios_base.binary | std::ios_base.@out | std::ios.trunc);
		if (p2p_data.fail())
		{
		  logger.functorMethod(INFO) << "Failed to save config to file " << state_file_path;
		  return false;
		};

		StdOutputStream stream = new StdOutputStream(p2p_data);
		BinaryOutputStreamSerializer a = new BinaryOutputStreamSerializer(stream);
		CryptoNote.GlobalMembers.serialize(this, a.functorMethod);
		return true;
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(WARNING) << "store_config failed: " << e.Message;
	  }

	  return false;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void initUpnp();


	//-----------------------------------------------------------------------------------
	private bool handshake(CryptoNote.LevinProtocol proto, P2pConnectionContext context, bool just_take_peerlist = false)
	{
	  COMMAND_HANDSHAKE.request arg = new COMMAND_HANDSHAKE.request();
	  COMMAND_HANDSHAKE.response rsp = new COMMAND_HANDSHAKE.response();
	  get_local_node_data(arg.node_data);
	  m_payload_handler.get_payload_sync_data(arg.payload_data);

	  if (!proto.invoke(COMMAND_HANDSHAKE.ID, arg, rsp))
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "A daemon on the network has departed. MSG: Failed to invoke COMMAND_HANDSHAKE, closing connection.";
		return false;
	  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: context.version = rsp.node_data.version;
	  context.version.CopyFrom(rsp.node_data.version);

	  if (rsp.node_data.network_id != m_network_id)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "COMMAND_HANDSHAKE Failed, wrong network! (" << rsp.node_data.network_id << "), closing connection.";
		return false;
	  }

	  if (rsp.node_data.version < CryptoNote.P2P_MINIMUM_VERSION)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << context << "COMMAND_HANDSHAKE Failed, peer is wrong version! (" << Convert.ToString(rsp.node_data.version) << "), closing connection.";
		return false;
	  }
	  else if ((rsp.node_data.version - CryptoNote.P2P_CURRENT_VERSION) >= CryptoNote.P2P_UPGRADE_WINDOW)
	  {
		logger.functorMethod(Logging.Level.WARNING) << context << "COMMAND_HANDSHAKE Warning, your software may be out of date. Please visit: " << CryptoNote.LATEST_VERSION_URL << " for the latest version.";
	  }

	  if (!handle_remote_peerlist(rsp.local_peerlist, new ulong(rsp.node_data.local_time), context))
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "COMMAND_HANDSHAKE: failed to handle_remote_peerlist(...), closing connection.";
		return false;
	  }

	  if (just_take_peerlist)
	  {
		return true;
	  }

	  if (!m_payload_handler.process_payload_sync_data(rsp.payload_data, context, true))
	  {
		logger.functorMethod(Logging.Level.ERROR) << context << "COMMAND_HANDSHAKE invoked, but process_payload_sync_data returned false, dropping connection.";
		return false;
	  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: context.peerId = rsp.node_data.peer_id;
	  context.peerId.CopyFrom(rsp.node_data.peer_id);
	  m_peerlist.set_peer_just_seen(new ulong(rsp.node_data.peer_id), new uint(context.m_remote_ip), new uint(context.m_remote_port));

	  if (rsp.node_data.peer_id == m_config.m_peer_id)
	  {
		logger.functorMethod(Logging.Level.TRACE) << context << "Connection to self detected, dropping connection";
		return false;
	  }

	  logger.functorMethod(Logging.Level.DEBUGGING) << context << "COMMAND_HANDSHAKE INVOKED OK";
	  return true;
	}
	private bool timedSync()
	{
	  COMMAND_TIMED_SYNC.request arg = boost::value_initialized<COMMAND_TIMED_SYNC.request>();
	  m_payload_handler.get_payload_sync_data(arg.payload_data);
	  var cmdBuf = LevinProtocol.encode<COMMAND_TIMED_SYNC.request>(arg);

	  forEachConnection((P2pConnectionContext conn) =>
	  {
		if (conn.peerId != null && (conn.m_state == CryptoNoteConnectionContext.state_normal || conn.m_state == CryptoNoteConnectionContext.state_idle))
		{
		  conn.pushMessage(new P2pMessage(P2pMessage.COMMAND, COMMAND_TIMED_SYNC.ID, cmdBuf));
		}
	  });

	  return true;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handleTimedSyncResponse(BinaryArray in, P2pConnectionContext context);
	private void forEachConnection(Action<P2pConnectionContext > action)
	{

	  // create copy of connection ids because the list can be changed during action
	  List<boost::uuids.uuid> connectionIds = new List<boost::uuids.uuid>();
	  connectionIds.Capacity = m_connections.Count;
	  foreach (var c in m_connections)
	  {
		connectionIds.Add(c.first);
	  }

	  foreach (var connId in connectionIds)
	  {
		var it = m_connections.find(connId);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		if (it != m_connections.end())
		{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		  action(it.second);
		}
	  }
	}

	//-----------------------------------------------------------------------------------

	private void on_connection_new(P2pConnectionContext context)
	{
	  logger.functorMethod(TRACE) << context << "NEW CONNECTION";
	  m_payload_handler.onConnectionOpened(context);
	}
	//-----------------------------------------------------------------------------------

	private void on_connection_close(P2pConnectionContext context)
	{
	  logger.functorMethod(TRACE) << context << "CLOSE CONNECTION";
	  m_payload_handler.onConnectionClosed(context);
	}

	//----------------- i_p2p_endpoint -------------------------------------------------------------
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void relay_notify_to_all(int command, BinaryArray data_buff, boost::uuids::uuid excludeConnection);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool invoke_notify_to_peer(int command, BinaryArray req_buff, CryptoNoteConnectionContext context);

	//-----------------------------------------------------------------------------------
	private void for_each_connection(Action<CryptoNoteConnectionContext , ulong> f)
	{
	  foreach (var ctx in m_connections)
	  {
		f(ctx.second, ctx.second.peerId);
	  }
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void externalRelayNotifyToAll(int command, BinaryArray data_buff, boost::uuids::uuid excludeConnection);

	//-----------------------------------------------------------------------------------------------

	//-----------------------------------------------------------------------------------

	private bool handle_command_line(boost::program_options.variables_map vm)
	{
	  m_bind_ip = command_line.GlobalMembers.get_arg(vm, GlobalMembers.arg_p2p_bind_ip);
	  m_port = command_line.GlobalMembers.get_arg(vm, GlobalMembers.arg_p2p_bind_port);
	  m_external_port = command_line.GlobalMembers.get_arg(vm, GlobalMembers.arg_p2p_external_port);
	  m_allow_local_ip = command_line.GlobalMembers.get_arg(vm, GlobalMembers.arg_p2p_allow_local_ip);

	  if (command_line.GlobalMembers.has_arg(vm, GlobalMembers.arg_p2p_add_peer))
	  {
		List<string> perrs = command_line.GlobalMembers.get_arg(vm, GlobalMembers.arg_p2p_add_peer);
		foreach (string pr_str in perrs)
		{
		  PeerlistEntry pe = boost::value_initialized<PeerlistEntry>();
		  pe.id = Crypto.GlobalMembers.rand<ulong>();
		  bool r = GlobalMembers.parse_peer_from_string(pe.adr, pr_str);
		  if (!(r))
		  {
			  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to parse address from string: " << pr_str;
			  return false;
		  }
		  m_command_line_peers.AddLast(pe);
		}
	  }

	  if (command_line.GlobalMembers.has_arg(vm, GlobalMembers.arg_p2p_add_exclusive_node))
	  {
		if (!parse_peers_and_add_to_container(vm, GlobalMembers.arg_p2p_add_exclusive_node, m_exclusive_peers))
		{
		  return false;
		}
	  }
	  if (command_line.GlobalMembers.has_arg(vm, GlobalMembers.arg_p2p_add_priority_node))
	  {
		if (!parse_peers_and_add_to_container(vm, GlobalMembers.arg_p2p_add_priority_node, m_priority_peers))
		{
		  return false;
		}
	  }
	  if (command_line.GlobalMembers.has_arg(vm, GlobalMembers.arg_p2p_seed_node))
	  {
		if (!parse_peers_and_add_to_container(vm, GlobalMembers.arg_p2p_seed_node, m_seed_nodes))
		{
		  return false;
		}
	  }

	  if (command_line.GlobalMembers.has_arg(vm, GlobalMembers.arg_p2p_hide_my_port))
	  {
		m_hide_my_port = true;
	  }

	  return true;
	}
	private bool handleConfig(NetNodeConfig config)
	{
	  m_bind_ip = config.getBindIp();
	  m_port = Convert.ToString(config.getBindPort());
	  m_external_port = config.getExternalPort();
	  m_allow_local_ip = config.getAllowLocalIp();

	  var peers = config.getPeers();
	  std::copy(peers.GetEnumerator(), peers.end(), std::back_inserter(m_command_line_peers));

	  var exclusiveNodes = config.getExclusiveNodes();
	  std::copy(exclusiveNodes.GetEnumerator(), exclusiveNodes.end(), std::back_inserter(m_exclusive_peers));

	  var priorityNodes = config.getPriorityNodes();
	  std::copy(priorityNodes.GetEnumerator(), priorityNodes.end(), std::back_inserter(m_priority_peers));

	  var seedNodes = config.getSeedNodes();
	  std::copy(seedNodes.GetEnumerator(), seedNodes.end(), std::back_inserter(m_seed_nodes));

	  m_hide_my_port = config.getHideMyPort();
	  return true;
	}
	private bool append_net_address(List<NetworkAddress> nodes, string addr)
	{
	  uint pos = addr.LastIndexOfAny((Convert.ToString(':')).ToCharArray());
	  if (!(-1 != pos && addr.Length - 1 != pos && 0 != pos))
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to parse seed address from string: '" << addr << '\'';
		return false;
	  }

	  string host = addr.Substring(0, pos);

	  try
	  {
		uint port = Common.GlobalMembers.fromString<uint>(addr.Substring(pos + 1));

		System.Ipv4Resolver resolver = new System.Ipv4Resolver(m_dispatcher);
		var addr = resolver.resolve(host);
		nodes.Add(new NetworkAddress({GlobalMembers.hostToNetwork(addr.getValue()), port}));

		logger.functorMethod(TRACE) << "Added seed node: " << nodes[nodes.Count - 1] << " (" << host << ")";

	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(ERROR, BRIGHT_YELLOW) << "Failed to resolve host name '" << host << "': " << e.Message;
		return false;
	  }

	  return true;
	}

	//-----------------------------------------------------------------------------------
	private bool idle_worker()
	{
	  try
	  {
		m_connections_maker_interval.call(std::bind(this.connections_maker, this));
		m_peerlist_store_interval.call(std::bind(this.store_config, this));
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(DEBUGGING) << "exception in idle_worker: " << e.Message;
	  }
	  return true;
	}

	//-----------------------------------------------------------------------------------

	private bool handle_remote_peerlist(LinkedList<PeerlistEntry> peerlist, DateTime local_time, CryptoNoteConnectionContext context)
	{
	  long delta = 0;
	  LinkedList<PeerlistEntry> peerlist_ = new LinkedList(peerlist);
	  if (!fix_time_delta(peerlist_, new DateTime(local_time), ref delta))
	  {
		return false;
	  }
	  logger.functorMethod(Logging.Level.TRACE) << context << "REMOTE PEERLIST: TIME_DELTA: " << delta << ", remote peerlist size=" << peerlist_.Count;
	  logger.functorMethod(Logging.Level.TRACE) << context << "REMOTE PEERLIST: " << GlobalMembers.print_peerlist_to_string(peerlist_);
	  return m_peerlist.merge_peerlist(peerlist_);
	}
	//-----------------------------------------------------------------------------------

	private bool get_local_node_data(basic_node_data node_data)
	{
	  node_data.version = CryptoNote.P2P_CURRENT_VERSION;
	  DateTime local_time = new DateTime();
	  time(local_time);
	  node_data.local_time = local_time;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: node_data.peer_id = m_config.m_peer_id;
	  node_data.peer_id.CopyFrom(m_config.m_peer_id);
	  if (!m_hide_my_port)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: node_data.my_port = m_external_port ? m_external_port : m_listeningPort;
		node_data.my_port.CopyFrom(m_external_port != null ? m_external_port : m_listeningPort);
	  }
	  else
	  {
		node_data.my_port = 0;
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: node_data.network_id = m_network_id;
	  node_data.network_id.CopyFrom(m_network_id);
	  return true;
	}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool merge_peerlist_with_local(ClassicLinkedList<PeerlistEntry> bs);

	//-----------------------------------------------------------------------------------
	private bool fix_time_delta(LinkedList<PeerlistEntry> local_peerlist, DateTime local_time, ref long delta)
	{
	  //fix time delta
	  DateTime now = 0;
	  time(now);
	  delta = now - local_time;

	  foreach (PeerlistEntry be in local_peerlist)
	  {
		if (be.last_seen > ulong(local_time))
		{
		  logger.functorMethod(ERROR) << "FOUND FUTURE peerlist for entry " << be.adr << " last_seen: " << be.last_seen << ", local_time(on remote node):" << local_time;
		  return false;
		}
		be.last_seen += delta;
	  }
	  return true;
	}

	//-----------------------------------------------------------------------------------

	private bool connections_maker()
	{
	  if (!connect_to_peerlist(m_exclusive_peers))
	  {
		return false;
	  }

	  if (m_exclusive_peers.Count > 0)
	  {
		return true;
	  }

	  if (m_peerlist.get_white_peers_count() == null && m_seed_nodes.Count)
	  {
		uint try_count = 0;
		uint current_index = Crypto.GlobalMembers.rand<uint>() % m_seed_nodes.Count;

		while (true)
		{
		  if (try_to_connect_and_handshake_with_new_peer(m_seed_nodes[current_index], true))
		  {
			break;
		  }

		  if (++try_count > m_seed_nodes.Count)
		  {
			logger.functorMethod(ERROR) << "Failed to connect to any of seed peers, continuing without seeds";
			break;
		  }
		  if (++current_index >= m_seed_nodes.Count)
		  {
			current_index = 0;
		  }
		}
	  }

	  if (!connect_to_peerlist(m_priority_peers))
	  {
		  return false;
	  }

	  uint expected_white_connections = (m_config.m_net_config.connections_count * CryptoNote.P2P_DEFAULT_WHITELIST_CONNECTIONS_PERCENT) / 100;

	  uint conn_count = get_outgoing_connections_count();
	  if (conn_count < m_config.m_net_config.connections_count)
	  {
		if (conn_count < expected_white_connections)
		{
		  //start from white list
		  if (!make_expected_connections_count(true, new uint(expected_white_connections)))
		  {
			return false;
		  }
		  //and then do grey list
		  if (!make_expected_connections_count(false, new uint(m_config.m_net_config.connections_count)))
		  {
			return false;
		  }
		}
		else
		{
		  //start from grey list
		  if (!make_expected_connections_count(false, new uint(m_config.m_net_config.connections_count)))
		  {
			return false;
		  }
		  //and then do white list
		  if (!make_expected_connections_count(true, new uint(m_config.m_net_config.connections_count)))
		  {
			return false;
		  }
		}
	  }

	  return true;
	}

	//-----------------------------------------------------------------------------------
	private bool make_new_connection_from_peerlist(bool use_white_list)
	{
	  uint local_peers_count = use_white_list ? m_peerlist.get_white_peers_count():m_peerlist.get_gray_peers_count();
	  if (local_peers_count == null)
	  {
		return false; //no peers
	  }

	  uint max_random_index = Math.Min<ulong>(local_peers_count - 1, 20);

	  SortedSet<uint> tried_peers = new SortedSet<uint>();

	  uint try_count = 0;
	  uint rand_count = 0;
	  while (rand_count < (max_random_index + 1) * 3 && try_count < 10 && m_stop == null)
	  {
		++rand_count;
		uint random_index = GlobalMembers.get_random_index_with_fixed_probability(new uint(max_random_index));
		if (!(random_index < local_peers_count))
		{
			logger.functorMethod(ERROR, BRIGHT_RED) << "random_starter_index < peers_local.size() failed!!";
			return false;
		}

		if (tried_peers.count(random_index))
		{
		  continue;
		}

		tried_peers.Add(random_index);
		PeerlistEntry pe = boost::value_initialized<PeerlistEntry>();
		bool r = use_white_list ? m_peerlist.get_white_peer_by_index(pe, new uint(random_index)):m_peerlist.get_gray_peer_by_index(pe, new uint(random_index));
		if (!(r))
		{
			logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get random peer from peerlist(white:" << use_white_list << ")";
			return false;
		}

		++try_count;

		if (is_peer_used(pe))
		{
		  continue;
		}

		logger.functorMethod(DEBUGGING) << "Selected peer: " << pe.id << " " << pe.adr << " [white=" << use_white_list << "] last_seen: " << (pe.last_seen != null ? Common.timeIntervalToString(time(null) - pe.last_seen) : "never");

		if (!try_to_connect_and_handshake_with_new_peer(pe.adr, false, new ulong(pe.last_seen), use_white_list))
		{
		  continue;
		}

		return true;
	  }
	  return false;
	}
	private bool try_to_connect_and_handshake_with_new_peer(NetworkAddress na, bool just_take_peerlist = false, ulong last_seen_stamp = 0, bool white = true)
	{

	  logger.functorMethod(DEBUGGING) << "Connecting to " << na << " (white=" << white << ", last_seen: " << (last_seen_stamp != null ? Common.timeIntervalToString(time(null) - last_seen_stamp) : "never") << ")...";

	  try
	  {
		System.TcpConnection connection = new System.TcpConnection();

		try
		{
		  System.Context<System.TcpConnection> connectionContext(m_dispatcher, () =>
		  {
			System.TcpConnector connector = new System.TcpConnector(m_dispatcher);
			return connector.connect(System.Ipv4Address(Common.ipAddressToString(na.ip)), (ushort)na.port);
		  });

		  System.Context<> timeoutContext(m_dispatcher, () =>
		  {
			System.Timer(m_dispatcher).sleep(std::chrono.milliseconds(m_config.m_net_config.connection_timeout));
			logger.functorMethod(DEBUGGING) << "Connection to " << na << " timed out, interrupt it";
			safeInterrupt(connectionContext);
		  });

		  connection = std::move(connectionContext.get());
		}
		catch (System.InterruptedException)
		{
		  logger.functorMethod(DEBUGGING) << "Connection timed out";
		  return false;
		}

		P2pConnectionContext ctx = new P2pConnectionContext(m_dispatcher, logger.GetLogger(), std::move(connection));

		ctx.m_connection_id = boost::uuids.random_generator()();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ctx.m_remote_ip = na.ip;
		ctx.m_remote_ip.CopyFrom(na.ip);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ctx.m_remote_port = na.port;
		ctx.m_remote_port.CopyFrom(na.port);
		ctx.m_is_income = false;
		ctx.m_started = time(null);


		try
		{
		  System.Context<bool> handshakeContext(m_dispatcher, () =>
		  {
			CryptoNote.LevinProtocol proto = new CryptoNote.LevinProtocol(ctx.connection);
			return handshake(proto, ctx, just_take_peerlist);
		  });

		  System.Context<> timeoutContext(m_dispatcher, () =>
		  {
			// Here we use connection_timeout * 3, one for this handshake, and two for back ping from peer.
			System.Timer(m_dispatcher).sleep(std::chrono.milliseconds(m_config.m_net_config.connection_timeout * 3));
			logger.functorMethod(DEBUGGING) << "Handshake with " << na << " timed out, interrupt it";
			safeInterrupt(handshakeContext);
		  });

		  if (!handshakeContext.get())
		  {
			logger.functorMethod(DEBUGGING) << "Failed to HANDSHAKE with peer " << na;
			return false;
		  }
		}
		catch (System.InterruptedException)
		{
		  logger.functorMethod(DEBUGGING) << "Handshake timed out";
		  return false;
		}

		if (just_take_peerlist)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING, Logging.BRIGHT_GREEN) << ctx << "CONNECTION HANDSHAKED OK AND CLOSED.";
		  return true;
		}

		PeerlistEntry pe_local = boost::value_initialized<PeerlistEntry>();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pe_local.adr = na;
		pe_local.adr.CopyFrom(na);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pe_local.id = ctx.peerId;
		pe_local.id.CopyFrom(ctx.peerId);
		pe_local.last_seen = time(null);
		m_peerlist.append_with_peer_white(pe_local);

		if (m_stop != null)
		{
		  throw new System.InterruptedException();
		}

		var iter = m_connections.Add(ctx.m_connection_id, std::move(ctx)).first;
		boost::uuids.uuid connectionId = iter.first;
		P2pConnectionContext connectionContext = iter.second;

		m_workingContextGroup.spawn(std::bind(this.connectionHandler, this, std::cref(connectionId), std::@ref(connectionContext)));

		return true;
	  }
	  catch (System.InterruptedException)
	  {
		logger.functorMethod(DEBUGGING) << "Connection process interrupted";
		throw;
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(DEBUGGING) << "Connection to " << na << " failed: " << e.Message;
	  }

	  return false;
	}

	//-----------------------------------------------------------------------------------
	private bool is_peer_used(PeerlistEntry peer)
	{
	  if (m_config.m_peer_id == peer.id)
	  {
		return true; //dont make connections to ourself
	  }

	  foreach (var kv in m_connections)
	  {
		auto cntxt = kv.second;
		if (cntxt.peerId == peer.id || (!cntxt.m_is_income && peer.adr.ip == cntxt.m_remote_ip && peer.adr.port == cntxt.m_remote_port))
		{
		  return true;
		}
	  }
	  return false;
	}
	//-----------------------------------------------------------------------------------

	private bool is_addr_connected(NetworkAddress peer)
	{
	  foreach (var conn in m_connections)
	  {
		if (!conn.second.m_is_income && peer.ip == conn.second.m_remote_ip && peer.port == conn.second.m_remote_port)
		{
		  return true;
		}
	  }
	  return false;
	}

	//-----------------------------------------------------------------------------------
	private bool try_ping(basic_node_data node_data, P2pConnectionContext context)
	{
	  if (node_data.my_port == null)
	  {
		return false;
	  }

	  uint actual_ip = new uint(context.m_remote_ip);
	  if (!m_peerlist.is_ip_allowed(new uint(actual_ip)))
	  {
		return false;
	  }

	  var ip = Common.ipAddressToString(actual_ip);
	  var port = node_data.my_port;
	  var peerId = node_data.peer_id;

	  try
	  {
		COMMAND_PING.request req = new COMMAND_PING.request();
		COMMAND_PING.response rsp = new COMMAND_PING.response();
		System.Context<> pingContext(m_dispatcher, () =>
		{
		  System.TcpConnector connector = new System.TcpConnector(m_dispatcher);
		  var connection = connector.connect(System.Ipv4Address(ip), (ushort)port);
		  LevinProtocol(connection).invoke(COMMAND_PING.ID, req, rsp);
		});

		System.Context<> timeoutContext(m_dispatcher, () =>
		{
		  System.Timer(m_dispatcher).sleep(std::chrono.milliseconds(m_config.m_net_config.connection_timeout * 2));
		  logger.functorMethod(DEBUGGING) << context << "Back ping timed out" << ip << ":" << port;
		  safeInterrupt(pingContext);
		});

		pingContext.get();

		if (rsp.status != PING_OK_RESPONSE_STATUS_TEXT || peerId != rsp.peer_id)
		{
		  logger.functorMethod(DEBUGGING) << context << "Back ping invoke wrong response \"" << rsp.status << "\" from" << ip << ":" << port << ", hsh_peer_id=" << peerId << ", rsp.peer_id=" << rsp.peer_id;
		  return false;
		}
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(DEBUGGING) << context << "Back ping connection to " << ip << ":" << port << " failed: " << e.Message;
		return false;
	  }

	  return true;
	}
	//-----------------------------------------------------------------------------------

	private bool make_expected_connections_count(bool white_list, uint expected_connections)
	{
	  uint conn_count = get_outgoing_connections_count();
	  //add new connections from white peers
	  while (conn_count < expected_connections)
	  {
		if (m_stopEvent.get())
		{
		  return false;
		}

		if (!make_new_connection_from_peerlist(white_list))
		{
		  break;
		}
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: conn_count = get_outgoing_connections_count();
		conn_count.CopyFrom(get_outgoing_connections_count());
	  }
	  return true;
	}
	private bool is_priority_node(NetworkAddress na)
	{
	  return (m_priority_peers.Contains(na)) || (m_exclusive_peers.Contains(na));
	}

	private bool connect_to_peerlist(List<NetworkAddress> peers)
	{
	  foreach (var na in peers)
	  {
		if (!is_addr_connected(na))
		{
		  try_to_connect_and_handshake_with_new_peer(na);
		}
	  }

	  return true;
	}

	private bool parse_peers_and_add_to_container(boost::program_options.variables_map vm, command_line.arg_descriptor<List<string>> arg, List<NetworkAddress> container)
	{
	  List<string> perrs = command_line.GlobalMembers.get_arg(vm, arg);

	  foreach (string pr_str in perrs)
	  {
		NetworkAddress na = new NetworkAddress();
		if (!GlobalMembers.parse_peer_from_string(na, pr_str))
		{
		  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to parse address from string: " << pr_str;
		  return false;
		}
		container.Add(na);
	  }

	  return true;
	}

	//debug functions
	//-----------------------------------------------------------------------------------

	private string print_connections_container()
	{

	  std::stringstream ss = new std::stringstream();

	  foreach (var cntxt in m_connections)
	  {
		ss << Common.ipAddressToString(cntxt.second.m_remote_ip) << ":" << cntxt.second.m_remote_port << " \t\tpeer_id " << cntxt.second.peerId << " \t\tconn_id " << cntxt.second.m_connection_id << (cntxt.second.m_is_income ? " INCOMING" : " OUTGOING") << std::endl;
	  }

	  return ss.str();
	}

	private Dictionary<boost::uuids.uuid, P2pConnectionContext, boost::hash<boost::uuids.uuid>> m_connections = new Dictionary<boost::uuids.uuid, P2pConnectionContext, boost::hash<boost::uuids.uuid>>();

	private void acceptLoop()
	{
	  while (m_stop == null)
	  {
		try
		{
		  P2pConnectionContext ctx = new P2pConnectionContext(m_dispatcher, logger.GetLogger(), m_listener.accept());
		  ctx.m_connection_id = boost::uuids.random_generator()();
		  ctx.m_is_income = true;
		  ctx.m_started = time(null);

		  var addressAndPort = ctx.connection.getPeerAddressAndPort();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: ctx.m_remote_ip = hostToNetwork(addressAndPort.first.getValue());
		  ctx.m_remote_ip.CopyFrom(GlobalMembers.hostToNetwork(addressAndPort.first.getValue()));
		  ctx.m_remote_port = addressAndPort.second;

		  var iter = m_connections.Add(ctx.m_connection_id, std::move(ctx)).first;
		  boost::uuids.uuid connectionId = iter.first;
		  P2pConnectionContext connection = iter.second;

		  m_workingContextGroup.spawn(std::bind(this.connectionHandler, this, std::cref(connectionId), std::@ref(connection)));
		}
		catch (System.InterruptedException)
		{
		  logger.functorMethod(DEBUGGING) << "acceptLoop() is interrupted";
		  break;
		}
		catch (System.Exception e)
		{
		  logger.functorMethod(DEBUGGING) << "Exception in acceptLoop: " << e.Message;
		}
	  }

	  logger.functorMethod(DEBUGGING) << "acceptLoop finished";
	}
	private void connectionHandler(boost::uuids.uuid connectionId, P2pConnectionContext ctx)
	{
	  // This inner context is necessary in order to stop connection handler at any moment
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::Context<> context(m_dispatcher, [this, &connectionId, &ctx]
	  System.Context<> context(m_dispatcher, () =>
	  {
		System.Context<> writeContext = new System.Context<>(m_dispatcher, std::bind(this.writeHandler, this, std::@ref(ctx)));

		try
		{
		  on_connection_new(ctx);

		  LevinProtocol proto = new LevinProtocol(ctx.connection);
		  LevinProtocol.Command cmd = new LevinProtocol.Command();

		  for (;;)
		  {
			if (ctx.m_state == CryptoNoteConnectionContext.state_sync_required)
			{
			  ctx.m_state = CryptoNoteConnectionContext.state_synchronizing;
			  m_payload_handler.start_sync(ctx);
			}
			else if (ctx.m_state == CryptoNoteConnectionContext.state_pool_sync_required)
			{
			  ctx.m_state = CryptoNoteConnectionContext.state_normal;
			  m_payload_handler.requestMissingPoolTransactions(ctx);
			}

			if (!proto.readCommand(cmd))
			{
			  break;
			}

			List<ushort> response = new List<ushort>();
			bool handled = false;
			var retcode = handleCommand(cmd, response, ctx, ref handled);

			// send response
			if (cmd.needReply())
			{
			  if (!handled)
			  {
				retcode = (int)LevinError.ERROR_CONNECTION_HANDLER_NOT_DEFINED;
				response.Clear();
			  }

			  ctx.pushMessage(new P2pMessage(P2pMessage.REPLY, new uint(cmd.command), std::move(response), retcode));
			}

			if (ctx.m_state == CryptoNoteConnectionContext.state_shutdown)
			{
			  break;
			}
		  }
		}
		catch (System.InterruptedException)
		{
		  logger.functorMethod(DEBUGGING) << ctx << "connectionHandler() inner context is interrupted";
		}
		catch (System.Exception e)
		{
		  logger.functorMethod(DEBUGGING) << ctx << "Exception in connectionHandler: " << e.Message;
		}

		safeInterrupt(ctx);
		safeInterrupt(writeContext);
		writeContext.wait();

		on_connection_close(ctx);
		m_connections.Remove(connectionId);
	  });

	  ctx.context = context;

	  try
	  {
		context.get();
	  }
	  catch (System.InterruptedException)
	  {
		logger.functorMethod(DEBUGGING) << "connectionHandler() is interrupted";
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(WARNING) << "connectionHandler() throws exception: " << e.Message;
	  }
	  catch
	  {
		logger.functorMethod(WARNING) << "connectionHandler() throws unknown exception";
	  }
	}
	private void writeHandler(P2pConnectionContext ctx)
	{
	  logger.functorMethod(DEBUGGING) << ctx << "writeHandler started";

	  try
	  {
		LevinProtocol proto = new LevinProtocol(ctx.connection);

		for (;;)
		{
		  var msgs = ctx.popBuffer();
		  if (msgs.Count == 0)
		  {
			break;
		  }

		  foreach (var msg in msgs)
		  {
			logger.functorMethod(DEBUGGING) << ctx << "msg " << msg.type << ':' << msg.command;
			switch (msg.type)
			{
			case P2pMessage.COMMAND:
			  proto.sendMessage(new uint(msg.command), msg.buffer, true);
			  break;
			case P2pMessage.NOTIFY:
			  proto.sendMessage(new uint(msg.command), msg.buffer, false);
			  break;
			case P2pMessage.REPLY:
			  proto.sendReply(new uint(msg.command), msg.buffer, new int(msg.returnCode));
			  break;
			default:
			  Debug.Assert(false);
			  break;
			}
		  }
		}
	  }
	  catch (System.InterruptedException)
	  {
		// connection stopped
		logger.functorMethod(DEBUGGING) << ctx << "writeHandler() is interrupted";
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(DEBUGGING) << ctx << "error during write: " << e.Message;
		safeInterrupt(ctx); // stop connection on write error
	  }

	  logger.functorMethod(DEBUGGING) << ctx << "writeHandler finished";
	}
	private void onIdle()
	{
	  logger.functorMethod(DEBUGGING) << "onIdle started";

	  while (m_stop == null)
	  {
		try
		{
		  idle_worker();
		  m_idleTimer.sleep(std::chrono.seconds(1));
		}
		catch (System.InterruptedException)
		{
		  logger.functorMethod(DEBUGGING) << "onIdle() is interrupted";
		  break;
		}
		catch (System.Exception e)
		{
		  logger.functorMethod(WARNING) << "Exception in onIdle: " << e.Message;
		}
	  }

	  logger.functorMethod(DEBUGGING) << "onIdle finished";
	}
	private void timedSyncLoop()
	{
	  try
	  {
		for (;;)
		{
		  m_timedSyncTimer.sleep(std::chrono.seconds(P2P_DEFAULT_HANDSHAKE_INTERVAL));
		  timedSync();
		}
	  }
	  catch (System.InterruptedException)
	  {
		logger.functorMethod(DEBUGGING) << "timedSyncLoop() is interrupted";
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(WARNING) << "Exception in timedSyncLoop: " << e.Message;
	  }

	  logger.functorMethod(DEBUGGING) << "timedSyncLoop finished";
	}
	private void timeoutLoop()
	{
	  try
	  {
		while (m_stop == null)
		{
		  m_timeoutTimer.sleep(std::chrono.seconds(10));
		  var now = std::chrono.steady_clock.now();

		  foreach (var kv in m_connections)
		  {
			auto ctx = kv.second;
			if (ctx.writeDuration(now) > P2P_DEFAULT_INVOKE_TIMEOUT)
			{
			  logger.functorMethod(DEBUGGING) << ctx << "write operation timed out, stopping connection";
			  safeInterrupt(ctx);
			}
		  }
		}
	  }
	  catch (System.InterruptedException)
	  {
		logger.functorMethod(DEBUGGING) << "timeoutLoop() is interrupted";
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(WARNING) << "Exception in timeoutLoop: " << e.Message;
	  }
	  catch
	  {
		logger.functorMethod(WARNING) << "Unknown exception in timeoutLoop";
	  }
	}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
	private void safeInterrupt<T>(T obj)
	{
	  try
	  {
		obj.interrupt();
	  }
	  catch (System.Exception e)
	  {
		logger.functorMethod(WARNING) << "interrupt() throws exception: " << e.Message;
	  }
	  catch
	  {
		logger.functorMethod(WARNING) << "interrupt() throws unknown exception";
	  }
	}

	private class config
	{
	  public network_config m_net_config = new network_config();
	  public ulong m_peer_id = new ulong();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(m_net_config, "m_net_config");
		s.functorMethod(m_peer_id, "m_peer_id");
	  }
	}

	private config m_config = new config();
	private string m_config_folder;

	private bool m_have_address;
	private bool m_first_connection_maker_call;
	private uint m_listeningPort = new uint();
	private uint m_external_port = new uint();
	private uint m_ip_address = new uint();
	private bool m_allow_local_ip;
	private bool m_hide_my_port;
	private string m_p2p_state_filename;

	private System.Dispatcher m_dispatcher;
	private System.ContextGroup m_workingContextGroup = new System.ContextGroup();
	private System.Event m_stopEvent = new System.Event();
	private System.Timer m_idleTimer = new System.Timer();
	private System.Timer m_timeoutTimer = new System.Timer();
	private System.TcpListener m_listener = new System.TcpListener();
	private Logging.LoggerRef logger = new Logging.LoggerRef();
	private std::atomic<bool> m_stop = new std::atomic<bool>();

	private CryptoNoteProtocolHandler m_payload_handler;
	private PeerlistManager m_peerlist = new PeerlistManager();

	// OnceInInterval m_peer_handshake_idle_maker_interval;
	private OnceInInterval m_connections_maker_interval = new OnceInInterval();
	private OnceInInterval m_peerlist_store_interval = new OnceInInterval();
	private System.Timer m_timedSyncTimer = new System.Timer();

	private string m_bind_ip;
	private string m_port;
#if ALLOW_DEBUG_COMMANDS
	private ulong m_last_stat_request_time = new ulong();
#endif
	private List<NetworkAddress> m_priority_peers = new List<NetworkAddress>();
	private List<NetworkAddress> m_exclusive_peers = new List<NetworkAddress>();
	private List<NetworkAddress> m_seed_nodes = new List<NetworkAddress>();
	private LinkedList<PeerlistEntry> m_command_line_peers = new LinkedList<PeerlistEntry>();
	private ulong m_peer_livetime = new ulong();
	private boost::uuids.uuid m_network_id = new boost::uuids.uuid();
  }
}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace



namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong P2pConnectionContext::writeDuration(TimePoint now) const


//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Command, typename Handler>


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INVOKE_HANDLER(CMD, Handler) case CMD::ID: { ret = invokeAdaptor<CMD>(cmd.buf, out, ctx, std::bind(Handler, this, _1, _2, _3, _4)); break; }



  //-----------------------------------------------------------------------------------

  //-----------------------------------------------------------------------------------

  //-----------------------------------------------------------------------------------


  //-----------------------------------------------------------------------------------

}
