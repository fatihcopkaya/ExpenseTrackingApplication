using Microsoft.Extensions.Configuration;

namespace MasrafTakipYonetim.Persistence
{
     static class Configuration
    {
        static public string ConnectionString
        {

            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MasrafTakipYonetim.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("MSSQL");
            }
        }
    }
}
