using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.





//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{

public class BlockMiningParameters
{
  public BlockTemplate blockTemplate = new BlockTemplate();
  public uint64_t difficulty = new uint64_t();
}

public class Miner : System.IDisposable
{
  public Miner(System.Dispatcher dispatcher, Logging.ILogger logger)
  {
	  this.m_dispatcher = dispatcher;
	  this.m_miningStopped = dispatcher;
	  this.m_state = MiningState.MINING_STOPPED;
	  this.m_logger = new Logging.LoggerRef(logger, "Miner");
  }
  public void Dispose()
  {
	Debug.Assert(m_state != MiningState.MINING_IN_PROGRESS);
  }

  public BlockTemplate mine(BlockMiningParameters blockMiningParameters, size_t threadCount)
  {
	if (threadCount == 0)
	{
	  throw new System.Exception("Miner requires at least one thread");
	}

	if (m_state == MiningState.MINING_IN_PROGRESS)
	{
	  throw new System.Exception("Mining is already in progress");
	}

	m_state = MiningState.MINING_IN_PROGRESS;
	m_miningStopped.clear();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: runWorkers(blockMiningParameters, threadCount);
	runWorkers(new CryptoNote.BlockMiningParameters(blockMiningParameters), new size_t(threadCount));

	Debug.Assert(m_state != MiningState.MINING_IN_PROGRESS);
	if (m_state == MiningState.MINING_STOPPED)
	{
	  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Mining has been stopped";
	  throw System.InterruptedException();
	}

	Debug.Assert(m_state == MiningState.BLOCK_FOUND);
	return m_block;
  }
  public uint64_t getHashCount()
  {
	lock (m_hashes_mutex)
	{
		return m_hash_count;
	}
  }

  //NOTE! this is blocking method
  public void stop()
  {
	MiningState state = MiningState.MINING_IN_PROGRESS;

	if (m_state.compare_exchange_weak(state, MiningState.MINING_STOPPED))
	{
	  m_miningStopped.wait();
	  m_miningStopped.clear();
	}
  }

  private System.Dispatcher m_dispatcher;
  private System.Event m_miningStopped = new System.Event();

  private enum MiningState : uint8_t
  {
	  MINING_STOPPED,
	  BLOCK_FOUND,
	  MINING_IN_PROGRESS
  }
  private std::atomic<MiningState> m_state = new std::atomic<MiningState>();

  private List<std::unique_ptr<System.RemoteContext>> m_workers = new List<std::unique_ptr<System.RemoteContext>>();

  private BlockTemplate m_block = new BlockTemplate();
  private uint64_t m_hash_count = new uint64_t();
  private object m_hashes_mutex = new object();

  private Logging.LoggerRef m_logger = new Logging.LoggerRef();

  private void runWorkers(BlockMiningParameters blockMiningParameters, size_t threadCount)
  {
	Debug.Assert(threadCount > 0);

	m_logger.functorMethod(Logging.Level.INFO) << "Starting mining for difficulty " << blockMiningParameters.difficulty;

	try
	{
	  blockMiningParameters.blockTemplate.nonce = Crypto.GlobalMembers.rand<uint32_t>();

	  for (size_t i = 0; i < threadCount; ++i)
	  {
		m_workers.emplace_back(std::unique_ptr<System.RemoteContext> (new System.RemoteContext(m_dispatcher, std::bind(this.workerFunc, this, blockMiningParameters.blockTemplate, blockMiningParameters.difficulty, (uint32_t)threadCount))));
		m_logger.functorMethod(Logging.Level.INFO) << "Thread " << i << " started at nonce: " << blockMiningParameters.blockTemplate.nonce;

		blockMiningParameters.blockTemplate.nonce++;
	  }

	  m_workers.Clear();

	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(Logging.Level.ERROR) << "Error occurred during mining: " << e.Message;
	  m_state = MiningState.MINING_STOPPED;
	}

	m_miningStopped.set();
  }
  private void workerFunc(BlockTemplate blockTemplate, uint64_t difficulty, uint32_t nonceStep)
  {
	try
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: BlockTemplate block = blockTemplate;
	  BlockTemplate block = new BlockTemplate(blockTemplate);

	  while (m_state == MiningState.MINING_IN_PROGRESS)
	  {
		CachedBlock cachedBlock = new CachedBlock(block);
		Crypto.Hash hash = cachedBlock.getBlockLongHash();
		if (check_hash(hash, difficulty))
		{
		  m_logger.functorMethod(Logging.Level.INFO) << "Found block for difficulty " << difficulty;

		  if (!setStateBlockFound())
		  {
			m_logger.functorMethod(Logging.Level.DEBUGGING) << "block is already found or mining stopped";
			return;
		  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_block = block;
		  m_block.CopyFrom(block);
		  return;
		}

		incrementHashCount();
		block.nonce += nonceStep;
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(Logging.Level.ERROR) << "Miner got error: " << e.Message;
	  m_state = MiningState.MINING_STOPPED;
	}
  }
  private bool setStateBlockFound()
  {
	var state = m_state.load();

	for (;;)
	{
	  switch (state)
	  {
		case MiningState.BLOCK_FOUND:
		  return false;

		case MiningState.MINING_IN_PROGRESS:
		  if (m_state.compare_exchange_weak(state, MiningState.BLOCK_FOUND))
		  {
			return true;
		  }
		  break;

		case MiningState.MINING_STOPPED:
		  return false;

		default:
		  Debug.Assert(false);
		  return false;
	  }
	}
  }
  private void incrementHashCount()
  {
	lock (m_hashes_mutex)
	{
		m_hash_count++;
	}
  }
}

} //namespace CryptoNote



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


