// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
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

public class P2pContext : System.IDisposable
{

  public class Message : P2pMessage
  {
	public enum Type
	{
	  NOTIFY,
	  REQUEST,
	  REPLY
	}

	public Type messageType;
	public uint32_t returnCode = new uint32_t();

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public Message(P2pMessage && msg, Type messageType, uint32_t returnCode = 0)
	{
		this.messageType = new CryptoNote.P2pContext.Message.Type(messageType);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.returnCode = returnCode;
		this.returnCode.CopyFrom(returnCode);
	  type = msg.type;
	  data = std::move(msg.data);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t size() const
	public size_t size()
	{
	  return data.size();
	}
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public P2pContext(Dispatcher dispatcher, TcpConnection && conn, bool isIncoming, NetworkAddress remoteAddress, std::chrono.nanoseconds timedSyncInterval, CORE_SYNC_DATA timedSyncData)
  {
	  this.incoming = isIncoming;
	  this.remoteAddress = new NetworkAddress(remoteAddress);
	  this.dispatcher = dispatcher;
	  this.contextGroup = dispatcher;
	  this.timeStarted = Clock.now();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.timedSyncInterval = timedSyncInterval;
	  this.timedSyncInterval.CopyFrom(timedSyncInterval);
	  this.timedSyncData = new CryptoNote.CORE_SYNC_DATA(timedSyncData);
	  this.timedSyncTimer = dispatcher;
	  this.timedSyncFinished = dispatcher;
	  this.connection = new System.TcpConnection(std::move(conn));
	  this.writeEvent = dispatcher;
	  this.readEvent = dispatcher;
	writeEvent.set();
	readEvent.set();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: lastReadTime = timeStarted;
	lastReadTime.CopyFrom(timeStarted); // use current time
	contextGroup.spawn(std::bind(this.timedSyncLoop, this));
  }
  public void Dispose()
  {
	stop();
	// wait for timedSyncLoop finish
	timedSyncFinished.wait();
	// ensure that all read/write operations completed
	readEvent.wait();
	writeEvent.wait();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getPeerId() const
  public uint64_t getPeerId()
  {
	return peerId;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getPeerPort() const
  public uint16_t getPeerPort()
  {
	return peerPort;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const NetworkAddress& getRemoteAddress() const
  public NetworkAddress getRemoteAddress()
  {
	return remoteAddress;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isIncoming() const
  public bool isIncoming()
  {
	return incoming;
  }

  public void setPeerInfo(uint8_t protocolVersion, uint64_t id, uint16_t port)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: version = protocolVersion;
	version.CopyFrom(protocolVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: peerId = id;
	peerId.CopyFrom(id);
	if (isIncoming())
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: peerPort = port;
	  peerPort.CopyFrom(port);
	}
  }
  public bool readCommand(LevinProtocol.Command cmd)
  {
	if (stopped)
	{
	  throw InterruptedException();
	}

	EventLock lk = new EventLock(readEvent);
	bool result = new LevinProtocol(connection).readCommand(cmd);
	lastReadTime = Clock.now();
	return result;
  }
  public void writeMessage(Message msg)
  {
	if (stopped)
	{
	  throw InterruptedException();
	}

	EventLock lk = new EventLock(writeEvent);
	LevinProtocol proto = new LevinProtocol(connection);

	switch (msg.messageType)
	{
	case P2pContext.Message.NOTIFY:
	  proto.sendMessage(new uint32_t(msg.type), msg.data, false);
	  break;
	case P2pContext.Message.REQUEST:
	  proto.sendMessage(new uint32_t(msg.type), msg.data, true);
	  break;
	case P2pContext.Message.REPLY:
	  proto.sendReply(new uint32_t(msg.type), msg.data, new uint32_t(msg.returnCode));
	  break;
	}
  }

  public void start()
  {
	// stub for OperationTimeout class
  }
  public void stop()
  {
	if (!stopped)
	{
	  stopped = true;
	  contextGroup.interrupt();
	}
  }


  private uint8_t version = 0;
  private readonly bool incoming;
  private readonly NetworkAddress remoteAddress = new NetworkAddress();
  private uint64_t peerId = 0;
  private uint16_t peerPort = 0;

  private System.Dispatcher dispatcher;
  private System.ContextGroup contextGroup = new System.ContextGroup();
  private readonly std::chrono.steady_clock.time_point timeStarted = new std::chrono.steady_clock.time_point();
  private bool stopped = false;
  private std::chrono.steady_clock.time_point lastReadTime = new std::chrono.steady_clock.time_point();

  // timed sync info
  private readonly std::chrono.nanoseconds timedSyncInterval = new std::chrono.nanoseconds();
  private readonly CORE_SYNC_DATA timedSyncData;
  private System.Timer timedSyncTimer = new System.Timer();
  private System.Event timedSyncFinished = new System.Event();

  private System.TcpConnection connection = new System.TcpConnection();
  private System.Event writeEvent = new System.Event();
  private System.Event readEvent = new System.Event();

  private void timedSyncLoop()
  {
	// construct message
	P2pContext.Message timedSyncMessage = new Message(new P2pMessage({COMMAND_TIMED_SYNC.ID, LevinProtocol.encode(new COMMAND_TIMED_SYNC.request({timedSyncData}))}), P2pContext.Message.REQUEST);

	while (!stopped)
	{
	  try
	  {
		timedSyncTimer.sleep(timedSyncInterval);

		OperationTimeout<P2pContext> timeout = new OperationTimeout<P2pContext>(dispatcher, this, timedSyncInterval);
		writeMessage(timedSyncMessage);

		// check if we had read operation in given time interval
		if ((lastReadTime + timedSyncInterval * 2) < Clock.now())
		{
		  stop();
		  break;
		}
	  }
	  catch (InterruptedException)
	  {
		// someone stopped us
	  }
	  catch (System.Exception)
	  {
		stop(); // stop connection on write error
		break;
	  }
	}

	timedSyncFinished.set();
  }
}

}

