using System;

using static Trion.SDK.Interfaces.Client.Entity.Structures.BaseCombatWeapon;

namespace Trion.Client.Configs
{
    [Serializable]
    internal class CSkinChanger
    {
        public bool SkinChangerActive = true;
    }

    [Serializable]
    internal class CSkinChangerWeapon
    {
        public WeaponId WeaponID;

        public int SkinID = 12;
        public float SkinWear;
        public bool StatTrackEnable;
        public uint StatTrack = 0;

        public string WeaponName;

        public QualityId QualityID = QualityId.Normal;

        public CSkinChangerWeapon() : this(WeaponId.None) { }

        public CSkinChangerWeapon(WeaponId WeaponID) => this.WeaponID = WeaponID;
    }
}