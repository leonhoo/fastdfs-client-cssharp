using org.csource.fastdfs;
using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQing
/// <p>
/// FastDFS Java Client may be copied only under the terms of the GNU Lesser
/// General Public License (LGPL).
/// Please visit the FastDFS Home Page https://github.com/happyfish100/fastdfs for more detail.
/// </summary>
namespace org.csource.fastdfs
{
    /// <summary>
    /// Global variables
    ///
    /// @author Happy Fish / YuQing
    /// @version Version 1.11
    /// </summary>
    public partial class ClientGlobal
    {
        /// <summary>
        /// load global config
        /// </summary>
        /// <param name="conf_filename">config filename</param>
        public static void init(FdfsConfig config)
        {
            string[] szTrackerServers;
            string[] parts;

            g_connect_timeout = config.ConnectTimeout;
            if (g_connect_timeout < 0)
            {
                g_connect_timeout = DEFAULT_CONNECT_TIMEOUT;
            }
            g_connect_timeout *= 1000; //millisecond
            g_network_timeout = config.NetworkTimeout;
            if (g_network_timeout < 0)
            {
                g_network_timeout = DEFAULT_NETWORK_TIMEOUT;
            }
            g_network_timeout *= 1000; //millisecond
            var charset = config.Charset;
            if (charset == null)
            {
                charset = Encoding.GetEncoding("ISO8859-1");
            }
            g_charset = charset;
            szTrackerServers = config.TrackerServers;
            if (szTrackerServers == null)
            {
                throw new MyException("item \"tracker_server\" not found");
            }
            InetSocketAddress[] tracker_servers = new InetSocketAddress[szTrackerServers.Length];
            for (int i = 0; i < szTrackerServers.Length; i++)
            {
                parts = szTrackerServers[i].Split(":", 2);
                if (parts.Length != 2)
                {
                    throw new MyException("the value of item \"tracker_server\" is invalid, the correct format is host:port");
                }
                tracker_servers[i] = new InetSocketAddress(parts[0].Trim(), int.Parse(parts[1].Trim()));
            }
            g_tracker_group = new TrackerGroup(tracker_servers);

            g_tracker_http_port = config.TrackerHttpPort;
            g_anti_steal_token = config.AntiStealToken;
            if (g_anti_steal_token)
            {
                g_secret_key = config.SecretKey;
            }
            g_connection_pool_enabled = config.ConnectionPoolEnabled;
            g_connection_pool_max_count_per_entry = config.ConnectionPoolMaxCountPerEntry;
            g_connection_pool_max_idle_time = config.ConnectionPoolMaxIdleTime;
            if (g_connection_pool_max_idle_time < 0)
            {
                g_connection_pool_max_idle_time = DEFAULT_CONNECTION_POOL_MAX_IDLE_TIME;
            }
            g_connection_pool_max_idle_time *= 1000;
            g_connection_pool_max_wait_time_in_ms = config.ConnectionPoolMaxWaitTime;
            if (g_connection_pool_max_wait_time_in_ms < 0)
            {
                g_connection_pool_max_wait_time_in_ms = DEFAULT_CONNECTION_POOL_MAX_WAIT_TIME_IN_MS;
            }
        }
    }
}