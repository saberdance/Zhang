using System;
using System.Collections.Generic;
using System.Text;

namespace LibZhangConfig
{
    /// <summary>
    /// 用于配置文件的接口
    /// </summary>
    public interface IConfigContext
    {
        ZhangConfig Deserialize(string configFile);
        string Serialize(ZhangConfig config);
    }
}
