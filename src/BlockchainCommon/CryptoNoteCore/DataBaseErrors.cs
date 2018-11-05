// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum DataBaseErrorCodes : int
{
  NOT_INITIALIZED = 1,
  ALREADY_INITIALIZED,
  INTERNAL_ERROR,
  IO_ERROR
}

public class DataBaseErrorCategory : std::error_category
{
  public static DataBaseErrorCategory INSTANCE = new DataBaseErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "DataBaseErrorCategory";
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
	  case (int)DataBaseErrorCodes.NOT_INITIALIZED :
		  return "Object was not initialized";
	  case (int)DataBaseErrorCodes.ALREADY_INITIALIZED :
		  return "Object has been already initialized";
	  case (int)DataBaseErrorCodes.INTERNAL_ERROR :
		  return "Internal error";
	  case (int)DataBaseErrorCodes.IO_ERROR :
		  return "IO error";
	  default:
		  return "Unknown error";
	}
  }

  private DataBaseErrorCategory()
  {

  }
}

} //namespace error
} //namespace CryptoNote


