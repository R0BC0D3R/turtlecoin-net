// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum TransactionValidationError
{
  VALIDATION_SUCCESS = 0,
  EMPTY_INPUTS,
  INPUT_UNKNOWN_TYPE,
  INPUT_EMPTY_OUTPUT_USAGE,
  INPUT_INVALID_DOMAIN_KEYIMAGES,
  INPUT_IDENTICAL_KEYIMAGES,
  INPUT_IDENTICAL_OUTPUT_INDEXES,
  INPUT_KEYIMAGE_ALREADY_SPENT,
  INPUT_INVALID_GLOBAL_INDEX,
  INPUT_SPEND_LOCKED_OUT,
  INPUT_INVALID_SIGNATURES,
  INPUT_WRONG_SIGNATURES_COUNT,
  INPUTS_AMOUNT_OVERFLOW,
  INPUT_WRONG_COUNT,
  INPUT_UNEXPECTED_TYPE,
  BASE_INPUT_WRONG_BLOCK_INDEX,
  OUTPUT_ZERO_AMOUNT,
  OUTPUT_INVALID_KEY,
  OUTPUT_INVALID_REQUIRED_SIGNATURES_COUNT,
  OUTPUT_UNKNOWN_TYPE,
  OUTPUTS_AMOUNT_OVERFLOW,
  WRONG_AMOUNT,
  WRONG_TRANSACTION_UNLOCK_TIME,
  INVALID_MIXIN
}

// custom category:
public class TransactionValidationErrorCategory : std::error_category
{
  public static TransactionValidationErrorCategory INSTANCE = new TransactionValidationErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw()
  public virtual string name()
  {
	return "TransactionValidationErrorCategory";
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
	TransactionValidationError code = (TransactionValidationError)ev;

	switch (code)
	{
	  case TransactionValidationError.VALIDATION_SUCCESS:
		  return "Transaction successfully validated";
	  case TransactionValidationError.EMPTY_INPUTS:
		  return "Transaction has no inputs";
	  case TransactionValidationError.INPUT_UNKNOWN_TYPE:
		  return "Transaction has input with unknown type";
	  case TransactionValidationError.INPUT_EMPTY_OUTPUT_USAGE:
		  return "Transaction's input uses empty output";
	  case TransactionValidationError.INPUT_INVALID_DOMAIN_KEYIMAGES:
		  return "Transaction uses key image not in the valid domain";
	  case TransactionValidationError.INPUT_IDENTICAL_KEYIMAGES:
		  return "Transaction has identical key images";
	  case TransactionValidationError.INPUT_IDENTICAL_OUTPUT_INDEXES:
		  return "Transaction has identical output indexes";
	  case TransactionValidationError.INPUT_KEYIMAGE_ALREADY_SPENT:
		  return "Transaction is already present in the queue";
	  case TransactionValidationError.INPUT_INVALID_GLOBAL_INDEX:
		  return "Transaction has input with invalid global index";
	  case TransactionValidationError.INPUT_SPEND_LOCKED_OUT:
		  return "Transaction uses locked input";
	  case TransactionValidationError.INPUT_INVALID_SIGNATURES:
		  return "Transaction has input with invalid signature";
	  case TransactionValidationError.INPUT_WRONG_SIGNATURES_COUNT:
		  return "Transaction has input with wrong signatures count";
	  case TransactionValidationError.INPUTS_AMOUNT_OVERFLOW:
		  return "Transaction's inputs sum overflow";
	  case TransactionValidationError.INPUT_WRONG_COUNT:
		  return "Wrong input count";
	  case TransactionValidationError.INPUT_UNEXPECTED_TYPE:
		  return "Wrong input type";
	  case TransactionValidationError.BASE_INPUT_WRONG_BLOCK_INDEX:
		  return "Base input has wrong block index";
	  case TransactionValidationError.OUTPUT_ZERO_AMOUNT:
		  return "Transaction has zero output amount";
	  case TransactionValidationError.OUTPUT_INVALID_KEY:
		  return "Transaction has output with invalid key";
	  case TransactionValidationError.OUTPUT_INVALID_REQUIRED_SIGNATURES_COUNT:
		  return "Transaction has output with invalid signatures count";
	  case TransactionValidationError.OUTPUT_UNKNOWN_TYPE:
		  return "Transaction has unknown output type";
	  case TransactionValidationError.OUTPUTS_AMOUNT_OVERFLOW:
		  return "Transaction has outputs amount overflow";
	  case TransactionValidationError.WRONG_AMOUNT:
		  return "Transaction wrong amount";
	  case TransactionValidationError.WRONG_TRANSACTION_UNLOCK_TIME:
		  return "Transaction has wrong unlock time";
	  case TransactionValidationError.INVALID_MIXIN:
		  return "Mixin too large or too small";
	  default:
		  return "Unknown error";
	}
  }

  private TransactionValidationErrorCategory()
  {
  }
}

}
}

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_code_enum<CryptoNote::error::TransactionValidationError>: public true_type
public class is_error_code_enum: true_type
{
}

}


