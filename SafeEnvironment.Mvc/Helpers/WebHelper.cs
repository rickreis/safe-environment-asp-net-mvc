using SafeEnvironment.Mvc.Configuration;
using System;
using System.Web;

namespace SafeEnvironment.Mvc
{
    internal static class WebHelper
    {
        static AppSettings _appSettings = new AppSettings();

        internal static string GetThisPageUrl(bool includeQueryString, bool useSsl = false)
        {
            string url = string.Empty;
            if (HttpContext.Current == null)
                return url;

            if (includeQueryString)
            {
                string absoluteUrl = GetStoreHost(useSsl);
                if (absoluteUrl.EndsWith("/"))
                    absoluteUrl = absoluteUrl.Substring(0, absoluteUrl.Length - 1);
                url = absoluteUrl + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            url = url.ToLowerInvariant();
            return url;
        }

        internal static string GetStoreHost(bool useSsl)
        {
            var httpHost = ServerVariables("HTTP_HOST");
            var result = "";
            if (!String.IsNullOrEmpty(httpHost))
            {
                result = "http://" + httpHost;
            }

            if (!result.EndsWith("/"))
                result += "/";

            if (useSsl)
            {
                //shared SSL certificate URL
                string sslUrl = _appSettings.SslUrl.Trim();
                if (!String.IsNullOrWhiteSpace(sslUrl))
                {
                    //shared SSL
                    result = sslUrl;
                }
                else
                {
                    //standard SSL
                    result = result.Replace("http:/", "https:/");
                }
            }
            else
            {
                if (SslEnabled())
                {
                    //SSL is enabled

                    //get shared SSL certificate URL
                    string sslUrl = _appSettings.SslUrl.Trim();
                    if (!String.IsNullOrWhiteSpace(sslUrl))
                    {
                        //shared SSL
                        string nonSslUrl = _appSettings.NonSslUrl.Trim();
                        if (string.IsNullOrWhiteSpace(nonSslUrl))
                            throw new Exception("NonSslUrl app config setting is not empty");
                        result = nonSslUrl;
                    }
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        internal static string ServerVariables(string name)
        {
            var _httpContext = HttpContext.Current;
            string tmpS = string.Empty;
            try
            {
                if (_httpContext.Request.ServerVariables[name] != null)
                {
                    tmpS = _httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        internal static bool SslEnabled()
        {
            return _appSettings.SslActived;
        }
    }
}