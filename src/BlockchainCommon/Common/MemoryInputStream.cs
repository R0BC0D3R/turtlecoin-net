// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Diagnostics;

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


