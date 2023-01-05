using System;
using System.Runtime.InteropServices;

using Trion.Client.Configs;
using Trion.Client.Menu;
using Trion.Modules;
using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Interfaces.Gui;
using Trion.SDK.Structures;

namespace Trion.SDK.Interfaces
{
    internal unsafe struct Hooks
    {
        public static IClientMode.CreateMoveHookDelegate CreateMoveDelegate = CreateMove;
        public static IClientMode.GetViewModelFovHookDelegate GetViewModelFovDelegate = GetViewModelFov;
        public static IClientMode.DoPostScreenEffectsHookDelegate DoPostScreenEffectsDelegate = DoPostScreenEffects;
        public static IBaseClientDLL.FrameStageNotifyHookDelegate FrameStageNotifyDelegate = FrameStageNotify;
        public static IGameEventManager.FireEventClientSideHookDelegate FireEventClientSideDelegate = FireEventClientSide;
        public static IPanel.PaintTraverseHookDelegate PaintTraverseDelegate = PaintTraverse;
        public static ISurface.LockCursorHookDelegate LockCursorDelegate = LockCursor;
        public static NetVar.SetViewModelSequenceHookDelegate SetViewModelSequenceDelegate = SetViewModelSequence;
        private static Main Main = new Main();

        private static bool CreateMove(float smt, ref IClientMode.UserCmd userCmd)
        {
            if (userCmd.IsNull)
            {
                return Interface.ClientMode.CreateMoveOriginal(smt, ref userCmd);
            }

            if (userCmd.commandNumber == 0)
            {
                return Interface.ClientMode.CreateMoveOriginal(smt, ref userCmd);
            }

            ref BasePlayer localPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

            if (localPlayer.IsNull)
            {
                return false;
            }

            if ((userCmd.buttons & IClientMode.Buttons.IN_JUMP) != 0)
            {
                Misc.BunnyHop(ref userCmd, ref localPlayer);
                Misc.AutoStrafe(ref userCmd, ref localPlayer);
            }

            if (ConfigManager.CMisc.BunnyHop)
            {
                Misc.BunnyHop(ref userCmd, ref localPlayer);
            }

            if (ConfigManager.CVisual.RevealRanks)
            {
                Visual.RevealRanks(ref userCmd);
            }

            if (ConfigManager.CMisc.AutoStrafe)
            {
                Misc.AutoStrafe(ref userCmd, ref localPlayer);
            }

            if (ConfigManager.CMisc.MoonWalk)
            {
                Misc.MoonWalk(ref userCmd, ref localPlayer);
            }

            return true;
        }

        private static float GetViewModelFov() => Interface.ClientMode.GetViewModelFovOriginal() + ConfigManager.CVisual.ViewModelFov;

        private static int DoPostScreenEffects(int param)
        {
            ref BasePlayer localPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

            if (localPlayer.IsNull)
            {
                return Interface.ClientMode.DoPostScreenEffectsOriginal(param);
            }

            if (ConfigManager.CVisual.GlowEnable)
            {
                Visual.GlowRender(ref localPlayer);
            }

            if (ConfigManager.CVisual.NoFlash)
            {
                Visual.NoFlash();
            }

            return Interface.ClientMode.DoPostScreenEffectsOriginal(param);
        }

        private static void FrameStageNotify(IBaseClientDLL.FrameStage frameStage)
        {
            if (frameStage == IBaseClientDLL.FrameStage.NET_UPDATE_POSTDATAUPDATE_START)
            {
                ref BasePlayer localPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

                if (localPlayer.IsNull)
                {
                    Interface.BaseClientDLL.FrameStageNotifyOriginal(frameStage);

                    return;
                }

                SkinChanger.OnFrameStage(ref localPlayer);
            }

            Interface.BaseClientDLL.FrameStageNotifyOriginal(frameStage);
        }

        private static bool FireEventClientSide(ref IGameEventManager.GameEvent gameEvent)
        {
            if (gameEvent.GetName() == "player_death")
            {
                int userId = gameEvent.GetInt("attacker");
                Interface.VEngineClient.GetPlayerInfo(Interface.VEngineClient.GetLocalPlayer, out IVEngineClient.PlayerInfo playerInfo);

                if (userId != playerInfo.m_nUserID)
                {
                    return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
                }

                ref BasePlayer localPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

                if (!localPlayer.IsAlive)
                {
                    return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
                }

                SkinChanger.ApplyKillIcon(ref gameEvent, ref localPlayer);
                SkinChanger.ApplyStatTrack(ref gameEvent, ref localPlayer);
            }

            return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
        }

        private static void PaintTraverse(uint panel, bool forcePaint, bool allowForce)
        {
            if (Interface.Panel.GetName(panel) == "MatSystemTopPanel")
            {
                Main.Show();
            }

            Interface.Panel.PaintTraverseOriginal(panel, forcePaint, allowForce);
        }

        private static IntPtr LockCursor()
        {
            if (Main.Visible)
            {
                return Interface.Surface.UnlockCursor();
            }

            return Interface.Surface.LockCursor();
        }

        private static void SetViewModelSequence(ref IBaseClientDLL.RecvProxyData data, void* Struct, void* Out)
        {
            ref BasePlayer LocalPlayer = ref Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

            if (LocalPlayer.IsNull || !LocalPlayer.IsAlive)
            {
                Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref data, Struct, Out);

                return;
            }

            BaseViewModel* viewModel = (BaseViewModel*)Struct;

            if (viewModel != null)
            {
                ref IClientEntity Owner = ref viewModel->GetOwner;

                if (!Owner.IsNull)
                {
                    fixed (void* ownerPtr = &Owner)
                    {
                        fixed (void* playerPtr = &LocalPlayer)
                        {
                            if (ownerPtr == playerPtr)
                            {
                                string ModelName = Interface.ModelInfoClient.GetModelName(Interface.ModelInfoClient.GetModel(viewModel->ModelIndex));
                                int Sequence = data.m_Value.m_Int;

                                data.m_Value.m_Int = SkinChanger.GetSequence(ModelName, Sequence);
                            }
                        }
                    }
                }
            }

            Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref data, Struct, Out);
        }
    }
}