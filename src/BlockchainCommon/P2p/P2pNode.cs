// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using Logging;
using System;
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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class P2pContext;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class P2pConnectionProxy;

public class P2pNode : IP2pNode, IStreamSerializable, IP2pNodeInternal, System.IDisposable
{


  public P2pNode(P2pNodeConfig cfg, Dispatcher dispatcher, Logging.ILogger log, Crypto.Hash genesisHash, ulong peerId)
  {
	  this.logger = new Logging.LoggerRef(log, "P2pNode:" + Convert.ToString(cfg.getBindPort()));
	  this.m_stopRequested = false;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_cfg = cfg;
	  this.m_cfg.CopyFrom(cfg);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_myPeerId = peerId;
	  this.m_myPeerId.CopyFrom(peerId);
	  this.m_genesisPayload = new CryptoNote.CORE_SYNC_DATA(new CORE_SYNC_DATA({1, genesisHash}));
	  this.m_dispatcher = dispatcher;
	  this.workingContextGroup = dispatcher;
	  this.m_connectorTimer = dispatcher;
	  this.m_queueEvent = dispatcher;
	m_peerlist.init(cfg.getAllowLocalIp());
	m_listener = TcpListener(m_dispatcher, Ipv4Address(m_cfg.getBindIp()), m_cfg.getBindPort());
	foreach (var peer in cfg.getPeers())
	{
	  m_peerlist.append_with_peer_white(peer);
	}
  }

  public void Dispose()
  {
	Debug.Assert(m_contexts.Count == 0);
	Debug.Assert(m_connectionQueue.Count == 0);
  }

  // IP2pNode
  public std::unique_ptr<IP2pConnection> receiveConnection()
  {
	while (m_connectionQueue.Count == 0)
	{
	  m_queueEvent.wait();
	  m_queueEvent.clear();
	  if (m_stopRequested)
	  {
		throw InterruptedException();
	  }
	}

	var connection = std::move(m_connectionQueue.First.Value);
	m_connectionQueue.RemoveFirst();

	return connection;
  }
  public void stop()
  {
	if (m_stopRequested)
	{
	  return; // already stopped
	}

	m_stopRequested = true;
	// clear prepared connections
	m_connectionQueue.Clear();
	// stop processing
	m_queueEvent.set();
	workingContextGroup.interrupt();
	workingContextGroup.wait();
  }

  // IStreamSerializable
  public void save(std::ostream os)
  {
	StdOutputStream stream = new StdOutputStream(os);
	BinaryOutputStreamSerializer a = new BinaryOutputStreamSerializer(stream);
	CryptoNote.GlobalMembers.serialize(this, a.functorMethod);
  }
  public void load(std::istream in)
  {
	StdInputStream stream = new StdInputStream(in);
	BinaryInputStreamSerializer a = new BinaryInputStreamSerializer(stream);
	CryptoNote.GlobalMembers.serialize(this, a.functorMethod);
  }

  // P2pNode
  public void start()
  {
	workingContextGroup.spawn(std::bind(this.acceptLoop, this));
	workingContextGroup.spawn(std::bind(this.connectorLoop, this));
  }
  public void serialize(ISerializer s)
  {
	ushort version = 1;
	s.functorMethod(version, "version");

	if (version != 1)
	{
	  return;
	}

	s.functorMethod(m_peerlist, "peerlist");
  }


  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private bool m_stopRequested;
  private readonly P2pNodeConfig m_cfg = new P2pNodeConfig();
  private readonly ulong m_myPeerId = new ulong();
  private readonly CORE_SYNC_DATA m_genesisPayload = new CORE_SYNC_DATA();

  private System.Dispatcher m_dispatcher;
  private System.ContextGroup workingContextGroup = new System.ContextGroup();
  private System.TcpListener m_listener = new System.TcpListener();
  private System.Timer m_connectorTimer = new System.Timer();
  private PeerlistManager m_peerlist = new PeerlistManager();
  private LinkedList<std::unique_ptr<P2pContext>> m_contexts = new LinkedList<std::unique_ptr<P2pContext>>();
  private System.Event m_queueEvent = new System.Event();
  private LinkedList<std::unique_ptr<IP2pConnection>> m_connectionQueue = new LinkedList<std::unique_ptr<IP2pConnection>>();

