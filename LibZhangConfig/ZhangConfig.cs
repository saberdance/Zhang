using System;
using System.Collections.Generic;
using System.Text;

namespace LibZhangConfig
{
    public class CrawlerConfig
    {
        //最大爬虫数目
        public int MaxCount { get; set; }
        //爬虫超时时间
        public TimeSpan Timeout { get; set; }
        //爬虫工作深度
        public int Depth { get; set; }
    }

    public class UserConfig
    {
        //本地用户Id
        public string UserId { get; set; }
        //本地用户密码哈希
        public string PasswordMd5 { get; set; }
    }

    public class ZhangConfig
    {
        public UserConfig UserConfig { get; set; } = new UserConfig();
        public CrawlerConfig CrawlerConfig { get; set; } = new CrawlerConfig();
    }
}
