// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IInputStream;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IOutputStream;
public class ContainerFormatter <T>
{
  public ContainerFormatter(T container)
  {
	  this.m_container = container;
  }

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& os, const ContainerFormatter<T>& formatter)
  public static std::ostream operator << (std::ostream os, ContainerFormatter<T> formatter)
  {
	os << '{';

	if (!formatter.m_container.empty())
	{
	  os << formatter.m_container.front();
	  for (var it = std::next(formatter.m_container.begin()); it != formatter.m_container.end(); ++it)
	  {
		os << ", " << *it;
	  }
	}

	os << '}';

	return os;
  }

  private readonly T m_container;
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>

}


