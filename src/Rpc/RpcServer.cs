using JsonRpc;


// CryptoNote
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH


using Logging;
using Crypto;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// Copyright (c) 2018, The Karai Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// Copyright (c) 2018, The Karai Developers
//
// Please see the included LICENSE file for more information.




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
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct ICryptoNoteProtocolHandler;

public class RpcServer : HttpServer
{
  public RpcServer(System.Dispatcher dispatcher, Logging.ILogger log, Core c, NodeServer p2p, ICryptoNoteProtocolHandler protocol) : base(dispatcher, log)
  {
	  this.logger = new Logging.LoggerRef(log, "RpcServer");
	  this.m_core = new CryptoNote.Core(c);
	  this.m_p2p = new CryptoNote.NodeServer(p2p);
	  this.m_protocol = new CryptoNote.ICryptoNoteProtocolHandler(protocol);
  }

  public delegate bool HandlerFunction(RpcServer UnnamedParameter, HttpRequest request, HttpResponse response);
  public bool enableCors(List<string> domains)
  {
	m_cors_domains = new List<string>(domains);
	return true;
  }
  public bool setFeeAddress(string fee_address)
  {
	m_fee_address = fee_address;
	return true;
  }
  public bool setFeeAmount(uint32_t fee_amount)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_fee_amount = fee_amount;
	m_fee_amount.CopyFrom(fee_amount);
	return true;
  }
  public List<string> getCorsDomains()
  {
	return m_cors_domains;
  }

  public bool on_get_block_headers_range(COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.request req, COMMAND_RPC_GET_BLOCK_HEADERS_RANGE.response res, JsonRpc.JsonRpcError error_resp)
  {
	  // TODO: change usage to jsonRpcHandlers?
	  uint64_t bc_height = m_core.get_current_blockchain_height();
	  if (req.start_height > bc_height || req.end_height >= bc_height != null || req.start_height > req.end_height)
	  {
		  error_resp.code = DefineConstants.CORE_RPC_ERROR_CODE_TOO_BIG_HEIGHT;
		  error_resp.message = "Invalid start/end heights.";
		  return false;
	  }

	  for (uint32_t h = (uint32_t)req.start_height; h <= (uint32_t)req.end_height; ++h)
	  {
		  Crypto.Hash block_hash = m_core.getBlockHashByIndex(new uint32_t(h));
		  CryptoNote.BlockTemplate blk = m_core.getBlockByHash(block_hash);

		  res.headers.Add(new block_header_response());
		  fill_block_header_response(blk, false, new uint32_t(h), block_hash, res.headers[res.headers.Count - 1]);

		  // TODO: Error handling like in monero?
		  /*block blk;
		  bool have_block = m_core.get_block_by_hash(block_hash, blk);
		  if (!have_block)
		  {
		  	error_resp.code = CORE_RPC_ERROR_CODE_INTERNAL_ERROR;
		  	error_resp.message = "Internal error: can't get block by height. Height = " + boost::lexical_cast<std::string>(h) + ". Hash = " + epee::string_tools::pod_to_hex(block_hash) + '.';
		  	return false;
		  }
		  if (blk.miner_tx.vin.size() != 1 || blk.miner_tx.vin.front().type() != typeid(txin_gen))
		  {
		  	error_resp.code = CORE_RPC_ERROR_CODE_INTERNAL_ERROR;
		  	error_resp.message = "Internal error: coinbase transaction in the block has the wrong type";
		  	return false;
		  }
		  uint64_t block_height = boost::get<txin_gen>(blk.miner_tx.vin.front()).height;
		  if (block_height != h)
		  {
		  	error_resp.code = CORE_RPC_ERROR_CODE_INTERNAL_ERROR;
		  	error_resp.message = "Internal error: coinbase transaction in the block has the wrong height";
		  	return false;
		  }
		  res.headers.push_back(block_header_response());
		  bool response_filled = fill_block_header_response(blk, false, block_height, block_hash, res.headers.back());
		  if (!response_filled)
		  {
		  	error_resp.code = CORE_RPC_ERROR_CODE_INTERNAL_ERROR;
		  	error_resp.message = "Internal error: can't produce valid response.";
		  	return false;
		  }*/
	  }

	  res.status = DefineConstants.CORE_RPC_STATUS_OK;
	  return true;
  }

  //
  // JSON handlers
  //

  public bool on_get_info(COMMAND_RPC_GET_INFO.request req, COMMAND_RPC_GET_INFO.response res)
  {
	res.height = m_core.getTopBlockIndex() + 1;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.difficulty = m_core.getDifficultyForNextBlock();
	res.difficulty.CopyFrom(m_core.getDifficultyForNextBlock());
	res.tx_count = m_core.getBlockchainTransactionCount() - res.height; //without coinbase
	res.tx_pool_size = m_core.getPoolTransactionCount();
	res.alt_blocks_count = m_core.getAlternativeBlockCount();
	uint64_t total_conn = m_p2p.get_connections_count();
	res.outgoing_connections_count = m_p2p.get_outgoing_connections_count();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.incoming_connections_count = total_conn - res.outgoing_connections_count;
	res.incoming_connections_count.CopyFrom(total_conn - res.outgoing_connections_count);
	res.white_peerlist_size = m_p2p.getPeerlistManager().get_white_peers_count();
	res.grey_peerlist_size = m_p2p.getPeerlistManager().get_gray_peers_count();
	res.last_known_block_index = Math.Max((uint32_t)1, m_protocol.getObservedHeight()) - 1;
	res.network_height = Math.Max((uint32_t)1, m_protocol.getBlockchainHeight());
	res.upgrade_heights = new List<uint64_t>(CryptoNote.parameters.FORK_HEIGHTS_SIZE == 0 ? new List<uint64_t>() : new List<uint64_t>(CryptoNote.parameters.FORK_HEIGHTS, CryptoNote.parameters.FORK_HEIGHTS + CryptoNote.parameters.FORK_HEIGHTS_SIZE));
	res.supported_height = CryptoNote.parameters.FORK_HEIGHTS_SIZE == 0 ? 0 : CryptoNote.parameters.FORK_HEIGHTS[CryptoNote.parameters.CURRENT_FORK_INDEX];
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.hashrate = (uint32_t)round(res.difficulty / CryptoNote::parameters::DIFFICULTY_TARGET);
	res.hashrate.CopyFrom((uint32_t)Math.Round(res.difficulty / CryptoNote.parameters.DIFFICULTY_TARGET));
	res.synced = ((uint64_t)res.height == (uint64_t)res.network_height);
	res.testnet = m_core.getCurrency().isTestnet();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.major_version = m_core.getBlockDetails(m_core.getTopBlockIndex()).majorVersion;
	res.major_version.CopyFrom(m_core.getBlockDetails(m_core.getTopBlockIndex()).majorVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.minor_version = m_core.getBlockDetails(m_core.getTopBlockIndex()).minorVersion;
	res.minor_version.CopyFrom(m_core.getBlockDetails(m_core.getTopBlockIndex()).minorVersion);
	res.version = PROJECT_VERSION;
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.start_time = (uint64_t)m_core.getStartTime();
	res.start_time.CopyFrom((uint64_t)m_core.getStartTime());
	return true;
  }


//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class Handler>
  private class RpcHandler <Handler>
  {
	public readonly Handler handler = new Handler();
	public readonly bool allowBusyCore;
  }

  private delegate void HandlerPtr(HttpRequest request, HttpResponse response);
  private static Dictionary<string, RpcHandler<HandlerFunction>> s_handlers = new Dictionary<string, RpcHandler<HandlerFunction>>();

  private override void processRequest(HttpRequest request, HttpResponse response)
  {
	var url = request.getUrl();
	if (url.find(".bin") == -1)
	{
		logger.functorMethod(TRACE) << "RPC request came: \n" << request << std::endl;
	}
	else
	{
		logger.functorMethod(TRACE) << "RPC request came: " << url << std::endl;
	}

	var it = GlobalMembers.s_handlers.find(url);
	if (it == GlobalMembers.s_handlers.end())
	{
	  response.setStatus(HttpResponse.STATUS_404);
	  return;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (!it.second.allowBusyCore && !isCoreReady())
	{
	  response.setStatus(HttpResponse.STATUS_500);
	  response.setBody("Core is busy");
	  return;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	it.second.handler(this, request, response);
  }
//C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
  private Dictionary<string, RpcServer.RpcHandler<JsonMemberMethod>> processJsonRpcRequest_jsonRpcHandlers = new Dictionary<string, RpcServer.RpcHandler<JsonMemberMethod>>()
  {
	  {
		  "f_blocks_list_json", {makeMemberMethod(RpcServer.f_on_blocks_list_json), false}
	  },
	  {
		  "f_block_json", {makeMemberMethod(RpcServer.f_on_block_json), false}
	  },
	  {
		  "f_transaction_json", {makeMemberMethod(RpcServer.f_on_transaction_json), false}
	  },
	  {
		  "f_on_transactions_pool_json", {makeMemberMethod(RpcServer.f_on_transactions_pool_json), false}
	  },
	  {
		  "getblockcount", {makeMemberMethod(RpcServer.on_getblockcount), true}
	  },
	  {
		  "on_getblockhash", {makeMemberMethod(RpcServer.on_getblockhash), false}
	  },
	  {
		  "getblocktemplate", {makeMemberMethod(RpcServer.on_getblocktemplate), false}
	  },
	  {
		  "getcurrencyid", {makeMemberMethod(RpcServer.on_get_currency_id), true}
	  },
	  {
		  "submitblock", {makeMemberMethod(RpcServer.on_submitblock), false}
	  },
	  {
		  "getlastblockheader", {makeMemberMethod(RpcServer.on_get_last_block_header), false}
	  },
	  {
		  "getblockheaderbyhash", {makeMemberMethod(RpcServer.on_get_block_header_by_hash), false}
	  },
	  {
		  "getblockheaderbyheight", {makeMemberMethod(RpcServer.on_get_block_header_by_height), false}
	  }
  };
  private bool processJsonRpcRequest(HttpRequest request, HttpResponse response)
  {


	foreach (var cors_domain in m_cors_domains)
	{
	  response.addHeader("Access-Control-Allow-Origin", cors_domain);
	}
	response.addHeader("Content-Type", "application/json");

	JsonRpcRequest jsonRequest = new JsonRpcRequest();
	JsonRpcResponse jsonResponse = new JsonRpcResponse();

	try
	{
	  logger.functorMethod(TRACE) << "JSON-RPC request: " << request.getBody();
	  jsonRequest.parseRequest(request.getBody());
	  jsonResponse.setId(jsonRequest.getId()); // copy id

//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
//	  static ClassicUnorderedMap<string, RpcServer::RpcHandler<JsonMemberMethod>> jsonRpcHandlers = { { "f_blocks_list_json", { makeMemberMethod(&RpcServer::f_on_blocks_list_json), false } }, { "f_block_json", { makeMemberMethod(&RpcServer::f_on_block_json), false } }, { "f_transaction_json", { makeMemberMethod(&RpcServer::f_on_transaction_json), false } }, { "f_on_transactions_pool_json", { makeMemberMethod(&RpcServer::f_on_transactions_pool_json), false } }, { "getblockcount", { makeMemberMethod(&RpcServer::on_getblockcount), true } }, { "on_getblockhash", { makeMemberMethod(&RpcServer::on_getblockhash), false } }, { "getblocktemplate", { makeMemberMethod(&RpcServer::on_getblocktemplate), false } }, { "getcurrencyid", { makeMemberMethod(&RpcServer::on_get_currency_id), true } }, { "submitblock", { makeMemberMethod(&RpcServer::on_submitblock), false } }, { "getlastblockheader", { makeMemberMethod(&RpcServer::on_get_last_block_header), false } }, { "getblockheaderbyhash", { makeMemberMethod(&RpcServer::on_get_block_header_by_hash), false } }, { "getblockheaderbyheight", { makeMemberMethod(&RpcServer::on_get_block_header_by_height), false } } };

	  var it = processJsonRpcRequest_jsonRpcHandlers.find(jsonRequest.getMethod());
	  if (it == processJsonRpcRequest_jsonRpcHandlers.end())
	  {
		throw new JsonRpcError(JsonRpc.GlobalMembers.errMethodNotFound);
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (!it.second.allowBusyCore && !isCoreReady())
	  {
		throw new JsonRpcError(DefineConstants.CORE_RPC_ERROR_CODE_CORE_BUSY, "Core is busy");
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.handler(this, jsonRequest, jsonResponse);

	}
	catch (JsonRpcError err)
	{
	  jsonResponse.setError(err);
	}
	catch (System.Exception e)
	{
	  jsonResponse.setError(new JsonRpcError(JsonRpc.GlobalMembers.errInternalError, e.Message));
	}

	response.setBody(jsonResponse.getBody());
	logger.functorMethod(TRACE) << "JSON-RPC response: " << jsonResponse.getBody();
	return true;
  }
  private bool isCoreReady()
  {
	return m_core.getCurrency().isTestnet() || m_p2p.get_payload_object().isSynchronized();
  }

  // json handlers
  private bool on_get_blocks(COMMAND_RPC_GET_BLOCKS_FAST.request req, COMMAND_RPC_GET_BLOCKS_FAST.response res)
  {
	// TODO code duplication see InProcessNode::doGetNewBlocks()
	if (req.block_ids.Count == 0)
	{
	  res.status = "Failed";
	  return false;
	}

	if (req.block_ids[req.block_ids.Count - 1] != m_core.getBlockHashByIndex(0))
	{
	  res.status = "Failed";
	  return false;
	}

	uint32_t totalBlockCount = new uint32_t();
	uint32_t startBlockIndex = new uint32_t();
	List<Crypto.Hash> supplement = m_core.findBlockchainSupplement(req.block_ids, COMMAND_RPC_GET_BLOCKS_FAST_MAX_COUNT, totalBlockCount, startBlockIndex);

	res.current_height = totalBlockCount;
	res.start_height = startBlockIndex;

	List<Crypto.Hash> missedHashes = new List<Crypto.Hash>();
	m_core.getBlocks(supplement, res.blocks, missedHashes);
	Debug.Assert(missedHashes.Count == 0);

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_query_blocks(COMMAND_RPC_QUERY_BLOCKS.request req, COMMAND_RPC_QUERY_BLOCKS.response res)
  {
	uint32_t startIndex = new uint32_t();
	uint32_t currentIndex = new uint32_t();
	uint32_t fullOffset = new uint32_t();

	if (!m_core.queryBlocks(req.block_ids, new uint64_t(req.timestamp), startIndex, currentIndex, fullOffset, res.items))
	{
	  res.status = "Failed to perform query";
	  return false;
	}

	res.start_height = startIndex + 1;
	res.current_height = currentIndex + 1;
	res.full_offset = fullOffset;
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_query_blocks_lite(COMMAND_RPC_QUERY_BLOCKS_LITE.request req, COMMAND_RPC_QUERY_BLOCKS_LITE.response res)
  {
	uint32_t startIndex = new uint32_t();
	uint32_t currentIndex = new uint32_t();
	uint32_t fullOffset = new uint32_t();
	if (!m_core.queryBlocksLite(req.blockIds, new uint64_t(req.timestamp), startIndex, currentIndex, fullOffset, res.items))
	{
	  res.status = "Failed to perform query";
	  return false;
	}

	res.startHeight = startIndex;
	res.currentHeight = currentIndex;
	res.fullOffset = fullOffset;
	res.status = DefineConstants.CORE_RPC_STATUS_OK;

	return true;
  }
  private bool on_get_indexes(COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES.request req, COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES.response res)
  {
	List<uint32_t> outputIndexes = new List<uint32_t>();
	if (!m_core.getTransactionGlobalIndexes(req.txid, outputIndexes))
	{
	  res.status = "Failed";
	  return true;
	}

	res.o_indexes.assign(outputIndexes.GetEnumerator(), outputIndexes.end());
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	logger.functorMethod(TRACE) << "COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES: [" << res.o_indexes.Count << "]";
	return true;
  }
  private bool on_get_random_outs(COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.request req, COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.response res)
  {
	res.status = "Failed";

	foreach (uint64_t amount in req.amounts)
	{
	  List<uint32_t> globalIndexes = new List<uint32_t>();
	  List<Crypto.PublicKey> publicKeys = new List<Crypto.PublicKey>();
	  if (!m_core.getRandomOutputs(new uint64_t(amount), (uint16_t)req.outs_count, globalIndexes, publicKeys))
	  {
		return true;
	  }

	  Debug.Assert(globalIndexes.Count == publicKeys.Count);
	  res.outs.emplace_back(new COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount({amount, {}}));
	  for (size_t i = 0; i < globalIndexes.Count; ++i)
	  {
		res.outs.back().outs.push_back({globalIndexes[i], publicKeys[i]});
	  }
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;

	std::stringstream ss = new std::stringstream();

	std::for_each(res.outs.begin(), res.outs.end(), (COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount ofa) =>
	{
	  ss << "[" << ofa.amount << "]:";

	  Debug.Assert(ofa.outs.Count && "internal error: ofa.outs.size() is empty");

  //C++ TO C# CONVERTER TODO TASK: The typedef 'out_entry' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  ofa.outs.ForEach((out_entry oe) =>
	  {
		ss << oe.global_amount_index << " ";
	  });
	  ss << std::endl;
	});
	string s = ss.str();
	logger.functorMethod(TRACE) << "COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS: " << std::endl << s;
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetPoolChanges(COMMAND_RPC_GET_POOL_CHANGES.request req, COMMAND_RPC_GET_POOL_CHANGES.response rsp)
  {
	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	rsp.isTailBlockActual = m_core.getPoolChanges(req.tailBlockId, req.knownTxsIds, rsp.addedTxs, rsp.deletedTxsIds);

	return true;
  }
  private bool onGetPoolChangesLite(COMMAND_RPC_GET_POOL_CHANGES_LITE.request req, COMMAND_RPC_GET_POOL_CHANGES_LITE.response rsp)
  {
	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	rsp.isTailBlockActual = m_core.getPoolChangesLite(req.tailBlockId, req.knownTxsIds, rsp.addedTxs, rsp.deletedTxsIds);

	return true;
  }
  private bool onGetBlocksDetailsByHeights(COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS.request req, COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS.response rsp)
  {
	try
	{
	  List<BlockDetails> blockDetails = new List<BlockDetails>();
	  foreach (uint32_t height in req.blockHeights)
	  {
		blockDetails.Add(m_core.getBlockDetails(height));
	  }

	  rsp.blocks = std::move(blockDetails);
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetBlocksDetailsByHashes(COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES.request req, COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES.response rsp)
  {
	try
	{
	  List<BlockDetails> blockDetails = new List<BlockDetails>();
	  foreach (Crypto  in :Hash & hash : req.blockHashes)
	  {
		blockDetails.Add(m_core.getBlockDetails(hash));
	  }

	  rsp.blocks = std::move(blockDetails);
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetBlockDetailsByHeight(COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT.request req, COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT.response rsp)
  {
	try
	{
	  BlockDetails blockDetails = m_core.getBlockDetails(req.blockHeight);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rsp.block = blockDetails;
	  rsp.block.CopyFrom(blockDetails);
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetBlocksHashesByTimestamps(COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS.request req, COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS.response rsp)
  {
	try
	{
	  var blockHashes = m_core.getBlockHashesByTimestamps(new uint64_t(req.timestampBegin), new uint64_t(req.secondsCount));
	  rsp.blockHashes = std::move(blockHashes);
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetTransactionDetailsByHashes(COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES.request req, COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES.response rsp)
  {
	try
	{
	  List<TransactionDetails> transactionDetails = new List<TransactionDetails>();
	  transactionDetails.Capacity = req.transactionHashes.Count;

	  foreach (var hash in req.transactionHashes)
	  {
		transactionDetails.Add(m_core.getTransactionDetails(hash));
	  }

	  rsp.transactions = std::move(transactionDetails);
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool onGetTransactionHashesByPaymentId(COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID.request req, COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID.response rsp)
  {
	try
	{
	  rsp.transactionHashes = new List<Crypto.Hash>(m_core.getTransactionHashesByPaymentId(req.paymentId));
	}
	catch (std::system_error e)
	{
	  rsp.status = e.what();
	  return false;
	}
	catch (System.Exception e)
	{
	  rsp.status = "Error: " + (string)e.Message;
	  return false;
	}

	rsp.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_height(COMMAND_RPC_GET_HEIGHT.request req, COMMAND_RPC_GET_HEIGHT.response res)
  {
	res.height = m_core.getTopBlockIndex() + 1;
	res.network_height = Math.Max((uint32_t)1, m_protocol.getBlockchainHeight());
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_transactions(COMMAND_RPC_GET_TRANSACTIONS.request req, COMMAND_RPC_GET_TRANSACTIONS.response res)
  {
	List<Hash> vh = new List<Hash>();
	foreach (var tx_hex_str in req.txs_hashes)
	{
	  BinaryArray b = new BinaryArray();
	  if (!fromHex(tx_hex_str, b))
	  {
		res.status = "Failed to parse hex representation of transaction hash";
		return true;
	  }

	  if (b.size() != sizeof(Hash))
	  {
		res.status = "Failed, size of data mismatch";
	  }

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  vh.Add(*reinterpret_cast<const Hash>(b.data()));
	}

	List<Hash> missed_txs = new List<Hash>();
	List<BinaryArray> txs = new List<BinaryArray>();
	m_core.getTransactions(vh, txs, missed_txs);

	foreach (var tx in txs)
	{
	  res.txs_as_hex.Add(toHex(tx));
	}

	foreach (var miss_tx in missed_txs)
	{
	  res.missed_tx.Add(Common.GlobalMembers.podToHex(miss_tx));
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_send_raw_tx(COMMAND_RPC_SEND_RAW_TX.request req, COMMAND_RPC_SEND_RAW_TX.response res)
  {
	List<BinaryArray> transactions = new List<BinaryArray>(1);
	if (!fromHex(req.tx_as_hex, transactions[transactions.Count - 1]))
	{
	  logger.functorMethod(INFO) << "[on_send_raw_tx]: Failed to parse tx from hexbuff: " << req.tx_as_hex;
	  res.status = "Failed";
	  return true;
	}

	Crypto.Hash transactionHash = Crypto.GlobalMembers.cn_fast_hash(transactions[transactions.Count - 1].data(), transactions[transactions.Count - 1].size());
	logger.functorMethod(DEBUGGING) << "transaction " << transactionHash << " came in on_send_raw_tx";

	if (!m_core.addTransactionToPool(transactions[transactions.Count - 1]))
	{
	  logger.functorMethod(DEBUGGING) << "[on_send_raw_tx]: tx verification failed";
	  res.status = "Failed";
	  return true;
	}

	m_protocol.relayTransactions(transactions);
	//TODO: make sure that tx has reached other nodes here, probably wait to receive reflections from other nodes
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_fee_info(EMPTY_STRUCT req, COMMAND_RPC_GET_FEE_ADDRESS.response res)
  {
	if (string.IsNullOrEmpty(m_fee_address))
	{
	  res.status = DefineConstants.CORE_RPC_STATUS_OK;
	  return false;
	}

	res.address = m_fee_address;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.amount = m_fee_amount;
	res.amount.CopyFrom(m_fee_amount);
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_peers(COMMAND_RPC_GET_PEERS.request req, COMMAND_RPC_GET_PEERS.response res)
  {
	LinkedList<PeerlistEntry> peers_white = new LinkedList<PeerlistEntry>();
	LinkedList<PeerlistEntry> peers_gray = new LinkedList<PeerlistEntry>();

	m_p2p.getPeerlistManager().get_peerlist_full(peers_gray, peers_white);

	foreach (var peer in peers_white)
	{
	  std::stringstream stream = new std::stringstream();
	  stream << peer.adr;
	  res.peers.Add(stream.str());
	}

	foreach (var peer in peers_gray)
	{
	  std::stringstream stream = new std::stringstream();
	  stream << peer.adr;
	  res.gray_peers.Add(stream.str());
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }

  // json rpc
  private bool on_getblockcount(COMMAND_RPC_GETBLOCKCOUNT.request req, COMMAND_RPC_GETBLOCKCOUNT.response res)
  {
	res.count = m_core.getTopBlockIndex() + 1;
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_getblockhash(COMMAND_RPC_GETBLOCKHASH.request req, ref COMMAND_RPC_GETBLOCKHASH.response res)
  {
	if (req.size() != 1)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "Wrong parameters, expected height"));
	}

	uint32_t h = (uint32_t)req[0];
	Crypto.Hash blockId = m_core.getBlockHashByIndex(h - 1);
	if (blockId == GlobalMembers.NULL_HASH)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_TOO_BIG_HEIGHT, "Too big height: " + Convert.ToString(h) + ", current blockchain height = " + Convert.ToString(m_core.getTopBlockIndex() + 1)));
	}

	res = Common.GlobalMembers.podToHex(blockId);
	return true;
  }
  private bool on_getblocktemplate(COMMAND_RPC_GETBLOCKTEMPLATE.request req, COMMAND_RPC_GETBLOCKTEMPLATE.response res)
  {
	if (req.reserve_size > DefineConstants.TX_EXTRA_NONCE_MAX_COUNT)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_TOO_BIG_RESERVE_SIZE, "To big reserved size, maximum 255"));
	}

	AccountPublicAddress acc = boost::value_initialized<AccountPublicAddress>();

	if (!req.wallet_address.Length || !m_core.getCurrency().parseAccountAddressString(req.wallet_address, acc))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_WALLET_ADDRESS, "Failed to parse wallet address"));
	}

	BlockTemplate blockTemplate = boost::value_initialized<BlockTemplate>();
	List<uint8_t> blob_reserve = new List<uint8_t>();
	blob_reserve.Resize(req.reserve_size, 0);

	if (!m_core.getBlockTemplate(blockTemplate, acc, blob_reserve, res.difficulty, res.height))
	{
	  logger.functorMethod(ERROR) << "Failed to create block template";
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: failed to create block template"));
	}

	BinaryArray block_blob = CryptoNote.GlobalMembers.toBinaryArray(blockTemplate);
	PublicKey tx_pub_key = CryptoNote.getTransactionPublicKeyFromExtra(blockTemplate.baseTransaction.extra);
	if (tx_pub_key == GlobalMembers.NULL_PUBLIC_KEY)
	{
	  logger.functorMethod(ERROR) << "Failed to find tx pub key in coinbase extra";
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: failed to find tx pub key in coinbase extra"));
	}

	if (0 < req.reserve_size)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.reserved_offset = slow_memmem((object*)block_blob.data(), block_blob.size(), &tx_pub_key, sizeof(tx_pub_key));
	  res.reserved_offset.CopyFrom(GlobalMembers.slow_memmem((object)block_blob.data(), block_blob.size(), tx_pub_key, sizeof(PublicKey)));
	  if (res.reserved_offset == null)
	  {
		logger.functorMethod(ERROR) << "Failed to find tx pub key in blockblob";
		throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: failed to create block template"));
	  }
	  res.reserved_offset += sizeof(PublicKey) + 3; //3 bytes: tag for TX_EXTRA_TAG_PUBKEY(1 byte), tag for TX_EXTRA_NONCE(1 byte), counter in TX_EXTRA_NONCE(1 byte)
	  if (res.reserved_offset + req.reserve_size > block_blob.size() != null)
	  {
		logger.functorMethod(ERROR) << "Failed to calculate offset for reserved bytes";
		throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: failed to create block template"));
	  }
	}
	else
	{
	  res.reserved_offset = 0;
	}

	res.blocktemplate_blob = toHex(block_blob);
	res.status = DefineConstants.CORE_RPC_STATUS_OK;

	return true;
  }
  private bool on_get_currency_id(COMMAND_RPC_GET_CURRENCY_ID.request req, COMMAND_RPC_GET_CURRENCY_ID.response res)
  {
	Hash genesisBlockHash = m_core.getCurrency().genesisBlockHash();
	res.currency_id_blob = Common.GlobalMembers.podToHex(genesisBlockHash);
	return true;
  }
  private bool on_submitblock(COMMAND_RPC_SUBMITBLOCK.request req, COMMAND_RPC_SUBMITBLOCK.response res)
  {
	if (req.size() != 1)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "Wrong param"));
	}

	BinaryArray blockblob = new BinaryArray();
	if (!fromHex(req[0], blockblob))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_BLOCKBLOB, "Wrong block blob"));
	}

	var blockToSend = blockblob;
	var submitResult = m_core.submitBlock(std::move(blockblob));
	if (submitResult != error.AddBlockErrorCondition.BLOCK_ADDED)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_BLOCK_NOT_ACCEPTED, "Block not accepted"));
	}

	if (submitResult == error.AddBlockErrorCode.ADDED_TO_MAIN || submitResult == error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED)
	{
	  NOTIFY_NEW_BLOCK.request newBlockMessage = new NOTIFY_NEW_BLOCK.request();
	  newBlockMessage.b = prepareRawBlockLegacy(std::move(blockToSend));
	  newBlockMessage.hop = 0;
	  newBlockMessage.current_blockchain_height = m_core.getTopBlockIndex() + 1; //+1 because previous version of core sent m_blocks.size()

	  m_protocol.relayBlock(newBlockMessage);
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_last_block_header(COMMAND_RPC_GET_LAST_BLOCK_HEADER.request req, COMMAND_RPC_GET_LAST_BLOCK_HEADER.response res)
  {
	var topBlock = m_core.getBlockByHash(m_core.getTopBlockHash());
	fill_block_header_response(topBlock, false, m_core.getTopBlockIndex(), m_core.getTopBlockHash(), res.block_header);
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_block_header_by_hash(COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH.request req, COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH.response res)
  {
	Hash blockHash = new Hash();
	if (!parse_hash256(req.hash, blockHash))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "Failed to parse hex representation of block hash. Hex = " + req.hash + '.'));
	}

	if (!m_core.hasBlock(blockHash))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: can't get block by hash. Hash = " + req.hash + '.'));
	}

	var block = m_core.getBlockByHash(blockHash);
	CachedBlock cachedBlock = new CachedBlock(block);
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	Debug.Assert(block.baseTransaction.inputs[0].type() != typeid(BaseInput));

	fill_block_header_response(block, false, cachedBlock.getBlockIndex(), cachedBlock.getBlockHash(), res.block_header);
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool on_get_block_header_by_height(COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT.request req, COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT.response res)
  {
	if (m_core.getTopBlockIndex() < req.height)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_TOO_BIG_HEIGHT, "To big height: " + Convert.ToString(req.height) + ", current blockchain height = " + Convert.ToString(m_core.getTopBlockIndex())));
	}

  uint32_t index = (uint32_t)req.height;
	var block = m_core.getBlockByIndex(new uint32_t(index));
	CachedBlock cachedBlock = new CachedBlock(block);
  Debug.Assert(cachedBlock.getBlockIndex() == req.height);
	fill_block_header_response(block, false, new uint32_t(index), cachedBlock.getBlockHash(), res.block_header);
	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }

  private void fill_block_header_response(BlockTemplate blk, bool orphan_status, uint32_t index, Hash hash, block_header_response response)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.major_version = blk.majorVersion;
	  response.major_version.CopyFrom(blk.majorVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.minor_version = blk.minorVersion;
	  response.minor_version.CopyFrom(blk.minorVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.timestamp = blk.timestamp;
	  response.timestamp.CopyFrom(blk.timestamp);
	  response.prev_hash = Common.GlobalMembers.podToHex(blk.previousBlockHash);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.nonce = blk.nonce;
	  response.nonce.CopyFrom(blk.nonce);
	  response.orphan_status = orphan_status;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.height = index;
	  response.height.CopyFrom(index);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.depth = m_core.getTopBlockIndex() - index;
	  response.depth.CopyFrom(m_core.getTopBlockIndex() - index);
	  response.hash = Common.GlobalMembers.podToHex(hash);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.difficulty = m_core.getBlockDifficulty(index);
	  response.difficulty.CopyFrom(m_core.getBlockDifficulty(new uint32_t(index)));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.reward = get_block_reward(blk);
	  response.reward.CopyFrom(GlobalMembers.get_block_reward(blk));
	  BlockDetails blkDetails = m_core.getBlockDetails(hash);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.num_txes = static_cast<uint32_t>(blkDetails.transactions.size());
	  response.num_txes.CopyFrom((uint32_t)blkDetails.transactions.Count);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: response.block_size = blkDetails.blockSize;
	  response.block_size.CopyFrom(blkDetails.blockSize);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private RawBlockLegacy prepareRawBlockLegacy(BinaryArray && blockBlob)
  {
	BlockTemplate blockTemplate = new BlockTemplate();
	bool result = CryptoNote.GlobalMembers.fromBinaryArray(ref blockTemplate, blockBlob);
	if (result)
	{
	}
	Debug.Assert(result);

	RawBlockLegacy rawBlock = new RawBlockLegacy();
	rawBlock.block = std::move(blockBlob);

	if (blockTemplate.transactionHashes.Count == 0)
	{
	  return rawBlock;
	}

	rawBlock.transactions.Capacity = blockTemplate.transactionHashes.Count;
	List<Crypto.Hash> missedTransactions = new List<Crypto.Hash>();
	m_core.getTransactions(blockTemplate.transactionHashes, rawBlock.transactions, missedTransactions);
	Debug.Assert(missedTransactions.Count == 0);

	return rawBlock;
  }


  //------------------------------------------------------------------------------------------------------------------------------
  // JSON RPC methods
  //------------------------------------------------------------------------------------------------------------------------------
  private bool f_on_blocks_list_json(F_COMMAND_RPC_GET_BLOCKS_LIST.request req, F_COMMAND_RPC_GET_BLOCKS_LIST.response res)
  {
	// check if blockchain explorer RPC is enabled
	if (m_core.getCurrency().isBlockexplorer() == false)
	{
	  return false;
	}

	if (m_core.getTopBlockIndex() + 1 <= req.height != null)
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_TOO_BIG_HEIGHT, "To big height: " + Convert.ToString(req.height) + ", current blockchain height = " + Convert.ToString(m_core.getTopBlockIndex())));
	}

	uint32_t print_blocks_count = 30;
	uint32_t last_height = (uint32_t)(req.height - print_blocks_count);
	if (req.height <= print_blocks_count)
	{
	  last_height = 0;
	}

	for (uint32_t i = (uint32_t)req.height; i >= last_height; i--)
	{
	  Hash block_hash = m_core.getBlockHashByIndex((uint32_t)i);
	  if (!m_core.hasBlock(block_hash))
	  {
		throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: can't get block by height. Height = " + Convert.ToString(i) + '.'));
	  }
	  BlockTemplate blk = m_core.getBlockByHash(block_hash);
	  BlockDetails blkDetails = m_core.getBlockDetails(block_hash);

	  f_block_short_response block_short = new f_block_short_response();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.cumul_size = blkDetails.blockSize;
	  block_short.cumul_size.CopyFrom(blkDetails.blockSize);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.timestamp = blk.timestamp;
	  block_short.timestamp.CopyFrom(blk.timestamp);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.difficulty = blkDetails.difficulty;
	  block_short.difficulty.CopyFrom(blkDetails.difficulty);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.height = i;
	  block_short.height.CopyFrom(i);
	  block_short.hash = Common.GlobalMembers.podToHex(block_hash);
	  block_short.tx_count = blk.transactionHashes.Count + 1;

	  res.blocks.Add(block_short);

	  if (i == 0)
	  {
		break;
	  }
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool f_on_block_json(F_COMMAND_RPC_GET_BLOCK_DETAILS.request req, F_COMMAND_RPC_GET_BLOCK_DETAILS.response res)
  {
	// check if blockchain explorer RPC is enabled
	if (m_core.getCurrency().isBlockexplorer() == false)
	{
	  // NOTE I think this should set a log error
	  return false;
	}

	Hash hash = new Hash();

	try
	{
	  uint32_t height = boost::lexical_cast<uint32_t>(req.hash);
	  hash = m_core.getBlockHashByIndex(new uint32_t(height));
	}
	catch (boost::bad_lexical_cast)
	{
	  if (!parse_hash256(req.hash, hash))
	  {
		throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "Failed to parse hex representation of block hash. Hex = " + req.hash + '.'));
	  }
	}

	if (!m_core.hasBlock(hash))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: can't get block by hash. Hash = " + req.hash + '.'));
	}
	BlockTemplate blk = m_core.getBlockByHash(hash);
	BlockDetails blkDetails = m_core.getBlockDetails(hash);

