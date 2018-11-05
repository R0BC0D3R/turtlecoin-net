// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

namespace System
{

public class ContextGroupTimeout
{
  public ContextGroupTimeout(Dispatcher dispatcher, ContextGroup contextGroup, std::chrono.nanoseconds timeout)
  {
	  this.workingContextGroup = dispatcher;
	  this.timeoutTimer = dispatcher;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: workingContextGroup.spawn([&, timeout]
	workingContextGroup.spawn(() =>
	{
	  try
	  {
		timeoutTimer.sleep(timeout);
		contextGroup.interrupt();
	  }
	  catch (InterruptedException)
	  {
	  }
	});
  }

  private Timer timeoutTimer = new Timer();
  private ContextGroup workingContextGroup = new ContextGroup();
}

}
