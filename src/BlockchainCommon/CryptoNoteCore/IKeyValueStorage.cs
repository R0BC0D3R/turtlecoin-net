// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class WriteBatch;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ReadBatch;

public abstract class IKeyValueStorage : System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract bool insert(WriteBatch batch, bool sync = false);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void read(const ReadBatch& batch) const = 0;
  public abstract void read(ReadBatch batch);
}
}
