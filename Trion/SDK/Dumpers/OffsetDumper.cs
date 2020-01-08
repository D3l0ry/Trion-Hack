using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using Trion.SDK.WinAPI;

using OffsetDictionary = System.Collections.Generic.Dictionary<string, uint>;

namespace Trion.SDK.Dumpers
{
    internal class OffsetDumper
    {
        #region Params
        private static OffsetDictionary GetOffsetDictionary = null;
        #endregion

        private static OffsetDictionary GetOffset()
        {
            if (GetOffsetDictionary == null)
            {
                GetOffsetDictionary = new Dictionary<string, uint>();

                string GetValueUrl = new WebClient().DownloadString("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.toml");

                foreach (var Value in GetValueUrl.Split('\n'))
                {
                    try
                    {
                        GetOffsetDictionary.Add(Value.Split('=')[0].TrimEnd(' '), uint.Parse(Value.Split('=')[1].TrimStart(' ')));
                    }
                    catch
                    {
                        continue;
                    }
                }

                return GetOffsetDictionary;
            }

            return GetOffsetDictionary;
        }

        /// <summary>
        /// Чтение смещений в файле
        /// </summary>
        /// <param name="Section">Секция</param>
        /// <param name="Key">Ключ</param>
        /// <param name="FileInfo">Имя файла</param>
        /// <returns></returns>
        public static int GetFileOffset(string Section, string Key, FileInfo FileInfo)
        {
            StringBuilder Offset = new StringBuilder(255);

            NativeMethods.GetPrivateProfileString(Section, Key, "", Offset, 255, FileInfo.FullName);

            return int.Parse(Offset.ToString());
        }

        /// <summary>
        /// Чтение смещений
        /// </summary>
        /// <param name="Url">Адрес скачивания смещений</param>
        /// <returns></returns>
        public static OffsetDictionary GetOnlineOffset => GetOffsetDictionary == null ? GetOffset() : GetOffsetDictionary;
    }
}