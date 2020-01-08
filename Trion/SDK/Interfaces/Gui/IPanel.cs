using System;
using System.Runtime.InteropServices;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Gui
{
    internal unsafe class IPanel : VMTable
    {
        #region Initializations
        public IPanel(void* Base) : base(Base)
        {
        }

        public IPanel(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetNameDelegate(void* Class, uint Panel);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void PaintTraverseHookDelegate(uint Panel, bool ForceRepaint, bool AllowForce);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PaintTraverseOriginalDelegate(void* Class, uint Panel, bool ForceRepaint, bool AllowForce);
        #endregion

        #region Virtual Methods
        public string GetName(uint Panel) => Marshal.PtrToStringAnsi((IntPtr)CallVirtualFunction<GetNameDelegate>(36)(this, Panel));

        public void PaintTraverseOriginal(uint Panel, bool ForceReapint, bool AllowForce) => CallOriginalFunction<PaintTraverseOriginalDelegate>(41)(this, Panel, ForceReapint, AllowForce);
        #endregion
    }
}