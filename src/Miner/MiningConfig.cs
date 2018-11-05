﻿using System;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.


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
  public MiningConfig()
  {
	  this.help = false;
	  this.version = false;
  }

  public void parse(int argc, string[] argv)
  {
	cxxopts.Options options = new cxxopts.Options(argv[0], getProjectCLIHeader());

	options.add_options("Core")("help", "Display this help message", cxxopts.value<bool>(help).implicit_value("true"))("version", "Output software version information", cxxopts.value<bool>(version).default_value("false").implicit_value("true"));

	options.add_options("Daemon")("daemon-address", "The daemon [host:port] combination to use for node operations. This option overrides --daemon-host and --daemon-rpc-port", cxxopts.value<string>(daemonAddress), "<host:port>")("daemon-host", "The daemon host to use for node operations", cxxopts.value<string>(daemonHost).default_value("127.0.0.1"), "<host>")("daemon-rpc-port", "The daemon RPC port to use for node operations", cxxopts.value<int>(daemonPort).default_value(Convert.ToString(CryptoNote.RPC_DEFAULT_PORT)), "#")("scan-time", "Blockchain polling interval (seconds). How often miner will check the Blockchain for updates", cxxopts.value<size_t>(scanPeriod).default_value("30"), "#");

	options.add_options("Mining")("address", "The valid CryptoNote miner's address", cxxopts.value<string>(miningAddress), "<address>")("block-timestamp-interval", "Timestamp incremental step for each subsequent block. May be set only if --first-block-timestamp has been set.", cxxopts.value<int64_t>(blockTimestampInterval).default_value("0"), "#")("first-block-timestamp", "Set timestamp to the first mined block. 0 means leave timestamp unchanged", cxxopts.value<uint64_t>(firstBlockTimestamp).default_value("0"), "#")("limit", "Mine this exact quantity of blocks and then stop. 0 means no limit", cxxopts.value<size_t>(blocksLimit).default_value("0"), "#")("log-level", "Specify log level. Must be 0 - 5", cxxopts.value<uint8_t>(logLevel).default_value("1"), "#")("threads", "The mining threads count. Must not exceed hardware capabilities.", cxxopts.value<size_t>(threadCount).default_value(Convert.ToString(GlobalMembers.CONCURRENCY_LEVEL)), "#");

	try
	{
	  var result = options.parse(argc, argv);
	}
	catch (cxxopts.OptionException e)
	{
	  Console.Write("Error: Unable to parse command line argument options: ");
	  Console.Write(e.what());
	  Console.Write("\n");
	  Console.Write("\n");
	  Console.Write(options.help({}));
	  Console.Write("\n");
	  Environment.Exit(1);
	}

	if (help) // Do we want to display the help message?
	{
	  Console.Write(options.help({}));
	  Console.Write("\n");
	  Environment.Exit(0);
	}
	else if (version) // Do we want to display the software version?
	{
	  Console.Write(getProjectCLIHeader());
	  Console.Write("\n");
	  Environment.Exit(0);
	}

	if (string.IsNullOrEmpty(miningAddress))
	{
	  throw new System.Exception("Specify --address option");
	}

	if (!string.IsNullOrEmpty(daemonAddress))
	{
	  if (!GlobalMembers.parseDaemonAddressFromString(ref daemonHost, ref daemonPort, daemonAddress))
	  {
		throw new System.Exception("Could not parse --daemon-address option");
	  }
	}

	if (threadCount == 0 || threadCount > GlobalMembers.CONCURRENCY_LEVEL)
	{
	  throw new System.Exception("--threads option must be 1.." + Convert.ToString(GlobalMembers.CONCURRENCY_LEVEL));
	}

	if (scanPeriod == 0)
	{
	  throw new System.Exception("--scan-time must not be zero");
	}

	if (logLevel > (uint8_t)Logging.Level.TRACE)
	{
	  throw new System.Exception("--log-level value is too big");
	}

	if (firstBlockTimestamp == 0 && blockTimestampInterval != 0)
	{
	  throw new System.Exception("If you specify --block-timestamp-interval you must also specify --first-block-timestamp");
	}
  }

  public string miningAddress;
  public string daemonAddress;
  public string daemonHost;
  public int daemonPort;
  public size_t threadCount = new size_t();
  public size_t scanPeriod = new size_t();
  public uint8_t logLevel = new uint8_t();
  public size_t blocksLimit = new size_t();
  public uint64_t firstBlockTimestamp = new uint64_t();
  public int64_t blockTimestampInterval = new int64_t();
  public bool help;
  public bool version;
}

} //namespace CryptoNote





//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}