using org.csource.fastdfs.encapsulation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace org.csource.fastdfs.pool
{
    public class ConnectionPool
    {
        /**
         * key is ip:port, value is ConnectionManager
         */
        private readonly static ConcurrentDictionary<string, ConnectionManager> CP = new ConcurrentDictionary<string, ConnectionManager>();
        private readonly static object locker = new object();
        public static Connection getConnection(InetSocketAddress socketAddress)
        {
            if (socketAddress == null)
            {
                return null;
            }
            string key = getKey(socketAddress);
            CP.TryGetValue(key, out ConnectionManager connectionManager);
            if (connectionManager == null)
            {
                lock (locker)
                {
                    CP.TryGetValue(key, out connectionManager);
                    if (connectionManager == null)
                    {
                        connectionManager = new ConnectionManager(socketAddress);
                        CP[key] = connectionManager;
                    }
                }
            }
            return connectionManager.getConnection();
        }

        public static void releaseConnection(Connection connection)
        {
            if (connection == null)
            {
                return;
            }
            string key = getKey(connection.getInetSocketAddress());
            CP.TryGetValue(key, out ConnectionManager connectionManager);
            if (connectionManager != null)
            {
                connectionManager.releaseConnection(connection);
            }
            else
            {
                connection.closeDirectly();
            }

        }

        public static void closeConnection(Connection connection)
        {
            if (connection == null)
            {
                return;
            }
            string key = getKey(connection.getInetSocketAddress());
            CP.TryGetValue(key, out ConnectionManager connectionManager);
            if (connectionManager != null)
            {
                connectionManager.closeConnection(connection);
            }
            else
            {
                connection.closeDirectly();
            }
        }

        private static string getKey(InetSocketAddress socketAddress)
        {
            if (socketAddress == null)
            {
                return null;
            }
            return string.Format("{0}:{1}", socketAddress.Address, socketAddress.Port);
        }


        public override string ToString()
        {
            if (!CP.IsEmpty)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var managerEntry in CP)
                {
                    builder.Append("key:[" + managerEntry.Key + " ]-------- entry:" + managerEntry.Value + "\n");
                }
                return builder.ToString();
            }
            return null;
        }
    }
}
