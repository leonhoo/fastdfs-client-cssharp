using org.csource.fastdfs.encapsulation;

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
    /// Server Info
    ///
    /// @author Happy Fish / YuQing
    /// @version Version 1.7
    /// </summary>
    public class ServerInfo
    {
        protected string ip_addr;
        protected int port;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip_addr">address of the server</param>
        /// <param name="port">the port of the server</param>
        public ServerInfo(string ip_addr, int port)
        {
            this.ip_addr = ip_addr;
            this.port = port;
        }

        /// <summary>
        /// return the ip address
        /// </summary>
        /// <returns> the ip address</returns>
        public string getIpAddr()
        {
            return this.ip_addr;
        }

        /// <summary>
        /// return the port of the server
        /// </summary>
        /// <returns> the port of the server</returns>
        public int getPort()
        {
            return this.port;
        }

        /// <summary>
        /// connect to server
        /// </summary>
        /// <returns> connected JavaSocket object</returns>
        public JavaSocket connect()
        {
            JavaSocket sock = new JavaSocket();
            sock.SetReuseAddress(true);
            sock.setSoTimeout(ClientGlobal.g_network_timeout);
            sock.Connect(new InetSocketAddress(ip_addr, port), ClientGlobal.g_connect_timeout);
            return sock;
        }
    }
}
