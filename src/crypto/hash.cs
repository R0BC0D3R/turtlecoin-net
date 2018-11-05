//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push, 1)
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//union hash_state

//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum:
public enum AnonymousEnum
{
  HASH_SIZE = 32,
  HASH_DATA_AREA = 136,
  SLOW_HASH_CONTEXT_SIZE = 2097552,
  SLOW_HASH_CONTEXT_LITE_SIZE = 1048976 // Suml: Unused for now but this is the right size for 1MB scratchpads.
}