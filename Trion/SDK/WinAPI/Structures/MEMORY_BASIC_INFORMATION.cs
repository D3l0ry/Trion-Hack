using System;
using System.Runtime.InteropServices;

using Trion.SDK.WinAPI.Enums;

namespace Trion.SDK.WinAPI.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public AllocationProtect AllocationProtect;
        public IntPtr RegionSize;
        public uint State;
        public AllocationProtect Protect;
        public uint Type;
    }
}