using System.Collections.Generic;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




namespace CryptoNote
{

public class NetNodeConfig
{
  public NetNodeConfig()
  {
	bindIp = "";
	bindPort = 0;
	externalPort = 0;
	allowLocalIp = false;
	hideMyPort = false;
	configFolder = Tools.getDefaultDataDirectory();
	testnet = false;
  }
  public bool init(string interface, int port, int external, bool localIp, bool hidePort, string dataDir, List<string> addPeers, List<string> addExclusiveNodes, List<string> addPriorityNodes, List<string> addSeedNodes)
  {
	bindIp = interface;
	bindPort = port;
	externalPort = external;
	allowLocalIp = localIp;
	hideMyPort = hidePort;
	configFolder = dataDir;
	p2pStateFilename = CryptoNote.parameters.P2P_NET_DATA_FILENAME;

	if (addPeers.Count > 0)
	{
	  if (!GlobalMembers.parsePeersAndAddToPeerListContainer(new List<string>(addPeers), peers))
	  {
		return false;
	  }
	}

	if (addExclusiveNodes.Count > 0)
	{
	  if (!GlobalMembers.parsePeersAndAddToNetworkContainer(new List<string>(addExclusiveNodes), exclusiveNodes))
	  {
		return false;
	  }
	}

	if (addPriorityNodes.Count > 0)
	{
	  if (!GlobalMembers.parsePeersAndAddToNetworkContainer(new List<string>(addPriorityNodes), priorityNodes))
	  {
		return false;
	  }
	}

	if (addSeedNodes.Count > 0)
	{
	  if (!GlobalMembers.parsePeersAndAddToNetworkContainer(new List<string>(addSeedNodes), seedNodes))
	  {
		return false;
	  }
	}

	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getP2pStateFilename() const
  public string getP2pStateFilename()
  {
	if (testnet)
	{
	  return "testnet_" + p2pStateFilename;
	}

	return p2pStateFilename;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getTestnet() const
  public bool getTestnet()
  {
	return testnet;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getBindIp() const
  public string getBindIp()
  {
	return bindIp;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getBindPort() const
  public uint16_t getBindPort()
  {
	return bindPort;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getExternalPort() const
  public uint16_t getExternalPort()
  {
	return externalPort;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getAllowLocalIp() const
  public bool getAllowLocalIp()
  {
	return allowLocalIp;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<PeerlistEntry> getPeers() const
  public List<PeerlistEntry> getPeers()
  {
	return peers;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getPriorityNodes() const
  public List<NetworkAddress> getPriorityNodes()
  {
	return priorityNodes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getExclusiveNodes() const
  public List<NetworkAddress> getExclusiveNodes()
  {
	return exclusiveNodes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getSeedNodes() const
  public List<NetworkAddress> getSeedNodes()
  {
	return seedNodes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getHideMyPort() const
  public bool getHideMyPort()
  {
	return hideMyPort;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getConfigFolder() const
  public string getConfigFolder()
  {
	return configFolder;
  }

  public void setP2pStateFilename(string filename)
  {
	p2pStateFilename = filename;
  }
  public void setTestnet(bool isTestnet)
  {
	testnet = isTestnet;
  }
  public void setBindIp(string ip)
  {
	bindIp = ip;
  }
  public void setBindPort(uint16_t port)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: bindPort = port;
	bindPort.CopyFrom(port);
  }
  public void setExternalPort(uint16_t port)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: externalPort = port;
	externalPort.CopyFrom(port);
  }
  public void setAllowLocalIp(bool allow)
  {
	allowLocalIp = allow;
  }
  public void setPeers(List<PeerlistEntry> peerList)
  {
	peers = new List<PeerlistEntry>(peerList);
  }
  public void setPriorityNodes(List<NetworkAddress> addresses)
  {
	priorityNodes = new List<NetworkAddress>(addresses);
  }
  public void setExclusiveNodes(List<NetworkAddress> addresses)
  {
	exclusiveNodes = new List<NetworkAddress>(addresses);
  }
  public void setSeedNodes(List<NetworkAddress> addresses)
  {
	seedNodes = new List<NetworkAddress>(addresses);
  }
  public void setHideMyPort(bool hide)
  {
	hideMyPort = hide;
  }
  public void setConfigFolder(string folder)
  {
	configFolder = folder;
  }

  private string bindIp;
  private uint16_t bindPort = new uint16_t();
  private uint16_t externalPort = new uint16_t();
  private bool allowLocalIp;
  private List<PeerlistEntry> peers = new List<PeerlistEntry>();
  private List<NetworkAddress> priorityNodes = new List<NetworkAddress>();
  private List<NetworkAddress> exclusiveNodes = new List<NetworkAddress>();
  private List<NetworkAddress> seedNodes = new List<NetworkAddress>();
  private bool hideMyPort;
  private string configFolder;
  private string p2pStateFilename;
  private bool testnet;
}

} //namespace nodetool


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
//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace



} //namespace nodetool
