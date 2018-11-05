using System;
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

public class ContextGroup : System.IDisposable
{
  public ContextGroup(Dispatcher dispatcher)
  {
	  this.dispatcher = dispatcher;
	contextGroup.firstContext = null;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ContextGroup(const ContextGroup&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public ContextGroup(ContextGroup && other)
  {
	  this.dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  Debug.Assert(other.contextGroup.firstContext == null);
	  contextGroup.firstContext = null;
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
	if (dispatcher != null)
	{
	  interrupt();
	  wait();
	}
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ContextGroup& operator =(const ContextGroup&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static ContextGroup operator = (ContextGroup && other)
  {
	Debug.Assert(dispatcher == null || contextGroup.firstContext == null);
	dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  Debug.Assert(other.contextGroup.firstContext == null);
	  contextGroup.firstContext = null;
	  other.dispatcher = null;
	}

	return this;
  }
  public void interrupt()
  {
	Debug.Assert(dispatcher != null);
	for (NativeContext * context = contextGroup.firstContext; context != null; context = context.groupNext)
	{
	  dispatcher.interrupt(context);
	}
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public void spawn(Action && procedure)
  {
	Debug.Assert(dispatcher != null);
	NativeContext context = dispatcher.getReusableContext();
	if (contextGroup.firstContext != null)
	{
	  context.groupPrev = contextGroup.lastContext;
	  Debug.Assert(contextGroup.lastContext.groupNext == null);
	  contextGroup.lastContext.groupNext = context;
	}
	else
	{
	  context.groupPrev = null;
	  contextGroup.firstContext = context;
	  contextGroup.firstWaiter = null;
	}

	context.interrupted = false;
	context.group = contextGroup;
	context.groupNext = null;
	context.procedure = std::move(procedure);
	contextGroup.lastContext = context;
	dispatcher.pushContext(context);
  }
  public void wait()
  {
	if (contextGroup.firstContext != null)
	{
	  NativeContext context = dispatcher.getCurrentContext();
	  context.next = null;

	  Debug.Assert(!context.inExecutionQueue);
	  context.inExecutionQueue = true;

	  if (contextGroup.firstWaiter != null)
	  {
		Debug.Assert(contextGroup.lastWaiter.next == null);
		contextGroup.lastWaiter.next = context;
	  }
	  else
	  {
		contextGroup.firstWaiter = context;
	  }

	  contextGroup.lastWaiter = context;
	  dispatcher.dispatch();
	  Debug.Assert(context == dispatcher.getCurrentContext());
	}
  }

  private Dispatcher dispatcher;
  private NativeContextGroup contextGroup = new NativeContextGroup();
}

}


