// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using JsonValue = Common.JsonValue;
using CryptoNote;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class JsonOutputStreamSerializer : ISerializer
{
  public JsonOutputStreamSerializer()
  {
	  this.root = new Common.JsonValue(JsonValue.OBJECT);
	chain.Add(root.functorMethod);
  }
  public override void Dispose()
  {
	  base.Dispose();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ISerializer::SerializerType type() const override
  public override ISerializer.SerializerType type()
  {
	return ISerializer.OUTPUT;
  }

  public override bool beginObject(Common.StringView name)
  {
	JsonValue parent = chain[chain.Count - 1].functorMethod;
	JsonValue obj = new JsonValue(JsonValue.OBJECT);

	if (parent.isObject())
	{
	  chain.Add(parent.insert((string)name, obj));
	}
	else
	{
	  chain.Add(parent.pushBack(obj));
	}

	return true;
  }
  public override void endObject()
  {
	Debug.Assert(chain.Count > 0);
	chain.RemoveAt(chain.Count - 1);
  }

  public override bool beginArray(ulong size, Common.StringView name)
  {
	JsonValue val = new JsonValue(JsonValue.ARRAY);
	JsonValue res = chain[chain.Count - 1].insert.functorMethod((string)name, val);
	chain.Add(res);
	return true;
  }
  public override void endArray()
  {
	Debug.Assert(chain.Count > 0);
	chain.RemoveAt(chain.Count - 1);
  }

  public static override bool functorMethod(ushort value, Common.StringView name)
  {
	GlobalMembers.insertOrPush(*chain[chain.Count - 1].functorMethod, new Common.StringView(name), (long)value);
	return true;
  }
  public static override bool functorMethod(short value, Common.StringView name)
  {
	long v = (long)value;
	return operator ()(v, new Common.StringView(name));
  }
  public static override bool functorMethod(ushort value, Common.StringView name)
  {
	ulong v = (ulong)value;
	return operator ()(v, new Common.StringView(name));
  }
  public static override bool functorMethod(int value, Common.StringView name)
  {
	long v = (long)value;
	return operator ()(v, new Common.StringView(name));
  }
  public static override bool functorMethod(uint value, Common.StringView name)
  {
	ulong v = (ulong)value;
	return operator ()(v, new Common.StringView(name));
  }
  public static override bool functorMethod(long value, Common.StringView name)
  {
	GlobalMembers.insertOrPush(*chain[chain.Count - 1].functorMethod, new Common.StringView(name), value);
	return true;
  }
  public static override bool functorMethod(ulong value, Common.StringView name)
  {
	long v = (long)value;
	return operator ()(v, new Common.StringView(name));
  }
  public static override bool functorMethod(ref double value, Common.StringView name)
  {
	GlobalMembers.insertOrPush(*chain[chain.Count - 1].functorMethod, new Common.StringView(name), value);
	return true;
  }
  public static override bool functorMethod(ref bool value, Common.StringView name)
  {
	GlobalMembers.insertOrPush(*chain[chain.Count - 1].functorMethod, new Common.StringView(name), value);
	return true;
  }
  public static override bool functorMethod(string value, Common.StringView name)
  {
	GlobalMembers.insertOrPush(*chain[chain.Count - 1].functorMethod, new Common.StringView(name), value);
	return true;
  }
  public override bool binary(object value, ulong size, Common.StringView name)
  {
	string hex = Common.toHex(value, size);
	return this.functorMethod(hex, name);
  }
  public override bool binary(string value, Common.StringView name)
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	return binary(const_cast<char>(value.data()), value.Length, new Common.StringView(name));
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Common::JsonValue& getValue() const
  public Common.JsonValue getValue()
  {
	return root.functorMethod;
  }

  public static std::ostream operator << (std::ostream @out, JsonOutputStreamSerializer enumerator)
  {
	@out << enumerator.root.functorMethod;
	return @out;
  }

  private Common.JsonValue root = new Common.JsonValue();
  private List<Common.JsonValue> chain = new List<Common.JsonValue>();
}

}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>