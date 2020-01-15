using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.csource.fastdfs.pool
{
    public class ConnectionFactory
    {
        /// <summary>
        /// create from InetSocketAddress
        /// </summary>
        /// <param name="socketAddress"></param>
        /// <returns></returns>
        public static Connection create(InetSocketAddress socketAddress)
        {
            try
            {
                JavaSocket sock = new JavaSocket();
                sock.SetReuseAddress(true);
                sock.setSoTimeout(ClientGlobal.g_network_timeout);
                sock.Connect(socketAddress, ClientGlobal.g_connect_timeout);
                return new Connection(sock, socketAddress);
            }
            catch (Exception e)
            {
                throw new MyException("connect to server " + socketAddress.Address + ":" + socketAddress.Port + " fail, emsg:" + e.Message);
            }
        }
    }
}
