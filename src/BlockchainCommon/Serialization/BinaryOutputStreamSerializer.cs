// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class BinaryOutputStreamSerializer : ISerializer
{
  public BinaryOutputStreamSerializer(Common.IOutputStream strm)
  {
	  this.stream = new Common.IOutputStream(strm);
  }
  public override void Dispose()
  {
	  base.Dispose();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ISerializer::SerializerType type() const override
  public override ISerializer.SerializerType type()
  {
	return ISerializer.OUTPUT;
  }

  public override bool beginObject(Common.StringView name)
  {
	return true;
  }
  public override void endObject()
  {
  }

  public override bool beginArray(uint64_t size, Common.StringView name)
  {
	writeVarint(stream, size);
	return true;
  }
  public override void endArray()
  {
  }

  public static override bool functorMethod(uint8_t value, Common.StringView name)
  {
	writeVarint(stream, value);
	return true;
  }
  public static override bool functorMethod(int16_t value, Common.StringView name)
  {
	writeVarint(stream, (uint16_t)value);
	return true;
  }
  public static override bool functorMethod(uint16_t value, Common.StringView name)
  {
	writeVarint(stream, value);
	return true;
  }
  public static override bool functorMethod(int32_t value, Common.StringView name)
  {
	writeVarint(stream, (uint32_t)value);
	return true;
  }
  public static override bool functorMethod(uint32_t value, Common.StringView name)
  {
	writeVarint(stream, value);
	return true;
  }
  public static override bool functorMethod(int64_t value, Common.StringView name)
  {
	writeVarint(stream, (uint64_t)value);
	return true;
  }
  public static override bool functorMethod(uint64_t value, Common.StringView name)
  {
	writeVarint(stream, value);
	return true;
  }
  public static override bool functorMethod(ref double value, Common.StringView name)
  {
	Debug.Assert(false); //the method is not supported for this type of serialization
	throw new System.Exception("double serialization is not supported in BinaryOutputStreamSerializer");
	return false;
  }
  public static override bool functorMethod(ref bool value, Common.StringView name)
  {
	char boolVal = value;
	checkedWrite(boolVal, 1);
	return true;
  }
  public static override bool functorMethod(string value, Common.StringView name)
  {
	writeVarint(stream, value.Length);
	checkedWrite(value.data(), value.Length);
	return true;
  }
  public override bool binary(object value, uint64_t size, Common.StringView name)
  {
	checkedWrite((char)value, new uint64_t(size));
	return true;
  }
  public override bool binary(string value, Common.StringView name)
  {
	// write as string (with size prefix)
	return this.functorMethod(value, name);
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }

  private void checkedWrite(string buf, uint64_t size)
  {
	write(stream, buf, size);
  }
  private Common.IOutputStream stream;
}

}

