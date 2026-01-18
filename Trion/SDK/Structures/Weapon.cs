using System;

using Trion.SDK.Enums;

namespace Trion.SDK.Structures
{
    internal static class Weapon
    {
        public static readonly Random Random = new Random();

        public static int GetRandomInt(int min, int max) => Random.Next() % (max - min + 1) + min;

        public static uint GetWeaponId(this WeaponId weaponId)
        {
            switch (weaponId)
            {
                default:
                return 0;

                case WeaponId.Glock:
                return 1;
                case WeaponId.Hkp2000:
                return 2;
                case WeaponId.Usp_s:
                return 3;
                case WeaponId.Elite:
                return 4;
                case WeaponId.P250:
                return 5;
                case WeaponId.Tec9:
                return 6;
                case WeaponId.Fiveseven:
                return 7;
                case WeaponId.Cz75a:
                return 8;
                case WeaponId.Deagle:
                return 9;
                case WeaponId.Revolver:
                return 10;

                case WeaponId.Nova:
                return 11;
                case WeaponId.Xm1014:
                return 12;
                case WeaponId.Sawedoff:
                return 13;
                case WeaponId.Mag7:
                return 14;
                case WeaponId.M249:
                return 15;
                case WeaponId.Negev:
                return 16;

                case WeaponId.Mac10:
                return 17;
                case WeaponId.Mp9:
                return 18;
                case WeaponId.Mp7:
                return 19;
                case WeaponId.Mp5sd:
                return 20;
                case WeaponId.Ump45:
                return 21;
                case WeaponId.P90:
                return 22;
                case WeaponId.Bizon:
                return 23;

                case WeaponId.GalilAr:
                return 24;
                case WeaponId.Famas:
                return 25;
                case WeaponId.Ak47:
                return 26;
                case WeaponId.M4A1:
                return 27;
                case WeaponId.M4a1_s:
                return 28;
                case WeaponId.Ssg08:
                return 29;
                case WeaponId.Sg553:
                return 30;
                case WeaponId.Aug:
                return 31;
                case WeaponId.Awp:
                return 32;
                case WeaponId.G3SG1:
                return 33;
                case WeaponId.Scar20:
                return 34;

                case WeaponId.Knife_CT:
                return 35;
                case WeaponId.Knife_T:
                return 35;
                case WeaponId.Karambit:
                return 35;
                case WeaponId.Gut:
                return 35;
                case WeaponId.Flip:
                return 35;
                case WeaponId.Bayonet:
                return 35;
                case WeaponId.M9Bayonet:
                return 35;
                case WeaponId.Huntsman:
                return 35;
                case WeaponId.Butterfly:
                return 35;
                case WeaponId.Falchion:
                return 35;
                case WeaponId.Daggers:
                return 35;
                case WeaponId.Bowie:
                return 35;
                case WeaponId.Talon:
                return 35;
                case WeaponId.Stiletto:
                return 35;
                case WeaponId.Navaja:
                return 35;
                case WeaponId.Ursus:
                return 35;
                case WeaponId.CSS:
                return 35;
                case WeaponId.Cord:
                return 35;
                case WeaponId.Canis:
                return 35;
                case WeaponId.OutDoor:
                return 35;
                case WeaponId.Skeleton:
                return 35;
            }
        }

        public static uint GetKnifeId(this WeaponId weaponId)
        {
            switch (weaponId)
            {
                default:
                return 0;

                case WeaponId.Knife_CT:
                return 35;
                case WeaponId.Knife_T:
                return 35;
                case WeaponId.Karambit:
                return 35;
                case WeaponId.Gut:
                return 35;
                case WeaponId.Flip:
                return 35;
                case WeaponId.Bayonet:
                return 35;
                case WeaponId.M9Bayonet:
                return 35;
                case WeaponId.Huntsman:
                return 35;
                case WeaponId.Butterfly:
                return 35;
                case WeaponId.Falchion:
                return 35;
                case WeaponId.Daggers:
                return 35;
                case WeaponId.Bowie:
                return 35;
                case WeaponId.Talon:
                return 35;
                case WeaponId.Stiletto:
                return 35;
                case WeaponId.Navaja:
                return 35;
                case WeaponId.Ursus:
                return 35;
                case WeaponId.CSS:
                return 35;
                case WeaponId.Cord:
                return 35;
                case WeaponId.Canis:
                return 35;
                case WeaponId.OutDoor:
                return 35;
                case WeaponId.Skeleton:
                return 35;
            }
        }

        public static bool IsKnife(this WeaponId weaponId)
        {
            switch (weaponId)
            {
                default:
                return false;

                case WeaponId.Knife_CT:
                case WeaponId.Knife_T:
                case WeaponId.Karambit:
                case WeaponId.Gut:
                case WeaponId.Flip:
                case WeaponId.Bayonet:
                case WeaponId.M9Bayonet:
                case WeaponId.Huntsman:
                case WeaponId.Butterfly:
                case WeaponId.Falchion:
                case WeaponId.Daggers:
                case WeaponId.Bowie:
                case WeaponId.Talon:
                case WeaponId.Stiletto:
                case WeaponId.Navaja:
                case WeaponId.Ursus:
                case WeaponId.CSS:
                case WeaponId.Cord:
                case WeaponId.Canis:
                case WeaponId.OutDoor:
                case WeaponId.Skeleton:
                return true;
            }
        }
    }
}