using System;
using System.Runtime.InteropServices;
using System.Security;

using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.WinAPI
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, ProtectCode flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern int VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);
    }
}