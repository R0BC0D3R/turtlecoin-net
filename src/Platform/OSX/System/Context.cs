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


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define setcontext(u) setmcontext(&(u)->uc_mcontext)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define getcontext(u) getmcontext(&(u)->uc_mcontext)

#if __cplusplus
#endif

public class mcontext
{
  /*
   * The first 20 fields must match the definition of
   * sigcontext. So that we can support sigcontext
   * and ucontext_t at the same time.
   */
  public int mc_onstack; // XXX - sigcontext compat.
  public int mc_rdi; // machine state (struct trapframe)
  public int mc_rsi;
  public int mc_rdx;
  public int mc_rcx;
  public int mc_r8;
  public int mc_r9;
  public int mc_rax;
  public int mc_rbx;
  public int mc_rbp;
  public int mc_r10;
  public int mc_r11;
  public int mc_r12;
  public int mc_r13;
  public int mc_r14;
  public int mc_r15;
  public int mc_trapno;
  public int mc_addr;
  public int mc_flags;
  public int mc_err;
  public int mc_rip;
  public int mc_cs;
  public int mc_rflags;
  public int mc_rsp;
  public int mc_ss;

  public int mc_len; // sizeof(mcontext_t)
	public const int _MC_FPFMT_NODEV = 0x10000; // device not present or configured
	public const int _MC_FPFMT_XMM = 0x10002;
  public int mc_fpformat;
	public const int _MC_FPOWNED_NONE = 0x20000; // FP state not used
	public const int _MC_FPOWNED_FPU = 0x20001; // FP state came from FPU
	public const int _MC_FPOWNED_PCB = 0x20002; // FP state came from PCB
  public int mc_ownedfp;
  /*
   * See <machine/fpu.h> for the internals of mc_fpstate[].
   */
  public int[] mc_fpstate = new int[64];
  public int[] mc_spare = new int[8];
}

public class ucontext
{
  /*
   * Keep the order of the first two fields. Also,
   * keep them the first two fields in the structure.
   * This way we can have a union with struct
   * sigcontext and ucontext_t. This allows us to
   * support them both at the same time.
   * note: the union is not defined, though.
   */
  public sigset_t uc_sigmask = new sigset_t();
  public mcontext uc_mcontext = new mcontext();

  public __ucontext uc_link;
  public stack_t uc_stack = new stack_t();
  public int[] __spare__ = new int[8];
}

#if __cplusplus
#endif