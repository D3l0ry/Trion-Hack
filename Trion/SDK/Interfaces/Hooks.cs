using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Trion.Client.Configs;
using Trion.Client.Menu;
using Trion.Modules;
using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Interfaces.Gui;
using Trion.SDK.Structures;

namespace Trion.SDK.Interfaces
{
    internal unsafe struct Hooks
    {
        #region Delegates
        public static IClientMode.CreateMoveHookDelegate CreateMoveDelegate = CreateMove;
        public static IClientMode.GetViewModelFovHookDelegate GetViewModelFovDelegate = GetViewModelFov;
        public static IClientMode.DoPostScreenEffectsHookDelegate DoPostScreenEffectsDelegate = DoPostScreenEffects;
        public static IBaseClientDLL.FrameStageNotifyHookDelegate FrameStageNotifyDelegate = FrameStageNotify;
        public static IGameEventManager.FireEventClientSideHookDelegate FireEventClientSideDelegate = FireEventClientSide;
        public static IPanel.PaintTraverseHookDelegate PaintTraverseDelegate = PaintTraverse;
        public static ISurface.LockCursorHookDelegate LockCursorDelegate = LockCursor;
        public static NetVar.SetViewModelSequenceHookDelegate SetViewModelSequenceDelegate = SetViewModelSequence;
        #endregion

        private static Main Main = new Main();

        private static bool CreateMove(float Smt, ref IClientMode.UserCmd UserCmd)
        {
            if (UserCmd.IsNull)
            {
                return Interface.ClientMode.CreateMoveOriginal(Smt, ref UserCmd);
            }

            if (UserCmd.commandNumber == 0)
            {
                return Interface.ClientMode.CreateMoveOriginal(Smt, ref UserCmd);
            }

            BasePlayer* localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if (localPlayer == null)
            {
                return false;
            }

            if (ConfigManager.CMisc.BunnyHop)
            {
                Misc.BunnyHop(ref UserCmd, localPlayer);
            }

            if (ConfigManager.CVisual.RevealRanks)
            {
                Visual.RevealRanks(ref UserCmd);
            }

            if (ConfigManager.CMisc.AutoStrafe)
            {
                Misc.AutoStrafe(ref UserCmd, localPlayer);
            }

            if (ConfigManager.CMisc.MoonWalk)
            {
                Misc.MoonWalk(ref UserCmd, localPlayer);
            }

            return false;
        }

        private static float GetViewModelFov()
        {
            return Interface.ClientMode.GetViewModelFovOriginal() + ConfigManager.CVisual.ViewModelFov;
        }

        private static int DoPostScreenEffects(int Param)
        {
            BasePlayer* localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if (localPlayer == null)
            {
                return Interface.ClientMode.DoPostScreenEffectsOriginal(Param);
            }

            if (ConfigManager.CVisual.GlowEnable)
            {
                Visual.GlowRender(localPlayer);
            }

            if (ConfigManager.CVisual.NoFlash)
            {
                Visual.NoFlash();
            }

            return Interface.ClientMode.DoPostScreenEffectsOriginal(Param);
        }

        private static void FrameStageNotify(IBaseClientDLL.FrameStage FrameStage)
        {
            if (FrameStage == IBaseClientDLL.FrameStage.RENDER_START)
            {
                Visual.FakePrime();
            }

            if (FrameStage == IBaseClientDLL.FrameStage.NET_UPDATE_POSTDATAUPDATE_START)
            {
                BasePlayer* localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

                if (localPlayer == null)
                {
                    Interface.BaseClientDLL.FrameStageNotifyOriginal(FrameStage);

                    return;
                }

                if (ConfigManager.CSkinChanger.SkinChangerActive)
                {
                    SkinChanger.OnFrameStage(FrameStage, localPlayer);
                }
            }

            Interface.BaseClientDLL.FrameStageNotifyOriginal(FrameStage);
        }

        private static bool FireEventClientSide(ref IGameEventManager.GameEvent Event)
        {
            if (Event.GetName() == "player_death")
            {
                SkinChanger.ApplyKillIcon(ref Event);
                SkinChanger.ApplyStatTrack(ref Event);
            }

            return Interface.GameEventManager.FireEventClientSideOriginal(ref Event);
        }

        private static void PaintTraverse(uint Panel, bool ForcePaint, bool AllowForce)
        {
            if (Interface.Panel.GetName(Panel) == "MatSystemTopPanel")
            {
                if (ConfigManager.CVisual.WaterMark)
                {
                    Visual.WaterMark();
                }

                Main.Show();
            }

            Interface.Panel.PaintTraverseOriginal(Panel, ForcePaint, AllowForce);
        }

        private static void* LockCursor()
        {
            if (Main.Visible)
            {
                return Interface.Surface.UnlockCursor();
            }

            return Interface.Surface.LockCursor();
        }

        private static void SetViewModelSequence(ref IBaseClientDLL.RecvProxyData data, void* Struct, void* Out)
        {
            var LocalPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;
            if (LocalPlayer == null || !LocalPlayer->IsAlive)
            {
                Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref data, Struct, Out);

                return;
            }

            BaseViewModel* viewModel = (BaseViewModel*)(Struct);

            if (viewModel != null)
            {
                var Owner = viewModel->GetOwner;

                if (Owner != null && Owner == LocalPlayer)
                {
                    string ModelName = Interface.ModelInfoClient.GetModelName(Interface.ModelInfoClient.GetModel(viewModel->ModelIndex));
                    int Sequence = data.m_Value.m_Int;

                    data.m_Value.m_Int = SkinChanger.GetSequence(ModelName, Sequence);
                }
            }

            Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref data, Struct, Out);
        }
    }
}