using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Trion.Client.Configs;
using Trion.SDK.Enums;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Structures;

namespace Trion.Modules
{
    internal unsafe class SkinChanger
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void MakeGloveDelegate(int entry, int serial);

        private static readonly Dictionary<WeaponId, string> KnifeModel = new Dictionary<WeaponId, string>()
        {
            [WeaponId.Bayonet] = "models/weapons/v_knife_bayonet.mdl",
            [WeaponId.M9Bayonet] = "models/weapons/v_knife_m9_bay.mdl",
            [WeaponId.Bowie] = "models/weapons/v_knife_survival_bowie.mdl",
            [WeaponId.Butterfly] = "models/weapons/v_knife_butterfly.mdl",
            [WeaponId.Canis] = "models/weapons/v_knife_canis.mdl",
            [WeaponId.Cord] = "models/weapons/v_knife_cord.mdl",
            [WeaponId.CSS] = "models/weapons/v_knife_css.mdl",
            [WeaponId.Daggers] = "models/weapons/v_knife_push.mdl",
            [WeaponId.Falchion] = "models/weapons/v_knife_falchion_advanced.mdl",
            [WeaponId.Flip] = "models/weapons/v_knife_flip.mdl",
            [WeaponId.Gut] = "models/weapons/v_knife_gut.mdl",
            [WeaponId.Huntsman] = "models/weapons/v_knife_tactical.mdl",
            [WeaponId.Karambit] = "models/weapons/v_knife_karam.mdl",
            [WeaponId.Knife_CT] = "models/weapons/v_knife_default_ct.mdl",
            [WeaponId.Knife_T] = "models/weapons/v_knife_default_t.mdl",
            [WeaponId.Navaja] = "models/weapons/v_knife_gypsy_jackknife.mdl",
            [WeaponId.OutDoor] = "models/weapons/v_knife_outdoor.mdl",
            [WeaponId.Skeleton] = "models/weapons/v_knife_skeleton.mdl",
            [WeaponId.Stiletto] = "models/weapons/v_knife_stiletto.mdl",
            [WeaponId.Talon] = "models/weapons/v_knife_widowmaker.mdl",
            [WeaponId.Ursus] = "models/weapons/v_knife_ursus.mdl"
        };
        private static readonly Dictionary<WeaponId, string> GloveModel = new Dictionary<WeaponId, string>()
        {
            [WeaponId.GloveStuddedBloodhound] = "models/weapons/v_models/arms/glove_bloodhound/v_glove_bloodhound.mdl",
            [WeaponId.GloveT] = "models/weapons/v_models/arms/glove_fingerless/v_glove_fingerless.mdl",
            [WeaponId.GloveCT] = "models/weapons/v_models/arms/glove_hardknuckle/v_glove_hardknuckle.mdl",
            [WeaponId.GloveSporty] = "models/weapons/v_models/arms/glove_sporty/v_glove_sporty.mdl",
            [WeaponId.GloveSlick] = "models/weapons/v_models/arms/glove_slick/v_glove_slick.mdl",
            [WeaponId.GloveLeatherWrap] = "models/weapons/v_models/arms/glove_handwrap_leathery/v_glove_handwrap_leathery.mdl",
            [WeaponId.GloveMotorcycle] = "models/weapons/v_models/arms/glove_motorcycle/v_glove_motorcycle.mdl",
            [WeaponId.GloveSpecialist] = "models/weapons/v_models/arms/glove_specialist/v_glove_specialist.mdl",
            [WeaponId.GloveHydra] = "models/weapons/v_models/arms/glove_bloodhound/v_glove_bloodhound_hydra.mdl",
        };
        private static Dictionary<string, string> KillIcon(WeaponId Index) => new Dictionary<string, string>()
        {
            ["knife"] = KnifeIcon[Index],
            ["knife_t"] = KnifeIcon[Index]
        };
        private static readonly Dictionary<WeaponId, string> KnifeIcon = new Dictionary<WeaponId, string>()
        {
            [WeaponId.Bayonet] = "bayonet",
            [WeaponId.M9Bayonet] = "knife_m9_bayonet",
            [WeaponId.Bowie] = "knife_survival_bowie",
            [WeaponId.Butterfly] = "knife_butterfly",
            [WeaponId.Canis] = "knife_canis",
            [WeaponId.Cord] = "knife_cord",
            [WeaponId.CSS] = "knife_css",
            [WeaponId.Daggers] = "knife_push",
            [WeaponId.Falchion] = "knife_falchion",
            [WeaponId.Flip] = "knife_flip",
            [WeaponId.Gut] = "knife_gut",
            [WeaponId.Huntsman] = "knife_tactical",
            [WeaponId.Karambit] = "knife_karambit",
            [WeaponId.Knife_CT] = "knife",
            [WeaponId.Knife_T] = "knife_t",
            [WeaponId.Navaja] = "knife_gypsy_jackknife",
            [WeaponId.OutDoor] = "knife_outdoor",
            [WeaponId.Skeleton] = "knife_skeleton",
            [WeaponId.Stiletto] = "knife_stiletto",
            [WeaponId.Talon] = "knife_widowmaker",
            [WeaponId.Ursus] = "knife_ursus"
        };

