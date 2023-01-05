using System;

using Trion.SDK.Interfaces.Client.Entity.Structures;

namespace Trion.SDK.Interfaces.Client.Entity
{
    internal unsafe struct IClientEntity
    {
        public IClientNetworkable* GetClientNetworkable
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (IClientNetworkable*)((IntPtr)Class + 8);
                }
            }
        }

        public BasePlayer* GetPlayer
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (BasePlayer*)Class;
                }
            }
        }

        public BaseCombatWeapon* GetWeapon
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (BaseCombatWeapon*)Class;
                }
            }
        }

        public BaseViewModel* GetViewModel
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (BaseViewModel*)Class;
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