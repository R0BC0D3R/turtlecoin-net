using System;
using System.Collections.Generic;

// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.

///////////////////////////////
///////////////////////////////

/*
 *  linenoise.hpp -- Multi-platfrom C++ header-only linenoise library.
 *
 *  All credits and commendations have to go to the authors of the
 *  following excellent libraries.
 *
 *  - linenoise.h and linenose.c (https://github.com/antirez/linenoise)
 *  - ANSI.c (https://github.com/adoxa/ansicon)
 *  - Win32_ANSI.h and Win32_ANSI.c (https://github.com/MSOpenTech/redis)
 *
 * ------------------------------------------------------------------------
 *
 *  Copyright (c) 2015 yhirose
 *  All rights reserved.
 *  
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions are met:
 *  
 *  1. Redistributions of source code must retain the above copyright notice, this
 *     list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright notice,
 *     this list of conditions and the following disclaimer in the documentation
 *     and/or other materials provided with the distribution.
 *  
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 *  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 *  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 *  DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 *  ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 *  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 *  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 *  ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

/* linenoise.h -- guerrilla line editing library against the idea that a
 * line editing lib needs to be 20,000 lines of C code.
 *
 * See linenoise.c for more information.
 *
 * ------------------------------------------------------------------------
 *
 * Copyright (c) 2010, Salvatore Sanfilippo <antirez at gmail dot com>
 * Copyright (c) 2010, Pieter Noordhuis <pcnoordhuis at gmail dot com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *  *  Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *
 *  *  Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

/*
 * ANSI.c - ANSI escape sequence console driver.
 *
 * Copyright (C) 2005-2014 Jason Hood
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the author be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 * 
 * Jason Hood
 * jadoxa@yahoo.com.au
 */

/*
 * Win32_ANSI.h and Win32_ANSI.c
 *
 * Derived from ANSI.c by Jason Hood, from his ansicon project (https://github.com/adoxa/ansicon), with modifications.
 *
 * Copyright (c), Microsoft Open Technologies, Inc.
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *  - Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *  - Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */


#if ! _WIN32
#else
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define STDIN_FILENO (_fileno(stdin))
#define STDIN_FILENO
#define STDOUT_FILENO
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define isatty _isatty
#define isatty
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define write win32_write
#define write
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define read _read
#define read
#endif

namespace linenoise
{

public delegate void CompletionCallback(string UnnamedParameter, List<string> UnnamedParameter2);

#if _WIN32

namespace ansi
{

//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define lenof(array) (sizeof(array)/sizeof(*(array)))
#define lenof

public class GRM
{
	public byte foreground; // ANSI base color (0 to 7; add 30)
	public byte background; // ANSI base color (0 to 7; add 40)
	public byte bold; // console FOREGROUND_INTENSITY bit
	public byte underline; // console BACKGROUND_INTENSITY bit
	public byte rvideo; // swap foreground/bold & background/underline
	public byte concealed; // set foreground/bold to background/underline
	public byte reverse; // swap console foreground & background attributes
}

} // namespace ansi

/* The linenoiseState structure represents the state during line editing.
 * We pass this state to functions implementing specific editing
 * functionalities. */
public class linenoiseState
{
	public int ifd; // Terminal stdin file descriptor.
	public int ofd; // Terminal stdout file descriptor.
	public string buf; // Edited line buffer.
	public int buflen; // Edited line buffer size.
	public string prompt; // Prompt to display.
	public int pos; // Current cursor position.
	public int oldcolpos; // Previous refresh cursor column position.
	public int len; // Current edited line length.
	public int cols; // Number of columns in terminal.
	public int maxrows; // Maximum num of rows used so far (multiline mode)
	public int history_index; // The history index we are currently editing.
}

public enum KEY_ACTION
{
	KEY_NULL = 0, // NULL
	CTRL_A = 1, // Ctrl+a
	CTRL_B = 2, // Ctrl-b
	CTRL_C = 3, // Ctrl-c
	CTRL_D = 4, // Ctrl-d
	CTRL_E = 5, // Ctrl-e
	CTRL_F = 6, // Ctrl-f
	CTRL_H = 8, // Ctrl-h
	TAB = 9, // Tab
	CTRL_K = 11, // Ctrl+k
	CTRL_L = 12, // Ctrl+l
	ENTER = 13, // Enter
	CTRL_N = 14, // Ctrl-n
	CTRL_P = 16, // Ctrl-p
	CTRL_T = 20, // Ctrl-t
	CTRL_U = 21, // Ctrl+u
	CTRL_W = 23, // Ctrl+w
	ESC = 27, // Escape
	BACKSPACE = 127 // Backspace
}

} // namespace linenoise

#if _WIN32
#undef isatty
#undef write
#undef read
#endif