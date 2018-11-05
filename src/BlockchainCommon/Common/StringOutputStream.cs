// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public class StringOutputStream : IOutputStream
{
  public StringOutputStream(string @out)
  {
	  this.@out = @out;
  }
  public override uint64_t writeSome(object data, uint64_t size)
  {
	@out.append((char)data, size);
	return size;
  }

  private string @out;
}

}


