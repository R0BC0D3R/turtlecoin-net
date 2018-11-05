// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public abstract class IOutputStream : System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public abstract ulong writeSome(object data, ulong size);
}

}

