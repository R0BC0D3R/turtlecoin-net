// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



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
