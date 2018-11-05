// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using JsonValue = Common.JsonValue;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class HttpResponse;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class HttpRequest;
}

namespace Common
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class JsonValue;
}

namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{

public abstract class JsonRpcServer : HttpServer
{
  public JsonRpcServer(System.Dispatcher sys, System.Event stopEvent, Logging.ILogger loggerGroup, PaymentService.ConfigurationManager config) : base(sys, loggerGroup)
  {
	  this.stopEvent = stopEvent;
	  this.logger = new Logging.LoggerRef(loggerGroup, "JsonRpcServer");
	  this.config = new PaymentService.ConfigurationManager(config);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  JsonRpcServer(const JsonRpcServer&) = delete;

  public new void start(string bindAddress, ushort bindPort)
  {
	base.start(bindAddress, new ushort(bindPort));
	stopEvent.wait();
	base.stop();
  }

  protected static void makeErrorResponse(std::error_code ec, Common.JsonValue resp)
  {

	JsonValue error = new JsonValue(JsonValue.OBJECT);

	JsonValue code = new JsonValue();
	code = (long)CryptoNote.JsonRpc.errParseError; //Application specific error code

	JsonValue message = new JsonValue();
	message = ec.message();

	JsonValue data = new JsonValue(JsonValue.OBJECT);
	JsonValue appCode = new JsonValue();
	appCode = (long)ec.value();
	data.insert("application_code", appCode);

	error.insert("code", code);
	error.insert("message", message);
	error.insert("data", data);

	resp.insert.functorMethod("error", error);
  }
  protected static void makeMethodNotFoundResponse(Common.JsonValue resp)
  {

	JsonValue error = new JsonValue(JsonValue.OBJECT);

	JsonValue code = new JsonValue();
	code = (long)CryptoNote.JsonRpc.errMethodNotFound; //ambigous declaration of JsonValue::operator= (between int and JsonValue)

	JsonValue message = new JsonValue();
	message = "Method not found";

	error.insert("code", code);
	error.insert("message", message);

	resp.insert.functorMethod("error", error);
  }
  protected static void makeInvalidPasswordResponse(Common.JsonValue resp)
  {

	JsonValue error = new JsonValue(JsonValue.OBJECT);

	JsonValue code = new JsonValue();
	code = (long)CryptoNote.JsonRpc.errInvalidPassword;

	JsonValue message = new JsonValue();
	message = "Invalid or no rpc password";

	error.insert("code", code);
	error.insert("message", message);

	resp.insert.functorMethod("error", error);
  }
  protected static void makeGenericErrorReponse(Common.JsonValue resp, string what, int errorCode = -32001)
  {

	JsonValue error = new JsonValue(JsonValue.OBJECT);

	JsonValue code = new JsonValue();
	code = (long)errorCode;

	string msg;
	if (what != null)
	{
	  msg = what;
	}
	else
	{
	  msg = "Unknown application error";
	}

	JsonValue message = new JsonValue();
	message = msg;

	error.insert("code", code);
	error.insert("message", message);

	resp.insert.functorMethod("error", error);

  }
  protected static void fillJsonResponse(Common.JsonValue v, Common.JsonValue resp)
  {
	resp.insert.functorMethod("result", v.functorMethod);
  }
  protected static void prepareJsonResponse(Common.JsonValue req, Common.JsonValue resp)
  {

	if (req.contains("id"))
	{
	  resp.insert.functorMethod("id", req.functorMethod("id"));
	}

	resp.insert.functorMethod("jsonrpc", "2.0");
  }
  protected static void makeJsonParsingErrorResponse(ref Common.JsonValue resp)
  {

	resp = new JsonValue(JsonValue.OBJECT);
	resp.insert.functorMethod("jsonrpc", "2.0");
	resp.insert.functorMethod("id", null);

	JsonValue error = new JsonValue(JsonValue.OBJECT);
	JsonValue code = new JsonValue();
	code = (long)CryptoNote.JsonRpc.errParseError; //ambigous declaration of JsonValue::operator= (between int and JsonValue)

	JsonValue message = "Parse error";

	error.insert("code", code);
	error.insert("message", message);

	resp.insert.functorMethod("error", error);
  }

  protected abstract void processJsonRpcRequest(Common.JsonValue req, Common.JsonValue resp);
  protected PaymentService.ConfigurationManager config;

  // HttpServer
  private override void processRequest(CryptoNote.HttpRequest req, CryptoNote.HttpResponse resp)
  {
	try
	{
	  logger.functorMethod(Logging.Level.TRACE) << "HTTP request came: \n" << req;

	  if (req.getUrl() == "/json_rpc")
	  {
		std::istringstream jsonInputStream = new std::istringstream(req.getBody());
		Common.JsonValue jsonRpcRequest = new Common.JsonValue();
		Common.JsonValue jsonRpcResponse = new Common.JsonValue(Common.JsonValue.OBJECT);

		try
		{
		  jsonInputStream >> jsonRpcRequest.functorMethod;
		}
		catch (System.Exception)
		{
		  logger.functorMethod(Logging.Level.DEBUGGING) << "Couldn't parse request: \"" << req.getBody() << "\"";
		  makeJsonParsingErrorResponse(ref jsonRpcResponse.functorMethod);
		  resp.setStatus(CryptoNote.HttpResponse.STATUS_200);
		  resp.setBody(jsonRpcResponse.toString());
		  return;
		}

		processJsonRpcRequest(jsonRpcRequest.functorMethod, jsonRpcResponse.functorMethod);

		std::ostringstream jsonOutputStream = new std::ostringstream();
		jsonOutputStream << jsonRpcResponse.functorMethod;

		if (config.serviceConfig.corsHeader != "")
		{
		  resp.addHeader("Access-Control-Allow-Origin", config.serviceConfig.corsHeader);
		}

		resp.setStatus(CryptoNote.HttpResponse.STATUS_200);
		resp.setBody(jsonOutputStream.str());

	  }
	  else
	  {
		logger.functorMethod(Logging.Level.WARNING) << "Requested url \"" << req.getUrl() << "\" is not found";
		resp.setStatus(CryptoNote.HttpResponse.STATUS_404);
		return;
	  }
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Error while processing http request: " << e.Message;
	  resp.setStatus(CryptoNote.HttpResponse.STATUS_500);
	}
  }

  private System.Event stopEvent;
  private Logging.LoggerRef logger = new Logging.LoggerRef();
}

} //namespace CryptoNote



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


