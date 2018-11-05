// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum BlockValidationError
{
  VALIDATION_SUCCESS = 0,
  WRONG_VERSION,
  PARENT_BLOCK_uintOO_BIG,
  PARENT_BLOCK_WRONG_VERSION,
  TIMESTAMP_TOO_FAR_IN_FUTURE,
  TIMESTAMP_TOO_FAR_IN_PAST,
  CUMULATIVE_BLOCK_uintOO_BIG,
  DIFFICULTY_OVERHEAD,
  BLOCK_REWARD_MISMATCH,
  CHECKPOINT_BLOCK_HASH_MISMATCH,
  PROOF_OF_WORK_TOO_WEAK,
  TRANSACTION_ABSENT_IN_POOL
}

// custom category:
public class BlockValidationErrorCategory : std::error_category
{
  public static BlockValidationErrorCategory INSTANCE = new BlockValidationErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw()
  public virtual string name()
  {
	return "BlockValidationErrorCategory";
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
	BlockValidationError code = (BlockValidationError)ev;

	switch (code)
	{
	  case BlockValidationError.VALIDATION_SUCCESS:
		  return "Block validated successfully";
	  case BlockValidationError.WRONG_VERSION:
		  return "Wrong block version";
	  case BlockValidationError.PARENT_BLOCK_uintOO_BIG:
		  return "Parent block size is too big";
	  case BlockValidationError.PARENT_BLOCK_WRONG_VERSION:
		  return "Parent block has wrong version";
	  case BlockValidationError.TIMESTAMP_TOO_FAR_IN_FUTURE:
		  return "Timestamp is too far in future";
	  case BlockValidationError.TIMESTAMP_TOO_FAR_IN_PAST:
		  return "Timestamp is too far in past";
	  case BlockValidationError.CUMULATIVE_BLOCK_uintOO_BIG:
		  return "Cumulative block size is too big";
	  case BlockValidationError.DIFFICULTY_OVERHEAD:
		  return "Block difficulty overhead occurred";
	  case BlockValidationError.BLOCK_REWARD_MISMATCH:
		  return "Block reward doesn't match expected reward";
	  case BlockValidationError.CHECKPOINT_BLOCK_HASH_MISMATCH:
		  return "Checkpoint block hash mismatch";
	  case BlockValidationError.PROOF_OF_WORK_TOO_WEAK:
		  return "Proof of work is too weak";
	  case BlockValidationError.TRANSACTION_ABSENT_IN_POOL:
		  return "Block's transaction is absent in transaction pool";
	  default:
		  return "Unknown error";
	}
  }

  private BlockValidationErrorCategory()
  {
  }
}

}
}

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_code_enum<CryptoNote::error::BlockValidationError>: public true_type
public class is_error_code_enum: true_type
{
}

}


