using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace org.csource.fastdfs.pool
{
    public class ConnectionManager
    {

        private InetSocketAddress inetSocketAddress;

        /// <summary>
        /// total create connection pool
        /// </summary>
        private int totalCount = 0;

        /// <summary>
        /// free connection count
        /// </summary>
        private int freeCount = 0;

        /// <summary>
        /// lock
        /// </summary>
        private object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// free connections
        /// </summary>
        private LinkedList<Connection> freeConnections = new LinkedList<Connection>();

        private ConnectionManager()
        {

        }

        public ConnectionManager(InetSocketAddress socketAddress)
        {
            this.inetSocketAddress = socketAddress;
        }

        public Connection getConnection()
        {
            lock (locker)
            {
                try
                {
                    Connection connection = null;
                    while (true)
                    {
                        if (freeCount > 0)
                        {
                            Interlocked.Decrement(ref freeCount);

                            connection = freeConnections.First.Value;  //poll();
                            freeConnections.RemoveFirst();

                            if (!connection.isAvaliable() || (DateTime.Now.Ticks - connection.getLastAccessTime()) > ClientGlobal.g_connection_pool_max_idle_time)
                            {
                                closeConnection(connection);
                                continue;
                            }
                        }
                        else if (ClientGlobal.g_connection_pool_max_count_per_entry == 0 || totalCount < ClientGlobal.g_connection_pool_max_count_per_entry)
                        {
                            connection = ConnectionFactory.create(this.inetSocketAddress);
                            Interlocked.Increment(ref totalCount);
                        }
                        else
                        {
                            try
                            {
                                if (manualResetEvent.WaitOne(ClientGlobal.g_connection_pool_max_wait_time_in_ms))
                                {
                                    //wait single success
                                    continue;
                                }
                                throw new MyException("connect to server " + inetSocketAddress.Address + ":" + inetSocketAddress.Port + " fail, wait_time > " + ClientGlobal.g_connection_pool_max_wait_time_in_ms + "ms");
                            }
                            catch (Exception e)
                            {
                                throw new MyException("connect to server " + inetSocketAddress.Address + ":" + inetSocketAddress.Port + " fail, emsg:" + e.Message);
                            }
                        }
                        return connection;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void releaseConnection(Connection connection)
        {
            if (connection == null)
            {
                return;
            }
            lock (locker)
            {
                try
                {
                    connection.setLastAccessTime(DateTime.Now.Ticks);
                    freeConnections.AddLast(connection);
                    Interlocked.Increment(ref freeCount);
                    manualResetEvent.Set();
                }
                catch
                {
                }
            }
        }

        public void closeConnection(Connection connection)
        {
            try
            {
                if (connection != null)
                {
                    Interlocked.Decrement(ref totalCount);
                    connection.closeDirectly();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("close socket[" + inetSocketAddress.Address + ":" + inetSocketAddress.Port + "] error ,emsg:" + e.Message);
                throw e;
            }
        }


        public override string ToString()
        {
            return "ConnectionManager{" +
                    "ip:port='" + inetSocketAddress.Address + ":" + inetSocketAddress.Port +
                    ", totalCount=" + totalCount +
                    ", freeCount=" + freeCount +
                    ", freeConnections =" + freeConnections +
                    '}';
        }
    }
}
