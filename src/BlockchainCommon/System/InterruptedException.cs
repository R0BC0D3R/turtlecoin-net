// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace System
{

public class InterruptedException : System.Exception
{
//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* what() const throw() override
	public override string what()
	{
	  return "interrupted";
	}
}

}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace