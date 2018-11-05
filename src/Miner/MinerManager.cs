// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


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

using CryptoNote;
using Logging;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH

namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Dispatcher;
}

namespace Miner
{

public class MinerManager : System.IDisposable
{
  public MinerManager(System.Dispatcher dispatcher, CryptoNote.MiningConfig config, Logging.ILogger logger)
  {
	  this.m_dispatcher = new System.Dispatcher(dispatcher);
	  this.m_logger = new Logging.LoggerRef(logger, "MinerManager");
	  this.m_contextGroup = dispatcher;
	  this.m_config = new CryptoNote.MiningConfig(config);
	  this.m_miner = new CryptoNote.Miner(dispatcher, logger);
	  this.m_blockchainMonitor = new BlockchainMonitor(dispatcher, m_config.daemonHost, m_config.daemonPort, new uint(m_config.scanPeriod), logger);
	  this.m_eventOccurred = dispatcher;
	  this.m_httpEvent = dispatcher;
	  this.m_lastBlockTimestamp = 0;

	m_httpEvent.set();
  }
  public void Dispose()
  {
  }

  public void start()
  {
	m_logger.functorMethod(Logging.Level.DEBUGGING) << "starting";

	BlockMiningParameters @params = new BlockMiningParameters();
	for (;;)
	{
	  m_logger.functorMethod(Logging.Level.INFO) << "requesting mining parameters";

	  try
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: params = requestMiningParameters(m_dispatcher, m_config.daemonHost, m_config.daemonPort, m_config.miningAddress);
		@params.CopyFrom(requestMiningParameters(m_dispatcher, m_config.daemonHost, m_config.daemonPort, m_config.miningAddress));
	  }
	  catch (ConnectException e)
	  {
		m_logger.functorMethod(Logging.Level.WARNING) << "Couldn't connect to daemon: " << e.what();
		System.Timer timer = new System.Timer(m_dispatcher);
		timer.sleep(std::chrono.seconds(m_config.scanPeriod));
		continue;
	  }

	  adjustBlockTemplate(@params.blockTemplate);
	  break;
	}

	isRunning = true;

	startBlockchainMonitoring();
	std::thread reporter = new std::thread(std::bind(this.printHashRate, this));
	startMining(@params);

	eventLoop();
	isRunning = false;
  }

  private System.Dispatcher m_dispatcher;
  private Logging.LoggerRef m_logger = new Logging.LoggerRef();
  private System.ContextGroup m_contextGroup = new System.ContextGroup();
  private CryptoNote.MiningConfig m_config = new CryptoNote.MiningConfig();
  private CryptoNote.Miner m_miner = new CryptoNote.Miner();
  private BlockchainMonitor m_blockchainMonitor = new BlockchainMonitor();

  private System.Event m_eventOccurred = new System.Event();
  private System.Event m_httpEvent = new System.Event();
  private Queue<MinerEvent> m_events = new Queue<MinerEvent>();
  private bool isRunning;

  private CryptoNote.BlockTemplate m_minedBlock = new CryptoNote.BlockTemplate();

  private ulong m_lastBlockTimestamp = new ulong();

