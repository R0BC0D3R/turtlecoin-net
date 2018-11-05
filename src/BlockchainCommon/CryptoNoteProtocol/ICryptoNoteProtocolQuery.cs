// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ICryptoNoteProtocolObserver;

public interface ICryptoNoteProtocolQuery
{
  bool addObserver(ICryptoNoteProtocolObserver observer);
  bool removeObserver(ICryptoNoteProtocolObserver observer);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getObservedHeight() const = 0;
  uint getObservedHeight();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockchainHeight() const = 0;
  uint getBlockchainHeight();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPeerCount() const = 0;
  uint getPeerCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isSynchronized() const = 0;
  bool isSynchronized();
}

} //namespace CryptoNote
