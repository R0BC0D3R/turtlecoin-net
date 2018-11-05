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
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;

public class TcpListener : System.IDisposable
{
  public TcpListener()
  {
	  this.dispatcher = null;
  }
  public TcpListener(Dispatcher dispatcher, Ipv4Address addr, uint16_t port)
  {
	  this.dispatcher = dispatcher;
	string message;
	listener = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listener == -1)
	{
	  message = "socket failed, " + lastErrorMessage();
	}
	else
	{
	  int flags = fcntl(listener, F_GETFL, 0);
	  if (flags == -1 || fcntl(listener, F_SETFL, flags | O_NONBLOCK) == -1)
	  {
		message = "fcntl failed, " + lastErrorMessage();
	  }
	  else
	  {
		int on = 1;
		if (setsockopt(listener, SOL_SOCKET, SO_REUSEADDR, on, sizeof (int)) == -1)
		{
		  message = "setsockopt failed, " + lastErrorMessage();
		}
		else
		{
		  sockaddr_in address = new sockaddr_in();
		  address.sin_family = AF_INET;
		  address.sin_port = htons(port);
		  address.sin_addr.s_addr = htonl(addr.getValue());
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  if (bind(listener, reinterpret_cast<sockaddr>(address), sizeof (sockaddr_in)) != 0)
		  {
			message = "bind failed, " + lastErrorMessage();
		  }
		  else if (listen(listener, SOMAXCONN) != 0)
		  {
			message = "listen failed, " + lastErrorMessage();
		  }
		  else
		  {
			epoll_event listenEvent = new epoll_event();
			listenEvent.events = EPOLLONESHOT;
			listenEvent.data.ptr = null;

			if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_ADD, listener, listenEvent) == -1)
			{
			  message = "epoll_ctl failed, " + lastErrorMessage();
			}
			else
			{
			  context = null;
			  return;
			}
		  }
		}
	  }

	  int result = close(listener);
	  if (result != 0)
	  {
	  }
	  Debug.Assert(result != -1);
	}

	throw new System.Exception("TcpListener::TcpListener, " + message);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpListener(const TcpListener&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public TcpListener(TcpListener && other)
  {
	  this.dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  listener = other.listener;
	  context = null;
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
	if (dispatcher != null)
	{
	  Debug.Assert(context == null);
	  int result = close(listener);
	  if (result != 0)
	  {
	  }
	  Debug.Assert(result != -1);
	}
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpListener& operator =(const TcpListener&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static TcpListener operator = (TcpListener && other)
  {
	if (dispatcher != null)
	{
	  Debug.Assert(context == null);
	  if (close(listener) == -1)
	  {
		throw new System.Exception("TcpListener::operator=, close failed, " + lastErrorMessage());
	  }
	}

	dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  listener = other.listener;
	  context = null;
	  other.dispatcher = null;
	}

	return this;
  }
  public TcpConnection accept()
  {
	Debug.Assert(dispatcher != null);
	Debug.Assert(context == null);
	if (dispatcher.interrupted())
	{
	  throw InterruptedException();
	}

	ContextPair contextPair = new ContextPair();
	OperationContext listenerContext = new OperationContext();
	listenerContext.interrupted = false;
	listenerContext.context = dispatcher.getCurrentContext();

	contextPair.writeContext = null;
	contextPair.readContext = listenerContext;

	epoll_event listenEvent = new epoll_event();
	listenEvent.events = EPOLLIN | EPOLLONESHOT;
	listenEvent.data.ptr = contextPair;
	string message;
	if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_MOD, listener, listenEvent) == -1)
	{
	  message = "epoll_ctl failed, " + lastErrorMessage();
	}
	else
	{
	  context = listenerContext;
	  dispatcher.getCurrentContext().interruptProcedure = () =>
	  {
		  Debug.Assert(dispatcher != null);
		  Debug.Assert(context != null);
		  OperationContext listenerContext = (OperationContext)context;
		  if (!listenerContext.interrupted)
		  {
			epoll_event listenEvent = new epoll_event();
			listenEvent.events = EPOLLONESHOT;
			listenEvent.data.ptr = null;

			if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_MOD, listener, listenEvent) == -1)
			{
			  throw new System.Exception("TcpListener::accept, interrupt procedure, epoll_ctl failed, " + lastErrorMessage());
			}

			listenerContext.interrupted = true;
			dispatcher.pushContext(listenerContext.context);
		  }
	  };

	  dispatcher.dispatch();
	  dispatcher.getCurrentContext().interruptProcedure = null;
	  Debug.Assert(dispatcher != null);
	  Debug.Assert(listenerContext.context == dispatcher.getCurrentContext());
	  Debug.Assert(contextPair.writeContext == null);
	  Debug.Assert(context == &listenerContext);
	  context = null;
	  listenerContext.context = null;
	  if (listenerContext.interrupted)
	  {
		throw InterruptedException();
	  }

	  if ((listenerContext.events & (EPOLLERR | EPOLLHUP)) != 0)
	  {
		throw new System.Exception("TcpListener::accept, accepting failed");
	  }

	  sockaddr inAddr = new sockaddr();
	  socklen_t inLen = sizeof(sockaddr);
	  int connection = global::accept(listener, inAddr, inLen);
	  if (connection == -1)
	  {
		message = "accept failed, " + lastErrorMessage();
	  }
	  else
	  {
		int flags = fcntl(connection, F_GETFL, 0);
		if (flags == -1 || fcntl(connection, F_SETFL, flags | O_NONBLOCK) == -1)
		{
		  message = "fcntl failed, " + lastErrorMessage();
		}
		else
		{
		  return new TcpConnection(dispatcher, connection);
		}

		int result = close(connection);
		if (result != 0)
		{
		}
		Debug.Assert(result != -1);
	  }
	}

	throw new System.Exception("TcpListener::accept, " + message);
  }

  private Dispatcher dispatcher;
  private object context;
  private int listener;
}

}




