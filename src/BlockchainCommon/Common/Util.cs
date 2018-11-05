// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


#if WIN32
#else
#endif



[System.Runtime.InteropServices.DllImport("kernel32.dll")]
	internal static extern void GetNativeSystemInfo(LPSYSTEM_INFO UnnamedParameter);
	[System.Runtime.InteropServices.DllImport("kernel32.dll")]
	internal static extern int GetProductInfo(uint UnnamedParameter, uint UnnamedParameter2, uint UnnamedParameter3, uint UnnamedParameter4, ref uint UnnamedParameter5);
