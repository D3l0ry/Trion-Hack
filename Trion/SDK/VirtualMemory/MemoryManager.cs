using System;

namespace Trion.SDK.VirtualMemory
{
    internal static unsafe class MemoryManager
    {
        public static ref T Read<T>(this IntPtr address) where T : unmanaged => ref *(T*)address;

        public static ref T Read<T>(this IntPtr address, int offset) where T : unmanaged => ref Read<T>(address + offset);

        public static ref T Read<T>(void* address) where T : unmanaged => ref *(T*)address;

        public static ref T Read<T>(void* address, int offset) where T : unmanaged => ref Read<T>((byte*)address + offset);

        public static void Write<T>(this IntPtr address, T value) where T : unmanaged => *(T*)address = value;

        public static void Write<T>(void* address, T value) where T : unmanaged => *(T*)address = value;

        public static void Write<T>(this IntPtr address, ref T value) where T : unmanaged => *(T*)address = value;

        public static void Write<T>(this IntPtr address, int offset, T value) where T : unmanaged => *(T*)(address + offset) = value;

        public static void Write<T>(void* address, int offset, T value) where T : unmanaged => *(T*)((byte*)address + offset) = value;

        public static void Write<T>(this IntPtr address, int offset, ref T value) where T : unmanaged => *(T*)(address + offset) = value;
    }
}