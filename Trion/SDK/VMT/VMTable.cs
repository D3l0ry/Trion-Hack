using System;
using System.Runtime.InteropServices;

using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.VMT
{
    internal unsafe class VMTable
    {
        #region Variable
        private uint** Class_Base;

        private uint* New_VMTable;
        private uint* Old_VMTable;

        private uint VMTable_Length;
        #endregion

        #region Operator
        public static implicit operator VMTable(void* Value) => new VMTable(Value);

        public static implicit operator VMTable(IntPtr Value) => new VMTable(Value);

        public static explicit operator IntPtr(VMTable Value) => (IntPtr)Value.Class_Base;

        public static implicit operator void*(VMTable Value) => Value.Class_Base;
        #endregion

        #region Initializations
        protected VMTable(void* Base)
        {
            if (Base != IntPtr.Zero.ToPointer())
            {
                Class_Base = (uint**)Base;
            }

            if (Class_Base == IntPtr.Zero.ToPointer())
            {
                throw new NullReferenceException("Class is NullPtr");
            }

            Old_VMTable = *Class_Base;

            VMTable_Length = VMTLength(Old_VMTable, sizeof(uint));

            if (VMTable_Length == 0)
            {
                throw new ArgumentNullException("VMTable Length is Null");
            }

            New_VMTable = (uint*)Marshal.AllocHGlobal((IntPtr)VMTable_Length);

            Buffer.MemoryCopy(Old_VMTable, New_VMTable, VMTable_Length, VMTable_Length);

            try
            {
                Protect(Class_Base, sizeof(uint));

                *Class_Base = New_VMTable;
            }
            catch
            {
                New_VMTable = (uint*)0;

                throw new AggregateException("Error create VMTable");
            }
        }

        protected VMTable(IntPtr Base) : this(Base.ToPointer()) { }
        #endregion

        #region Hook Methods
        public void Hook<T>(int Index, T Funct) => New_VMTable[Index] = (uint)Marshal.GetFunctionPointerForDelegate(Funct);

        public void UnHook(int Index) => New_VMTable[Index] = Old_VMTable[Index];
        #endregion

        #region Call Function Methods
        protected T CallOriginalFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>((IntPtr)Old_VMTable[Index]);

        protected T CallVirtualFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>((IntPtr)(*Class_Base)[Index]);

        public static T CallVirtualFunction<T>(void* Class, int Index) => Marshal.GetDelegateForFunctionPointer<T>((IntPtr)(*(uint**)Class)[Index]);
        #endregion

        #region Helper
        private uint VMTLength(uint* table, uint size)
        {
            uint length = 0U;

            while (NativeMethods.VirtualQuery((IntPtr)table[length], out MEMORY_BASIC_INFORMATION MEMORY_INFORMATION, Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != 0 && MEMORY_INFORMATION.Protect == AllocationProtect.PAGE_EXECUTE_READ)
            {
                length++;
            }

            return length * size;
        }

        private bool Protect(void* Base, uint Size, ProtectCode ProtectCode = ProtectCode.PAGE_EXECUTE_READWRITE) => NativeMethods.VirtualProtect((IntPtr)Base, Size, ProtectCode, out uint Old_Protect);
        #endregion
    }
}