using JsonValue = Common.JsonValue;
using CryptoNote;
using System.Collections.Generic;
using System.Diagnostics;

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


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

//deserialization
public class JsonInputValueSerializer : ISerializer
{
  public JsonInputValueSerializer(Common.JsonValue value)
  {
	if (!value.isObject())
	{
	  throw new System.Exception("Serializer doesn't support this type of serialization: Object expected.");
	}

	chain.Add(value.functorMethod);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public JsonInputValueSerializer(Common.JsonValue && value)
  {
	  this.value = new Common.JsonValue(std::move(value.functorMethod));
	if (!this.value.isObject())
	{
	  throw new System.Exception("Serializer doesn't support this type of serialization: Object expected.");
	}

	chain.Add(this.value.functorMethod);
  }
  public override void Dispose()
  {
	  base.Dispose();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ISerializer::SerializerType type() const override
  public override ISerializer.SerializerType type()
  {
	return ISerializer.INPUT;
  }

  public override bool beginObject(Common.StringView name)
  {
	JsonValue parent = chain[chain.Count - 1].functorMethod;

	if (parent.isArray())
	{
	  JsonValue v = parent[idxs[idxs.Count - 1]++];
	  chain.Add(v);
	  return true;
	}

	if (parent.contains((string)name))
	{
	  JsonValue v = parent((string)name);
	  chain.Add(v);
	  return true;
	}

	return false;
  }
  public override void endObject()
  {
	Debug.Assert(chain.Count > 0);
	chain.RemoveAt(chain.Count - 1);
  }

  public override bool beginArray(ref uint64_t size, Common.StringView name)
  {
	JsonValue parent = chain[chain.Count - 1].functorMethod;
	string strName = name;

	if (parent.contains(strName))
	{
	  JsonValue arr = parent(strName);
	  size = arr.size();
	  chain.Add(arr);
	  idxs.Add(0);
	  return true;
	}

	size = 0;
	return false;
  }
  public override void endArray()
  {
	Debug.Assert(chain.Count > 0);
	Debug.Assert(idxs.Count > 0);

	chain.RemoveAt(chain.Count - 1);
	idxs.RemoveAt(idxs.Count - 1);
  }

  public static override bool functorMethod(uint8_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(int16_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(uint16_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(int32_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(uint32_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(int64_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(uint64_t value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(ref double value, Common.StringView name)
  {
	return getNumber(new Common.StringView(name), ref value);
  }
  public static override bool functorMethod(ref bool value, Common.StringView name)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));
	if (ptr == null)
	{
	  return false;
	}
	value = ptr.getBool();
	return true;
  }
  public static override bool functorMethod(ref string value, Common.StringView name)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));
	if (ptr == null)
	{
	  return false;
	}
	value = ptr.getString();
	return true;
  }
  public override bool binary(object value, uint64_t size, Common.StringView name)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));
	if (ptr == null)
	{
	  return false;
	}

	Common.fromHex(ptr.getString(), value, size);
	return true;
  }
  public override bool binary(ref string value, Common.StringView name)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));
	if (ptr == null)
	{
	  return false;
	}

	string valueHex = ptr.getString();
	value = Common.asString(Common.fromHex(valueHex));

	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public static new bool functorMethod<T>(T value, Common.StringView name)
  {
	return base  .functorMethod(value, name);
  }

  private Common.JsonValue value = new Common.JsonValue();
  private readonly List<Common.JsonValue> chain = new List<Common.JsonValue>();
  private List<uint64_t> idxs = new List<uint64_t>();

  private JsonValue getValue(Common.StringView name)
  {
	JsonValue val = chain[chain.Count - 1].functorMethod;
	if (val.isArray())
	{
	  return val[idxs[idxs.Count - 1]++];
	}

	string strName = name;
	return val.contains(strName) ? &val(strName) : null;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  private bool getNumber<T>(Common.StringView name, ref T v)
  {
	var ptr = getValue.functorMethod(new Common.StringView(name));

	if (ptr == null)
	{
	  return false;
	}

	v = (T)ptr.getInteger();
	return true;
  }
}

}
