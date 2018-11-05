// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using CryptoNote;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class KVBinaryOutputStreamSerializer : ISerializer
{

  public KVBinaryOutputStreamSerializer()
  {
	beginObject(string());
  }
  public override void Dispose()
  {
	  base.Dispose();
  }

  public void dump(IOutputStream target)
  {
	Debug.Assert(m_objectsStack.Count == 1);
	Debug.Assert(m_stack.Count == 1);

	KVBinaryStorageBlockHeader hdr = new KVBinaryStorageBlockHeader();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: hdr.m_signature_a = PORTABLE_STORAGE_SIGNATUREA;
	hdr.m_signature_a.CopyFrom(GlobalMembers.PORTABLE_STORAGE_SIGNATUREA);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: hdr.m_signature_b = PORTABLE_STORAGE_SIGNATUREB;
	hdr.m_signature_b.CopyFrom(GlobalMembers.PORTABLE_STORAGE_SIGNATUREB);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: hdr.m_ver = PORTABLE_STORAGE_FORMAT_VER;
	hdr.m_ver.CopyFrom(GlobalMembers.PORTABLE_STORAGE_FORMAT_VER);

	Common.write(target, hdr, sizeof(CryptoNote.KVBinaryStorageBlockHeader));
	GlobalMembers.writeArraySize(target, new ulong(m_stack[0].count));
	write(target, stream().data(), stream().size());
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ISerializer::SerializerType type() const override
  public override ISerializer.SerializerType type()
  {
	return ISerializer.OUTPUT;
  }

  public override bool beginObject(Common.StringView name)
  {
	checkArrayPreamble(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_OBJECT));

	m_stack.Add(new Level(new Common.StringView(name)));
	m_objectsStack.Add(new MemoryStream());

	return true;
  }
  public override void endObject()
  {
	Debug.Assert(m_objectsStack.Count);

	var level = std::move(m_stack[m_stack.Count - 1]);
	m_stack.RemoveAt(m_stack.Count - 1);

	var objStream = std::move(m_objectsStack[m_objectsStack.Count - 1]);
	m_objectsStack.RemoveAt(m_objectsStack.Count - 1);

	auto @out = stream();

	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_OBJECT), level.name);

	GlobalMembers.writeArraySize(@out, level.count);
	write(@out, objStream.data(), objStream.size());
  }

  public override bool beginArray(ulong size, Common.StringView name)
  {
	m_stack.Add(new Level(new Common.StringView(name), new ulong(size)));
	return true;
  }
  public override void endArray()
  {
	bool validArray = m_stack[m_stack.Count - 1].state == State.Array;
	m_stack.RemoveAt(m_stack.Count - 1);

	if (m_stack[m_stack.Count - 1].state == State.Object && validArray)
	{
	  ++m_stack[m_stack.Count - 1].count;
	}
  }

  public static override bool functorMethod(ushort value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT8), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(short value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT16), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(ushort value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT16), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(int value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT32), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(uint value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT32), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(long value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT64), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(ulong value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT64), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(ref double value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_DOUBLE), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(ref bool value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_BOOL), new Common.StringView(name));
	GlobalMembers.writePod(stream(), value);
	return true;
  }
  public static override bool functorMethod(string value, Common.StringView name)
  {
	writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_STRING), new Common.StringView(name));

	auto @out = stream();
	GlobalMembers.writeArraySize(@out, value.Length);
	write(@out, value.data(), value.Length);
	return true;
  }
  public override bool binary(object value, ulong size, Common.StringView name)
  {
	if (size > 0)
	{
	  writeElementPrefix(new ushort(GlobalMembers.BIN_KV_SERIALIZE_TYPE_STRING), new Common.StringView(name));
	  auto @out = stream();
	  GlobalMembers.writeArraySize(@out, new ulong(size));
	  write(@out, value, size);
	}
	return true;
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


  private void writeElementPrefix(ushort type, Common.StringView name)
  {
	Debug.Assert(m_stack.Count);

	checkArrayPreamble(new ushort(type));
	Level level = m_stack[m_stack.Count - 1];

	if (level.state != State.Array)
	{
	  if (!name.isEmpty())
	  {
		auto s = stream();
		GlobalMembers.writeElementName(s, new Common.StringView(name));
		write(s, type, 1);
	  }
	  ++level.count;
	}
  }
  private void checkArrayPreamble(ushort type)
  {
	if (m_stack.Count == 0)
	{
	  return;
	}

	Level level = m_stack[m_stack.Count - 1];

	if (level.state == State.ArrayPrefix)
	{
	  auto s = stream();
	  GlobalMembers.writeElementName(s, level.name);
	  char c = GlobalMembers.BIN_KV_SERIALIZE_FLAG_ARRAY | type;
	  write(s, c, 1);
	  GlobalMembers.writeArraySize(s, new ulong(level.count));
	  level.state = State.Array;
	}
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void updateState(ushort type);
  private MemoryStream stream()
  {
	Debug.Assert(m_objectsStack.Count);
	return m_objectsStack[m_objectsStack.Count - 1];
  }

  private enum State
  {
	Root,
	Object,
	ArrayPrefix,
	Array
  }

  private class Level
  {
	public State state;
	public string name;
	public ulong count = new ulong();

	public Level(Common.StringView nm)
	{
		this.name = nm;
		this.state = new CryptoNote.KVBinaryOutputStreamSerializer.State.Object;
		this.count = 0;
	}

	public Level(Common.StringView nm, ulong arraySize)
	{
		this.name = nm;
		this.state = new CryptoNote.KVBinaryOutputStreamSerializer.State.ArrayPrefix;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.count = arraySize;
		this.count.CopyFrom(arraySize);
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public Level(Level && rv)
	{
	  state = rv.state;
	  name = std::move(rv.name);
	  count = rv.count;
	}

  }

  private List<MemoryStream> m_objectsStack = new List<MemoryStream>();
  private List<Level> m_stack = new List<Level>();
}

}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>


