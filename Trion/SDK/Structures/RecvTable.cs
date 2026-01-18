namespace Trion.SDK.Structures
{
    public unsafe struct RecvTable
    {
        public RecvProp* props;
        public int propCount;
        readonly void* decoder;
        public char* netTableName;
        readonly bool isInitialized;
        readonly bool isInMainList;
    };
}