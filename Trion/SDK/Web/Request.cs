using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Trion.SDK.Web.Enums;
using Trion.SDK.Web.Headers;

namespace Trion.SDK.Web
{
    internal sealed class Request
    {
        #region Params
        private readonly string Address = string.Empty;
        private readonly RequestMethod RequestMethod;

        private WebResponse HttpWebResponse;
        private RequestHeader RequestHeader;

        private Dictionary<string, string> ParamsValue = new Dictionary<string, string>();
        #endregion

        #region Initialization
        public Request()
        {
        }

        public Request(string Address, RequestMethod RequestMethod = RequestMethod.GET, RequestHeader RequestHeader = null)
        {
            this.Address = Address;

            this.RequestMethod = RequestMethod;
            this.RequestHeader = RequestHeader;
        }
        #endregion

        #region Indexer
        public object this[string Param]
        {
            set => ParamsValue.Add(Param, value.ToString());
        }
        #endregion

        #region Operators
        public static implicit operator string(Request Value) => Value.GetResponse();
        #endregion

        #region Params
        public string ContentType
        {
            private get;
            set;
        }

        public string Accept
        {
            private get;
            set;
        }

        public string UserAgent
        {
            private get;
            set;
        }
        #endregion

        #region Methods
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        private string GetResponse()
        {
            StringBuilder ReadResponse = new StringBuilder(15);

            HttpWebResponse = Response();

            using (StreamReader StreamReader = new StreamReader(HttpWebResponse.GetResponseStream()))
            {
                ReadResponse.Append(StreamReader.ReadToEnd());
            }

            return ReadResponse.ToString();
        }

        private async Task<string> GetResponseAsync()
        {
            StringBuilder ReadResponse = new StringBuilder(15);

            HttpWebResponse = await ResponseAsync();

            using (StreamReader StreamReader = new StreamReader(HttpWebResponse.GetResponseStream()))
            {
                ReadResponse.Append(await StreamReader.ReadToEndAsync());
            }

            return ReadResponse.ToString();
        }

        private HttpWebRequest WebRequest
        {
            get
            {
                HttpWebRequest HttpWebRequest;

                HttpWebRequest = RequestMethod == RequestMethod.GET ? (HttpWebRequest)System.Net.WebRequest.Create(Address + Encoding.UTF8.GetString(GetParam())) : (HttpWebRequest)System.Net.WebRequest.Create(Address);
                HttpWebRequest.Method = RequestMethod == RequestMethod.GET ? "GET" : "POST";

                if (RequestHeader != null)
                {
                    foreach (var Headers in RequestHeader?.RequestValue)
                    {
                        HttpWebRequest.Headers.Add(Headers.Key, Headers.Value);
                    }
                }

                HttpWebRequest.ContentType = ContentType ?? "application/x-www-form-urlencoded";
                HttpWebRequest.Accept = Accept ?? "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                HttpWebRequest.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 YaBrowser/19.3.1.887 Yowser/2.5 Safari/537.36";

                return HttpWebRequest;
            }
        }

        public HttpWebResponse Response()
        {
            HttpWebRequest HttpWebRequest = WebRequest;

            if (RequestMethod == RequestMethod.POST)
            {
                HttpWebRequest.ContentLength = GetParam().Length;

                using (Stream StreamRequest = HttpWebRequest.GetRequestStream())
                {
                    StreamRequest.Write(GetParam(), 0, GetParam().Length);
                }
            }

            ParamsValue.Clear();

            return (HttpWebResponse)HttpWebRequest.GetResponse();
        }

        public async Task<WebResponse> ResponseAsync()
        {
            HttpWebRequest HttpWebRequest = WebRequest;

            if (RequestMethod == RequestMethod.POST)
            {
                HttpWebRequest.ContentLength = GetParam().Length;

                using (Stream StreamRequest = await HttpWebRequest.GetRequestStreamAsync())
                {
                    StreamRequest.Write(GetParam(), 0, GetParam().Length);
                }
            }

            ParamsValue.Clear();

            return await HttpWebRequest.GetResponseAsync();
        }
        #endregion

        #region Helper Methods
        private byte[] GetParam()
        {
            try
            {
                string Params = null;

                foreach (var Param in ParamsValue)
                {
                    Params += Param.Key + "=" + Param.Value + "&";
                }

                return RequestMethod == RequestMethod.GET ? Encoding.UTF8.GetBytes(Params.Insert(0, "?").TrimEnd('&')) : Encoding.UTF8.GetBytes(Params.TrimEnd('&'));
            }
            catch
            {
                return new byte[1];
            }
        }
        #endregion

        #region Overide
        public override string ToString() => GetResponse();
        #endregion
    }
}