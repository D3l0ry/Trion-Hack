using System;

using Trion.SDK.Structures.Numerics;
using static Trion.SDK.Interfaces.Client.Entity.Structures.BaseCombatWeapon;

namespace Trion.SDK.Structures.Modules
{
    internal static class Weapon
    {
        #region Variables
        public static readonly Random Random = new Random();
        #endregion

        #region Math
        public static double Rad2Degrees(double Yaw) => Yaw * (180f / Math.PI);

        public static double Degrees2Rad(double Yaw) => Yaw * (Math.PI / 180f);

        public static double LengthSqr(this Vector3 Player) => (Player.X * Player.X) + (Player.Y * Player.Y) + (Player.Z * Player.Z);

        public static int GetRandomInt(int Min, int Max) => Random.Next() % (Max - Min + 1) + Min;
        #endregion

        #region Methods Aim
        /// <summary>
        /// Метод, вычисляющий расстояние до цели в градусах
        /// </summary>
        /// <param name="ViewAngel">Угол обзора локального игрока</param>
        /// <param name="Dst">Противник</param>
        /// <returns></returns>
        public static float GetFov(Vector3 ViewAngel, Vector3 Dst) => (float)Math.Sqrt(Math.Pow(Dst.X - ViewAngel.X, 2) + Math.Pow(Dst.Y - ViewAngel.Y, 2));

        /// <summary>
        /// Метод, вычисляющий расстояние до цели в градусах
        /// </summary>
        /// <param name="ViewAngel">Угол обзора локального игрока</param>
        /// <param name="Dst">Противник</param>
        /// <returns></returns>
        public static float GetFov(Vector3 ViewAngel, Vector3 Dst, float Distance) => (float)Math.Sqrt(Math.Pow(Math.Sin(Degrees2Rad(ViewAngel.X - Dst.X)) * Distance, 2) + Math.Pow(Math.Sin(Degrees2Rad(ViewAngel.Y - Dst.Y)) * Distance, 2));

        /// <summary>
        /// Захват угла
        /// </summary>
        /// <param name="Angles">Угол</param>
        /// <returns></returns>
        public static Vector3 ClampAngle(this Vector3 Angles)
        {
            if (Angles.X >= 89.0f)
            {
                Angles.X = 89f;
            }

            if (Angles.X <= -89.0f)
            {
                Angles.X = -89f;
            }

            if (Angles.Y >= 180.0f)
            {
                Angles.Y = 180f;
            }

            if (Angles.Y <= -180.0f)
            {
                Angles.Y = -180f;
            }

            Angles.Z = 0.0f;

            return Angles;
        }

        /// <summary>
        /// Нормализация угла
        /// </summary>
        /// <param name="Angle">Угол</param>
        /// <returns></returns>
        public static Vector3 NormalizeAngle(this Vector3 Angle)
        {
            while (Angle.X >= 89.0f)
            {
                Angle.X -= 180f;
            }

            while (Angle.X <= -89.0f)
            {
                Angle.X += 180f;
            }

            while (Angle.Y >= 180.0f)
            {
                Angle.Y -= 360f;
            }

            while (Angle.Y <= -180.0f)
            {
                Angle.Y += 360f;
            }

            return Angle;
        }

        /// <summary>
        /// Высчитывание угла
        /// </summary>
        /// <param name="Src">От локального игрока</param>
        /// <param name="Dst">До противника</param>
        /// <returns></returns>
        public static Vector3 CalcAngle(this Vector3 Src, Vector3 Dst) => new Vector3()
        {
            X = (float)(Math.Atan((Src - Dst).Z / Math.Sqrt((Src - Dst).X * (Src - Dst).X + (Src - Dst).Y * (Src - Dst).Y)) * 57.295779513082f),
            Y = (float)(Math.Atan2((Src - Dst).Y, (Src - Dst).X) * 57.295779513082f) + 180.0f,
            Z = 0.0f
        };

        /// <summary>
        /// Плавная доводка до нужного угла
        /// </summary>
        /// <param name="Src">От</param>
        /// <param name="Dst">До</param>
        /// <param name="SmoothAmount">Скорость доводки</param>
        /// <returns></returns>
        public static Vector3 SmoothAngle(Vector3 Src, Vector3 Dst, float SmoothAmount) => (Src + NormalizeAngle(Dst - Src) / 100f * SmoothAmount).ClampAngle();
        #endregion

