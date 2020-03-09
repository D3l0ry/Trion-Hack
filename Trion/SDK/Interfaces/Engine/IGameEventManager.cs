using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IGameEventManager : VMTable
    {
        #region Initializations
        public IGameEventManager(void* Base) : base(Base)
        {
        }

        public IGameEventManager(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool FireEventClientSideHookDelegate(ref GameEvent Event);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool FireEventClientSideOriginalDelegate(void* Class, ref GameEvent Event);
        #endregion

        #region Structures
        public struct GameEvent
        {
            #region Delegates
            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate void* GetNameDelegate(void* Class);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate int GetIntDelegate(void* Class, string Key, int Index);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate void* GetStringDelegate(void* Class, string Key, string Index);
            #endregion

            #region Virtual Methods
            public string GetName()
            {
                fixed (void* StructPtr = &this)
                {
                    return Marshal.PtrToStringAnsi((IntPtr)CallVirtualFunction<GetNameDelegate>(StructPtr, 1)(StructPtr));
                }
            }

            public int GetInt(string Key)
            {
                fixed (void* StructPtr = &this)
                {
                    return CallVirtualFunction<GetIntDelegate>(StructPtr, 6)(StructPtr, Key, 0);
                }
            }

            public string GetString(string Key)
            {
                fixed (void* StructPtr = &this)
                {
                    return Marshal.PtrToStringAnsi((IntPtr)CallVirtualFunction<GetStringDelegate>(StructPtr, 9)(StructPtr, Key, ""));
                }
            }

            public void SetString(string Key, string Value)
            {
                fixed (void* StructPtr = &this)
                {
                    CallVirtualFunction<GetStringDelegate>(StructPtr, 16)(StructPtr, Key, Value);
                }
            }
            #endregion
        }
        #endregion

        #region Methods
        public bool FireEventClientSideOriginal(ref GameEvent Event) => CallOriginalFunction<FireEventClientSideOriginalDelegate>(9)(this, ref Event);
        #endregion
    }
}