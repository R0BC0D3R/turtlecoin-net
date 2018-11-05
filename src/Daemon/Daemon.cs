// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// Copyright (c) 2018, The Karai Developers
//
// Please see the included LICENSE file for more information.


// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




using json = nlohmann.json;

using JsonValue = Common.JsonValue;
using CryptoNote;
using Logging;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace
  public class DaemonConfiguration
  {
	public string dataDirectory;
	public string logFile;
	public string feeAddress;
	public string rpcInterface;
	public string p2pInterface;
	public string checkPoints;

	public List<string> peers = new List<string>();
	public List<string> priorityNodes = new List<string>();
	public List<string> exclusiveNodes = new List<string>();
	public List<string> seedNodes = new List<string>();
	public List<string> enableCors = new List<string>();

	public int logLevel;
	public int feeAmount;
	public int rpcPort;
	public int p2pPort;
	public int p2pExternalPort;
	public int dbThreads;
	public int dbMaxOpenFiles;
	public int dbWriteBufferSize;
	public int dbReadCacheSize;

	public bool noConsole;
	public bool enableBlockExplorer;
	public bool localIp;
	public bool hideMyPort;

	public string configFile;
	public string outputFile;
	public List<string> genesisAwardAddresses = new List<string>();
	public bool help;
	public bool version;
	public bool osVersion;
	public bool printGenesisTx;
	public bool dumpConfig;
  }
// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.



// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// Copyright (c) 2018, The Karai Developers
//
// Please see the included LICENSE file for more information.


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






namespace CryptoNote
{

public abstract class HttpServer
{


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  HttpServer(System::Dispatcher dispatcher, Logging::ILogger log);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void start(string address, uint16_t port);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void stop();

  public abstract void processRequest(HttpRequest request, HttpResponse response);


  protected System.Dispatcher m_dispatcher;


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void acceptLoop();
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void connectionHandler(System::TcpConnection&& conn);

  private System.ContextGroup workingContextGroup = new System.ContextGroup();
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private System.TcpListener m_listener = new System.TcpListener();
  private HashSet<System.TcpConnection> m_connections = new HashSet<System.TcpConnection>();
}

}



// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// Copyright (c) 2018, The Karai Developers
// 
// Please see the included LICENSE file for more information.


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



// ISerializer-based serialization
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




namespace CryptoNote
{

public abstract class ISerializer : System.IDisposable
{

  public enum SerializerType
  {
	INPUT,
	OUTPUT
  }

  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual SerializerType type() const = 0;
  public abstract SerializerType type();

  public abstract bool beginObject(Common.StringView name);
  public abstract void endObject();
  public abstract bool beginArray(uint64_t size, Common.StringView name);
  public abstract void endArray();

  public static abstract bool operator ()(uint8_t value, Common.StringView name);
  public static abstract bool operator ()(int16_t value, Common.StringView name);
  public static abstract bool operator ()(uint16_t value, Common.StringView name);
  public static abstract bool operator ()(int32_t value, Common.StringView name);
  public static abstract bool operator ()(uint32_t value, Common.StringView name);
  public static abstract bool operator ()(int64_t value, Common.StringView name);
  public static abstract bool operator ()(uint64_t value, Common.StringView name);
  public static abstract bool operator ()(ref double value, Common.StringView name);
  public static abstract bool operator ()(ref bool value, Common.StringView name);
  public static abstract bool operator ()(string value, Common.StringView name);

  // read/write binary block
  public abstract bool binary(object value, uint64_t size, Common.StringView name);
  public abstract bool binary(string value, Common.StringView name);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static bool functorMethod<T>(T value, Common.StringView name)
  {
	return CryptoNote.GlobalMembers.serialize(value, new Common.StringView(name), this.functorMethod);
  }
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

}

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


  /************************************************************************/
  /*                                                                      */
  /************************************************************************/

  //just to keep backward compatibility with BlockCompleteEntry serialization
  public class RawBlockLegacy
  {
	public BinaryArray block = new BinaryArray();
	public List<BinaryArray> transactions = new List<BinaryArray>();
  }

  public class NOTIFY_NEW_BLOCK_request
  {
	public RawBlockLegacy b = new RawBlockLegacy();
	public uint32_t current_blockchain_height = new uint32_t();
	public uint32_t hop = new uint32_t();
  }

  public class NOTIFY_NEW_BLOCK
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 1;
  }

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class NOTIFY_NEW_TRANSACTIONS_request
  {
	public List<BinaryArray> txs = new List<BinaryArray>();
  }

  public class NOTIFY_NEW_TRANSACTIONS
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 2;
  }

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class NOTIFY_REQUEST_GET_OBJECTS_request
  {
	public List<Crypto.Hash> txs = new List<Crypto.Hash>();
	public List<Crypto.Hash> blocks = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  CryptoNote.GlobalMembers.serializeAsBinary(txs, "txs", s.functorMethod);
	  CryptoNote.GlobalMembers.serializeAsBinary(blocks, "blocks", s.functorMethod);
	}
  }

  public class NOTIFY_REQUEST_GET_OBJECTS
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 3;
  }

  public class NOTIFY_RESPONSE_GET_OBJECTS_request
  {
	public List<string> txs = new List<string>();
	public List<RawBlockLegacy> blocks = new List<RawBlockLegacy>();
	public List<Crypto.Hash> missed_ids = new List<Crypto.Hash>();
	public uint32_t current_blockchain_height = new uint32_t();
  }

  public class NOTIFY_RESPONSE_GET_OBJECTS
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 4;
  }

  public class NOTIFY_REQUEST_CHAIN
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 6;

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public class request
	{
	  public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //IDs of the first 10 blocks are sequential, next goes with pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block

	  public void serialize(ISerializer s)
	  {
		CryptoNote.GlobalMembers.serializeAsBinary(block_ids, "block_ids", s.functorMethod);
	  }
	}
  }

  public class NOTIFY_RESPONSE_CHAIN_ENTRY_request
  {
	public uint32_t start_height = new uint32_t();
	public uint32_t total_height = new uint32_t();
	public List<Crypto.Hash> m_block_ids = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(start_height, "start_height");
	  s.functorMethod(total_height, "total_height");
	  CryptoNote.GlobalMembers.serializeAsBinary(m_block_ids, "m_block_ids", s.functorMethod);
	}
  }

  public class NOTIFY_RESPONSE_CHAIN_ENTRY
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 7;
  }

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class NOTIFY_REQUEST_TX_POOL_request
  {
	public List<Crypto.Hash> txs = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  CryptoNote.GlobalMembers.serializeAsBinary(txs, "txs", s.functorMethod);
	}
  }

  public class NOTIFY_REQUEST_TX_POOL
  {
	public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 8;
  }
}


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





namespace CryptoNote
{

public enum TransactionRemoveReason : uint8_t
{
  INCLUDED_IN_BLOCK = 0,
  TIMEOUT = 1
}

public class TransactionOutputDetails
{
  public TransactionOutput output = new TransactionOutput();
  public uint64_t globalIndex = new uint64_t();
}

public class TransactionOutputReferenceDetails
{
  public Crypto.Hash transactionHash = new Crypto.Hash();
  public size_t number = new size_t();
}

public class BaseInputDetails
{
  public BaseInput input = new BaseInput();
  public uint64_t amount = new uint64_t();
}

public class KeyInputDetails
{
  public KeyInput input = new KeyInput();
  public uint64_t mixin = new uint64_t();
  public TransactionOutputReferenceDetails output = new TransactionOutputReferenceDetails();
}



public class TransactionExtraDetails
{
  public Crypto.PublicKey publicKey = new Crypto.PublicKey();
  public BinaryArray nonce = new BinaryArray();
  public BinaryArray raw = new BinaryArray();
}

public class TransactionDetails
{
  public Crypto.Hash hash = new Crypto.Hash();
  public uint64_t size = 0;
  public uint64_t fee = 0;
  public uint64_t totalInputsAmount = 0;
  public uint64_t totalOutputsAmount = 0;
  public uint64_t mixin = 0;
  public uint64_t unlockTime = 0;
  public uint64_t timestamp = 0;
  public Crypto.Hash paymentId = new Crypto.Hash();
  public bool hasPaymentId = false;
  public bool inBlockchain = false;
  public Crypto.Hash blockHash = new Crypto.Hash();
  public uint32_t blockIndex = 0;
  public TransactionExtraDetails extra = new TransactionExtraDetails();
  public List<List<Crypto.Signature>> signatures = new List<List<Crypto.Signature>>();
  public List<TransactionInputDetails> inputs = new List<TransactionInputDetails>();
  public List<TransactionOutputDetails> outputs = new List<TransactionOutputDetails>();
}

public class BlockDetails
{
  public uint8_t majorVersion = 0;
  public uint8_t minorVersion = 0;
  public uint64_t timestamp = 0;
  public Crypto.Hash prevBlockHash = new Crypto.Hash();
  public uint32_t nonce = 0;
  public bool isAlternative = false;
  public uint32_t index = 0;
  public Crypto.Hash hash = new Crypto.Hash();
  public uint64_t difficulty = 0;
  public uint64_t reward = 0;
  public uint64_t baseReward = 0;
  public uint64_t blockSize = 0;
  public uint64_t transactionsCumulativeSize = 0;
  public uint64_t alreadyGeneratedCoins = 0;
  public uint64_t alreadyGeneratedTransactions = 0;
  public uint64_t sizeMedian = 0;
  public double penalty = 0.0;
  public uint64_t totalFeeAmount = 0;
  public List<TransactionDetails> transactions = new List<TransactionDetails>();
}

}


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






namespace CryptoNote
{
//-----------------------------------------------

public class EMPTY_STRUCT
{
  public void serialize(ISerializer s)
  {
  }
}

public class STATUS_STRUCT
{
  public string status;

  public void serialize(ISerializer s)
  {
	s.functorMethod(status, "status");
  }
}

public class COMMAND_RPC_GET_HEIGHT
{

  public class response
  {
	public uint64_t height = new uint64_t();
	public uint32_t network_height = new uint32_t();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(height, "height");
	  s.functorMethod(network_height, "network_height");
	  s.functorMethod(status, "status");
	}
  }
}

public class COMMAND_RPC_GET_BLOCKS_FAST
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */

	public void serialize(ISerializer s)
	{
	  s.functorMethod(block_ids, "block_ids");
	}
  }

  public class response
  {
	public List<RawBlock> blocks = new List<RawBlock>();
	public uint64_t start_height = new uint64_t();
	public uint64_t current_height = new uint64_t();
	public string status;
  }
}
//-----------------------------------------------
public class COMMAND_RPC_GET_TRANSACTIONS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<string> txs_hashes = new List<string>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(txs_hashes, "txs_hashes");
	}
  }

  public class response
  {
	public List<string> txs_as_hex = new List<string>(); //transactions blobs as hex
	public List<string> missed_tx = new List<string>(); //not found transactions
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(txs_as_hex, "txs_as_hex");
	  s.functorMethod(missed_tx, "missed_tx");
	  s.functorMethod(status, "status");
	}
  }
}
//-----------------------------------------------
public class COMMAND_RPC_GET_POOL_CHANGES
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public Crypto.Hash tailBlockId = new Crypto.Hash();
	public List<Crypto.Hash> knownTxsIds = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(tailBlockId, "tailBlockId");
	  s.functorMethod(knownTxsIds, "knownTxsIds");
	}
  }

  public class response
  {
	public bool isTailBlockActual;
	public List<BinaryArray> addedTxs = new List<BinaryArray>(); // Added transactions blobs
	public List<Crypto.Hash> deletedTxsIds = new List<Crypto.Hash>(); // IDs of not found transactions
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(isTailBlockActual, "isTailBlockActual");
	  s.functorMethod(addedTxs, "addedTxs");
	  s.functorMethod(deletedTxsIds, "deletedTxsIds");
	  s.functorMethod(status, "status");
	}
  }
}

public class COMMAND_RPC_GET_POOL_CHANGES_LITE
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public Crypto.Hash tailBlockId = new Crypto.Hash();
	public List<Crypto.Hash> knownTxsIds = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(tailBlockId, "tailBlockId");
	  s.functorMethod(knownTxsIds, "knownTxsIds");
	}
  }

  public class response
  {
	public bool isTailBlockActual;
	public List<TransactionPrefixInfo> addedTxs = new List<TransactionPrefixInfo>(); // Added transactions blobs
	public List<Crypto.Hash> deletedTxsIds = new List<Crypto.Hash>(); // IDs of not found transactions
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(isTailBlockActual, "isTailBlockActual");
	  s.functorMethod(addedTxs, "addedTxs");
	  s.functorMethod(deletedTxsIds, "deletedTxsIds");
	  s.functorMethod(status, "status");
	}
  }
}

//-----------------------------------------------
public class COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public Crypto.Hash txid = new Crypto.Hash();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(txid, "txid");
	}
  }

  public class response
  {
	public List<uint64_t> o_indexes = new List<uint64_t>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(o_indexes, "o_indexes");
	  s.functorMethod(status, "status");
	}
  }
}
//-----------------------------------------------
public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_request
{
  public List<uint64_t> amounts = new List<uint64_t>();
  public uint16_t outs_count = new uint16_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(amounts, "amounts");
	s.functorMethod(outs_count, "outs_count");
  }
}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push, 1)
public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry
{
  public uint32_t global_amount_index = new uint32_t();
  public Crypto.PublicKey out_key = new Crypto.PublicKey();

  public void serialize(ISerializer s)
  {
	s.functorMethod(global_amount_index, "global_amount_index");
	s.functorMethod(out_key, "out_key");
  }
}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(pop)

public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount
{
  public uint64_t amount = new uint64_t();
  public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry> outs = new List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry>();

  public void serialize(ISerializer s)
  {
	s.functorMethod(amount, "amount");
	s.functorMethod(outs, "outs");
  }
}

public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_response
{
  public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> outs = new List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>();
  public string status;

  public void serialize(ISerializer s)
  {
	s.functorMethod(outs, "outs");
	s.functorMethod(status, "status");
  }
}

public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS
{

}

//-----------------------------------------------
public class COMMAND_RPC_SEND_RAW_TX
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public string tx_as_hex;

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public request()
	{
	}
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	request(Transaction UnnamedParameter);

	public void serialize(ISerializer s)
	{
	  s.functorMethod(tx_as_hex, "tx_as_hex");
	}
  }

  public class response
  {
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	}
  }
}
//-----------------------------------------------
public class COMMAND_RPC_START_MINING
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public string miner_address;
	public uint64_t threads_count = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(miner_address, "miner_address");
	  s.functorMethod(threads_count, "threads_count");
	}
  }

  public class response
  {
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	}
  }
}
//-----------------------------------------------
public class COMMAND_RPC_GET_INFO
{

