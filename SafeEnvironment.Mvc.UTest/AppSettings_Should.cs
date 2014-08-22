using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafeEnvironment.Mvc.Configuration;

namespace SafeEnvironment.UTest
{
    [TestClass]
    public class AppSettings_Should
    {
        [TestMethod]
        public void Should_return_settings_ssl_actived()
        {
            //arange
            AppSettings settings = new AppSettings();

            //act
            var result = settings.SslActived;

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_return_url_ssl_settings()
        {
            //arange
            AppSettings settings = new AppSettings();

            //act
            string result = settings.SslUrl;

            //assert
            Assert.AreEqual("http://localhost:3003", result);
        }

        [TestMethod]
        public void Should_return_non_url_ssl_settings()
        {
            //arange
            AppSettings settings = new AppSettings();

            //act
            string result = settings.NonSslUrl;

            //assert
            Assert.AreEqual("http://localhost:3000", result);
        }
    }
}
