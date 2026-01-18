using System;
using System.Runtime.InteropServices;

using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.WinAPI;

namespace Trion.SDK.Interfaces
{
    internal static unsafe class Interface
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr CreateInterfaceFn([MarshalAs(UnmanagedType.LPStr)] string Function, IntPtr Index);

        public static IntPtr ClientAddress = NativeMethods.GetModuleHandle("client.dll");
        public static IntPtr EngineAddress = NativeMethods.GetModuleHandle("engine.dll");

        public static IBaseClientDLL BaseClientDLL = new IBaseClientDLL(ClientAddress.GetInterface("VClient018"));
        public static IClientEntityList ClientEntityList = new IClientEntityList(ClientAddress.GetInterface("VClientEntityList003"));
        public static IVEngineClient VEngineClient = new IVEngineClient(EngineAddress.GetInterface("VEngineClient014"));
        public static IVModelInfoClient ModelInfoClient = new IVModelInfoClient(EngineAddress.GetInterface("VModelInfoClient004"));
        public static IGameEventManager GameEventManager = new IGameEventManager(EngineAddress.GetInterface("GAMEEVENTSMANAGER002"));
        public static NetVar NetVar = new NetVar();

        private static CreateInterfaceFn CreateInterface(IntPtr hLibrary) => Marshal.GetDelegateForFunctionPointer<CreateInterfaceFn>(NativeMethods.GetProcAddress(hLibrary, "CreateInterface"));

        private static IntPtr GetInterface(this IntPtr hLibrary, string Function) => CreateInterface(hLibrary)(Function, IntPtr.Zero);
    }
}