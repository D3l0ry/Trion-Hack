using System;
using System.Runtime.InteropServices;

using Trion.SDK.Structures;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IClientMode : VMTable
    {
        public enum Buttons
        {
            IN_ATTACK = 1 << 0,
            IN_JUMP = 1 << 1,
            IN_DUCK = 1 << 2,
            IN_FORWARD = 1 << 3,
            IN_BACK = 1 << 4,
            IN_USE = 1 << 5,
            IN_MOVELEFT = 1 << 9,
            IN_MOVERIGHT = 1 << 10,
            IN_ATTACK2 = 1 << 11,
            IN_SCORE = 1 << 16,
            IN_BULLRUSH = 1 << 22
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UserCmd
        {
            private readonly int pad;
            public int commandNumber;
            private readonly int tickCount;
            public Vector3 ViewAngles;
            private readonly float aimDirectionX;
            private readonly float aimDirectionY;
            private readonly float aimDirectionZ;
            public float forwardmove;
            public float sidemove;
            private readonly float upmove;
            public Buttons buttons;
            private readonly int impulse;
            private readonly int weaponselect;
            private readonly int weaponsubtype;
            private readonly int randomSeed;
            public short mousedx;
            public short mousedy;
            private readonly bool hasbeenpredicted;

            public bool IsNull
            {
                get
                {
                    fixed (void* Class = &this)
                    {
                        return Class == null;
                    }
                }
            }
        };

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool CreateMoveDelegate(IntPtr Class, float Smt, ref UserCmd UserCmd);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool CreateMoveHookDelegate(float Smt, ref UserCmd UserCmd);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate float GetViewModelFovDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate float GetViewModelFovHookDelegate();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int DoPostScreenEffectsDelegate(IntPtr Class, int Param);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int DoPostScreenEffectsHookDelegate(int Param);

        public IClientMode(IntPtr Base) : base(Base) { }

        public bool CreateMoveOriginal(float Smt, ref UserCmd UserCmd) => CallOriginalFunction<CreateMoveDelegate>(24)(this, Smt, ref UserCmd);

        public float GetViewModelFovOriginal() => CallOriginalFunction<GetViewModelFovDelegate>(35)(this);

        public int DoPostScreenEffectsOriginal(int Param) => CallOriginalFunction<DoPostScreenEffectsDelegate>(44)(this, Param);
    }
}