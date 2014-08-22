using System;
using System.Configuration;

namespace SafeEnvironment.Mvc.Configuration
{
    public abstract class BaseSettings<T>
    {
        public abstract string SectionName { get; }

        T currentSettings;
        protected T CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                {
                    try
                    {
                        currentSettings = (T)ConfigurationManager.GetSection(SectionName);
                    }
                    catch (ConfigurationErrorsException configEx)
                    {
                        throw configEx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return currentSettings;
            }
        }
    }
}
