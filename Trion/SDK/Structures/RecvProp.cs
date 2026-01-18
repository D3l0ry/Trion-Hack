using Trion.SDK.Enums;

namespace Trion.SDK.Structures
{
    public unsafe struct RecvProp
    {
        public char* name;
        public SendPropType type;
        readonly int flags;
        readonly int stringBufferSize;
        readonly int insideArray;
        readonly void* extraData;
        public RecvProp* arrayProp;
        readonly void* arrayLengthProxy;
        public void* proxy;
        readonly void* dataTableProxy;
        public RecvTable* dataTable;
        public int offset;
        readonly int elementStride;
        readonly int elementCount;
        readonly char* parentArrayPropName;
    }
}