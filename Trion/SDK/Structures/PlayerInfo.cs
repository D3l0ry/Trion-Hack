using System;
using System.Runtime.InteropServices;

namespace Trion.SDK.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct PlayerInfo
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
        public ulong m_nSteam3ID;

        [MarshalAs(UnmanagedType.LPArray)]
        public fixed char m_szFriendsName[128];

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsFakePlayer;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsHLTV;

        public uint m_dwCustomFiles;
        public char m_FilesDownloaded;
    }
}