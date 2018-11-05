// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Diagnostics;

namespace System
{

//C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
//ORIGINAL LINE: template<class T = void>
public class RemoteContext <T = void> : System.IDisposable
{
  // Start a thread, execute operation in it, continue execution of current context.
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: RemoteContext(Dispatcher& d, System.Func<T>&& operation) : dispatcher(d), event(d), procedure(std::move(operation)), future(System::Detail::async<T>([this]
  public RemoteContext(Dispatcher & d, Func<T>&& operation) : dispatcher(d), event(d), procedure(std::move(operation)), future(System.Detail.async<T>(() =>
  {
	  return asyncProcedure();
  })), interrupted(false){} // Run other task on dispatcher until future is ready, then return lambda's result, or rethrow exception. UB if called more than once.T get() const{wait();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	return future.get();
}

  public class NotifyOnDestruction
  {
	public NotifyOnDestruction(Dispatcher & d, Event & e) : dispatcher(d), event(e)
	{
	}

	public void Dispose()
	{
	  // make a local copy; event reference will be dead when function is called
	  var localEvent = &event;
	  // die if this throws...
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: dispatcher.remoteSpawn([=]
	  dispatcher.remoteSpawn(() =>
	  {
		  localEvent.set();
	  });
	}

	public Dispatcher dispatcher;
	public Event & event;
  }
}

}