//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	if (blk.baseTransaction.inputs[0].type() != typeid(BaseInput))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: coinbase transaction in the block has the wrong type"));
	}

	block_header_response block_header = new block_header_response();
	res.block.height = boost::get<BaseInput>(blk.baseTransaction.inputs[0]).blockIndex;
	fill_block_header_response(blk, false, new uint32_t(res.block.height), hash, block_header);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.major_version = block_header.major_version;
	res.block.major_version.CopyFrom(block_header.major_version);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.minor_version = block_header.minor_version;
	res.block.minor_version.CopyFrom(block_header.minor_version);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.timestamp = block_header.timestamp;
	res.block.timestamp.CopyFrom(block_header.timestamp);
	res.block.prev_hash = block_header.prev_hash;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.nonce = block_header.nonce;
	res.block.nonce.CopyFrom(block_header.nonce);
	res.block.hash = Common.GlobalMembers.podToHex(hash);
	res.block.depth = m_core.getTopBlockIndex() - res.block.height;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.difficulty = m_core.getBlockDifficulty(res.block.height);
	res.block.difficulty.CopyFrom(m_core.getBlockDifficulty(new uint32_t(res.block.height)));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.transactionsCumulativeSize = blkDetails.transactionsCumulativeSize;
	res.block.transactionsCumulativeSize.CopyFrom(blkDetails.transactionsCumulativeSize);
	res.block.alreadyGeneratedCoins = Convert.ToString(blkDetails.alreadyGeneratedCoins);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.alreadyGeneratedTransactions = blkDetails.alreadyGeneratedTransactions;
	res.block.alreadyGeneratedTransactions.CopyFrom(blkDetails.alreadyGeneratedTransactions);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.reward = block_header.reward;
	res.block.reward.CopyFrom(block_header.reward);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.sizeMedian = blkDetails.sizeMedian;
	res.block.sizeMedian.CopyFrom(blkDetails.sizeMedian);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.blockSize = blkDetails.blockSize;
	res.block.blockSize.CopyFrom(blkDetails.blockSize);
	res.block.orphan_status = blkDetails.isAlternative;

	uint64_t maxReward = 0;
	uint64_t currentReward = 0;
	int64_t emissionChange = 0;

	if (maxReward != null)
	{
	}
	if (currentReward != null)
	{
	}
	if (emissionChange != null)
	{
	}

	uint64_t blockGrantedFullRewardZone = m_core.getCurrency().blockGrantedFullRewardZoneByBlockVersion(new uint8_t(block_header.major_version));
	res.block.effectiveSizeMedian = Math.Max(res.block.sizeMedian, blockGrantedFullRewardZone);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block.baseReward = blkDetails.baseReward;
	res.block.baseReward.CopyFrom(blkDetails.baseReward);
	res.block.penalty = blkDetails.penalty;

	// Base transaction adding
	f_transaction_short_response transaction_short = new f_transaction_short_response();
	transaction_short.hash = Common.GlobalMembers.podToHex(CryptoNote.GlobalMembers.getObjectHash(blk.baseTransaction));
	transaction_short.fee = 0;
	transaction_short.amount_out = getOutputAmount(blk.baseTransaction);
	transaction_short.size = CryptoNote.GlobalMembers.getObjectBinarySize(blk.baseTransaction);
	res.block.transactions.Add(transaction_short);

	List<Crypto.Hash> missed_txs = new List<Crypto.Hash>();
	List<BinaryArray> txs = new List<BinaryArray>();
	m_core.getTransactions(blk.transactionHashes, txs, missed_txs);

	res.block.totalFeeAmount = 0;

	foreach (BinaryArray ba in txs)
	{
	  Transaction tx = new Transaction();
	  if (!CryptoNote.GlobalMembers.fromBinaryArray(ref tx, ba))
	  {
		throw new System.Exception("Couldn't deserialize transaction");
	  }
	  f_transaction_short_response transaction_short = new f_transaction_short_response();
	  uint64_t amount_in = getInputAmount(tx);
	  uint64_t amount_out = getOutputAmount(tx);

	  transaction_short.hash = Common.GlobalMembers.podToHex(CryptoNote.GlobalMembers.getObjectHash(tx));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transaction_short.fee = amount_in - amount_out;
	  transaction_short.fee.CopyFrom(amount_in - amount_out);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transaction_short.amount_out = amount_out;
	  transaction_short.amount_out.CopyFrom(amount_out);
	  transaction_short.size = CryptoNote.GlobalMembers.getObjectBinarySize(tx);
	  res.block.transactions.Add(transaction_short);

	  res.block.totalFeeAmount += transaction_short.fee;
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool f_on_transaction_json(F_COMMAND_RPC_GET_TRANSACTION_DETAILS.request req, F_COMMAND_RPC_GET_TRANSACTION_DETAILS.response res)
  {
	// check if blockchain explorer RPC is enabled
	if (m_core.getCurrency().isBlockexplorer() == false)
	{
	  return false;
	}

	Hash hash = new Hash();

	if (!parse_hash256(req.hash, hash))
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "Failed to parse hex representation of transaction hash. Hex = " + req.hash + '.'));
	}

	List<Crypto.Hash> tx_ids = new List<Crypto.Hash>();
	tx_ids.Add(hash);

	List<Crypto.Hash> missed_txs = new List<Crypto.Hash>();
	List<BinaryArray> txs = new List<BinaryArray>();
	m_core.getTransactions(tx_ids, txs, missed_txs);

	if (1 == txs.Count)
	{
	  Transaction transaction = new Transaction();
	  if (!CryptoNote.GlobalMembers.fromBinaryArray(ref transaction, txs[0]))
	  {
		throw new System.Exception("Couldn't deserialize transaction");
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.tx = transaction;
	  res.tx.CopyFrom(transaction);
	}
	else
	{
	  throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_WRONG_PARAM, "transaction wasn't found. Hash = " + req.hash + '.'));
	}
	TransactionDetails transactionDetails = m_core.getTransactionDetails(hash);

	Crypto.Hash blockHash = new Crypto.Hash();
	if (transactionDetails.inBlockchain)
	{
	  uint32_t blockHeight = new uint32_t(transactionDetails.blockIndex);
	  if (blockHeight == null)
	  {
		throw new JsonRpc.JsonRpcError(new int(DefineConstants.CORE_RPC_ERROR_CODE_INTERNAL_ERROR, "Internal error: can't get transaction by hash. Hash = " + Common.GlobalMembers.podToHex(hash) + '.'));
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockHash = m_core.getBlockHashByIndex(blockHeight);
	  blockHash.CopyFrom(m_core.getBlockHashByIndex(new uint32_t(blockHeight)));
	  BlockTemplate blk = m_core.getBlockByHash(blockHash);
	  BlockDetails blkDetails = m_core.getBlockDetails(blockHash);

	  f_block_short_response block_short = new f_block_short_response();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.cumul_size = blkDetails.blockSize;
	  block_short.cumul_size.CopyFrom(blkDetails.blockSize);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.timestamp = blk.timestamp;
	  block_short.timestamp.CopyFrom(blk.timestamp);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: block_short.height = blockHeight;
	  block_short.height.CopyFrom(blockHeight);
	  block_short.hash = Common.GlobalMembers.podToHex(blockHash);
	  block_short.tx_count = blk.transactionHashes.Count + 1;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.block = block_short;
	  res.block.CopyFrom(block_short);
	}

	uint64_t amount_in = getInputAmount(res.tx);
	uint64_t amount_out = getOutputAmount(res.tx);

	res.txDetails.hash = Common.GlobalMembers.podToHex(CryptoNote.GlobalMembers.getObjectHash(res.tx));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.txDetails.fee = amount_in - amount_out;
	res.txDetails.fee.CopyFrom(amount_in - amount_out);
	if (amount_in == 0)
	{
	  res.txDetails.fee = 0;
	}
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.txDetails.amount_out = amount_out;
	res.txDetails.amount_out.CopyFrom(amount_out);
	res.txDetails.size = CryptoNote.GlobalMembers.getObjectBinarySize(res.tx);

	uint64_t mixin = new uint64_t();
	if (!f_getMixin(res.tx, ref mixin))
	{
	  return false;
	}
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: res.txDetails.mixin = mixin;
	res.txDetails.mixin.CopyFrom(mixin);

	Crypto.Hash paymentId = new Crypto.Hash();
	if (CryptoNote.getPaymentIdFromTxExtra(res.tx.extra, paymentId))
	{
	  res.txDetails.paymentId = Common.GlobalMembers.podToHex(paymentId);
	}
	else
	{
	  res.txDetails.paymentId = "";
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool f_on_transactions_pool_json(F_COMMAND_RPC_GET_POOL.request req, F_COMMAND_RPC_GET_POOL.response res)
  {
	// check if blockchain explorer RPC is enabled
	if (m_core.getCurrency().isBlockexplorer() == false)
	{
	  return false;
	}

	var pool = m_core.getPoolTransactions();
	foreach (Transaction tx in pool)
	{
	  f_transaction_short_response transaction_short = new f_transaction_short_response();
	  uint64_t amount_in = getInputAmount(tx);
	  uint64_t amount_out = getOutputAmount(tx);

	  transaction_short.hash = Common.GlobalMembers.podToHex(CryptoNote.GlobalMembers.getObjectHash(tx));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transaction_short.fee = amount_in - amount_out;
	  transaction_short.fee.CopyFrom(amount_in - amount_out);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transaction_short.amount_out = amount_out;
	  transaction_short.amount_out.CopyFrom(amount_out);
	  transaction_short.size = CryptoNote.GlobalMembers.getObjectBinarySize(tx);
	  res.transactions.Add(transaction_short);
	}

	res.status = DefineConstants.CORE_RPC_STATUS_OK;
	return true;
  }
  private bool f_getMixin(Transaction transaction, ref uint64_t mixin)
  {
	mixin = 0;
	foreach (TransactionInput txin in transaction.inputs)
	{
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (txin.type() != typeid(KeyInput))
	  {
		continue;
	  }
	  uint64_t currentMixin = boost::get<KeyInput>(txin).outputIndexes.size();
	  if (currentMixin > mixin)
	  {
		mixin = currentMixin;
	  }
	}
	return true;
  }

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

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Command>

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
