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



namespace CryptoNote
{
namespace error
{

public enum WalletServiceErrorCode
{
  WRONG_KEY_FORMAT = 1,
  WRONG_PAYMENT_ID_FORMAT,
  WRONG_HASH_FORMAT,
  OBJECT_NOT_FOUND,
  DUPLICATE_KEY,
  KEYS_NOT_DETERMINISTIC,
}

// custom category:
public class WalletServiceErrorCategory : std::error_category
{
  public static WalletServiceErrorCategory INSTANCE = new WalletServiceErrorCategory();

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* name() const throw() override
  public override string name()
  {
	return "WalletServiceErrorCategory";
  }

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual std::error_condition default_error_condition(int ev) const throw() override
  public override std::error_condition default_error_condition(int ev) const
  {
	return std::error_condition(ev, this);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual string message(int ev) const override
  public override string message(int ev)
  {
	WalletServiceErrorCode code = (WalletServiceErrorCode)ev;

	switch (code)
	{
	  case WalletServiceErrorCode.WRONG_KEY_FORMAT:
		  return "Wrong key format";
	  case WalletServiceErrorCode.WRONG_PAYMENT_ID_FORMAT:
		  return "Wrong payment id format";
	  case WalletServiceErrorCode.WRONG_HASH_FORMAT:
		  return "Wrong block id format";
	  case WalletServiceErrorCode.OBJECT_NOT_FOUND:
		  return "Requested object not found";
	  case WalletServiceErrorCode.DUPLICATE_KEY:
		  return "Duplicate key";
	  case WalletServiceErrorCode.KEYS_NOT_DETERMINISTIC:
		  return "Keys not deterministic";
	  default:
		  return "Unknown error";
	}
  }

  private WalletServiceErrorCategory()
  {
  }
}

} //namespace error
} //namespace CryptoNote

namespace std
{

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct is_error_code_enum<CryptoNote::error::WalletServiceErrorCode>: public true_type
public class is_error_code_enum: true_type
{
}

}


