using System;
using System.Runtime.InteropServices;

namespace Trion.SDK.WinAPI.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MODULEINFO
    {
        public IntPtr lpBaseOfDll;
        public long SizeOfImage;
        public IntPtr EntryPoint;
    }
}