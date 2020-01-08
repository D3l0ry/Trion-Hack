using System;
using System.Runtime.InteropServices;
using System.Text;

using Trion.SDK.Dumpers;
using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;
using Trion.SDK.WinAPI.Variables;

namespace Trion.SDK.VMT
{
    internal unsafe class VMTable
    {
        #region Variable
        protected HANDLE Class_Base;

        private uint* New_VMTable;
        private uint* Old_VMTable;

        private uint VMTable_Length;
        #endregion

        #region Operator
        public static implicit operator VMTable(void* Value) => new VMTable(Value);

        public static implicit operator VMTable(IntPtr Value) => new VMTable(Value);

        public static explicit operator IntPtr(VMTable Value) => Value.Class_Base;

        public static implicit operator void*(VMTable Value) => Value.Class_Base;

        public static implicit operator HANDLE(VMTable Value) => Value.Class_Base;
        #endregion

        #region Initializations
        protected VMTable() { }

        protected VMTable(void* Base, uint Module = 0)
        {
            if ((IntPtr)Base != IntPtr.Zero)
            {
                Class_Base = Base;
            }

            if (Class_Base == IntPtr.Zero)
            {
                throw new ArgumentNullException("Class is NullPtr");
            }

            Old_VMTable = *(uint**)Class_Base;

            VMTable_Length = VMTLength(Old_VMTable);

            if (VMTable_Length == 0)
            {
                throw new ArgumentNullException("VMTable Length is Null");
            }

            if (Module == 0)
            {
                New_VMTable = (uint*)Marshal.AllocHGlobal((IntPtr)((VMTable_Length + 1) * sizeof(uint)));
            }
            else
            {
                New_VMTable = GetFreeMemory(Module, (VMTable_Length + 1) * sizeof(uint));
            }

            uint Length = VMTable_Length * sizeof(uint);

            Buffer.MemoryCopy(Old_VMTable, New_VMTable, Length, Length);

            try
            {
                Protect(Class_Base, sizeof(uint));

                *(uint**)Class_Base = New_VMTable;
            }
            catch
            {
                New_VMTable = (uint*)0;

                throw new AggregateException("Error create VMTable");
            }
        }

        protected VMTable(IntPtr Base, uint Module = 0) : this(Base.ToPointer(), Module) { }
        #endregion

        #region Hook Methods
        public void Hook<T>(int Index, T Funct) => New_VMTable[Index] = (uint)Marshal.GetFunctionPointerForDelegate(Funct);

        public void UnHook(int Index) => New_VMTable[Index] = Old_VMTable[Index];
        #endregion

        #region Call Function Methods
        protected T CallOriginalFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>((HANDLE)Old_VMTable[Index]);

        protected T CallVirtualFunction<T>(int Index) => Marshal.GetDelegateForFunctionPointer<T>(Class_Base[Index]);

        public static T CallVirtualFunction<T>(HANDLE Class, int Index) => Marshal.GetDelegateForFunctionPointer<T>(Class[Index]);
        #endregion

        #region Helper
        private uint VMTLength(uint* Table)
        {
            uint Length = 0U;

            while (NativeMethods.VirtualQueryEx(NativeMethods.GetCurrentProcess(), (IntPtr)Table[Length], out MEMORY_BASIC_INFORMATION MEMORY_INFORMATION, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != 0 && MEMORY_INFORMATION.Protect == AllocationProtect.PAGE_EXECUTE_READ)
            {
                Length++;
            }

            return Length;
        }

        private uint* GetFreeMemory(uint Module, uint VMTSize)
        {
            StringBuilder SearcherPattern = new StringBuilder();

            for (uint Size = 0; Size < VMTSize; Size++)
            {
                SearcherPattern.Append("00 ");
            }

            return (uint*)(byte**)Pattern.FindPattern(Module, SearcherPattern.ToString().TrimEnd());
        }

        private bool Protect(void* Base, uint Size, ProtectCode ProtectCode = ProtectCode.PAGE_EXECUTE_READWRITE) => NativeMethods.VirtualProtect((HANDLE)Base, (HANDLE)Size, ProtectCode, out uint Old_Protect);
        #endregion
    }
}