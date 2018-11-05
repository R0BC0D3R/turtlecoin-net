// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum AddBlockErrorCode
{
  ADDED_TO_MAIN = 1,
  ADDED_TO_ALTERNATIVE,
  ADDED_TO_ALTERNATIVE_AND_SWITCHED,
  ALREADY_EXISTS,
  REJECTED_AS_ORPHANED,
  DESERIALIZATION_FAILED
}

// custom category:
public class AddBlockErrorCategory : std::error_category
{
  public static AddBlockErrorCategory INSTANCE = new AddBlockErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw()
  public virtual string name()
  {
	return "AddBlockErrorCategory";
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
	AddBlockErrorCode code = (AddBlockErrorCode)ev;

	switch (code)
	{
	  case AddBlockErrorCode.ADDED_TO_MAIN:
		  return "Block added to main chain";
	  case AddBlockErrorCode.ADDED_TO_ALTERNATIVE:
		  return "Block added to alternative chain";
	  case AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED:
		  return "Chain switched";
	  case AddBlockErrorCode.ALREADY_EXISTS:
		  return "Block already exists";
	  case AddBlockErrorCode.REJECTED_AS_ORPHANED:
		  return "Block rejected as orphaned";
	  case AddBlockErrorCode.DESERIALIZATION_FAILED:
		  return "Deserialization error";
	  default:
		  return "Unknown error";
	}
  }

  private AddBlockErrorCategory()
  {
  }
}

}
}

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_code_enum<CryptoNote::error::AddBlockErrorCode>: public true_type
public class is_error_code_enum: true_type
{
}

}


