using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.VirtualMemory;

namespace Trion.SDK.Interfaces.Client.Entity
{
    internal unsafe struct IClientEntity
    {
        public ref IClientNetworkable GetClientNetworkable
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref MemoryManager.Read<IClientNetworkable>(Class, 8);
                }
            }
        }

        public ref BasePlayer GetPlayer
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref MemoryManager.Read<BasePlayer>(Class);
                }
            }
        }

        public ref BaseCombatWeapon GetWeapon
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref MemoryManager.Read<BaseCombatWeapon>(Class);
                }
            }
        }

        public ref BaseViewModel GetViewModel
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref MemoryManager.Read<BaseViewModel>(Class);
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