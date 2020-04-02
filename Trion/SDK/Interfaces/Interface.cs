using System;
using System.Runtime.InteropServices;

using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Interfaces.Gui;
using Trion.SDK.WinAPI;

namespace Trion.SDK.Interfaces
{
    internal unsafe static class Interface
    {
        #region Libraries
        public static IntPtr ClientAddress = GetLibraryAddress("client_panorama.dll");
        public static IntPtr EngineAddress = GetLibraryAddress("engine.dll");
        public static IntPtr ValveStdFactory = GetLibraryAddress("vstdlib.dll");
        public static IntPtr ValveGui = GetLibraryAddress("vgui2.dll");
        public static IntPtr ValveGuiMatSurface = GetLibraryAddress("vguimatsurface.dll");
        public static IntPtr DataCache = GetLibraryAddress("datacache.dll");
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* CreateInterfaceFn([MarshalAs(UnmanagedType.LPStr)]string Function, IntPtr Index);
        #endregion

        #region Private Methods
        private static IntPtr GetLibraryAddress(string Library) => NativeMethods.GetModuleHandle(Library);

        private static CreateInterfaceFn GetInterface(string Library) => Marshal.GetDelegateForFunctionPointer<CreateInterfaceFn>(NativeMethods.GetProcAddress(GetLibraryAddress(Library), "CreateInterface"));

        private static CreateInterfaceFn GetInterface(IntPtr Library) => Marshal.GetDelegateForFunctionPointer<CreateInterfaceFn>(NativeMethods.GetProcAddress(Library, "CreateInterface"));

        private static void* GetInterface(string Library, string Function) => GetInterface(Library)(Function, IntPtr.Zero);

        private static void* GetInterface(this IntPtr Library, string Function) => GetInterface(Library)(Function, IntPtr.Zero);
        #endregion

        #region Interfaces
        public static IGameUI GameUI = new IGameUI(ClientAddress.GetInterface("GameUI011"));
        public static IBaseClientDLL BaseClientDLL = new IBaseClientDLL(ClientAddress.GetInterface("VClient018"));
        public static IClientEntityList ClientEntityList = new IClientEntityList(ClientAddress.GetInterface("VClientEntityList003"));
        public static IClientMode ClientMode = new IClientMode(**(void***)((*(uint**)BaseClientDLL)[10] + 0x5));

        public static IVEngineClient VEngineClient = new IVEngineClient(EngineAddress.GetInterface("VEngineClient014"));
        public static IVModelInfoClient ModelInfoClient = new IVModelInfoClient(EngineAddress.GetInterface("VModelInfoClient004"));
        public static IGameEventManager GameEventManager = new IGameEventManager(EngineAddress.GetInterface("GAMEEVENTSMANAGER002"));

        public static IPanel Panel = new IPanel(ValveGui.GetInterface("VGUI_Panel009"));
        public static ISurface Surface = new ISurface(ValveGuiMatSurface.GetInterface("VGUI_Surface031"));
        #endregion

        #region Dumpers
        public static NetVar NetVar = new NetVar();
        #endregion
    }
}