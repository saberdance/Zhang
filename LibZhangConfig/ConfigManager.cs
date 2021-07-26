using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibZhangConfig
{
    public class ConfigManager
    {
        IConfigContext ConfigContext { get; set; }

        string ConfigFilePath { get; set; }

        public ConfigManager(string configFilePath, IConfigContext configContext)
        {
            ConfigFilePath = configFilePath;
            ConfigContext = configContext;
        }

        public ZhangConfig LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return null;
            }
            string fileContext = File.ReadAllText(ConfigFilePath);
            return ConfigContext.Deserialize(fileContext);
        }

        public bool WriteConfig(ZhangConfig config)
        {
            string fileContext = ConfigContext.Serialize(config);
            if (string.IsNullOrEmpty(fileContext))
            {
                return false;
            }
            string fileFolder = Path.GetDirectoryName(ConfigFilePath);
            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }
            using (var sw = new StreamWriter(ConfigFilePath))
            {
                sw.Write(fileContext);
            }
            return true;
        }
    }
}
