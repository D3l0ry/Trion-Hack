using System;

namespace CMDInjector.Enums
{
    [Flags]
    internal enum MillisecondFlags : uint
    {
        INFINITE = 0xFFFFFFFF,
        WAIT_ABANDONED = 0x00000080,
        WAIT_OBJECT_0 = 0x00000000,
        WAIT_TIMEOUT = 0x00000102
    }
}