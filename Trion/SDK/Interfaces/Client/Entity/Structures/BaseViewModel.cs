using System;
using System.Runtime.InteropServices;

using Trion.SDK.VirtualMemory;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BaseViewModel
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetModelIndexDelegate(void* Class, int Index);

        public void SetModelIndex(int Index)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<SetModelIndexDelegate>(Class, 75)(Class, Index);
            }
        }

        public ref IClientEntity GetOwner
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return ref Interface.ClientEntityList.GetClientEntityFromHandle(MemoryManager.Read<IntPtr>(Class, Interface.NetVar["DT_BaseViewModel", "m_hOwner"]));
                }
            }
        }

        public int ModelIndex
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return MemoryManager.Read<int>(Class, Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    MemoryManager.Write(Class, Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"], value);
                }
            }
        }
    }
}