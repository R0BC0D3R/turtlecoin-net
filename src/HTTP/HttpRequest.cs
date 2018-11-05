using System;
using System.Collections.Generic;

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
  public class HttpRequest
  {

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getMethod() const
	public string getMethod()
	{
	  return method;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getUrl() const
	public string getUrl()
	{
	  return url;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ClassicMap<string, string>& getHeaders() const
	public SortedDictionary<string, string> getHeaders()
	{
	  return headers;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getBody() const
	public string getBody()
	{
	  return body;
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
	public void setUrl(string u)
	{
	  url = u;
	}

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//	friend class HttpParser;

	private string method;
	private string url;
	private SortedDictionary<string, string> headers = new SortedDictionary<string, string>();
	private string body;

	private static std::ostream operator << (std::ostream os, HttpRequest resp)
	{
	  return resp.printHttpRequest(os);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: std::ostream& printHttpRequest(std::ostream& os) const
	private std::ostream printHttpRequest(std::ostream os)
	{
	  os << "POST " << url << " HTTP/1.1\r\n";
	  var host = headers.find("Host");
	  if (host == headers.end())
	  {
		os << "Host: " << "127.0.0.1" << "\r\n";
	  }

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
  }
}


