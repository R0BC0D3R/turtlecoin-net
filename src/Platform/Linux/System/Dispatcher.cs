using System;
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


#if ! __GLIBC__
#endif

namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct NativeContextGroup;

public class NativeContext
{
  public object ucontext;
  public object[] stackPtr;
  public bool interrupted;
  public bool inExecutionQueue;
  public NativeContext next;
  public NativeContextGroup group;
  public NativeContext groupPrev;
  public NativeContext groupNext;
  public Action procedure;
  public Action interruptProcedure;
}

public class NativeContextGroup
{
  public NativeContext firstContext;
  public NativeContext lastContext;
  public NativeContext firstWaiter;
  public NativeContext lastWaiter;
}

public class OperationContext
{
  public NativeContext context;
  public bool interrupted;
  public uint32_t events = new uint32_t();
}

public class ContextPair
{
  public OperationContext readContext;
  public OperationContext writeContext;
}

public class Dispatcher : System.IDisposable
{
  public Dispatcher()
  {
	string message;
	epoll = global::epoll_create1(0);
	if (epoll == -1)
	{
	  message = "epoll_create1 failed, " + lastErrorMessage();
	}
	else
	{
	  mainContext.ucontext = new ucontext_t();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (getcontext(reinterpret_cast<ucontext_t>(mainContext.ucontext)) == -1)
	  {
		message = "getcontext failed, " + lastErrorMessage();
	  }
	  else
	  {
		remoteSpawnEvent = eventfd(0, O_NONBLOCK);
		if (remoteSpawnEvent == -1)
		{
		  message = "eventfd failed, " + lastErrorMessage();
		}
		else
		{
		  remoteSpawnEventContext.writeContext = null;
		  remoteSpawnEventContext.readContext = null;

		  epoll_event remoteSpawnEventEpollEvent = new epoll_event();
		  remoteSpawnEventEpollEvent.events = EPOLLIN;
		  remoteSpawnEventEpollEvent.data.ptr = remoteSpawnEventContext;

		  if (epoll_ctl(epoll, EPOLL_CTL_ADD, remoteSpawnEvent, remoteSpawnEventEpollEvent) == -1)
		  {
			message = "epoll_ctl failed, " + lastErrorMessage();
		  }
		  else
		  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
			*reinterpret_cast<pthread_mutex_t>(this.mutex) = pthread_mutex_t(PTHREAD_MUTEX_INITIALIZER);

			mainContext.interrupted = false;
			mainContext.group = contextGroup;
			mainContext.groupPrev = null;
			mainContext.groupNext = null;
			mainContext.inExecutionQueue = false;
			contextGroup.firstContext = null;
			contextGroup.lastContext = null;
			contextGroup.firstWaiter = null;
			contextGroup.lastWaiter = null;
			currentContext = mainContext;
			firstResumingContext = null;
			firstReusableContext = null;
			runningContextCount = 0;
			return;
		  }

		  var result = close(remoteSpawnEvent);
		  if (result)
		  {
		  }
		  Debug.Assert(result == 0);
		}
	  }

	  var result = close(epoll);
	  if (result)
	  {
	  }
	  Debug.Assert(result == 0);
	}

