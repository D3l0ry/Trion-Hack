using Trion.Client.Site.Strucutres;
using Trion.SDK.Serializers;
using Trion.SDK.Web;

namespace Trion.Client.Site
{
    internal static class Api
    {
        #region Variables
        private static readonly TrionObjectSerializer TrionObjectSerializer = new TrionObjectSerializer();
        private static readonly Request TrionRequest = new Request("http://api.trion.tk/");
        #endregion

        public static User Authorization()
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "auth";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<User>(TrionRequest);
        }

        public static User Authorization(this User user)
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "auth";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<User>(TrionRequest);
        }

        public static Profile GetProfile()
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "getUser";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<Profile>(TrionRequest);
        }

        public static Profile GetProfile(this Profile user)
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "getUser";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<Profile>(TrionRequest);
        }

        public static Hwid SetHwid()
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "sethwid";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<Hwid>(TrionRequest);
        }

        public static Hwid SetHwid(this Hwid hwid)
        {
            TrionRequest.ClearParams();

            TrionRequest["method"] = "sethwid";
            TrionRequest["login"] = "DanilPidor@ya.ru";
            TrionRequest["password"] = "DanilPidor";
            TrionRequest["hwid"] = "1";

            return TrionObjectSerializer.Deserialize<Hwid>(TrionRequest);
        }
    }
}