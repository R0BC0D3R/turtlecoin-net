// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace CryptoNote
{

//Blocking HttpParser
public class HttpParser
{
  public HttpParser()
  {
  }

  public void receiveRequest(std::istream stream, HttpRequest request)
  {
	readWord(stream, request.method);
	readWord(stream, request.url);

	string httpVersion;
	readWord(stream, httpVersion);

	readHeaders(stream, request.headers);

	string body;
	uint bodyLen = getBodyLen(request.headers);
	if (bodyLen != null)
	{
	  readBody(stream, request.body, new uint(bodyLen));
	}
  }
  public void receiveResponse(std::istream stream, HttpResponse response)
  {
	string httpVersion;
	readWord(stream, httpVersion);

	string status;
	char c;

	stream.get(c);
	while (stream.good() && c != '\r')
	{ //Till the end
	  status += c;
	  stream.get(c);
	}

	GlobalMembers.throwIfNotGood(stream);

	if (c == '\r')
	{
	  stream.get(c);
	  if (c != '\n')
	  {
		throw new System.Exception("Parser error: '\\n' symbol is expected");
	  }
	}

	response.setStatus(parseResponseStatusFromString(status));

	string name;
	string value;

	while (readHeader(stream, name, value))
	{
	  response.addHeader(name, value);
	  name = "";
	  value = "";
	}

	response.addHeader(name, value);
	var headers = response.getHeaders();
	uint length = 0;
	var it = headers.find("content-length");
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != headers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  length = Convert.ToUInt32(it.second);
	}

	string body;
	if (length != null)
	{
	  readBody(stream, body, new uint(length));
	}

	response.setBody(body);
  }
  public static HttpResponse.HTTP_STATUS parseResponseStatusFromString(string status)
  {
	if (status == "200 OK" || status == "200 Ok")
	{
		return CryptoNote.HttpResponse.STATUS_200;
	}
	else if (status == "404 Not Found")
	{
		return CryptoNote.HttpResponse.STATUS_404;
	}
	else if (status == "500 Internal Server Error")
	{
		return CryptoNote.HttpResponse.STATUS_500;
	}
	else
	{
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.HttpParserErrorCodes.UNEXPECTED_SYMBOL), "Unknown HTTP status code is given");
	}

	return CryptoNote.HttpResponse.STATUS_200; //unaccessible
  }
  private void readWord(std::istream stream, string word)
  {
	char c;

	stream.get(c);
	while (stream.good() && c != ' ' && c != '\r')
	{
	  word += c;
	  stream.get(c);
	}

	GlobalMembers.throwIfNotGood(stream);

	if (c == '\r')
	{
	  stream.get(c);
	  if (c != '\n')
	  {
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.HttpParserErrorCodes.UNEXPECTED_SYMBOL));
	  }
	}
  }
  private void readHeaders(std::istream stream, SortedDictionary<string, string> headers)
  {
	string name;
	string value;

	while (readHeader(stream, name, value))
	{
	  headers[name] = value; //use insert
	  name = "";
	  value = "";
	}

	headers[name] = value; //use insert
  }
  private bool readHeader(std::istream stream, string name, string value)
  {
	char c;
	bool isName = true;

	stream.get(c);
	while (stream.good() && c != '\r')
	{
	  if (c == ':')
	  {
		if (stream.peek() == ' ')
		{
		  stream.get(c);
		}

		if (string.IsNullOrEmpty(name))
		{
		  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.HttpParserErrorCodes.EMPTY_HEADER));
		}

		if (isName)
		{
		  isName = false;
		  stream.get(c);
		  continue;
		}
	  }

	  if (isName)
	  {
		name += c;
		stream.get(c);
	  }
	  else
	  {
		value += c;
		stream.get(c);
	  }
	}

	GlobalMembers.throwIfNotGood(stream);

	stream.get(c);
	if (c != '\n')
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.HttpParserErrorCodes.UNEXPECTED_SYMBOL));
	}

	std::transform(name.GetEnumerator(), name.end(), name.GetEnumerator(), global::tolower);

	c = stream.peek();
	if (c == '\r')
	{
	  stream.get(c).get(c);
	  if (c != '\n')
	  {
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.HttpParserErrorCodes.UNEXPECTED_SYMBOL));
	  }

	  return false; //no more headers
	}

	return true;
  }
  private uint getBodyLen(SortedDictionary<string, string> headers)
  {
	var it = headers.find("content-length");
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != headers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  uint bytes = Convert.ToUInt32(it.second);
	  return bytes;
	}

	return 0;
  }
  private void readBody(std::istream stream, string body, uint bodyLen)
  {
	uint read = 0;

	while (stream.good() && read < bodyLen)
	{
	  body += stream.get();
	  ++read;
	}

	GlobalMembers.throwIfNotGood(stream);
  }
}

} //namespace CryptoNote





//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


