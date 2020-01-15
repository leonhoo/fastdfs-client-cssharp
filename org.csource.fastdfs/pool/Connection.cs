using org.csource.fastdfs.encapsulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace org.csource.fastdfs.pool
{
    public class Connection
    {

        private JavaSocket sock;
        private InetSocketAddress inetSockAddr;
        private long lastAccessTime = DateTime.Now.Ticks;

        public Connection(JavaSocket sock, InetSocketAddress inetSockAddr)
        {
            this.sock = sock;
            this.inetSockAddr = inetSockAddr;
        }

        /**
         * get the server info
         *
         * @return the server info
         */
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

        public long getLastAccessTime()
        {
            return lastAccessTime;
        }

        public void setLastAccessTime(long lastAccessTime)
        {
            this.lastAccessTime = lastAccessTime;
        }

        /**
         *
         * @throws IOException
         */
        public void close()
        {
            //if connection enabled get from connection pool
            if (ClientGlobal.g_connection_pool_enabled)
            {
                ConnectionPool.closeConnection(this);
            }
            else
            {
                this.closeDirectly();
            }
        }

        public void release()
        {
            if (ClientGlobal.g_connection_pool_enabled)
            {
                ConnectionPool.releaseConnection(this);
            }
            else
            {
                this.closeDirectly();
            }
        }

        /**
         * force close socket,
         */
        public void closeDirectly()
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

        public bool activeTest()
        {
            if (this.sock == null)
            {
                return false;
            }
            return ProtoCommon.activeTest(this.sock);
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

        public override string ToString()
        {
            return "TrackerServer{" +
                    "sock=" + sock +
                    ", inetSockAddr=" + inetSockAddr +
                    '}';
        }
    }
}
