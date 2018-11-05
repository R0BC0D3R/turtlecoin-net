/* 
 * ---------------------------------------------------------------------------
 * OpenAES License
 * ---------------------------------------------------------------------------
 * Copyright (c) 2012, Nabil S. Al Ramli, www.nalramli.com
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 *   - Redistributions of source code must retain the above copyright notice,
 *     this list of conditions and the following disclaimer.
 *   - Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 * ---------------------------------------------------------------------------
 */

// OS X, FreeBSD, and OpenBSD don't need malloc.h
#if !__APPLE__ && !__FreeBSD__ && !__OpenBSD__ && !__DragonFly__
#endif

// ANDROID, FreeBSD, and OpenBSD also don't need timeb.h
#if !__FreeBSD__ && !__OpenBSD__ && !__ANDROID__
#else
#endif

#if _WIN32
#else
#endif

/* 
 * ---------------------------------------------------------------------------
 * OpenAES License
 * ---------------------------------------------------------------------------
 * Copyright (c) 2012, Nabil S. Al Ramli, www.nalramli.com
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 *   - Redistributions of source code must retain the above copyright notice,
 *     this list of conditions and the following disclaimer.
 *   - Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 * ---------------------------------------------------------------------------
 */



#if __cplusplus
#endif

#if _WIN32
# ifdef OAES_SHARED
# ifdef oaes_lib_EXPORTS
# define OAES_API __declspec(dllexport)
# else
# define OAES_API __declspec(dllimport)
# endif
# else
# define OAES_API
# endif
#else
# define OAES_API
#endif



public enum OAES_RET
{
	OAES_RET_FIRST = 0,
	OAES_RET_SUCCESS = 0,
	OAES_RET_UNKNOWN,
	OAES_RET_ARG1,
	OAES_RET_ARG2,
	OAES_RET_ARG3,
	OAES_RET_ARG4,
	OAES_RET_ARG5,
	OAES_RET_NOKEY,
	OAES_RET_MEM,
	OAES_RET_BUF,
	OAES_RET_HEADER,
	OAES_RET_COUNT
}

/*
 * oaes_set_option() takes one of these values for its [option] parameter
 * some options accept either an optional or a required [value] parameter
 */
// no option
// enable ECB mode, disable CBC mode
// enable CBC mode, disable ECB mode
// value is optional, may pass ushort iv[OAES_BLOCK_SIZE] to specify
// the value of the initialization vector, iv

#if OAES_DEBUG
public delegate int oaes_step_cb(ushort[] state, string step_name, int step_count, object user_data);
// enable state stepping mode
// value is required, must pass oaes_step_cb to receive the state at each step
#define OAES_OPTION_STEP_ON
// disable state stepping mode
#define OAES_OPTION_STEP_OFF
#endif


public class _oaes_key
{
  public size_t data_len = new size_t();
  public ushort[] data;
  public size_t exp_data_len = new size_t();
  public ushort[] exp_data;
  public size_t num_keys = new size_t();
  public size_t key_base = new size_t();
}

public class _oaes_ctx
{
#if OAES_HAVE_ISAAC
  public randctx rctx;
#endif

#if OAES_DEBUG
  public oaes_step_cb step_cb;
#endif

  public _oaes_key[] key;
  public ushort options = new ushort();
  public ushort[] iv = Arrays.InitializeWithDefaultInstances<ushort>(DefineConstants.OAES_BLOCK_SIZE);
}