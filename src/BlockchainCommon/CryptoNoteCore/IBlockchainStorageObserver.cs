// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
  public abstract class IBlockchainStorageObserver : System.IDisposable
  {
	public virtual void Dispose()
	{
	}

	public abstract void blockchainUpdated();
  }
}
