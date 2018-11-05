// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public class ICryptoNoteProtocolObserver
{
  public virtual void peerCountUpdated(uint count)
  {
  }
  public virtual void lastKnownBlockHeightUpdated(uint height)
  {
  }
  public virtual void blockchainSynchronized(uint topHeight)
  {
  }
}

} //namespace CryptoNote
