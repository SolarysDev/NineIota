using System;
using System.IO;

using Newtonsoft.Json;

namespace ZeroTwo.Resources
{
    public static class ConfigLoader
    {
        public static T LoadFrom<T>(string pathToFile)
        {
            if (!File.Exists(pathToFile))
                throw new FileNotFoundException("Target file not found.");
            string json = File.ReadAllText(pathToFile);

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                throw new FileLoadException("Parse failed. File might not be valid JSON.");
            }
        }
    }
}
