using System;
using System.Runtime.InteropServices;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BaseCombatWeapon
    {
        #region Enums
        public enum WeaponId : short
        {
            None,
            Deagle = 1,
            Elite,
            Fiveseven,
            Glock,
            Ak47 = 7,
            Aug,
            Awp,
            Famas,
            G3SG1,
            GalilAr = 13,
            M249,
            M4A1 = 16,
            Mac10,
            P90 = 19,
            Mp5sd = 23,
            Ump45,
            Xm1014,
            Bizon,
            Mag7,
            Negev,
            Sawedoff,
            Tec9,
            Taser,
            Hkp2000,
            Mp7,
            Mp9,
            Nova,
            P250,
            Scar20 = 38,
            Sg553,
            Ssg08,
            GoldenKnife,
            Knife_CT = 42,
            Knife_T = 59,
            M4a1_s = 60,
            Usp_s,
            Cz75a = 63,
            Revolver,
            GhostKnife = 80,

            Bayonet = 500,
            CSS = 503,
            Flip = 505,
            Gut,
            Karambit,
            M9Bayonet,
            Huntsman,
            Falchion = 512,
            Bowie = 514,
            Butterfly,
            Daggers,
            Cord,
            Canis,
            Ursus = 519,
            Navaja,
            OutDoor,
            Stiletto = 522,
            Talon,
            Skeleton = 525,

            GloveStuddedBloodhound = 5027,
            GloveT,
            GloveCT,
            GloveSporty,
            GloveSlick,
            GloveLeatherWrap,
            GloveMotorcycle,
            GloveSpecialist,
            GloveHydra
        }

        public enum QualityId : int
        {
            Normal,
            Genuine,
            Vintage,
            Unusual,
            Community = 5,
            Developer,
            SelfMade,
            Customized,
            Strange,
            Completed = 11,
            Tournament
        }
        #endregion



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
                    *(short*)((uint)Class + Interface.NetVar["DT_BaseCombatWeapon", "m_iItemDefinitionIndex"]) = (short)value;
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
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iAccountID"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iAccountID"]) = value;
                }
            }
        }

        public QualityId EntityQuality
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (QualityId)(*(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iEntityQuality"]));
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iEntityQuality"]) = (int)value;
                }
            }
        }

        public int ItemIdHigh
        {
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_iItemIDHigh"]) = value;
                }
            }
        }

        public int FallBackPaintKit
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_nFallbackPaintKit"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseAttributableItem", "m_nFallbackPaintKit"]) = value;
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
    }
}