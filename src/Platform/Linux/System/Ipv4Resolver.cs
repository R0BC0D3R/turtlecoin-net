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



namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Dispatcher;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Ipv4Address;

public class Ipv4Resolver : System.IDisposable
{
  public Ipv4Resolver()
  {
	  this.dispatcher = null;
  }
  public Ipv4Resolver(Dispatcher dispatcher)
  {
	  this.dispatcher = dispatcher;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Ipv4Resolver(const Ipv4Resolver&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public Ipv4Resolver(Ipv4Resolver && other)
  {
	  this.dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Ipv4Resolver& operator =(const Ipv4Resolver&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static Ipv4Resolver operator = (Ipv4Resolver && other)
  {
	dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  other.dispatcher = null;
	}

	return this;
  }
  public Ipv4Address resolve(string host)
  {
	Debug.Assert(dispatcher != null);
	if (dispatcher.interrupted())
	{
	  throw InterruptedException();
	}

	addrinfo hints = new addrinfo(0, AF_INET, SOCK_STREAM, IPPROTO_TCP, 0, null, null, null);
	addrinfo addressInfos;
	int result = getaddrinfo(host, null, hints, addressInfos);
	if (result != 0)
	{
	  throw new System.Exception("Ipv4Resolver::resolve, getaddrinfo failed, " + errorMessage(result));
	}

	uint count = 0;
	for (addrinfo * addressInfo = addressInfos; addressInfo != null; addressInfo = addressInfo.ai_next)
	{
	  ++count;
	}

	std::mt19937 generator = new std::mt19937(std::random_device()());
	uint index = std::uniform_int_distribution<uint>(0, count - 1)(generator);
	addrinfo addressInfo = addressInfos;
	for (uint i = 0; i < index; ++i)
	{
	  addressInfo = addressInfo.ai_next;
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	Ipv4Address address = new Ipv4Address(ntohl(reinterpret_cast<sockaddr_in>(addressInfo.ai_addr).sin_addr.s_addr));
	freeaddrinfo(addressInfo);
	return address;
  }

  private Dispatcher dispatcher;
}

}




