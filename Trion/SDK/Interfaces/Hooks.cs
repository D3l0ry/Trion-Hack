using System;
using System.Runtime.InteropServices;

using Trion.Client.Configs;
using Trion.Client.Menu;
using Trion.Modules;
using Trion.SDK.Dumpers;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Interfaces.Gui;
using Trion.SDK.Structures.Modules;

namespace Trion.SDK.Interfaces
{
    internal unsafe class Hooks
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

        private static bool CreateMove(float Smt, void* CMD)
        {
            if (CMD == null)
            {
                return Interface.ClientMode.CreateMoveOriginal(Smt, CMD);
            }

            IClientMode.UserCmd* UserCmd = (IClientMode.UserCmd*)CMD;

            if (UserCmd->commandNumber == 0)
            {
                return Interface.ClientMode.CreateMoveOriginal(Smt, CMD);
            }

            if (!Interface.VEngineClient.IsInGame)
            {
                return false;
            }

            if (ConfigManager.CVisual.BunnyHop)
            {
                Misc.BunnyHop(UserCmd);
            }

            if (ConfigManager.CVisual.RevealRanks)
            {
                Visual.RevealRanks(UserCmd);
            }

            if (ConfigManager.CVisual.AutoStrafe)
            {
                Misc.AutoStrafe(UserCmd);
            }

            if (ConfigManager.CVisual.MoonWalk)
            {
                Misc.MoonWalk(UserCmd);
            }

            return false;
        }

        private static float GetViewModelFov()
        {
            return Interface.ClientMode.GetViewModelFovOriginal() + ConfigManager.CVisual.ViewModelFov;
        }

        private static int DoPostScreenEffects(int Param)
        {
            if(!Interface.VEngineClient.IsInGame)
            {
                return Interface.ClientMode.DoPostScreenEffectsOriginal(Param);
            }

            if (ConfigManager.CVisual.GlowEnable)
            {
                Visual.GlowRender();
            }

            if (ConfigManager.CVisual.NoFlash)
            {
                Visual.NoFlash();
            }

            return Interface.ClientMode.DoPostScreenEffectsOriginal(Param);
        }

        private static void FrameStageNotify(IBaseClientDLL.FrameStage FrameStage)
        {
            if(!Interface.VEngineClient.IsInGame)
            {
                Interface.BaseClientDLL.FrameStageNotifyOriginal(FrameStage);

                return;
            }

            if (ConfigManager.CSkinChanger.SkinChangerActive)
            {
                SkinChanger.OnFrameStage(FrameStage);
            }

            Interface.BaseClientDLL.FrameStageNotifyOriginal(FrameStage);
        }

        private static bool FireEventClientSide(IGameEventManager.GameEvent* Event)
        {
            if (Event->GetName().Equals("player_death"))
            {
                SkinChanger.ApplyKillIcon(Event);
                SkinChanger.UpdateStatTrack(Event);
            }

            return Interface.GameEventManager.FireEventClientSideOriginal(Event);
        }

        private static void PaintTraverse(uint Panel, bool ForcePaint, bool AllowForce)
        {
            if (Interface.Panel.GetName(Panel).Equals("MatSystemTopPanel"))
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

        private static void SetViewModelSequence(ref IBaseClientDLL.RecvProxyData Data, void* Struct, void* Out)
        {
            var LocalPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;
            if (LocalPlayer == null || !LocalPlayer->IsAlive)
            {
                Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref Data, Struct, Out);

                return;
            }

            BaseViewModel* ViewModel = (BaseViewModel*)Struct;
            if (ViewModel != null)
            {
                var Owner = ViewModel->GetOwner;

                if (Owner != null && Owner == LocalPlayer)
                {
                    string ModelName = Interface.ModelInfoClient.GetModelName(Interface.ModelInfoClient.GetModel(ViewModel->ModelIndex));
                    int Sequence = Data.m_Value.m_Int;

                    if (ModelName.Equals("models/weapons/v_knife_butterfly.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_DRAW, (int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_DRAW2);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT01:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT01, (int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT03);
                                break;
                            default:
                                Sequence++;
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_falchion_advanced.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                                Sequence = (int)SkinChanger.AnimationSequence.SEQUENCE_FALCHION_IDLE1;
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_HEAVY_MISS1:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_FALCHION_HEAVY_MISS1, (int)SkinChanger.AnimationSequence.SEQUENCE_FALCHION_HEAVY_MISS1_NOFLIP);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_FALCHION_LOOKAT01, (int)SkinChanger.AnimationSequence.SEQUENCE_FALCHION_LOOKAT02);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                                break;
                            default:
                                Sequence -= 1;
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_css.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_CSS_LOOKAT01, (int)SkinChanger.AnimationSequence.SEQUENCE_CSS_LOOKAT02);
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_push.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                                Sequence = (int)SkinChanger.AnimationSequence.SEQUENCE_DAGGERS_IDLE1;
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LIGHT_MISS1:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LIGHT_MISS2:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_DAGGERS_LIGHT_MISS1, (int)SkinChanger.AnimationSequence.SEQUENCE_DAGGERS_LIGHT_MISS5);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_HEAVY_MISS1:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_DAGGERS_HEAVY_MISS2, (int)SkinChanger.AnimationSequence.SEQUENCE_DAGGERS_HEAVY_MISS1);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_HEAVY_HIT1:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_HEAVY_BACKSTAB:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence += 3;
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                                break;
                            default:
                                Sequence += 2;
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_survival_bowie.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                                Sequence = (int)SkinChanger.AnimationSequence.SEQUENCE_BOWIE_IDLE1;
                                break;
                            default:
                                Sequence -= 1;
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_ursus.mdl") || ModelName.Equals("models/weapons/v_knife_cord.mdl") || ModelName.Equals("models/weapons/v_knife_canis.mdl") || ModelName.Equals("models/weapons/v_knife_outdoor.mdl") || ModelName.Equals("models/weapons/v_knife_skeleton.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_DRAW, (int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_DRAW2);
                                break;
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence = Weapon.GetRandomInt((int)SkinChanger.AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT01, 14);
                                break;
                            default:
                                Sequence += 1;
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_stiletto.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence = Weapon.GetRandomInt(12, 13);
                                break;
                        }
                    }
                    else if (ModelName.Equals("models/weapons/v_knife_widowmaker.mdl"))
                    {
                        switch (Sequence)
                        {
                            case (int)SkinChanger.AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                                Sequence = Weapon.GetRandomInt(14, 15);
                                break;
                        }
                    }

                    Data.m_Value.m_Int = Sequence;
                }
            }

            Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref Data, Struct, Out);
        }
    }
}