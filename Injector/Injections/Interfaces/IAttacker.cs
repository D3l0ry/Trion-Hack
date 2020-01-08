using System;

using Injector.Injections.Enums;

namespace Injector.Injections.Interfaces
{
    internal interface IAttacker: IDisposable
    {
        ReturnCode Injecting();
    }
}