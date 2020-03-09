using System;
using System.Runtime.InteropServices;

using Trion.SDK.Dumpers;
using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IVEngineClient : VMTable
    {
        #region Initialization
        public IVEngineClient(void* Base) : base(Base)
        {
        }

        public IVEngineClient(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Structures
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
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetScreenSizeDelegate(void* Class, int* Width, int* Height);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void ClientCmdDelegate(void* Class, [MarshalAs(UnmanagedType.LPStr)]string Message);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool GetPlayerInfoDelegate(void* Class, int EntityIndex, void* PlayerInfo);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetPlayerForUserIDDelegate(void* Class, int User);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetLocalPlayerDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetViewAnglesDelegate(void* Class, out Vector3 Angles);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetViewAnglesDelegate(void* Class, void* Angles);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetMaxClientsDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsInGameDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsConnectedDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate float* WorldToScreenMatrixDelegate(void* Class);

        [UnmanagedFunctionPointer( CallingConvention.ThisCall)]
        private delegate void SetClanTagDelegate(string Arg1, string Arg2);
        #endregion

        #region Methods
        public void GetScreenSize(out int Width, out int Height)
        {
            fixed (int* WidthPtr = &Width)
            {
                fixed (int* HeightPtr = &Height)
                {
                    CallVirtualFunction<GetScreenSizeDelegate>(5)(this, WidthPtr, HeightPtr);
                }
            }
        }

        public void ClientCmd(string Message) => CallVirtualFunction<ClientCmdDelegate>(7)(this, Message);

        public bool GetPlayerInfo(int EntityIndex, out PlayerInfo PlayerInfo)
        {
            fixed (PlayerInfo* InfoPtr = &PlayerInfo)
            {
                return CallVirtualFunction<GetPlayerInfoDelegate>(8)(this, EntityIndex, InfoPtr);
            }
        }

        public int GetPlayerForUserID(int User) => CallVirtualFunction<GetPlayerForUserIDDelegate>(9)(this, User);

        public int GetLocalPlayer => CallVirtualFunction<GetLocalPlayerDelegate>(12)(this);

        public void GetViewAngles(out Vector3 Angles) => CallVirtualFunction<GetViewAnglesDelegate>(18)(this, out Angles);

        public Vector3 SetViewAngles
        {
            set => CallVirtualFunction<SetViewAnglesDelegate>(19)(this, &value);
        }

        public int GetMaxClients => CallVirtualFunction<GetMaxClientsDelegate>(20)(this);

        public bool IsInGame => CallVirtualFunction<IsInGameDelegate>(26)(this);

        public bool IsConnected => CallVirtualFunction<IsConnectedDelegate>(27)(this);

        public float* WorldToScreen => CallVirtualFunction<WorldToScreenMatrixDelegate>(37)(this);

        public void SetClanTag(string ClanTag) => Marshal.GetDelegateForFunctionPointer<SetClanTagDelegate>((IntPtr)Offsets.dwSetClanTag)(ClanTag,ClanTag);
        #endregion
    }
}