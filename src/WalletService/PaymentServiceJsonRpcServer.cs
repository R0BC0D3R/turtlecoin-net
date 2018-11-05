using System.Collections.Generic;

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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace PaymentService
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class WalletService;

public class PaymentServiceJsonRpcServer : CryptoNote.JsonRpcServer
{
  public PaymentServiceJsonRpcServer(System.Dispatcher sys, System.Event stopEvent, WalletService service, Logging.ILogger loggerGroup, PaymentService.ConfigurationManager config) : base(sys, stopEvent, loggerGroup, config)
  {
	  this.service = new PaymentService.WalletService(service);
	  this.logger = new Logging.LoggerRef(loggerGroup, "PaymentServiceJsonRpcServer");
	handlers.Add("save", jsonHandler<Save.Request, Save.Response>(std::bind(this.handleSave, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("export", jsonHandler<Export.Request, Export.Response>(std::bind(this.handleExport, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("reset", jsonHandler<Reset.Request, Reset.Response>(std::bind(this.handleReset, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("createAddress", jsonHandler<CreateAddress.Request, CreateAddress.Response>(std::bind(this.handleCreateAddress, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("createAddressList", jsonHandler<CreateAddressList.Request, CreateAddressList.Response>(std::bind(this.handleCreateAddressList, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("deleteAddress", jsonHandler<DeleteAddress.Request, DeleteAddress.Response>(std::bind(this.handleDeleteAddress, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getSpendKeys", jsonHandler<GetSpendKeys.Request, GetSpendKeys.Response>(std::bind(this.handleGetSpendKeys, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getBalance", jsonHandler<GetBalance.Request, GetBalance.Response>(std::bind(this.handleGetBalance, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getBlockHashes", jsonHandler<GetBlockHashes.Request, GetBlockHashes.Response>(std::bind(this.handleGetBlockHashes, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getTransactionHashes", jsonHandler<GetTransactionHashes.Request, GetTransactionHashes.Response>(std::bind(this.handleGetTransactionHashes, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getTransactions", jsonHandler<GetTransactions.Request, GetTransactions.Response>(std::bind(this.handleGetTransactions, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getUnconfirmedTransactionHashes", jsonHandler<GetUnconfirmedTransactionHashes.Request, GetUnconfirmedTransactionHashes.Response>(std::bind(this.handleGetUnconfirmedTransactionHashes, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getTransaction", jsonHandler<GetTransaction.Request, GetTransaction.Response>(std::bind(this.handleGetTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("sendTransaction", jsonHandler<SendTransaction.Request, SendTransaction.Response>(std::bind(this.handleSendTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("createDelayedTransaction", jsonHandler<CreateDelayedTransaction.Request, CreateDelayedTransaction.Response>(std::bind(this.handleCreateDelayedTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getDelayedTransactionHashes", jsonHandler<GetDelayedTransactionHashes.Request, GetDelayedTransactionHashes.Response>(std::bind(this.handleGetDelayedTransactionHashes, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("deleteDelayedTransaction", jsonHandler<DeleteDelayedTransaction.Request, DeleteDelayedTransaction.Response>(std::bind(this.handleDeleteDelayedTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("sendDelayedTransaction", jsonHandler<SendDelayedTransaction.Request, SendDelayedTransaction.Response>(std::bind(this.handleSendDelayedTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getViewKey", jsonHandler<GetViewKey.Request, GetViewKey.Response>(std::bind(this.handleGetViewKey, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getMnemonicSeed", jsonHandler<GetMnemonicSeed.Request, GetMnemonicSeed.Response>(std::bind(this.handleGetMnemonicSeed, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getStatus", jsonHandler<GetStatus.Request, GetStatus.Response>(std::bind(this.handleGetStatus, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getAddresses", jsonHandler<GetAddresses.Request, GetAddresses.Response>(std::bind(this.handleGetAddresses, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("sendFusionTransaction", jsonHandler<SendFusionTransaction.Request, SendFusionTransaction.Response>(std::bind(this.handleSendFusionTransaction, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("estimateFusion", jsonHandler<EstimateFusion.Request, EstimateFusion.Response>(std::bind(this.handleEstimateFusion, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("createIntegratedAddress", jsonHandler<CreateIntegratedAddress.Request, CreateIntegratedAddress.Response>(std::bind(this.handleCreateIntegratedAddress, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getFeeInfo", jsonHandler<NodeFeeInfo.Request, NodeFeeInfo.Response>(std::bind(this.handleNodeFeeInfo, this, std::placeholders._1, std::placeholders._2)));
	handlers.Add("getNodeFeeInfo", jsonHandler<NodeFeeInfo.Request, NodeFeeInfo.Response>(std::bind(this.handleNodeFeeInfo, this, std::placeholders._1, std::placeholders._2)));
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  PaymentServiceJsonRpcServer(const PaymentServiceJsonRpcServer&) = delete;

  protected override void processJsonRpcRequest(Common.JsonValue req, Common.JsonValue resp)
  {
	try
	{
	  prepareJsonResponse(req.functorMethod, resp.functorMethod);

	  if (!config.serviceConfig.legacySecurity)
	  {
		string clientPassword;
		if (!req.contains("password"))
		{
		  makeInvalidPasswordResponse(resp.functorMethod);
		  return;
		}
		if (!req.functorMethod("password").isString())
		{
		  makeInvalidPasswordResponse(resp.functorMethod);
		  return;
		}
		clientPassword = req.functorMethod("password").getString();

		List<byte> rawData = new List<byte>(clientPassword.GetEnumerator(), clientPassword.end());
		Crypto.Hash hashedPassword = new Crypto.Hash();
		Crypto.GlobalMembers.cn_slow_hash_v0(rawData.data(), rawData.Count, hashedPassword);
		if (hashedPassword != config.rpcSecret)
		{
		  makeInvalidPasswordResponse(resp.functorMethod);
		  return;
		}
	  }

	  if (!req.contains("method"))
	  {
		logger.functorMethod(Logging.Level.WARNING) << "Field \"method\" is not found in json request: " << req.functorMethod;
		makeGenericErrorReponse(resp.functorMethod, "Invalid Request", -3600);
		return;
	  }

	  if (!req.functorMethod("method").isString())
	  {
		logger.functorMethod(Logging.Level.WARNING) << "Field \"method\" is not a string type: " << req.functorMethod;
		makeGenericErrorReponse(resp.functorMethod, "Invalid Request", -3600);
		return;
	  }

	  string method = req.functorMethod("method").getString();

	  var it = handlers.find(method);
	  if (it == handlers.end())
	  {
		logger.functorMethod(Logging.Level.WARNING) << "Requested method not found: " << method;
		makeMethodNotFoundResponse(resp.functorMethod);
		return;
	  }

	  logger.functorMethod(Logging.Level.DEBUGGING) << method << " request came";

	  Common.JsonValue @params = new Common.JsonValue(Common.JsonValue.OBJECT);
	  if (req.contains("params"))
	  {
		@params = req.functorMethod("params");
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second(@params.functorMethod, resp.functorMethod);
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Error occurred while processing JsonRpc request: " << e.Message;
	  makeGenericErrorReponse(resp.functorMethod, e.Message);
	}
  }

  private WalletService service;
  private Logging.LoggerRef logger = new Logging.LoggerRef();

  private delegate void HandlerFunction(Common.JsonValue jsonRpcParams, Common.JsonValue jsonResponse);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename RequestType, typename ResponseType, typename RequestHandler>
  private HandlerFunction jsonHandler<RequestType, ResponseType, RequestHandler>(RequestHandler handler)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: return [handler, this] (const Common::JsonValue& jsonRpcParams, Common::JsonValue& jsonResponse) mutable
	return (Common.JsonValue jsonRpcParams.functorMethod, Common.JsonValue jsonResponse.functorMethod) => mutable
	{
	  RequestType request = new default(RequestType);
	  ResponseType response = new default(ResponseType);

	  try
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
		CryptoNote.JsonInputValueSerializer inputSerializer = new CryptoNote.JsonInputValueSerializer(const_cast<Common.JsonValue&>(jsonRpcParams.functorMethod));
		SerializeRequest(request, inputSerializer.functorMethod);
	  }
	  catch (System.Exception)
	  {
		makeGenericErrorReponse(jsonResponse.functorMethod, "Invalid Request", -32600);
		return;
	  }

	  std::error_code ec = handler(request, response);
	  if (ec != null)
	  {
		makeErrorResponse(ec, jsonResponse.functorMethod);
		return;
	  }

	  CryptoNote.JsonOutputStreamSerializer outputSerializer = new CryptoNote.JsonOutputStreamSerializer();
	  CryptoNote.GlobalMembers.serialize(response, outputSerializer.functorMethod);
	  fillJsonResponse(outputSerializer.getValue.functorMethod(), jsonResponse.functorMethod);
	};
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename RequestType>
  private void SerializeRequest<RequestType>(RequestType request, CryptoNote.JsonInputValueSerializer inputSerializer)
  {
	  CryptoNote.GlobalMembers.serialize(request, inputSerializer.functorMethod);
  }

  private void SerializeRequest(SendTransaction.Request request, CryptoNote.JsonInputValueSerializer inputSerializer)
  {
	  request.serialize(inputSerializer.functorMethod, service);
  }

  private void SerializeRequest(CreateDelayedTransaction.Request request, CryptoNote.JsonInputValueSerializer inputSerializer)
  {
	  request.serialize(inputSerializer.functorMethod, service);
  }

  private void SerializeRequest(SendFusionTransaction.Request request, CryptoNote.JsonInputValueSerializer inputSerializer)
  {
	  request.serialize(inputSerializer.functorMethod, service);
  }

  private Dictionary<string, HandlerFunction> handlers = new Dictionary<string, HandlerFunction>();

  private std::error_code handleSave(Save.Request request, Save.Response response)
  {
	return service.saveWalletNoThrow();
  }
  private std::error_code handleExport(Export.Request request, Export.Response response)
  {
	return service.exportWallet(request.fileName);
  }
  private std::error_code handleReset(Reset.Request request, Reset.Response response)
  {
	return service.resetWallet(request.scanHeight);
  }
  private std::error_code handleCreateAddress(CreateAddress.Request request, CreateAddress.Response response)
  {
	if (string.IsNullOrEmpty(request.spendSecretKey) && string.IsNullOrEmpty(request.spendPublicKey))
	{
	  return service.createAddress(response.address);
	}
	else if (!string.IsNullOrEmpty(request.spendSecretKey))
	{
	  return service.createAddress(request.spendSecretKey, request.scanHeight, request.newAddress, response.address);
	}
	else
	{
	  return service.createTrackingAddress(request.spendPublicKey, request.scanHeight, request.newAddress, response.address);
	}
  }
  private std::error_code handleCreateAddressList(CreateAddressList.Request request, CreateAddressList.Response response)
  {
	return service.createAddressList(request.spendSecretKeys, request.scanHeight, request.newAddress, response.addresses);
  }
  private std::error_code handleDeleteAddress(DeleteAddress.Request request, DeleteAddress.Response response)
  {
	return service.deleteAddress(request.address);
  }
  private std::error_code handleGetSpendKeys(GetSpendKeys.Request request, GetSpendKeys.Response response)
  {
	return service.getSpendkeys(request.address, response.spendPublicKey, response.spendSecretKey);
  }
  private std::error_code handleGetBalance(GetBalance.Request request, GetBalance.Response response)
  {
	if (!string.IsNullOrEmpty(request.address))
	{
	  return service.getBalance(request.address, ref response.availableBalance, ref response.lockedAmount);
	}
	else
	{
	  return service.getBalance(ref response.availableBalance, ref response.lockedAmount);
	}
  }
  private std::error_code handleGetBlockHashes(GetBlockHashes.Request request, GetBlockHashes.Response response)
  {
	return service.getBlockHashes(request.firstBlockIndex, request.blockCount, response.blockHashes);
  }
  private std::error_code handleGetTransactionHashes(GetTransactionHashes.Request request, GetTransactionHashes.Response response)
  {
	if (!string.IsNullOrEmpty(request.blockHash))
	{
	  return service.getTransactionHashes(request.addresses, request.blockHash, request.blockCount, request.paymentId, response.items);
	}
	else
	{
	  return service.getTransactionHashes(request.addresses, request.firstBlockIndex, request.blockCount, request.paymentId, response.items);
	}
  }
  private std::error_code handleGetTransactions(GetTransactions.Request request, GetTransactions.Response response)
  {
	if (!string.IsNullOrEmpty(request.blockHash))
	{
	  return service.getTransactions(request.addresses, request.blockHash, request.blockCount, request.paymentId, response.items);
	}
	else
	{
	  return service.getTransactions(request.addresses, request.firstBlockIndex, request.blockCount, request.paymentId, response.items);
	}
  }
  private std::error_code handleGetUnconfirmedTransactionHashes(GetUnconfirmedTransactionHashes.Request request, GetUnconfirmedTransactionHashes.Response response)
  {
	return service.getUnconfirmedTransactionHashes(request.addresses, response.transactionHashes);
  }
  private std::error_code handleGetTransaction(GetTransaction.Request request, GetTransaction.Response response)
  {
	return service.getTransaction(request.transactionHash, response.transaction);
  }
  private std::error_code handleSendTransaction(SendTransaction.Request request, SendTransaction.Response response)
  {
	return service.sendTransaction(request, response.transactionHash);
  }
  private std::error_code handleCreateDelayedTransaction(CreateDelayedTransaction.Request request, CreateDelayedTransaction.Response response)
  {
	return service.createDelayedTransaction(request, response.transactionHash);
  }
  private std::error_code handleGetDelayedTransactionHashes(GetDelayedTransactionHashes.Request request, GetDelayedTransactionHashes.Response response)
  {
	return service.getDelayedTransactionHashes(response.transactionHashes);
  }
  private std::error_code handleDeleteDelayedTransaction(DeleteDelayedTransaction.Request request, DeleteDelayedTransaction.Response response)
  {
	return service.deleteDelayedTransaction(request.transactionHash);
  }
  private std::error_code handleSendDelayedTransaction(SendDelayedTransaction.Request request, SendDelayedTransaction.Response response)
  {
	return service.sendDelayedTransaction(request.transactionHash);
  }
  private std::error_code handleGetViewKey(GetViewKey.Request request, GetViewKey.Response response)
  {
	return service.getViewKey(response.viewSecretKey);
  }
  private std::error_code handleGetMnemonicSeed(GetMnemonicSeed.Request request, GetMnemonicSeed.Response response)
  {
	return service.getMnemonicSeed(request.address, response.mnemonicSeed);
  }
  private std::error_code handleGetStatus(GetStatus.Request request, GetStatus.Response response)
  {
	return service.getStatus(ref response.blockCount, ref response.knownBlockCount, ref response.localDaemonBlockCount, response.lastBlockHash, ref response.peerCount);
  }
  private std::error_code handleGetAddresses(GetAddresses.Request request, GetAddresses.Response response)
  {
	return service.getAddresses(response.addresses);
  }

  private std::error_code handleSendFusionTransaction(SendFusionTransaction.Request request, SendFusionTransaction.Response response)
  {
	return service.sendFusionTransaction(request.threshold, request.anonymity, request.addresses, request.destinationAddress, response.transactionHash);
  }
  private std::error_code handleEstimateFusion(EstimateFusion.Request request, EstimateFusion.Response response)
  {
	return service.estimateFusion(request.threshold, request.addresses, ref response.fusionReadyCount, ref response.totalOutputCount);
  }
  private std::error_code handleCreateIntegratedAddress(CreateIntegratedAddress.Request request, CreateIntegratedAddress.Response response)
  {
	return service.createIntegratedAddress(request.address, request.paymentId, response.integratedAddress);
  }
  private std::error_code handleNodeFeeInfo(NodeFeeInfo.Request request, NodeFeeInfo.Response response)
  {
	return service.getFeeInfo(response.address, ref response.amount);
  }

}

} //namespace PaymentService



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



