// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

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
