using System;
using System.Runtime.InteropServices;

using Trion.SDK.Enums;

namespace Trion.SDK.Structures
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe struct DVariant
    {
        [FieldOffset(0x0)]
        public float m_Float;
        [FieldOffset(0x0)]
        public int m_Int;
        [FieldOffset(0x0)]
        public char* m_pString;
        [FieldOffset(0x0)]
        public void* m_pData;
        [FieldOffset(0x0)]
        public fixed float Vector[3];
        [FieldOffset(0x0)]
        readonly Int64 m_Int64;
        [FieldOffset(0x8)]
        readonly SendPropType m_Type;
    }
}