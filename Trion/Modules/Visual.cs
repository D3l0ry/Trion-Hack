using System.Drawing;

using Trion.Client.Configs;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Surface.Controls;
using Trion.SDK.Surface.Drawing;

namespace Trion.Modules
{
    internal unsafe class Visual
    {
        #region Variables
        private static readonly Label WaterMarkLabel = new Label()
        {
            ForeColor = Color.FromArgb(152, 241, 221),
            Position = new Point(5, 5),
            Font = new Font("Tahoma",29, Font.FontFlags.FONTFLAG_OUTLINE),
            Text = "Trion Project"
        };
        #endregion

        public static void GlowRender()
        {
            BasePlayer* LocalPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            for (int Index = 0; Index < Interface.VEngineClient.GetMaxClients; Index++)
            {
                BasePlayer* Entity = Interface.ClientEntityList.GetClientEntity(Index)->GetPlayer;

                if (Entity == null || !Entity->IsAlive)
                {
                    continue;
                }

                if (LocalPlayer->TeamNum == Entity->TeamNum)
                {
                    continue;
                }

                _ = ConfigManager.CVisual.GlowHPEnable ? Entity->GlowObject->Color = new IGlowObjectManager.GlowColor(1 - (Entity->GetHealth / 100f), Entity->GetHealth / 100f, 0, 175 / 255f) : Entity->GlowObject->Color = ConfigManager.CVisual;

                Entity->GlowObject->fullBloomRender = ConfigManager.CVisual.FullBloom;
                Entity->GlowObject->glowStyle = ConfigManager.CVisual.GlowStyle;
                Entity->GlowObject->renderWhenOccluded = true;
            }
        }

        public static void RadarRender(BasePlayer* Entity) => Entity->Spotted = 1;

        public static void RevealRanks(IClientMode.UserCmd* CMD)
        {
            if ((CMD->buttons & IClientMode.Buttons.IN_SCORE) != 0)
            {
                Interface.BaseClientDLL.DispatchUserMessage(50, 0, 0, (void*)0);
            }
        }

        public static void NoFlash()
        {
            var LocalPLayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if (!LocalPLayer->IsAlive)
            {
                return;
            }

            LocalPLayer->FlashMax = 0f;
        }

        public static void WaterMark() => WaterMarkLabel.Show();
    }
}