using System.Collections.Generic;

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

