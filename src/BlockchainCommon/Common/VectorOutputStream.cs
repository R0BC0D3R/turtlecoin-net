// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public class VectorOutputStream : IOutputStream
{
  public VectorOutputStream(List<ushort> @out)
  {
	  this.@out = @out;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  VectorOutputStream& operator =(const VectorOutputStream&) = delete;
  public override ulong writeSome(object data, ulong size)
  {
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
	@out.insert(@out.end(), (ushort)data, (ushort)data + size);
	return size;
  }

  private List<ushort> @out;
}

}


