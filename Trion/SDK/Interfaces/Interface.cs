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
        public static IntPtr ClientAddress = NativeMethods.GetModuleHandle("client_panorama.dll");
        public static IntPtr EngineAddress = NativeMethods.GetModuleHandle("engine.dll");
        public static IntPtr ValveStdFactory = NativeMethods.GetModuleHandle("vstdlib.dll");
        public static IntPtr ValveGui = NativeMethods.GetModuleHandle("vgui2.dll");
        public static IntPtr ValveGuiMatSurface = NativeMethods.GetModuleHandle("vguimatsurface.dll");
        public static IntPtr DataCache = NativeMethods.GetModuleHandle("datacache.dll");
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr CreateInterfaceFn([MarshalAs(UnmanagedType.LPStr)]string Function, IntPtr Index);
        #endregion

        #region Private Methods
        private static CreateInterfaceFn CreateInterface(IntPtr Library) => Marshal.GetDelegateForFunctionPointer<CreateInterfaceFn>(NativeMethods.GetProcAddress(Library, "CreateInterface"));

        private static IntPtr GetInterface(this IntPtr Library, string Function) => CreateInterface(Library)(Function, IntPtr.Zero);
        #endregion

        #region Interfaces
        public static IGameUI GameUI = new IGameUI(ClientAddress.GetInterface("GameUI011"));
        public static IBaseClientDLL BaseClientDLL = new IBaseClientDLL(ClientAddress.GetInterface("VClient018"));
        public static IClientEntityList ClientEntityList = new IClientEntityList(ClientAddress.GetInterface("VClientEntityList003"));
        public static IClientMode ClientMode = new IClientMode(**(IntPtr**)((*(IntPtr**)BaseClientDLL)[10] + 0x5));

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