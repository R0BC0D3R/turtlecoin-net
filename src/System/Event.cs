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

public class Event : System.IDisposable
{
  public Event()
  {
	  this.dispatcher = null;
  }
  public Event(Dispatcher dispatcher)
  {
	  this.dispatcher = dispatcher;
	  this.state = false;
	  this.first = null;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Event(const Event&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public Event(Event && other)
  {
	  this.dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  state = other.state;
	  if (!state)
	  {
		Debug.Assert(other.first == null);
		first = null;
	  }

	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
	Debug.Assert(dispatcher == null || state || first == null);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Event& operator =(const Event&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static Event operator = (Event && other)
  {
	Debug.Assert(dispatcher == null || state || first == null);
	dispatcher = other.dispatcher;
	if (dispatcher != null)
	{
	  state = other.state;
	  if (!state)
	  {
		Debug.Assert(other.first == null);
		first = null;
	  }

	  other.dispatcher = null;
	}

	return this;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get() const
  public bool get()
  {
	Debug.Assert(dispatcher != null);
	return state;
  }
  public void clear()
  {
	Debug.Assert(dispatcher != null);
	if (state)
	{
	  state = false;
	  first = null;
	}
  }
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  void set();
  public void wait()
  {
	Debug.Assert(dispatcher != null);
	if (dispatcher.interrupted())
	{
	  throw InterruptedException();
	}

	if (!state)
	{
	  EventWaiter waiter = new EventWaiter(false, null, null, dispatcher.getCurrentContext());
	  waiter.context.interruptProcedure = () =>
	  {
		if (waiter.next != null)
		{
		  Debug.Assert(waiter.next.prev == waiter);
		  waiter.next.prev = waiter.prev;
		}
		else
		{
		  Debug.Assert(last == waiter);
		  last = waiter.prev;
		}

		if (waiter.prev != null)
		{
		  Debug.Assert(waiter.prev.next == waiter);
		  waiter.prev.next = waiter.next;
		}
		else
		{
		  Debug.Assert(first == waiter);
		  first = waiter.next;
		}

		Debug.Assert(!waiter.interrupted);
		waiter.interrupted = true;
		dispatcher.pushContext(waiter.context);
	  };

	  if (first != null)
	  {
		((EventWaiter)last).next = waiter;
		waiter.prev = (EventWaiter)last;
	  }
	  else
	  {
		first = waiter;
	  }

	  last = waiter;
	  dispatcher.dispatch();
	  Debug.Assert(waiter.context == dispatcher.getCurrentContext());
	  Debug.Assert(waiter.context.interruptProcedure == null);
	  Debug.Assert(dispatcher != null);
	  if (waiter.interrupted)
	  {
		throw InterruptedException();
	  }
	}
  }

  private Dispatcher dispatcher;
  private bool state;
  private object first;
  private object last;
}

}


namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class EventWaiter
{
  public bool interrupted;
  public EventWaiter prev;
  public EventWaiter next;
  public NativeContext context;
}



}
