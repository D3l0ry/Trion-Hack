using System;
using System.Runtime.InteropServices;

using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe ref struct BasePlayer
    {
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsAliveDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate BaseCombatWeapon* GetActiveWeaponDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate Vector3* GetEyePositionDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate BasePlayer* GetObserverTargetDelegate(void* Class);
        #endregion

        #region Enums
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

        public enum LifeState
        {
            Alive = 0,
            KillCam = 1,
            Dead = 2
        }

        public enum VisibleId
        {
            Spotted,
            SpottedByMask
        };
        #endregion

        #region Virtual
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

        public Vector3 GetEyePosition
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *VMTable.CallVirtualFunction<GetEyePositionDelegate>(Class, 284)(Class);
                }
            }
        }

        public BasePlayer* GetObserverTarget
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return VMTable.CallVirtualFunction<GetObserverTargetDelegate>(Class, 294)(Class);
                }
            }
        }
        #endregion

        public Vector3 AimPunchAngle
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_aimPunchAngle"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_aimPunchAngle"]) = value;
                }
            }
        }

        public Vector3 ViewPunchAngle
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_viewPunchAngle"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_viewPunchAngle"]) = value;
                }
            }
        }

        public Vector3 AimPunchAngleVelocity
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_aimPunchAngleVel"]);
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

        public Vector3 GetOrigin
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(Vector3*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_vecOrigin"]);
                }
            }
        }

        public Vector3 GetBonePosition(int Bone)
        {
            Vector3 BonePosition;

            fixed (void* Enemies = &this)
            {
                BonePosition.X = *(float*)(*(int*)((uint)Enemies + (Interface.NetVar["DT_BaseAnimating", "m_nForceBone"] + 28)) + 0x30 * Bone + 0xC);
                BonePosition.Y = *(float*)(*(int*)((uint)Enemies + (Interface.NetVar["DT_BaseAnimating", "m_nForceBone"] + 28)) + 0x30 * Bone + 0x1C);
                BonePosition.Z = *(float*)(*(int*)((uint)Enemies + (Interface.NetVar["DT_BaseAnimating", "m_nForceBone"] + 28)) + 0x30 * Bone + 0x2C);
            }

            return BonePosition;
        }

        public int Body
        {
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAnimating", "m_nBody"]) = value;
                }
            }
        }

        public int GetArmor
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_ArmorValue"]);
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

        public bool IsDefusing
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(bool*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_bIsDefusing"]);
                }
            }
        }

        public bool IsScoped
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(bool*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_bIsScoped"]);
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

        public int CrosshairId
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_bHasDefuser"] + 92);
                }
            }
        }

        public int Spotted
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseEntity", "m_bSpotted"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseEntity", "m_bSpotted"]) = value;
                }
            }
        }

        public int ShotsFired
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_iShotsFired"]);
                }
            }
        }

        public LifeState GetLifeState
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (LifeState)(*(int*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_lifeState"]));
                }
            }
        }

        public string LastPlaceName
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return Marshal.PtrToStringAnsi((IntPtr)(char*)((uint)Class + Interface.NetVar["DT_CSPlayer", "m_szLastPlaceName"]));
                }
            }
        }

        public BaseCombatWeapon* GetMyWeapons(int Index)
        {
            fixed (void* Class = &this)
            {
                return (BaseCombatWeapon*)Interface.ClientEntityList.GetClientEntityFromHandle((void*)((int*)((uint)Class + (Interface.NetVar["DT_BasePlayer", "m_hActiveWeapon"] - 256)))[Index]);
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

        public BaseViewModel* GetViewModel
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (BaseViewModel*)Interface.ClientEntityList.GetClientEntityFromHandle((void*)*(uint*)((uint)Class + Interface.NetVar["DT_BasePlayer", "m_hViewModel[0]"]));
                }
            }
        }
    }
}