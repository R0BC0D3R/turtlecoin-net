// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
namespace error
{

public enum BlockchainExplorerErrorCodes : int
{
  NOT_INITIALIZED = 1,
  ALREADY_INITIALIZED,
  INTERNAL_ERROR,
  REQUEST_ERROR
}

public class BlockchainExplorerErrorCategory : std::error_category
{
  public static BlockchainExplorerErrorCategory INSTANCE = new BlockchainExplorerErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "BlockchainExplorerErrorCategory";
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
	  case (int)BlockchainExplorerErrorCodes.NOT_INITIALIZED:
		  return "Object was not initialized";
	  case (int)BlockchainExplorerErrorCodes.ALREADY_INITIALIZED:
		  return "Object has been already initialized";
	  case (int)BlockchainExplorerErrorCodes.INTERNAL_ERROR:
		  return "Internal error";
	  case (int)BlockchainExplorerErrorCodes.REQUEST_ERROR:
		  return "Error in request parameters";
	  default:
		  return "Unknown error";
	}
  }

  private BlockchainExplorerErrorCategory()
  {
  }
}

} //namespace error
} //namespace CryptoNote




