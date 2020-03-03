using System;
using System.Drawing;

using Trion.Client.Configs;
using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Surface.Controls;
using Trion.SDK.Surface.Drawing;
using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;

namespace Trion.Modules
{
    internal unsafe struct Visual
    {
        #region Variables
        private static readonly Label WaterMarkLabel = new Label()
        {
            ForeColor = Color.FromArgb(152, 241, 221),
            Position = new Point(5, 5),
            Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_OUTLINE),
            Text = "Trion"
        };

        private static IGlowObjectManager* IGlowObjectManager = Offsets.GlowObjectManager;
        #endregion

        #region Private
        private static byte[] PrimeCode = { 0x74, 0xEB };
        private static bool PrimeState = false;
        #endregion

        public static void GlowRender(BasePlayer* localPlayer)
        {
            int myTeam = localPlayer->TeamNum;

            for (int index = 0; index < IGlowObjectManager->Size; index++)
            {
                if (index == Interface.VEngineClient.GetLocalPlayer)
                {
                    continue;
                }

                BasePlayer* entity = Interface.ClientEntityList.GetClientEntity(index)->GetPlayer;

                if (entity == null)
                {
                    continue;
                }

                if (myTeam == entity->TeamNum)
                {
                    continue;
                }

                IGlowObjectManager->SetEntity(index, entity);
                IGlowObjectManager->SetColor(index, ConfigManager.CVisual.GlowHPEnable, entity->GetHealth);
                IGlowObjectManager->SetRenderFlags(index, true, false);
            }
        }

        public static void RadarRender(BasePlayer* entity) => entity->Spotted = 1;

        public static void RevealRanks(ref IClientMode.UserCmd cmd)
        {
            if ((cmd.buttons & IClientMode.Buttons.IN_SCORE) != 0)
            {
                Interface.BaseClientDLL.DispatchUserMessage(50, 0, 0, (void*)0);
            }
        }

        public static void NoFlash()
        {
            var localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if(localPlayer == null)
            {
                return;
            }

            if (!localPlayer->IsAlive)
            {
                return;
            }

            localPlayer->FlashMax = 0f;
        }

        public static void FakePrime()
        {
            if (ConfigManager.CVisual.Prime != PrimeState)
            {
                PrimeState = ConfigManager.CVisual.Prime;

                if (NativeMethods.VirtualProtect((IntPtr)Offsets.FakePrime, 1, ProtectCode.PAGE_EXECUTE_READWRITE, out uint Old))
                {
                    *Offsets.FakePrime = ConfigManager.CVisual.Prime ? PrimeCode[1] : PrimeCode[0];

                    NativeMethods.VirtualProtect((IntPtr)Offsets.FakePrime, 1, (ProtectCode)Old, out uint nullptr);
                }
            }
        }

        public static void WaterMark() => WaterMarkLabel.Show();
    }
}