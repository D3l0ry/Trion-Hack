using System;
using System.Runtime.InteropServices;

using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IClientEntityList : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate ref IClientEntity GetClientEntityDelegate(IntPtr Class, int Index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate ref IClientEntity GetClientEntityFromHandleDelegate(IntPtr Class, IntPtr Handle);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetHighestEntityIndexDelegate(IntPtr Class);

        public IClientEntityList(IntPtr Base) : base(Base) { }

        public ref IClientEntity GetClientEntity(int Index) => ref CallVirtualFunction<GetClientEntityDelegate>(3)(this, Index);

        public ref IClientEntity GetClientEntityFromHandle(IntPtr Handle) => ref CallVirtualFunction<GetClientEntityFromHandleDelegate>(4)(this, Handle);

        public int GetHighestEntityIndex => CallVirtualFunction<GetHighestEntityIndexDelegate>(6)(this);
    }
}