// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum AddBlockErrorCondition
{
  BLOCK_ADDED = 0,
  BLOCK_REJECTED,
  BLOCK_VALIDATION_FAILED,
  TRANSACTION_VALIDATION_FAILED,
  DESERIALIZATION_FAILED
}

public class AddBlockErrorConditionCategory: std::error_category
{
  public static AddBlockErrorConditionCategory INSTANCE = new AddBlockErrorConditionCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "AddBlockErrorCondition";
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
	AddBlockErrorCondition code = (AddBlockErrorCondition)ev;

	switch (code)
	{
	  case AddBlockErrorCondition.BLOCK_ADDED:
		  return "Block successfully added";
	  case AddBlockErrorCondition.BLOCK_REJECTED:
		  return "Block rejected";
	  case AddBlockErrorCondition.BLOCK_VALIDATION_FAILED:
		  return "Block validation failed";
	  case AddBlockErrorCondition.TRANSACTION_VALIDATION_FAILED:
		  return "Transaction validation failed";
	  default:
		  return "Unknown error condition";
	}
  }

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual bool equivalent(const std::error_code& errorCode, int condition) const throw() override
  public override bool equivalent(std::error_code errorCode, int condition) const
  {
	AddBlockErrorCondition code = (AddBlockErrorCondition)condition;

	switch (code)
	{
	  case AddBlockErrorCondition.BLOCK_ADDED:
		return errorCode == AddBlockErrorCode.ADDED_TO_MAIN || errorCode == AddBlockErrorCode.ADDED_TO_ALTERNATIVE || errorCode == AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED || errorCode == AddBlockErrorCode.ALREADY_EXISTS;

	  case AddBlockErrorCondition.DESERIALIZATION_FAILED:
		return errorCode == AddBlockErrorCode.DESERIALIZATION_FAILED;

	  case AddBlockErrorCondition.BLOCK_REJECTED:
		return errorCode == AddBlockErrorCode.REJECTED_AS_ORPHANED;

	  case AddBlockErrorCondition.BLOCK_VALIDATION_FAILED:
		return errorCode.category() == BlockValidationErrorCategory.INSTANCE;

	  case AddBlockErrorCondition.TRANSACTION_VALIDATION_FAILED:
		return errorCode.category() == TransactionValidationErrorCategory.INSTANCE;

	  default:
		  return false;
	}
  }
}

}
}

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_condition_enum<CryptoNote::error::AddBlockErrorCondition>: public true_type
public class is_error_condition_enum: true_type
{
}

}


