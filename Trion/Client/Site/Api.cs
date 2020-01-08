using Trion.Client.Site.Strucutres;
using Trion.SDK.Serializers;
using Trion.SDK.Web;

namespace Trion.Client.Site
{
    internal static class Api
    {
        #region Variables
        private static TrionObjectSerializer TrionObjectSerializer = new TrionObjectSerializer();
        #endregion

        public static User Authorization() =>  TrionObjectSerializer.Deserialize<User>(new Request("http://api.trion.tk/")
        {
            ["method"] = "auth",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });

        public static User Authorization(this User User) => TrionObjectSerializer.Deserialize<User>(new Request("http://api.trion.tk/")
        {
            ["method"] = "auth",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });

        public static Profile GetProfile() => TrionObjectSerializer.Deserialize<Profile>(new Request("http://api.trion.tk/")
        {
            ["method"] = "getUser",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });

        public static Profile GetProfile(this User User) => TrionObjectSerializer.Deserialize<Profile>(new Request("http://api.trion.tk/")
        {
            ["method"] = "getUser",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });

        public static Hwid SetHwid() => TrionObjectSerializer.Deserialize<Hwid>(new Request("http://api.trion.tk/")
        {
            ["method"] = "sethwid",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });

        public static Hwid SetHwid(this Hwid Hwid) => TrionObjectSerializer.Deserialize<Hwid>(new Request("http://api.trion.tk/")
        {
            ["method"] = "sethwid",
            ["login"] = "DanilPidor@ya.ru",
            ["password"] = "DanilPidor",
            ["hwid"] = "1"
        });
    }
}