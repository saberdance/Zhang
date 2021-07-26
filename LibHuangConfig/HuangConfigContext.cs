using System;
using System.Collections.Generic;
using LibZhangConfig;

namespace LibHuangConfig
{
    public class HuangConfigContext : IConfigContext
    {
        public ZhangConfig Deserialize(string configFile)
        {
            string[] lines = SplitStringByLine(configFile);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"{lines[i]}");
            }
            return LinesToZhangConfig(lines);
        }

        public string Serialize(ZhangConfig config)
        {
            return $@"UserId={GetFormatLine(config.UserConfig.UserId)}
PasswordMd5={GetFormatLine(config.UserConfig.UserId)}
MaxCount={GetFormatLine(config.CrawlerConfig.MaxCount.ToString())}
Timeout={GetFormatLine(config.CrawlerConfig.Timeout.TotalSeconds.ToString())}
Deepth={GetFormatLine(config.CrawlerConfig.Depth.ToString())}";
        }

        private string GetFormatLine(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            return str;
        }

        private string[] SplitStringByLine(string source)
        {
            return source.Split(System.Environment.NewLine);
        }

        private ZhangConfig LinesToZhangConfig(string[] lines)
        {
            ZhangConfig config = new ZhangConfig();
            foreach (var line in lines)
            {
                KeyValuePair<string, string> oneConfigPair = AnalyzeOneLine(line);
                if (oneConfigPair.Key == null)
                {
                    Console.WriteLine($"无法读取的配置文件,无效行:{line}");
                    return null;
                }
                if (!SetConfig(oneConfigPair,ref config))
                {
                    Console.WriteLine($"不支持的配置:{line}");
                    return null;
                }
            }
            return config;
        }

        private bool SetConfig(KeyValuePair<string, string> oneConfigPair, ref ZhangConfig config)
        {
            switch(oneConfigPair.Key)
            {
                case "UserId":
                    config.UserConfig.UserId = oneConfigPair.Value;
                    break;
                case "PasswordMd5":
                    config.UserConfig.PasswordMd5 = oneConfigPair.Value;
                    break;
                case "MaxCount":
                    try
                    {
                        config.CrawlerConfig.MaxCount = int.Parse(oneConfigPair.Value);
                    }
                    catch (Exception)
                    {
                        return false;
                    }                    
                    break;
                case "Timeout":
                    try
                    {
                        int timeoutSeconds = int.Parse(oneConfigPair.Value);
                        config.CrawlerConfig.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                case "Deepth":
                    try
                    {
                        config.CrawlerConfig.Depth = int.Parse(oneConfigPair.Value);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        private KeyValuePair<string, string> AnalyzeOneLine(string line)
        {
            int hasSplitPattern = line.IndexOf("=");
            if (hasSplitPattern == -1)
            {
                return new KeyValuePair<string, string>(null, null);
            }
            string key = line.Substring(0, hasSplitPattern);
            string value = line.Substring(hasSplitPattern + 1);
            if (hasSplitPattern != line.Length - 1)
            {
                return new KeyValuePair<string, string>(key, value);
            }
            return new KeyValuePair<string, string>(key, null);
        }
    }
}