        public enum AnimationSequence : int
        {
            SEQUENCE_DEFAULT_DRAW = 0,
            SEQUENCE_DEFAULT_IDLE1 = 1,
            SEQUENCE_DEFAULT_IDLE2 = 2,
            SEQUENCE_DEFAULT_LIGHT_MISS1 = 3,
            SEQUENCE_DEFAULT_LIGHT_MISS2 = 4,
            SEQUENCE_DEFAULT_HEAVY_MISS1 = 9,
            SEQUENCE_DEFAULT_HEAVY_HIT1 = 10,
            SEQUENCE_DEFAULT_HEAVY_BACKSTAB = 11,
            SEQUENCE_DEFAULT_LOOKAT01 = 12,

            SEQUENCE_BUTTERFLY_DRAW = 0,
            SEQUENCE_BUTTERFLY_DRAW2 = 1,
            SEQUENCE_BUTTERFLY_LOOKAT01 = 13,
            SEQUENCE_BUTTERFLY_LOOKAT03 = 15,

            SEQUENCE_FALCHION_IDLE1 = 1,
            SEQUENCE_FALCHION_HEAVY_MISS1 = 8,
            SEQUENCE_FALCHION_HEAVY_MISS1_NOFLIP = 9,
            SEQUENCE_FALCHION_LOOKAT01 = 12,
            SEQUENCE_FALCHION_LOOKAT02 = 13,

            SEQUENCE_CSS_LOOKAT01 = 14,
            SEQUENCE_CSS_LOOKAT02 = 15,

            SEQUENCE_DAGGERS_IDLE1 = 1,
            SEQUENCE_DAGGERS_LIGHT_MISS1 = 2,
            SEQUENCE_DAGGERS_LIGHT_MISS5 = 6,
            SEQUENCE_DAGGERS_HEAVY_MISS2 = 11,
            SEQUENCE_DAGGERS_HEAVY_MISS1 = 12,

            SEQUENCE_BOWIE_IDLE1 = 1,
        };

        public static void OnFrameStage(BasePlayer* localPlayer)
        {
            if (!localPlayer->IsAlive)
            {
                return;
            }

            Interface.VEngineClient.GetPlayerInfo(Interface.VEngineClient.GetLocalPlayer, out PlayerInfo PlayerInfo);

            for (int Index = 0; Index < 8; Index++)
            {
                BaseCombatWeapon* Weapon = localPlayer->GetMyWeapons(Index);

                if (Weapon->IsNull)
                {
                    continue;
                }

                if (Weapon->OriginalOwnerXuidLow != PlayerInfo.m_nXuidLow)
                {
                    continue;
                }

                if (Weapon->OriginalOwnerXuidHigh != PlayerInfo.m_nXuidHigh)
                {
                    continue;
                }

                CSkinChangerWeapon WeaponId = ConfigManager.CSkinChangerWeapons[Weapon->ItemDefinitionIndex.GetWeaponId()];

                ApplyWeapon(Weapon, WeaponId, PlayerInfo);
            }

            //ApplyKnife(localPlayer, ref ConfigManager.CSkinChangerWeapons[35]);

            //ApplyWearable(localPlayer, ref ConfigManager.CSkinChangerWeapons[36], PlayerInfo);
        }

