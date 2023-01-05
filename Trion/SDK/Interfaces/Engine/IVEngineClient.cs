using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IVEngineClient : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void ClientCmdDelegate(IntPtr Class, [MarshalAs(UnmanagedType.LPStr)] string Message);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool GetPlayerInfoDelegate(IntPtr Class, int EntityIndex, void* PlayerInfo);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetPlayerForUserIDDelegate(IntPtr Class, int User);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetLocalPlayerDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetMaxClientsDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetClanTagDelegate(string Arg1, string Arg2);

        public IVEngineClient(IntPtr Base) : base(Base)
        {
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PlayerInfo
        {
            public Int64 __pad0;
            public int m_nXuidLow;
            public int m_nXuidHigh;

            [MarshalAs(UnmanagedType.LPArray)]
            public fixed byte m_szPlayerName[128];
            public int m_nUserID;

            [MarshalAs(UnmanagedType.LPArray)]
            public fixed char m_szSteamID[20];
            public fixed char pad1[16];
            public UInt64 m_nSteam3ID;

            [MarshalAs(UnmanagedType.LPArray)]
            public fixed char m_szFriendsName[128];

            [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
            public bool m_bIsFakePlayer;

            [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
            public bool m_bIsHLTV;

            public uint m_dwCustomFiles;
            public char m_FilesDownloaded;
        }

        public bool GetPlayerInfo(int EntityIndex, out PlayerInfo PlayerInfo)
        {
            fixed (PlayerInfo* InfoPtr = &PlayerInfo)
            {
                return CallVirtualFunction<GetPlayerInfoDelegate>(8)(this, EntityIndex, InfoPtr);
            }
        }

        public int GetLocalPlayer => CallVirtualFunction<GetLocalPlayerDelegate>(12)(this);
    }
}