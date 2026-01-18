using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IGameEventManager : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool FireEventClientSideHookDelegate(ref GameEvent Event);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool FireEventClientSideOriginalDelegate(IntPtr Class, ref GameEvent Event);

        public struct GameEvent
        {
            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate IntPtr GetNameDelegate(void* Class);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate int GetIntDelegate(void* Class, string Key, int Index);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            private delegate IntPtr GetStringDelegate(void* Class, string Key, string Index);

            public string GetName()
            {
                fixed (void* StructPtr = &this)
                {
                    return Marshal.PtrToStringAnsi(CallVirtualFunction<GetNameDelegate>(StructPtr, 1)(StructPtr));
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
                    return Marshal.PtrToStringAnsi(CallVirtualFunction<GetStringDelegate>(StructPtr, 9)(StructPtr, Key, ""));
                }
            }

            public void SetString(string Key, string Value)
            {
                fixed (void* StructPtr = &this)
                {
                    CallVirtualFunction<GetStringDelegate>(StructPtr, 16)(StructPtr, Key, Value);
                }
            }
        }

        public IGameEventManager(IntPtr Base) : base(Base) { }

        public bool FireEventClientSideOriginal(ref GameEvent Event) => CallOriginalFunction<FireEventClientSideOriginalDelegate>(9)(Address, ref Event);
    }
}