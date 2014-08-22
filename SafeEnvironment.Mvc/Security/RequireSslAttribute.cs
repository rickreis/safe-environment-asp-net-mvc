using SafeEnvironment.Mvc.Configuration;
using System;
using System.Web;
using System.Web.Mvc;

namespace SafeEnvironment.Mvc
{
    /// <summary>
    /// Filter responsible por perform redirect
    /// when address required connection security
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class RequireSslAttribute : FilterAttribute, IAuthorizationFilter
    {
        static AppSettings _appSettings = new AppSettings();
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null)
                return;

            if (filterContext.IsChildAction)
                return;

            // only redirect for GET requests, 
            // otherwise the browser might not propagate the verb and request body correctly.
            if (request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) == false)
                return;

            if (WebHelper.SslEnabled() == false)
                return;

            var currentConnectionSecured = request.IsSecureConnection;
            if (currentConnectionSecured == false)
            {
                RedirectToSsl(filterContext);
            }
            else
            {
                var secureSsl = _appSettings.SslUrl.Trim();
                if (request.Url.AbsoluteUri.Contains(secureSsl) == false)
                {
                    RedirectToSsl(filterContext);
                }
            }
        }

        private void RedirectToSsl(AuthorizationContext filterContext)
        {
            //redirect to HTTPS version of page
            string url = WebHelper.GetThisPageUrl(true, true);
            filterContext.Result = new RedirectResult(url);
        }
    }
}