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
