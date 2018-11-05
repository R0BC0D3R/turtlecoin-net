using System;
using System.Diagnostics;

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



namespace Common
{

  public class MemoryInputStream : IInputStream
  {
	public MemoryInputStream(object buffer, uint64_t bufferSize)
	{
		this.buffer = (char)buffer;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.bufferSize = bufferSize;
		this.bufferSize.CopyFrom(bufferSize);
		this.position = 0;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getPosition() const
	public uint64_t getPosition()
	{
	  return position;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endOfStream() const
	public bool endOfStream()
	{
	  return position == bufferSize;
	}

	// IInputStream
	public override uint64_t readSome(object data, uint64_t size)
	{
	  Debug.Assert(position <= bufferSize);
	  uint64_t readSize = Math.Min(size, bufferSize - position);

	  if (readSize > 0)
	  {
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
		memcpy(data, buffer + position, readSize);
		position += readSize;
	  }

	  return readSize;
	}

	private readonly string buffer;
	private uint64_t bufferSize = new uint64_t();
	private uint64_t position = new uint64_t();
  }
}


