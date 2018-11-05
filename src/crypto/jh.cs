/*This program gives the 64-bit optimized bitslice implementation of JH using ANSI C

   --------------------------------
   Performance

   Microprocessor: Intel CORE 2 processor (Core 2 Duo Mobile T6600 2.2GHz)
   Operating System: 64-bit Ubuntu 10.04 (Linux kernel 2.6.32-22-generic)
   Speed for long message:
   1) 45.8 cycles/byte   compiler: Intel C++ Compiler 11.1   compilation option: icc -O2
   2) 56.8 cycles/byte   compiler: gcc 4.4.3                 compilation option: gcc -O3

   --------------------------------
   Last Modified: January 16, 2011
*/

/*This program gives the 64-bit optimized bitslice implementation of JH using ANSI C

   --------------------------------
   Performance

   Microprocessor: Intel CORE 2 processor (Core 2 Duo Mobile T6600 2.2GHz)
   Operating System: 64-bit Ubuntu 10.04 (Linux kernel 2.6.32-22-generic)
   Speed for long message:
   1) 45.8 cycles/byte   compiler: Intel C++ Compiler 11.1   compilation option: icc -O2
   2) 56.8 cycles/byte   compiler: gcc 4.4.3                 compilation option: gcc -O3

   --------------------------------
   Last Modified: January 16, 2011
*/

public enum HashReturn
{
	SUCCESS = 0,
	FAIL = 1,
	BAD_HASHLEN = 2
}


public class hashState
{
	public int hashbitlen; //the message digest size
	public ulong databitlen; //the message size in bits
	public ulong datasize_in_buffer; //the size of the message remained in buffer; assumed to be multiple of 8bits except for the last partial block at the end of the message
//C++ TO C# CONVERTER TODO TASK: The #define macro 'DATA_ALIGN16' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	DATA_ALIGN16(uint64_t x[8][2]); //the 1024-bit state, ( x[i][0] || x[i][1] ) is the ith row of the state in the pseudocode
	public byte[] buffer = new byte[64]; //the 512-bit message block to be hashed;
}