  public class response
  {
	public string status;
	public uint64_t height = new uint64_t();
	public uint64_t difficulty = new uint64_t();
	public uint64_t tx_count = new uint64_t();
	public uint64_t tx_pool_size = new uint64_t();
	public uint64_t alt_blocks_count = new uint64_t();
	public uint64_t outgoing_connections_count = new uint64_t();
	public uint64_t incoming_connections_count = new uint64_t();
	public uint64_t white_peerlist_size = new uint64_t();
	public uint64_t grey_peerlist_size = new uint64_t();
	public uint32_t last_known_block_index = new uint32_t();
	public uint32_t network_height = new uint32_t();
	public List<uint64_t> upgrade_heights = new List<uint64_t>();
	public uint64_t supported_height = new uint64_t();
	public uint32_t hashrate = new uint32_t();
	public uint8_t major_version = new uint8_t();
	public uint8_t minor_version = new uint8_t();
	public string version;
	public uint64_t start_time = new uint64_t();
	public bool synced;
	public bool testnet;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(height, "height");
	  s.functorMethod(difficulty, "difficulty");
	  s.functorMethod(tx_count, "tx_count");
	  s.functorMethod(tx_pool_size, "tx_pool_size");
	  s.functorMethod(alt_blocks_count, "alt_blocks_count");
	  s.functorMethod(outgoing_connections_count, "outgoing_connections_count");
	  s.functorMethod(incoming_connections_count, "incoming_connections_count");
	  s.functorMethod(white_peerlist_size, "white_peerlist_size");
	  s.functorMethod(grey_peerlist_size, "grey_peerlist_size");
	  s.functorMethod(last_known_block_index, "last_known_block_index");
	  s.functorMethod(network_height, "network_height");
	  s.functorMethod(upgrade_heights, "upgrade_heights");
	  s.functorMethod(supported_height, "supported_height");
	  s.functorMethod(hashrate, "hashrate");
	  s.functorMethod(major_version, "major_version");
	  s.functorMethod(minor_version, "minor_version");
	  s.functorMethod(start_time, "start_time");
	  s.functorMethod(synced, "synced");
	  s.functorMethod(testnet, "testnet");
	  s.functorMethod(version, "version");
	}
  }
}

//-----------------------------------------------
public class COMMAND_RPC_STOP_MINING
{
}

//-----------------------------------------------
public class COMMAND_RPC_STOP_DAEMON
{
}

//
public class COMMAND_RPC_GETBLOCKCOUNT
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public uint64_t count = new uint64_t();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(count, "count");
	  s.functorMethod(status, "status");
	}
  }
}

public class COMMAND_RPC_GETBLOCKHASH
{
}

public class COMMAND_RPC_GETBLOCKTEMPLATE
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public uint64_t reserve_size = new uint64_t(); //max 255 bytes
	public string wallet_address;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(reserve_size, "reserve_size");
	  s.functorMethod(wallet_address, "wallet_address");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public uint64_t difficulty = new uint64_t();
	public uint32_t height = new uint32_t();
	public uint64_t reserved_offset = new uint64_t();
	public string blocktemplate_blob;
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(difficulty, "difficulty");
	  s.functorMethod(height, "height");
	  s.functorMethod(reserved_offset, "reserved_offset");
	  s.functorMethod(blocktemplate_blob, "blocktemplate_blob");
	  s.functorMethod(status, "status");
	}
  }
}

public class COMMAND_RPC_GET_CURRENCY_ID
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public string currency_id_blob;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(currency_id_blob, "currency_id_blob");
	}
  }
}

public class COMMAND_RPC_SUBMITBLOCK
{
}

public class block_header_response
{
  public uint8_t major_version = new uint8_t();
  public uint8_t minor_version = new uint8_t();
  public uint64_t timestamp = new uint64_t();
  public string prev_hash;
  public uint32_t nonce = new uint32_t();
  public bool orphan_status;
  public uint32_t height = new uint32_t();
  public uint32_t depth = new uint32_t();
  public string hash;
  public uint64_t difficulty = new uint64_t();
  public uint64_t reward = new uint64_t();
  public uint32_t num_txes = new uint32_t();
  public uint64_t block_size = new uint64_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(major_version, "major_version");
	s.functorMethod(minor_version, "minor_version");
	s.functorMethod(timestamp, "timestamp");
	s.functorMethod(prev_hash, "prev_hash");
	s.functorMethod(nonce, "nonce");
	s.functorMethod(orphan_status, "orphan_status");
	s.functorMethod(height, "height");
	s.functorMethod(depth, "depth");
	s.functorMethod(hash, "hash");
	s.functorMethod(difficulty, "difficulty");
	s.functorMethod(reward, "reward");
	s.functorMethod(num_txes, "num_txes");
	s.functorMethod(block_size, "block_size");
  }
}

public class BLOCK_HEADER_RESPONSE
{
  public string status;
  public block_header_response block_header = new block_header_response();

  public void serialize(ISerializer s)
  {
	s.functorMethod(block_header, "block_header");
	s.functorMethod(status, "status");
  }
}


public class COMMAND_RPC_GET_BLOCK_HEADERS_RANGE
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public class request
	{
		public uint64_t start_height = new uint64_t();
		public uint64_t end_height = new uint64_t();

		public void serialize(ISerializer s)
		{
			s.functorMethod(start_height, "start_height");
			s.functorMethod(end_height, "end_height");
		}
		/*BEGIN_KV_SERIALIZE_MAP()
		KV_SERIALIZE(start_height)
		KV_SERIALIZE(end_height)
		END_KV_SERIALIZE_MAP()*/
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public class response
	{
		public string status;
		public List<block_header_response> headers = new List<block_header_response>();
		public bool untrusted;

		public void serialize(ISerializer s)
		{
			s.functorMethod(status, "status");
			s.functorMethod(headers, "headers");
			s.functorMethod(untrusted, "untrusted");
		}
		/*BEGIN_KV_SERIALIZE_MAP()
		KV_SERIALIZE(status)
		KV_SERIALIZE(headers)
		KV_SERIALIZE(untrusted)
		END_KV_SERIALIZE_MAP()*/
	}
}

public class f_transaction_short_response
{
  public string hash;
  public uint64_t fee = new uint64_t();
  public uint64_t amount_out = new uint64_t();
  public uint64_t size = new uint64_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(hash, "hash");
	s.functorMethod(fee, "fee");
	s.functorMethod(amount_out, "amount_out");
	s.functorMethod(size, "size");
  }
}

public class f_transaction_details_response
{
  public string hash;
  public uint64_t size = new uint64_t();
  public string paymentId;
  public uint64_t mixin = new uint64_t();
  public uint64_t fee = new uint64_t();
  public uint64_t amount_out = new uint64_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(hash, "hash");
	s.functorMethod(size, "size");
	s.functorMethod(paymentId, "paymentId");
	s.functorMethod(mixin, "mixin");
	s.functorMethod(fee, "fee");
	s.functorMethod(amount_out, "amount_out");
  }
}

public class f_block_short_response
{
  public uint64_t difficulty = new uint64_t();
  public uint64_t timestamp = new uint64_t();
  public uint32_t height = new uint32_t();
  public string hash;
  public uint64_t tx_count = new uint64_t();
  public uint64_t cumul_size = new uint64_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(difficulty, "difficulty");
	s.functorMethod(timestamp, "timestamp");
	s.functorMethod(height, "height");
	s.functorMethod(hash, "hash");
	s.functorMethod(cumul_size, "cumul_size");
	s.functorMethod(tx_count, "tx_count");
  }
}

public class f_block_details_response
{
  public uint8_t major_version = new uint8_t();
  public uint8_t minor_version = new uint8_t();
  public uint64_t timestamp = new uint64_t();
  public string prev_hash;
  public uint32_t nonce = new uint32_t();
  public bool orphan_status;
  public uint32_t height = new uint32_t();
  public uint64_t depth = new uint64_t();
  public string hash;
  public uint64_t difficulty = new uint64_t();
  public uint64_t reward = new uint64_t();
  public uint64_t blockSize = new uint64_t();
  public uint64_t sizeMedian = new uint64_t();
  public uint64_t effectiveSizeMedian = new uint64_t();
  public uint64_t transactionsCumulativeSize = new uint64_t();
  public string alreadyGeneratedCoins;
  public uint64_t alreadyGeneratedTransactions = new uint64_t();
  public uint64_t baseReward = new uint64_t();
  public double penalty;
  public uint64_t totalFeeAmount = new uint64_t();
  public List<f_transaction_short_response> transactions = new List<f_transaction_short_response>();

  public void serialize(ISerializer s)
  {
	s.functorMethod(major_version, "major_version");
	s.functorMethod(minor_version, "minor_version");
	s.functorMethod(timestamp, "timestamp");
	s.functorMethod(prev_hash, "prev_hash");
	s.functorMethod(nonce, "nonce");
	s.functorMethod(orphan_status, "orphan_status");
	s.functorMethod(height, "height");
	s.functorMethod(depth, "depth");
	s.functorMethod(hash, "hash");
	s.functorMethod(difficulty, "difficulty");
	s.functorMethod(reward, "reward");
	s.functorMethod(blockSize, "blockSize");
	s.functorMethod(sizeMedian, "sizeMedian");
	s.functorMethod(effectiveSizeMedian, "effectiveSizeMedian");
	s.functorMethod(transactionsCumulativeSize, "transactionsCumulativeSize");
	s.functorMethod(alreadyGeneratedCoins, "alreadyGeneratedCoins");
	s.functorMethod(alreadyGeneratedTransactions, "alreadyGeneratedTransactions");
	s.functorMethod(baseReward, "baseReward");
	s.functorMethod(penalty, "penalty");
	s.functorMethod(transactions, "transactions");
	s.functorMethod(totalFeeAmount, "totalFeeAmount");
  }
}
public class COMMAND_RPC_GET_LAST_BLOCK_HEADER
{
}

public class COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public string hash;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(hash, "hash");
	}
  }

}

public class COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public uint64_t height = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(height, "height");
	}
  }

}

public class F_COMMAND_RPC_GET_BLOCKS_LIST
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public uint64_t height = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(height, "height");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<f_block_short_response> blocks = new List<f_block_short_response>(); //transactions blobs as hex
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(blocks, "blocks");
	  s.functorMethod(status, "status");
	}
  }
}

public class F_COMMAND_RPC_GET_BLOCK_DETAILS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public string hash;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(hash, "hash");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public f_block_details_response block = new f_block_details_response();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(block, "block");
	  s.functorMethod(status, "status");
	}
  }
}

public class F_COMMAND_RPC_GET_TRANSACTION_DETAILS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public string hash;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(hash, "hash");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public Transaction tx = new Transaction();
	public f_transaction_details_response txDetails = new f_transaction_details_response();
	public f_block_short_response block = new f_block_short_response();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(tx, "tx");
	  s.functorMethod(txDetails, "txDetails");
	  s.functorMethod(block, "block");
	  s.functorMethod(status, "status");
	}
  }
}

public class F_COMMAND_RPC_GET_POOL
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<f_transaction_short_response> transactions = new List<f_transaction_short_response>(); //transactions blobs as hex
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(transactions, "transactions");
	  s.functorMethod(status, "status");
	}
  }
}
public class COMMAND_RPC_QUERY_BLOCKS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */
	public uint64_t timestamp = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(block_ids, "block_ids");
	  s.functorMethod(timestamp, "timestamp");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public string status;
	public uint64_t start_height = new uint64_t();
	public uint64_t current_height = new uint64_t();
	public uint64_t full_offset = new uint64_t();
	public List<BlockFullInfo> items = new List<BlockFullInfo>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(start_height, "start_height");
	  s.functorMethod(current_height, "current_height");
	  s.functorMethod(full_offset, "full_offset");
	  s.functorMethod(items, "items");
	}
  }
}

public class COMMAND_RPC_QUERY_BLOCKS_LITE
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<Crypto.Hash> blockIds = new List<Crypto.Hash>();
	public uint64_t timestamp = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(blockIds, "blockIds");
	  s.functorMethod(timestamp, "timestamp");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public string status;
	public uint64_t startHeight = new uint64_t();
	public uint64_t currentHeight = new uint64_t();
	public uint64_t fullOffset = new uint64_t();
	public List<BlockShortInfo> items = new List<BlockShortInfo>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(startHeight, "startHeight");
	  s.functorMethod(currentHeight, "currentHeight");
	  s.functorMethod(fullOffset, "fullOffset");
	  s.functorMethod(items, "items");
	}
  }
}

public class COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<uint32_t> blockHeights = new List<uint32_t>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(blockHeights, "blockHeights");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<BlockDetails> blocks = new List<BlockDetails>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(blocks, "blocks");
	}
  }
}

public class COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(blockHashes, "blockHashes");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<BlockDetails> blocks = new List<BlockDetails>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(blocks, "blocks");
	}
  }
}

public class COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public uint32_t blockHeight = new uint32_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(blockHeight, "blockHeight");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public BlockDetails block = new BlockDetails();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(block, "block");
	}
  }
}

public class COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public uint64_t timestampBegin = new uint64_t();
	public uint64_t secondsCount = new uint64_t();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(timestampBegin, "timestampBegin");
	  s.functorMethod(secondsCount, "secondsCount");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(blockHashes, "blockHashes");
	}
  }
}

public class COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public Crypto.Hash paymentId = new Crypto.Hash();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(paymentId, "paymentId");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(transactionHashes, "transactionHashes");
	}
  }
}

public class COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES
{
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class request
  {
	public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(transactionHashes, "transactionHashes");
	}
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public List<TransactionDetails> transactions = new List<TransactionDetails>();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(transactions, "transactions");
	}
  }
}

public class COMMAND_RPC_GET_PEERS
{
  // TODO: rename peers to white_peers - do at v1 

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public string status;
	public List<string> peers = new List<string>();
	public List<string> gray_peers = new List<string>();

	public void serialize(ISerializer s)
	{
	  s.functorMethod(status, "status");
	  s.functorMethod(peers, "peers");
	  s.functorMethod(gray_peers, "gray_peers");
	}
  }
}

public class COMMAND_RPC_GET_FEE_ADDRESS
{

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public class response
  {
	public string address;
	public uint32_t amount = new uint32_t();
	public string status;

	public void serialize(ISerializer s)
	{
	  s.functorMethod(address, "address");
	  s.functorMethod(amount, "amount");
	  s.functorMethod(status, "status");
	}
  }
}

}

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



namespace CryptoNote
{

//deserialization
public class JsonInputValueSerializer : ISerializer
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonInputValueSerializer(Common::JsonValue value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonInputValueSerializer(Common::JsonValue&& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SerializerType type() const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override SerializerType type();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginObject(Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endObject();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginArray(uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endArray();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint8_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref double value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref bool value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(string value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(object value, uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(string value, Common::StringView name);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }

  private Common.JsonValue value = new Common.JsonValue();
  private readonly List<Common.JsonValue> chain = new List<Common.JsonValue>();
  private List<uint64_t> idxs = new List<uint64_t>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  Common::JsonValue getValue(Common::StringView name);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  private bool getNumber<T>(Common.StringView name, ref T v)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));

	if (ptr == null)
	{
	  return false;
	}

	v = (T)ptr.getInteger();
	return true;
  }
}

}


namespace CryptoNote
{

//deserialization
public class JsonInputStreamSerializer : JsonInputValueSerializer
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonInputStreamSerializer(std::istream stream);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();
}

}

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



