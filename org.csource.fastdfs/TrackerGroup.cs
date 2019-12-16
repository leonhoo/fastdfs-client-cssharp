using org.csource.fastdfs.encapsulation;
using System;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Tracker server group
    /// </summary>
    public class TrackerGroup
    {
        public int tracker_server_index;
        public InetSocketAddress[] tracker_servers;
        protected object locker;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tracker_servers">tracker servers</param>
        public TrackerGroup(InetSocketAddress[] tracker_servers)
        {
            this.tracker_servers = tracker_servers;
            this.locker = new object();
            this.tracker_server_index = 0;
        }

        /// <summary>
        /// return connected tracker server
        /// </summary>
        /// <returns> connected tracker server, null for fail</returns>
        public TrackerServer getConnection(int serverIndex)
        {
            JavaSocket sock = new JavaSocket();
            sock.SetReuseAddress(true);
            sock.setSoTimeout(ClientGlobal.g_network_timeout);
            sock.Connect(this.tracker_servers[serverIndex], ClientGlobal.g_connect_timeout);
            return new TrackerServer(sock, this.tracker_servers[serverIndex]);
        }

        /// <summary>
        /// return connected tracker server
        /// </summary>
        /// <returns> connected tracker server, null for fail</returns>
        public TrackerServer getConnection()
        {
            int current_index;
            lock (this.locker)
            {
                this.tracker_server_index++;
                if (this.tracker_server_index >= this.tracker_servers.Length)
                {
                    this.tracker_server_index = 0;
                }
                current_index = this.tracker_server_index;
            }
            try
            {
                return this.getConnection(current_index);
            }
            catch
            {
                Console.WriteLine("connect to server " + this.tracker_servers[current_index].Address + ":" + this.tracker_servers[current_index].Port + " fail");
            }
            for (int i = 0; i < this.tracker_servers.Length; i++)
            {
                if (i == current_index)
                {
                    continue;
                }
                try
                {
                    TrackerServer trackerServer = this.getConnection(i);
                    lock (this.locker)
                    {
                        if (this.tracker_server_index == current_index)
                        {
                            this.tracker_server_index = i;
                        }
                    }
                    return trackerServer;
                }
                catch
                {
                    Console.WriteLine("connect to server " + this.tracker_servers[i].Address + ":" + this.tracker_servers[i].Port + " fail");
                }
            }
            return null;
        }
        public object clone()
        {
            InetSocketAddress[] trackerServers = new InetSocketAddress[this.tracker_servers.Length];
            for (int i = 0; i < trackerServers.Length; i++)
            {
                trackerServers[i] = new InetSocketAddress(this.tracker_servers[i].Address, this.tracker_servers[i].Port);
            }
            return new TrackerGroup(trackerServers);
        }
    }
}
