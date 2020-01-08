using System.IO;
using System.Xml.Serialization;

using Trion.SDK.Interfaces.Client.Entity.Structures;

namespace Trion.Client.Configs
{
    internal static class ConfigManager
    {
        private static string ConfigPath<T>() => Path.Combine($@"C:\Config", $"{typeof(T).Name}.xml");

        private static string ConfigPath(object ObjectSave) => Path.Combine($@"C:\Config", $@"{ObjectSave.GetType().Name}.xml");

        public static CVisual CVisual = new CVisual();
        public static CSkinChanger CSkinChanger = new CSkinChanger();
        public static CSkinChangerWeapon[] CSkinChangerWeapons =
        {
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.None),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Glock) { SkinID = 918},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Hkp2000),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Usp_s) { SkinID = 705},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Elite),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.P250),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Tec9){ SkinID = 644},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Fiveseven){ SkinID = 660},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Cz75a),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Deagle)  { SkinID = 645},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Revolver),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Nova),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Xm1014),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Sawedoff),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mag7),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M249),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Negev),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mac10){ SkinID = 38},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp9),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp7),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Mp5sd){ SkinID = 810},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ump45){ SkinID = 802},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.P90),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Bizon),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.GalilAr),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Famas){ SkinID = 919},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ak47) {  SkinID =474, StatTrackEnable = true},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M4A1) { SkinID = 309},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M4a1_s),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Ssg08){ SkinID = 899},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Sg553),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Aug),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Awp) { SkinID = 344},
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.G3SG1),
            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.Scar20),

            new CSkinChangerWeapon(BaseCombatWeapon.WeaponId.M9Bayonet) {  SkinID = 522},
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