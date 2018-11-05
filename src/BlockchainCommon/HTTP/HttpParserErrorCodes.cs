// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum HttpParserErrorCodes
{
  STREAM_NOT_GOOD = 1,
  END_OF_STREAM,
  UNEXPECTED_SYMBOL,
  EMPTY_HEADER
}

// custom category:
public class HttpParserErrorCategory : std::error_category
{
  public static HttpParserErrorCategory INSTANCE = new HttpParserErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "HttpParserErrorCategory";
  }

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual std::error_condition default_error_condition(int ev) const throw() override
  public override std::error_condition default_error_condition(int ev) const
  {
	return std::error_condition(ev, this);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual string message(int ev) const override
  public override string message(int ev)
  {
	switch (ev)
	{
	  case STREAM_NOT_GOOD:
		  return "The stream is not good";
	  case END_OF_STREAM:
		  return "The stream is ended";
	  case UNEXPECTED_SYMBOL:
		  return "Unexpected symbol";
	  case EMPTY_HEADER:
		  return "The header name is empty";
	  default:
		  return "Unknown error";
	}
  }

  private HttpParserErrorCategory()
  {
  }
}

} //namespace error
} //namespace CryptoNote


