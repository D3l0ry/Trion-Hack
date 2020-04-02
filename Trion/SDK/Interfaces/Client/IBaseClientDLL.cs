using System;
using System.Runtime.InteropServices;

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
           CAI_BaseNPC,
           CAK47,
           CBaseAnimating,
           CBaseAnimatingOverlay,
           CBaseAttributableItem,
           CBaseButton,
           CBaseCombatCharacter,
           CBaseCombatWeapon,
           CBaseCSGrenade,
           CBaseCSGrenadeProjectile,
           CBaseDoor,
           CBaseEntity,
           CBaseFlex,
           CBaseGrenade,
           CBaseParticleEntity,
           CBasePlayer,
           CBasePropDoor,
           CBaseTeamObjectiveResource,
           CBaseTempEntity,
           CBaseToggle,
           CBaseTrigger,
           CBaseViewModel,
           CBaseVPhysicsTrigger,
           CBaseWeaponWorldModel,
           CBeam,
           CBeamSpotlight,
           CBoneFollower,
           CBRC4Target,
           CBreachCharge,
           CBreachChargeProjectile,
           CBreakableProp,
           CBreakableSurface,
           CBumpMine,
           CBumpMineProjectile,
           CC4,
           CCascadeLight,
           CChicken,
           CColorCorrection,
           CColorCorrectionVolume,
           CCSGameRulesProxy,
           CCSPlayer,
           CCSPlayerResource,
           CCSRagdoll,
           CCSTeam,
           CDangerZone,
           CDangerZoneController,
           CDEagle,
           CDecoyGrenade,
           CDecoyProjectile,
           CDrone,
           CDronegun,
           CDynamicLight,
           CDynamicProp,
           CEconEntity,
           CEconWearable,
           CEmbers,
           CEntityDissolve,
           CEntityFlame,
           CEntityFreezing,
           CEntityParticleTrail,
           CEnvAmbientLight,
           CEnvDetailController,
           CEnvDOFController,
           CEnvGasCanister,
           CEnvParticleScript,
           CEnvProjectedTexture,
           CEnvQuadraticBeam,
           CEnvScreenEffect,
           CEnvScreenOverlay,
           CEnvTonemapController,
           CEnvWind,
           CFEPlayerDecal,
           CFireCrackerBlast,
           CFireSmoke,
           CFireTrail,
           CFish,
           CFists,
           CFlashbang,
           CFogController,
           CFootstepControl,
           CFunc_Dust,
           CFunc_LOD,
           CFuncAreaPortalWindow,
           CFuncBrush,
           CFuncConveyor,
           CFuncLadder,
           CFuncMonitor,
           CFuncMoveLinear,
           CFuncOccluder,
           CFuncReflectiveGlass,
           CFuncRotating,
           CFuncSmokeVolume,
           CFuncTrackTrain,
           CGameRulesProxy,
           CGrassBurn,
           CHandleTest,
           CHEGrenade,
           CHostage,
           CHostageCarriableProp,
           CIncendiaryGrenade,
           CInferno,
           CInfoLadderDismount,
           CInfoMapRegion,
           CInfoOverlayAccessor,
           CItem_Healthshot,
           CItemCash,
           CItemDogtags,
           CKnife,
           CKnifeGG,
           CLightGlow,
           CMaterialModifyControl,
           CMelee,
           CMolotovGrenade,
           CMolotovProjectile,
           CMovieDisplay,
           CParadropChopper,
           CParticleFire,
           CParticlePerformanceMonitor,
           CParticleSystem,
           CPhysBox,
           CPhysBoxMultiplayer,
           CPhysicsProp,
           CPhysicsPropMultiplayer,
           CPhysMagnet,
           CPhysPropAmmoBox,
           CPhysPropLootCrate,
           CPhysPropRadarJammer,
           CPhysPropWeaponUpgrade,
           CPlantedC4,
           CPlasma,
           CPlayerPing,
           CPlayerResource,
           CPointCamera,
           CPointCommentaryNode,
           CPointWorldText,
           CPoseController,
           CPostProcessController,
           CPrecipitation,
           CPrecipitationBlocker,
           CPredictedViewModel,
           CProp_Hallucination,
           CPropCounter,
           CPropDoorRotating,
           CPropJeep,
           CPropVehicleDriveable,
           CRagdollManager,
           CRagdollProp,
           CRagdollPropAttached,
           CRopeKeyframe,
           CSCAR17,
           CSceneEntity,
           CSensorGrenade,
           CSensorGrenadeProjectile,
           CShadowControl,
           CSlideshowDisplay,
           CSmokeGrenade,
           CSmokeGrenadeProjectile,
           CSmokeStack,
           CSnowball,
           CSnowballPile,
           CSnowballProjectile,
           CSpatialEntity,
           CSpotlightEnd,
           CSprite,
           CSpriteOriented,
           CSpriteTrail,
           CStatueProp,
           CSteamJet,
           CSun,
           CSunlightShadowControl,
           CSurvivalSpawnChopper,
           CTablet,
           CTeam,
           CTeamplayRoundBasedRulesProxy,
           CTEArmorRicochet,
           CTEBaseBeam,
           CTEBeamEntPoint,
           CTEBeamEnts,
           CTEBeamFollow,
           CTEBeamLaser,
           CTEBeamPoints,
           CTEBeamRing,
           CTEBeamRingPoint,
           CTEBeamSpline,
           CTEBloodSprite,
           CTEBloodStream,
           CTEBreakModel,
           CTEBSPDecal,
           CTEBubbles,
           CTEBubbleTrail,
           CTEClientProjectile,
           CTEDecal,
           CTEDust,
           CTEDynamicLight,
           CTEEffectDispatch,
           CTEEnergySplash,
           CTEExplosion,
           CTEFireBullets,
           CTEFizz,
           CTEFootprintDecal,
           CTEFoundryHelpers,
           CTEGaussExplosion,
           CTEGlowSprite,
           CTEImpact,
           CTEKillPlayerAttachments,
           CTELargeFunnel,
           CTEMetalSparks,
           CTEMuzzleFlash,
           CTEParticleSystem,
           CTEPhysicsProp,
           CTEPlantBomb,
           CTEPlayerAnimEvent,
           CTEPlayerDecal,
           CTEProjectedDecal,
           CTERadioIcon,
           CTEShatterSurface,
           CTEShowLine,
           CTesla,
           CTESmoke,
           CTESparks,
           CTESprite,
           CTESpriteSpray,
           CTest_ProxyToggle_Networkable,
           CTestTraceline,
           CTEWorldDecal,
           CTriggerPlayerMovement,
           CTriggerSoundOperator,
           CVGuiScreen,
           CVoteController,
           CWaterBullet,
           CWaterLODControl,
           CWeaponAug,
           CWeaponAWP,
           CWeaponBaseItem,
           CWeaponBizon,
           CWeaponCSBase,
           CWeaponCSBaseGun,
           CWeaponCycler,
           CWeaponElite,
           CWeaponFamas,
           CWeaponFiveSeven,
           CWeaponG3SG1,
           CWeaponGalil,
           CWeaponGalilAR,
           CWeaponGlock,
           CWeaponHKP2000,
           CWeaponM249,
           CWeaponM3,
           CWeaponM4A1,
           CWeaponMAC10,
           CWeaponMag7,
           CWeaponMP5Navy,
           CWeaponMP7,
           CWeaponMP9,
           CWeaponNegev,
           CWeaponNOVA,
           CWeaponP228,
           CWeaponP250,
           CWeaponP90,
           CWeaponSawedoff,
           CWeaponSCAR20,
           CWeaponScout,
           CWeaponSG550,
           CWeaponSG552,
           CWeaponSG556,
           CWeaponShield,
           CWeaponSSG08,
           CWeaponTaser,
           CWeaponTec9,
           CWeaponTMP,
           CWeaponUMP45,
           CWeaponUSP,
           CWeaponXM1014,
           CWorld,
           CWorldVguiText,
            DustTrail,
            MovieExplosion,
            ParticleSmokeGrenade,
            RocketTrail,
            SmokeTrail,
            SporeExplosion,
            SporeTrail
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
        private delegate ClientClass* GetAllClassesDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void HudUpdateDelegate(void* Class, bool IsActive);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void FrameStageNotifyDelegate(void* Class, FrameStage FrameStage);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void FrameStageNotifyHookDelegate(FrameStage FrameStage);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool DispatchUserMessageDelegate(void* Class, int TypeMessage, int Argument, int Argument2, void* Data);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void EquipWearableDelegate(void* Entity, void* Local);
        #endregion

        #region Virtual Methods
        public void ShutDown() => CallVirtualFunction<ShutDownDelegate>(4)(this);

        public ClientClass* GetAllClasses() => CallVirtualFunction<GetAllClassesDelegate>(8)(this);

        public void HudUpdate(bool IsActive) => CallVirtualFunction<HudUpdateDelegate>(11)(this, IsActive);

        public void FrameStageNotifyOriginal(FrameStage FrameStage) => CallOriginalFunction<FrameStageNotifyDelegate>(37)(this, FrameStage);

        public bool DispatchUserMessage(int TypeMessage, int Argument, int Argument2, void* Data) => CallVirtualFunction<DispatchUserMessageDelegate>(38)(this, TypeMessage, Argument, Argument2, Data);
        #endregion
    }
}