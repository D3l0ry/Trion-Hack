using Trion.Client.Configs;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;

namespace Trion.Modules
{
    internal unsafe class Misc
    {
        public static void BunnyHop(IClientMode.UserCmd* CMD)
        {
            var LocalPLayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if (LocalPLayer->GetMoveType == BasePlayer.MoveType.LADDER || LocalPLayer->GetMoveType == BasePlayer.MoveType.NOCLIP && (LocalPLayer->GetFlags & (int)BasePlayer.Flags.FL_INWATER) != 0)
            {
                return;
            }

            _ = ((CMD->buttons & IClientMode.Buttons.IN_JUMP) != 0 && (LocalPLayer->GetFlags & (int)BasePlayer.Flags.FL_ONGROUND) != 0) ? CMD->buttons |= IClientMode.Buttons.IN_JUMP : CMD->buttons &= ~IClientMode.Buttons.IN_JUMP;
        }

        public static void AutoStrafe(IClientMode.UserCmd* CMD)
        {
            if (ConfigManager.CVisual.AutoStrafe && (Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer->GetFlags & 1) == 0)
            {
                if (CMD->mousedx < -25)
                {
                    CMD->sidemove = -475.0f;
                }
                else if (CMD->mousedx > 25)
                {
                    CMD->sidemove = 475.0f;
                }
            }
        }

        public static void MoonWalk(IClientMode.UserCmd* CMD)
        {
            if (ConfigManager.CVisual.MoonWalk && Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer->GetFlags != (int)BasePlayer.MoveType.LADDER)
            {
                CMD->buttons ^= IClientMode.Buttons.IN_FORWARD | IClientMode.Buttons.IN_BACK | IClientMode.Buttons.IN_MOVELEFT | IClientMode.Buttons.IN_MOVERIGHT;
            }
        }
    }
}