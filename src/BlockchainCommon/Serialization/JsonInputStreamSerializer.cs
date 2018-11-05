// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

//deserialization
public class JsonInputStreamSerializer : JsonInputValueSerializer
{
  public JsonInputStreamSerializer(std::istream stream) : base.functorMethod(getJsonValueFromStreamHelper(stream))
  {
  }
  public override void Dispose()
  {
	  base.Dispose();
  }
}

}



namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


} //namespace CryptoNote
