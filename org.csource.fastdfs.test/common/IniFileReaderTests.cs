using System;
using System.Collections.Generic;
using System.Text;

namespace org.csource.fastdfs.test.common
{
    public class IniFileReaderTests
    {
        public static void main(string[] args)
        {
            string conf_filename = "fdfs_client.conf";
            IniFileReader iniFileReader = new IniFileReader(conf_filename);
            Console.WriteLine("getConfFilename: " + iniFileReader.getConfFilename());
            Console.WriteLine("connect_timeout: " + iniFileReader.getIntValue("connect_timeout", 3));
            Console.WriteLine("network_timeout: " + iniFileReader.getIntValue("network_timeout", 45));
            Console.WriteLine("charset: " + iniFileReader.getStrValue("charset"));
            Console.WriteLine("http.tracker_http_port: " + iniFileReader.getIntValue("http.tracker_http_port", 8080));
            Console.WriteLine("http.anti_steal_token: " + iniFileReader.getBoolValue("http.anti_steal_token", false));
            Console.WriteLine("http.secret_key: " + iniFileReader.getStrValue("http.secret_key"));
            string[] tracker_servers = iniFileReader.getValues("tracker_server");
            if (tracker_servers != null)
            {
                Console.WriteLine("tracker_servers.Length: " + tracker_servers.Length);
                for (int i = 0; i < tracker_servers.Length; i++)
                {
                    Console.WriteLine(string.Format("tracker_servers[%s]: %s", i, tracker_servers[i]));
                }
            }
        }
    }
}
