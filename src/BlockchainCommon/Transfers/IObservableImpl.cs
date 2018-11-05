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
