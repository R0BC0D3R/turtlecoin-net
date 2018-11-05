// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Event;

public class EventLock : System.IDisposable
{
  public explicit EventLock(Event & event);
  public void Dispose()
  {
	event.set();
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  EventLock& operator =(const EventLock&) = delete;

  private Event & event;
}

}


namespace System
{

private EventLock.EventLock(Event & event) : event(event)
{
  while (!event.get())
  {
	event.wait();
  }

  event.clear();
}

}
