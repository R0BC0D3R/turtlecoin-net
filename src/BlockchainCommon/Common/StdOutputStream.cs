// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public class StdOutputStream : IOutputStream
{
  public StdOutputStream(std::ostream @out)
  {
	  this.@out = @out;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  StdOutputStream& operator =(const StdOutputStream&) = delete;
  public override ulong writeSome(object data, ulong size)
  {
	@out.write((char)data, size);
	if (@out.bad())
	{
	  return 0;
	}

	return size;
  }

  private std::ostream @out;
}

}


