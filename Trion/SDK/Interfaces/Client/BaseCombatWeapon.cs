using System;
using System.Runtime.InteropServices;

using Trion.SDK.Enums;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BaseCombatWeapon
    {
        public WeaponId ItemDefinitionIndex
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (WeaponId)(*(short*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iItemDefinitionIndex"]));
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(short*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iItemDefinitionIndex"]) = (short)value;
                }
            }
        }

        public int OriginalOwnerXuidHigh
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_OriginalOwnerXuidHigh"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_OriginalOwnerXuidHigh"]) = value;
                }
            }
        }

        public int OriginalOwnerXuidLow
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_OriginalOwnerXuidLow"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_OriginalOwnerXuidLow"]) = value;
                }
            }
        }

        public float FallBackWear
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(float*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_flFallbackWear"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(float*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_flFallbackWear"]) = value;
                }
            }
        }

        public int AccountId
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iAccountID"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iAccountID"]) = value;
                }
            }
        }

        public QualityId EntityQuality
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (QualityId)(*(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iEntityQuality"]));
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iEntityQuality"]) = (int)value;
                }
            }
        }

        public int ItemIdHigh
        {
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iItemIDHigh"]) = value;
                }
            }
        }

        public int FallBackPaintKit
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_nFallbackPaintKit"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_nFallbackPaintKit"]) = value;
                }
            }
        }

        public int FallBackSeed
        {
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_nFallbackSeed"]) = value;
                }
            }
        }

        public int FallBackStatTrack
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_nFallbackStatTrak"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_nFallbackStatTrak"]) = value;
                }
            }
        }

        public string CustomName
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return Marshal.PtrToStringAnsi((IntPtr)(char*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_szCustomName"]));
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    fixed (char* Name = value)
                    {
                        *(char**)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_szCustomName"]) = Name;
                    }
                }
            }
        }

        public int ModelIndex
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseEntity", "m_nModelIndex"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseEntity", "m_nModelIndex"]) = value;
                }
            }
        }

        public ref BaseViewModel GetViewModel
        {
            get
            {
                fixed (void* classPtr = &this)
                {
                    return ref *(BaseViewModel*)classPtr;
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