// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public class P2pMessage
{
  public uint32_t type = new uint32_t();
  public BinaryArray data = new BinaryArray();
}

public abstract class IP2pConnection : System.IDisposable
{
  public virtual void Dispose()
  {
  }
  public abstract void read(P2pMessage message);
  public abstract void write(P2pMessage message);
  public abstract void ban();
  public abstract void stop();
}

public interface IP2pNode
{
  std::unique_ptr<IP2pConnection> receiveConnection();
  void stop();
}

}


