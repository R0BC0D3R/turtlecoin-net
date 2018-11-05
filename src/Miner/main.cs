// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

public class BlockMiningParameters
{
  public BlockTemplate blockTemplate = new BlockTemplate();
  public ulong difficulty = new ulong();
}

public class Miner : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  Miner(System::Dispatcher dispatcher, Logging::ILogger logger);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  BlockTemplate mine(BlockMiningParameters blockMiningParameters, uint threadCount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ulong getHashCount();

  //NOTE! this is blocking method
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void stop();

  private System.Dispatcher m_dispatcher;
  private System.Event m_miningStopped = new System.Event();

  private enum MiningState : ushort
  {
	  MINING_STOPPED,
	  BLOCK_FOUND,
	  MINING_IN_PROGRESS
  }
  private std::atomic<MiningState> m_state = new std::atomic<MiningState>();

  private List<std::unique_ptr<System.RemoteContext>> m_workers = new List<std::unique_ptr<System.RemoteContext>>();

  private BlockTemplate m_block = new BlockTemplate();
  private ulong m_hash_count = new ulong();
  private object m_hashes_mutex = new object();

  private Logging.LoggerRef m_logger = new Logging.LoggerRef();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void runWorkers(BlockMiningParameters blockMiningParameters, uint threadCount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void workerFunc(BlockTemplate blockTemplate, ulong difficulty, uint nonceStep);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool setStateBlockFound();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void incrementHashCount();
}

} //namespace CryptoNote

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.


namespace Miner
{

public enum MinerEventType: ushort
{
  BLOCK_MINED,
  BLOCKCHAIN_UPDATED,
}

public class MinerEvent
{
  public MinerEventType type;
}

} //namespace Miner

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.


// Copyright (c) 2011-present, Facebook, Inc.  All rights reserved.
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. An additional grant
// of patent rights can be found in the PATENTS file in the same directory.


// Do not use these. We made the mistake of declaring macros starting with
// double underscore. Now we have to live with our choice. We'll deprecate these
// at some point
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH



namespace CryptoNote
{

public class MiningConfig
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  MiningConfig();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void parse(int argc, string[] argv);

  public string miningAddress;
  public string daemonAddress;
  public string daemonHost;
  public int daemonPort;
  public uint threadCount = new uint();
  public uint scanPeriod = new uint();
  public ushort logLevel = new ushort();
  public uint blocksLimit = new uint();
  public ulong firstBlockTimestamp = new ulong();
  public long blockTimestampInterval = new long();
  public bool help;
  public bool version;
}

} //namespace CryptoNote


namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Dispatcher;
}

namespace Miner
{

public class MinerManager : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  MinerManager(System::Dispatcher dispatcher, CryptoNote::MiningConfig config, Logging::ILogger logger);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void start();

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

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void eventLoop();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  MinerEvent waitEvent();
  private void pushEvent(MinerEvent && event);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void printHashRate();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void startMining(CryptoNote::BlockMiningParameters @params);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void stopMining();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void startBlockchainMonitoring();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void stopBlockchainMonitoring();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool submitBlock(CryptoNote::BlockTemplate minedBlock, string daemonHost, ushort daemonPort);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  CryptoNote::BlockMiningParameters requestMiningParameters(System::Dispatcher dispatcher, string daemonHost, ushort daemonPort, string miningAddress);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void adjustBlockTemplate(CryptoNote::BlockTemplate& blockTemplate) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void adjustBlockTemplate(CryptoNote::BlockTemplate blockTemplate);
}

} //namespace Miner