﻿// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(uint) <= sizeof(type), "Size of " #type " must be at least that of uint"); inline uint hash_value(const type &_v) { return reinterpret_cast<const uint &>(_v); } } namespace std { template<> struct hash<Crypto::type> { uint operator()(const Crypto::type &_v) const { return reinterpret_cast<const uint &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class TransferListFormatter
{
  public TransferListFormatter(CryptoNote.Currency currency, Tuple<WalletTransfers.const_iterator, WalletTransfers.const_iterator> range)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_range = range;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void print(std::ostream& os) const
  public void print(std::ostream os)
  {
	for (var it = m_range.Item1; it != m_range.Item2; ++it)
	{
	  os << '\n' << std::setw(21) << m_currency.formatAmount(it.second.amount) << ' ' << (it.second.address.empty() ? "<UNKNOWN>" : it.second.address) << ' ' << it.second.type;
	}
  }

  public static std::ostream operator << (std::ostream os, TransferListFormatter formatter)
  {
	formatter.print(os);
	return os;
  }

  private readonly CryptoNote.Currency m_currency;
  private readonly Tuple<WalletTransfers.const_iterator, WalletTransfers.const_iterator> m_range;
}

public class WalletOrderListFormatter
{
  public WalletOrderListFormatter(CryptoNote.Currency currency, List<CryptoNote.WalletOrder> walletOrderList)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_walletOrderList = walletOrderList;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void print(std::ostream& os) const
  public void print(std::ostream os)
  {
	os << '{';

	if (m_walletOrderList.Count > 0)
	{
	  os << '<' << m_currency.formatAmount(m_walletOrderList[0].amount) << ", " << m_walletOrderList[0].address << '>';

	  for (var it = std::next(m_walletOrderList.GetEnumerator()); it != m_walletOrderList.end(); ++it)
	  {
		os << '<' << m_currency.formatAmount(it.amount) << ", " << it.address << '>';
	  }
	}

	os << '}';
  }

  public static std::ostream operator << (std::ostream os, WalletOrderListFormatter formatter)
  {
	formatter.print(os);
	return os;
  }

  private readonly CryptoNote.Currency m_currency;
  private readonly List<CryptoNote.WalletOrder> m_walletOrderList;
}

}



