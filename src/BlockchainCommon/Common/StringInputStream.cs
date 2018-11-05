// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public class StringInputStream : IInputStream
{
  public StringInputStream(string in)
  {
	  this.in = in;
	  this.offset = 0;
  }
  public override ulong readSome(object data, ulong size)
  {
	if (size > in.Length - offset)
	{
	  size = in.Length - offset;
	}

//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, in.data() + offset, size);
	offset += size;
	return size;
  }

  private readonly string in;
  private ulong offset = new ulong();
}

}


