using System;

using Trion.SDK.Interfaces;

namespace Trion
{
    internal unsafe class EntryPoint
    {
        public static int DllMain()
        {
            //Interface.Panel.Hook(41, Hooks.PaintTraverseDelegate);
            Interface.ClientMode.Hook(24, Hooks.CreateMoveDelegate);
            Interface.GameEventManager.Hook(9, Hooks.FireEventClientSideDelegate);
            Interface.BaseClientDLL.Hook(37, Hooks.FrameStageNotifyDelegate);
            //Interface.ClientMode.Hook(44, Hooks.DoPostScreenEffectsDelegate);
            Interface.NetVar.HookProp("DT_BaseViewModel", "m_nSequence", Hooks.SetViewModelSequenceDelegate, ref Interface.NetVar.SequencePtr);
            //Interface.Surface.Hook(67, Hooks.LockCursorDelegate);
            Console.Beep();
            return 0;
        }
    }
}