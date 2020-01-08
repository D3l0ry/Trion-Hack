using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Trion.Client.Site;
using Trion.Client.Site.Strucutres;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Gui;
using Trion.SDK.Serializers;
using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;
using static Trion.SDK.Interfaces.Engine.IVEngineClient;

namespace Trion
{
    unsafe class EntryPoint
    {
        public static int DLLMain()
        {
            Interface.Panel.Hook(41, Hooks.PaintTraverseDelegate);
            Interface.ClientMode.Hook(24, Hooks.CreateMoveDelegate);
            Interface.GameEventManager.Hook(9, Hooks.FireEventClientSideDelegate);
            Interface.BaseClientDLL.Hook(37, Hooks.FrameStageNotifyDelegate);
            Interface.NetVar.HookProp("DT_BaseViewModel", "m_nSequence", Hooks.SetViewModelSequenceDelegate, ref Interface.NetVar.SequencePtr);
            Interface.GameUI.MessageBox("HI", Api.Authorization().IsHwid.ToString());
            return 0;
        }
    }
}