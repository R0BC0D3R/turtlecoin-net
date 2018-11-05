// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct hash < CryptoNote::AccountPublicAddress >
public partial class hash 
{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint operator ()(const CryptoNote::AccountPublicAddress& val) const
  public static uint functorMethod(CryptoNote.AccountPublicAddress val)
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	uint spend = (reinterpret_cast<const uint>(val.spendPublicKey));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	uint view = (reinterpret_cast<const uint>(val.viewPublicKey));
	return spend ^ view;
  }
}

}
