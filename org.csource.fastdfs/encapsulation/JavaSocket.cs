using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    public class JavaSocket : TcpClient
    {
        private InetSocketAddress socketAddress;
        public Stream getOutputStream()
        {
            return base.GetStream();
        }
        public Stream getInputStream()
        {
            return base.GetStream();
        }
        public void setSoTimeout(int g_network_timeout)
        {
            SendTimeout = g_network_timeout;
        }
        public void Connect(InetSocketAddress inetSocketAddress, int g_connect_timeout)
        {
            socketAddress = inetSocketAddress;
            base.Connect(inetSocketAddress.Address, inetSocketAddress.Port);
        }
        public void SetReuseAddress(bool v)
        {
            //base.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, v);
            base.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, v);
        }
        public int getPort()
        {
            return socketAddress.Port;
        }
        public string getInetAddress()
        {
            return socketAddress.Address;
        }
        public string getRemoteSocketAddress()
        {
            return socketAddress.Address;
        }
        public bool isInputShutdown()
        {
            return base.Connected;
        }
        public bool isOutputShutdown()
        {
            return base.Connected;
        }
    }
}
