using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IVModelInfoClient : VMTable
    {
        #region Initialization
        public IVModelInfoClient(void* Base) : base(Base)
        {
        }

        public IVModelInfoClient(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetModelDelegate(void* Class, int Index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetModelIndexDelegate(void* Class, [MarshalAs(UnmanagedType.LPStr)]string Name);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetModelNameDelegate(void* Class, void* Model);
        #endregion

        #region Virtual Methods
        public void* GetModel(int Index) => CallVirtualFunction<GetModelDelegate>(1)(this, Index);

        public int GetModelIndex(string Name) => CallVirtualFunction<GetModelIndexDelegate>(2)(this, Name);

        public string GetModelName(void* Model) => Marshal.PtrToStringAnsi((IntPtr)CallVirtualFunction<GetModelNameDelegate>(3)(this, Model));
        #endregion
    }
}