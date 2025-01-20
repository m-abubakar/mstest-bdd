using Microsoft.Extensions.Configuration;

namespace Utils
{
    public class ConfigReader
    {
        public ConfigReader() {
            // Build configuration 
            var basePath = System.IO.Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();
            configuration = configurationBuilder
                .SetBasePath(basePath) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               	.Build(); 

        }

        private string appSetting(string key)
        {
            return configuration[key] ?? throw new Exception($"Key '{key}' not found in app settings.");
        }

        public String Browser() {
            return appSetting("browser");
        }

        public String AppUrl() {
            return appSetting("baseUrl");
        }

        //public String GetCorrectUserName() {
        //}

        //public String GetCorrectPassword() {
        //}

        IConfigurationRoot configuration;
    }
}