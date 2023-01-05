using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Gui
{
    internal unsafe class IPanel : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr GetNameDelegate(IntPtr Class, uint Panel);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void PaintTraverseHookDelegate(uint Panel, bool ForceRepaint, bool AllowForce);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PaintTraverseOriginalDelegate(IntPtr Class, uint Panel, bool ForceRepaint, bool AllowForce);

        public IPanel(IntPtr Base) : base(Base) { }

        public string GetName(uint Panel) => Marshal.PtrToStringAnsi(CallVirtualFunction<GetNameDelegate>(36)(this, Panel));

        public void PaintTraverseOriginal(uint Panel, bool ForceReapint, bool AllowForce) => CallOriginalFunction<PaintTraverseOriginalDelegate>(41)(this, Panel, ForceReapint, AllowForce);
    }
}