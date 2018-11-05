// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

public class MemoryStream: Common.IOutputStream
{

  public MemoryStream()
  {
	  this.m_writePos = 0;
  }

  public override uint64_t writeSome(object data, uint64_t size)
  {
	if (size == 0)
	{
	  return 0;
	}

	if (m_writePos + size > m_buffer.Count != null)
	{
	  m_buffer.Resize(m_writePos + size);
	}

//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(m_buffer[m_writePos], data, size);
	m_writePos += size;
	return size;
  }

  public uint64_t size()
  {
	return m_buffer.Count;
  }

  public uint8_t data()
  {
	return m_buffer.data();
  }

  public void clear()
  {
	m_writePos = 0;
	m_buffer.Resize(0);
  }

  private uint64_t m_writePos = new uint64_t();
  private List<uint8_t> m_buffer = new List<uint8_t>();
}

}

