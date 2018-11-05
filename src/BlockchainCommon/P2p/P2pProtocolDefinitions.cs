using System.Collections.Generic;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




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

// new serialization

namespace CryptoNote
{

  public class network_config
  {
	public void serialize(ISerializer s)
	{
	  s.functorMethod(connections_count, "connections_count");
	  s.functorMethod(handshake_interval, "handshake_interval");
	  s.functorMethod(packet_max_size, "packet_max_size");
	  s.functorMethod(config_id, "config_id");
	}

	public uint connections_count;
	public uint connection_timeout;
	public uint ping_connection_timeout;
	public uint handshake_interval;
	public uint packet_max_size;
	public uint config_id;
	public uint send_peerlist_sz;
  }

  public class basic_node_data
  {
	public boost::uuids.uuid network_id = new boost::uuids.uuid();
	public byte version;
	public ulong local_time;
	public uint my_port;
	public ulong peer_id;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(network_id, "network_id");
	  if (s.type() == ISerializer.INPUT)
	  {
		version = 0;
	  }
	  s.functorMethod(version, "version");
	  s.functorMethod(peer_id, "peer_id");
	  s.functorMethod(local_time, "local_time");
	  s.functorMethod(my_port, "my_port");
	}
  }

  public class CORE_SYNC_DATA
  {
	public uint current_height;
	public Crypto.Hash top_id = new Crypto.Hash();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(current_height, "current_height");
	  s.functorMethod(top_id, "top_id");
	}
  }


  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class COMMAND_HANDSHAKE
  {
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum3:
	public enum AnonymousEnum3
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 1
	}

	public class request
	{
	  public basic_node_data node_data = new basic_node_data();
	  public CORE_SYNC_DATA payload_data = new CORE_SYNC_DATA();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(node_data, "node_data");
		s.functorMethod(payload_data, "payload_data");
	  }

	}

	public class response
	{
	  public basic_node_data node_data = new basic_node_data();
	  public CORE_SYNC_DATA payload_data = new CORE_SYNC_DATA();
	  public LinkedList<PeerlistEntry> local_peerlist = new LinkedList<PeerlistEntry>();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(node_data, "node_data");
		s.functorMethod(payload_data, "payload_data");
		CryptoNote.GlobalMembers.serializeAsBinary(local_peerlist, "local_peerlist", s.functorMethod);
	  }
	}
  }


  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class COMMAND_TIMED_SYNC
  {
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum4:
	public enum AnonymousEnum4
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 2
	}

	public class request
	{
	  public CORE_SYNC_DATA payload_data = new CORE_SYNC_DATA();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(payload_data, "payload_data");
	  }

	}

	public class response
	{
	  public ulong local_time;
	  public CORE_SYNC_DATA payload_data = new CORE_SYNC_DATA();
	  public LinkedList<PeerlistEntry> local_peerlist = new LinkedList<PeerlistEntry>();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(local_time, "local_time");
		s.functorMethod(payload_data, "payload_data");
		CryptoNote.GlobalMembers.serializeAsBinary(local_peerlist, "local_peerlist", s.functorMethod);
	  }
	}
  }

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/

  public class COMMAND_PING
  {
	/*
	  Used to make "callback" connection, to be sure that opponent node
	  have accessible connection point. Only other nodes can add peer to peerlist,
	  and ONLY in case when peer has accepted connection and answered to ping.
	*/
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum5:
	public enum AnonymousEnum5
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 3
	}

	public const string PING_OK_RESPONSE_STATUS_TEXT = "OK";

	public class request
	{
	  /*actually we don't need to send any real data*/
	  public void serialize(ISerializer s)
	  {
	  }
	}

	public class response
	{
	  public string status;
	  public ulong peer_id;

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(status, "status");
		s.functorMethod(peer_id, "peer_id");
	  }
	}
  }


#if ALLOW_DEBUG_COMMANDS
  //These commands are considered as insecure, and made in debug purposes for a limited lifetime.
  //Anyone who feel unsafe with this commands can disable the ALLOW_GET_STAT_COMMAND macro.

  public class proof_of_trust
  {
	public ulong peer_id;
	public ulong time;
	public Crypto.Signature sign = new Crypto.Signature();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(peer_id, "peer_id");
	  s.functorMethod(time, "time");
	  s.functorMethod(sign, "sign");
	}
  }

  public class COMMAND_REQUEST_STAT_INFO
  {
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum6:
	public enum AnonymousEnum6
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 4
	}

	public class request
	{
	  public proof_of_trust tr = new proof_of_trust();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(tr, "tr");
	  }
	}

	public class response
	{
	  public string version;
	  public string os_version;
	  public ulong connections_count;
	  public ulong incoming_connections_count;
	  public CoreStatistics payload_info = new CoreStatistics();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(version, "version");
		s.functorMethod(os_version, "os_version");
		s.functorMethod(connections_count, "connections_count");
		s.functorMethod(incoming_connections_count, "incoming_connections_count");
		s.functorMethod(payload_info, "payload_info");
	  }
	}
  }


  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class COMMAND_REQUEST_NETWORK_STATE
  {
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum7:
	public enum AnonymousEnum7
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 5
	}

	public class request
	{
	  public proof_of_trust tr = new proof_of_trust();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(tr, "tr");
	  }
	}

	public class response
	{
	  public LinkedList<PeerlistEntry> local_peerlist_white = new LinkedList<PeerlistEntry>();
	  public LinkedList<PeerlistEntry> local_peerlist_gray = new LinkedList<PeerlistEntry>();
	  public LinkedList<connection_entry> connections_list = new LinkedList<connection_entry>();
	  public ulong my_id;
	  public ulong local_time;

	  public void serialize(ISerializer s)
	  {
		CryptoNote.GlobalMembers.serializeAsBinary(local_peerlist_white, "local_peerlist_white", s.functorMethod);
		CryptoNote.GlobalMembers.serializeAsBinary(local_peerlist_gray, "local_peerlist_gray", s.functorMethod);
		CryptoNote.GlobalMembers.serializeAsBinary(connections_list, "connections_list", s.functorMethod);
		s.functorMethod(my_id, "my_id");
		s.functorMethod(local_time, "local_time");
	  }
	}
  }

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class COMMAND_REQUEST_PEER_ID
  {
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum8:
	public enum AnonymousEnum8
	{
		ID = DefineConstants.P2P_COMMANDS_POOL_BASE + 6
	}

	public class request
	{
	  public void serialize(ISerializer s)
	  {
	  }
	}

	public class response
	{
	  public ulong my_id;

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(my_id, "my_id");
	  }
	}
  }

#endif


}
