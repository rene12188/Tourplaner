using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Tourplaner_Utility
{


    /// <summary>Loads the Config and anwers calls for certain Config settings</summary>
    public class CFGManager
    {
       static readonly IConfiguration  config = new ConfigurationBuilder()
            .AddJsonFile("settings.json", false, true) // add as content / copy-always
            .Build();


        /// <summary>Reads the Config setting.</summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <para>Returns a String which Represents the read setting or "No"</para>
        /// </returns>
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



        /// <summary>
        ///   <para>
        /// Updates application settings, but only in the Runtime.
        /// </para>
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
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

    }

}

