using System;
using System.Collections.Generic;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    public class InetSocketAddress
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public InetSocketAddress(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}
