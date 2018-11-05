// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace CryptoNote
{

  public class HttpResponse
  {
	public enum HTTP_STATUS
	{
	  STATUS_200,
	  STATUS_404,
	  STATUS_500
	}

	public HttpResponse()
	{
	  status = HTTP_STATUS.STATUS_200;
	  headers["Server"] = "CryptoNote-based HTTP server";
	}

	public void setStatus(HTTP_STATUS s)
	{
	  status = s;

	  if (status != HttpResponse.STATUS_200)
	  {
		setBody(GlobalMembers.getErrorBody(status));
	  }
	}
	public void addHeader(string name, string value)
	{
	  headers[name] = value;
	}
	public void setBody(string b)
	{
	  body = b;
	  if (!string.IsNullOrEmpty(body))
	  {
		headers["Content-Length"] = Convert.ToString(body.Length);
	  }
	  else
	  {
		headers.Remove("Content-Length");
	  }
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ClassicMap<string, string>& getHeaders() const
	public SortedDictionary<string, string> getHeaders()
	{
		return headers;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: HTTP_STATUS getStatus() const
	public HTTP_STATUS getStatus()
	{
		return status;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getBody() const
	public string getBody()
	{
		return body;
	}

	private static std::ostream operator << (std::ostream os, HttpResponse resp)
	{
	  return resp.printHttpResponse(os);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: std::ostream& printHttpResponse(std::ostream& os) const
	private std::ostream printHttpResponse(std::ostream os)
	{
	  os << "HTTP/1.1 " << GlobalMembers.getStatusString(status) << "\r\n";

	  foreach (var pair in headers)
	  {
		os << pair.first << ": " << pair.second << "\r\n";
	  }
	  os << "\r\n";

	  if (!string.IsNullOrEmpty(body))
	  {
		os << body;
	  }

	  return os;
	}

	private HTTP_STATUS status;
	private SortedDictionary<string, string> headers = new SortedDictionary<string, string>();
	private string body;
  }

} //namespace CryptoNote



//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


