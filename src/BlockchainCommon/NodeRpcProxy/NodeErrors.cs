// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information


namespace CryptoNote
{
namespace error
{

// custom error conditions enum type:
public enum NodeErrorCodes
{
  NOT_INITIALIZED = 1,
  ALREADY_INITIALIZED,
  NETWORK_ERROR,
  NODE_BUSY,
  INTERNAL_NODE_ERROR,
  REQUEST_ERROR,
  CONNECT_ERROR
}

// custom category:
public class NodeErrorCategory : std::error_category
{
  public static NodeErrorCategory INSTANCE = new NodeErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "NodeErrorCategory";
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
	case NOT_INITIALIZED:
		return "Object was not initialized";
	case ALREADY_INITIALIZED:
		return "Object has been already initialized";
	case NETWORK_ERROR:
		return "Network error";
	case NODE_BUSY:
		return "Node is busy";
	case INTERNAL_NODE_ERROR:
		return "Internal node error";
	case REQUEST_ERROR:
		return "Error in request parameters";
	case CONNECT_ERROR:
		return "Can't connect to daemon";
	default:
		return "Unknown error";
	}
  }

  private NodeErrorCategory()
  {
  }
}

}
}


