// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace command_line
{
//C++ TO C# CONVERTER TODO TASK: C++ template specifiers with non-type parameters cannot be converted to C#:
//ORIGINAL LINE: template<typename T, bool required = false>
//C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  struct arg_descriptor;

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct arg_descriptor<T, false>
  public partial class arg_descriptor <T>
  {

	public readonly string name;
	public readonly string description;
	public T default_value = new T();
	public bool not_use_default;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct arg_descriptor<ClassicVector<T>, false>
  public partial class arg_descriptor <T>
  {

	public readonly string name;
	public readonly string description;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct arg_descriptor<T, true>
  public partial class arg_descriptor <T>
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
//	static_assert(!std::is_same<T, bool>::value, "Boolean switch can't be required");


	public readonly string name;
	public readonly string description;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
}


