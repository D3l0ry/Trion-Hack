using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IVModelInfoClient : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr GetModelDelegate(IntPtr Class, int Index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetModelIndexDelegate(IntPtr Class, [MarshalAs(UnmanagedType.LPStr)] string Name);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr GetModelNameDelegate(IntPtr Class, IntPtr Model);

        public IVModelInfoClient(IntPtr Base) : base(Base) { }

        public IntPtr GetModel(int Index) => CallVirtualFunction<GetModelDelegate>(1)(this, Index);

        public int GetModelIndex(string Name) => CallVirtualFunction<GetModelIndexDelegate>(2)(this, Name);

        public string GetModelName(IntPtr Model) => Marshal.PtrToStringAnsi(CallVirtualFunction<GetModelNameDelegate>(3)(this, Model));
    }
}