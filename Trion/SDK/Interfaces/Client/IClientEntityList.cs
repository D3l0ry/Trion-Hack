using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;
using Trion.SDK.WinAPI.Variables;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IClientEntityList : VMTable
    {
        #region Initialization
        public IClientEntityList(void* Base) : base(Base)
        {
        }

        public IClientEntityList(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetClientEntityDelegate(void* Class, int Index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetClientEntityFromHandleDelegate(void* Class, uint Handle);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetHighestEntityIndexDelegate(void* Class);
        #endregion

        #region Methods
        public IClientEntity* GetClientEntity(int Index) => (IClientEntity*)CallVirtualFunction<GetClientEntityDelegate>(3)(this, Index);

        public IClientEntity* GetClientEntityFromHandle(uint Handle) => (IClientEntity*)CallVirtualFunction<GetClientEntityFromHandleDelegate>(4)(this, Handle);

        public void* GetClientEntityAddress(int Index) => CallVirtualFunction<GetClientEntityDelegate>(3)(this, Index);

        public void* GetClientEntityFromHandleAddress(uint Handle) => CallVirtualFunction<GetClientEntityFromHandleDelegate>(4)(this, Handle);

        public int GetHighestEntityIndex => CallVirtualFunction<GetHighestEntityIndexDelegate>(6)(this);
        #endregion
    }
}