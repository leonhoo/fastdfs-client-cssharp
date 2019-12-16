using org.csource.fastdfs;
using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail./namespace org.csource.fastdfs{Global variables
/// </summary>
public class ClientGlobal
{
    public const string CONF_KEY_CONNECT_TIMEOUT = "connect_timeout";
    public const string CONF_KEY_NETWORK_TIMEOUT = "network_timeout";
    public const string CONF_KEY_CHARSET = "charset";
    public const string CONF_KEY_HTTP_ANTI_STEAL_TOKEN = "http.anti_steal_token";
    public const string CONF_KEY_HTTP_SECRET_KEY = "http.secret_key";
    public const string CONF_KEY_HTTP_TRACKER_HTTP_PORT = "http.tracker_http_port";
    public const string CONF_KEY_TRACKER_SERVER = "tracker_server";
    public const string PROP_KEY_CONNECT_TIMEOUT_IN_SECONDS = "fastdfs.connect_timeout_in_seconds";
    public const string PROP_KEY_NETWORK_TIMEOUT_IN_SECONDS = "fastdfs.network_timeout_in_seconds";
    public const string PROP_KEY_CHARSET = "fastdfs.charset";
    public const string PROP_KEY_HTTP_ANTI_STEAL_TOKEN = "fastdfs.http_anti_steal_token";
    public const string PROP_KEY_HTTP_SECRET_KEY = "fastdfs.http_secret_key";
    public const string PROP_KEY_HTTP_TRACKER_HTTP_PORT = "fastdfs.http_tracker_http_port";
    public const string PROP_KEY_TRACKER_SERVERS = "fastdfs.tracker_servers";
    public const int DEFAULT_CONNECT_TIMEOUT = 5; //second
    public const int DEFAULT_NETWORK_TIMEOUT = 30; //second
    public const string DEFAULT_CHARSET = "UTF-8";
    public const bool DEFAULT_HTTP_ANTI_STEAL_TOKEN = false;
    public const string DEFAULT_HTTP_SECRET_KEY = "FastDFS1234567890";
    public const int DEFAULT_HTTP_TRACKER_HTTP_PORT = 80;
    public static int g_connect_timeout = DEFAULT_CONNECT_TIMEOUT * 1000; //millisecond
    public static int g_network_timeout = DEFAULT_NETWORK_TIMEOUT * 1000; //millisecond
    public static Encoding g_charset = Encoding.GetEncoding(DEFAULT_CHARSET);
    public static bool g_anti_steal_token = DEFAULT_HTTP_ANTI_STEAL_TOKEN; //if anti-steal token
    public static string g_secret_key = DEFAULT_HTTP_SECRET_KEY; //generage token secret key
    public static int g_tracker_http_port = DEFAULT_HTTP_TRACKER_HTTP_PORT;
    public static TrackerGroup g_tracker_group;
    private ClientGlobal()
    {
    }

    /// <summary>
    /// load global variables
    /// </summary>
    /// <param name="conf_filename">config filename</param>
    public static void init(string conf_filename)
    {
        IniFileReader iniReader;
        string[] szTrackerServers;
        string[] parts;
        iniReader = new IniFileReader(conf_filename);
        g_connect_timeout = iniReader.getIntValue("connect_timeout", DEFAULT_CONNECT_TIMEOUT);
        if (g_connect_timeout < 0)
        {
            g_connect_timeout = DEFAULT_CONNECT_TIMEOUT;
        }
        g_connect_timeout *= 1000; //millisecond
        g_network_timeout = iniReader.getIntValue("network_timeout", DEFAULT_NETWORK_TIMEOUT);
        if (g_network_timeout < 0)
        {
            g_network_timeout = DEFAULT_NETWORK_TIMEOUT;
        }
        g_network_timeout *= 1000; //millisecond
        var charset = iniReader.getStrValue("charset");
        if (charset == null || charset.Length == 0)
        {
            charset = "ISO8859-1";
        }
        g_charset = Encoding.GetEncoding(charset);
        szTrackerServers = iniReader.getValues("tracker_server");
        if (szTrackerServers == null)
        {
            throw new MyException("item \"tracker_server\" in " + conf_filename + " not found");
        }
        InetSocketAddress[] tracker_servers = new InetSocketAddress[szTrackerServers.Length];
        for (int i = 0; i < szTrackerServers.Length; i++)
        {
            parts = szTrackerServers[i].Split("\\:", 2);
            if (parts.Length != 2)
            {
                throw new MyException("the value of item \"tracker_server\" is invalid, the correct format is host:port");
            }
            tracker_servers[i] = new InetSocketAddress(parts[0].Trim(), int.Parse(parts[1].Trim()));
        }
        g_tracker_group = new TrackerGroup(tracker_servers);
        g_tracker_http_port = iniReader.getIntValue("http.tracker_http_port", 80);
        g_anti_steal_token = iniReader.getBoolValue("http.anti_steal_token", false);
        if (g_anti_steal_token)
        {
            g_secret_key = iniReader.getStrValue("http.secret_key");
        }
    }
    /// <summary>
    /// load from properties file
    /// </summary>
    /// <param name="propsFilePath">
    ///         properties file path, eg:
    ///                      "fastdfs-client.properties"
    ///                      "config/fastdfs-client.properties"
    ///                      "/opt/fastdfs-client.properties"
    ///                      "C:\\Users\\James\\config\\fastdfs-client.properties"
    ///                      properties文件至少包含一个配置项 fastdfs.tracker_servers 例如：
    ///                      fastdfs.tracker_servers = 10.0.11.245:22122,10.0.11.246:22122
    ///                      server的IP和端口用冒号':'分隔
    ///                      server之间用逗号','分隔
    /// </param>
    public static void initByProperties(string propsFilePath)
    {
        Properties props = new Properties();
        Stream stream = IniFileReader.loadFromOsFileSystemOrClasspathAsStream(propsFilePath);
        if (stream != null)
        {
            props.load(stream);
        }
        initByProperties(props);
    }
    public static void initByProperties(Properties props)
    {
        string trackerServersConf = props.getProperty(PROP_KEY_TRACKER_SERVERS);
        if (trackerServersConf == null || trackerServersConf.Trim().Length == 0)
        {
            throw new MyException(string.Format("configure item {0} is required", PROP_KEY_TRACKER_SERVERS));
        }
        initByTrackers(trackerServersConf.Trim());
        string connectTimeoutInSecondsConf = props.getProperty(PROP_KEY_CONNECT_TIMEOUT_IN_SECONDS);
        string networkTimeoutInSecondsConf = props.getProperty(PROP_KEY_NETWORK_TIMEOUT_IN_SECONDS);
        string charsetConf = props.getProperty(PROP_KEY_CHARSET);
        string httpAntiStealTokenConf = props.getProperty(PROP_KEY_HTTP_ANTI_STEAL_TOKEN);
        string httpSecretKeyConf = props.getProperty(PROP_KEY_HTTP_SECRET_KEY);
        string httpTrackerHttpPortConf = props.getProperty(PROP_KEY_HTTP_TRACKER_HTTP_PORT);
        if (connectTimeoutInSecondsConf != null && connectTimeoutInSecondsConf.Trim().Length != 0)
        {
            g_connect_timeout = int.Parse(connectTimeoutInSecondsConf.Trim()) * 1000;
        }
        if (networkTimeoutInSecondsConf != null && networkTimeoutInSecondsConf.Trim().Length != 0)
        {
            g_network_timeout = int.Parse(networkTimeoutInSecondsConf.Trim()) * 1000;
        }
        if (charsetConf != null && charsetConf.Trim().Length != 0)
        {
            g_charset = Encoding.GetEncoding(charsetConf.Trim());
        }
        if (httpAntiStealTokenConf != null && httpAntiStealTokenConf.Trim().Length != 0)
        {
            g_anti_steal_token = (string.Equals(httpAntiStealTokenConf.Trim(), "true", System.StringComparison.OrdinalIgnoreCase) || httpAntiStealTokenConf.Trim() == "1");
        }
        if (httpSecretKeyConf != null && httpSecretKeyConf.Trim().Length != 0)
        {
            g_secret_key = httpSecretKeyConf.Trim();
        }
        if (httpTrackerHttpPortConf != null && httpTrackerHttpPortConf.Trim().Length != 0)
        {
            g_tracker_http_port = int.Parse(httpTrackerHttpPortConf);
        }
    }

    /// <summary>
    /// load from properties file
    /// </summary>
    /// <param name="trackerServers">例如："10.0.11.245:22122,10.0.11.246:22122"server的IP和端口用冒号':'分隔server之间用逗号','分隔</param>
    public static void initByTrackers(string trackerServers)
    {
        List<InetSocketAddress> list = new List<InetSocketAddress>();
        char spr1 = ',';
        char spr2 = ':';
        string[] arr1 = trackerServers.Trim().Split(spr1);
        foreach (string addrStr in arr1)
        {
            string[] arr2 = addrStr.Trim().Split(spr2);
            string host = arr2[0].Trim();
            int port = int.Parse(arr2[1].Trim());
            list.Add(new InetSocketAddress(host, port));
        }
        InetSocketAddress[] trackerAddresses = list.ToArray();
        initByTrackers(trackerAddresses);
    }
    public static void initByTrackers(InetSocketAddress[] trackerAddresses)
    {
        g_tracker_group = new TrackerGroup(trackerAddresses);
    }

    /// <summary>
    /// construct JavaSocket object
    /// </summary>
    /// <param name="ip_addr">ip address or hostname</param>
    /// <param name="port">port number</param>
    /// <returns> connected JavaSocket object</returns>
    public static JavaSocket getSocket(string ip_addr, int port)
    {
        JavaSocket sock = new JavaSocket();
        sock.SendTimeout = (ClientGlobal.g_network_timeout);
        sock.Connect(ip_addr, port);
        return sock;
    }

    /// <summary>
    /// construct JavaSocket object
    /// </summary>
    /// <param name="addr">InetSocketAddress object, including ip address and port</param>
    /// <returns> connected JavaSocket object</returns>
    public static JavaSocket getSocket(InetSocketAddress addr)
    {
        return getSocket(addr.Address, addr.Port);
    }
    public static int getG_connect_timeout()
    {
        return g_connect_timeout;
    }
    public static void setG_connect_timeout(int connect_timeout)
    {
        ClientGlobal.g_connect_timeout = connect_timeout;
    }
    public static int getG_network_timeout()
    {
        return g_network_timeout;
    }
    public static void setG_network_timeout(int network_timeout)
    {
        ClientGlobal.g_network_timeout = network_timeout;
    }
    public static string getG_charset()
    {
        return g_charset.ToString(); // ???????
    }
    public static void setG_charset(string charset)
    {
        ClientGlobal.g_charset = Encoding.GetEncoding(charset);
    }
    public static int getG_tracker_http_port()
    {
        return g_tracker_http_port;
    }
    public static void setG_tracker_http_port(int tracker_http_port)
    {
        ClientGlobal.g_tracker_http_port = tracker_http_port;
    }
    public static bool getG_anti_steal_token()
    {
        return g_anti_steal_token;
    }
    public static bool isG_anti_steal_token()
    {
        return g_anti_steal_token;
    }
    public static void setG_anti_steal_token(bool anti_steal_token)
    {
        ClientGlobal.g_anti_steal_token = anti_steal_token;
    }
    public static string getG_secret_key()
    {
        return g_secret_key;
    }
    public static void setG_secret_key(string secret_key)
    {
        ClientGlobal.g_secret_key = secret_key;
    }
    public static TrackerGroup getG_tracker_group()
    {
        return g_tracker_group;
    }
    public static void setG_tracker_group(TrackerGroup tracker_group)
    {
        ClientGlobal.g_tracker_group = tracker_group;
    }
    public static string configInfo()
    {
        string trackerServers = "";
        if (g_tracker_group != null)
        {
            InetSocketAddress[] trackerAddresses = g_tracker_group.tracker_servers;
            foreach (InetSocketAddress inetSocketAddress in trackerAddresses)
            {
                if (trackerServers.Length > 0) trackerServers += ",";
                trackerServers += inetSocketAddress.ToString().Substring(1); // ????
            }
        }
        return "{"
        + "\n  g_connect_timeout(ms) = " + g_connect_timeout
        + "\n  g_network_timeout(ms) = " + g_network_timeout
        + "\n  g_charset = " + g_charset
        + "\n  g_anti_steal_token = " + g_anti_steal_token
        + "\n  g_secret_key = " + g_secret_key
        + "\n  g_tracker_http_port = " + g_tracker_http_port
        + "\n  trackerServers = " + trackerServers
        + "\n}";
    }
}