        private static void ApplyWeapon(BaseCombatWeapon* weaponPtr, CSkinChangerWeapon configWeapon, PlayerInfo playerInfo)
        {
            if (configWeapon.WeaponID == WeaponId.None)
            {
                return;
            }

            weaponPtr->ItemIdHigh = -1;

            weaponPtr->AccountId = playerInfo.m_nXuidLow;

            if (!string.IsNullOrWhiteSpace(configWeapon.WeaponName))
            {
                weaponPtr->CustomName = configWeapon.WeaponName;
            }

            if (configWeapon.StatTrackEnable && configWeapon.StatTrack >= 0)
            {
                weaponPtr->EntityQuality = QualityId.Strange;
                weaponPtr->FallBackStatTrack = configWeapon.StatTrack;
            }

            weaponPtr->FallBackPaintKit = configWeapon.SkinID;

            WeaponId WeaponIndex = weaponPtr->ItemDefinitionIndex;

            if (WeaponIndex.IsKnife() && WeaponIndex != configWeapon.WeaponID)
            {
                weaponPtr->ItemDefinitionIndex = configWeapon.WeaponID;
                weaponPtr->GetViewModel.SetModelIndex(Interface.ModelInfoClient.GetModelIndex(KnifeModel[weaponPtr->ItemDefinitionIndex]));
            }
        }

        private static void ApplyKnife(BasePlayer* localPlayer, ref CSkinChangerWeapon configWeapon)
        {
            if (configWeapon.WeaponID == WeaponId.None)
            {
                return;
            }

            WeaponId ActiveIndex = localPlayer->GetActiveWeapon.ItemDefinitionIndex;

            if (ActiveIndex.IsKnife())
            {
                localPlayer->GetViewModel->SetModelIndex(Interface.ModelInfoClient.GetModelIndex(KnifeModel[ActiveIndex]));
            }
        }

        private static void ApplyWearable(BasePlayer* localPlayer, ref CSkinChangerWeapon configWeapon, PlayerInfo playerInfo)
        {
            uint* wearables = localPlayer->GetMyWearables;

            if (wearables == null)
            {
                return;
            }

            ClientClass* Class = Interface.BaseClientDLL.GetAllClasses();

            if (Class == null)
            {
                return;
            }

            int serial = Weapon.Random.Next() % 0x1000;
            int entry = Interface.ClientEntityList.GetHighestEntityIndex + 1;

            if (entry == 0)
            {
                return;
            }

            if (Interface.ClientEntityList.GetClientEntity((int)(wearables[0] & 0xFFF)).IsNull)
            {
                while (Class != null)
                {
                    if (Class->ClassId == ClassId.CEconWearable)
                    {
                        break;
                    }

                    Class = Class->Next;
                }

                Marshal.GetDelegateForFunctionPointer<MakeGloveDelegate>((IntPtr)Class->CreateFunction)(entry, serial);

                wearables[0] = (uint)(entry | (serial << 16));
            }

            ref IClientEntity clientEntity = ref Interface.ClientEntityList.GetClientEntityFromHandle((IntPtr)wearables[0]);
            BaseCombatWeapon* glove = clientEntity.GetWeapon;

            if (glove->IsNull)
            {
                return;
            }

            if (glove->ItemDefinitionIndex != configWeapon.WeaponID)
            {
                glove->ItemIdHigh = -1;
                glove->AccountId = playerInfo.m_nXuidLow;

                glove->ItemDefinitionIndex = configWeapon.WeaponID;
                BaseViewModel* viewModel = clientEntity.GetViewModel;

                viewModel->SetModelIndex(Interface.ModelInfoClient.GetModelIndex(GloveModel[glove->ItemDefinitionIndex]));

                glove->EntityQuality = QualityId.Normal;
                glove->FallBackPaintKit = configWeapon.SkinID;

                clientEntity.GetClientNetworkable->PreDataUpdate(0);
            }
        }

        public static void ApplyKillIcon(ref IGameEventManager.GameEvent gameEvent, BasePlayer* localPlayer)
        {
            string Weapon = gameEvent.GetString("weapon");

            WeaponId WeaponIndex = localPlayer->GetActiveWeapon.ItemDefinitionIndex;

            if (WeaponIndex.IsKnife())
            {
                foreach (KeyValuePair<string, string> Icon in KillIcon(WeaponIndex))
                {
                    if (Weapon == Icon.Key)
                    {
                        gameEvent.SetString("weapon", Icon.Value);

                        break;
                    }
                }
            }
        }

