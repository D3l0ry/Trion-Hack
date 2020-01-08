using Trion.SDK.Interfaces;

namespace Trion.SDK.Dumpers
{
    internal unsafe static class Offsets
    {
        public static void* dwSetClanTag = Interface.EngineAddress.FindPattern("53 56 57 8B DA 8B F9 FF 15");
    }
}