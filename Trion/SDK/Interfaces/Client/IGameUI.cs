using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IGameUI : VMTable
    {
        #region Initializations
        public IGameUI(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void MessageBoxDelegate(void* Class, [MarshalAs(UnmanagedType.LPStr)]string Title, [MarshalAs(UnmanagedType.LPStr)]string Text, bool arg1, bool arg2, void* arg3, void* arg4, void* arg5, void* arg6, void* arg7);
        #endregion

        #region Methods
        public void MessageBox(string Title, string Message) => CallVirtualFunction<MessageBoxDelegate>(20)(this, Title, Message, true, false, (void*)0, (void*)0, (void*)0, (void*)0, (void*)0);
        #endregion
    }
}