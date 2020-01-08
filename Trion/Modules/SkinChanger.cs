using System.Collections.Generic;

using Trion.Client.Configs;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;
using Trion.SDK.Interfaces.Client.Entity;
using Trion.SDK.Interfaces.Client.Entity.Structures;
using Trion.SDK.Interfaces.Engine;
using Trion.SDK.Structures.Modules;
using static Trion.SDK.Interfaces.Client.Entity.Structures.BaseCombatWeapon;
using WeaponIndex = Trion.SDK.Interfaces.Client.Entity.Structures.BaseCombatWeapon.WeaponId;

namespace Trion.Modules
{
    internal unsafe class SkinChanger
    {
        private static readonly Dictionary<WeaponIndex, string> WeaponModel = new Dictionary<WeaponIndex, string>()
        {
            [WeaponIndex.Bayonet] = "models/weapons/v_knife_bayonet.mdl",
            [WeaponIndex.M9Bayonet] = "models/weapons/v_knife_m9_bay.mdl",
            [WeaponIndex.Bowie] = "models/weapons/v_knife_survival_bowie.mdl",
            [WeaponIndex.Butterfly] = "models/weapons/v_knife_butterfly.mdl",
            [WeaponIndex.Canis] = "models/weapons/v_knife_canis.mdl",
            [WeaponIndex.Cord] = "models/weapons/v_knife_cord.mdl",
            [WeaponIndex.CSS] = "models/weapons/v_knife_css.mdl",
            [WeaponIndex.Daggers] = "models/weapons/v_knife_push.mdl",
            [WeaponIndex.Falchion] = "models/weapons/v_knife_falchion_advanced.mdl",
            [WeaponIndex.Flip] = "models/weapons/v_knife_flip.mdl",
            [WeaponIndex.Gut] = "models/weapons/v_knife_gut.mdl",
            [WeaponIndex.Huntsman] = "models/weapons/v_knife_tactical.mdl",
            [WeaponIndex.Karambit] = "models/weapons/v_knife_karam.mdl",
            [WeaponIndex.Knife_CT] = "models/weapons/v_knife_default_ct.mdl",
            [WeaponIndex.Knife_T] = "models/weapons/v_knife_default_t.mdl",
            [WeaponIndex.Navaja] = "models/weapons/v_knife_gypsy_jackknife.mdl",
            [WeaponIndex.OutDoor] = "models/weapons/v_knife_outdoor.mdl",
            [WeaponIndex.Skeleton] = "models/weapons/v_knife_skeleton.mdl",
            [WeaponIndex.Stiletto] = "models/weapons/v_knife_stiletto.mdl",
            [WeaponIndex.Talon] = "models/weapons/v_knife_widowmaker.mdl",
            [WeaponIndex.Ursus] = "models/weapons/v_knife_ursus.mdl"
        };
        private static Dictionary<string, string> KillIcon(WeaponIndex Index) => new Dictionary<string, string>()
        {
            ["knife"] = KnifeIcon[Index],
            ["knife_t"] = KnifeIcon[Index]
        };
        private static readonly Dictionary<WeaponIndex, string> KnifeIcon = new Dictionary<WeaponIndex, string>()
        {
            [WeaponIndex.Bayonet] = "bayonet",
            [WeaponIndex.M9Bayonet] = "knife_m9_bayonet",
            [WeaponIndex.Bowie] = "knife_survival_bowie",
            [WeaponIndex.Butterfly] = "knife_butterfly",
            [WeaponIndex.Canis] = "knife_canis",
            [WeaponIndex.Cord] = "knife_cord",
            [WeaponIndex.CSS] = "knife_css",
            [WeaponIndex.Daggers] = "knife_push",
            [WeaponIndex.Falchion] = "knife_falchion",
            [WeaponIndex.Flip] = "knife_flip",
            [WeaponIndex.Gut] = "knife_gut",
            [WeaponIndex.Huntsman] = "knife_tactical",
            [WeaponIndex.Karambit] = "knife_karambit",
            [WeaponIndex.Knife_CT] = "knife",
            [WeaponIndex.Knife_T] = "knife_t",
            [WeaponIndex.Navaja] = "knife_gypsy_jackknife",
            [WeaponIndex.OutDoor] = "knife_outdoor",
            [WeaponIndex.Skeleton] = "knife_skeleton",
            [WeaponIndex.Stiletto] = "knife_stiletto",
            [WeaponIndex.Talon] = "knife_widowmaker",
            [WeaponIndex.Ursus] = "knife_ursus"
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

        public static void OnFrameStage(IBaseClientDLL.FrameStage FrameStage)
        {
            BasePlayer* LocalPlayer = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer;

            if (FrameStage == IBaseClientDLL.FrameStage.NET_UPDATE_POSTDATAUPDATE_START)
            {
                if (!LocalPlayer->IsAlive)
                {
                    return;
                }

                Interface.VEngineClient.GetPlayerInfo(Interface.VEngineClient.GetLocalPlayer, out IVEngineClient.PlayerInfo PlayerInfo);

                for (int Index = 0; Index < 3; Index++)
                {
                    BaseCombatWeapon* Weapon = LocalPlayer->GetMyWeapons(Index);

                    if (Weapon == null)
                    {
                        continue;
                    }

                    if (Weapon->OriginalOwnerXuidLow != PlayerInfo.m_nXuidLow || Weapon->OriginalOwnerXuidHigh != PlayerInfo.m_nXuidHigh)
                    {
                        continue;
                    }

                    ApplyWeapon(LocalPlayer, Weapon, ConfigManager.CSkinChangerWeapons[Weapon->ItemDefinitionIndex.GetWeaponId()], PlayerInfo);

                    ApplyKnife(LocalPlayer, Weapon, ConfigManager.CSkinChangerWeapons[Weapon->ItemDefinitionIndex.GetWeaponId()]);
                }
            }
        }

        private static void ApplyWeapon(BasePlayer* LocalPlayer, BaseCombatWeapon* WeaponPtr, CSkinChangerWeapon Weapon, IVEngineClient.PlayerInfo PlayerInfo)
        {
            if (Weapon.WeaponID != WeaponIndex.None)
            {
                WeaponPtr->AccountId = PlayerInfo.m_nXuidLow;

                if (!string.IsNullOrWhiteSpace(Weapon.WeaponName))
                {
                    WeaponPtr->CustomName = Weapon.WeaponName;
                }
                WeaponPtr->FallBackPaintKit = Weapon.SkinID;

                if (Weapon.StatTrackEnable && Weapon.StatTrack >= 0)
                {
                    WeaponPtr->EntityQuality = QualityId.Strange;
                    WeaponPtr->FallBackStatTrack = (int)Weapon.StatTrack;
                }

                WeaponPtr->ItemIdHigh = -1;
            }
        }

        private static void ApplyKnife(BasePlayer* LocalPlayer, BaseCombatWeapon* WeaponPtr, CSkinChangerWeapon Weapon)
        {
            if (WeaponPtr->ItemDefinitionIndex.IsKnife())
            {
                WeaponPtr->ItemDefinitionIndex = Weapon.WeaponID;
                ((BaseViewModel*)WeaponPtr)->SetModelIndex(Interface.ModelInfoClient.GetModelIndex(WeaponModel[WeaponPtr->ItemDefinitionIndex]));

                if (LocalPlayer->GetActiveWeapon->ItemDefinitionIndex.IsKnife())
                {
                    LocalPlayer->GetViewModel->SetModelIndex(Interface.ModelInfoClient.GetModelIndex(WeaponModel[WeaponPtr->ItemDefinitionIndex]));
                }
            }
        }

        public static void ApplyKillIcon(IGameEventManager.GameEvent* Event)
        {
            int UserId = Event->GetInt("attacker");

            if (UserId == 0)
            {
                return;
            }

            if (Interface.VEngineClient.GetPlayerForUserID(UserId) != Interface.VEngineClient.GetLocalPlayer)
            {
                return;
            }

            string Weapon = Event->GetString("weapon");

            var WeaponIndex = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer->GetActiveWeapon->ItemDefinitionIndex;

            if (WeaponIndex.IsKnife())
            {
                foreach (var Icon in KillIcon(WeaponIndex))
                {
                    if (Weapon.Equals(Icon.Key))
                    {
                        Event->SetString("weapon", Icon.Value);
                        break;
                    }
                }
            }
        }

        public static void UpdateStatTrack(IGameEventManager.GameEvent* Event)
        {
            int UserId = Event->GetInt("attacker");

            if (UserId == 0)
            {
                return;
            }

            if (Interface.VEngineClient.GetPlayerForUserID(UserId) != Interface.VEngineClient.GetLocalPlayer)
            {
                return;
            }

            var WeaponIndex = Interface.ClientEntityList.GetClientEntity(Interface.VEngineClient.GetLocalPlayer)->GetPlayer->GetActiveWeapon;

            if (ConfigManager.CSkinChangerWeapons[WeaponIndex->ItemDefinitionIndex.GetWeaponId()].StatTrackEnable)
            {
                ConfigManager.CSkinChangerWeapons[WeaponIndex->ItemDefinitionIndex.GetWeaponId()].StatTrack++;

                ((IClientEntity*)WeaponIndex)->GetClientNetworkable->PostDataUpdate(0);
                ((IClientEntity*)WeaponIndex)->GetClientNetworkable->OnDataChanged(0);
            }
        }
    }
}