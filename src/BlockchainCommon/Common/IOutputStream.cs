﻿// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Common
{

public abstract class IOutputStream : System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public abstract uint64_t writeSome(object data, uint64_t size);
}

}

