// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.

using System;
using System.Collections.Generic;

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
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Core;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class NodeServer;
}

public class DaemonCommandsHandler
{
  public DaemonCommandsHandler(CryptoNote.Core core, CryptoNote.NodeServer srv, Logging.LoggerManager log, CryptoNote.RpcServer prpc_server)
  {
	  this.m_core = new CryptoNote.Core(core);
	  this.m_srv = new CryptoNote.NodeServer(srv);
	  this.logger = new Logging.LoggerRef(log, "daemon");
	  this.m_logManager = log;
	  this.m_prpc_server = prpc_server;
	m_consoleHandler.setHandler("exit", boost::bind(this.exit, this, _1), "Shutdown the daemon");
	m_consoleHandler.setHandler("help", boost::bind(this.help, this, _1), "Show this help");
	m_consoleHandler.setHandler("print_pl", boost::bind(this.print_pl, this, _1), "Print peer list");
	m_consoleHandler.setHandler("print_cn", boost::bind(this.print_cn, this, _1), "Print connections");
	m_consoleHandler.setHandler("print_bc", boost::bind(this.print_bc, this, _1), "Print blockchain info in a given blocks range, print_bc <begin_height> [<end_height>]");
	//m_consoleHandler.setHandler("print_bci", boost::bind(&DaemonCommandsHandler::print_bci, this, _1));
	//m_consoleHandler.setHandler("print_bc_outs", boost::bind(&DaemonCommandsHandler::print_bc_outs, this, _1));
	m_consoleHandler.setHandler("print_block", boost::bind(this.print_block, this, _1), "Print block, print_block <block_hash> | <block_height>");
	m_consoleHandler.setHandler("print_tx", boost::bind(this.print_tx, this, _1), "Print transaction, print_tx <transaction_hash>");
	m_consoleHandler.setHandler("print_pool", boost::bind(this.print_pool, this, _1), "Print transaction pool (long format)");
	m_consoleHandler.setHandler("print_pool_sh", boost::bind(this.print_pool_sh, this, _1), "Print transaction pool (short format)");
	m_consoleHandler.setHandler("set_log", boost::bind(this.set_log, this, _1), "set_log <level> - Change current log level, <level> is a number 0-4");
	m_consoleHandler.setHandler("status", boost::bind(this.status, this, _1), "Show daemon status");
  }

  public bool start_handling()
  {
	m_consoleHandler.start();
	return true;
  }

  public void stop_handling()
  {
	m_consoleHandler.stop();
  }


  private Common.ConsoleHandler m_consoleHandler = new Common.ConsoleHandler();
  private CryptoNote.Core m_core;
  private CryptoNote.NodeServer m_srv;
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private Logging.LoggerManager m_logManager;
  private CryptoNote.RpcServer m_prpc_server;


  //--------------------------------------------------------------------------------
  private string get_commands_str()
  {
	std::stringstream ss = new std::stringstream();
	ss << CryptoNote.CRYPTONOTE_NAME << " v" << PROJECT_VERSION_LONG << std::endl;
	ss << "Commands: " << std::endl;
	string usage = m_consoleHandler.getUsage();
	boost::replace_all(usage, "\n", "\n  ");
	usage = usage.Insert(0, "  ");
	ss << usage << std::endl;
	return ss.str();
  }

