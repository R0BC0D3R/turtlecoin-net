// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Observer, typename Base>
//C++ TO C# CONVERTER TODO TASK: Classes cannot inherit from generic type parameters in C#:
public class IObservableImpl <Observer, Base>: Base
{

  public override void addObserver(Observer observer)
  {
	m_observerManager.add(observer);
  }

  public override void removeObserver(Observer observer)
  {
	m_observerManager.remove(observer);
  }

  protected Tools.ObserverManager<Observer> m_observerManager = new Tools.ObserverManager<Observer>();
}

}
