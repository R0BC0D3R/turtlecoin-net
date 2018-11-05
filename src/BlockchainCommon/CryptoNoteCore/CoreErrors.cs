// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum CoreErrorCode
{
  NOT_INITIALIZED,
  CORRUPTED_BLOCKCHAIN
}

// custom category:
public class CoreErrorCategory : std::error_category
{
  public static CoreErrorCategory INSTANCE = new CoreErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw()
  public virtual string name()
  {
	return "CoreErrorCategory";
  }

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual std::error_condition default_error_condition(int ev) const throw()
  public virtual std::error_condition default_error_condition(int ev) const
  {
	return std::error_condition(ev, this);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual string message(int ev) const
  public virtual string message(int ev)
  {
	CoreErrorCode code = (CoreErrorCode)ev;

	switch (code)
	{
	  case CoreErrorCode.NOT_INITIALIZED:
		  return "Core is not initialized";
	  case CoreErrorCode.CORRUPTED_BLOCKCHAIN:
		  return "Blockchain storage is corrupted";
	  default:
		  return "Unknown error";
	}
  }

  private CoreErrorCategory()
  {
  }
}

}
}

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_code_enum<CryptoNote::error::CoreErrorCode>: public true_type
public class is_error_code_enum: true_type
{
}

}


