using System;
using System.Runtime.InteropServices;

using Trion.SDK.Dumpers;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Client
{
    internal unsafe class IBaseClientDLL : VMTable
    {
        #region Initialization
        public IBaseClientDLL(void* Base) : base(Base)
        {
        }

        public IBaseClientDLL(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Enums
        public enum FrameStage : int
        {
            UNDEFINED = -1,
            START,
            NET_UPDATE_START,
            NET_UPDATE_POSTDATAUPDATE_START,
            NET_UPDATE_POSTDATAUPDATE_END,
            NET_UPDATE_END,
            RENDER_START,
            RENDER_END
        };

        public enum ClassId
        {
            BaseCSGrenadeProjectile = 9,
            BreachChargeProjectile = 29,
            BumpMineProjectile = 33,
            C4,
            Chicken = 36,
            CSPlayer = 40,
            CSRagdoll = 42,
            Deagle = 46,
            DecoyProjectile = 48,
            Drone,
            Dronegun,
            EconEntity = 53,
            Weariables = 70,
            Hostage = 97,
            Knife = 107,
            KnifeGG,
            MolotovProjectile = 113,
            PlantedC4 = 128,
            PropDoorRotating = 142,
            SensorGrenadeProjectile = 152,
            SmokeGrenadeProjectile = 156,
            SnowballProjectile = 160,
            Tablet = 171,
            Aug = 231,
            Awp,
            Elite = 238,
            FiveSeven = 240,
            G3sg1,
            Glock = 244,
            P2000,
            P250 = 257,
            Scar20 = 260,
            Sg553 = 264,
            Ssg08 = 266,
            Tec9 = 268
        }

        public enum SendPropType:int
        {
            DPT_Int = 0,
            DPT_Float,
            DPT_Vector,
            DPT_VectorXY,
            DPT_String,
            DPT_Array,
            DPT_DataTable,
            DPT_Int64,
            DPT_NUMSendPropTypes
        };
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Explicit, Size = 24)]
        public struct DVariant
        {
            [FieldOffset(0x0)]
            public float m_Float;
            [FieldOffset(0x0)]
            public int m_Int;
            [FieldOffset(0x0)]
            public char* m_pString;
            [FieldOffset(0x0)]
            public void* m_pData;
            [FieldOffset(0x0)]
            public fixed float Vector[3];
            [FieldOffset(0x0)]
            Int64 m_Int64;

            [FieldOffset(0x8)]
            SendPropType m_Type;
        }

        public struct RecvProxyData
        {
            RecvProp* m_pRecvProp;
            public DVariant m_Value;
            int m_iElement;
            int m_ObjectID;
        };

        public struct RecvProp
        {
            public char* name;
            public SendPropType type;
            int flags;
            int stringBufferSize;
            int insideArray;
            void* extraData;
            public RecvProp* arrayProp;
            void* arrayLengthProxy;
            public void* proxy;
            void* dataTableProxy;
            public RecvTable* dataTable;
            public int offset;
            int elementStride;
            int elementCount;
            char* parentArrayPropName;
        }

        public unsafe struct RecvTable
        {
            public RecvProp* props;
            public int propCount;
            void* decoder;
            public char* netTableName;
            bool isInitialized;
            bool isInMainList;
        };

        public unsafe struct ClientClass
        {
            public void* CreateFunction;
            public void* CreateEventFunction;
            public char* NetworkName;
            public RecvTable* RecVTable;
            public ClientClass* Next;
            public ClassId ClassId;
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void ShutDownDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* GetAllClassesDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void HudUpdateDelegate(void* Class, bool IsActive);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void FrameStageNotifyDelegate(void* Class, FrameStage FrameStage);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void FrameStageNotifyHookDelegate(FrameStage FrameStage);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool DispatchUserMessageDelegate(void* Class, int TypeMessage, int Argument, int Argument2, void* Data);
        #endregion

        #region Virtual Methods
        public void ShutDown() => CallVirtualFunction<ShutDownDelegate>(4)(this);

        public ClientClass* GetAllClasses() => (ClientClass*)CallVirtualFunction<GetAllClassesDelegate>(8)(this);

        public void HudUpdate(bool IsActive) => CallVirtualFunction<HudUpdateDelegate>(11)(this, IsActive);

        public void FrameStageNotifyOriginal(FrameStage FrameStage) => CallOriginalFunction<FrameStageNotifyDelegate>(37)(this, FrameStage);

        public bool DispatchUserMessage(int TypeMessage, int Argument, int Argument2, void* Data) => CallVirtualFunction<DispatchUserMessageDelegate>(38)(this, TypeMessage, Argument, Argument2, Data);
        #endregion

        #region Methods
        public IGlowObjectManager GlowObjectManager => *(uint*)(Interface.ClientAddress + OffsetDumper.GetOnlineOffset["dwGlowObjectManager"]);
        #endregion
    }
}