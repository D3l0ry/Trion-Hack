using System;
using System.Runtime.InteropServices;

using Trion.Modules;
using Trion.SDK.Dumpers;
using Trion.SDK.Enums;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Structures;

namespace Trion.SDK.Interfaces
{
    internal unsafe struct Hooks
    {
        public static IBaseClientDLL.FrameStageNotifyHookDelegate FrameStageNotifyDelegate = FrameStageNotify;
        public static IGameEventManager.FireEventClientSideHookDelegate FireEventClientSideDelegate = FireEventClientSide;
        public static NetVar.SetViewModelSequenceHookDelegate SetViewModelSequenceDelegate = SetViewModelSequence;

        private static void FrameStageNotify(FrameStage frameStage)
        {
            if (frameStage == FrameStage.NET_UPDATE_POSTDATAUPDATE_START)
            {
                BasePlayer* localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

                if (localPlayer->IsNull)
                {
                    Interface.BaseClientDLL.FrameStageNotifyOriginal(frameStage);

                    return;
                }

                SkinChanger.OnFrameStage(localPlayer);
            }

            Interface.BaseClientDLL.FrameStageNotifyOriginal(frameStage);
        }

        private static bool FireEventClientSide(ref IGameEventManager.GameEvent gameEvent)
        {
            if (gameEvent.GetName() == "player_death")
            {
                int userId = gameEvent.GetInt("attacker");
                Interface.VEngineClient.GetPlayerInfo(Interface.VEngineClient.GetLocalPlayer, out PlayerInfo playerInfo);

                if (userId != playerInfo.m_nUserID)
                {
                    return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
                }

                BasePlayer* localPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

                if (!localPlayer->IsAlive)
                {
                    return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
                }

                SkinChanger.ApplyKillIcon(ref gameEvent, localPlayer);
                SkinChanger.ApplyStatTrack(ref gameEvent, localPlayer);
            }

            return Interface.GameEventManager.FireEventClientSideOriginal(ref gameEvent);
        }

        private static void SetViewModelSequence(ref RecvProxyData data, void* Struct, void* Out)
        {
            BasePlayer* LocalPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer).GetPlayer;

            if (LocalPlayer->IsNull || !LocalPlayer->IsAlive)
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
                        if (ownerPtr == LocalPlayer)
                        {
                            string ModelName = Interface.ModelInfoClient.GetModelName(Interface.ModelInfoClient.GetModel(viewModel->ModelIndex));
                            int Sequence = data.m_Value.m_Int;

                            data.m_Value.m_Int = SkinChanger.GetSequence(ModelName, Sequence);
                        }
                    }
                }
            }

            Marshal.GetDelegateForFunctionPointer<NetVar.SetViewModelSequenceOriginalDelegate>((IntPtr)Interface.NetVar.SequencePtr)(ref data, Struct, Out);
        }
    }
}