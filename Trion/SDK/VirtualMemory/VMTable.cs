using System;
using System.Runtime.InteropServices;

using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.VMT
{
    internal abstract unsafe class VMTable
    {
        private readonly IntPtr m_ClassBase;
        private readonly IntPtr m_NewVmTable;
        private readonly IntPtr m_OldVmTable;
        private readonly int m_VmTableLength;

        protected VMTable(IntPtr classPtr)
        {
            if (classPtr == IntPtr.Zero)
            {
                throw new NullReferenceException("Class is Null Ptr");
            }

            m_ClassBase = classPtr;
            m_OldVmTable = *(IntPtr*)classPtr;
            m_VmTableLength = VMTLength(m_OldVmTable, 0x4);

            if (m_VmTableLength == 0)
            {
                throw new ArgumentNullException("VMTable Length is zero");
            }

            m_NewVmTable = Marshal.AllocHGlobal((IntPtr)m_VmTableLength);

            Buffer.MemoryCopy(m_OldVmTable.ToPointer(), m_NewVmTable.ToPointer(), m_VmTableLength, m_VmTableLength);

            NativeMethods.VirtualProtect(m_ClassBase, 0x4, ProtectCode.PAGE_EXECUTE_READWRITE, out uint _);

            *(IntPtr*)m_ClassBase = m_NewVmTable;
        }

        public IntPtr Address => m_ClassBase;

        public void Hook<T>(int index, T func) => *(IntPtr*)(m_NewVmTable + (index * 0x4)) = Marshal.GetFunctionPointerForDelegate(func);

        public void UnHook(int index)
        {
            int size = index * 0x4;

            *(IntPtr*)(m_NewVmTable + size) = *(IntPtr*)(m_OldVmTable + size);
        }

        protected T CallOriginalFunction<T>(int index) => Marshal.GetDelegateForFunctionPointer<T>(*(IntPtr*)(m_OldVmTable + (index * 0x4)));

        protected T CallVirtualFunction<T>(int index) => Marshal.GetDelegateForFunctionPointer<T>(*(IntPtr*)(m_NewVmTable + (index * 0x4)));

        public static T CallVirtualFunction<T>(IntPtr classPtr, int index) => Marshal.GetDelegateForFunctionPointer<T>(*(IntPtr*)(*(IntPtr*)classPtr + (index * 0x4)));

        public static T CallVirtualFunction<T>(void* classPtr, int index) => CallVirtualFunction<T>((IntPtr)classPtr, index);

        private int VMTLength(IntPtr table, int size)
        {
            int length = 0;

            while (NativeMethods.VirtualQuery(*(IntPtr*)(table + (length * 0x4)), out MEMORY_BASIC_INFORMATION MEMORY_INFORMATION, Marshal.SizeOf<MEMORY_BASIC_INFORMATION>()) != 0 && MEMORY_INFORMATION.Protect == AllocationProtect.PAGE_EXECUTE_READ)
            {
                length++;
            }

            return length * size;
        }
    }
}