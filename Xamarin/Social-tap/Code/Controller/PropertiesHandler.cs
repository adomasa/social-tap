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
        static readonly string Tag = typeof(PropertiesHandler).Name;

        private readonly string ConfigFileKey;

        private static IPropertiesHandler instance;

        private Context _context;
        private readonly Properties _properties;

        private PropertiesHandler(Context context)
        {
            _context = context;
            var assetManager = context.Assets;
            ConfigFileKey = context.GetString(Resource.String.config_file_key);
            var streamReader = new StreamReader(assetManager.Open(ConfigFileKey));
            var rawResource = streamReader.BaseStream;
            _properties = new Properties();
            _properties.Load(rawResource);
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
            Log.Debug(Tag, $"Fetching {key}");
            return _properties.GetProperty(key);
        }
    }
}
