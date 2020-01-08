using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity
{
    internal unsafe struct IClientNetworkable
    {
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void ReleaseDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetClientClassDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void OnPreDataChangedDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void OnDataChangedDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PreDataUpdateDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PostDataUpdateDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool IsDormantDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int EntityIndexDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetDestroyedOnRecreateEntitiesDelegate(void* Class);
        #endregion

        #region Virtual Methods
        public void Release()
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<ReleaseDelegate>(Class,1)(Class);
            }
        }

        public IBaseClientDLL.ClientClass* GetClientClass
        {
            get
            {
                fixed (void* Class = &this)
                {
                   return (IBaseClientDLL.ClientClass*)VMTable.CallVirtualFunction<GetClientClassDelegate>(Class, 2)(Class);
                }
            }
        }

        public void OnPreDataChanged(int UpdateType)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<OnPreDataChangedDelegate>(Class, 4)(Class,UpdateType);
            }
        }

        public void OnDataChanged(int UpdateType)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<OnDataChangedDelegate>(Class, 5)(Class, UpdateType);
            }
        }

        public void PreDataUpdate(int UpdateType)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<PreDataUpdateDelegate>(Class, 6)(Class, UpdateType);
            }
        }

        public void PostDataUpdate(int UpdateType)
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<PostDataUpdateDelegate>(Class, 7)(Class, UpdateType);
            }
        }

        public bool IsDormant()
        {
            fixed (void* Class = &this)
            {
               return VMTable.CallVirtualFunction<IsDormantDelegate>(Class, 9)(Class);
            }
        }

        public int EntityIndex()
        {
            fixed (void* Class = &this)
            {
                return VMTable.CallVirtualFunction<EntityIndexDelegate>(Class, 10)(Class);
            }
        }

        public void SetDestroyedOnRecreateEntities()
        {
            fixed (void* Class = &this)
            {
                VMTable.CallVirtualFunction<SetDestroyedOnRecreateEntitiesDelegate>(Class, 13)(Class);
            }
        }
        #endregion
    }
}