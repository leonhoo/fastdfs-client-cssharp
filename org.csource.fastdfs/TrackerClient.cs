using org.csource.fastdfs.encapsulation;
using System;
using System.IO;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Tracker client
    /// </summary>
    public class TrackerClient
    {
        protected TrackerGroup tracker_group;
        protected byte errno;

        /// <summary>
        /// constructor with global tracker group
        /// </summary>
        public TrackerClient()
        {
            this.tracker_group = ClientGlobal.g_tracker_group;
        }

        /// <summary>
        /// constructor with specified tracker group
        /// </summary>
        /// <param name="tracker_group">the tracker group object</param>
        public TrackerClient(TrackerGroup tracker_group)
        {
            this.tracker_group = tracker_group;
        }

        /// <summary>
        /// get the error code of last call
        /// </summary>
        /// <returns> the error code of last call</returns>
        public byte getErrorCode()
        {
            return this.errno;
        }

        /// <summary>
        /// get a connection to tracker server
        /// </summary>
        /// <returns> tracker server JavaSocket object, return null if fail</returns>
        public TrackerServer getConnection()
        {
            return this.tracker_group.getConnection();
        }

        /// <summary>
        /// query storage server to upload file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <returns> storage server JavaSocket object, return null if fail</returns>
        public StorageServer getStoreStorage(TrackerServer trackerServer)
        {
            const string groupName = null;
            return this.getStoreStorage(trackerServer, groupName);
        }

        /// <summary>
        /// query storage server to upload file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name to upload file to, can be empty</param>
        /// <returns> storage server object, return null if fail</returns>
        public StorageServer getStoreStorage(TrackerServer trackerServer, string groupName)
        {
            byte[] header;
            string ip_addr;
            int port;
            byte cmd;
            int out_len;
            bool bNewConnection;
            byte store_path;
            JavaSocket trackerSocket;
            if (trackerServer == null)
            {
                trackerServer = getConnection();
                if (trackerServer == null)
                {
                    return null;
                }
                bNewConnection = true;
            }
            else
            {
                bNewConnection = false;
            }
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            try
            {
                if (groupName == null || groupName.Length == 0)
                {
                    cmd = ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITHOUT_GROUP_ONE;
                    out_len = 0;
                }
                else
                {
                    cmd = ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITH_GROUP_ONE;
                    out_len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                }
                header = ProtoCommon.packHeader(cmd, out_len, (byte)0);
                outStream.Write(header, 0, header.Length);
                if (groupName != null && groupName.Length > 0)
                {
                    byte[] bGroupName;
                    byte[] bs;
                    int group_len;
                    bs = ClientGlobal.g_charset.GetBytes(groupName);
                    bGroupName = new byte[ProtoCommon.FDFS_GROUP_NAME_MAX_LEN];
                    if (bs.Length <= ProtoCommon.FDFS_GROUP_NAME_MAX_LEN)
                    {
                        group_len = bs.Length;
                    }
                    else
                    {
                        group_len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                    }
                    Arrays.fill(bGroupName, (byte)0);
                    Array.Copy(bs, 0, bGroupName, 0, group_len);
                    outStream.Write(bGroupName, 0, bGroupName.Length);
                }
                ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
                ProtoCommon.TRACKER_PROTO_CMD_RESP,
                ProtoCommon.TRACKER_QUERY_STORAGE_STORE_BODY_LEN);
                this.errno = pkgInfo.errno;
                if (pkgInfo.errno != 0)
                {
                    return null;
                }
                ip_addr = Strings.Get(pkgInfo.body, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN, ProtoCommon.FDFS_IPADDR_SIZE - 1).Trim();
                port = (int)ProtoCommon.buff2long(pkgInfo.body, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN
                + ProtoCommon.FDFS_IPADDR_SIZE - 1);
                store_path = pkgInfo.body[ProtoCommon.TRACKER_QUERY_STORAGE_STORE_BODY_LEN - 1];
                return new StorageServer(ip_addr, port, store_path);
            }
            catch (IOException ex)
            {
                if (!bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
                throw ex;
            }
            finally
            {
                if (bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// query storage servers to upload file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name to upload file to, can be empty</param>
        /// <returns> storage servers, return null if fail</returns>
        public StorageServer[] getStoreStorages(TrackerServer trackerServer, string groupName)
        {
            byte[] header;
            string ip_addr;
            int port;
            byte cmd;
            int out_len;
            bool bNewConnection;
            JavaSocket trackerSocket;
            if (trackerServer == null)
            {
                trackerServer = getConnection();
                if (trackerServer == null)
                {
                    return null;
                }
                bNewConnection = true;
            }
            else
            {
                bNewConnection = false;
            }
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            try
            {
                if (groupName == null || groupName.Length == 0)
                {
                    cmd = ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITHOUT_GROUP_ALL;
                    out_len = 0;
                }
                else
                {
                    cmd = ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITH_GROUP_ALL;
                    out_len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                }
                header = ProtoCommon.packHeader(cmd, out_len, (byte)0);
                outStream.Write(header, 0, header.Length);
                if (groupName != null && groupName.Length > 0)
                {
                    byte[] bGroupName;
                    byte[] bs;
                    int group_len;
                    bs = ClientGlobal.g_charset.GetBytes(groupName);
                    bGroupName = new byte[ProtoCommon.FDFS_GROUP_NAME_MAX_LEN];
                    if (bs.Length <= ProtoCommon.FDFS_GROUP_NAME_MAX_LEN)
                    {
                        group_len = bs.Length;
                    }
                    else
                    {
                        group_len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                    }
                    Arrays.fill(bGroupName, (byte)0);
                    Array.Copy(bs, 0, bGroupName, 0, group_len);
                    outStream.Write(bGroupName, 0, bGroupName.Length);
                }
                ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
                ProtoCommon.TRACKER_PROTO_CMD_RESP, -1);
                this.errno = pkgInfo.errno;
                if (pkgInfo.errno != 0)
                {
                    return null;
                }
                if (pkgInfo.body.Length < ProtoCommon.TRACKER_QUERY_STORAGE_STORE_BODY_LEN)
                {
                    this.errno = ProtoCommon.ERR_NO_EINVAL;
                    return null;
                }
                int ipPortLen = pkgInfo.body.Length - (ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + 1);
                const int recordLength = ProtoCommon.FDFS_IPADDR_SIZE - 1 + ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
                if (ipPortLen % recordLength != 0)
                {
                    this.errno = ProtoCommon.ERR_NO_EINVAL;
                    return null;
                }
                int serverCount = ipPortLen / recordLength;
                if (serverCount > 16)
                {
                    this.errno = ProtoCommon.ERR_NO_ENOSPC;
                    return null;
                }
                StorageServer[] results = new StorageServer[serverCount];
                byte store_path = pkgInfo.body[pkgInfo.body.Length - 1];
                int offset = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                for (int i = 0; i < serverCount; i++)
                {
                    ip_addr = Strings.Get(pkgInfo.body, offset, ProtoCommon.FDFS_IPADDR_SIZE - 1).Trim();
                    offset += ProtoCommon.FDFS_IPADDR_SIZE - 1;
                    port = (int)ProtoCommon.buff2long(pkgInfo.body, offset);
                    offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
                    results[i] = new StorageServer(ip_addr, port, store_path);
                }
                return results;
            }
            catch (IOException ex)
            {
                if (!bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
                throw ex;
            }
            finally
            {
                if (bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// query storage server to download file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="filename">filename on storage server</param>
        /// <returns> storage server JavaSocket object, return null if fail</returns>
        public StorageServer getFetchStorage(TrackerServer trackerServer,
        string groupName, string filename)
        {
            ServerInfo[] servers = this.getStorages(trackerServer, ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_FETCH_ONE,
            groupName, filename);
            if (servers == null)
            {
                return null;
            }
            else
            {
                return new StorageServer(servers[0].getIpAddr(), servers[0].getPort(), 0);
            }
        }

        /// <summary>
        /// query storage server to update file (delete file or set meta data)
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="filename">filename on storage server</param>
        /// <returns> storage server JavaSocket object, return null if fail</returns>
        public StorageServer getUpdateStorage(TrackerServer trackerServer,
        string groupName, string filename)
        {
            ServerInfo[] servers = this.getStorages(trackerServer, ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_UPDATE,
            groupName, filename);
            if (servers == null)
            {
                return null;
            }
            else
            {
                return new StorageServer(servers[0].getIpAddr(), servers[0].getPort(), 0);
            }
        }

        /// <summary>
        /// get storage servers to download file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="filename">filename on storage server</param>
        /// <returns> storage servers, return null if fail</returns>
        public ServerInfo[] getFetchStorages(TrackerServer trackerServer,
        string groupName, string filename)
        {
            return this.getStorages(trackerServer, ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_FETCH_ALL,
            groupName, filename);
        }

        /// <summary>
        /// query storage server to download file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="cmd">command code, ProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_FETCH_ONE orProtoCommon.TRACKER_PROTO_CMD_SERVICE_QUERY_UPDATE</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="filename">filename on storage server</param>
        /// <returns> storage server JavaSocket object, return null if fail</returns>
        protected ServerInfo[] getStorages(TrackerServer trackerServer,
        byte cmd, string groupName, string filename)
        {
            byte[] header;
            byte[] bFileName;
            byte[] bGroupName;
            byte[] bs;
            int len;
            string ip_addr;
            int port;
            bool bNewConnection;
            JavaSocket trackerSocket;
            if (trackerServer == null)
            {
                trackerServer = getConnection();
                if (trackerServer == null)
                {
                    return null;
                }
                bNewConnection = true;
            }
            else
            {
                bNewConnection = false;
            }
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            try
            {
                bs = ClientGlobal.g_charset.GetBytes(groupName);
                bGroupName = new byte[ProtoCommon.FDFS_GROUP_NAME_MAX_LEN];
                bFileName = ClientGlobal.g_charset.GetBytes(filename);
                if (bs.Length <= ProtoCommon.FDFS_GROUP_NAME_MAX_LEN)
                {
                    len = bs.Length;
                }
                else
                {
                    len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                }
                Arrays.fill(bGroupName, (byte)0);
                Array.Copy(bs, 0, bGroupName, 0, len);
                header = ProtoCommon.packHeader(cmd, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + bFileName.Length, (byte)0);
                byte[] wholePkg = new byte[header.Length + bGroupName.Length + bFileName.Length];
                Array.Copy(header, 0, wholePkg, 0, header.Length);
                Array.Copy(bGroupName, 0, wholePkg, header.Length, bGroupName.Length);
                Array.Copy(bFileName, 0, wholePkg, header.Length + bGroupName.Length, bFileName.Length);
                outStream.Write(wholePkg, 0, wholePkg.Length);
                ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
                ProtoCommon.TRACKER_PROTO_CMD_RESP, -1);
                this.errno = pkgInfo.errno;
                if (pkgInfo.errno != 0)
                {
                    return null;
                }
                if (pkgInfo.body.Length < ProtoCommon.TRACKER_QUERY_STORAGE_FETCH_BODY_LEN)
                {
                    throw new IOException("Invalid body length: " + pkgInfo.body.Length);
                }
                if ((pkgInfo.body.Length - ProtoCommon.TRACKER_QUERY_STORAGE_FETCH_BODY_LEN) % (ProtoCommon.FDFS_IPADDR_SIZE - 1) != 0)
                {
                    throw new IOException("Invalid body length: " + pkgInfo.body.Length);
                }
                int server_count = 1 + (pkgInfo.body.Length - ProtoCommon.TRACKER_QUERY_STORAGE_FETCH_BODY_LEN) / (ProtoCommon.FDFS_IPADDR_SIZE - 1);
                ip_addr = Strings.Get(pkgInfo.body, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN, ProtoCommon.FDFS_IPADDR_SIZE - 1).Trim();
                int offset = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + ProtoCommon.FDFS_IPADDR_SIZE - 1;
                port = (int)ProtoCommon.buff2long(pkgInfo.body, offset);
                offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
                ServerInfo[] servers = new ServerInfo[server_count];
                servers[0] = new ServerInfo(ip_addr, port);
                for (int i = 1; i < server_count; i++)
                {
                    servers[i] = new ServerInfo(Strings.Get(pkgInfo.body, offset, ProtoCommon.FDFS_IPADDR_SIZE - 1).Trim(), port);
                    offset += ProtoCommon.FDFS_IPADDR_SIZE - 1;
                }
                return servers;
            }
            catch (IOException ex)
            {
                if (!bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
                throw ex;
            }
            finally
            {
                if (bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// query storage server to download file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> storage server JavaSocket object, return null if fail</returns>
        public StorageServer getFetchStorage1(TrackerServer trackerServer, string file_id)
        {
            string[] parts = new string[2];
            this.errno = StorageClient1.split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.getFetchStorage(trackerServer, parts[0], parts[1]);
        }

        /// <summary>
        /// get storage servers to download file
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> storage servers, return null if fail</returns>
        public ServerInfo[] getFetchStorages1(TrackerServer trackerServer, string file_id)
        {
            string[] parts = new string[2];
            this.errno = StorageClient1.split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.getFetchStorages(trackerServer, parts[0], parts[1]);
        }

        /// <summary>
        /// list groups
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <returns> group stat array, return null if fail</returns>
        public StructGroupStat[] listGroups(TrackerServer trackerServer)
        {
            byte[] header;
            //string ip_addr;
            //int port;
            //byte cmd;
            //int out_len;
            bool bNewConnection;
            //byte store_path;
            JavaSocket trackerSocket;
            if (trackerServer == null)
            {
                trackerServer = getConnection();
                if (trackerServer == null)
                {
                    return null;
                }
                bNewConnection = true;
            }
            else
            {
                bNewConnection = false;
            }
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            try
            {
                header = ProtoCommon.packHeader(ProtoCommon.TRACKER_PROTO_CMD_SERVER_LIST_GROUP, 0, (byte)0);
                outStream.Write(header, 0, header.Length);
                ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
                ProtoCommon.TRACKER_PROTO_CMD_RESP, -1);
                this.errno = pkgInfo.errno;
                if (pkgInfo.errno != 0)
                {
                    return null;
                }
                ProtoStructDecoder<StructGroupStat> decoder = new ProtoStructDecoder<StructGroupStat>();
                return decoder.decode(pkgInfo.body, StructGroupStat.getFieldsTotalSize());
            }
            catch (IOException ex)
            {
                if (!bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                this.errno = ProtoCommon.ERR_NO_EINVAL;
                throw ex;
            }
            finally
            {
                if (bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// query storage server stat info of the group
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <returns> storage server stat array, return null if fail</returns>
        public StructStorageStat[] listStorages(TrackerServer trackerServer, string groupName)
        {
            const string storageIpAddr = null;
            return this.listStorages(trackerServer, groupName, storageIpAddr);
        }

        /// <summary>
        /// query storage server stat info of the group
        /// </summary>
        /// <param name="trackerServer">the tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="storageIpAddr">the storage server ip address, can be null or empty</param>
        /// <returns> storage server stat array, return null if fail</returns>
        public StructStorageStat[] listStorages(TrackerServer trackerServer,
        string groupName, string storageIpAddr)
        {
            byte[] header;
            byte[] bGroupName;
            byte[] bs;
            int len;
            bool bNewConnection;
            JavaSocket trackerSocket;
            if (trackerServer == null)
            {
                trackerServer = getConnection();
                if (trackerServer == null)
                {
                    return null;
                }
                bNewConnection = true;
            }
            else
            {
                bNewConnection = false;
            }
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            try
            {
                bs = ClientGlobal.g_charset.GetBytes(groupName);
                bGroupName = new byte[ProtoCommon.FDFS_GROUP_NAME_MAX_LEN];
                if (bs.Length <= ProtoCommon.FDFS_GROUP_NAME_MAX_LEN)
                {
                    len = bs.Length;
                }
                else
                {
                    len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
                }
                Arrays.fill(bGroupName, (byte)0);
                Array.Copy(bs, 0, bGroupName, 0, len);
                int ipAddrLen;
                byte[] bIpAddr;
                if (storageIpAddr != null && storageIpAddr.Length > 0)
                {
                    bIpAddr = ClientGlobal.g_charset.GetBytes(storageIpAddr);
                    if (bIpAddr.Length < ProtoCommon.FDFS_IPADDR_SIZE)
                    {
                        ipAddrLen = bIpAddr.Length;
                    }
                    else
                    {
                        ipAddrLen = ProtoCommon.FDFS_IPADDR_SIZE - 1;
                    }
                }
                else
                {
                    bIpAddr = null;
                    ipAddrLen = 0;
                }
                header = ProtoCommon.packHeader(ProtoCommon.TRACKER_PROTO_CMD_SERVER_LIST_STORAGE, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + ipAddrLen, (byte)0);
                byte[] wholePkg = new byte[header.Length + bGroupName.Length + ipAddrLen];
                Array.Copy(header, 0, wholePkg, 0, header.Length);
                Array.Copy(bGroupName, 0, wholePkg, header.Length, bGroupName.Length);
                if (ipAddrLen > 0)
                {
                    Array.Copy(bIpAddr, 0, wholePkg, header.Length + bGroupName.Length, ipAddrLen);
                }
                outStream.Write(wholePkg, 0, wholePkg.Length);
                ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
                ProtoCommon.TRACKER_PROTO_CMD_RESP, -1);
                this.errno = pkgInfo.errno;
                if (pkgInfo.errno != 0)
                {
                    return null;
                }
                ProtoStructDecoder<StructStorageStat> decoder = new ProtoStructDecoder<StructStorageStat>();
                return decoder.decode(pkgInfo.body, StructStorageStat.getFieldsTotalSize());
            }
            catch (IOException ex)
            {
                if (!bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                this.errno = ProtoCommon.ERR_NO_EINVAL;
                throw ex;
            }
            finally
            {
                if (bNewConnection)
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// delete a storage server from the tracker server
        /// </summary>
        /// <param name="trackerServer">the connected tracker server</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="storageIpAddr">the storage server ip address</param>
        /// <returns> true for success, false for fail</returns>
        private bool deleteStorage(TrackerServer trackerServer,
        string groupName, string storageIpAddr)
        {
            byte[] header;
            byte[] bGroupName;
            byte[] bs;
            int len;
            JavaSocket trackerSocket;
            trackerSocket = trackerServer.getSocket();
            Stream outStream = trackerSocket.getOutputStream();
            bs = ClientGlobal.g_charset.GetBytes(groupName);
            bGroupName = new byte[ProtoCommon.FDFS_GROUP_NAME_MAX_LEN];
            if (bs.Length <= ProtoCommon.FDFS_GROUP_NAME_MAX_LEN)
            {
                len = bs.Length;
            }
            else
            {
                len = ProtoCommon.FDFS_GROUP_NAME_MAX_LEN;
            }
            Arrays.fill(bGroupName, (byte)0);
            Array.Copy(bs, 0, bGroupName, 0, len);
            int ipAddrLen;
            byte[] bIpAddr = ClientGlobal.g_charset.GetBytes(storageIpAddr);
            if (bIpAddr.Length < ProtoCommon.FDFS_IPADDR_SIZE)
            {
                ipAddrLen = bIpAddr.Length;
            }
            else
            {
                ipAddrLen = ProtoCommon.FDFS_IPADDR_SIZE - 1;
            }
            header = ProtoCommon.packHeader(ProtoCommon.TRACKER_PROTO_CMD_SERVER_DELETE_STORAGE, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + ipAddrLen, (byte)0);
            byte[] wholePkg = new byte[header.Length + bGroupName.Length + ipAddrLen];
            Array.Copy(header, 0, wholePkg, 0, header.Length);
            Array.Copy(bGroupName, 0, wholePkg, header.Length, bGroupName.Length);
            Array.Copy(bIpAddr, 0, wholePkg, header.Length + bGroupName.Length, ipAddrLen);
            outStream.Write(wholePkg, 0, wholePkg.Length);
            ProtoCommon.RecvPackageInfo pkgInfo = ProtoCommon.recvPackage(trackerSocket.getInputStream(),
            ProtoCommon.TRACKER_PROTO_CMD_RESP, 0);
            this.errno = pkgInfo.errno;
            return pkgInfo.errno == 0;
        }

        /// <summary>
        /// delete a storage server from the global FastDFS cluster
        /// </summary>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="storageIpAddr">the storage server ip address</param>
        /// <returns> true for success, false for fail</returns>
        public bool deleteStorage(string groupName, string storageIpAddr)
        {
            return this.deleteStorage(ClientGlobal.g_tracker_group, groupName, storageIpAddr);
        }

        /// <summary>
        /// delete a storage server from the FastDFS cluster
        /// </summary>
        /// <param name="trackerGroup">the tracker server group</param>
        /// <param name="groupName">the group name of storage server</param>
        /// <param name="storageIpAddr">the storage server ip address</param>
        /// <returns> true for success, false for fail</returns>
        public bool deleteStorage(TrackerGroup trackerGroup,
        string groupName, string storageIpAddr)
        {
            int serverIndex;
            int notFoundCount;
            TrackerServer trackerServer;
            notFoundCount = 0;
            for (serverIndex = 0; serverIndex < trackerGroup.tracker_servers.Length; serverIndex++)
            {
                try
                {
                    trackerServer = trackerGroup.getConnection(serverIndex);
                }
                catch
                {
                    this.errno = ProtoCommon.ECONNREFUSED;
                    return false;
                }
                try
                {
                    StructStorageStat[] storageStats = listStorages(trackerServer, groupName, storageIpAddr);
                    if (storageStats == null)
                    {
                        if (this.errno == ProtoCommon.ERR_NO_ENOENT)
                        {
                            notFoundCount++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (storageStats.Length == 0)
                    {
                        notFoundCount++;
                    }
                    else if (storageStats[0].getStatus() == ProtoCommon.FDFS_STORAGE_STATUS_ONLINE ||
                    storageStats[0].getStatus() == ProtoCommon.FDFS_STORAGE_STATUS_ACTIVE)
                    {
                        this.errno = ProtoCommon.ERR_NO_EBUSY;
                        return false;
                    }
                }
                finally
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
            if (notFoundCount == trackerGroup.tracker_servers.Length)
            {
                this.errno = ProtoCommon.ERR_NO_ENOENT;
                return false;
            }
            notFoundCount = 0;
            for (serverIndex = 0; serverIndex < trackerGroup.tracker_servers.Length; serverIndex++)
            {
                try
                {
                    trackerServer = trackerGroup.getConnection(serverIndex);
                }
                catch
                {
                    Console.WriteLine("connect to server " + trackerGroup.tracker_servers[serverIndex].Address + ":" + trackerGroup.tracker_servers[serverIndex].Port + " fail");
                    this.errno = ProtoCommon.ECONNREFUSED;
                    return false;
                }
                try
                {
                    if (!this.deleteStorage(trackerServer, groupName, storageIpAddr))
                    {
                        if (this.errno != 0)
                        {
                            if (this.errno == ProtoCommon.ERR_NO_ENOENT)
                            {
                                notFoundCount++;
                            }
                            else if (this.errno != ProtoCommon.ERR_NO_EALREADY)
                            {
                                return false;
                            }
                        }
                    }
                }
                finally
                {
                    try
                    {
                        trackerServer.close();
                    }
                    catch
                    {
                    }
                }
            }
            if (notFoundCount == trackerGroup.tracker_servers.Length)
            {
                this.errno = ProtoCommon.ERR_NO_ENOENT;
                return false;
            }
            if (this.errno == ProtoCommon.ERR_NO_ENOENT)
            {
                this.errno = 0;
            }
            return this.errno == 0;
        }
    }
}
