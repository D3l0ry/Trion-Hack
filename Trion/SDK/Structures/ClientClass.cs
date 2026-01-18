using Trion.SDK.Enums;

namespace Trion.SDK.Structures
{
    public unsafe struct ClientClass
    {
        public void* CreateFunction;
        public void* CreateEventFunction;
        public char* NetworkName;
        public RecvTable* RecVTable;
        public ClientClass* Next;
        public ClassId ClassId;
    }
}