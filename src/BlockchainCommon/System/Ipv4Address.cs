// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

namespace System
{

public class Ipv4Address
{
  public Ipv4Address(uint value)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.value = value;
	  this.value.CopyFrom(value);
  }
  public Ipv4Address(string dottedDecimal)
  {
	uint offset = 0;
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
//ORIGINAL LINE: uint getValue() const
  public uint getValue()
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

  private uint value = new uint();
}

}


namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