	throw new System.Exception("Dispatcher::Dispatcher, " + message);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Dispatcher(const Dispatcher&) = delete;
  public void Dispose()
  {
	for (NativeContext * context = contextGroup.firstContext; context != null; context = context.groupNext)
	{
	  interrupt(context);
	}

	yield();
	Debug.Assert(contextGroup.firstContext == null);
	Debug.Assert(contextGroup.firstWaiter == null);
	Debug.Assert(firstResumingContext == null);
	Debug.Assert(runningContextCount == 0);
	while (firstReusableContext != null)
	{
	  var ucontext = (ucontext_t)firstReusableContext.ucontext;
	  var stackPtr = (uint8_t)firstReusableContext.stackPtr;
	  firstReusableContext = firstReusableContext.next;
	  Arrays.DeleteArray(stackPtr);
	  if (ucontext != null)
	  {
		  ucontext.Dispose();
	  }
	}

	while (timers.Count > 0)
	{
	  int result = global::close(timers.Peek());
	  if (result != 0)
	  {
	  }
	  Debug.Assert(result == 0);
	  timers.Pop();
	}

	var result = close(epoll);
	if (result)
	{
	}
	Debug.Assert(result == 0);
	result = close(remoteSpawnEvent);
	Debug.Assert(result == 0);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	result = pthread_mutex_destroy(reinterpret_cast<pthread_mutex_t>(this.mutex));
	Debug.Assert(result == 0);
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  Dispatcher& operator =(const Dispatcher&) = delete;
  public void clear()
  {
	while (firstReusableContext != null)
	{
	  var ucontext = (ucontext_t)firstReusableContext.ucontext;
	  var stackPtr = (uint8_t)firstReusableContext.stackPtr;
	  firstReusableContext = firstReusableContext.next;
	  stackPtr = null;
	  ucontext = null;
	}

	while (timers.Count > 0)
	{
	  int result = global::close(timers.Peek());
	  if (result == -1)
	  {
		throw new System.Exception("Dispatcher::clear, close failed, " + lastErrorMessage());
	  }

	  timers.Pop();
	}
  }
  public void dispatch()
  {
	NativeContext context;
	for (;;)
	{
	  if (firstResumingContext != null)
	  {
		context = firstResumingContext;
		firstResumingContext = context.next;

		Debug.Assert(context.inExecutionQueue);
		context.inExecutionQueue = false;

		break;
	  }

	  epoll_event event = new epoll_event();
	  int count = epoll_wait(epoll, event, 1, -1);
	  if (count == 1)
	  {
		ContextPair contextPair = (ContextPair)event.data.ptr;
		if (((event.events & (EPOLLIN | EPOLLOUT)) != 0) && contextPair.readContext == null && contextPair.writeContext == null)
		{
		  uint64_t buf = new uint64_t();
		  var transferred = read(remoteSpawnEvent, buf, sizeof (uint64_t));
		  if (transferred == -1)
		  {
			  throw new System.Exception("Dispatcher::dispatch, read(remoteSpawnEvent) failed, " + lastErrorMessage());
		  }

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  MutextGuard guard = new MutextGuard(*reinterpret_cast<pthread_mutex_t>(this.mutex));
		  while (remoteSpawningProcedures.Count > 0)
		  {
			spawn(std::move(remoteSpawningProcedures.Peek()));
			remoteSpawningProcedures.Dequeue();
		  }

		  continue;
		}

		if ((event.events & EPOLLOUT) != 0)
		{
		  context = contextPair.writeContext.context;
		  contextPair.writeContext.events = event.events;
		}
		else if ((event.events & EPOLLIN) != 0)
		{
		  context = contextPair.readContext.context;
		  contextPair.readContext.events = event.events;
		}
		else
		{
		  continue;
		}

		Debug.Assert(context != null);
		break;
	  }

	  if (errno != EINTR)
	  {
		throw new System.Exception("Dispatcher::dispatch, epoll_wait failed, " + lastErrorMessage());
	  }
	}

	if (context != currentContext)
	{
	  ucontext_t oldContext = (ucontext_t)currentContext.ucontext;
	  currentContext = context;
	  if (swapcontext(oldContext, (ucontext_t)context.ucontext) == -1)
	  {
		throw new System.Exception("Dispatcher::dispatch, swapcontext failed, " + lastErrorMessage());
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: NativeContext* getCurrentContext() const
  public NativeContext getCurrentContext()
  {
	return currentContext;
  }
  public void interrupt()
  {
	interrupt(currentContext);
  }
  public void interrupt(NativeContext context)
  {
	Debug.Assert(context != null);
	if (!context.interrupted)
	{
	  if (context.interruptProcedure != null)
	  {
		context.interruptProcedure();
		context.interruptProcedure = null;
	  }
	  else
	  {
		context.interrupted = true;
	  }
	}
  }
  public bool interrupted()
  {
	if (currentContext.interrupted)
	{
	  currentContext.interrupted = false;
	  return true;
	}

	return false;
  }
  public void pushContext(NativeContext context)
  {
	Debug.Assert(context != null);

	if (context.inExecutionQueue)
	{
	  return;
	}

	context.next = null;
	context.inExecutionQueue = true;

	if (firstResumingContext != null)
	{
	  Debug.Assert(lastResumingContext != null);
	  lastResumingContext.next = context;
	}
	else
	{
	  firstResumingContext = context;
	}

	lastResumingContext = context;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public void remoteSpawn(Action && procedure)
  {
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  MutextGuard guard = new MutextGuard(*reinterpret_cast<pthread_mutex_t>(this.mutex));
	  remoteSpawningProcedures.Enqueue(std::move(procedure));
	}
	uint64_t one = 1;
	var transferred = write(remoteSpawnEvent, one, sizeof (uint64_t));
	if (transferred == - 1)
	{
	  throw new System.Exception("Dispatcher::remoteSpawn, write failed, " + lastErrorMessage());
	}
  }
  public void yield()
  {
	for (;;)
	{
	  epoll_event[] events = Arrays.InitializeWithDefaultInstances<epoll_event>(16);
	  int count = epoll_wait(epoll, events, 16, 0);
	  if (count == 0)
	  {
		break;
	  }

	  if (count > 0)
	  {
		for (int i = 0; i < count; ++i)
		{
		  ContextPair contextPair = (ContextPair)(events[i].data.ptr);
		  if (((events[i].events & (EPOLLIN | EPOLLOUT)) != 0) && contextPair.readContext == null && contextPair.writeContext == null)
		  {
			uint64_t buf = new uint64_t();
			var transferred = read(remoteSpawnEvent, buf, sizeof (uint64_t));
			if (transferred == -1)
			{
			  throw new System.Exception("Dispatcher::dispatch, read(remoteSpawnEvent) failed, " + lastErrorMessage());
			}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
			MutextGuard guard = new MutextGuard(*reinterpret_cast<pthread_mutex_t>(this.mutex));
			while (remoteSpawningProcedures.Count > 0)
			{
			  spawn(std::move(remoteSpawningProcedures.Peek()));
			  remoteSpawningProcedures.Dequeue();
			}

			continue;
		  }

		  if ((events[i].events & EPOLLOUT) != 0)
		  {
			if (contextPair.writeContext != null)
			{
			  if (contextPair.writeContext.context != null)
			  {
				contextPair.writeContext.context.interruptProcedure = null;
			  }
			  pushContext(contextPair.writeContext.context);
			  contextPair.writeContext.events = events[i].events;
			}
		  }
		  else if ((events[i].events & EPOLLIN) != 0)
		  {
			if (contextPair.readContext != null)
			{
			  if (contextPair.readContext.context != null)
			  {
				contextPair.readContext.context.interruptProcedure = null;
			  }
			  pushContext(contextPair.readContext.context);
			  contextPair.readContext.events = events[i].events;
			}
		  }
		  else
		  {
			continue;
		  }
		}
	  }
	  else
	  {
		if (errno != EINTR)
		{
		  throw new System.Exception("Dispatcher::dispatch, epoll_wait failed, " + lastErrorMessage());
		}
	  }
	}

	if (firstResumingContext != null)
	{
	  pushContext(currentContext);
	  dispatch();
	}
  }

  // system-dependent
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: int getEpoll() const
  public int getEpoll()
  {
	return epoll;
  }
  public NativeContext getReusableContext()
  {
	if (firstReusableContext == null)
	{
	  ucontext_t newlyCreatedContext = new ucontext_t();
	  if (getcontext(newlyCreatedContext) == -1)
	  { //makecontext precondition
		throw new System.Exception("Dispatcher::getReusableContext, getcontext failed, " + lastErrorMessage());
	  }

	  var stackPointer = Arrays.InitializeWithDefaultInstances<uint8_t>(GlobalMembers.STACK_SIZE);
	  newlyCreatedContext.uc_stack.ss_sp = stackPointer;
	  newlyCreatedContext.uc_stack.ss_size = GlobalMembers.STACK_SIZE;

	  ContextMakingData makingContextData = new ContextMakingData(this, newlyCreatedContext);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  makecontext(newlyCreatedContext, contextProcedureStatic, 1, reinterpret_cast<int>(makingContextData));

	  ucontext_t oldContext = (ucontext_t)currentContext.ucontext;
	  if (swapcontext(oldContext, newlyCreatedContext) == -1)
	  {
		throw new System.Exception("Dispatcher::getReusableContext, swapcontext failed, " + lastErrorMessage());
	  }

	  Debug.Assert(firstReusableContext != null);
	  Debug.Assert(firstReusableContext.ucontext == newlyCreatedContext);
	  firstReusableContext.stackPtr = stackPointer;
	};

	NativeContext context = firstReusableContext;
	firstReusableContext = firstReusableContext.next;
	return context;
  }
  public void pushReusableContext(NativeContext context)
  {
	context.next = firstReusableContext;
	firstReusableContext = context;
	--runningContextCount;
  }
  public int getTimer()
  {
	int timer;
	if (timers.Count == 0)
	{
	  timer = timerfd_create(CLOCK_MONOTONIC, TFD_NONBLOCK);
	  epoll_event timerEvent = new epoll_event();
	  timerEvent.events = EPOLLONESHOT;
	  timerEvent.data.ptr = null;

	  if (epoll_ctl(getEpoll(), EPOLL_CTL_ADD, timer, timerEvent) == -1)
	  {
		throw new System.Exception("Dispatcher::getTimer, epoll_ctl failed, " + lastErrorMessage());
	  }
	}
	else
	{
	  timer = timers.Peek();
	  timers.Pop();
	}

	return timer;
  }
  public void pushTimer(int timer)
  {
	timers.Push(timer);
  }

#if __x86_64__
//C++ TO C# CONVERTER TODO TASK: C# does not allow setting or comparing #define constants:
	#if __WORDSIZE == 64
	public const int SIZEOF_PTHREAD_MUTEX_T = 40;
	#else
	public const int SIZEOF_PTHREAD_MUTEX_T = 32;
	#endif
#elif __aarch64__
public const int SIZEOF_PTHREAD_MUTEX_T = 48;
#else
public const int SIZEOF_PTHREAD_MUTEX_T = 24;
#endif

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private void spawn(Action && procedure)
  {
	NativeContext context = getReusableContext();
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
	pushContext(context);
  }
  private int epoll;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'alignas':
//ORIGINAL LINE: alignas(void*) uint8_t mutex[SIZEOF_PTHREAD_MUTEX_T];
   private uint8_t[] mutex = Arrays.InitializeWithDefaultInstances<uint8_t>(SIZEOF_PTHREAD_MUTEX_T);
  private int remoteSpawnEvent;
  private ContextPair remoteSpawnEventContext = new ContextPair();
  private Queue<Action> remoteSpawningProcedures = new Queue<Action>();
  private Stack<int> timers = new Stack<int>();

  private NativeContext mainContext = new NativeContext();
  private NativeContextGroup contextGroup = new NativeContextGroup();
  private NativeContext currentContext;
  private NativeContext firstResumingContext;
  private NativeContext lastResumingContext;
  private NativeContext firstReusableContext;
  private size_t runningContextCount = new size_t();

  private void contextProcedure(object ucontext)
  {
	Debug.Assert(firstReusableContext == null);
	NativeContext context = new NativeContext();
	context.ucontext = ucontext;
	context.interrupted = false;
	context.next = null;
	context.inExecutionQueue = false;
	firstReusableContext = context;
	ucontext_t oldContext = (ucontext_t)context.ucontext;
	if (swapcontext(oldContext, (ucontext_t)currentContext.ucontext) == -1)
	{
	  throw new System.Exception("Dispatcher::contextProcedure, swapcontext failed, " + lastErrorMessage());
	}

	for (;;)
	{
	  ++runningContextCount;
	  try
	  {
		context.procedure();
	  }
	  catch
	  {
	  }

	  if (context.group != null)
	  {
		if (context.groupPrev != null)
		{
		  Debug.Assert(context.groupPrev.groupNext == context);
		  context.groupPrev.groupNext = context.groupNext;
		  if (context.groupNext != null)
		  {
			Debug.Assert(context.groupNext.groupPrev == context);
			context.groupNext.groupPrev = context.groupPrev;
		  }
		  else
		  {
			Debug.Assert(context.group.lastContext == context);
			context.group.lastContext = context.groupPrev;
		  }
		}
		else
		{
		  Debug.Assert(context.group.firstContext == context);
		  context.group.firstContext = context.groupNext;
		  if (context.groupNext != null)
		  {
			Debug.Assert(context.groupNext.groupPrev == context);
			context.groupNext.groupPrev = null;
		  }
		  else
		  {
			Debug.Assert(context.group.lastContext == context);
			if (context.group.firstWaiter != null)
			{
			  if (firstResumingContext != null)
			  {
				Debug.Assert(lastResumingContext.next == null);
				lastResumingContext.next = context.group.firstWaiter;
			  }
			  else
			  {
				firstResumingContext = context.group.firstWaiter;
			  }

			  lastResumingContext = context.group.lastWaiter;
			  context.group.firstWaiter = null;
			}
		  }
		}

		pushReusableContext(context);
	  }

	  dispatch();
	}
  }
  private static void contextProcedureStatic(object context)
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	ContextMakingData makingContextData = reinterpret_cast<ContextMakingData>(context);
	makingContextData.dispatcher.contextProcedure(makingContextData.ucontext);
  }
}

}



namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class ContextMakingData
{
  public Dispatcher dispatcher;
  public object ucontext;
}

public class MutextGuard : System.IDisposable
{
  public MutextGuard(pthread_mutex_t _mutex)
  {
	  this.mutex = _mutex;
	var ret = pthread_mutex_lock(mutex);
	if (ret != 0)
	{
	  throw new System.Exception("pthread_mutex_lock failed, " + errorMessage(ret));
	}
  }

  public void Dispose()
  {
	pthread_mutex_unlock(mutex);
  }

  private pthread_mutex_t mutex;
}


}
