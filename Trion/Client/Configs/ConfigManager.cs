using System.IO;
using System.Xml.Serialization;

using Trion.SDK.Interfaces.Client.Entity.Structures;

namespace Trion.Client.Configs
{
    internal static class ConfigManager
    {
        private static string ConfigPath<T>() => Path.Combine($@"C:\Config", $"{typeof(T).Name}.xml");

        private static string ConfigPath(object ObjectSave) => Path.Combine($@"C:\Config", $@"{ObjectSave.GetType().Name}.xml");

        public static CVisual CVisual = new CVisual()
        {
            Alpha = 1f
        };
        public static CMisc CMisc = new CMisc();
        public static CSkinChanger CSkinChanger = new CSkinChanger() {  SkinChangerActive = true};
        public static CSkinChangerWeapon[] CSkinChangerWeapons =
        {
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.None,12),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Glock,918),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Hkp2000),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Usp_s,705),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Elite,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.P250,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Tec9,644),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Fiveseven,660),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Cz75a,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Deagle,645),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Revolver,12),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Nova,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Xm1014,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Sawedoff,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mag7,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M249,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Negev,12),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mac10,38),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp9,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp7,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp5sd,810),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ump45,802),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.P90,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Bizon,12),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.GalilAr,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Famas,919),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ak47,474,0,true,1),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M4A1,309),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M4a1_s,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ssg08,899),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Sg553,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Aug,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Awp,344),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.G3SG1,12),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Scar20,12),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Talon,522,0,false,0,null),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.GloveMotorcycle,10028,0,false,0,null),
        };

        public static void SaveConfig()
        {
            object[] ObjectType =
            {
                CVisual,
                CSkinChanger,
                CSkinChangerWeapons
            };

            if (!Directory.Exists("Config"))
            {
                Directory.CreateDirectory("Config");
            }

            foreach (var ObjectSave in ObjectType)
            {
                using (FileStream FileStream = new FileStream(ConfigPath(ObjectSave), FileMode.Create, FileAccess.Write))
                {
                    new XmlSerializer(ObjectSave.GetType()).Serialize(FileStream, ObjectSave);
                }
            }
        }

        public static T LoadConfig<T>()
        {
        TryLoad:
            try
            {
                using (FileStream FileStream = new FileStream(ConfigPath<T>(), FileMode.Open, FileAccess.Read))
                {
                    return (T)new XmlSerializer(typeof(T)).Deserialize(FileStream);
                }
            }
            catch
            {
                object[] ObjectType =
                {
                    CVisual,
                    CSkinChanger,
                    CSkinChangerWeapons
                };

                if (!Directory.Exists("Config"))
                {
                    Directory.CreateDirectory("Config");
                }

                using (FileStream FileStream = new FileStream(ConfigPath<T>(), FileMode.Create, FileAccess.Write))
                {
                    foreach (var ObjectSave in ObjectType)
                    {
                        if (typeof(T).Name == ObjectSave.GetType().Name)
                        {
                            new XmlSerializer(typeof(T)).Serialize(FileStream, ObjectSave);
                        }
                    }
                }

                goto TryLoad;
            }
        }
    }
}