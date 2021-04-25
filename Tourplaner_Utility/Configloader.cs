using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Tourplaner_Utility
{
    public class CFGManager
    {
       static  IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("settings.json", false, true) // add as content / copy-always
            .Build();

        public static string ReadSetting(string key)
        {
            try
            {
                if (config[key] != null)
                {
                    return config[key];
                }
                else
                {
                    return "No";
                }
            }
            catch 
            {
                Console.WriteLine("Error reading app settings");
            }

            return null;
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
               
                if (config[key] == null)
                {
                    
                }
                else
                {
                    config[key] = value;
                }
            }
            catch 
            {
                Console.WriteLine("Error writing app settings");
            }
        }


        public static void WriteSettingsToFile()
        {

            try
            {


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }

}

