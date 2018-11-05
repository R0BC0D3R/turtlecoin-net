using System;

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



namespace System
{

public class Ipv4Address
{
  public Ipv4Address(uint32_t value)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.value = value;
	  this.value.CopyFrom(value);
  }
  public Ipv4Address(string dottedDecimal)
  {
	size_t offset = 0;
	value = GlobalMembers.readUint8(dottedDecimal, ref offset);
	if (offset == dottedDecimal.Length || dottedDecimal[offset] != '.')
	{
	  throw new System.Exception("Invalid Ipv4 address string");
	}

	++offset;
	value = value << 8 | GlobalMembers.readUint8(dottedDecimal, ref offset);
	if (offset == dottedDecimal.Length || dottedDecimal[offset] != '.')
	{
	  throw new System.Exception("Invalid Ipv4 address string");
	}

	++offset;
	value = value << 8 | GlobalMembers.readUint8(dottedDecimal, ref offset);
	if (offset == dottedDecimal.Length || dottedDecimal[offset] != '.')
	{
	  throw new System.Exception("Invalid Ipv4 address string");
	}

	++offset;
	value = value << 8 | GlobalMembers.readUint8(dottedDecimal, ref offset);
	if (offset < dottedDecimal.Length)
	{
	  throw new System.Exception("Invalid Ipv4 address string");
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(const Ipv4Address& other) const
  public static bool operator != (Ipv4Address ImpliedObject, Ipv4Address other)
  {
	return ImpliedObject.value != other.value;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const Ipv4Address& other) const
  public static bool operator == (Ipv4Address ImpliedObject, Ipv4Address other)
  {
	return ImpliedObject.value == other.value;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getValue() const
  public uint32_t getValue()
  {
	return value;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isLoopback() const
  public bool isLoopback()
  {
	// 127.0.0.0/8
	return (value & 0xff000000) == (127 << 24);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isPrivate() const
  public bool isPrivate()
  {
	return (int)(value & 0xff000000) == (int)(10 << 24) || (int)(value & 0xfff00000) == (int)((172 << 24) | (16 << 16)) || (int)(value & 0xffff0000) == (int)((192 << 24) | (168 << 16));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string toDottedDecimal() const
  public string toDottedDecimal()
  {
	string result;
	result += Convert.ToString(value >> 24);
	result += '.';
	result += Convert.ToString(value >> 16 & 255);
	result += '.';
	result += Convert.ToString(value >> 8 & 255);
	result += '.';
	result += Convert.ToString(value & 255);
	return result;
  }

  private uint32_t value = new uint32_t();
}

}


namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
