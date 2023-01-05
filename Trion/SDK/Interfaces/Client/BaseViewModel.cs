using System;
using System.Runtime.InteropServices;

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
                    return ref Interface.ClientEntityList.GetClientEntityFromHandle((IntPtr)(*(int*)((IntPtr)Class + Interface.NetVar["DT_BaseViewModel", "m_hOwner"])));
                }
            }
        }

        public int ModelIndex
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((IntPtr)Class + Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((IntPtr)Class + Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"]) = value;
                }
            }
        }
    }
}