        public static void ApplyStatTrack(ref IGameEventManager.GameEvent gameEvent, BasePlayer* localPlayer)
        {
            ref BaseCombatWeapon WeaponIndex = ref localPlayer->GetActiveWeapon;
            uint WeaponId = WeaponIndex.ItemDefinitionIndex.GetWeaponId();

            fixed (void* weaponPtr = &WeaponIndex)
            {
                IClientNetworkable* ClientNetworkable = ((IClientEntity*)weaponPtr)->GetClientNetworkable;

                ref CSkinChangerWeapon Weapon = ref ConfigManager.CSkinChangerWeapons[WeaponId];

                if (Weapon.StatTrackEnable)
                {
                    WeaponIndex.FallBackStatTrack = ++Weapon.StatTrack;

                    ClientNetworkable->PostDataUpdate(0);
                }
            }
        }

        public static int GetSequence(string modelName, int sequence)
        {
            if (modelName == "models/weapons/v_knife_butterfly.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_BUTTERFLY_DRAW, (int)AnimationSequence.SEQUENCE_BUTTERFLY_DRAW2);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT01, (int)AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT03);
                    default:
                    return sequence + 1;
                }
            }
            else if (modelName == "models/weapons/v_knife_falchion_advanced.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                    return (int)AnimationSequence.SEQUENCE_FALCHION_IDLE1;
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_HEAVY_MISS1:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_FALCHION_HEAVY_MISS1, (int)AnimationSequence.SEQUENCE_FALCHION_HEAVY_MISS1_NOFLIP);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_FALCHION_LOOKAT01, (int)AnimationSequence.SEQUENCE_FALCHION_LOOKAT02);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                    return sequence;
                    default:
                    return sequence - 1;

                }
            }
            else if (modelName == "models/weapons/v_knife_css.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_CSS_LOOKAT01, (int)AnimationSequence.SEQUENCE_CSS_LOOKAT02);
                }
            }
            else if (modelName == "models/weapons/v_knife_push.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                    return (int)AnimationSequence.SEQUENCE_DAGGERS_IDLE1;
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LIGHT_MISS1:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LIGHT_MISS2:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_DAGGERS_LIGHT_MISS1, (int)AnimationSequence.SEQUENCE_DAGGERS_LIGHT_MISS5);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_HEAVY_MISS1:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_DAGGERS_HEAVY_MISS2, (int)AnimationSequence.SEQUENCE_DAGGERS_HEAVY_MISS1);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_HEAVY_HIT1:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_HEAVY_BACKSTAB:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return sequence + 3;
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                    return sequence;
                    default:
                    return sequence + 2;
                }
            }
            else if (modelName == "models/weapons/v_knife_survival_bowie.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE1:
                    return sequence;
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_IDLE2:
                    return (int)AnimationSequence.SEQUENCE_BOWIE_IDLE1;
                    default:
                    return sequence - 1;
                }
            }
            else if (modelName == "models/weapons/v_knife_ursus.mdl" || modelName == "models/weapons/v_knife_cord.mdl" || modelName == "models/weapons/v_knife_canis.mdl" || modelName == "models/weapons/v_knife_outdoor.mdl" || modelName == "models/weapons/v_knife_skeleton.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_DRAW:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_BUTTERFLY_DRAW, (int)AnimationSequence.SEQUENCE_BUTTERFLY_DRAW2);
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt((int)AnimationSequence.SEQUENCE_BUTTERFLY_LOOKAT01, 14);
                    default:
                    return sequence + 1;
                }
            }
            else if (modelName == "models/weapons/v_knife_stiletto.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt(12, 13);
                }
            }
            else if (modelName == "models/weapons/v_knife_widowmaker.mdl")
            {
                switch (sequence)
                {
                    case (int)AnimationSequence.SEQUENCE_DEFAULT_LOOKAT01:
                    return Weapon.GetRandomInt(14, 15);
                }
            }

            return sequence;
        }
    }
}