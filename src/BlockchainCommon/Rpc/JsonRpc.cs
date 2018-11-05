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
//class HttpClient;

namespace JsonRpc
{

public class JsonRpcError: System.Exception
{
  public JsonRpcError()
  {
	  this.code = 0;
  }
  public JsonRpcError(int c)
  {
	  this.code = c;
	switch (c)
	{
	case GlobalMembers.errParseError:
		message = "Parse error";
		break;
	case GlobalMembers.errInvalidRequest:
		message = "Invalid request";
		break;
	case GlobalMembers.errMethodNotFound:
		message = "Method not found";
		break;
	case GlobalMembers.errInvalidParams:
		message = "Invalid params";
		break;
	case GlobalMembers.errInternalError:
		message = "Internal error";
		break;
	case GlobalMembers.errInvalidPassword:
		message = "Invalid or no password supplied";
		break;
	default:
		message = "Unknown error";
		break;
	}
  }
  public JsonRpcError(int c, string msg)
  {
	  this.code = c;
	  this.message = msg;
  }

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


