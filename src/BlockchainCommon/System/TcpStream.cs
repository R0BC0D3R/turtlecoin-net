// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;

public class TcpStreambuf : std::streambuf, System.IDisposable
{
  public TcpStreambuf(TcpConnection connection)
  {
	  this.connection = new System.TcpConnection(connection);
	setg(readBuf[0], readBuf[0], readBuf[0]);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	setp(reinterpret_cast<char>(writeBuf[0]), reinterpret_cast<char>(writeBuf[0] + writeBuf.max_size()));
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpStreambuf(const TcpStreambuf&) = delete;
  public void Dispose()
  {
	dumpBuffer(true);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpStreambuf& operator =(const TcpStreambuf&) = delete;

  private TcpConnection connection;
  private List<char> readBuf = new List<char>(4096);
  private List<ushort> writeBuf = new List<ushort>(1024);

  private override std::streambuf.int_type overflow(std::streambuf.int_type ch)
  {
	if (ch == traits_type.eof())
	{
	  return traits_type.eof();
	}

	if (pptr() == epptr())
	{
	  if (!dumpBuffer(false))
	  {
		return traits_type.eof();
	  }
	}

	*pptr() = (char)ch;
	pbump(1);
	return ch;
  }
  private override int sync()
  {
	return dumpBuffer(true) ? 0 : -1;
  }
  private override std::streambuf.int_type underflow()
  {
	if (gptr() < egptr())
	{
	  return traits_type.to_int_type(*gptr());
	}

	uint bytesRead = new uint();
	try
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  bytesRead = connection.read(reinterpret_cast<ushort>(readBuf[0]), readBuf.max_size());
	}
	catch (System.Exception)
	{
	  return traits_type.eof();
	}

	if (bytesRead == 0)
	{
	  return traits_type.eof();
	}

	setg(readBuf[0], readBuf[0], readBuf[0] + bytesRead);
	return traits_type.to_int_type(*gptr());
  }
  private bool dumpBuffer(bool finalize)
  {
	try
	{
	  uint count = pptr() - pbase();
	  if (count == 0)
	  {
		return true;
	  }

	  uint transferred = connection.write(writeBuf[0], count);
	  if (transferred == count)
	  {
		pbump(-(int)count);
	  }
	  else
	  {
		if (!finalize)
		{
		  uint front = 0;
		  for (uint pos = transferred; pos < count; ++pos, ++front)
		  {
			writeBuf[front] = writeBuf[pos];
		  }

		  pbump(-(int)transferred);
		}
		else
		{
		  uint offset = new uint(transferred);
		  while (offset != count)
		  {
			offset += connection.write(writeBuf[0] + offset, count - offset);
		  }

		  pbump(-(int)count);
		}
	  }
	}
	catch
	{
	  return false;
	}

	return true;
  }
}

}


