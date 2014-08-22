using System;
using System.Configuration;

namespace SafeEnvironment.Mvc.Configuration
{
    internal class AppSettings : BaseSettings<AppConfiguration>
    {
        public override string SectionName
        {
            get { return "SafeEnvironment/Configuration"; }
        }

        internal virtual bool SslActived
        {
            get
            {
                return CurrentSettings != null ? Convert.ToBoolean(CurrentSettings.SslActived) : false;
            }
        }

        internal virtual string NonSslUrl
        {
            get
            {
                return CurrentSettings != null ? CurrentSettings.NonSslUrl : String.Empty;
            }
        }

        internal virtual string SslUrl
        {
            get
            {
                return CurrentSettings != null ? CurrentSettings.SslUrl : String.Empty;
            }
        }
    }
}