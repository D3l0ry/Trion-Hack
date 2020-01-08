using System.Runtime.InteropServices;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity
{
    internal unsafe struct IClientEntity
    {
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsPlayerDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsWeaponDelegate(void* Class);
        #endregion

        #region Inheritance
        public IClientNetworkable* GetClientNetworkable
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return (IClientNetworkable*)((uint)Class + 8);
                }
            }
        }
        #endregion

        #region Virtual Methods
        public bool IsPlayer
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return VMTable.CallVirtualFunction<IsPlayerDelegate>(Class, 157)(Class);
                }
            }
        }

        public bool IsWeapon
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return VMTable.CallVirtualFunction<IsWeaponDelegate>(Class, 165)(Class);
                }
            }
        }
        #endregion

        #region Structures
        public BasePlayer* GetPlayer
        {
            get
            {
                fixed(void* Class = &this)
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
        #endregion
    }
}