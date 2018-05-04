using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZeroTwo
{
    class Config
    {
        const string configFolder = "Resources";
        const string configFile = "config.json";

        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);
            
            if (!File.Exists($"{configFolder}/{configFile}"))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText($"{configFolder}/{configFile}", json);
            }
            else 
            {
                string json = File.ReadAllText($"{configFolder}/{configFile}");
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
                
        }

        public struct BotConfig
        {
            public string token;
            public string prefix;
            public string osukey;
            public string riotKey;
            public string malUser;
            public string malKey;
            public string[] FormatCatch;
            public string[] FormatReplace;
            public string FortniteKey;
            public string DBLKey;
            public string Giphy;
        }
    }
}
