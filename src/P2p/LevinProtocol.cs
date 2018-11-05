using CryptoNote;

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

namespace System
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;
}

namespace CryptoNote
{

public enum LevinError: int32_t
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
  public bool invoke<Request, Response>(uint32_t command, Request request, Response response)
  {
	sendMessage(new uint32_t(command), encode(request), true);

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
  public void notify<Request>(uint32_t command, Request request, int UnnamedParameter)
  {
	sendMessage(new uint32_t(command), encode(request), false);
  }

  public class Command
  {
	public uint32_t command = new uint32_t();
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
	if (!readStrict(reinterpret_cast<uint8_t>(head), sizeof(bucket_head2)))
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
	  if (!readStrict(buf[0], new uint64_t(head.m_cb)))
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

  public void sendMessage(uint32_t command, BinaryArray @out, bool needResponse)
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
  public void sendReply(uint32_t command, BinaryArray @out, int32_t returnCode)
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


  private bool readStrict(uint8_t ptr, size_t size)
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
  private void writeStrict(uint8_t ptr, size_t size)
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
  public uint64_t m_signature = new uint64_t();
  public uint64_t m_cb = new uint64_t();
  public bool m_have_to_return_data;
  public uint32_t m_command = new uint32_t();
  public int32_t m_return_code = new int32_t();
  public uint32_t m_flags = new uint32_t();
  public uint32_t m_protocol_version = new uint32_t();
}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(pop)

