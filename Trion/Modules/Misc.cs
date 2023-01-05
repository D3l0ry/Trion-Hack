using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;

namespace Trion.Modules
{
    internal unsafe struct Misc
    {
        public static void BunnyHop(ref IClientMode.UserCmd cmd, ref BasePlayer localPlayer)
        {
            if (localPlayer.GetMoveType == BasePlayer.MoveType.LADDER || localPlayer.GetMoveType == BasePlayer.MoveType.NOCLIP && (localPlayer.GetFlags & (int)BasePlayer.Flags.FL_INWATER) != 0)
            {
                return;
            }

            if ((localPlayer.GetFlags & (int)BasePlayer.Flags.FL_ONGROUND) != 0)
            {
                cmd.buttons |= IClientMode.Buttons.IN_JUMP;
            }
            else
            {
                cmd.buttons &= ~IClientMode.Buttons.IN_JUMP;
            }
        }

        public static void AutoStrafe(ref IClientMode.UserCmd cmd, ref BasePlayer localPlayer)
        {
            if ((localPlayer.GetFlags & 1) == 0)
            {
                if (cmd.mousedx < 0)
                {
                    cmd.sidemove = -450.0f;
                }
                else if (cmd.mousedx > 0)
                {
                    cmd.sidemove = 450.0f;
                }
            }
        }

        public static void MoonWalk(ref IClientMode.UserCmd cmd, ref BasePlayer localPlayer)
        {
            if (localPlayer.GetFlags != (int)BasePlayer.MoveType.LADDER)
            {
                cmd.buttons ^= IClientMode.Buttons.IN_FORWARD | IClientMode.Buttons.IN_BACK | IClientMode.Buttons.IN_MOVELEFT | IClientMode.Buttons.IN_MOVERIGHT;
            }
        }
    }
}