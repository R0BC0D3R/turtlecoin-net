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
