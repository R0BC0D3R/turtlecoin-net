using System.Collections.Generic;

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

public abstract class IFusionManager : System.IDisposable
{
  public class EstimateResult
  {
	public uint fusionReadyCount;
	public uint totalOutputCount;
  }

  public virtual void Dispose()
  {
  }

  public abstract uint createFusionTransaction(ulong threshold, ushort mixin, List<string> sourceAddresses = {}, string destinationAddress = "");
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isFusionTransaction(uint transactionId) const = 0;
  public abstract bool isFusionTransaction(uint transactionId);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual EstimateResult estimate(ulong threshold, const ClassicVector<string>& sourceAddresses = {}) const = 0;
  public abstract EstimateResult estimate(ulong threshold, List<string> sourceAddresses = {});
}

}
