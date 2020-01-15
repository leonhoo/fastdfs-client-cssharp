using org.csource.fastdfs.encapsulation;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Storage Server Info
    /// </summary>
    public class StorageServer : TrackerServer
    {
        protected int store_path_index = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip_addr">the ip address of storage server</param>
        /// <param name="port">the port of storage server</param>
        /// <param name="store_path">the store path index on the storage server</param>
        public StorageServer(string ip_addr, int port, int store_path) : base(new InetSocketAddress(ip_addr, port))
        {
            this.store_path_index = store_path;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip_addr">the ip address of storage server</param>
        /// <param name="port">the port of storage server</param>
        /// <param name="store_path">the store path index on the storage server</param>
        public StorageServer(string ip_addr, int port, byte store_path) : base(new InetSocketAddress(ip_addr, port))
        {
            if (store_path < 0)
            {
                this.store_path_index = 256 + store_path;
            }
            else
            {
                this.store_path_index = store_path;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> the store path index on the storage server</returns>
        public int getStorePathIndex()
        {
            return this.store_path_index;
        }
    }
}
