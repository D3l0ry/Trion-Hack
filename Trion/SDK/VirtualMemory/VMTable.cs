using System;
using System.Runtime.InteropServices;

using Trion.SDK.VirtualMemory;
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

        public static implicit operator IntPtr(VMTable value) => value.m_ClassBase;

        public static implicit operator void*(VMTable value) => value.m_ClassBase.ToPointer();

        protected VMTable(IntPtr classPtr)
        {
            if (classPtr != IntPtr.Zero)
            {
                m_ClassBase = classPtr;
            }

            if (m_ClassBase == IntPtr.Zero)
            {
                throw new NullReferenceException("Class is NullPtr");
            }

            m_OldVmTable = m_ClassBase.Read<IntPtr>();

            m_VmTableLength = VMTLength(m_OldVmTable, 0x4);

            if (m_VmTableLength == 0)
            {
                throw new ArgumentNullException("VMTable Length is zero");
            }

            m_NewVmTable = Marshal.AllocHGlobal((IntPtr)m_VmTableLength);

            Buffer.MemoryCopy(m_OldVmTable.ToPointer(), m_NewVmTable.ToPointer(), m_VmTableLength, m_VmTableLength);

            Protect(m_ClassBase, 0x4);

            m_ClassBase.Write(m_NewVmTable);
        }

        public void Hook<T>(int index, T funct) => m_NewVmTable.Write(index * 0x4, Marshal.GetFunctionPointerForDelegate(funct));

        public void UnHook(int index)
        {
            int size = index * 0x4;

            m_NewVmTable.Write(size, Marshal.ReadIntPtr(m_OldVmTable, size));
        }

        protected T CallOriginalFunction<T>(int index) => Marshal.GetDelegateForFunctionPointer<T>((m_OldVmTable + (index * 0x4)).Read<IntPtr>());

        protected T CallVirtualFunction<T>(int index) => Marshal.GetDelegateForFunctionPointer<T>((m_NewVmTable + (index * 0x4)).Read<IntPtr>());

        public static T CallVirtualFunction<T>(IntPtr classPtr, int index) => Marshal.GetDelegateForFunctionPointer<T>((classPtr.Read<IntPtr>() + (index * 0x4)).Read<IntPtr>());

        public static T CallVirtualFunction<T>(void* classPtr, int index) => CallVirtualFunction<T>((IntPtr)classPtr, index);

        private int VMTLength(IntPtr table, int size)
        {
            int length = 0;

            while (NativeMethods.VirtualQuery((table + (length * 0x4)).Read<IntPtr>(), out MEMORY_BASIC_INFORMATION MEMORY_INFORMATION, Marshal.SizeOf<MEMORY_BASIC_INFORMATION>()) != 0 && MEMORY_INFORMATION.Protect == AllocationProtect.PAGE_EXECUTE_READ)
            {
                length++;
            }

            return length * size;
        }

        private bool Protect(IntPtr address, uint Size, ProtectCode ProtectCode = ProtectCode.PAGE_EXECUTE_READWRITE) => NativeMethods.VirtualProtect(address, Size, ProtectCode, out uint Old_Protect);
    }
}