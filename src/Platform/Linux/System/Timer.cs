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

public class Timer : System.IDisposable
{
  public Timer()
  {
	  this.dispatcher = null;
  }
  public Timer(Dispatcher dispatcher)
  {
	  this.dispatcher = dispatcher;
	  this.context = null;
	  this.timer = -1;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Timer(const Timer&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public Timer(Timer && other)
  {
	  this.dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  timer = other.timer;
	  context = null;
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
	Debug.Assert(dispatcher == null || context == null);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Timer& operator =(const Timer&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static Timer operator = (Timer && other)
  {
	Debug.Assert(dispatcher == null || context == null);
	dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  timer = other.timer;
	  context = null;
	  other.dispatcher = null;
	  other.timer = -1;
	}

	return this;
  }
  public void sleep(std::chrono.nanoseconds duration)
  {
	Debug.Assert(dispatcher != null);
	Debug.Assert(context == null);
	if (dispatcher.interrupted())
	{
	  throw InterruptedException();
	}

	if (duration.count() == 0)
	{
	  dispatcher.yield();
	}
	else
	{
	  timer = dispatcher.getTimer();

	  var seconds = std::chrono.duration_cast<std::chrono.seconds>(duration);
	  itimerspec expires = new itimerspec();
	  expires.it_interval.tv_nsec = expires.it_interval.tv_sec = 0;
	  expires.it_value.tv_sec = seconds.count();
	  expires.it_value.tv_nsec = std::chrono.duration_cast<std::chrono.nanoseconds>(duration - seconds).count();
	  timerfd_settime(timer, 0, expires, null);

	  ContextPair contextPair = new ContextPair();
	  OperationContext timerContext = new OperationContext();
	  timerContext.interrupted = false;
	  timerContext.context = dispatcher.getCurrentContext();
	  contextPair.writeContext = null;
	  contextPair.readContext = timerContext;

	  epoll_event timerEvent = new epoll_event();
	  timerEvent.events = EPOLLIN | EPOLLONESHOT;
	  timerEvent.data.ptr = contextPair;

	  if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_MOD, timer, timerEvent) == -1)
	  {
		throw new System.Exception("Timer::sleep, epoll_ctl failed, " + lastErrorMessage());
	  }
	  dispatcher.getCurrentContext().interruptProcedure = () =>
	  {
		  Debug.Assert(dispatcher != null);
		  Debug.Assert(context != null);
		  OperationContext timerContext = (OperationContext)context;
		  if (!timerContext.interrupted)
		  {
			uint64_t value = 0;
			if (global::read(timer, value, sizeof (uint64_t)) == -1)
			{
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
  //#pragma GCC diagnostic push
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
  //#pragma GCC diagnostic ignored "-Wlogical-op"
			  if (errno == EAGAIN || errno == EWOULDBLOCK)
			  {
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
  //#pragma GCC diagnostic pop
				timerContext.interrupted = true;
				dispatcher.pushContext(timerContext.context);
			  }
			  else
			  {
				throw new System.Exception("Timer::sleep, interrupt procedure, read failed, " + lastErrorMessage());
			  }
			}
			else
			{
			  Debug.Assert(value > 0);
			  dispatcher.pushContext(timerContext.context);
			}

			epoll_event timerEvent = new epoll_event();
			timerEvent.events = EPOLLONESHOT;
			timerEvent.data.ptr = null;

			if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_MOD, timer, timerEvent) == -1)
			{
			  throw new System.Exception("Timer::sleep, interrupt procedure, epoll_ctl failed, " + lastErrorMessage());
			}
		  }
	  };

	  context = timerContext;
	  dispatcher.dispatch();
	  dispatcher.getCurrentContext().interruptProcedure = null;
	  Debug.Assert(dispatcher != null);
	  Debug.Assert(timerContext.context == dispatcher.getCurrentContext());
	  Debug.Assert(contextPair.writeContext == null);
	  Debug.Assert(context == &timerContext);
	  context = null;
	  timerContext.context = null;
	  dispatcher.pushTimer(timer);
	  if (timerContext.interrupted)
	  {
		throw InterruptedException();
	  }
	}
  }

  private Dispatcher dispatcher;
  private object context;
  private int timer;
}

}




