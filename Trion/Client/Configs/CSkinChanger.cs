using System;

using static Trion.SDK.Interfaces.Client.Entity.Structures.BaseCombatWeapon;

namespace Trion.Client.Configs
{
    [Serializable]
    internal class CSkinChanger
    {
        public bool SkinChangerActive;
    }

    [Serializable]
    internal class CSkinChangerWeapon
    {
        public WeaponId WeaponID;

        public int SkinID;
        public float SkinWear;
        public bool StatTrackEnable;
        public int StatTrack;

        public string WeaponName;

        public QualityId QualityID;

        public CSkinChangerWeapon(WeaponId weaponID, int skinID = 0, float skinWear = 0, bool statTrackEnable = false, int statTrack = 0, string weaponName = null, QualityId qualityID = QualityId.Normal)
        {
            WeaponID = weaponID;
            SkinID = skinID;
            SkinWear = skinWear;
            StatTrackEnable = statTrackEnable;
            StatTrack = statTrack;
            WeaponName = weaponName;
            QualityID = qualityID;
        }
    }
}