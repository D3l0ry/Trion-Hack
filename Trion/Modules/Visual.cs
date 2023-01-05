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
        private static IGlowObjectManager* m_GlowObjectManager = Offsets.GlowObjectManager;

        public static void GlowRender(ref BasePlayer localPlayer)
        {
            int myTeam = localPlayer.TeamNum;

            for (int index = 0; index < m_GlowObjectManager->Size; index++)
            {
                if (index == Interface.VEngineClient.GetLocalPlayer)
                {
                    continue;
                }

                ref BasePlayer entity = ref Interface.ClientEntityList.GetClientEntity(index).GetPlayer;

                if (entity.IsNull)
                {
                    continue;
                }

                if (myTeam == entity.TeamNum)
                {
                    continue;
                }

                fixed (void* entityPtr = &entity)
                {
                    m_GlowObjectManager->SetEntity(index, entityPtr);
                }

                m_GlowObjectManager->SetColor(index, ConfigManager.CVisual.GlowHPEnable, entity.GetHealth);
                m_GlowObjectManager->SetBloom(index, ConfigManager.CVisual.GlowFullBloom);
                m_GlowObjectManager->SetRenderFlags(index, true, false);
            }
        }

        public static void RevealRanks(ref IClientMode.UserCmd userCmd)
        {
            if ((userCmd.buttons & IClientMode.Buttons.IN_SCORE) == 0)
            {
                return;
            }

            Interface.BaseClientDLL.DispatchUserMessage(50, 0, 0, (void*)0);
        }

        public static void NoFlash()
        {
            ref BasePlayer localPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

            if (localPlayer.IsNull)
            {
                return;
            }

            if (!localPlayer.IsAlive)
            {
                return;
            }

            localPlayer.FlashMax = 0f;
        }
    }
}