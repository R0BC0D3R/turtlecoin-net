// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using CryptoNote;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{

public enum LevinError: int
{
  OK = 0,
  ERROR_CONNECTION = -1,
  ERROR_CONNECTION_NOT_FOUND = -2,
  ERROR_CONNECTION_DESTROYED = -3,
  ERROR_CONNECTION_TIMEDOUT = -4,
  ERROR_CONNECTION_NO_DUPLEX_PROTOCOL = -5,
  ERROR_CONNECTION_HANDLER_NOT_DEFINED = -6,
  ERROR_FORMAT = -7,
}

public class LevinProtocol
{

  public LevinProtocol(System.TcpConnection connection)
  {
	  this.m_conn = new System.TcpConnection(connection);
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>
  public bool invoke<Request, Response>(uint command, Request request, Response response)
  {
	sendMessage(new uint(command), encode(request), true);

	Command cmd = new Command();
	readCommand(cmd);

	if (!cmd.isResponse)
	{
	  return false;
	}

	return decode(cmd.buf, response);
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request>
  public void notify<Request>(uint command, Request request, int UnnamedParameter)
  {
	sendMessage(new uint(command), encode(request), false);
  }

  public class Command
  {
	public uint command = new uint();
	public bool isNotify;
	public bool isResponse;
	public BinaryArray buf = new BinaryArray();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool needReply() const
	public bool needReply()
	{
	  return !(isNotify || isResponse);
	}
  }

  public bool readCommand(Command cmd)
  {
	bucket_head2 head = new bucket_head2();

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	if (!readStrict(reinterpret_cast<ushort>(head), sizeof(bucket_head2)))
	{
	  return false;
	}

	if (head.m_signature != GlobalMembers.LEVIN_SIGNATURE)
	{
	  throw new System.Exception("Levin signature mismatch");
	}

	if (head.m_cb > GlobalMembers.LEVIN_DEFAULT_MAX_PACKET_SIZE)
	{
	  throw new System.Exception("Levin packet size is too big");
	}

	BinaryArray buf = new BinaryArray();

	if (head.m_cb != 0)
	{
	  buf.resize(head.m_cb);
	  if (!readStrict(buf[0], new ulong(head.m_cb)))
	  {
		return false;
	  }
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cmd.command = head.m_command;
	cmd.command.CopyFrom(head.m_command);
	cmd.buf = std::move(buf);
	cmd.isNotify = !head.m_have_to_return_data;
	cmd.isResponse = (head.m_flags & GlobalMembers.LEVIN_PACKET_RESPONSE) == GlobalMembers.LEVIN_PACKET_RESPONSE;

	return true;
  }

  public void sendMessage(uint command, BinaryArray @out, bool needResponse)
  {
	bucket_head2 head = new bucket_head2();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_signature = LEVIN_SIGNATURE;
	head.m_signature.CopyFrom(GlobalMembers.LEVIN_SIGNATURE);
	head.m_cb = @out.size();
	head.m_have_to_return_data = needResponse;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_command = command;
	head.m_command.CopyFrom(command);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_protocol_version = LEVIN_PROTOCOL_VER_1;
	head.m_protocol_version.CopyFrom(GlobalMembers.LEVIN_PROTOCOL_VER_1);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_flags = LEVIN_PACKET_REQUEST;
	head.m_flags.CopyFrom(GlobalMembers.LEVIN_PACKET_REQUEST);

	// write header and body in one operation
	BinaryArray writeBuffer = new BinaryArray();
	writeBuffer.reserve(sizeof(bucket_head2) + @out.size());

	Common.VectorOutputStream stream = new Common.VectorOutputStream(writeBuffer);
	stream.writeSome(head, sizeof(bucket_head2));
	stream.writeSome(@out.data(), @out.size());

	writeStrict(writeBuffer.data(), writeBuffer.size());
  }
  public void sendReply(uint command, BinaryArray @out, int returnCode)
  {
	bucket_head2 head = new bucket_head2();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_signature = LEVIN_SIGNATURE;
	head.m_signature.CopyFrom(GlobalMembers.LEVIN_SIGNATURE);
	head.m_cb = @out.size();
	head.m_have_to_return_data = false;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_command = command;
	head.m_command.CopyFrom(command);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_protocol_version = LEVIN_PROTOCOL_VER_1;
	head.m_protocol_version.CopyFrom(GlobalMembers.LEVIN_PROTOCOL_VER_1);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_flags = LEVIN_PACKET_RESPONSE;
	head.m_flags.CopyFrom(GlobalMembers.LEVIN_PACKET_RESPONSE);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: head.m_return_code = returnCode;
	head.m_return_code.CopyFrom(returnCode);

	BinaryArray writeBuffer = new BinaryArray();
	writeBuffer.reserve(sizeof(bucket_head2) + @out.size());

	Common.VectorOutputStream stream = new Common.VectorOutputStream(writeBuffer);
	stream.writeSome(head, sizeof(bucket_head2));
	stream.writeSome(@out.data(), @out.size());

	writeStrict(writeBuffer.data(), writeBuffer.size());
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public static bool decode<T>(BinaryArray buf, T value)
  {
	try
	{
	  Common.MemoryInputStream stream = new Common.MemoryInputStream(buf.data(), buf.size());
	  KVBinaryInputStreamSerializer serializer = new KVBinaryInputStreamSerializer(stream);
	  CryptoNote.GlobalMembers.serialize(value, serializer);
	}
	catch (System.Exception)
	{
	  return false;
	}

	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
  public static BinaryArray encode<T>(T value)
  {
	BinaryArray result = new BinaryArray();
	KVBinaryOutputStreamSerializer serializer = new KVBinaryOutputStreamSerializer();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	CryptoNote.GlobalMembers.serialize(const_cast<T&>(value), serializer.functorMethod);
	Common.VectorOutputStream stream = new Common.VectorOutputStream(result);
	serializer.dump(stream);
	return result;
  }


  private bool readStrict(ushort ptr, size_t size)
  {
	size_t offset = 0;
	while (offset < size)
	{
	  size_t read = m_conn.read(ptr + offset, size - offset);
	  if (read == 0)
	  {
		return false;
	  }

	  offset += read;
	}

	return true;
  }
  private void writeStrict(ushort ptr, size_t size)
  {
	size_t offset = 0;
	while (offset < size)
	{
	  offset += m_conn.write(ptr + offset, size - offset);
	}
  }
  private System.TcpConnection m_conn;
}

}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push)
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(1)
public class bucket_head2
{
  public ulong m_signature = new ulong();
  public ulong m_cb = new ulong();
  public bool m_have_to_return_data;
  public uint m_command = new uint();
  public int m_return_code = new int();
  public uint m_flags = new uint();
  public uint m_protocol_version = new uint();
}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(pop)

