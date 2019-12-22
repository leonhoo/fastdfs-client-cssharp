using System;
using System.Collections.Generic;
using System.Text;

namespace org.csource.fastdfs.encapsulation
{
    /// <summary>
    /// 配置类; 除了tracker_server，其它配置项都是可选的
    /// </summary>
    public class FdfsConfig
    {
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeout { get; set; } = ClientGlobal.DEFAULT_CONNECT_TIMEOUT;
        /// <summary>
        /// 网络超时时间
        /// </summary>
        public int NetworkTimeout { get; set; } = ClientGlobal.DEFAULT_NETWORK_TIMEOUT;
        /// <summary>
        /// 字符集编码
        /// </summary>
        public Encoding Charset { get; set; } = Encoding.GetEncoding(ClientGlobal.DEFAULT_CHARSET);
        /// <summary>
        /// 是否使用Token
        /// </summary>
        public bool AntiStealToken { get; set; } = ClientGlobal.DEFAULT_HTTP_ANTI_STEAL_TOKEN;
        /// <summary>
        /// Token加密密钥
        /// </summary>
        public string SecretKey { get; set; } = ClientGlobal.DEFAULT_HTTP_SECRET_KEY;
        /// <summary>
        /// 
        /// </summary>
        public int TrackerHttpPort { get; set; } = 80;
        /// <summary>
        /// 指向您自己IP地址和端口，1-n个, 必填
        /// </summary> 
        public string[] TrackerServers { get; set; }
    }
}
