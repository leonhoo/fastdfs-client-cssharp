using System;
using System.Collections.Generic;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    public class InetSocketAddress
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public InetSocketAddress(string addressAndPort)
        {
            var tmp = addressAndPort.Split(':');
            Address = tmp[0].Trim();
            Port = int.Parse(tmp[1].Trim());
        }

        public InetSocketAddress(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}