        #region Weapon Methods
        public static uint GetWeaponId(this WeaponId WeaponId)
        {
            switch (WeaponId)
            {
                default: return 0;

                case WeaponId.Glock: return 1;
                case WeaponId.Hkp2000: return 2;
                case WeaponId.Usp_s: return 3;
                case WeaponId.Elite: return 4;
                case WeaponId.P250: return 5;
                case WeaponId.Tec9: return 6;
                case WeaponId.Fiveseven: return 7;
                case WeaponId.Cz75a: return 8;
                case WeaponId.Deagle: return 9;
                case WeaponId.Revolver: return 10;

                case WeaponId.Nova: return 11;
                case WeaponId.Xm1014: return 12;
                case WeaponId.Sawedoff: return 13;
                case WeaponId.Mag7: return 14;
                case WeaponId.M249: return 15;
                case WeaponId.Negev: return 16;

                case WeaponId.Mac10: return 17;
                case WeaponId.Mp9: return 18;
                case WeaponId.Mp7: return 19;
                case WeaponId.Mp5sd: return 20;
                case WeaponId.Ump45: return 21;
                case WeaponId.P90: return 22;
                case WeaponId.Bizon: return 23;

                case WeaponId.GalilAr: return 24;
                case WeaponId.Famas: return 25;
                case WeaponId.Ak47: return 26;
                case WeaponId.M4A1: return 27;
                case WeaponId.M4a1_s: return 28;
                case WeaponId.Ssg08: return 29;
                case WeaponId.Sg553: return 30;
                case WeaponId.Aug: return 31;
                case WeaponId.Awp: return 32;
                case WeaponId.G3SG1: return 33;
                case WeaponId.Scar20: return 34;

                case WeaponId.Knife_CT: return 35;
                case WeaponId.Knife_T: return 35;
                case WeaponId.Karambit: return 35;
                case WeaponId.Gut: return 35;
                case WeaponId.Flip: return 35;
                case WeaponId.Bayonet: return 35;
                case WeaponId.M9Bayonet: return 35;
                case WeaponId.Huntsman: return 35;
                case WeaponId.Butterfly: return 35;
                case WeaponId.Falchion: return 35;
                case WeaponId.Daggers: return 35;
                case WeaponId.Bowie: return 35;
                case WeaponId.Talon: return 35;
                case WeaponId.Stiletto: return 35;
                case WeaponId.Navaja: return 35;
                case WeaponId.Ursus: return 35;
                case WeaponId.CSS: return 35;
                case WeaponId.Cord: return 35;
                case WeaponId.Canis: return 35;
                case WeaponId.OutDoor: return 35;
                case WeaponId.Skeleton: return 35;
            }
        }

        public static uint GetKnifeId(this WeaponId WeaponId)
        {
            switch (WeaponId)
            {
                default: return 0;

                case WeaponId.Knife_CT: return 35;
                case WeaponId.Knife_T: return 35;
                case WeaponId.Karambit: return 35;
                case WeaponId.Gut: return 35;
                case WeaponId.Flip: return 35;
                case WeaponId.Bayonet: return 35;
                case WeaponId.M9Bayonet: return 35;
                case WeaponId.Huntsman: return 35;
                case WeaponId.Butterfly: return 35;
                case WeaponId.Falchion: return 35;
                case WeaponId.Daggers: return 35;
                case WeaponId.Bowie: return 35;
                case WeaponId.Talon: return 35;
                case WeaponId.Stiletto: return 35;
                case WeaponId.Navaja: return 35;
                case WeaponId.Ursus: return 35;
                case WeaponId.CSS: return 35;
                case WeaponId.Cord: return 35;
                case WeaponId.Canis: return 35;
                case WeaponId.OutDoor: return 35;
                case WeaponId.Skeleton: return 35;
            }
        }

        public static bool IsKnife(this WeaponId WeaponId)
        {
            switch (WeaponId)
            {
                default: return false;

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
        #endregion
    }
}