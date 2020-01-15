using org.csource.fastdfs.encapsulation;
using org.csource.fastdfs.pool;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Tracker Server Info
    /// </summary>
    public class TrackerServer
    {
        protected InetSocketAddress inetSockAddr;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sock">JavaSocket of server</param>
        /// <param name="inetSockAddr">the server info</param>
        public TrackerServer(InetSocketAddress inetSockAddr)
        {
            this.inetSockAddr = inetSockAddr;
        }

        public Connection getConnection()
        {
            Connection connection;
            if (ClientGlobal.g_connection_pool_enabled)
            {
                connection = ConnectionPool.getConnection(this.inetSockAddr);
            }
            else
            {
                connection = ConnectionFactory.create(this.inetSockAddr);
            }
            return connection;
        }

        /// <summary>
        /// get the server info
        /// </summary>
        /// <returns> the server info</returns>
        public InetSocketAddress getInetSocketAddress()
        {
            return this.inetSockAddr;
        }
    }
}
