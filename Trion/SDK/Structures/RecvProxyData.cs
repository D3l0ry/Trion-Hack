namespace Trion.SDK.Structures
{
    internal unsafe struct RecvProxyData
    {
        readonly RecvProp* m_pRecvProp;
        public DVariant m_Value;
        readonly int m_iElement;
        readonly int m_ObjectID;
    }
}