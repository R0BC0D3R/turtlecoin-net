// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
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

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class P2pContext;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class P2pNode;

public class P2pConnectionProxy : IP2pConnection
{

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public P2pConnectionProxy(P2pContextOwner && ctx, IP2pNodeInternal node)
  {
	  this.m_contextOwner = new CryptoNote.P2pContextOwner(std::move(ctx));
	  this.m_context = new CryptoNote.P2pContext(m_contextOwner.get());
	  this.m_node = new CryptoNote.IP2pNodeInternal(node);
  }
  public new void Dispose()
  {
	m_context.stop();
	  base.Dispose();
  }

  public bool processIncomingHandshake()
  {
	LevinProtocol.Command cmd = new LevinProtocol.Command();
	if (!m_context.readCommand(cmd))
	{
	  throw new System.Exception("Connection unexpectedly closed");
	}

	if (cmd.command == COMMAND_HANDSHAKE.ID)
	{
	  handleHandshakeRequest(cmd);
	  return true;
	}

	if (cmd.command == COMMAND_PING.ID)
	{
	  COMMAND_PING.response resp = new CryptoNote.COMMAND_PING.response(PING_OK_RESPONSE_STATUS_TEXT, m_node.getPeerId());
	  m_context.writeMessage(makeReply(COMMAND_PING.ID, LevinProtocol.encode(resp), GlobalMembers.LEVIN_PROTOCOL_RETCODE_SUCCESS));
	  return false;
	}

	throw new System.Exception("Unexpected command: " + Convert.ToString(cmd.command));
  }

  // IP2pConnection
  public override void read(ref P2pMessage message)
  {
	if (m_readQueue.Count > 0)
	{
	  message = std::move(m_readQueue.Peek());
	  m_readQueue.Dequeue();
	  return;
	}

	for (;;)
	{
	  LevinProtocol.Command cmd = new LevinProtocol.Command();
	  if (!m_context.readCommand(cmd))
	  {
		throw InterruptedException();
	  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: message.type = cmd.command;
	  message.type.CopyFrom(cmd.command);

	  if (cmd.command == COMMAND_HANDSHAKE.ID)
	  {
		handleHandshakeResponse(cmd, message);
		break;
	  }
	  else if (cmd.command == COMMAND_TIMED_SYNC.ID)
	  {
		handleTimedSync(cmd);
	  }
	  else
	  {
		message.data = std::move(cmd.buf);
		break;
	  }
	}
  }
  public override void write(P2pMessage message)
  {
	if (message.type == COMMAND_HANDSHAKE.ID)
	{
	  writeHandshake(message);
	}
	else
	{
	  m_context.writeMessage(new P2pContext.Message(new P2pMessage(message), P2pContext.Message.NOTIFY));
	}
  }
  public override void ban()
  {
	// not implemented
  }
  public override void stop()
  {
	m_context.stop();
  }


  private void writeHandshake(P2pMessage message)
  {
	CORE_SYNC_DATA coreSync = new CORE_SYNC_DATA();
	LevinProtocol.decode(message.data, coreSync);

	if (m_context.isIncoming())
	{
	  // response
	  COMMAND_HANDSHAKE.response res = new COMMAND_HANDSHAKE.response();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.node_data = m_node.getNodeData();
	  res.node_data.CopyFrom(m_node.getNodeData());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.payload_data = coreSync;
	  res.payload_data.CopyFrom(coreSync);
	  res.local_peerlist = new LinkedList<PeerlistEntry>(m_node.getLocalPeerList());
	  m_context.writeMessage(makeReply(COMMAND_HANDSHAKE.ID, LevinProtocol.encode(res), GlobalMembers.LEVIN_PROTOCOL_RETCODE_SUCCESS));
	  m_node.tryPing(m_context);
	}
	else
	{
	  // request
	  COMMAND_HANDSHAKE.request req = new COMMAND_HANDSHAKE.request();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.node_data = m_node.getNodeData();
	  req.node_data.CopyFrom(m_node.getNodeData());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: req.payload_data = coreSync;
	  req.payload_data.CopyFrom(coreSync);
	  m_context.writeMessage(makeRequest(COMMAND_HANDSHAKE.ID, LevinProtocol.encode(req)));
	}
  }
  private void handleHandshakeRequest(LevinProtocol.Command cmd)
  {
	COMMAND_HANDSHAKE.request req = new COMMAND_HANDSHAKE.request();
	if (!LevinProtocol.decode<COMMAND_HANDSHAKE.request>(cmd.buf, req))
	{
	  throw new System.Exception("Failed to decode COMMAND_HANDSHAKE request");
	}

	m_node.handleNodeData(req.node_data, m_context);
	m_readQueue.Enqueue(new P2pMessage({cmd.command, LevinProtocol.encode(req.payload_data)})); // enqueue payload info
  }
  private void handleHandshakeResponse(LevinProtocol.Command cmd, P2pMessage message)
  {
	if (m_context.isIncoming())
	{
	  // handshake should be already received by P2pNode
	  throw new System.Exception("Unexpected COMMAND_HANDSHAKE from incoming connection");
	}

	COMMAND_HANDSHAKE.response res = new COMMAND_HANDSHAKE.response();
	if (!LevinProtocol.decode(cmd.buf, res))
	{
	  throw new System.Exception("Invalid handshake message format");
	}

	m_node.handleNodeData(res.node_data, m_context);
	m_node.handleRemotePeerList(res.local_peerlist, new ulong(res.node_data.local_time));

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: message.data = LevinProtocol::encode(res.payload_data);
	message.data.CopyFrom(LevinProtocol.encode(res.payload_data));
  }
  private void handleTimedSync(LevinProtocol.Command cmd)
  {
	if (cmd.isResponse)
	{
	  COMMAND_TIMED_SYNC.response res = new COMMAND_TIMED_SYNC.response();
	  LevinProtocol.decode(cmd.buf, res);
	  m_node.handleRemotePeerList(res.local_peerlist, new ulong(res.local_time));
	}
	else
	{
	  // we ignore information from the request
	  // COMMAND_TIMED_SYNC::request req;
	  // LevinProtocol::decode(cmd.buf, req);
	  COMMAND_TIMED_SYNC.response res = new COMMAND_TIMED_SYNC.response();

	  res.local_time = time(null);
	  res.local_peerlist = new LinkedList<PeerlistEntry>(m_node.getLocalPeerList());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.payload_data = m_node.getGenesisPayload();
	  res.payload_data.CopyFrom(m_node.getGenesisPayload());

	  m_context.writeMessage(makeReply(COMMAND_TIMED_SYNC.ID, LevinProtocol.encode(res), GlobalMembers.LEVIN_PROTOCOL_RETCODE_SUCCESS));
	}
  }

  private Queue<P2pMessage> m_readQueue = new Queue<P2pMessage>();
  private P2pContextOwner m_contextOwner = new P2pContextOwner();
  private P2pContext m_context;
  private IP2pNodeInternal m_node;
}

}

