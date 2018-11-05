// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using CryptoNote;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class KVBinaryInputStreamSerializer : JsonInputValueSerializer
{
  public KVBinaryInputStreamSerializer(Common.IInputStream strm) : base.functorMethod(parseBinary(strm))
  {
  }

  public override bool binary(object value, uint64_t size, Common.StringView name)
  {
	string str;

	if (!this(str, name))
	{
	  return false;
	}

	if (str.Length != size)
	{
	  throw new System.Exception("Binary block size mismatch");
	}

//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(value, str.data(), size);
	return true;
  }
  public override bool binary(string value, Common.StringView name)
  {
	return this(value, name); // load as string
  }
}

}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>