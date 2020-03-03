using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity.Structures
{
    internal unsafe struct BaseViewModel
    {
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetModelIndexDelegate(void* Class, int Index);
        #endregion

        #region Virtual
        public void SetModelIndex(int Index)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<SetModelIndexDelegate>(Class, 75)(Class, Index);
            }
        }
        #endregion

        public IClientEntity* GetOwner
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return Interface.ClientEntityList.GetClientEntityFromHandle(*(int**)((uint)Class + Interface.NetVar["DT_BaseViewModel", "m_hOwner"]));
                }
            }
        }

        public int ModelIndex
        {
            get
            {
                fixed (void* Class = &this)
                {
                    return *(int*)((uint)Class + Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"]);
                }
            }
            set
            {
                fixed (void* Class = &this)
                {
                    *(int*)((uint)Class + Interface.NetVar["DT_BaseViewModel", "m_nModelIndex"]) = value;
                }
            }
        }
    }
}