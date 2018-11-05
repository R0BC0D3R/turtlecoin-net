/***********************************************************************
**
** Implementation of the Skein hash function.
**
** Source code author: Doug Whiting, 2008.
**
** This algorithm and source code is released to the public domain.
** 
************************************************************************/


/**************************************************************************
**
** Interface declarations and internal definitions for Skein hashing.
**
** Source code author: Doug Whiting, 2008.
**
** This algorithm and source code is released to the public domain.
**
***************************************************************************
** 
** The following compile-time switches may be defined to control some
** tradeoffs between speed, code size, error checking, and security.
**
** The "default" note explains what happens when the switch is not defined.
**
**  SKEIN_DEBUG            -- make callouts from inside Skein code
**                            to examine/display intermediate values.
**                            [default: no callouts (no overhead)]
**
**  SKEIN_ERR_CHECK        -- how error checking is handled inside Skein
**                            code. If not defined, most error checking 
**                            is disabled (for performance). Otherwise, 
**                            the switch value is interpreted as:
**                                0: use assert()      to flag errors
**                                1: return SKEIN_FAIL to flag errors
**
***************************************************************************/
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN __declspec( dllexport ) void __stdcall
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN __declspec( dllexport ) int __stdcall
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN __declspec( __dllexport__ ) void
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN __declspec( __dllexport__ ) int
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN __declspec( dllimport ) void __stdcall
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN __declspec( dllimport ) int __stdcall
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN __declspec( __dllimport__ ) void
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN __declspec( __dllimport__ ) int
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN void __cdecl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN int __cdecl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VOID_RETURN void
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define INT_RETURN int
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ui_type(size) uint##size##_t
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define dec_unit_type(size,x) typedef ui_type(size) x
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define dec_bufr_type(size,bsize,x) typedef ui_type(size) x[bsize / (size >> 3)]
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ptr_cast(x,size) ((ui_type(size)*)(x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define RotL_64(x,N) (((x) << (N)) | ((x) >> (64-(N))))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define inline __inline
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT32(x) ((uint32_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT64(x) ((uint64_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32(x) ((((uint32_t) (x) & 0x000000ff) << 24) | (((uint32_t) (x) & 0x0000ff00) << 8) | (((uint32_t) (x) & 0x00ff0000) >> 8) | (((uint32_t) (x) & 0xff000000) >> 24))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64(x) ((((uint64_t) (x) & 0x00000000000000ff) << 56) | (((uint64_t) (x) & 0x000000000000ff00) << 40) | (((uint64_t) (x) & 0x0000000000ff0000) << 24) | (((uint64_t) (x) & 0x00000000ff000000) << 8) | (((uint64_t) (x) & 0x000000ff00000000) >> 8) | (((uint64_t) (x) & 0x0000ff0000000000) >> 24) | (((uint64_t) (x) & 0x00ff000000000000) >> 40) | (((uint64_t) (x) & 0xff00000000000000) >> 56))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define PLATFORM_BYTE_ORDER IS_LITTLE_ENDIAN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define PLATFORM_BYTE_ORDER IS_BIG_ENDIAN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define PLATFORM_BYTE_ORDER IS_LITTLE_ENDIAN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define Skein_Put64_LSB_First(dst08,src64,bCnt) memcpy(dst08,src64,bCnt)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define Skein_Get64_LSB_First(dst64,src08,wCnt) memcpy(dst64,src08,8*(wCnt))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define Skein_Swap64(w64) ( (( ((u64b_t)(w64)) & 0xFF) << 56) | (((((u64b_t)(w64)) >> 8) & 0xFF) << 48) | (((((u64b_t)(w64)) >>16) & 0xFF) << 40) | (((((u64b_t)(w64)) >>24) & 0xFF) << 32) | (((((u64b_t)(w64)) >>32) & 0xFF) << 24) | (((((u64b_t)(w64)) >>40) & 0xFF) << 16) | (((((u64b_t)(w64)) >>48) & 0xFF) << 8) | (((((u64b_t)(w64)) >>56) & 0xFF) ) )
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define Skein_Swap64(w64) (w64)

public enum HashReturn
{
  SKEIN_SUCCESS = 0, // return codes from Skein calls
  SKEIN_FAIL = 1,
  SKEIN_BAD_HASHLEN = 2
}








//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_256_STATE_BYTES ( 8*SKEIN_256_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_512_STATE_BYTES ( 8*SKEIN_512_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN1024_STATE_BYTES ( 8*SKEIN1024_STATE_WORDS)

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_256_STATE_BITS (64*SKEIN_256_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_512_STATE_BITS (64*SKEIN_512_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN1024_STATE_BITS (64*SKEIN1024_STATE_WORDS)

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_256_BLOCK_BYTES ( 8*SKEIN_256_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_512_BLOCK_BYTES ( 8*SKEIN_512_STATE_WORDS)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN1024_BLOCK_BYTES ( 8*SKEIN1024_STATE_WORDS)

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_RND_SPECIAL (1000u)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_RND_KEY_INITIAL (SKEIN_RND_SPECIAL+0u)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_RND_KEY_INJECT (SKEIN_RND_SPECIAL+1u)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_RND_FEED_FWD (SKEIN_RND_SPECIAL+2u)

public class Skein_Ctxt_Hdr_t
{
  public size_t hashBitLen = new size_t(); // size of hash result, in bits
  public size_t bCnt = new size_t(); // current byte count in buffer b[]
  public uint64_t[] T = Arrays.InitializeWithDefaultInstances<uint64_t>(DefineConstants.SKEIN_MODIFIER_WORDS); // tweak words: T[0]=byte cnt, T[1]=flags
}

public class Skein_256_Ctxt_t //  256-bit Skein hash context structure
{
  public Skein_Ctxt_Hdr_t h = new Skein_Ctxt_Hdr_t(); // common header context variables
  public uint64_t[] X = Arrays.InitializeWithDefaultInstances<uint64_t>(DefineConstants.SKEIN_256_STATE_WORDS); // chaining variables
  uint8_t b[(8 * DefineConstants.SKEIN_256_STATE_WORDS)]; // partial block buffer (8-byte aligned)
}

public class Skein_512_Ctxt_t //  512-bit Skein hash context structure
{
  public Skein_Ctxt_Hdr_t h = new Skein_Ctxt_Hdr_t(); // common header context variables
  public uint64_t[] X = Arrays.InitializeWithDefaultInstances<uint64_t>(DefineConstants.SKEIN_512_STATE_WORDS); // chaining variables
  uint8_t b[(8 * DefineConstants.SKEIN_512_STATE_WORDS)]; // partial block buffer (8-byte aligned)
}

public class Skein1024_Ctxt_t // 1024-bit Skein hash context structure
{
  public Skein_Ctxt_Hdr_t h = new Skein_Ctxt_Hdr_t(); // common header context variables
  public uint64_t[] X = Arrays.InitializeWithDefaultInstances<uint64_t>(DefineConstants.SKEIN1024_STATE_WORDS); // chaining variables
  uint8_t b[(8 * DefineConstants.SKEIN1024_STATE_WORDS)]; // partial block buffer (8-byte aligned)
}

/*****************************************************************
** Skein block function constants (shared across Ref and Opt code)
******************************************************************/
//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum2:
public enum AnonymousEnum2
{
  /* Skein_256 round rotation constants */
  R_256_0_0 = 14,
  R_256_0_1 = 16,
  R_256_1_0 = 52,
  R_256_1_1 = 57,
  R_256_2_0 = 23,
  R_256_2_1 = 40,
  R_256_3_0 = 5,
  R_256_3_1 = 37,
  R_256_4_0 = 25,
  R_256_4_1 = 33,
  R_256_5_0 = 46,
  R_256_5_1 = 12,
  R_256_6_0 = 58,
  R_256_6_1 = 22,
  R_256_7_0 = 32,
  R_256_7_1 = 32,

  /* Skein_512 round rotation constants */
  R_512_0_0 = 46,
  R_512_0_1 = 36,
  R_512_0_2 = 19,
  R_512_0_3 = 37,
  R_512_1_0 = 33,
  R_512_1_1 = 27,
  R_512_1_2 = 14,
  R_512_1_3 = 42,
  R_512_2_0 = 17,
  R_512_2_1 = 49,
  R_512_2_2 = 36,
  R_512_2_3 = 39,
  R_512_3_0 = 44,
  R_512_3_1 = 9,
  R_512_3_2 = 54,
  R_512_3_3 = 56,
  R_512_4_0 = 39,
  R_512_4_1 = 30,
  R_512_4_2 = 34,
  R_512_4_3 = 24,
  R_512_5_0 = 13,
  R_512_5_1 = 50,
  R_512_5_2 = 10,
  R_512_5_3 = 17,
  R_512_6_0 = 25,
  R_512_6_1 = 29,
  R_512_6_2 = 39,
  R_512_6_3 = 43,
  R_512_7_0 = 8,
  R_512_7_1 = 35,
  R_512_7_2 = 56,
  R_512_7_3 = 22,

  /* Skein1024 round rotation constants */
  R1024_0_0 = 24,
  R1024_0_1 = 13,
  R1024_0_2 = 8,
  R1024_0_3 = 47,
  R1024_0_4 = 8,
  R1024_0_5 = 17,
  R1024_0_6 = 22,
  R1024_0_7 = 37,
  R1024_1_0 = 38,
  R1024_1_1 = 19,
  R1024_1_2 = 10,
  R1024_1_3 = 55,
  R1024_1_4 = 49,
  R1024_1_5 = 18,
  R1024_1_6 = 23,
  R1024_1_7 = 52,
  R1024_2_0 = 33,
  R1024_2_1 = 4,
  R1024_2_2 = 51,
  R1024_2_3 = 13,
  R1024_2_4 = 34,
  R1024_2_5 = 41,
  R1024_2_6 = 59,
  R1024_2_7 = 17,
  R1024_3_0 = 5,
  R1024_3_1 = 20,
  R1024_3_2 = 48,
  R1024_3_3 = 41,
  R1024_3_4 = 47,
  R1024_3_5 = 28,
  R1024_3_6 = 16,
  R1024_3_7 = 25,
  R1024_4_0 = 41,
  R1024_4_1 = 9,
  R1024_4_2 = 37,
  R1024_4_3 = 31,
  R1024_4_4 = 12,
  R1024_4_5 = 47,
  R1024_4_6 = 44,
  R1024_4_7 = 30,
  R1024_5_0 = 16,
  R1024_5_1 = 34,
  R1024_5_2 = 56,
  R1024_5_3 = 51,
  R1024_5_4 = 4,
  R1024_5_5 = 53,
  R1024_5_6 = 42,
  R1024_5_7 = 41,
  R1024_6_0 = 31,
  R1024_6_1 = 44,
  R1024_6_2 = 47,
  R1024_6_3 = 46,
  R1024_6_4 = 19,
  R1024_6_5 = 42,
  R1024_6_6 = 44,
  R1024_6_7 = 25,
  R1024_7_0 = 9,
  R1024_7_1 = 48,
  R1024_7_2 = 35,
  R1024_7_3 = 52,
  R1024_7_4 = 23,
  R1024_7_5 = 31,
  R1024_7_6 = 37,
  R1024_7_7 = 20
}

#if ! SKEIN_ROUNDS
#define SKEIN_256_ROUNDS_TOTAL
#define SKEIN_512_ROUNDS_TOTAL
#define SKEIN1024_ROUNDS_TOTAL
#else
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_256_ROUNDS_TOTAL (8*((((SKEIN_ROUNDS/100) + 5) % 10) + 5))
#define SKEIN_256_ROUNDS_TOTAL
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN_512_ROUNDS_TOTAL (8*((((SKEIN_ROUNDS/ 10) + 5) % 10) + 5))
#define SKEIN_512_ROUNDS_TOTAL
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SKEIN1024_ROUNDS_TOTAL (8*((((SKEIN_ROUNDS ) + 5) % 10) + 5))
#define SKEIN1024_ROUNDS_TOTAL
#endif

public class hashState
{
  public uint statebits; // 256, 512, or 1024
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	Skein_Ctxt_Hdr_t h; // common header "overlay"
//	Skein_256_Ctxt_t ctx_256;
//	Skein_512_Ctxt_t ctx_512;
//	Skein1024_Ctxt_t ctx1024;
//  }
//C++ TO C# CONVERTER NOTE: Access declarations are not available in C#:
//  u;
}