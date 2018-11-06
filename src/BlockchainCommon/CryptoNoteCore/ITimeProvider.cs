// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

  public abstract class ITimeProvider : System.IDisposable
  {
	public abstract DateTime now();
	public virtual void Dispose()
	{
	}
  }

  public class RealTimeProvider : ITimeProvider
  {
	public override DateTime now()
	{
	  return time(null);
	}
  }

}

