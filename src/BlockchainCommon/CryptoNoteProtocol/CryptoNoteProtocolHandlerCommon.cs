// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  struct NOTIFY_NEW_BLOCK_request;

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public interface ICryptoNoteProtocol
  {
	void relayBlock(NOTIFY_NEW_BLOCK_request arg);
	void relayTransactions(List<BinaryArray> transactions);
  }

  public class ICryptoNoteProtocolHandler : ICryptoNoteProtocol, ICryptoNoteProtocolQuery
  {
  }
}
