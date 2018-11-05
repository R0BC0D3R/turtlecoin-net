// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace System
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
public class OperationTimeout <T> : System.IDisposable
{
  public OperationTimeout(Dispatcher dispatcher, T @object, std::chrono.nanoseconds timeout)
  {
	  this.@object = @object;
	  this.timerContext = dispatcher;
	  this.timeoutTimer = dispatcher;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: timerContext.spawn([this, timeout]()
	timerContext.spawn(() =>
	{
	  try
	  {
		timeoutTimer.sleep(timeout);
		timerContext.interrupt();
	  }
	  catch
	  {
	  }
	});
  }

  public void Dispose()
  {
	timerContext.interrupt();
	timerContext.wait();
  }

  private T @object;
  private ContextGroup timerContext = new ContextGroup();
  private Timer timeoutTimer = new Timer();
}

}