namespace CryptoNote
{

public class JsonOutputStreamSerializer : ISerializer
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonOutputStreamSerializer();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  public void Dispose();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SerializerType type() const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override SerializerType type();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginObject(Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endObject();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginArray(uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endArray();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint8_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref double value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref bool value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(string value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(object value, uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(string value, Common::StringView name);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Common::JsonValue& getValue() const
  public Common.JsonValue getValue()
  {
	return root.functorMethod;
  }

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& out, const JsonOutputStreamSerializer& enumerator);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::ostream operator <<(std::ostream @out, JsonOutputStreamSerializer enumerator);

  private Common.JsonValue root = new Common.JsonValue();
  private List<Common.JsonValue> chain = new List<Common.JsonValue>();
}

}

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



namespace CryptoNote
{

public class KVBinaryInputStreamSerializer : JsonInputValueSerializer
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  KVBinaryInputStreamSerializer(Common::IInputStream strm);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(object value, uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(string value, Common::StringView name);
}

}

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



namespace CryptoNote
{

public class MemoryStream: Common.IOutputStream
{

  public MemoryStream()
  {
	  this.m_writePos = 0;
  }

  public override uint64_t writeSome(object data, uint64_t size)
  {
	if (size == 0)
	{
	  return 0;
	}

	if (m_writePos + size > m_buffer.Count != null)
	{
	  m_buffer.Resize(m_writePos + size);
	}

//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(m_buffer[m_writePos], data, size);
	m_writePos += size;
	return size;
  }

  public uint64_t size()
  {
	return m_buffer.Count;
  }

  public uint8_t data()
  {
	return m_buffer.data();
  }

  public void clear()
  {
	m_writePos = 0;
	m_buffer.Resize(0);
  }

  private uint64_t m_writePos = new uint64_t();
  private List<uint8_t> m_buffer = new List<uint8_t>();
}

}


namespace CryptoNote
{

public class KVBinaryOutputStreamSerializer : ISerializer
{

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  KVBinaryOutputStreamSerializer();
  public override void Dispose()
  {
	  base.Dispose();
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void dump(Common::IOutputStream target);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ISerializer::SerializerType type() const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override ISerializer::SerializerType type();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginObject(Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endObject();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool beginArray(uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void endArray();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint8_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint16_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint32_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(int64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(uint64_t value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref double value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(ref bool value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool operator ()(string value, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(object value, uint64_t size, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool binary(string value, Common::StringView name);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void writeElementPrefix(uint8_t type, Common::StringView name);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void checkArrayPreamble(uint8_t type);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void updateState(uint8_t type);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  MemoryStream stream();

  private enum State
  {
	Root,
	Object,
	ArrayPrefix,
	Array
  }

  private class Level
  {
	public State state;
	public string name;
	public uint64_t count = new uint64_t();

	public Level(Common.StringView nm)
	{
		this.name = nm;
		this.state = new CryptoNote.KVBinaryOutputStreamSerializer.State.Object;
		this.count = 0;
	}

	public Level(Common.StringView nm, uint64_t arraySize)
	{
		this.name = nm;
		this.state = new CryptoNote.KVBinaryOutputStreamSerializer.State.ArrayPrefix;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.count = arraySize;
		this.count.CopyFrom(arraySize);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public Level(Level && rv)
	{
	  state = rv.state;
	  name = std::move(rv.name);
	  count = rv.count;
	}

  }

  private List<MemoryStream> m_objectsStack = new List<MemoryStream>();
  private List<Level> m_stack = new List<Level>();
}

}





namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class HttpClient;

namespace JsonRpc
{

public class JsonRpcError: System.Exception
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonRpcError();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonRpcError(int c);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonRpcError(int c, string msg);

#if _MSC_VER
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const char* what() const override
  public override string what()
  {
#else
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to 'noexcept':
//ORIGINAL LINE: virtual const char* what() const noexcept override
  public override string what()
  {
#endif
	return message.c_str();
  }

  public void serialize(ISerializer s)
  {
	s.functorMethod(code, "code");
	s.functorMethod(message, "message");
  }

  public int code;
  public string message;
}


public class JsonRpcRequest
{

  public JsonRpcRequest()
  {
	  this.psReq = Common.JsonValue.OBJECT;
  }

  public bool parseRequest(string requestBody)
  {
	try
	{
	  psReq = Common.JsonValue.fromString.functorMethod(requestBody);
	}
	catch (System.Exception)
	{
	  throw new JsonRpcError(GlobalMembers.errParseError);
	}

	if (!psReq.contains("method"))
	{
	  throw new JsonRpcError(GlobalMembers.errInvalidRequest);
	}

	method = psReq("method").getString();

	if (psReq.contains("id"))
	{
	  id = psReq("id");
	}

	if (psReq.contains("password"))
	{
		password = psReq("password");
	}

	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool loadParams(T& v) const
  public bool loadParams<T>(T v)
  {
	CryptoNote.GlobalMembers.loadFromJsonValue(v, psReq.contains("params") ? psReq("params") : new Common.JsonValue(Common.JsonValue.NIL));
	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public bool setParams<T>(T v)
  {
	psReq.set("params", CryptoNote.GlobalMembers.storeToJsonValue.functorMethod(v));
	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getMethod() const
  public string getMethod()
  {
	return method;
  }

  public void setMethod(string m)
  {
	method = m;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const OptionalId& getId() const
  public OptionalId getId()
  {
	return id;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const OptionalPassword& getPassword() const
  public OptionalPassword getPassword()
  {
	  return password;
  }

  public string getBody()
  {
	psReq.set("jsonrpc", "2.0");
	psReq.set("method", method);
	return psReq.toString();
  }


  private Common.JsonValue psReq = new Common.JsonValue();
  private OptionalId id = new OptionalId();
  private OptionalPassword password = new OptionalPassword();
  private string method;
}


public class JsonRpcResponse
{

  public JsonRpcResponse()
  {
	  this.psResp = Common.JsonValue.OBJECT;
  }

  public void parse(string responseBody)
  {
	try
	{
	  psResp = Common.JsonValue.fromString.functorMethod(responseBody);
	}
	catch (System.Exception)
	{
	  throw new JsonRpcError(GlobalMembers.errParseError);
	}
  }

  public void setId(OptionalId id)
  {
	if (id.is_initialized())
	{
	  psResp.insert("id", id.get());
	}
  }

  public void setError(JsonRpcError err)
  {
	psResp.set("error", CryptoNote.GlobalMembers.storeToJsonValue.functorMethod(err));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getError(JsonRpcError& err) const
  public bool getError(JsonRpcError err)
  {
	if (!psResp.contains("error"))
	{
	  return false;
	}

	CryptoNote.GlobalMembers.loadFromJsonValue(err, psResp("error"));
	return true;
  }

  public string getBody()
  {
	psResp.set("jsonrpc", "2.0");
	return psResp.toString();
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public bool setResult<T>(T v)
  {
	psResp.set("result", CryptoNote.GlobalMembers.storeToJsonValue.functorMethod(v));
	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getResult(T& v) const
  public bool getResult<T>(T v)
  {
	if (!psResp.contains("result"))
	{
	  return false;
	}

	CryptoNote.GlobalMembers.loadFromJsonValue(v, psResp("result"));
	return true;
  }

  private Common.JsonValue psResp = new Common.JsonValue();
}


}


}


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Core;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class NodeServer;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct ICryptoNoteProtocolHandler;

public class RpcServer : HttpServer
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  RpcServer(System::Dispatcher dispatcher, Logging::ILogger log, Core c, NodeServer p2p, ICryptoNoteProtocolHandler protocol);

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public delegate bool HandlerFunction(RpcServer UnnamedParameter, HttpRequest request, HttpResponse response);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool enableCors(ClassicVector<string> domains);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool setFeeAddress(string fee_address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool setFeeAmount(uint32_t fee_amount);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<string> getCorsDomains();

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_block_headers_range(COMMAND_RPC_GET_BLOCK_HEADERS_RANGE::request req, COMMAND_RPC_GET_BLOCK_HEADERS_RANGE::response res, JsonRpc::JsonRpcError error_resp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_info(COMMAND_RPC_GET_INFO::request req, COMMAND_RPC_GET_INFO::response res);


//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class Handler>
  private class RpcHandler <Handler>
  {
	public readonly Handler handler = new Handler();
	public readonly bool allowBusyCore;
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  private delegate void HandlerPtr(HttpRequest request, HttpResponse response);
  private static Dictionary<string, RpcHandler<HandlerFunction>> s_handlers = new Dictionary<string, RpcHandler<HandlerFunction>>();

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override void processRequest(HttpRequest request, HttpResponse response);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool processJsonRpcRequest(HttpRequest request, HttpResponse response);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool isCoreReady();

  // json handlers
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_blocks(COMMAND_RPC_GET_BLOCKS_FAST::request req, COMMAND_RPC_GET_BLOCKS_FAST::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_query_blocks(COMMAND_RPC_QUERY_BLOCKS::request req, COMMAND_RPC_QUERY_BLOCKS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_query_blocks_lite(COMMAND_RPC_QUERY_BLOCKS_LITE::request req, COMMAND_RPC_QUERY_BLOCKS_LITE::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_indexes(COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES::request req, COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_random_outs(COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS::request req, COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetPoolChanges(COMMAND_RPC_GET_POOL_CHANGES::request req, COMMAND_RPC_GET_POOL_CHANGES::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetPoolChangesLite(COMMAND_RPC_GET_POOL_CHANGES_LITE::request req, COMMAND_RPC_GET_POOL_CHANGES_LITE::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetBlocksDetailsByHeights(COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS::request req, COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetBlocksDetailsByHashes(COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES::request req, COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetBlockDetailsByHeight(COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT::request req, COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetBlocksHashesByTimestamps(COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS::request req, COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetTransactionDetailsByHashes(COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES::request req, COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool onGetTransactionHashesByPaymentId(COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID::request req, COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID::response rsp);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_height(COMMAND_RPC_GET_HEIGHT::request req, COMMAND_RPC_GET_HEIGHT::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_transactions(COMMAND_RPC_GET_TRANSACTIONS::request req, COMMAND_RPC_GET_TRANSACTIONS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_send_raw_tx(COMMAND_RPC_SEND_RAW_TX::request req, COMMAND_RPC_SEND_RAW_TX::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_fee_info(COMMAND_RPC_GET_FEE_ADDRESS::request req, COMMAND_RPC_GET_FEE_ADDRESS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_peers(COMMAND_RPC_GET_PEERS::request req, COMMAND_RPC_GET_PEERS::response res);

  // json rpc
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_getblockcount(COMMAND_RPC_GETBLOCKCOUNT::request req, COMMAND_RPC_GETBLOCKCOUNT::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_getblockhash(COMMAND_RPC_GETBLOCKHASH::request req, COMMAND_RPC_GETBLOCKHASH::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_getblocktemplate(COMMAND_RPC_GETBLOCKTEMPLATE::request req, COMMAND_RPC_GETBLOCKTEMPLATE::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_currency_id(COMMAND_RPC_GET_CURRENCY_ID::request req, COMMAND_RPC_GET_CURRENCY_ID::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_submitblock(COMMAND_RPC_SUBMITBLOCK::request req, COMMAND_RPC_SUBMITBLOCK::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_last_block_header(COMMAND_RPC_GET_LAST_BLOCK_HEADER::request req, COMMAND_RPC_GET_LAST_BLOCK_HEADER::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_block_header_by_hash(COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH::request req, COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool on_get_block_header_by_height(COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT::request req, COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT::response res);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void fill_block_header_response(BlockTemplate blk, bool orphan_status, uint32_t index, Crypto::Hash hash, block_header_response responce);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  RawBlockLegacy prepareRawBlockLegacy(BinaryArray&& blockBlob);

//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool f_on_blocks_list_json(F_COMMAND_RPC_GET_BLOCKS_LIST::request req, F_COMMAND_RPC_GET_BLOCKS_LIST::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool f_on_block_json(F_COMMAND_RPC_GET_BLOCK_DETAILS::request req, F_COMMAND_RPC_GET_BLOCK_DETAILS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool f_on_transaction_json(F_COMMAND_RPC_GET_TRANSACTION_DETAILS::request req, F_COMMAND_RPC_GET_TRANSACTION_DETAILS::response res);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool f_on_transactions_pool_json(F_COMMAND_RPC_GET_POOL::request req, F_COMMAND_RPC_GET_POOL::response res);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool f_getMixin(Transaction transaction, uint64_t mixin);

  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private Core m_core;
  private NodeServer m_p2p;
  private ICryptoNoteProtocolHandler m_protocol;
  private List<string> m_cors_domains = new List<string>();
  private string m_fee_address;
  private uint32_t m_fee_amount = new uint32_t();
}

}


namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Core;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class NodeServer;
}

public class DaemonCommandsHandler
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  DaemonCommandsHandler(CryptoNote::Core core, CryptoNote::NodeServer srv, Logging::LoggerManager log, CryptoNote::RpcServer prpc_server);

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

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  string get_commands_str();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_block_by_height(uint32_t height);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_block_by_hash(string arg);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool exit(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool help(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_pl(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool show_hr(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool hide_hr(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_bc_outs(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_cn(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_bc(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_bci(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool set_log(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_block(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_tx(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_pool(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool print_pool_sh(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool start_mining(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool stop_mining(ClassicVector<string> args);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool status(ClassicVector<string> args);
}

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
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



namespace CryptoNote
{

/*
 * Implementation of IBlockchainCache that uses database to store internal indexes.
 * Current implementation is designed to always be the root of blockchain, ie
 * start index is always zero, parent is always nullptr, no methods
 * do recursive calls to parent.
 */
public class DatabaseBlockchainCache : IBlockchainCache
{

  /*
   * Constructs new DatabaseBlockchainCache object. Currnetly, only factories that produce 
   * BlockchainCache objects as children are supported.
   */
	public DatabaseBlockchainCache(Currency curr, IDataBase dataBase, IBlockchainCacheFactory blockchainCacheFactory, Logging.ILogger _logger)
	{
		this.currency = curr;
		this.database = dataBase;
		this.blockchainCacheFactory = blockchainCacheFactory;
		this.logger = new <type missing>(_logger, "DatabaseBlockchainCache");
	  DatabaseVersionReadBatch readBatch = new DatabaseVersionReadBatch();
	  var ec = database.read(readBatch);
	  if (ec)
	  {
		throw std::system_error(ec);
	  }
    
	  var version = readBatch.getDbSchemeVersion();
	  if (!version)
	  {
		logger(Logging.DEBUGGING) << "DB scheme version not found, writing: " << GlobalMembers.CURRENT_DB_SCHEME_VERSION;
    
		DatabaseVersionWriteBatch writeBatch = new DatabaseVersionWriteBatch(new uint32_t(GlobalMembers.CURRENT_DB_SCHEME_VERSION));
		var writeError = database.write(writeBatch);
		if (writeError)
		{
		  throw std::system_error(writeError);
		}
	  }
	  else
	  {
		logger(Logging.DEBUGGING) << "Current db scheme version: " << *version;
	  }
    
	  if (getTopBlockIndex() == 0)
	  {
		logger(Logging.DEBUGGING) << "top block index is nill, add genesis block";
		addGenesisBlock(CachedBlock(currency.genesisBlock()));
	  }
	}

	public bool checkDBSchemeVersion(IDataBase database, Logging.ILogger _logger)
	{
	  Logging.LoggerRef logger = new Logging.LoggerRef(_logger, "DatabaseBlockchainCache");
    
	  DatabaseVersionReadBatch readBatch = new DatabaseVersionReadBatch();
	  var ec = database.read(readBatch);
	  if (ec)
	  {
		throw std::system_error(ec);
	  }
    
	  var version = readBatch.getDbSchemeVersion();
	  if (!version)
	  {
		//DB scheme version not found. Looks like it was just created.
		return true;
	  }
	  else if (*version < GlobalMembers.CURRENT_DB_SCHEME_VERSION)
	  {
		logger(Logging.WARNING) << "DB scheme version is less than expected. Expected version " << GlobalMembers.CURRENT_DB_SCHEME_VERSION << ". Actual version " << *version << ". DB will be destroyed and recreated from blocks.bin file.";
		return false;
	  }
	  else if (*version > GlobalMembers.CURRENT_DB_SCHEME_VERSION)
	  {
		logger(Logging.ERROR) << "DB scheme version is greater than expected. Expected version " << GlobalMembers.CURRENT_DB_SCHEME_VERSION << ". Actual version " << *version << ". Please update your software.";
		throw new System.Exception("DB scheme version is greater than expected");
	  }
	  else
	  {
		return true;
	  }
	}

  /*
   * This methods splits cache, upper part (ie blocks with indexes larger than splitBlockIndex)
   * is copied to new BlockchainCache. Unfortunately, implementation requires return value to be of
   * BlockchainCache type.
   */
	public std::unique_ptr<IBlockchainCache> split(uint32_t splitBlockIndex)
	{
	  Debug.Assert(splitBlockIndex <= getTopBlockIndex());
	  logger(Logging.DEBUGGING) << "split at index " << splitBlockIndex << " started, top block index: " << getTopBlockIndex();
    
	  var cache = blockchainCacheFactory.createBlockchainCache(currency, this, splitBlockIndex);
    
	  List<Tuple<uint32_t, Crypto.Hash, TransactionValidatorState, uint64_t>> deletingBlocks = new List<Tuple<uint32_t, Crypto.Hash, TransactionValidatorState, uint64_t>>();
    
	  BlockchainWriteBatch writeBatch = new BlockchainWriteBatch();
	  var currentTop = getTopBlockIndex();
	  for (uint32_t blockIndex = splitBlockIndex; blockIndex <= currentTop; ++blockIndex)
	  {
		ExtendedPushedBlockInfo extendedInfo = getExtendedPushedBlockInfo(blockIndex);
    
		var validatorState = extendedInfo.pushedBlockInfo.validatorState;
		logger(Logging.DEBUGGING) << "pushing block " << blockIndex << " to child segment";
		var blockHash = pushBlockToAnotherCache(*cache, std::move(extendedInfo.pushedBlockInfo));
    
		deletingBlocks.emplace_back(blockIndex, blockHash, validatorState, extendedInfo.timestamp);
	  }
    
	  for (var it = deletingBlocks.rbegin(); it != deletingBlocks.rend(); ++it)
	  {
		var blockIndex = std::get<0>(*it);
		var blockHash = std::get<1>(*it);
		auto validatorState = std::get<2>(*it);
		uint64_t timestamp = std::get<3>(*it);
    
		writeBatch.removeCachedBlock(blockHash, blockIndex).removeRawBlock(blockIndex);
		requestDeleteSpentOutputs(writeBatch, blockIndex, validatorState);
		requestRemoveTimestamp(writeBatch, timestamp, blockHash);
	  }
    
	  var deletingTransactionHashes = requestTransactionHashesFromBlockIndex(splitBlockIndex);
	  requestDeleteTransactions(writeBatch, deletingTransactionHashes);
	  requestDeletePaymentIds(writeBatch, deletingTransactionHashes);
    
	  List<ExtendedTransactionInfo> extendedTransactions = new List<ExtendedTransactionInfo>();
	  if (!GlobalMembers.requestExtendedTransactionInfos(deletingTransactionHashes, database, extendedTransactions))
	  {
		logger(Logging.ERROR) << "Error while split: failed to request extended transaction info";
		throw new System.Exception("failed to request extended transaction info"); //TODO: make error codes
	  }
    
	  SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> keyIndexSplitBoundaries = new SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex>();
	  foreach (var transaction in extendedTransactions)
	  {
		var txkeyBoundaries = GlobalMembers.getMinGlobalIndexesByAmount(transaction.amountToKeyIndexes);
    
		GlobalMembers.mergeOutputsSplitBoundaries(keyIndexSplitBoundaries, txkeyBoundaries);
	  }
    
	  requestDeleteKeyOutputs(writeBatch, keyIndexSplitBoundaries);
    
	  deleteClosestTimestampBlockIndex(writeBatch, splitBlockIndex);
    
	  logger(Logging.DEBUGGING) << "Performing delete operations";
	  // all data and indexes are now copied, no errors detected, can now erase data from database
	  var err = database.write(writeBatch);
	  if (err)
	  {
		logger(Logging.ERROR) << "split write failed, " << err.message();
		throw new System.Exception(err.message());
	  }
    
	  GlobalMembers.cutTail(unitsCache, currentTop + 1 - splitBlockIndex);
    
	  children.push_back(cache.get());
	  logger(Logging.TRACE) << "Delete successfull";
    
	  // invalidate top block index and hash
	  topBlockIndex = boost.none;
	  topBlockHash = boost.none;
	  transactionsCount = boost.none;
    
	  logger(Logging.DEBUGGING) << "split completed";
	  // return new cache
	  return cache;
	}
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public void pushBlock(CachedBlock cachedBlock, List<CachedTransaction> cachedTransactions, TransactionValidatorState validatorState, size_t blockSize, uint64_t generatedCoins, uint64_t blockDifficulty, RawBlock && rawBlock)
	{
	  BlockchainWriteBatch batch = new BlockchainWriteBatch();
	  logger(Logging.DEBUGGING) << "push block with hash " << cachedBlock.getBlockHash() << ", and " << cachedTransactions.Count + 1 << " transactions"; //+1 for base transaction
    
	  // TODO: cache top block difficulty, size, timestamp, coins; use it here
	  var lastBlockInfo = getCachedBlockInfo(getTopBlockIndex());
	  var cumulativeDifficulty = lastBlockInfo.cumulativeDifficulty + blockDifficulty;
	  var alreadyGeneratedCoins = lastBlockInfo.alreadyGeneratedCoins + generatedCoins;
	  var alreadyGeneratedTransactions = lastBlockInfo.alreadyGeneratedTransactions + cachedTransactions.Count + 1;
    
	  CachedBlockInfo blockInfo = new CachedBlockInfo();
	  blockInfo.blockHash = cachedBlock.getBlockHash();
	  blockInfo.alreadyGeneratedCoins = alreadyGeneratedCoins;
	  blockInfo.alreadyGeneratedTransactions = alreadyGeneratedTransactions;
	  blockInfo.cumulativeDifficulty = cumulativeDifficulty;
	  blockInfo.blockSize = (uint32_t)blockSize;
	  blockInfo.timestamp = cachedBlock.getBlock().timestamp;
    
	  batch.insertSpentKeyImages(getTopBlockIndex() + 1, validatorState.spentKeyImages);
    
	  var txHashes = cachedBlock.getBlock().transactionHashes;
	  var baseTransaction = cachedBlock.getBlock().baseTransaction;
	  var cachedBaseTransaction = new CachedTransaction(new Transaction(std::move(baseTransaction)));
    
	  // base transaction's hash is always the first one in index for this block
	  txHashes.insert(txHashes.begin(), cachedBaseTransaction.getTransactionHash());
    
	  batch.insertCachedBlock(blockInfo, getTopBlockIndex() + 1, txHashes);
	  batch.insertRawBlock(getTopBlockIndex() + 1, std::move(rawBlock));
    
	  var transactionIndex = 0;
	  pushTransaction(cachedBaseTransaction, getTopBlockIndex() + 1, transactionIndex++, batch);
    
	  foreach (var transaction in cachedTransactions)
	  {
		pushTransaction(transaction, getTopBlockIndex() + 1, transactionIndex++, batch);
	  }
    
	  var closestBlockIndexDb = GlobalMembers.requestClosestBlockIndexByTimestamp(GlobalMembers.roundToMidnight(cachedBlock.getBlock().timestamp), database);
	  if (!closestBlockIndexDb.second)
	  {
		logger(Logging.ERROR) << "push block " << cachedBlock.getBlockHash() << " request closest block index by timestamp failed";
		throw new System.Exception("Couldn't get closest to timestamp block index");
	  }
    
	  if (!closestBlockIndexDb.first)
	  {
		batch.insertClosestTimestampBlockIndex(GlobalMembers.roundToMidnight(cachedBlock.getBlock().timestamp), getTopBlockIndex() + 1);
	  }
    
	  insertBlockTimestamp(batch, cachedBlock.getBlock().timestamp, cachedBlock.getBlockHash());
    
	  var res = database.write(batch);
	  if (res)
	  {
		logger(Logging.ERROR) << "push block " << cachedBlock.getBlockHash() << " write failed: " << res.message();
		throw new System.Exception(res.message());
	  }
    
	  topBlockIndex = *topBlockIndex + 1;
	  topBlockHash = cachedBlock.getBlockHash();
	  logger(Logging.DEBUGGING) << "push block " << cachedBlock.getBlockHash() << " completed";
    
	  unitsCache.push_back(blockInfo);
	  if (unitsCache.size() > unitsCacheSize)
	  {
		unitsCache.pop_front();
	  }
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual PushedBlockInfo getPushedBlockInfo(uint32_t index) const override;
	public PushedBlockInfo getPushedBlockInfo(uint32_t blockIndex)
	{
	  return getExtendedPushedBlockInfo(blockIndex).pushedBlockInfo;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfSpent(const Crypto::KeyImage& keyImage, uint32_t blockIndex) const override;
	public bool checkIfSpent(Crypto.KeyImage keyImage, uint32_t blockIndex)
	{
	  var batch = BlockchainReadBatch().requestBlockIndexBySpentKeyImage(keyImage);
	  var res = database.read(batch);
	  if (res)
	  {
		logger(Logging.ERROR) << "checkIfSpent failed, request to database failed: " << res.message();
		return false;
	  }
    
	  var readResult = batch.extractResult();
	  var it = readResult.getBlockIndexesBySpentKeyImages().find(keyImage);
    
	  return it != readResult.getBlockIndexesBySpentKeyImages().end() && it.second <= blockIndex;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfSpent(const Crypto::KeyImage& keyImage) const override;
	public bool checkIfSpent(Crypto.KeyImage keyImage)
	{
	  return checkIfSpent(keyImage, getTopBlockIndex());
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTransactionSpendTimeUnlocked(uint64_t unlockTime) const override;
	public bool isTransactionSpendTimeUnlocked(uint64_t unlockTime)
	{
	  return isTransactionSpendTimeUnlocked(unlockTime, getTopBlockIndex());
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTransactionSpendTimeUnlocked(uint64_t unlockTime, uint32_t blockIndex) const override;
	public bool isTransactionSpendTimeUnlocked(uint64_t unlockTime, uint32_t blockIndex)
	{
	  if (unlockTime < currency.maxBlockHeight())
	  {
		// interpret as block index
		return blockIndex + currency.lockedTxAllowedDeltaBlocks() >= unlockTime;
	  }
    
	  // interpret as time
	  return (uint64_t)time(null) + currency.lockedTxAllowedDeltaSeconds() >= unlockTime;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const override;
	public ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<Crypto.PublicKey> publicKeys)
	{
	  return extractKeyOutputKeys(amount, getTopBlockIndex(), globalIndexes, publicKeys);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, uint32_t blockIndex, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const override;
	public ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, uint32_t blockIndex, Common.ArrayView<uint32_t> globalIndexes, List<Crypto.PublicKey> publicKeys)
	{
	//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
	//ORIGINAL LINE: return extractKeyOutputs(amount, blockIndex, globalIndexes, [this, &publicKeys, blockIndex] (const CachedTransactionInfo& info, PackedOutIndex index, uint32_t globalIndex)
	  return extractKeyOutputs(amount, blockIndex, globalIndexes, (CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex) =>
	  {
		if (!isTransactionSpendTimeUnlocked(info.unlockTime, blockIndex))
		{
		  logger(Logging.DEBUGGING) << "extractKeyOutputKeys: output " << globalIndex << " is locked";
		  return ExtractOutputKeysResult.OUTPUT_LOCKED;
		}
    
		auto output = info.outputs[index.outputIndex];
		Debug.Assert(output.type() == typeid(KeyOutput));
		publicKeys.Add(boost::get<KeyOutput>(output).key);
    
		return ExtractOutputKeysResult.SUCCESS;
	  });
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOtputIndexes(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<PackedOutIndex>& outIndexes) const override;
	public ExtractOutputKeysResult extractKeyOtputIndexes(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<PackedOutIndex> outIndexes)
	{
	  if (!GlobalMembers.requestPackedOutputs(new uint64_t(amount), new Common.ArrayView<uint32_t>(globalIndexes), database, outIndexes))
	  {
		logger(Logging.ERROR) << "extractKeyOtputIndexes failed: failed to read database";
		return ExtractOutputKeysResult.INVALID_GLOBAL_INDEX;
	  }
    
	  return ExtractOutputKeysResult.SUCCESS;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOtputReferences(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<System.Tuple<Crypto::Hash, size_t>>& outputReferences) const override;
	public ExtractOutputKeysResult extractKeyOtputReferences(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<Tuple<Crypto.Hash, size_t>> outputReferences)
	{
    
	  return extractKeyOutputs(amount, getTopBlockIndex(), globalIndexes, (CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex) =>
	  {
		outputReferences.Add(Tuple.Create(info.transactionHash, index.outputIndex));
		return ExtractOutputKeysResult.SUCCESS;
	  });
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getTopBlockIndex() const override;
	public uint32_t getTopBlockIndex()
	{
	  if (!topBlockIndex)
	  {
		var batch = BlockchainReadBatch().requestLastBlockIndex();
		var result = database.read(batch);
    
		if (result)
		{
		  logger(Logging.ERROR) << "Failed to read top block index from database";
		  throw std::system_error(result);
		}
    
		var readResult = batch.extractResult();
		if (!readResult.getLastBlockIndex().second)
		{
		  logger(Logging.TRACE) << "Top block index does not exist in database";
		  topBlockIndex = 0;
		}
    
		topBlockIndex = readResult.getLastBlockIndex().first;
	  }
    
	  return *topBlockIndex;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTopBlockHash() const override;
	public Crypto.Hash getTopBlockHash()
	{
	  if (!topBlockHash)
	  {
		var batch = BlockchainReadBatch().requestCachedBlock(getTopBlockIndex());
		var result = readDatabase(batch);
		topBlockHash = result.getCachedBlocks().at(getTopBlockIndex()).blockHash;
	  }
	  return *topBlockHash;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getBlockCount() const override;
	public uint32_t getBlockCount()
	{
	  return getTopBlockIndex() + 1;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool hasBlock(const Crypto::Hash& blockHash) const override;
	public bool hasBlock(Crypto.Hash blockHash)
	{
	  var batch = BlockchainReadBatch().requestBlockIndexByBlockHash(blockHash);
	  var result = database.read(batch);
	  return !result && batch.extractResult().getBlockIndexesByBlockHashes().count(blockHash);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getBlockIndex(const Crypto::Hash& blockHash) const override;
	public uint32_t getBlockIndex(Crypto.Hash blockHash)
	{
	  if (blockHash == getTopBlockHash())
	  {
		return getTopBlockIndex();
	  }
    
	  var batch = BlockchainReadBatch().requestBlockIndexByBlockHash(blockHash);
	  var result = readDatabase(batch);
	  return result.getBlockIndexesByBlockHashes().at(blockHash);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool hasTransaction(const Crypto::Hash& transactionHash) const override;
	public bool hasTransaction(Crypto.Hash transactionHash)
	{
	  var batch = BlockchainReadBatch().requestCachedTransaction(transactionHash);
	  var result = database.read(batch);
	  return !result && batch.extractResult().getCachedTransactions().count(transactionHash);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastTimestamps(size_t count) const override;
	public List<uint64_t> getLastTimestamps(size_t count)
	{
	  return getLastTimestamps(count, getTopBlockIndex(), UseGenesis({true}));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastTimestamps(size_t count, uint32_t blockIndex, UseGenesis) const override;
	public List<uint64_t> getLastTimestamps(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
	{
	  return getLastUnits(count, blockIndex, useGenesis, (CachedBlockInfo inf) =>
	  {
		  return inf.timestamp;
	  });
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastBlocksSizes(size_t count) const override;
	public List<uint64_t> getLastBlocksSizes(size_t count)
	{
	  return getLastBlocksSizes(count, getTopBlockIndex(), UseGenesis({true}));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastBlocksSizes(size_t count, uint32_t blockIndex, UseGenesis) const override;
	public List<uint64_t> getLastBlocksSizes(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
	{
	  return getLastUnits(count, blockIndex, useGenesis, (CachedBlockInfo cb) =>
	  {
		  return cb.blockSize;
	  });
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastCumulativeDifficulties(size_t count, uint32_t blockIndex, UseGenesis) const override;
	public List<uint64_t> getLastCumulativeDifficulties(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
	{
	  return getLastUnits(count, blockIndex, useGenesis, (CachedBlockInfo info) =>
	  {
		  return info.cumulativeDifficulty;
	  });
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastCumulativeDifficulties(size_t count) const override;
	public List<uint64_t> getLastCumulativeDifficulties(size_t count)
	{
	  return getLastCumulativeDifficulties(count, getTopBlockIndex(), UseGenesis({true}));
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getDifficultyForNextBlock() const override;
	public uint64_t getDifficultyForNextBlock()
	{
	  return getDifficultyForNextBlock(getTopBlockIndex());
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getDifficultyForNextBlock(uint32_t blockIndex) const override;
	public uint64_t getDifficultyForNextBlock(uint32_t blockIndex)
	{
	  Debug.Assert(blockIndex <= getTopBlockIndex());
	  uint8_t nextBlockMajorVersion = getBlockMajorVersionForHeight(blockIndex + 1);
	  var timestamps = getLastTimestamps(currency.difficultyBlocksCountByBlockVersion(nextBlockMajorVersion, blockIndex), blockIndex, UseGenesis({false}));
	  var commulativeDifficulties = getLastCumulativeDifficulties(currency.difficultyBlocksCountByBlockVersion(nextBlockMajorVersion, blockIndex), blockIndex, UseGenesis({false}));
	  return currency.getNextDifficulty(nextBlockMajorVersion, blockIndex, std::move(timestamps), std::move(commulativeDifficulties));
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getCurrentCumulativeDifficulty() const override;
	public uint64_t getCurrentCumulativeDifficulty()
	{
	  return getCachedBlockInfo(getTopBlockIndex()).cumulativeDifficulty;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getCurrentCumulativeDifficulty(uint32_t blockIndex) const override;
	public uint64_t getCurrentCumulativeDifficulty(uint32_t blockIndex)
	{
	  Debug.Assert(blockIndex <= getTopBlockIndex());
	  return getCachedBlockInfo(blockIndex).cumulativeDifficulty;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedCoins() const override;
	public uint64_t getAlreadyGeneratedCoins()
	{
	  return getAlreadyGeneratedCoins(getTopBlockIndex());
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedCoins(uint32_t blockIndex) const override;
	public uint64_t getAlreadyGeneratedCoins(uint32_t blockIndex)
	{
	  return getCachedBlockInfo(blockIndex).alreadyGeneratedCoins;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedTransactions(uint32_t blockIndex) const override;
	public uint64_t getAlreadyGeneratedTransactions(uint32_t blockIndex)
	{
	  return getCachedBlockInfo(blockIndex).alreadyGeneratedTransactions;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastUnits(size_t count, uint32_t blockIndex, UseGenesis use, System.Func<const CachedBlockInfo&, uint64_t> pred) const override;
	public List<uint64_t> getLastUnits(size_t count, uint32_t blockIndex, UseGenesis useGenesis, Func<CachedBlockInfo , uint64_t> pred)
	{
	  Debug.Assert(count <= uint32_t.MaxValue);
    
	  var cachedUnits = getLastCachedUnits(blockIndex, count, useGenesis);
    
	  uint32_t availableUnits = new uint32_t(blockIndex);
	  if (useGenesis != null)
	  {
		availableUnits += 1;
	  }
    
	  Debug.Assert(availableUnits >= cachedUnits.size());
    
	  if (availableUnits - cachedUnits.size() == 0 != null)
	  {
		List<uint64_t> result = new List<uint64_t>();
		result.Capacity = cachedUnits.size();
		foreach (var unit in cachedUnits)
		{
		  result.Add(pred(unit));
		}
    
		return result;
	  }
    
	  Debug.Assert(blockIndex + 1 >= cachedUnits.size());
	  uint32_t dbIndex = blockIndex - (uint32_t)cachedUnits.size();
    
	  Debug.Assert(count >= cachedUnits.size());
	  size_t leftCount = count - cachedUnits.size();
    
	  var dbUnits = getLastDbUnits(dbIndex, leftCount, useGenesis);
	  List<uint64_t> result = new List<uint64_t>();
	  result.Capacity = dbUnits.size() + cachedUnits.size();
	  foreach (var unit in dbUnits)
	  {
		result.Add(pred(unit));
	  }
    
	  foreach (var unit in cachedUnits)
	  {
		result.Add(pred(unit));
	  }
    
	  return result;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Crypto::Hash getBlockHash(uint32_t blockIndex) const override;
	public Crypto.Hash getBlockHash(uint32_t blockIndex)
	{
	  if (blockIndex == getTopBlockIndex())
	  {
		return getTopBlockHash();
	  }
    
	  var batch = BlockchainReadBatch().requestCachedBlock(blockIndex);
	  var result = readDatabase(batch);
	  return result.getCachedBlocks().at(blockIndex).blockHash;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashes(uint32_t startIndex, size_t maxCount) const override;
	public List<Crypto.Hash> getBlockHashes(uint32_t startIndex, size_t maxCount)
	{
	  Debug.Assert(startIndex <= getTopBlockIndex());
	  Debug.Assert(maxCount <= uint32_t.MaxValue);
    
	  uint32_t count = Math.Min(getTopBlockIndex() - startIndex + 1, (uint32_t)maxCount);
	  if (count == 0)
	  {
		return new List<Crypto.Hash>();
	  }
    
	  BlockchainReadBatch request = new BlockchainReadBatch();
	  var index = startIndex;
	  while (index != startIndex + count)
	  {
		request.requestCachedBlock(index++);
	  }
    
	  var result = readDatabase(request);
	  Debug.Assert(result.getCachedBlocks().size() == count);
    
	  List<Crypto.Hash> hashes = new List<Crypto.Hash>();
	  hashes.Capacity = count;
    
	  SortedDictionary<uint32_t, CachedBlockInfo> sortedResult = new SortedDictionary<uint32_t, CachedBlockInfo>(result.getCachedBlocks().begin(), result.getCachedBlocks().end());
    
	  std::transform(sortedResult.GetEnumerator(), sortedResult.end(), std::back_inserter(hashes), (Tuple<uint32_t, CachedBlockInfo> cb) =>
	  {
		  return cb.Item2.blockHash;
	  });
	  return hashes;
	}

  /*
   * This method always returns zero
   */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getStartBlockIndex() const override;
	public uint32_t getStartBlockIndex()
	{
	  return 0;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getKeyOutputsCountForAmount(uint64_t amount, uint32_t blockIndex) const override;
	public size_t getKeyOutputsCountForAmount(uint64_t amount, uint32_t blockIndex)
	{
	  uint32_t outputsCount = GlobalMembers.requestKeyOutputGlobalIndexesCountForAmount(new uint64_t(amount), database);
    
	  var getOutput = std::bind(GlobalMembers.retrieveKeyOutput, std::placeholders._1, std::placeholders._2, std::@ref(database));
	  var begin = new DbOutputConstIterator(getOutput, new uint64_t(amount), 0);
	  var end = new DbOutputConstIterator(getOutput, new uint64_t(amount), new uint32_t(outputsCount));
    
	//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	  var it = std::lower_bound(begin, end, blockIndex, (PackedOutIndex output, uint32_t blockIndex) =>
	  {
		return output.blockIndex < blockIndex;
	  });
    
	  size_t result = (size_t)std::distance(begin, it);
	  logger(Logging.DEBUGGING) << "Key outputs count for amount " << amount << " is " << result << " by block index " << blockIndex;
    
	  return result;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getTimestampLowerBoundBlockIndex(uint64_t timestamp) const override;
	public uint32_t getTimestampLowerBoundBlockIndex(uint64_t timestamp)
	{
	  var midnight = GlobalMembers.roundToMidnight(new uint64_t(timestamp));
    
	  while (midnight > 0)
	  {
		var dbRes = GlobalMembers.requestClosestBlockIndexByTimestamp(midnight, database);
		if (!dbRes.second)
		{
		  logger(Logging.DEBUGGING) << "getTimestampLowerBoundBlockIndex failed: failed to read database";
		  throw new System.Exception("Couldn't get closest to timestamp block index");
		}
    
		if (!dbRes.first)
		{
		  midnight -= 60 * 60 * 24;
		  continue;
		}
    
		return *dbRes.first;
	  }
    
	  return 0;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint32_t>& globalIndexes) const override;
	public bool getTransactionGlobalIndexes(Crypto.Hash transactionHash, ref List<uint32_t> globalIndexes)
	{
	  var batch = BlockchainReadBatch().requestCachedTransaction(transactionHash);
	  var result = database.read(batch);
	  if (result)
	  {
		logger(Logging.DEBUGGING) << "getTransactionGlobalIndexes failed: failed to read database";
		return false;
	  }
    
	  var readResult = batch.extractResult();
	  var it = readResult.getCachedTransactions().find(transactionHash);
	  if (it == readResult.getCachedTransactions().end())
	  {
		logger(Logging.DEBUGGING) << "getTransactionGlobalIndexes failed: cached transaction for hash " << transactionHash << " not present";
		return false;
	  }
    
	  globalIndexes = it.second.globalIndexes;
	  return true;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getTransactionCount() const override;
	public size_t getTransactionCount()
	{
	  return (size_t)getCachedTransactionsCount();
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getBlockIndexContainingTx(const Crypto::Hash& transactionHash) const override;
	public uint32_t getBlockIndexContainingTx(Crypto.Hash transactionHash)
	{
	  var batch = BlockchainReadBatch().requestCachedTransaction(transactionHash);
	  var result = readDatabase(batch);
	  return result.getCachedTransactions().at(transactionHash).blockIndex;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getChildCount() const override;
	public size_t getChildCount()
	{
	  return children.size();
	}

  /*
   * This method always returns nullptr
   */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IBlockchainCache* getParent() const override;
	public IBlockchainCache getParent()
	{
	  return null;
	}
  /*
   * This method does nothing, is here only to support full interface
   */
	public void setParent(IBlockchainCache ptr)
	{
	  Debug.Assert(false);
	}
	public void addChild(IBlockchainCache ptr)
	{
	  if (std::find(children.begin(), children.end(), ptr) == children.end())
	  {
		children.push_back(ptr);
	  }
	}
	public bool deleteChild(IBlockchainCache ptr)
	{
	  var it = std::remove(children.begin(), children.end(), ptr);
	  var res = it != children.end();
	  children.erase(it, children.end());
	  return res;
	}

	public void save()
	{
	}
	public void load()
	{
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash>& transactions, ClassicVector<Crypto::Hash>& missedTransactions) const override;
	public List<List<uint8_t>> getRawTransactions(List<Crypto.Hash> transactions, List<Crypto.Hash> missedTransactions)
	{
	  List<List<uint8_t>> found = new List<List<uint8_t>>();
	  getRawTransactions(transactions, found, missedTransactions);
	  return found;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash>& transactions) const override;
	public List<List<uint8_t>> getRawTransactions(List<Crypto.Hash> transactions)
	{
	  List<Crypto.Hash> missed = new List<Crypto.Hash>();
	  List<List<uint8_t>> found = new List<List<uint8_t>>();
	  getRawTransactions(transactions, found, missed);
	  return found;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void getRawTransactions(const ClassicVector<Crypto::Hash>& transactions, ClassicVector<BinaryArray>& foundTransactions, ClassicVector<Crypto::Hash>& missedTransactions) const override;
	public void getRawTransactions(List<Crypto.Hash> transactions, List<List<uint8_t>> foundTransactions, List<Crypto.Hash> missedTransactions)
	{
	  BlockchainReadBatch batch = new BlockchainReadBatch();
	  foreach (var hash in transactions)
	  {
		batch.requestCachedTransaction(hash);
	  }
    
	  var res = readDatabase(batch);
	  foreach (var tx in res.getCachedTransactions())
	  {
		batch.requestRawBlock(tx.second.blockIndex);
	  }
    
	  var blocks = readDatabase(batch);
    
	  foundTransactions.Capacity = foundTransactions.Count + transactions.Count;
	  auto hashesMap = res.getCachedTransactions();
	  auto blocksMap = blocks.getRawBlocks();
	  foreach (var hash in transactions)
	  {
		var transactionIt = hashesMap.find(hash);
		if (transactionIt == hashesMap.end())
		{
		  logger(Logging.DEBUGGING) << "detected missing transaction for hash " << hash << " in getRawTransaction";
		  missedTransactions.Add(hash);
		  continue;
		}
    
		var blockIt = blocksMap.find(transactionIt.second.blockIndex);
		if (blockIt == blocksMap.end())
		{
		  logger(Logging.DEBUGGING) << "detected missing transaction for hash " << hash << " in getRawTransaction";
		  missedTransactions.Add(hash);
		  continue;
		}
    
		if (transactionIt.second.transactionIndex == 0)
		{
		  var block = fromBinaryArray<BlockTemplate>(blockIt.second.block);
		  foundTransactions.emplace_back(toBinaryArray(block.baseTransaction));
		}
		else
		{
		  Debug.Assert(blockIt.second.transactions.size() >= transactionIt.second.transactionIndex - 1);
		  foundTransactions.emplace_back(blockIt.second.transactions[transactionIt.second.transactionIndex - 1]);
		}
	  }
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint32_t index) const override;
	public RawBlock getBlockByIndex(uint32_t index)
	{
	  var batch = BlockchainReadBatch().requestRawBlock(index);
	  var res = readDatabase(batch);
	  return std::move(res.getRawBlocks().at(index));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BinaryArray getRawTransaction(uint32_t blockIndex, uint32_t transactionIndex) const override;
	public List<uint8_t> getRawTransaction(uint32_t blockIndex, uint32_t transactionIndex)
	{
	  return getBlockByIndex(blockIndex).transactions.at(transactionIndex);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const override;
	public List<Crypto.Hash> getTransactionHashes()
	{
	  Debug.Assert(false);
	  return new List<Crypto.Hash>();
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint32_t> getRandomOutsByAmount(uint64_t amount, size_t count, uint32_t blockIndex) const override;
	public List<uint32_t> getRandomOutsByAmount(uint64_t amount, size_t count, uint32_t blockIndex)
	{
	  var batch = BlockchainReadBatch().requestKeyOutputGlobalIndexesCountForAmount(amount);
	  var result = readDatabase(batch);
	  var outputsCount = result.getKeyOutputGlobalIndexesCountForAmounts();
	  var outputsToPick = Math.Min((uint32_t)count, outputsCount[amount]);
    
	  List<uint32_t> resultOuts = new List<uint32_t>();
	  resultOuts.Capacity = outputsToPick;
    
	  ShuffleGenerator<uint32_t, Crypto.random_engine<uint32_t>> generator = new ShuffleGenerator<uint32_t, Crypto.random_engine<uint32_t>>(outputsCount[amount]);
    
	  while (outputsToPick)
	  {
		List<uint32_t> globalIndexes = new List<uint32_t>();
		globalIndexes.Capacity = outputsToPick;
    
		try
		{
		  for (uint32_t i = 0; i < outputsToPick; ++i, globalIndexes.Add(generator()))
		  {
		  }
		  //std::generate_n(std::back_inserter(globalIndexes), outputsToPick, generator);
		}
		catch (SequenceEnded)
		{
		  logger(Logging.TRACE) << "getRandomOutsByAmount: generator reached sequence end";
		  return resultOuts;
		}
    
		List<PackedOutIndex> outputs = new List<PackedOutIndex>();
		if (extractKeyOtputIndexes(amount, Common.ArrayView<uint32_t>(globalIndexes.data(), globalIndexes.Count), outputs) != ExtractOutputKeysResult.SUCCESS)
		{
		  logger(Logging.DEBUGGING) << "getRandomOutsByAmount: failed to extract key output indexes";
		  throw new System.Exception("Invalid output index"); //TODO: make error code
		}
    
		List<ExtendedTransactionInfo> transactions = new List<ExtendedTransactionInfo>();
		if (!GlobalMembers.requestExtendedTransactionInfos(outputs, database, transactions))
		{
		  logger(Logging.TRACE) << "getRandomOutsByAmount: requestExtendedTransactionInfos failed";
		  throw new System.Exception("Error while requesting transactions"); //TODO: make error code
		}
    
		Debug.Assert(globalIndexes.Count == transactions.Count);
    
		uint32_t uppperBlockIndex = 0;
		if (blockIndex > currency.minedMoneyUnlockWindow())
		{
	//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
	//ORIGINAL LINE: uppperBlockIndex = blockIndex - currency.minedMoneyUnlockWindow();
		  uppperBlockIndex.CopyFrom(blockIndex - currency.minedMoneyUnlockWindow());
		}
    
		for (size_t i = 0; i < transactions.Count; ++i)
		{
		  if (!isTransactionSpendTimeUnlocked(transactions[i].unlockTime, blockIndex) || transactions[i].blockIndex > uppperBlockIndex)
		  {
			continue;
		  }
    
		  resultOuts.Add(globalIndexes[i]);
		  --outputsToPick;
		}
	  }
    
	  return resultOuts;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOutputs(uint64_t amount, uint32_t blockIndex, Common::ArrayView<uint32_t> globalIndexes, System.Func<const CachedTransactionInfo& info, PackedOutIndex index, uint32_t globalIndex, ExtractOutputKeysResult> pred) const override;
	public ExtractOutputKeysResult extractKeyOutputs(uint64_t amount, uint32_t blockIndex, Common.ArrayView<uint32_t> globalIndexes, Func<CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex, ExtractOutputKeysResult> callback)
	{
	  BlockchainReadBatch batch = new BlockchainReadBatch();
	  for (var it = globalIndexes.begin(); it != globalIndexes.end(); ++it)
	  {
		batch.requestKeyOutputInfo(amount, *it);
	  }
    
	  var result = readDatabase(batch).getKeyOutputInfo();
	  SortedDictionary<Tuple<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex>, KeyOutputInfo> sortedResult = new SortedDictionary<Tuple<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex>, KeyOutputInfo>(result.begin(), result.end());
	  foreach (var kv in sortedResult)
	  {
		ExtendedTransactionInfo tx = new ExtendedTransactionInfo();
		tx.unlockTime = kv.second.unlockTime;
		tx.transactionHash = kv.second.transactionHash;
		tx.outputs.resize(kv.second.outputIndex + 1);
		tx.outputs[kv.second.outputIndex] = new KeyOutput({kv.second.publicKey});
		PackedOutIndex fakePoi = new PackedOutIndex();
		fakePoi.outputIndex = kv.second.outputIndex;
    
		//TODO: change the interface of extractKeyOutputs to return vector of structures instead of passing callback as predicate
		var ret = callback(tx, fakePoi, kv.first.second);
		if (ret != ExtractOutputKeysResult.SUCCESS)
		{
		  logger(Logging.DEBUGGING) << "extractKeyOutputs failed : callback returned error";
		  return ret;
		}
	  }
    
	  return ExtractOutputKeysResult.SUCCESS;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const override;
	public List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId)
	{
	  var countBatch = BlockchainReadBatch().requestTransactionCountByPaymentId(paymentId);
	  uint32_t transactionsCountByPaymentId = readDatabase(countBatch).getTransactionCountByPaymentIds().at(paymentId);
    
	  BlockchainReadBatch transactionBatch = new BlockchainReadBatch();
	  for (uint32_t i = 0; i < transactionsCountByPaymentId; ++i)
	  {
		transactionBatch.requestTransactionHashByPaymentId(paymentId, i);
	  }
    
	  var result = readDatabase(transactionBatch);
	  List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
	  transactionHashes.Capacity = result.getTransactionHashesByPaymentIds().size();
	  foreach (var kv in result.getTransactionHashesByPaymentIds())
	  {
		transactionHashes.emplace_back(kv.second);
	  }
    
	  return transactionHashes;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount) const override;
	public List<Crypto.Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount)
	{
	  List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();
	  if (secondsCount == 0)
	  {
		return blockHashes;
	  }
    
	  BlockchainReadBatch batch = new BlockchainReadBatch();
	  for (uint64_t timestamp = timestampBegin; timestamp < timestampBegin + (uint64_t)secondsCount; ++timestamp)
	  {
		batch.requestBlockHashesByTimestamp(timestamp);
	  }
    
	  var result = readDatabase(batch);
	  for (uint64_t timestamp = timestampBegin; timestamp < timestampBegin + (uint64_t)secondsCount; ++timestamp)
	  {
		if (result.getBlockHashesByTimestamp().count(timestamp) == 0)
		{
		  continue;
		}
    
		auto hashes = result.getBlockHashesByTimestamp().at(timestamp);
		blockHashes.AddRange(hashes);
	  }
    
	  return blockHashes;
	}

  private readonly Currency currency;
  private IDataBase database;
  private IBlockchainCacheFactory blockchainCacheFactory;
  private boost.optional<uint32_t> topBlockIndex;
  private boost.optional<Crypto.Hash> topBlockHash;
  private boost.optional<uint64_t> transactionsCount;
  private boost.optional<uint32_t> keyOutputAmountsCount;
//C++ TO C# CONVERTER TODO TASK: The typedef 'Amount' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  private Dictionary<Amount, int32_t> keyOutputCountsForAmounts = new Dictionary<Amount, int32_t>();
  private List<IBlockchainCache> children = new List<IBlockchainCache>();
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private LinkedList<CachedBlockInfo> unitsCache = new LinkedList<CachedBlockInfo>();
  private readonly size_t unitsCacheSize = 1000;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following type could not be found:
//  struct ExtendedPushedBlockInfo;
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtendedPushedBlockInfo getExtendedPushedBlockInfo(uint32_t blockIndex) const;
	public DatabaseBlockchainCache.ExtendedPushedBlockInfo getExtendedPushedBlockInfo(uint32_t blockIndex)
	{
	  Debug.Assert(blockIndex <= getTopBlockIndex());
    
	  var batch = BlockchainReadBatch().requestRawBlock(blockIndex).requestCachedBlock(blockIndex).requestSpentKeyImagesByBlock(blockIndex);
    
	  if (blockIndex > 0)
	  {
		batch.requestCachedBlock(blockIndex - 1);
	  }
    
	  var dbResult = readDatabase(batch);
	  CachedBlockInfo blockInfo = dbResult.getCachedBlocks().at(blockIndex);
	  CachedBlockInfo previousBlockInfo = blockIndex > 0 ? dbResult.getCachedBlocks().at(blockIndex - 1) : GlobalMembers.NULL_CACHED_BLOCK_INFO;
    
	  ExtendedPushedBlockInfo extendedInfo = new ExtendedPushedBlockInfo();
    
	  extendedInfo.pushedBlockInfo.rawBlock = dbResult.getRawBlocks().at(blockIndex);
	  extendedInfo.pushedBlockInfo.blockSize = blockInfo.blockSize;
	  extendedInfo.pushedBlockInfo.blockDifficulty = blockInfo.cumulativeDifficulty - previousBlockInfo.cumulativeDifficulty;
	  extendedInfo.pushedBlockInfo.generatedCoins = blockInfo.alreadyGeneratedCoins - previousBlockInfo.alreadyGeneratedCoins;
    
	  auto spentKeyImages = dbResult.getSpentKeyImagesByBlock().at(blockIndex);
    
	  extendedInfo.pushedBlockInfo.validatorState.spentKeyImages.insert(spentKeyImages.begin(), spentKeyImages.end());
    
	  extendedInfo.timestamp = blockInfo.timestamp;
    
	  return extendedInfo;
	}

	public void deleteClosestTimestampBlockIndex(BlockchainWriteBatch writeBatch, uint32_t splitBlockIndex)
	{
	  var batch = BlockchainReadBatch().requestCachedBlock(splitBlockIndex);
	  var blockResult = readDatabase(batch);
	  var timestamp = blockResult.getCachedBlocks().at(splitBlockIndex).timestamp;
    
	  var midnight = GlobalMembers.roundToMidnight(timestamp);
	  var timestampResult = GlobalMembers.requestClosestBlockIndexByTimestamp(midnight, database);
	  if (!timestampResult.second)
	  {
		logger(Logging.ERROR) << "deleteClosestTimestampBlockIndex error: get closest timestamp block index, database read failed";
		throw new System.Exception("Couldn't get closest timestamp block index");
	  }
    
	  Debug.Assert((bool)timestampResult.first);
    
	  var blockIndex = *timestampResult.first;
	  Debug.Assert(splitBlockIndex >= blockIndex);
    
	  if (splitBlockIndex != blockIndex)
	  {
		midnight += GlobalMembers.ONE_DAY_SECONDS;
	  }
    
	  BlockchainReadBatch midnightBatch = new BlockchainReadBatch();
	  while (readDatabase(midnightBatch.requestClosestTimestampBlockIndex(midnight)).getClosestTimestampBlockIndex().count(midnight))
	  {
		writeBatch.removeClosestTimestampBlockIndex(midnight);
		midnight += GlobalMembers.ONE_DAY_SECONDS;
	  }
    
	  logger(Logging.TRACE) << "deleted closest timestamp";
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: CachedBlockInfo getCachedBlockInfo(uint32_t index) const;
	public CachedBlockInfo getCachedBlockInfo(uint32_t index)
	{
	  var batch = BlockchainReadBatch().requestCachedBlock(index);
	  var result = readDatabase(batch);
	  return result.getCachedBlocks().at(index);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: BlockchainReadResult readDatabase(BlockchainReadBatch& batch) const;
	public BlockchainReadResult readDatabase(BlockchainReadBatch batch)
	{
	  var result = database.read(batch);
	  if (result)
	  {
		logger(Logging.ERROR) << "failed to read database, error is " << result.message();
		throw new System.Exception(result.message());
	  }
    
	  return batch.extractResult();
	}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void addSpentKeyImage(Crypto::KeyImage keyImage, uint32_t blockIndex);
	public void pushTransaction(CachedTransaction cachedTransaction, uint32_t blockIndex, uint16_t transactionBlockIndex, BlockchainWriteBatch batch)
	{
    
	  logger(Logging.DEBUGGING) << "push transaction with hash " << cachedTransaction.getTransactionHash();
	  auto tx = cachedTransaction.getTransaction();
    
	  ExtendedTransactionInfo transactionCacheInfo = new ExtendedTransactionInfo();
	  transactionCacheInfo.blockIndex = blockIndex;
	  transactionCacheInfo.transactionIndex = transactionBlockIndex;
	  transactionCacheInfo.transactionHash = cachedTransaction.getTransactionHash();
	  transactionCacheInfo.unlockTime = tx.unlockTime;
    
	  Debug.Assert(tx.outputs.size() <= uint16_t.MaxValue);
    
	  transactionCacheInfo.globalIndexes.reserve(tx.outputs.size());
	  transactionCacheInfo.outputs.reserve(tx.outputs.size());
	  var outputCount = 0;
	  Dictionary<Amount, List<PackedOutIndex>> keyIndexes = new Dictionary<Amount, List<PackedOutIndex>>();
    
	  SortedSet<Amount> newKeyAmounts = new SortedSet<Amount>();
    
	  foreach (var output in tx.outputs)
	  {
		transactionCacheInfo.outputs.push_back(output.target);
    
		PackedOutIndex poi = new PackedOutIndex();
		poi.blockIndex = blockIndex;
		poi.transactionIndex = transactionBlockIndex;
		poi.outputIndex = outputCount++;
    
	//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
		if (output.target.type() == typeid(KeyOutput))
		{
		  keyIndexes[output.amount].Add(poi);
		  var outputCountForAmount = updateKeyOutputCount(output.amount, 1);
		  if (outputCountForAmount == 1)
		  {
			newKeyAmounts.Add(output.amount);
		  }
    
		  Debug.Assert(outputCountForAmount > 0);
		  var globalIndex = outputCountForAmount - 1;
		  transactionCacheInfo.globalIndexes.push_back(globalIndex);
		  //output global index:
		  transactionCacheInfo.amountToKeyIndexes[output.amount].push_back(globalIndex);
    
		  KeyOutputInfo outputInfo = new KeyOutputInfo();
		  outputInfo.publicKey = boost::get<KeyOutput>(output.target).key;
		  outputInfo.transactionHash = transactionCacheInfo.transactionHash;
		  outputInfo.unlockTime = transactionCacheInfo.unlockTime;
		  outputInfo.outputIndex = poi.outputIndex;
    
		  batch.insertKeyOutputInfo(output.amount, globalIndex, outputInfo);
		}
	  }
    
	  foreach (var amountToOutputs in keyIndexes)
	  {
		batch.insertKeyOutputGlobalIndexes(amountToOutputs.first, amountToOutputs.second, updateKeyOutputCount(amountToOutputs.first, 0)); //Size already updated.
	  }
    
	  if (newKeyAmounts.Count > 0)
	  {
		Debug.Assert(keyOutputAmountsCount.is_initialized());
		batch.insertKeyOutputAmounts(newKeyAmounts, *keyOutputAmountsCount);
	  }
    
	  Crypto.Hash paymentId = new Crypto.Hash();
	  if (getPaymentIdFromTxExtra(cachedTransaction.getTransaction().extra, paymentId))
	  {
		insertPaymentId(batch, cachedTransaction.getTransactionHash(), paymentId);
	  }
    
	  batch.insertCachedTransaction(transactionCacheInfo, getCachedTransactionsCount() + 1);
	  transactionsCount = *transactionsCount + 1;
	  logger(Logging.DEBUGGING) << "push transaction with hash " << cachedTransaction.getTransactionHash() << " finished";
	}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint32_t insertKeyOutputToGlobalIndex(uint64_t amount, PackedOutIndex output); //TODO not implemented. Should it be removed?
//C++ TO C# CONVERTER TODO TASK: The typedef 'Amount' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t updateKeyOutputCount(Amount amount, int32_t diff) const;
	public uint32_t updateKeyOutputCount(Amount amount, int32_t diff)
	{
	  var it = keyOutputCountsForAmounts.find(amount);
	  if (it == keyOutputCountsForAmounts.end())
	  {
		logger(Logging.TRACE) << "updateKeyOutputCount: failed to found key for amount, request database";
    
		BlockchainReadBatch batch = new BlockchainReadBatch();
		var result = readDatabase(batch.requestKeyOutputGlobalIndexesCountForAmount(amount));
		var found = result.getKeyOutputGlobalIndexesCountForAmounts().find(amount);
		var val = found != result.getKeyOutputGlobalIndexesCountForAmounts().end() ? found.second : 0;
		it = keyOutputCountsForAmounts.insert({amount, val}).first;
		logger(Logging.TRACE) << "updateKeyOutputCount: database replied: amount " << amount << " value " << val;
    
		if (val == 0)
		{
		  if (!keyOutputAmountsCount)
		  {
			var result = readDatabase(batch.requestKeyOutputAmountsCount());
			keyOutputAmountsCount = result.getKeyOutputAmountsCount();
		  }
    
		  keyOutputAmountsCount = *keyOutputAmountsCount + 1;
		}
	  }
	  else if (!keyOutputAmountsCount)
	  {
		var result = readDatabase(BlockchainReadBatch().requestKeyOutputAmountsCount());
		keyOutputAmountsCount = result.getKeyOutputAmountsCount();
	  }
    
	  it.second += diff;
	  Debug.Assert(it.second >= 0);
	  return it.second;
	}
	public void insertPaymentId(BlockchainWriteBatch batch, Crypto.Hash transactionHash, Crypto.Hash paymentId)
	{
	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
	  uint32_t count = 0;
    
	  var readResult = readDatabase(readBatch.requestTransactionCountByPaymentId(paymentId));
	  if (readResult.getTransactionCountByPaymentIds().count(paymentId) != 0)
	  {
		count = readResult.getTransactionCountByPaymentIds().at(paymentId);
	  }
    
	  count += 1;
    
	  batch.insertPaymentId(transactionHash, paymentId, count);
	}
	public void insertBlockTimestamp(BlockchainWriteBatch batch, uint64_t timestamp, Crypto.Hash blockHash)
	{
	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
	  readBatch.requestBlockHashesByTimestamp(timestamp);
    
	  List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();
	  var readResult = readDatabase(readBatch);
    
	  if (readResult.getBlockHashesByTimestamp().count(timestamp) != 0)
	  {
		blockHashes = readResult.getBlockHashesByTimestamp().at(timestamp);
	  }
    
	  blockHashes.emplace_back(blockHash);
    
	  batch.insertTimestamp(timestamp, blockHashes);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public void addGenesisBlock(CachedBlock && genesisBlock)
	{
	  uint64_t minerReward = 0;
	  foreach (TransactionOutput output in genesisBlock.getBlock().baseTransaction.outputs)
	  {
		minerReward += output.amount;
	  }
    
	  Debug.Assert(minerReward > 0);
    
	  uint64_t baseTransactionSize = getObjectBinarySize(genesisBlock.getBlock().baseTransaction);
	  Debug.Assert(baseTransactionSize < uint32_t.MaxValue);
    
	  BlockchainWriteBatch batch = new BlockchainWriteBatch();
    
	  CachedBlockInfo blockInfo = new CachedBlockInfo(genesisBlock.getBlockHash(), genesisBlock.getBlock().timestamp, 1, minerReward, 1, uint32_t(baseTransactionSize));
    
	  var baseTransaction = genesisBlock.getBlock().baseTransaction;
	  var cachedBaseTransaction = new CachedTransaction(new Transaction(std::move(baseTransaction)));
    
	  pushTransaction(cachedBaseTransaction, 0, 0, batch);
    
	  batch.insertCachedBlock(blockInfo, 0, {cachedBaseTransaction.getTransactionHash()});
	  batch.insertRawBlock(0, {toBinaryArray(genesisBlock.getBlock()), {}});
	  batch.insertClosestTimestampBlockIndex(GlobalMembers.roundToMidnight(genesisBlock.getBlock().timestamp), 0);
    
	  var res = database.write(batch);
	  if (res)
	  {
		logger(Logging.ERROR) << "addGenesisBlock failed: failed to write to database, " << res.message();
		throw new System.Exception(res.message());
	  }
    
	  topBlockHash = genesisBlock.getBlockHash();
    
	  unitsCache.push_back(blockInfo);
	}

  private enum OutputSearchResult : uint8_t
  {
	  FOUND,
	  NOT_FOUND,
	  INVALID_ARGUMENT
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TransactionValidatorState fillOutputsSpentByBlock(uint32_t blockIndex) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  TransactionValidatorState fillOutputsSpentByBlock(uint32_t blockIndex);

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public Crypto.Hash pushBlockToAnotherCache(IBlockchainCache segment, PushedBlockInfo && pushedBlockInfo)
	{
	  BlockTemplate block = new BlockTemplate();
	  bool br = fromBinaryArray(ref block, pushedBlockInfo.rawBlock.block);
	  if (br)
	  {
	  }
	  Debug.Assert(br);
    
	  List<CachedTransaction> transactions = new List<CachedTransaction>();
	  bool tr = Utils.restoreCachedTransactions(pushedBlockInfo.rawBlock.transactions, transactions);
	  if (tr)
	  {
	  }
	  Debug.Assert(tr);
    
	  CachedBlock cachedBlock = new CachedBlock(block);
	  segment.pushBlock(cachedBlock, transactions, pushedBlockInfo.validatorState, pushedBlockInfo.blockSize, pushedBlockInfo.generatedCoins, pushedBlockInfo.blockDifficulty, std::move(pushedBlockInfo.rawBlock));
    
	  return cachedBlock.getBlockHash();
	}
	public void requestDeleteSpentOutputs(BlockchainWriteBatch writeBatch, uint32_t blockIndex, TransactionValidatorState spentOutputs)
	{
	  logger(Logging.DEBUGGING) << "Deleting spent outputs for block index " << blockIndex;
    
	  List<Crypto.KeyImage> spentKeys = new List<Crypto.KeyImage>(spentOutputs.spentKeyImages.begin(), spentOutputs.spentKeyImages.end());
    
	  writeBatch.removeSpentKeyImages(blockIndex, spentKeys);
	}
	public List<Crypto.Hash> requestTransactionHashesFromBlockIndex(uint32_t splitBlockIndex)
	{
	  logger(Logging.DEBUGGING) << "Requesting transaction hashes starting from block index " << splitBlockIndex;
    
	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
	  for (uint32_t blockIndex = splitBlockIndex; blockIndex <= getTopBlockIndex(); ++blockIndex)
	  {
		readBatch.requestTransactionHashesByBlock(blockIndex);
	  }
    
	  List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
    
	  var dbResult = readDatabase(readBatch);
	  foreach (var kv in dbResult.getTransactionHashesByBlocks())
	  {
		foreach (var hash in kv.second)
		{
		  transactionHashes.emplace_back(hash);
		}
	  }
    
	  return transactionHashes;
	}
	public void requestDeleteTransactions(BlockchainWriteBatch writeBatch, List<Crypto.Hash> transactionHashes)
	{
	  foreach (var hash in transactionHashes)
	  {
		Debug.Assert(getCachedTransactionsCount() > 0);
		writeBatch.removeCachedTransaction(hash, getCachedTransactionsCount() - 1);
		transactionsCount = *transactionsCount - 1;
	  }
	}
	public void requestDeletePaymentIds(BlockchainWriteBatch writeBatch, List<Crypto.Hash> transactionHashes)
	{
	  Dictionary<Crypto.Hash, size_t> paymentCounts = new Dictionary<Crypto.Hash, size_t>();
    
	  foreach (var hash in transactionHashes)
	  {
		Crypto.Hash paymentId = new Crypto.Hash();
		if (!GlobalMembers.requestPaymentId(database, hash, paymentId))
		{
		  continue;
		}
    
		paymentCounts[paymentId] += 1;
	  }
    
	  foreach (var kv in paymentCounts)
	  {
		requestDeletePaymentId(writeBatch, kv.first, kv.second);
	  }
	}
	public void requestDeletePaymentId(BlockchainWriteBatch writeBatch, Crypto.Hash paymentId, size_t toDelete)
	{
	  size_t count = GlobalMembers.requestPaymentIdTransactionsCount(database, paymentId);
	  Debug.Assert(count > 0);
	  Debug.Assert(count >= toDelete);
    
	  logger(Logging.DEBUGGING) << "Deleting last " << toDelete << " transaction hashes of payment id " << paymentId;
	  writeBatch.removePaymentId(paymentId, (uint32_t)(count - toDelete));
	}
//C++ TO C# CONVERTER TODO TASK: The typedef 'GlobalOutputIndex' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Amount' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public void requestDeleteKeyOutputs(BlockchainWriteBatch writeBatch, SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> boundaries)
	{
	  if (boundaries.Count == 0)
	  {
		//hardly possible
		logger(Logging.DEBUGGING) << "No key output amounts...";
		return;
	  }
    
	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
	  foreach (var kv in boundaries)
	  {
		readBatch.requestKeyOutputGlobalIndexesCountForAmount(kv.first);
	  }
    
	  Dictionary<IBlockchainCache.Amount, uint32_t> amountCounts = readDatabase(readBatch).getKeyOutputGlobalIndexesCountForAmounts();
	  Debug.Assert(amountCounts.Count == boundaries.Count);
    
	  foreach (var kv in amountCounts)
	  {
		var it = boundaries.find(kv.first); //can't be equal end() since assert(amountCounts.size() == boundaries.size())
	//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		requestDeleteKeyOutputsAmount(writeBatch, kv.first, it.second, kv.second);
	  }
	}
//C++ TO C# CONVERTER TODO TASK: The typedef 'GlobalOutputIndex' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Amount' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	public void requestDeleteKeyOutputsAmount(BlockchainWriteBatch writeBatch, IBlockchainCache.Amount amount, IBlockchainCache.GlobalOutputIndex boundary, uint32_t outputsCount)
	{
	  logger(Logging.DEBUGGING) << "Requesting delete for key output amount " << amount << " starting from global index " << boundary << " to " << (outputsCount - 1);
    
	  writeBatch.removeKeyOutputGlobalIndexes(amount, outputsCount - boundary, boundary);
	  for (GlobalOutputIndex index = boundary; index < outputsCount; ++index)
	  {
		writeBatch.removeKeyOutputInfo(amount, index);
	  }
    
	  updateKeyOutputCount(amount, boundary - outputsCount);
	}
	public void requestRemoveTimestamp(BlockchainWriteBatch batch, uint64_t timestamp, Crypto.Hash blockHash)
	{
	  var readBatch = BlockchainReadBatch().requestBlockHashesByTimestamp(timestamp);
	  var result = readDatabase(readBatch);
    
	  if (result.getBlockHashesByTimestamp().count(timestamp) == 0)
	  {
		return;
	  }
    
	  var indexes = result.getBlockHashesByTimestamp().at(timestamp);
	  var it = std::find(indexes.begin(), indexes.end(), blockHash);
	  indexes.erase(it);
    
	  if (indexes.empty())
	  {
		logger(Logging.DEBUGGING) << "Deleting timestamp " << timestamp;
		batch.removeTimestamp(timestamp);
	  }
	  else
	  {
		logger(Logging.DEBUGGING) << "Deleting block hash " << blockHash << " from timestamp " << timestamp;
		batch.insertTimestamp(timestamp, indexes);
	  }
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint8_t getBlockMajorVersionForHeight(uint32_t height) const;
	public uint8_t getBlockMajorVersionForHeight(uint32_t height)
	{
	  UpgradeManager upgradeManager = new UpgradeManager();
	  upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_2, currency.upgradeHeight(BLOCK_MAJOR_VERSION_2));
	  upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_3, currency.upgradeHeight(BLOCK_MAJOR_VERSION_3));
	  return upgradeManager.getBlockMajorVersion(height);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getCachedTransactionsCount() const;
	public uint64_t getCachedTransactionsCount()
	{
	  if (!transactionsCount)
	  {
		var batch = BlockchainReadBatch().requestTransactionsCount();
		var result = database.read(batch);
    
		if (result)
		{
		  logger(Logging.ERROR) << "Failed to read transactions count from database";
		  throw std::system_error(result);
		}
    
		var readResult = batch.extractResult();
		if (!readResult.getTransactionsCount().second)
		{
		  logger(Logging.TRACE) << "Transactions count does not exist in database";
		  transactionsCount = 0;
		}
		else
		{
		  transactionsCount = readResult.getTransactionsCount().first;
		}
	  }
    
	  return *transactionsCount;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CachedBlockInfo> getLastCachedUnits(uint32_t blockIndex, size_t count, UseGenesis useGenesis) const;
	public List<CachedBlockInfo> getLastCachedUnits(uint32_t blockIndex, size_t count, UseGenesis useGenesis)
	{
	  Debug.Assert(blockIndex <= getTopBlockIndex());
    
	  List<CachedBlockInfo> cachedResult = new List<CachedBlockInfo>();
	  uint32_t cacheStartIndex = (getTopBlockIndex() + 1) - (uint32_t)unitsCache.size();
    
	  count = Math.Min(unitsCache.size(), count);
    
	  if (cacheStartIndex > blockIndex || count == 0)
	  {
		return cachedResult;
	  }
    
	  count = Math.Min(blockIndex - cacheStartIndex + 1, (uint32_t)count);
	  uint32_t offset = (uint32_t)(blockIndex + 1 - count) - cacheStartIndex;
    
	  Debug.Assert(offset < unitsCache.size());
    
	  if (useGenesis == null && cacheStartIndex == 0 && offset == 0)
	  {
		++offset;
		--count;
	  }
    
	  if (offset >= unitsCache.size() || count == 0)
	  {
		return cachedResult;
	  }
    
	  cachedResult.Capacity = count;
	  for (size_t i = 0; i < count; ++i)
	  {
		cachedResult.Add(unitsCache[offset + i]);
	  }
    
	  return cachedResult;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CachedBlockInfo> getLastDbUnits(uint32_t blockIndex, size_t count, UseGenesis useGenesis) const;
	public List<CachedBlockInfo> getLastDbUnits(uint32_t blockIndex, size_t count, UseGenesis useGenesis)
	{
	  uint32_t readFrom = blockIndex + 1 - Math.Min(blockIndex + 1, (uint32_t)count);
	  if (readFrom == 0 && useGenesis == null)
	  {
		readFrom += 1;
	  }
    
	  uint32_t toRead = blockIndex - readFrom + 1;
	  List<CachedBlockInfo> units = new List<CachedBlockInfo>();
	  units.Capacity = toRead;
    
	  const uint32_t step = 200;
	  while (toRead > 0)
	  {
		var next = Math.Min(toRead, step);
		toRead -= next;
    
		BlockchainReadBatch batch = new BlockchainReadBatch();
		for (var id = readFrom; id < readFrom + next; ++id)
		{
		  batch.requestCachedBlock(id);
		}
    
		readFrom += next;
    
		var res = readDatabase(batch);
    
		SortedDictionary<uint32_t, CachedBlockInfo> sortedResult = new SortedDictionary<uint32_t, CachedBlockInfo>(res.getCachedBlocks().begin(), res.getCachedBlocks().end());
		foreach (var kv in sortedResult)
		{
		  units.Add(kv.second);
		}
	//    std::transform(sortedResult.begin(), sortedResult.end(), std::back_inserter(units),
	//                   [&](const std::pair<uint32_t, CachedBlockInfo>& cb) { return pred(cb.second); });
	  }
    
	  return units;
	}
}
}

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ROCKSDB_USING_THREAD_STATUS !ROCKSDB_LITE && !NROCKSDB_THREAD_STATUS && !OS_MACOSX && !IOS_CROSS_COMPILE
// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.





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



namespace CryptoNote
{

public class OnceInInterval
{

  public OnceInInterval(uint interval, bool startNow = true)
  {
	  this.m_interval = interval;
	  this.m_lastCalled = startNow ? 0 : time(null);
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class F>
  public bool call<F>(F func)
  {
	time_t currentTime = time(null);

	if (currentTime - m_lastCalled > m_interval != null)
	{
	  bool res = func();
	  time(m_lastCalled);
	  return res;
	}

	return true;
  }

  private time_t m_lastCalled = new time_t();
  private time_t m_interval = new time_t();
}

}


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



namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{

public enum LevinError: int32_t
{
  OK = 0,
  ERROR_CONNECTION = -1,
  ERROR_CONNECTION_NOT_FOUND = -2,
  ERROR_CONNECTION_DESTROYED = -3,
  ERROR_CONNECTION_TIMEDOUT = -4,
  ERROR_CONNECTION_NO_DUPLEX_PROTOCOL = -5,
  ERROR_CONNECTION_HANDLER_NOT_DEFINED = -6,
  ERROR_FORMAT = -7,
}

public class LevinProtocol
{

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  LevinProtocol(System::TcpConnection connection);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public bool invoke<Request, Response>(uint32_t command, Request request, Response response)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	sendMessage(new uint32_t(command), encode(request), true);

	Command cmd = new Command();
	readCommand(cmd);

	if (!cmd.isResponse)
	{
	  return false;
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	return decode(cmd.buf, response);
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request>
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public void notify<Request>(uint32_t command, Request request, int UnnamedParameter)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	sendMessage(new uint32_t(command), encode(request), false);
  }

  public class Command
  {
	public uint32_t command = new uint32_t();
	public bool isNotify;
	public bool isResponse;
	public BinaryArray buf = new BinaryArray();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool needReply() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool needReply();
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool readCommand(Command cmd);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void sendMessage(uint32_t command, BinaryArray @out, bool needResponse);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void sendReply(uint32_t command, BinaryArray @out, int32_t returnCode);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public static bool decode<T>(BinaryArray buf, T value)
  {
	try
	{
	  Common.MemoryInputStream stream = new Common.MemoryInputStream(buf.data(), buf.size());
	  KVBinaryInputStreamSerializer serializer = new KVBinaryInputStreamSerializer(stream);
	  CryptoNote.GlobalMembers.serialize(value, serializer);
	}
	catch (System.Exception)
	{
	  return false;
	}

	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public static BinaryArray encode<T>(T value)
  {
	BinaryArray result = new BinaryArray();
	KVBinaryOutputStreamSerializer serializer = new KVBinaryOutputStreamSerializer();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	CryptoNote.GlobalMembers.serialize(const_cast<T&>(value), serializer.functorMethod);
	Common.VectorOutputStream stream = new Common.VectorOutputStream(result);
	serializer.dump(stream);
	return result;
  }


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool readStrict(uint8_t ptr, size_t size);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void writeStrict(uint8_t ptr, size_t size);
  private System.TcpConnection m_conn;
}

}

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




namespace CryptoNote
{

public class NetNodeConfig
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  NetNodeConfig();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool init(string interface, int port, int external, bool localIp, bool hidePort, string dataDir, ClassicVector<string> addPeers, ClassicVector<string> addExclusiveNodes, ClassicVector<string> addPriorityNodes, ClassicVector<string> addSeedNodes);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getP2pStateFilename() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  string getP2pStateFilename();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getTestnet() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool getTestnet();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getBindIp() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  string getBindIp();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getBindPort() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint16_t getBindPort();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getExternalPort() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint16_t getExternalPort();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getAllowLocalIp() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool getAllowLocalIp();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<PeerlistEntry> getPeers() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<PeerlistEntry> getPeers();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getPriorityNodes() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<NetworkAddress> getPriorityNodes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getExclusiveNodes() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<NetworkAddress> getExclusiveNodes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<NetworkAddress> getSeedNodes() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  ClassicVector<NetworkAddress> getSeedNodes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getHideMyPort() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool getHideMyPort();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getConfigFolder() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  string getConfigFolder();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setP2pStateFilename(string filename);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setTestnet(bool isTestnet);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setBindIp(string ip);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setBindPort(uint16_t port);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setExternalPort(uint16_t port);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setAllowLocalIp(bool allow);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setPeers(ClassicVector<PeerlistEntry> peerList);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setPriorityNodes(ClassicVector<NetworkAddress> addresses);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setExclusiveNodes(ClassicVector<NetworkAddress> addresses);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setSeedNodes(ClassicVector<NetworkAddress> addresses);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setHideMyPort(bool hide);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void setConfigFolder(string folder);

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

// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.






public class PeerlistManager
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		PeerlistManager();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool init(bool allow_local_ip);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t get_white_peers_count() const
		public size_t get_white_peers_count()
		{
			return m_peers_white.Count;
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t get_gray_peers_count() const
		public size_t get_gray_peers_count()
		{
			return m_peers_gray.Count;
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool merge_peerlist(ClassicLinkedList<PeerlistEntry> outer_bs);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool get_peerlist_head(ClassicLinkedList<PeerlistEntry> bs_head, uint32_t depth = CryptoNote::P2P_DEFAULT_PEERS_IN_HANDSHAKE);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_peerlist_full(ClassicLinkedList<PeerlistEntry>& pl_gray, ClassicLinkedList<PeerlistEntry>& pl_white) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool get_peerlist_full(ClassicLinkedList<PeerlistEntry> pl_gray, ClassicLinkedList<PeerlistEntry> pl_white);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_white_peer_by_index(PeerlistEntry& p, size_t i) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool get_white_peer_by_index(PeerlistEntry p, size_t i);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_gray_peer_by_index(PeerlistEntry& p, size_t i) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool get_gray_peer_by_index(PeerlistEntry p, size_t i);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool append_with_peer_white(PeerlistEntry pr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool append_with_peer_gray(PeerlistEntry pr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool set_peer_just_seen(uint64_t peer, uint32_t ip, uint32_t port);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool set_peer_just_seen(uint64_t peer, NetworkAddress addr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool set_peer_unreachable(PeerlistEntry pr);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool is_ip_allowed(uint32_t ip) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool is_ip_allowed(uint32_t ip);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void trim_white_peerlist();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void trim_gray_peerlist();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void serialize(CryptoNote::ISerializer s);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		Peerlist getWhite();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		Peerlist getGray();

		private string m_config_folder;
		private bool m_allow_local_ip;
		private List<PeerlistEntry> m_peers_gray = new List<PeerlistEntry>();
		private List<PeerlistEntry> m_peers_white = new List<PeerlistEntry>();
		private Peerlist m_whitePeerlist = new Peerlist();
		private Peerlist m_grayPeerlist = new Peerlist();
}


namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class LevinProtocol;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class ISerializer;

  public class P2pMessage
  {
	public enum Type
	{
	  COMMAND,
	  REPLY,
	  NOTIFY
	}

	public P2pMessage(Type type, uint32_t command, BinaryArray buffer, int32_t returnCode = 0)
	{
		this.type = new CryptoNote.P2pMessage.Type(type);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.command = command;
		this.command.CopyFrom(command);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.buffer = buffer;
		this.buffer.CopyFrom(buffer);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.returnCode = returnCode;
		this.returnCode.CopyFrom(returnCode);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pMessage(P2pMessage && msg)
	{
		this.type = new CryptoNote.P2pMessage.Type(msg.type);
		this.command = msg.command;
		this.buffer = std::move(msg.buffer);
		this.returnCode = msg.returnCode;
	}

	public size_t size()
	{
	  return buffer.size();
	}

	public Type type;
	public uint32_t command = new uint32_t();
	public readonly BinaryArray buffer = new BinaryArray();
	public int32_t returnCode = new int32_t();
  }

  public class P2pConnectionContext : CryptoNoteConnectionContext
  {

	public System.Context context;
	public uint64_t peerId = new uint64_t();
	public System.TcpConnection connection = new System.TcpConnection();

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pConnectionContext(System.Dispatcher dispatcher, Logging.ILogger log, System.TcpConnection && conn)
	{
		this.context = null;
		this.peerId = 0;
		this.connection = new System.TcpConnection(std::move(conn));
		this.logger = new Logging.LoggerRef(log, "node_server");
		this.queueEvent = new System.Event(dispatcher);
		this.stopped = false;
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public P2pConnectionContext(P2pConnectionContext && ctx) : base(std::move(ctx))
	{
		this.context = ctx.context;
		this.peerId = ctx.peerId;
		this.connection = new System.TcpConnection(std::move(ctx.connection));
		this.logger = new Logging.LoggerRef(ctx.logger.getLogger(), "node_server");
		this.queueEvent = new System.Event(std::move(ctx.queueEvent));
		this.stopped = std::move(ctx.stopped);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool pushMessage(P2pMessage&& msg);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	ClassicVector<P2pMessage> popBuffer();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void interrupt();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t writeDuration(std::chrono::steady_clock::time_point now) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	uint64_t writeDuration(std::chrono::steady_clock::time_point now);

	private Logging.LoggerRef logger = new Logging.LoggerRef();
	private std::chrono.steady_clock.time_point writeOperationStartTime = new std::chrono.steady_clock.time_point();
	private System.Event queueEvent = new System.Event();
	private List<P2pMessage> writeQueue = new List<P2pMessage>();
	private size_t writeQueueSize = 0;
	private bool stopped;
  }

  public class NodeServer : IP2pEndpoint
  {

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	static void init_options(boost::program_options::options_description desc);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	NodeServer(System::Dispatcher dispatcher, CryptoNote::CryptoNoteProtocolHandler payload_handler, Logging::ILogger log);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool run();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool init(NetNodeConfig config);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool deinit();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool sendStopSignal();
	public uint32_t get_this_peer_port()
	{
		return m_listeningPort;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	CryptoNote::CryptoNoteProtocolHandler get_payload_object();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void serialize(ISerializer s);

	// debug functions
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool log_peerlist();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool log_connections();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	uint64_t get_connections_count();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	size_t get_outgoing_connections_count();

	public PeerlistManager getPeerlistManager()
	{
		return m_peerlist;
	}


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handleCommand(LevinProtocol::Command cmd, BinaryArray buff_out, P2pConnectionContext context, ref bool handled);

	//----------------- commands handlers ----------------------------------------------
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_handshake(int command, COMMAND_HANDSHAKE::request arg, COMMAND_HANDSHAKE::response rsp, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_timed_sync(int command, COMMAND_TIMED_SYNC::request arg, COMMAND_TIMED_SYNC::response rsp, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_ping(int command, COMMAND_PING::request arg, COMMAND_PING::response rsp, P2pConnectionContext context);
#if ALLOW_DEBUG_COMMANDS
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_get_stat_info(int command, COMMAND_REQUEST_STAT_INFO::request arg, COMMAND_REQUEST_STAT_INFO::response rsp, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_get_network_state(int command, COMMAND_REQUEST_NETWORK_STATE::request arg, COMMAND_REQUEST_NETWORK_STATE::response rsp, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int handle_get_peer_id(int command, COMMAND_REQUEST_PEER_ID::request arg, COMMAND_REQUEST_PEER_ID::response rsp, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool check_trust(proof_of_trust tr);
#endif

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool init_config();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool make_default_config();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool store_config();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void initUpnp();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handshake(CryptoNote::LevinProtocol proto, P2pConnectionContext context, bool just_take_peerlist = false);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool timedSync();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handleTimedSyncResponse(BinaryArray in, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void forEachConnection(System.Action<P2pConnectionContext > action);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void on_connection_new(P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void on_connection_close(P2pConnectionContext context);

	//----------------- i_p2p_endpoint -------------------------------------------------------------
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void relay_notify_to_all(int command, BinaryArray data_buff, boost::uuids::uuid excludeConnection);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool invoke_notify_to_peer(int command, BinaryArray req_buff, CryptoNoteConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void for_each_connection(System.Action<CryptoNote::CryptoNoteConnectionContext , uint64_t> f);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void externalRelayNotifyToAll(int command, BinaryArray data_buff, boost::uuids::uuid excludeConnection);

	//-----------------------------------------------------------------------------------------------
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handle_command_line(boost::program_options::variables_map vm);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handleConfig(NetNodeConfig config);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool append_net_address(ClassicVector<NetworkAddress> nodes, string addr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool idle_worker();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool handle_remote_peerlist(ClassicLinkedList<PeerlistEntry> peerlist, time_t local_time, CryptoNoteConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool get_local_node_data(basic_node_data node_data);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool merge_peerlist_with_local(ClassicLinkedList<PeerlistEntry> bs);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool fix_time_delta(ClassicLinkedList<PeerlistEntry> local_peerlist, time_t local_time, int64_t delta);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool connections_maker();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool make_new_connection_from_peerlist(bool use_white_list);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool try_to_connect_and_handshake_with_new_peer(NetworkAddress na, bool just_take_peerlist = false, uint64_t last_seen_stamp = 0, bool white = true);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool is_peer_used(PeerlistEntry peer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool is_addr_connected(NetworkAddress peer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool try_ping(basic_node_data node_data, P2pConnectionContext context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool make_expected_connections_count(bool white_list, size_t expected_connections);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool is_priority_node(NetworkAddress na);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool connect_to_peerlist(ClassicVector<NetworkAddress> peers);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool parse_peers_and_add_to_container(boost::program_options::variables_map vm, command_line::arg_descriptor<ClassicVector<string>> arg, ClassicVector<NetworkAddress> container);

	//debug functions
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	string print_connections_container();

	private Dictionary<boost::uuids.uuid, P2pConnectionContext, boost::hash<boost::uuids.uuid>> m_connections = new Dictionary<boost::uuids.uuid, P2pConnectionContext, boost::hash<boost::uuids.uuid>>();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void acceptLoop();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void connectionHandler(boost::uuids::uuid connectionId, P2pConnectionContext connection);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void writeHandler(P2pConnectionContext ctx);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void onIdle();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void timedSyncLoop();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void timeoutLoop();

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void safeInterrupt<T>(T obj);

	private class config <T>
	{
	  public network_config m_net_config = new network_config();
	  public uint64_t m_peer_id = new uint64_t();

	  public void serialize(ISerializer s)
	  {
		s.functorMethod(m_net_config, "m_net_config");
		s.functorMethod(m_peer_id, "m_peer_id");
	  }
	}

	private config m_config = new config();
	private string m_config_folder;

	private bool m_have_address;
	private bool m_first_connection_maker_call;
	private uint32_t m_listeningPort = new uint32_t();
	private uint32_t m_external_port = new uint32_t();
	private uint32_t m_ip_address = new uint32_t();
	private bool m_allow_local_ip;
	private bool m_hide_my_port;
	private string m_p2p_state_filename;

	private System.Dispatcher m_dispatcher;
	private System.ContextGroup m_workingContextGroup = new System.ContextGroup();
	private System.Event m_stopEvent = new System.Event();
	private System.Timer m_idleTimer = new System.Timer();
	private System.Timer m_timeoutTimer = new System.Timer();
	private System.TcpListener m_listener = new System.TcpListener();
	private Logging.LoggerRef logger = new Logging.LoggerRef();
	private std::atomic<bool> m_stop = new std::atomic<bool>();

	private CryptoNoteProtocolHandler m_payload_handler;
	private PeerlistManager m_peerlist = new PeerlistManager();

	// OnceInInterval m_peer_handshake_idle_maker_interval;
	private OnceInInterval m_connections_maker_interval = new OnceInInterval();
	private OnceInInterval m_peerlist_store_interval = new OnceInInterval();
	private System.Timer m_timedSyncTimer = new System.Timer();

	private string m_bind_ip;
	private string m_port;
#if ALLOW_DEBUG_COMMANDS
	private uint64_t m_last_stat_request_time = new uint64_t();
#endif
	private List<NetworkAddress> m_priority_peers = new List<NetworkAddress>();
	private List<NetworkAddress> m_exclusive_peers = new List<NetworkAddress>();
	private List<NetworkAddress> m_seed_nodes = new List<NetworkAddress>();
	private LinkedList<PeerlistEntry> m_command_line_peers = new LinkedList<PeerlistEntry>();
	private uint64_t m_peer_livetime = new uint64_t();
	private boost::uuids.uuid m_network_id = new boost::uuids.uuid();
  }
}



#if WIN32
#else
#endif