  // IP2pNodeInternal
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const CORE_SYNC_DATA& getGenesisPayload() const override
  private CORE_SYNC_DATA getGenesisPayload()
  {
	return m_genesisPayload;
  }
  private LinkedList<PeerlistEntry> getLocalPeerList()
  {
	LinkedList<PeerlistEntry> peerlist = new LinkedList<PeerlistEntry>();
	m_peerlist.get_peerlist_head(peerlist);
	return peerlist;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual basic_node_data getNodeData() const override
  private basic_node_data getNodeData()
  {
	basic_node_data nodeData = new basic_node_data();
	nodeData.network_id = m_cfg.getNetworkId();
	nodeData.version = CryptoNote.P2P_CURRENT_VERSION;
	nodeData.local_time = time(null);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: nodeData.peer_id = m_myPeerId;
	nodeData.peer_id.CopyFrom(m_myPeerId);

	if (m_cfg.getHideMyPort())
	{
	  nodeData.my_port = 0;
	}
	else
	{
	  nodeData.my_port = m_cfg.getExternalPort() ? m_cfg.getExternalPort() : m_cfg.getBindPort();
	}

	return nodeData;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getPeerId() const override
  private ulong getPeerId()
  {
	return m_myPeerId;
  }

  private void handleNodeData(basic_node_data node, P2pContext context)
  {
	if (node.network_id != m_cfg.getNetworkId())
	{
	  std::ostringstream msg = new std::ostringstream();
	  msg << context << "COMMAND_HANDSHAKE Failed, wrong network!  (" << node.network_id << ")";
	  throw new System.Exception(msg.str());
	}

	if (node.version < CryptoNote.P2P_MINIMUM_VERSION)
	{
	  std::ostringstream msg = new std::ostringstream();
	  msg << context << "COMMAND_HANDSHAKE Failed, peer is wrong version! (" << Convert.ToString(node.version) << ")";
	  throw new System.Exception(msg.str());
	}

	if (node.peer_id == m_myPeerId)
	{
	  throw new System.Exception("Connection to self detected");
	}

	context.setPeerInfo(new ushort(node.version), new ulong(node.peer_id), (ushort)node.my_port);
	if (!context.isIncoming())
	{
	  m_peerlist.set_peer_just_seen(node.peer_id, context.getRemoteAddress());
	}
  }
  private bool handleRemotePeerList(LinkedList<PeerlistEntry> peerlist, time_t remoteTime)
  {
	return m_peerlist.merge_peerlist(GlobalMembers.fixTimeDelta(peerlist, new time_t(remoteTime)));
  }
  private void tryPing(P2pContext ctx)
  {
	if (ctx.getPeerId() == m_myPeerId || !m_peerlist.is_ip_allowed(ctx.getRemoteAddress().ip) || ctx.getPeerPort() == 0)
	{
	  return;
	}

	NetworkAddress peerAddress = new NetworkAddress();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: peerAddress.ip = ctx.getRemoteAddress().ip;
	peerAddress.ip.CopyFrom(ctx.getRemoteAddress().ip);
	peerAddress.port = ctx.getPeerPort();

	try
	{
	  TcpConnector connector = new TcpConnector(m_dispatcher);
	  TcpConnection connection = new TcpConnection();

	  GlobalMembers.doWithTimeoutAndThrow(m_dispatcher, m_cfg.getConnectTimeout(), () =>
	  {
		connection = connector.connect(Ipv4Address(Common.ipAddressToString(peerAddress.ip)), (ushort)peerAddress.port);
	  });

	  GlobalMembers.doWithTimeoutAndThrow(m_dispatcher, m_cfg.getHandshakeTimeout(), () =>
	  {
		LevinProtocol proto = new LevinProtocol(connection);
		COMMAND_PING.request request = new COMMAND_PING.request();
		COMMAND_PING.response response = new COMMAND_PING.response();
		proto.invoke(COMMAND_PING.ID, request, response);

		if (response.status == PING_OK_RESPONSE_STATUS_TEXT && response.peer_id == ctx.getPeerId())
		{
		  PeerlistEntry entry = new PeerlistEntry();
		  entry.adr.CopyFrom(peerAddress);
		  entry.id.CopyFrom(ctx.getPeerId());
		  entry.last_seen = time(null);
		  m_peerlist.append_with_peer_white(entry);
		}
		else
		{
		  logger(Logging.DEBUGGING) << ctx << "back ping invoke wrong response \"" << response.status << "\" from" << peerAddress << ", expected peerId=" << ctx.getPeerId() << ", got " << response.peer_id;
		}
	  });
	}
	catch (System.Exception e)
	{
	  logger(DEBUGGING) << "Ping to " << peerAddress << " failed: " << e.Message;
	}
  }

  // spawns
  private void acceptLoop()
  {
	while (!m_stopRequested)
	{
	  try
	  {
		var connection = m_listener.accept();
		var ctx = new P2pContext(m_dispatcher, std::move(connection), true, GlobalMembers.getRemoteAddress(connection), m_cfg.getTimedSyncInterval(), getGenesisPayload());
		logger(INFO) << "Incoming connection from " << ctx.getRemoteAddress();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: workingContextGroup.spawn([this, ctx]
		workingContextGroup.spawn(() =>
		{
		  preprocessIncomingConnection(ContextPtr(ctx));
		});
	  }
	  catch (InterruptedException)
	  {
		break;
	  }
	  catch (System.Exception e)
	  {
		logger(DEBUGGING) << "Exception in acceptLoop: " << e.Message;
	  }
	}

	logger(DEBUGGING) << "acceptLoop finished";
  }
  private void connectorLoop()
  {
	while (!m_stopRequested)
	{
	  try
	  {
		connectPeers();
		m_connectorTimer.sleep(m_cfg.getConnectInterval());
	  }
	  catch (InterruptedException)
	  {
		break;
	  }
	  catch (System.Exception e)
	  {
		logger(WARNING) << "Exception in connectorLoop: " << e.Message;
	  }
	}
  }

  // connection related
  private void connectPeers()
  {
	if (!m_cfg.getExclusiveNodes().empty())
	{
	  connectPeerList(m_cfg.getExclusiveNodes());
	  return;
	}

	// if white peer list is empty, get peers from seeds
	if (m_peerlist.get_white_peers_count() == 0 && !m_cfg.getSeedNodes().empty())
	{
	  var seedNodes = m_cfg.getSeedNodes();
	  std::shuffle(seedNodes.begin(), seedNodes.end(), std::random_device({}));
	  foreach (var seed in seedNodes)
	  {
		var conn = tryToConnectPeer(seed);
		if (conn != null && fetchPeerList(std::move(conn)))
		{
		  break;
		}
	  }
	}

	connectPeerList(m_cfg.getPriorityNodes());

	uint totalExpectedConnectionsCount = m_cfg.getExpectedOutgoingConnectionsCount();
	uint expectedWhiteConnections = (totalExpectedConnectionsCount * m_cfg.getWhiteListConnectionsPercent()) / 100;
	uint outgoingConnections = getOutgoingConnectionsCount();

	if (outgoingConnections < totalExpectedConnectionsCount)
	{
	  if (outgoingConnections < expectedWhiteConnections)
	  {
		//start from white list
		makeExpectedConnectionsCount(m_peerlist.getWhite(), new uint(expectedWhiteConnections));
		//and then do grey list
		makeExpectedConnectionsCount(m_peerlist.getGray(), new uint(totalExpectedConnectionsCount));
	  }
	  else
	  {
		//start from grey list
		makeExpectedConnectionsCount(m_peerlist.getGray(), new uint(totalExpectedConnectionsCount));
		//and then do white list
		makeExpectedConnectionsCount(m_peerlist.getWhite(), new uint(totalExpectedConnectionsCount));
	  }
	}
  }
  private void connectPeerList(List<NetworkAddress> peers)
  {
	foreach (var address in peers)
	{
	  if (!isPeerConnected(address))
	  {
		var conn = tryToConnectPeer(address);
		if (conn != null)
		{
		  enqueueConnection(createProxy(std::move(conn)));
		}
	  }
	}
  }
  private bool isPeerConnected(NetworkAddress address)
  {
	foreach (var c in m_contexts)
	{
	  if (!c.isIncoming() && c.getRemoteAddress() == address)
	  {
		return true;
	  }
	}

	return false;
  }
  private bool isPeerUsed(PeerlistEntry peer)
  {
	if (m_myPeerId == peer.id)
	{
	  return true; //dont make connections to ourself
	}

	foreach (var c in m_contexts)
	{
	  if (c.getPeerId() == peer.id || (!c.isIncoming() && c.getRemoteAddress() == peer.adr))
	  {
		return true;
	  }
	}

	return false;
  }
  private std::unique_ptr<P2pContext> tryToConnectPeer(NetworkAddress address)
  {
	try
	{
	  TcpConnector connector = new TcpConnector(m_dispatcher);
	  TcpConnection tcpConnection = new TcpConnection();

	  GlobalMembers.doWithTimeoutAndThrow(m_dispatcher, m_cfg.getConnectTimeout(), () =>
	  {
		tcpConnection = connector.connect(Ipv4Address(Common.ipAddressToString(address.ip)), (ushort)address.port);
	  });

	  logger(DEBUGGING) << "connection established to " << address;

	  return ContextPtr(new P2pContext(m_dispatcher, std::move(tcpConnection), false, address, m_cfg.getTimedSyncInterval(), getGenesisPayload()));
	}
	catch (System.Exception e)
	{
	  logger(DEBUGGING) << "Connection to " << address << " failed: " << e.Message;
	}

	return ContextPtr();
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool fetchPeerList(std::unique_ptr<P2pContext> connection);

  // making and processing connections
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint getOutgoingConnectionsCount() const
  private uint getOutgoingConnectionsCount()
  {
	uint count = 0;

	foreach (var ctx in m_contexts)
	{
	  if (!ctx.isIncoming())
	  {
		++count;
	  }
	}

	return count;
  }
  private void makeExpectedConnectionsCount(Peerlist peerlist, uint connectionsCount)
  {
	while (getOutgoingConnectionsCount() < connectionsCount)
	{
	  if (peerlist.count() == 0)
	  {
		return;
	  }

	  if (!makeNewConnectionFromPeerlist(peerlist))
	  {
		break;
	  }
	}
  }
  private bool makeNewConnectionFromPeerlist(Peerlist peerlist)
  {
	uint peerIndex = new uint();
	PeerIndexGenerator idxGen = new PeerIndexGenerator(Math.Min<ulong>(peerlist.count() - 1, m_cfg.getPeerListConnectRange()));

	for (uint tryCount = 0; idxGen.generate(ref peerIndex) && tryCount < m_cfg.getPeerListGetTryCount(); ++tryCount)
	{
	  PeerlistEntry peer = new PeerlistEntry();
	  if (!peerlist.get(peer, peerIndex))
	  {
		logger(WARNING) << "Failed to get peer from list, idx = " << peerIndex;
		continue;
	  }

	  if (isPeerUsed(peer))
	  {
		continue;
	  }

	  logger(DEBUGGING) << "Selected peer: [" << peer.id << " " << peer.adr << "] last_seen: " << (peer.last_seen != null ? Common.timeIntervalToString(time(null) - peer.last_seen) : "never");

	  var conn = tryToConnectPeer(peer.adr);
	  if (conn.get())
	  {
		enqueueConnection(createProxy(std::move(conn)));
		return true;
	  }
	}

	return false;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void preprocessIncomingConnection(std::unique_ptr<P2pContext> ctx);
  private void enqueueConnection(std::unique_ptr<P2pConnectionProxy> proxy)
  {
	if (m_stopRequested)
	{
	  return; // skip operation
	}

	m_connectionQueue.AddLast(std::move(proxy));
	m_queueEvent.set();
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::unique_ptr<P2pConnectionProxy> createProxy(std::unique_ptr<P2pContext> ctx);
}

}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class PeerIndexGenerator
{

  public PeerIndexGenerator(uint maxIndex)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.maxIndex = maxIndex;
	  this.maxIndex.CopyFrom(maxIndex);
	  this.randCount = 0;
	Debug.Assert(maxIndex > 0);
  }

  public bool generate(ref uint num)
  {
	while (randCount < (maxIndex + 1) * 3)
	{
	  ++randCount;
	  var idx = getRandomIndex();
	  if (visited.count(idx) == 0)
	  {
		visited.Add(idx);
		num = idx;
		return true;
	  }
	}

	return false;
  }


  private uint getRandomIndex()
  {
	//divide by zero workaround
	if (maxIndex == 0)
	{
	  return 0;
	}

	uint x = Crypto.GlobalMembers.rand<uint>() % (maxIndex + 1);
	return (x * x * x) / (maxIndex * maxIndex);
  }

  private readonly uint maxIndex = new uint();
  private uint randCount = new uint();
  private SortedSet<uint> visited = new SortedSet<uint>();
}




//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
