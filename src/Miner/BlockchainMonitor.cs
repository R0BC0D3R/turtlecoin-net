// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

public class BlockchainMonitor
{
  public BlockchainMonitor(System.Dispatcher dispatcher, string daemonHost, uint16_t daemonPort, size_t pollingInterval, Logging.ILogger logger)
  {
	  this.m_dispatcher = dispatcher;
	  this.m_daemonHost = daemonHost;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_daemonPort = daemonPort;
	  this.m_daemonPort.CopyFrom(daemonPort);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_pollingInterval = pollingInterval;
	  this.m_pollingInterval.CopyFrom(pollingInterval);
	  this.m_stopped = false;
	  this.m_httpEvent = dispatcher;
	  this.m_sleepingContext = dispatcher;
	  this.m_logger = new Logging.LoggerRef(logger, "BlockchainMonitor");

	m_httpEvent.set();
  }

  public void waitBlockchainUpdate()
  {
	m_logger.functorMethod(Logging.Level.DEBUGGING) << "Waiting for blockchain updates";
	m_stopped = false;

	Crypto.Hash lastBlockHash = requestLastBlockHash();

	while (!m_stopped)
	{
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_sleepingContext.spawn([this]()
	  m_sleepingContext.spawn(() =>
	  {
		System.Timer timer = new System.Timer(m_dispatcher);
		timer.sleep(std::chrono.seconds(m_pollingInterval));
	  });

	  m_sleepingContext.wait();

	  if (lastBlockHash != requestLastBlockHash())
	  {
		m_logger.functorMethod(Logging.Level.DEBUGGING) << "Blockchain has been updated";
		break;
	  }
	}

	if (m_stopped)
	{
	  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Blockchain monitor has been stopped";
	  throw System.InterruptedException();
	}
  }
  public void stop()
  {
	m_logger.functorMethod(Logging.Level.DEBUGGING) << "Sending stop signal to blockchain monitor";
	m_stopped = true;

	m_sleepingContext.interrupt();
	m_sleepingContext.wait();
  }
  private System.Dispatcher m_dispatcher;
  private string m_daemonHost;
  private uint16_t m_daemonPort = new uint16_t();
  private size_t m_pollingInterval = new size_t();
  private bool m_stopped;
  private System.Event m_httpEvent = new System.Event();
  private System.ContextGroup m_sleepingContext = new System.ContextGroup();

  private Logging.LoggerRef m_logger = new Logging.LoggerRef();

  private Crypto.Hash requestLastBlockHash()
  {
	m_logger.functorMethod(Logging.Level.DEBUGGING) << "Requesting last block hash";

	try
	{
	  CryptoNote.HttpClient client = new CryptoNote.HttpClient(m_dispatcher, m_daemonHost, new uint16_t(m_daemonPort));

	  CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.request request = new CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.request();
	  CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.response response = new CryptoNote.COMMAND_RPC_GET_LAST_BLOCK_HEADER.response();

	  System.EventLock lk = new System.EventLock(m_httpEvent);
	  CryptoNote.JsonRpc.invokeJsonRpcCommand(client, "getlastblockheader", request, response);

	  if (response.status != DefineConstants.CORE_RPC_STATUS_OK)
	  {
		throw new System.Exception("Core responded with wrong status: " + response.status);
	  }

	  Crypto.Hash blockHash = new Crypto.Hash();
	  if (!Common.GlobalMembers.podFromHex(response.block_header.hash, blockHash))
	  {
		throw new System.Exception("Couldn't parse block hash: " + response.block_header.hash);
	  }

	  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Last block hash: " << Common.GlobalMembers.podToHex(blockHash);

	  return blockHash;
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(Logging.Level.ERROR) << "Failed to request last block hash: " << e.Message;
	  throw;
	}
  }
}




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
