using org.csource.fastdfs.encapsulation;
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
        protected JavaSocket sock;
        protected InetSocketAddress inetSockAddr;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sock">JavaSocket of server</param>
        /// <param name="inetSockAddr">the server info</param>
        public TrackerServer(JavaSocket sock, InetSocketAddress inetSockAddr)
        {
            this.sock = sock;
            this.inetSockAddr = inetSockAddr;
        }

        /// <summary>
        /// get the connected socket
        /// </summary>
        /// <returns> the socket</returns>
        public JavaSocket getSocket()
        {
            if (this.sock == null)
            {
                this.sock = ClientGlobal.getSocket(this.inetSockAddr);
            }
            return this.sock;
        }

        /// <summary>
        /// get the server info
        /// </summary>
        /// <returns> the server info</returns>
        public InetSocketAddress getInetSocketAddress()
        {
            return this.inetSockAddr;
        }
        public Stream getOutputStream()
        {
            return this.sock.getOutputStream();
        }
        public Stream getInputStream()
        {
            return this.sock.getInputStream();
        }
        public void close()
        {
            if (this.sock != null)
            {
                try
                {
                    ProtoCommon.closeSocket(this.sock);
                }
                finally
                {
                    this.sock = null;
                }
            }
        }
        protected void finalize()
        {
            this.close();
        }
        public bool isConnected()
        {
            bool isConnected = false;
            if (sock != null)
            {
                if (sock.Connected)
                {
                    isConnected = true;
                }
            }
            return isConnected;
        }
        public bool isAvaliable()
        {
            if (isConnected())
            {
                if (sock.getPort() == 0)
                {
                    return false;
                }
                if (sock.getInetAddress() == null)
                {
                    return false;
                }
                if (sock.getRemoteSocketAddress() == null)
                {
                    return false;
                }
                if (sock.isInputShutdown())
                {
                    return false;
                }
                if (sock.isOutputShutdown())
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
