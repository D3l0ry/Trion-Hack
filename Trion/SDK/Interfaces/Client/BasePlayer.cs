using System;
using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BasePlayer
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsAliveDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate ref BaseCombatWeapon GetActiveWeaponDelegate(void* Class);

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

        public ref BaseCombatWeapon GetActiveWeapon
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref VMTable.CallVirtualFunction<GetActiveWeaponDelegate>(Class, 267)(Class);
                }
            }
        }

        public int GetHealth
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((IntPtr)Class + Interface.NetVar["DT_BasePlayer", "m_iHealth"]);
                }
            }
        }

        public BaseCombatWeapon* GetMyWeapons(int Index)
        {
            fixed (void* Class = &this)
            {
                IntPtr weaponPtr = (IntPtr)((int*)((uint)Class + (Interface.NetVar["DT_BasePlayer", "m_hActiveWeapon"] - 256)))[Index];

                return Interface.ClientEntityList.GetClientEntityFromHandle(weaponPtr).GetWeapon;
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
                    uint viewModelPtr = *(uint*)((IntPtr)Class + Interface.NetVar["DT_BasePlayer", "m_hViewModel[0]"]);

                    return Interface.ClientEntityList.GetClientEntityFromHandle((IntPtr)viewModelPtr).GetViewModel;
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