using System;
using System.Runtime.InteropServices;

using Trion.SDK.Enums;
using Trion.SDK.Structures;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IBaseClientDLL : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate ClientClass* GetAllClassesDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void FrameStageNotifyDelegate(IntPtr Class, FrameStage FrameStage);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void FrameStageNotifyHookDelegate(FrameStage FrameStage);

        public IBaseClientDLL(IntPtr Base) : base(Base) { }

        public ClientClass* GetAllClasses() => CallVirtualFunction<GetAllClassesDelegate>(8)(Address);

        public void FrameStageNotifyOriginal(FrameStage FrameStage) => CallOriginalFunction<FrameStageNotifyDelegate>(37)(Address, FrameStage);
    }
}