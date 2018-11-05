// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public class StdInputStream : IInputStream
{
  public StdInputStream(std::istream in)
  {
	  this.in = in;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  StdInputStream& operator =(const StdInputStream&) = delete;
  public override uint64_t readSome(object data, uint64_t size)
  {
	in.read((char)data, size);
	return in.gcount();
  }

  private std::istream in;
}

}


