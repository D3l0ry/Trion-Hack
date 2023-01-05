using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Trion.SDK.Interfaces;

namespace Trion
{
    internal unsafe class EntryPoint
    {
        //1)Открой консоль и напиши ildasm.У тебя откроется приложение для дизасемблирования кода на шарпе или любом.NET языке.
        //2)Перенеси мышкой библиотеку в это приложение
        //3)Нажми ctrl+D и выбери папку для сохранения il файла(Не забудь задать имя)
        //4)Открой il файл
        //5) После строчки corflags 0x00000003(Она может отличаться последней цифрой) добавь следующие 2 строчки кода
        //.vtfixup [1] int32 fromunmanaged at VT_01
        //.data VT_01 = int32(0)
        //6)Потом найди функцию DllMain и вставь в этот метод следующие 2 строчки
        //.vtentry 1 : 1
        //.export[1]
        //7)После всего можешь скомпилировать файл следующей командой(Твой путь может отличаться от моего)
        //ilasm /OUT:"P:\Trion Compilation\Internal\test.dll" "P:\Trion Compilation\Internal\test.il" /dll
        public static bool DllMain()
        {
            Interface.Panel.Hook(41, Hooks.PaintTraverseDelegate);
            Interface.ClientMode.Hook(24, Hooks.CreateMoveDelegate);
            Interface.ClientMode.Hook(35, Hooks.GetViewModelFovDelegate);
            Interface.GameEventManager.Hook(9, Hooks.FireEventClientSideDelegate);
            Interface.BaseClientDLL.Hook(37, Hooks.FrameStageNotifyDelegate);
            Interface.ClientMode.Hook(44, Hooks.DoPostScreenEffectsDelegate);
            Interface.NetVar.HookProp("DT_BaseViewModel", "m_nSequence", Hooks.SetViewModelSequenceDelegate, ref Interface.NetVar.SequencePtr);
            Interface.Surface.Hook(67, Hooks.LockCursorDelegate);

            Interface.GameUI.MessageBox("Запущено успешно", "Библиотека успешно запущена");
            Console.Beep();

            return true;
        }
    }
}