  //--------------------------------------------------------------------------------
  private bool print_block_by_height(uint32_t height)
  {
	if (height - 1 > m_core.getTopBlockIndex() != null)
	{
	  Console.Write("block wasn't found. Current block chain height: ");
	  Console.Write(m_core.getTopBlockIndex() + 1);
	  Console.Write(", requested: ");
	  Console.Write(height);
	  Console.Write("\n");
	  return false;
	}

	var hash = m_core.getBlockHashByIndex(height - 1);
	Console.Write("block_id: ");
	Console.Write(hash);
	Console.Write("\n");
	GlobalMembers.print_as_json(m_core.getBlockByIndex(height - 1));

	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_block_by_hash(string arg)
  {
	Crypto.Hash block_hash = new Crypto.Hash();
	if (!parse_hash256(arg, block_hash))
	{
	  return false;
	}

	if (m_core.hasBlock(block_hash))
	{
	  GlobalMembers.print_as_json(m_core.getBlockByHash(block_hash));
	}
	else
	{
	  Console.Write("block wasn't found: ");
	  Console.Write(arg);
	  Console.Write("\n");
	  return false;
	}

	return true;
  }


  //--------------------------------------------------------------------------------
  private bool exit(List<string> args)
  {
	m_consoleHandler.requestStop();
	m_srv.sendStopSignal();
	return true;
  }

  //--------------------------------------------------------------------------------
  private bool help(List<string> args)
  {
	Console.Write(get_commands_str());
	Console.Write("\n");
	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_pl(List<string> args)
  {
	m_srv.log_peerlist();
	return true;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool show_hr(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool hide_hr(ClassicVector<string> args);
  //--------------------------------------------------------------------------------
  private bool print_bc_outs(List<string> args)
  {
	if (args.Count != 1)
	{
	  Console.Write("need file path as parameter");
	  Console.Write("\n");
	  return true;
	}

	//TODO m_core.print_blockchain_outs(args[0]);
	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_cn(List<string> args)
  {
	m_srv.get_payload_object().log_connections();
	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_bc(List<string> args)
  {
	  if (args.Count == 0)
	  {
		  Console.Write("need block index parameter");
		  Console.Write("\n");
		  return false;
	  }

	  uint32_t start_index = 0;
	  uint32_t end_index = 0;
	  uint32_t end_block_parametr = m_core.getTopBlockIndex();

	  if (!Common.GlobalMembers.fromString(args[0], start_index))
	  {
		  Console.Write("wrong starter block index parameter");
		  Console.Write("\n");
		  return false;
	  }

	  if (args.Count > 1 && !Common.GlobalMembers.fromString(args[1], end_index))
	  {
		  Console.Write("wrong end block index parameter");
		  Console.Write("\n");
		  return false;
	  }

	  if (end_index == 0)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: end_index = start_index;
		  end_index.CopyFrom(start_index);
	  }

	  if (end_index > end_block_parametr)
	  {
		  Console.Write("end block index parameter shouldn't be greater than ");
		  Console.Write(end_block_parametr);
		  Console.Write("\n");
		  return false;
	  }

	  if (end_index < start_index)
	  {
		  Console.Write("end block index should be greater than or equal to starter block index");
		  Console.Write("\n");
		  return false;
	  }

	  CryptoNote.COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.request req = new CryptoNote.COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.request();
	  CryptoNote.COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.response res = new CryptoNote.COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.response();
	  CryptoNote.JsonRpc.JsonRpcError error_resp = new CryptoNote.JsonRpc.JsonRpcError();

	  req.start_height = start_index;
	  req.end_height = end_index;

	  // TODO: implement m_is_rpc handling like in monero?
	  if (!m_prpc_server.on_get_block_headers_range(req, res, error_resp) || res.status != DefineConstants.CORE_RPC_STATUS_OK)
	  {
		  // TODO res.status handling
		  Console.Write("Response status not CORE_RPC_STATUS_OK");
		  Console.Write("\n");
		  return false;
	  }

	  CryptoNote.Currency currency = m_core.getCurrency();

	  bool first = true;
	  foreach (CryptoNote  in :block_header_response & header : res.headers)
	  {
		  if (!first)
		  {
			  Console.Write("\n");
			  first = false;
		  }

		  Console.Write("height: ");
		  Console.Write(header.height);
		  Console.Write(", timestamp: ");
		  Console.Write(header.timestamp);
		  Console.Write(", difficulty: ");
		  Console.Write(header.difficulty);
		  Console.Write(", size: ");
		  Console.Write(header.block_size);
		  Console.Write(", transactions: ");
		  Console.Write(header.num_txes);
		  Console.Write("\n");
		  Console.Write("major version: ");
		  Console.Write((uint)header.major_version);
		  Console.Write(", minor version: ");
		  Console.Write((uint)header.minor_version);
		  Console.Write("\n");
		  Console.Write("block id: ");
		  Console.Write(header.hash);
		  Console.Write(", previous block id: ");
		  Console.Write(header.prev_hash);
		  Console.Write("\n");
		  Console.Write("difficulty: ");
		  Console.Write(header.difficulty);
		  Console.Write(", nonce: ");
		  Console.Write(header.nonce);
		  Console.Write(", reward: ");
		  Console.Write(currency.formatAmount(header.reward));
		  Console.Write("\n");
	  }

	  return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_bci(List<string> args)
  {
	//TODO m_core.print_blockchain_index();
	return true;
  }
  private bool set_log(List<string> args)
  {
	if (args.Count != 1)
	{
	  Console.Write("use: set_log <log_level_number_0-4>");
	  Console.Write("\n");
	  return true;
	}

	uint16_t l = 0;
	if (!Common.GlobalMembers.fromString(args[0], l))
	{
	  Console.Write("wrong number format, use: set_log <log_level_number_0-4>");
	  Console.Write("\n");
	  return true;
	}

	++l;

	if (l > Logging.Level.TRACE)
	{
	  Console.Write("wrong number range, use: set_log <log_level_number_0-4>");
	  Console.Write("\n");
	  return true;
	}

	m_logManager.setMaxLevel((Logging.Level)l);
	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_block(List<string> args)
  {
	if (args.Count == 0)
	{
	  Console.Write("expected: print_block (<block_hash> | <block_height>)");
	  Console.Write("\n");
	  return true;
	}

	string arg = args[0];
	try
	{
	  uint32_t height = boost::lexical_cast<uint32_t>(arg);
	  print_block_by_height(new uint32_t(height));
	}
	catch (boost::bad_lexical_cast)
	{
	  print_block_by_hash(arg);
	}

	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_tx(List<string> args)
  {
	if (args.Count == 0)
	{
	  Console.Write("expected: print_tx <transaction hash>");
	  Console.Write("\n");
	  return true;
	}

	string str_hash = args[0];
	Crypto.Hash tx_hash = new Crypto.Hash();
	if (!parse_hash256(str_hash, tx_hash))
	{
	  return true;
	}

	List<Crypto.Hash> tx_ids = new List<Crypto.Hash>();
	tx_ids.Add(tx_hash);
	List<List<uint8_t>> txs = new List<List<uint8_t>>();
	List<Crypto.Hash> missed_ids = new List<Crypto.Hash>();
	m_core.getTransactions(tx_ids, txs, missed_ids);

	if (1 == txs.Count)
	{
	  CryptoNote.CachedTransaction tx = new CryptoNote.CachedTransaction(new List<List<uint8_t>>(txs[0]));
	  GlobalMembers.print_as_json(tx.getTransaction());
	}
	else
	{
	  Console.Write("transaction wasn't found: <");
	  Console.Write(str_hash);
	  Console.Write('>');
	  Console.Write("\n");
	}

	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_pool(List<string> args)
  {
	Console.Write("Pool state: \n");
	var pool = m_core.getPoolTransactions();

	foreach (var tx in pool)
	{
	  CryptoNote.CachedTransaction ctx = new CryptoNote.CachedTransaction(new Transaction(tx));
	  Console.Write(GlobalMembers.printTransactionFullInfo(ctx));
	  Console.Write("\n");
	}

	Console.Write("\n");

	return true;
  }
  //--------------------------------------------------------------------------------
  private bool print_pool_sh(List<string> args)
  {
	Console.Write("Pool short state: \n");
	var pool = m_core.getPoolTransactions();

	foreach (var tx in pool)
	{
	  CryptoNote.CachedTransaction ctx = new CryptoNote.CachedTransaction(new Transaction(tx));
	  Console.Write(GlobalMembers.printTransactionShortInfo(ctx));
	  Console.Write("\n");
	}

	Console.Write("\n");

	return true;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool start_mining(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool stop_mining(ClassicVector<string> args);
  //--------------------------------------------------------------------------------
  private bool status(List<string> args)
  {
	CryptoNote.COMMAND_RPC_GET_INFO.request ireq = new CryptoNote.COMMAND_RPC_GET_INFO.request();
	CryptoNote.COMMAND_RPC_GET_INFO.response iresp = new CryptoNote.COMMAND_RPC_GET_INFO.response();

	if (!m_prpc_server.on_get_info(ireq, iresp) || iresp.status != DefineConstants.CORE_RPC_STATUS_OK)
	{
	  Console.Write("Problem retrieving information from RPC server.");
	  Console.Write("\n");
	  return false;
	}

	Console.Write(Common.get_status_string(iresp));
	Console.Write("\n");

	return true;
  }
}


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>