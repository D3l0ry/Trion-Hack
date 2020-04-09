using System;
using System.Runtime.InteropServices;

using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.VMT
{
    internal abstract unsafe class VMTable
    {
        #region Variable
        private IntPtr Class_Base;

        private IntPtr New_VMTable;
        private IntPtr Old_VMTable;

        private int VMTable_Length;
        #endregion

        #region Operator
        public static implicit operator IntPtr(VMTable Value) => Value.Class_Base;

        public static implicit operator void*(VMTable Value) => Value.Class_Base.ToPointer();
        #endregion

        #region Initializations
        protected VMTable(IntPtr Base)
        {
            if (Base != IntPtr.Zero)
            {
                Class_Base = Base;
            }

            if (Class_Base == IntPtr.Zero)
            {
                throw new NullReferenceException("Class is NullPtr");
            }

            Old_VMTable = Marshal.ReadIntPtr(Class_Base);

            VMTable_Length = VMTLength(Old_VMTable, 0x4);

            if (VMTable_Length == 0)
            {
                throw new ArgumentNullException("VMTable Length is zero");
            }

            New_VMTable = Marshal.AllocHGlobal((IntPtr)VMTable_Length);

            Buffer.MemoryCopy(Old_VMTable.ToPointer(), New_VMTable.ToPointer(), VMTable_Length, VMTable_Length);

            try
            {
                Protect(Class_Base, 0x4);

                Marshal.WriteIntPtr(Class_Base, New_VMTable);
            }
            catch
            {
                New_VMTable = IntPtr.Zero;

                throw new AggregateException("Error create VMTable");
            }
        }
        #endregion

        #region Hook Methods
        public void Hook<T>(int Index, T Funct) => Marshal.WriteIntPtr(New_VMTable, Index * 0x4, Marshal.GetFunctionPointerForDelegate(Funct));

        public void UnHook(int Index)
        {
            int size = Index * 0x4;

            Marshal.WriteIntPtr(New_VMTable, size, Marshal.ReadIntPtr(Old_VMTable,size));
        }
        #endregion

        #region Call Function Methods
        protected T CallOriginalFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>(Marshal.ReadIntPtr(Old_VMTable + (Index * 0x4)));

        protected T CallVirtualFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>(Marshal.ReadIntPtr(New_VMTable + (Index * 0x4)));

        public static T CallVirtualFunction<T>(IntPtr Class, int Index) => Marshal.GetDelegateForFunctionPointer<T>(Marshal.ReadIntPtr(Marshal.ReadIntPtr(Class) + (Index * 0x4)));

        public static T CallVirtualFunction<T>(void* Class, int Index) => CallVirtualFunction<T>((IntPtr)Class, Index);
        #endregion

        #region Helper
        private int VMTLength(IntPtr table, int size)
        {
            int length = 0;

            while (NativeMethods.VirtualQuery(Marshal.ReadIntPtr(table + (length * 0x4)), out MEMORY_BASIC_INFORMATION MEMORY_INFORMATION, Marshal.SizeOf<MEMORY_BASIC_INFORMATION>()) != 0 && MEMORY_INFORMATION.Protect == AllocationProtect.PAGE_EXECUTE_READ)
            {
                length++;
            }

            return length * size;
        }

        private bool Protect(IntPtr Base, uint Size, ProtectCode ProtectCode = ProtectCode.PAGE_EXECUTE_READWRITE) => NativeMethods.VirtualProtect(Base, Size, ProtectCode, out uint Old_Protect);
        #endregion
    }
}