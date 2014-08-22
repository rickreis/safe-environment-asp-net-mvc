using System.Configuration;

namespace SafeEnvironment.Mvc.Configuration
{
    /// <summary>
    /// App Configuration Section
    /// </summary>
    internal class AppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("sslActived", DefaultValue = "", IsRequired = true)]
        internal string SslActived
        {
            get { return (string)base["sslActived"]; }
            set { base["sslActived"] = value; }
        }

        [ConfigurationProperty("sslUrl", DefaultValue = "", IsRequired = true)]
        internal string SslUrl
        {
            get { return (string)base["sslUrl"]; }
            set { base["sslUrl"] = value; }
        }

        [ConfigurationProperty("nonSslUrl", DefaultValue = "", IsRequired = false)]
        internal string NonSslUrl
        {
            get { return (string)base["nonSslUrl"]; }
            set { base["nonSslUrl"] = value; }
        }        
    }    
}
