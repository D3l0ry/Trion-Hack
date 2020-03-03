using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;

namespace Trion.SDK.Dumpers
{
    internal unsafe struct Offsets
    {
        public static IGlowObjectManager* GlowObjectManager = *(IGlowObjectManager**)Interface.ClientAddress.FindPattern("0F 11 05 ? ? ? ? 83 C8 01", 3);

        public static void* UTIL_TraceLine = Interface.ClientAddress.FindPattern("55 8B EC 83 E4 F0 83 EC 7C 56 52");

        public static void* dwSetClanTag = Interface.EngineAddress.FindPattern("53 56 57 8B DA 8B F9 FF 15");

        public static byte* FakePrime = (byte*)Interface.ClientAddress.FindPattern("17 F6 40 14 10",-1);
    }
}