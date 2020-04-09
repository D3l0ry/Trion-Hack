using System;
using System.Runtime.InteropServices;

using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IClientEntityList : VMTable
    {
        #region Initialization
        public IClientEntityList(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IClientEntity* GetClientEntityDelegate(void* Class, int Index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IClientEntity* GetClientEntityFromHandleDelegate(void* Class, void* Handle);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetHighestEntityIndexDelegate(void* Class);
        #endregion

        #region Methods
        public IClientEntity* GetClientEntity(int Index) => CallVirtualFunction<GetClientEntityDelegate>(3)(this, Index);

        public IClientEntity* GetClientEntityFromHandle(void* Handle) => CallVirtualFunction<GetClientEntityFromHandleDelegate>(4)(this, Handle);

        public int GetHighestEntityIndex => CallVirtualFunction<GetHighestEntityIndexDelegate>(6)(this);
        #endregion
    }
}