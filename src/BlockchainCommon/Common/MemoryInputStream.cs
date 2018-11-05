// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Diagnostics;

namespace Common
{

  public class MemoryInputStream : IInputStream
  {
	public MemoryInputStream(object buffer, ulong bufferSize)
	{
		this.buffer = (char)buffer;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.bufferSize = bufferSize;
		this.bufferSize.CopyFrom(bufferSize);
		this.position = 0;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getPosition() const
	public ulong getPosition()
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
	public override ulong readSome(object data, ulong size)
	{
	  Debug.Assert(position <= bufferSize);
	  ulong readSize = Math.Min(size, bufferSize - position);

	  if (readSize > 0)
	  {
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
		memcpy(data, buffer + position, readSize);
		position += readSize;
	  }

	  return readSize;
	}

	private readonly string buffer;
	private ulong bufferSize = new ulong();
	private ulong position = new ulong();
  }
}


