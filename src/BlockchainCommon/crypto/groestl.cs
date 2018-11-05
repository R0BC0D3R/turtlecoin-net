/* hash.c     April 2012
 * Groestl ANSI C code optimised for 32-bit machines
 * Author: Thomas Krinninger
 *
 *  This work is based on the implementation of
 *          Soeren S. Thomsen and Krystian Matusiewicz
 *          
 *
 */

/*
#include "crypto_uint8.h"
#include "crypto_uint32.h"
#include "crypto_uint64.h"
#include "crypto_hash.h" 

typedef crypto_uint8 ushort; 
typedef crypto_uint32 uint; 
typedef crypto_uint64 ulong;
*/

/* some sizes (number of bytes) */
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define LENGTHFIELDLEN ROWS

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SIZE512 (ROWS*COLS512)


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ROTL32(v, n) ((((v)<<(n))|((v)>>(32-(n))))&li_32(ffffffff))


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define li_32(h) 0x##h##u
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define EXT_BYTE(var,n) ((ushort)((uint)(var) >> (8*n)))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define u32BIG(a) ((ROTL32(a,8) & li_32(00FF00FF)) | (ROTL32(a,24) & li_32(FF00FF00)))


/* NIST API begin */
public class hashState
{
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  uint chaining[(DefineConstants.ROWS * DefineConstants.COLS512) / sizeof(uint)]; // actual state
  public uint block_counter1 = new uint(); // message block counter(s)
  public uint block_counter2 = new uint();
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  byte buffer[(DefineConstants.ROWS * DefineConstants.COLS512)]; // data buffer
  public int buf_ptr; // data buffer pointer
  public int bits_in_last_byte; /* no. of message bits in last byte of
			       data buffer */
}