using System;
using System.Runtime.InteropServices;

using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IClientMode : VMTable
    {
        #region Initialization
        public IClientMode(void* Base) : base(Base)
        {
        }

        public IClientMode(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Enums
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
        #endregion

        #region Structures
        public struct UserCmd
        {
            private int pad;
            public int commandNumber;
            public int tickCount;
            public Vector3 viewangles;
            public Vector3 aimdirection;
            public float forwardmove;
            public  float sidemove;
            public float upmove;
            public  Buttons buttons;
            public int impulse;
            public int weaponselect;
            public int weaponsubtype;
            public int randomSeed;
            public short mousedx;
            public short mousedy;
            public bool hasbeenpredicted;
        };
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool CreateMoveDelegate(void* Class, float Smt, void* UserCmd);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool CreateMoveHookDelegate(float Smt, void* UserCmd);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate float GetViewModelFovDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate float GetViewModelFovHookDelegate();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int DoPostScreenEffectsDelegate(void* Class, int Param);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int DoPostScreenEffectsHookDelegate(int Param);
        #endregion

        #region Methods
        public bool CreateMoveOriginal(float Smt, void* UserCmd) => CallOriginalFunction<CreateMoveDelegate>(24)(this, Smt, UserCmd);

        public float GetViewModelFovOriginal() => CallOriginalFunction<GetViewModelFovDelegate>(35)(this);

        public int DoPostScreenEffectsOriginal(int Param) => CallOriginalFunction<DoPostScreenEffectsDelegate>(44)(this, Param);
        #endregion
    }
}