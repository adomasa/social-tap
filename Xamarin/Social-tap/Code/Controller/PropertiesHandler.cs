using System;
using System.IO;
using Android.Content;
using Android.Util;
using Java.Util;
using Socialtap.Code.Controller.Interfaces;

namespace Socialtap.Code.Controller
{
    public class PropertiesHandler : IPropertiesHandler
    {
        private static String TAG = "PropertiesHandler";

        private static IPropertiesHandler instance;

        private Context _context;
        private readonly Properties _properties;

        private PropertiesHandler(Context context)
        {
            _context = context;
            var assetManager = context.Assets;

            try
            {
                var streamReader = new StreamReader(assetManager.Open("config.properties"));
                var rawResource = streamReader.BaseStream;
                _properties = new Properties();
                _properties.Load(rawResource);
            }
            //Todo: handle exception separately
            catch (Exception e)
            {
                Log.Error(TAG, $"Failed to read .properties: {e.Message}");
            }
        }

        //Initialised first time 
        public static IPropertiesHandler GetInstance(Context context)
        {
            return instance = instance ?? new PropertiesHandler(context);
        }

        /// <summary>
        /// Search value using a key from config file
        /// </summary>
        /// <returns>The configuration value</returns>
        /// <param name="key">Configuration value key</param>
        public string GetConfigValue(String key)
        {
            return _properties.GetProperty(key);
        }
    }
}
