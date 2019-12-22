using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.csource.fastdfs.encapsulation;
using System;

namespace org.csource.fastdfs
{
    /**
     * Created by James on 2017/5/19.
     */
    public class ClientGlobalTests
    {
        [TestMethod]
        public void ClientGlobalTest()
        {
            string trackerServers = "10.0.11.101:22122,10.0.11.102:22122";
            ClientGlobal.initByTrackers(trackerServers);
            Console.WriteLine("ClientGlobal.configInfo() : " + ClientGlobal.configInfo());

            string propFilePath = "fastdfs-client.properties";
            ClientGlobal.initByProperties(propFilePath);
            Console.WriteLine("ClientGlobal.configInfo() : " + ClientGlobal.configInfo());

            Properties props = new Properties();
            props.put(ClientGlobal.PROP_KEY_TRACKER_SERVERS, "10.0.11.101:22122,10.0.11.102:22122");
            ClientGlobal.initByProperties(props);
            Console.WriteLine("ClientGlobal.configInfo(): " + ClientGlobal.configInfo());

        }
    }
}