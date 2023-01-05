using System.Runtime.InteropServices;

using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client.Entity
{
    internal unsafe struct IClientNetworkable
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void OnDataChangedDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PreDataUpdateDelegate(void* Class, int UpdateType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PostDataUpdateDelegate(void* Class, int UpdateType);

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
    }
}