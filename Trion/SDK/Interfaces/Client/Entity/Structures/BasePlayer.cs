using System;
using System.Runtime.InteropServices;

using Trion.SDK.Structures;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BasePlayer
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsAliveDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate BaseCombatWeapon* GetActiveWeaponDelegate(void* Class);

        public enum MoveType
        {
            NONE = 0,
            ISOMETRIC,
            WALK,
            STEP,
            FLY,
            FLYGRAVITY,
            VPHYSICS,
            PUSH,
            NOCLIP,
            LADDER,
            OBSERVER,
            CUSTOM,
            LAST = CUSTOM,
            MAX_BITS = 4
        };

        public enum Flags : int
        {
            FL_ONGROUND = 1 << 0,
            FL_DUCKING = 1 << 1,
            FL_WATERJUMP = 1 << 2,
            FL_ONTRAIN = 1 << 3,
            FL_INRAIN = 1 << 4,
            FL_FROZEN = 1 << 5,
            FL_ATCONTROLS = 1 << 6,
            FL_CLIENT = 1 << 7,
            FL_FAKECLIENT = 1 << 8,
            FL_INWATER = 1 << 9
        }

        public bool IsAlive
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return VMTable.CallVirtualFunction<IsAliveDelegate>(Class, 154)(Class) && GetHealth > 0;
                }
            }
        }

        public BaseCombatWeapon* GetActiveWeapon
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return VMTable.CallVirtualFunction<GetActiveWeaponDelegate>(Class, 267)(Class);
                }
            }
        }

        public int GetHealth
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_iHealth"]);
                }
            }
        }

        public int TeamNum
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_iTeamNum"]);
                }
            }
        }

        public MoveType GetMoveType
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (MoveType)(*(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_nRenderMode"] + 1));
                }
            }
        }

        public int GetFlags
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_fFlags"]);
                }
            }
        }

        public float FlashMax
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(float*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_flFlashMaxAlpha"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(float*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_flFlashMaxAlpha"]) = value;
                }
            }
        }

        public ref BaseCombatWeapon GetMyWeapons(int Index)
        {
            fixed (void* Class = &this)
            {
                return ref Interface.ClientEntityList.GetClientEntityFromHandle((IntPtr)((int*)((uint)Class + (Interface.NetVar["DT_BasePlayer", "m_hActiveWeapon"] - 256)))[Index]).GetWeapon;
            }
        }

        public uint* GetMyWearables
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (uint*)((uint)Class + Interface.NetVar["DT_BaseCombatCharacter", "m_hMyWearables"]);
                }
            }
        }

        public ref BaseViewModel GetViewModel
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref Interface.ClientEntityList.GetClientEntityFromHandle((IntPtr)(*(uint*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_hViewModel[0]"]))).GetViewModel;
                }
            }
        }

        public bool IsNull
        {
            get
            {
                fixed (void* classPtr = &this)
                {
                    return classPtr == null;
                }
            }
        }
    }
}