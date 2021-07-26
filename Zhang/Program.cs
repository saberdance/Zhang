using System;
using LibZhangConfig;
using LibHuangConfig;

namespace Zhang
{
    class Program
    {
        static void Main(string[] args)
        {
            ZhangConfig config = new ZhangConfig();
            config.UserConfig.UserId = "12312dae12ff";
            config.UserConfig.PasswordMd5 = "aaaaaaaaaaaaaaaaaa";
            config.CrawlerConfig.Depth = 3;
            config.CrawlerConfig.MaxCount = 10;
            config.CrawlerConfig.Timeout = TimeSpan.FromSeconds(3600);

            ConfigManager configManager = new ConfigManager("./fuck/fuckyou.conf", new XmlConfigContext());
            //configManager.WriteConfig(config);
            var loadConf = configManager.LoadConfig();
            Console.WriteLine(loadConf.UserConfig.UserId);
        }
    }
}
