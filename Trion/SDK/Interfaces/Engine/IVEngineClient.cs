using System;
using System.Runtime.InteropServices;

using Trion.SDK.Structures;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe class IVEngineClient : VMTable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool GetPlayerInfoDelegate(IntPtr Class, int EntityIndex, ref PlayerInfo PlayerInfo);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetLocalPlayerDelegate(IntPtr Class);

        public IVEngineClient(IntPtr address) : base(address) { }

        public bool GetPlayerInfo(int entityIndex, out PlayerInfo playerInfo)
        {
            playerInfo = new PlayerInfo();

            return CallVirtualFunction<GetPlayerInfoDelegate>(8)(Address, entityIndex, ref playerInfo);
        }

        public int GetLocalPlayer => CallVirtualFunction<GetLocalPlayerDelegate>(12)(Address);
    }
}