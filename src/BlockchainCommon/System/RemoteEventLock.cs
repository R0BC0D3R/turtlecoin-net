// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Dispatcher;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Event;

public class RemoteEventLock : System.IDisposable
{
  public RemoteEventLock(Dispatcher & dispatcher, Event & event);
  public void Dispose()
  {
	object mutex = new object();
	std::condition_variable condition = new std::condition_variable();
	bool locked = true;

	Event eventPointer = event;
	dispatcher.remoteSpawn(() =>
	{
	  Debug.Assert(!eventPointer.get());
	  eventPointer.set();

	  mutex.@lock();
	  locked = false;
	  condition.notify_one();
	  mutex.unlock();
	});

	std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);
	while (locked)
	{
	  condition.wait(@lock);
	}
  }

  private Dispatcher dispatcher;
  private Event & event;
}

}


namespace System
{

private RemoteEventLock.RemoteEventLock(Dispatcher & dispatcher, Event & event) : dispatcher(dispatcher), event(event)
{
  object mutex = new object();
  std::condition_variable condition = new std::condition_variable();
  bool locked = false;

  dispatcher.remoteSpawn(() =>
  {
	while (!event.get())
	{
	  event.wait();
	}

	event.clear();
	mutex.@lock();
	locked = true;
	condition.notify_one();
	mutex.unlock();
  });

  std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);
  while (!locked)
  {
	condition.wait(@lock);
  }
}

}
