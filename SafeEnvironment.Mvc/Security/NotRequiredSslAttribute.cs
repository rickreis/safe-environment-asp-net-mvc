using System;
using System.Web;
using System.Web.Mvc;

namespace SafeEnvironment.Mvc
{
    /// <summary>
    /// Filter responsible perform redirect
    /// when address not require a connection sucurity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class NotRequireSslAttribute : FilterAttribute, IAuthorizationFilter
    {
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
            if (currentConnectionSecured)
            {
                //redirect to HTTP version of page
                string url = WebHelper.GetThisPageUrl(true, false);
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}