  private void eventLoop()
  {
	uint blocksMined = 0;

	for (;;)
	{
	  m_logger.functorMethod(Logging.Level.DEBUGGING) << "waiting for event";
	  MinerEvent event = waitEvent();

	  switch (event.type)
	  {
		case MinerEventType.BLOCK_MINED:
		{
		  m_logger.functorMethod(Logging.Level.DEBUGGING) << "got BLOCK_MINED event";
		  stopBlockchainMonitoring();

		  if (submitBlock(m_minedBlock, m_config.daemonHost, m_config.daemonPort))
		  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_lastBlockTimestamp = m_minedBlock.timestamp;
			m_lastBlockTimestamp.CopyFrom(m_minedBlock.timestamp);

			if (m_config.blocksLimit != 0 && ++blocksMined == m_config.blocksLimit)
			{
			  m_logger.functorMethod(Logging.Level.INFO) << "Miner mined requested " << m_config.blocksLimit << " blocks. Quitting";
			  return;
			}
		  }

		  BlockMiningParameters @params = requestMiningParameters(m_dispatcher, m_config.daemonHost, m_config.daemonPort, m_config.miningAddress);
		  adjustBlockTemplate(@params.blockTemplate);

		  startBlockchainMonitoring();
		  startMining(@params);
		  break;
		}

		case MinerEventType.BLOCKCHAIN_UPDATED:
		{
		  m_logger.functorMethod(Logging.Level.DEBUGGING) << "got BLOCKCHAIN_UPDATED event";
		  stopMining();
		  stopBlockchainMonitoring();
		  BlockMiningParameters @params = requestMiningParameters(m_dispatcher, m_config.daemonHost, m_config.daemonPort, m_config.miningAddress);
		  adjustBlockTemplate(@params.blockTemplate);

		  startBlockchainMonitoring();
		  startMining(@params);
		  break;
		}

		default:
		  Debug.Assert(false);
		  return;
	  }
	}
  }
  private MinerEvent waitEvent()
  {
	while (m_events.Count == 0)
	{
	  m_eventOccurred.wait();
	  m_eventOccurred.clear();
	}

	MinerEvent event = std::move(m_events.Peek());
	m_events.Dequeue();

	return event;
  }
  private void pushEvent(MinerEvent && event);
  private void printHashRate()
  {
	ulong last_hash_count = m_miner.getHashCount();

	while (isRunning)
	{
	  std::this_thread.sleep_for(std::chrono.seconds(60));
	  ulong current_hash_count = m_miner.getHashCount();
	  double hashes = (double)((current_hash_count - last_hash_count) / 60);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: last_hash_count = current_hash_count;
	  last_hash_count.CopyFrom(current_hash_count);
	  m_logger.functorMethod(Logging.Level.INFO, BRIGHT_GREEN) << "Mining at " << hashes << " H/s";
	}
  }

  private void startMining(CryptoNote.BlockMiningParameters @params)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_contextGroup.spawn([this, params]()
	m_contextGroup.spawn(() =>
	{
	  try
	  {
		m_minedBlock.CopyFrom(m_miner.mine(@params, new uint(m_config.threadCount)));
		pushEvent(BlockMinedEvent());
	  }
	  catch (System.InterruptedException)
	  {
	  }
	  catch (System.Exception e)
	  {
		m_logger.functorMethod(Logging.Level.ERROR) << "Miner context unexpectedly finished: " << e.Message;
	  }
	});
  }
  private void stopMining()
  {
	m_miner.stop();
  }

  private void startBlockchainMonitoring()
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_contextGroup.spawn([this]()
	m_contextGroup.spawn(() =>
	{
	  try
	  {
		m_blockchainMonitor.waitBlockchainUpdate();
		pushEvent(BlockchainUpdatedEvent());
	  }
	  catch (System.InterruptedException)
	  {
	  }
	  catch (System.Exception e)
	  {
		m_logger.functorMethod(Logging.Level.ERROR) << "BlockchainMonitor context unexpectedly finished: " << e.Message;
	  }
	});
  }
  private void stopBlockchainMonitoring()
  {
	m_blockchainMonitor.stop();
  }

  private bool submitBlock(BlockTemplate minedBlock, string daemonHost, ushort daemonPort)
  {
	CachedBlock cachedBlock = new CachedBlock(minedBlock);

	try
	{
	  HttpClient client = new HttpClient(m_dispatcher, daemonHost, new ushort(daemonPort));

	  COMMAND_RPC_SUBMITBLOCK.request request = new COMMAND_RPC_SUBMITBLOCK.request();
	  request.emplace_back(Common.toHex(CryptoNote.GlobalMembers.toBinaryArray(minedBlock)));

	  COMMAND_RPC_SUBMITBLOCK.response response = new COMMAND_RPC_SUBMITBLOCK.response();

	  System.EventLock lk = new System.EventLock(m_httpEvent);
	  JsonRpc.invokeJsonRpcCommand(client, "submitblock", request, response);

	  m_logger.functorMethod(Logging.Level.INFO) << "Block has been successfully submitted. Block hash: " << Common.GlobalMembers.podToHex(cachedBlock.getBlockHash());
	  return true;
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(Logging.Level.WARNING) << "Couldn't submit block: " << Common.GlobalMembers.podToHex(cachedBlock.getBlockHash()) << ", reason: " << e.Message;
	  return false;
	}
  }
  private BlockMiningParameters requestMiningParameters(System.Dispatcher dispatcher, string daemonHost, ushort daemonPort, string miningAddress)
  {
	try
	{
	  HttpClient client = new HttpClient(dispatcher, daemonHost, new ushort(daemonPort));

	  COMMAND_RPC_GETBLOCKTEMPLATE.request request = new COMMAND_RPC_GETBLOCKTEMPLATE.request();
	  request.wallet_address = miningAddress;
	  request.reserve_size = 0;

	  COMMAND_RPC_GETBLOCKTEMPLATE.response response = new COMMAND_RPC_GETBLOCKTEMPLATE.response();

	  System.EventLock lk = new System.EventLock(m_httpEvent);
	  JsonRpc.invokeJsonRpcCommand(client, "getblocktemplate", request, response);

	  if (response.status != DefineConstants.CORE_RPC_STATUS_OK)
	  {
		throw new System.Exception("Core responded with wrong status: " + response.status);
	  }

	  BlockMiningParameters @params = new BlockMiningParameters();
	  @params.difficulty = response.difficulty;

	  if (!CryptoNote.GlobalMembers.fromBinaryArray(ref @params.blockTemplate, Common.fromHex(response.blocktemplate_blob)))
	  {
		throw new System.Exception("Couldn't deserialize block template");
	  }

	  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Requested block template with previous block hash: " << Common.GlobalMembers.podToHex(@params.blockTemplate.previousBlockHash);
	  return @params;
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(Logging.Level.WARNING) << "Couldn't get block template: " << e.Message;
	  throw;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void adjustBlockTemplate(CryptoNote::BlockTemplate& blockTemplate) const
  private void adjustBlockTemplate(CryptoNote.BlockTemplate blockTemplate)
  {
	GlobalMembers.adjustMergeMiningTag(blockTemplate);

	if (m_config.firstBlockTimestamp == 0)
	{
	  //no need to fix timestamp
	  return;
	}

	if (m_lastBlockTimestamp == 0)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockTemplate.timestamp = m_config.firstBlockTimestamp;
	  blockTemplate.timestamp.CopyFrom(m_config.firstBlockTimestamp);
	}
	else if (m_lastBlockTimestamp != 0 && m_config.blockTimestampInterval != 0)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockTemplate.timestamp = m_lastBlockTimestamp + m_config.blockTimestampInterval;
	  blockTemplate.timestamp.CopyFrom(m_lastBlockTimestamp + m_config.blockTimestampInterval);
	}
  }
}

} //namespace Miner

namespace Miner
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

} //namespace Miner
