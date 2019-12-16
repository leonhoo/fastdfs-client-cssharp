using System;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// C struct body decoder
    /// </summary>
    public class StructStorageStat : StructBase
    {
        protected const int FIELD_INDEX_STATUS = 0;
        protected const int FIELD_INDEX_ID = 1;
        protected const int FIELD_INDEX_IP_ADDR = 2;
        protected const int FIELD_INDEX_DOMAIN_NAME = 3;
        protected const int FIELD_INDEX_SRC_IP_ADDR = 4;
        protected const int FIELD_INDEX_VERSION = 5;
        protected const int FIELD_INDEX_JOIN_TIME = 6;
        protected const int FIELD_INDEX_UP_TIME = 7;
        protected const int FIELD_INDEX_TOTAL_MB = 8;
        protected const int FIELD_INDEX_FREE_MB = 9;
        protected const int FIELD_INDEX_UPLOAD_PRIORITY = 10;
        protected const int FIELD_INDEX_STORE_PATH_COUNT = 11;
        protected const int FIELD_INDEX_SUBDIR_COUNT_PER_PATH = 12;
        protected const int FIELD_INDEX_CURRENT_WRITE_PATH = 13;
        protected const int FIELD_INDEX_STORAGE_PORT = 14;
        protected const int FIELD_INDEX_STORAGE_HTTP_PORT = 15;
        protected const int FIELD_INDEX_CONNECTION_ALLOC_COUNT = 16;
        protected const int FIELD_INDEX_CONNECTION_CURRENT_COUNT = 17;
        protected const int FIELD_INDEX_CONNECTION_MAX_COUNT = 18;
        protected const int FIELD_INDEX_TOTAL_UPLOAD_COUNT = 19;
        protected const int FIELD_INDEX_SUCCESS_UPLOAD_COUNT = 20;
        protected const int FIELD_INDEX_TOTAL_APPEND_COUNT = 21;
        protected const int FIELD_INDEX_SUCCESS_APPEND_COUNT = 22;
        protected const int FIELD_INDEX_TOTAL_MODIFY_COUNT = 23;
        protected const int FIELD_INDEX_SUCCESS_MODIFY_COUNT = 24;
        protected const int FIELD_INDEX_TOTAL_TRUNCATE_COUNT = 25;
        protected const int FIELD_INDEX_SUCCESS_TRUNCATE_COUNT = 26;
        protected const int FIELD_INDEX_TOTAL_SET_META_COUNT = 27;
        protected const int FIELD_INDEX_SUCCESS_SET_META_COUNT = 28;
        protected const int FIELD_INDEX_TOTAL_DELETE_COUNT = 29;
        protected const int FIELD_INDEX_SUCCESS_DELETE_COUNT = 30;
        protected const int FIELD_INDEX_TOTAL_DOWNLOAD_COUNT = 31;
        protected const int FIELD_INDEX_SUCCESS_DOWNLOAD_COUNT = 32;
        protected const int FIELD_INDEX_TOTAL_GET_META_COUNT = 33;
        protected const int FIELD_INDEX_SUCCESS_GET_META_COUNT = 34;
        protected const int FIELD_INDEX_TOTAL_CREATE_LINK_COUNT = 35;
        protected const int FIELD_INDEX_SUCCESS_CREATE_LINK_COUNT = 36;
        protected const int FIELD_INDEX_TOTAL_DELETE_LINK_COUNT = 37;
        protected const int FIELD_INDEX_SUCCESS_DELETE_LINK_COUNT = 38;
        protected const int FIELD_INDEX_TOTAL_UPLOAD_BYTES = 39;
        protected const int FIELD_INDEX_SUCCESS_UPLOAD_BYTES = 40;
        protected const int FIELD_INDEX_TOTAL_APPEND_BYTES = 41;
        protected const int FIELD_INDEX_SUCCESS_APPEND_BYTES = 42;
        protected const int FIELD_INDEX_TOTAL_MODIFY_BYTES = 43;
        protected const int FIELD_INDEX_SUCCESS_MODIFY_BYTES = 44;
        protected const int FIELD_INDEX_TOTAL_DOWNLOAD_BYTES = 45;
        protected const int FIELD_INDEX_SUCCESS_DOWNLOAD_BYTES = 46;
        protected const int FIELD_INDEX_TOTAL_SYNC_IN_BYTES = 47;
        protected const int FIELD_INDEX_SUCCESS_SYNC_IN_BYTES = 48;
        protected const int FIELD_INDEX_TOTAL_SYNC_OUT_BYTES = 49;
        protected const int FIELD_INDEX_SUCCESS_SYNC_OUT_BYTES = 50;
        protected const int FIELD_INDEX_TOTAL_FILE_OPEN_COUNT = 51;
        protected const int FIELD_INDEX_SUCCESS_FILE_OPEN_COUNT = 52;
        protected const int FIELD_INDEX_TOTAL_FILE_READ_COUNT = 53;
        protected const int FIELD_INDEX_SUCCESS_FILE_READ_COUNT = 54;
        protected const int FIELD_INDEX_TOTAL_FILE_WRITE_COUNT = 55;
        protected const int FIELD_INDEX_SUCCESS_FILE_WRITE_COUNT = 56;
        protected const int FIELD_INDEX_LAST_SOURCE_UPDATE = 57;
        protected const int FIELD_INDEX_LAST_SYNC_UPDATE = 58;
        protected const int FIELD_INDEX_LAST_SYNCED_TIMESTAMP = 59;
        protected const int FIELD_INDEX_LAST_HEART_BEAT_TIME = 60;
        protected const int FIELD_INDEX_IF_TRUNK_FILE = 61;
        protected static int fieldsTotalSize;
        protected static StructBase.FieldInfo[] fieldsArray = new StructBase.FieldInfo[62];
        static StructStorageStat()
        {
            int offset = 0;
            fieldsArray[FIELD_INDEX_STATUS] = new StructBase.FieldInfo("status", offset, 1);
            offset += 1;
            fieldsArray[FIELD_INDEX_ID] = new StructBase.FieldInfo("id", offset, ProtoCommon.FDFS_STORAGE_ID_MAX_SIZE);
            offset += ProtoCommon.FDFS_STORAGE_ID_MAX_SIZE;
            fieldsArray[FIELD_INDEX_IP_ADDR] = new StructBase.FieldInfo("ipAddr", offset, ProtoCommon.FDFS_IPADDR_SIZE);
            offset += ProtoCommon.FDFS_IPADDR_SIZE;
            fieldsArray[FIELD_INDEX_DOMAIN_NAME] = new StructBase.FieldInfo("domainName", offset, ProtoCommon.FDFS_DOMAIN_NAME_MAX_SIZE);
            offset += ProtoCommon.FDFS_DOMAIN_NAME_MAX_SIZE;
            fieldsArray[FIELD_INDEX_SRC_IP_ADDR] = new StructBase.FieldInfo("srcIpAddr", offset, ProtoCommon.FDFS_IPADDR_SIZE);
            offset += ProtoCommon.FDFS_IPADDR_SIZE;
            fieldsArray[FIELD_INDEX_VERSION] = new StructBase.FieldInfo("version", offset, ProtoCommon.FDFS_VERSION_SIZE);
            offset += ProtoCommon.FDFS_VERSION_SIZE;
            fieldsArray[FIELD_INDEX_JOIN_TIME] = new StructBase.FieldInfo("joinTime", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_UP_TIME] = new StructBase.FieldInfo("upTime", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_MB] = new StructBase.FieldInfo("totalMB", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_FREE_MB] = new StructBase.FieldInfo("freeMB", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_UPLOAD_PRIORITY] = new StructBase.FieldInfo("uploadPriority", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORE_PATH_COUNT] = new StructBase.FieldInfo("storePathCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUBDIR_COUNT_PER_PATH] = new StructBase.FieldInfo("subdirCountPerPath", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_CURRENT_WRITE_PATH] = new StructBase.FieldInfo("currentWritePath", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORAGE_PORT] = new StructBase.FieldInfo("storagePort", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORAGE_HTTP_PORT] = new StructBase.FieldInfo("storageHttpPort", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_CONNECTION_ALLOC_COUNT] = new StructBase.FieldInfo("connectionAllocCount", offset, 4);
            offset += 4;
            fieldsArray[FIELD_INDEX_CONNECTION_CURRENT_COUNT] = new StructBase.FieldInfo("connectionCurrentCount", offset, 4);
            offset += 4;
            fieldsArray[FIELD_INDEX_CONNECTION_MAX_COUNT] = new StructBase.FieldInfo("connectionMaxCount", offset, 4);
            offset += 4;
            fieldsArray[FIELD_INDEX_TOTAL_UPLOAD_COUNT] = new StructBase.FieldInfo("totalUploadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_UPLOAD_COUNT] = new StructBase.FieldInfo("successUploadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_APPEND_COUNT] = new StructBase.FieldInfo("totalAppendCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_APPEND_COUNT] = new StructBase.FieldInfo("successAppendCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_MODIFY_COUNT] = new StructBase.FieldInfo("totalModifyCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_MODIFY_COUNT] = new StructBase.FieldInfo("successModifyCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_TRUNCATE_COUNT] = new StructBase.FieldInfo("totalTruncateCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_TRUNCATE_COUNT] = new StructBase.FieldInfo("successTruncateCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_SET_META_COUNT] = new StructBase.FieldInfo("totalSetMetaCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_SET_META_COUNT] = new StructBase.FieldInfo("successSetMetaCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_DELETE_COUNT] = new StructBase.FieldInfo("totalDeleteCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_DELETE_COUNT] = new StructBase.FieldInfo("successDeleteCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_DOWNLOAD_COUNT] = new StructBase.FieldInfo("totalDownloadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_DOWNLOAD_COUNT] = new StructBase.FieldInfo("successDownloadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_GET_META_COUNT] = new StructBase.FieldInfo("totalGetMetaCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_GET_META_COUNT] = new StructBase.FieldInfo("successGetMetaCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_CREATE_LINK_COUNT] = new StructBase.FieldInfo("totalCreateLinkCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_CREATE_LINK_COUNT] = new StructBase.FieldInfo("successCreateLinkCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_DELETE_LINK_COUNT] = new StructBase.FieldInfo("totalDeleteLinkCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_DELETE_LINK_COUNT] = new StructBase.FieldInfo("successDeleteLinkCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_UPLOAD_BYTES] = new StructBase.FieldInfo("totalUploadBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_UPLOAD_BYTES] = new StructBase.FieldInfo("successUploadBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_APPEND_BYTES] = new StructBase.FieldInfo("totalAppendBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_APPEND_BYTES] = new StructBase.FieldInfo("successAppendBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_MODIFY_BYTES] = new StructBase.FieldInfo("totalModifyBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_MODIFY_BYTES] = new StructBase.FieldInfo("successModifyBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_DOWNLOAD_BYTES] = new StructBase.FieldInfo("totalDownloadloadBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_DOWNLOAD_BYTES] = new StructBase.FieldInfo("successDownloadloadBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_SYNC_IN_BYTES] = new StructBase.FieldInfo("totalSyncInBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_SYNC_IN_BYTES] = new StructBase.FieldInfo("successSyncInBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_SYNC_OUT_BYTES] = new StructBase.FieldInfo("totalSyncOutBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_SYNC_OUT_BYTES] = new StructBase.FieldInfo("successSyncOutBytes", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_FILE_OPEN_COUNT] = new StructBase.FieldInfo("totalFileOpenCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_FILE_OPEN_COUNT] = new StructBase.FieldInfo("successFileOpenCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_FILE_READ_COUNT] = new StructBase.FieldInfo("totalFileReadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_FILE_READ_COUNT] = new StructBase.FieldInfo("successFileReadCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TOTAL_FILE_WRITE_COUNT] = new StructBase.FieldInfo("totalFileWriteCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUCCESS_FILE_WRITE_COUNT] = new StructBase.FieldInfo("successFileWriteCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_LAST_SOURCE_UPDATE] = new StructBase.FieldInfo("lastSourceUpdate", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_LAST_SYNC_UPDATE] = new StructBase.FieldInfo("lastSyncUpdate", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_LAST_SYNCED_TIMESTAMP] = new StructBase.FieldInfo("lastSyncedTimestamp", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_LAST_HEART_BEAT_TIME] = new StructBase.FieldInfo("lastHeartBeatTime", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_IF_TRUNK_FILE] = new StructBase.FieldInfo("ifTrunkServer", offset, 1);
            offset += 1;
            fieldsTotalSize = offset;
        }
        protected byte status;
        protected string id;
        protected string ipAddr;
        protected string srcIpAddr;
        protected string domainName; //http domain name
        protected string version;
        protected long totalMB; //total disk storage in MB
        protected long freeMB;  //free disk storage in MB
        protected int uploadPriority;  //upload priority
        protected DateTime joinTime; //storage join timestamp (create timestamp)
        protected DateTime upTime;   //storage service started timestamp
        protected int storePathCount;  //store base path count of each storage server
        protected int subdirCountPerPath;
        protected int storagePort;
        protected int storageHttpPort; //storage http server port
        protected int currentWritePath; //current write path index
        protected int connectionAllocCount;
        protected int connectionCurrentCount;
        protected int connectionMaxCount;
        protected long totalUploadCount;
        protected long successUploadCount;
        protected long totalAppendCount;
        protected long successAppendCount;
        protected long totalModifyCount;
        protected long successModifyCount;
        protected long totalTruncateCount;
        protected long successTruncateCount;
        protected long totalSetMetaCount;
        protected long successSetMetaCount;
        protected long totalDeleteCount;
        protected long successDeleteCount;
        protected long totalDownloadCount;
        protected long successDownloadCount;
        protected long totalGetMetaCount;
        protected long successGetMetaCount;
        protected long totalCreateLinkCount;
        protected long successCreateLinkCount;
        protected long totalDeleteLinkCount;
        protected long successDeleteLinkCount;
        protected long totalUploadBytes;
        protected long successUploadBytes;
        protected long totalAppendBytes;
        protected long successAppendBytes;
        protected long totalModifyBytes;
        protected long successModifyBytes;
        protected long totalDownloadloadBytes;
        protected long successDownloadloadBytes;
        protected long totalSyncInBytes;
        protected long successSyncInBytes;
        protected long totalSyncOutBytes;
        protected long successSyncOutBytes;
        protected long totalFileOpenCount;
        protected long successFileOpenCount;
        protected long totalFileReadCount;
        protected long successFileReadCount;
        protected long totalFileWriteCount;
        protected long successFileWriteCount;
        protected DateTime lastSourceUpdate;
        protected DateTime lastSyncUpdate;
        protected DateTime lastSyncedTimestamp;
        protected DateTime lastHeartBeatTime;
        protected bool ifTrunkServer;

        /// <summary>
        /// get fields total size
        /// </summary>
        /// <returns> fields total size</returns>
        public static int getFieldsTotalSize()
        {
            return fieldsTotalSize;
        }

        /// <summary>
        /// get storage status
        /// </summary>
        /// <returns> storage status</returns>
        public byte getStatus()
        {
            return this.status;
        }

        /// <summary>
        /// get storage server id
        /// </summary>
        /// <returns> storage server id</returns>
        public string getId()
        {
            return this.id;
        }

        /// <summary>
        /// get storage server ip address
        /// </summary>
        /// <returns> storage server ip address</returns>
        public string getIpAddr()
        {
            return this.ipAddr;
        }

        /// <summary>
        /// get source storage ip address
        /// </summary>
        /// <returns> source storage ip address</returns>
        public string getSrcIpAddr()
        {
            return this.srcIpAddr;
        }

        /// <summary>
        /// get the domain name of the storage server
        /// </summary>
        /// <returns> the domain name of the storage server</returns>
        public string getDomainName()
        {
            return this.domainName;
        }

        /// <summary>
        /// get storage version
        /// </summary>
        /// <returns> storage version</returns>
        public string getVersion()
        {
            return this.version;
        }

        /// <summary>
        /// get total disk space in MB
        /// </summary>
        /// <returns> total disk space in MB</returns>
        public long getTotalMB()
        {
            return this.totalMB;
        }

        /// <summary>
        /// get free disk space in MB
        /// </summary>
        /// <returns> free disk space in MB</returns>
        public long getFreeMB()
        {
            return this.freeMB;
        }

        /// <summary>
        /// get storage server upload priority
        /// </summary>
        /// <returns> storage server upload priority</returns>
        public int getUploadPriority()
        {
            return this.uploadPriority;
        }

        /// <summary>
        /// get storage server join time
        /// </summary>
        /// <returns> storage server join time</returns>
        public DateTime getJoinTime()
        {
            return this.joinTime;
        }

        /// <summary>
        /// get storage server up time
        /// </summary>
        /// <returns> storage server up time</returns>
        public DateTime getUpTime()
        {
            return this.upTime;
        }

        /// <summary>
        /// get store base path count of each storage server
        /// </summary>
        /// <returns> store base path count of each storage server</returns>
        public int getStorePathCount()
        {
            return this.storePathCount;
        }

        /// <summary>
        /// get sub dir count per store path
        /// </summary>
        /// <returns> sub dir count per store path</returns>
        public int getSubdirCountPerPath()
        {
            return this.subdirCountPerPath;
        }

        /// <summary>
        /// get storage server port
        /// </summary>
        /// <returns> storage server port</returns>
        public int getStoragePort()
        {
            return this.storagePort;
        }

        /// <summary>
        /// get storage server HTTP port
        /// </summary>
        /// <returns> storage server HTTP port</returns>
        public int getStorageHttpPort()
        {
            return this.storageHttpPort;
        }

        /// <summary>
        /// get current write path index
        /// </summary>
        /// <returns> current write path index</returns>
        public int getCurrentWritePath()
        {
            return this.currentWritePath;
        }

        /// <summary>
        /// get total upload file count
        /// </summary>
        /// <returns> total upload file count</returns>
        public long getTotalUploadCount()
        {
            return this.totalUploadCount;
        }

        /// <summary>
        /// get success upload file count
        /// </summary>
        /// <returns> success upload file count</returns>
        public long getSuccessUploadCount()
        {
            return this.successUploadCount;
        }

        /// <summary>
        /// get total append count
        /// </summary>
        /// <returns> total append count</returns>
        public long getTotalAppendCount()
        {
            return this.totalAppendCount;
        }

        /// <summary>
        /// get success append count
        /// </summary>
        /// <returns> success append count</returns>
        public long getSuccessAppendCount()
        {
            return this.successAppendCount;
        }

        /// <summary>
        /// get total modify count
        /// </summary>
        /// <returns> total modify count</returns>
        public long getTotalModifyCount()
        {
            return this.totalModifyCount;
        }

        /// <summary>
        /// get success modify count
        /// </summary>
        /// <returns> success modify count</returns>
        public long getSuccessModifyCount()
        {
            return this.successModifyCount;
        }

        /// <summary>
        /// get total truncate count
        /// </summary>
        /// <returns> total truncate count</returns>
        public long getTotalTruncateCount()
        {
            return this.totalTruncateCount;
        }

        /// <summary>
        /// get success truncate count
        /// </summary>
        /// <returns> success truncate count</returns>
        public long getSuccessTruncateCount()
        {
            return this.successTruncateCount;
        }

        /// <summary>
        /// get total set meta data count
        /// </summary>
        /// <returns> total set meta data count</returns>
        public long getTotalSetMetaCount()
        {
            return this.totalSetMetaCount;
        }

        /// <summary>
        /// get success set meta data count
        /// </summary>
        /// <returns> success set meta data count</returns>
        public long getSuccessSetMetaCount()
        {
            return this.successSetMetaCount;
        }

        /// <summary>
        /// get total delete file count
        /// </summary>
        /// <returns> total delete file count</returns>
        public long getTotalDeleteCount()
        {
            return this.totalDeleteCount;
        }

        /// <summary>
        /// get success delete file count
        /// </summary>
        /// <returns> success delete file count</returns>
        public long getSuccessDeleteCount()
        {
            return this.successDeleteCount;
        }

        /// <summary>
        /// get total download file count
        /// </summary>
        /// <returns> total download file count</returns>
        public long getTotalDownloadCount()
        {
            return this.totalDownloadCount;
        }

        /// <summary>
        /// get success download file count
        /// </summary>
        /// <returns> success download file count</returns>
        public long getSuccessDownloadCount()
        {
            return this.successDownloadCount;
        }

        /// <summary>
        /// get total get metadata count
        /// </summary>
        /// <returns> total get metadata count</returns>
        public long getTotalGetMetaCount()
        {
            return this.totalGetMetaCount;
        }

        /// <summary>
        /// get success get metadata count
        /// </summary>
        /// <returns> success get metadata count</returns>
        public long getSuccessGetMetaCount()
        {
            return this.successGetMetaCount;
        }

        /// <summary>
        /// get total create linke count
        /// </summary>
        /// <returns> total create linke count</returns>
        public long getTotalCreateLinkCount()
        {
            return this.totalCreateLinkCount;
        }

        /// <summary>
        /// get success create linke count
        /// </summary>
        /// <returns> success create linke count</returns>
        public long getSuccessCreateLinkCount()
        {
            return this.successCreateLinkCount;
        }

        /// <summary>
        /// get total delete link count
        /// </summary>
        /// <returns> total delete link count</returns>
        public long getTotalDeleteLinkCount()
        {
            return this.totalDeleteLinkCount;
        }

        /// <summary>
        /// get success delete link count
        /// </summary>
        /// <returns> success delete link count</returns>
        public long getSuccessDeleteLinkCount()
        {
            return this.successDeleteLinkCount;
        }

        /// <summary>
        /// get total upload file bytes
        /// </summary>
        /// <returns> total upload file bytes</returns>
        public long getTotalUploadBytes()
        {
            return this.totalUploadBytes;
        }

        /// <summary>
        /// get success upload file bytes
        /// </summary>
        /// <returns> success upload file bytes</returns>
        public long getSuccessUploadBytes()
        {
            return this.successUploadBytes;
        }

        /// <summary>
        /// get total append bytes
        /// </summary>
        /// <returns> total append bytes</returns>
        public long getTotalAppendBytes()
        {
            return this.totalAppendBytes;
        }

        /// <summary>
        /// get success append bytes
        /// </summary>
        /// <returns> success append bytes</returns>
        public long getSuccessAppendBytes()
        {
            return this.successAppendBytes;
        }

        /// <summary>
        /// get total modify bytes
        /// </summary>
        /// <returns> total modify bytes</returns>
        public long getTotalModifyBytes()
        {
            return this.totalModifyBytes;
        }

        /// <summary>
        /// get success modify bytes
        /// </summary>
        /// <returns> success modify bytes</returns>
        public long getSuccessModifyBytes()
        {
            return this.successModifyBytes;
        }

        /// <summary>
        /// get total download file bytes
        /// </summary>
        /// <returns> total download file bytes</returns>
        public long getTotalDownloadloadBytes()
        {
            return this.totalDownloadloadBytes;
        }

        /// <summary>
        /// get success download file bytes
        /// </summary>
        /// <returns> success download file bytes</returns>
        public long getSuccessDownloadloadBytes()
        {
            return this.successDownloadloadBytes;
        }

        /// <summary>
        /// get total sync in bytes
        /// </summary>
        /// <returns> total sync in bytes</returns>
        public long getTotalSyncInBytes()
        {
            return this.totalSyncInBytes;
        }

        /// <summary>
        /// get success sync in bytes
        /// </summary>
        /// <returns> success sync in bytes</returns>
        public long getSuccessSyncInBytes()
        {
            return this.successSyncInBytes;
        }

        /// <summary>
        /// get total sync out bytes
        /// </summary>
        /// <returns> total sync out bytes</returns>
        public long getTotalSyncOutBytes()
        {
            return this.totalSyncOutBytes;
        }

        /// <summary>
        /// get success sync out bytes
        /// </summary>
        /// <returns> success sync out bytes</returns>
        public long getSuccessSyncOutBytes()
        {
            return this.successSyncOutBytes;
        }

        /// <summary>
        /// get total file opened count
        /// </summary>
        /// <returns> total file opened bytes</returns>
        public long getTotalFileOpenCount()
        {
            return this.totalFileOpenCount;
        }

        /// <summary>
        /// get success file opened count
        /// </summary>
        /// <returns> success file opened count</returns>
        public long getSuccessFileOpenCount()
        {
            return this.successFileOpenCount;
        }

        /// <summary>
        /// get total file read count
        /// </summary>
        /// <returns> total file read bytes</returns>
        public long getTotalFileReadCount()
        {
            return this.totalFileReadCount;
        }

        /// <summary>
        /// get success file read count
        /// </summary>
        /// <returns> success file read count</returns>
        public long getSuccessFileReadCount()
        {
            return this.successFileReadCount;
        }

        /// <summary>
        /// get total file write count
        /// </summary>
        /// <returns> total file write bytes</returns>
        public long getTotalFileWriteCount()
        {
            return this.totalFileWriteCount;
        }

        /// <summary>
        /// get success file write count
        /// </summary>
        /// <returns> success file write count</returns>
        public long getSuccessFileWriteCount()
        {
            return this.successFileWriteCount;
        }

        /// <summary>
        /// get last source update timestamp
        /// </summary>
        /// <returns> last source update timestamp</returns>
        public DateTime getLastSourceUpdate()
        {
            return this.lastSourceUpdate;
        }

        /// <summary>
        /// get last synced update timestamp
        /// </summary>
        /// <returns> last synced update timestamp</returns>
        public DateTime getLastSyncUpdate()
        {
            return this.lastSyncUpdate;
        }

        /// <summary>
        /// get last synced timestamp
        /// </summary>
        /// <returns> last synced timestamp</returns>
        public DateTime getLastSyncedTimestamp()
        {
            return this.lastSyncedTimestamp;
        }

        /// <summary>
        /// get last heart beat timestamp
        /// </summary>
        /// <returns> last heart beat timestamp</returns>
        public DateTime getLastHeartBeatTime()
        {
            return this.lastHeartBeatTime;
        }

        /// <summary>
        /// if the trunk server
        /// </summary>
        /// <returns> true for the trunk server, otherwise false</returns>
        public bool isTrunkServer()
        {
            return this.ifTrunkServer;
        }

        /// <summary>
        /// get connection alloc count
        /// </summary>
        /// <returns> connection alloc count</returns>
        public int getConnectionAllocCount()
        {
            return this.connectionAllocCount;
        }

        /// <summary>
        /// get connection current count
        /// </summary>
        /// <returns> connection current count</returns>
        public int getConnectionCurrentCount()
        {
            return this.connectionCurrentCount;
        }

        /// <summary>
        /// get connection max count
        /// </summary>
        /// <returns> connection max count</returns>
        public int getConnectionMaxCount()
        {
            return this.connectionMaxCount;
        }

        /// <summary>
        /// set fields
        /// </summary>
        /// <param name="bs">byte array</param>
        /// <param name="offset">start offset</param>
        public override void setFields(byte[] bs, int offset)
        {
            this.status = byteValue(bs, offset, fieldsArray[FIELD_INDEX_STATUS]);
            this.id = stringValue(bs, offset, fieldsArray[FIELD_INDEX_ID]);
            this.ipAddr = stringValue(bs, offset, fieldsArray[FIELD_INDEX_IP_ADDR]);
            this.srcIpAddr = stringValue(bs, offset, fieldsArray[FIELD_INDEX_SRC_IP_ADDR]);
            this.domainName = stringValue(bs, offset, fieldsArray[FIELD_INDEX_DOMAIN_NAME]);
            this.version = stringValue(bs, offset, fieldsArray[FIELD_INDEX_VERSION]);
            this.totalMB = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_MB]);
            this.freeMB = longValue(bs, offset, fieldsArray[FIELD_INDEX_FREE_MB]);
            this.uploadPriority = intValue(bs, offset, fieldsArray[FIELD_INDEX_UPLOAD_PRIORITY]);
            this.joinTime = dateValue(bs, offset, fieldsArray[FIELD_INDEX_JOIN_TIME]);
            this.upTime = dateValue(bs, offset, fieldsArray[FIELD_INDEX_UP_TIME]);
            this.storePathCount = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORE_PATH_COUNT]);
            this.subdirCountPerPath = intValue(bs, offset, fieldsArray[FIELD_INDEX_SUBDIR_COUNT_PER_PATH]);
            this.storagePort = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORAGE_PORT]);
            this.storageHttpPort = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORAGE_HTTP_PORT]);
            this.currentWritePath = intValue(bs, offset, fieldsArray[FIELD_INDEX_CURRENT_WRITE_PATH]);
            this.connectionAllocCount = int32Value(bs, offset, fieldsArray[FIELD_INDEX_CONNECTION_ALLOC_COUNT]);
            this.connectionCurrentCount = int32Value(bs, offset, fieldsArray[FIELD_INDEX_CONNECTION_CURRENT_COUNT]);
            this.connectionMaxCount = int32Value(bs, offset, fieldsArray[FIELD_INDEX_CONNECTION_MAX_COUNT]);
            this.totalUploadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_UPLOAD_COUNT]);
            this.successUploadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_UPLOAD_COUNT]);
            this.totalAppendCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_APPEND_COUNT]);
            this.successAppendCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_APPEND_COUNT]);
            this.totalModifyCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_MODIFY_COUNT]);
            this.successModifyCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_MODIFY_COUNT]);
            this.totalTruncateCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_TRUNCATE_COUNT]);
            this.successTruncateCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_TRUNCATE_COUNT]);
            this.totalSetMetaCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_SET_META_COUNT]);
            this.successSetMetaCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_SET_META_COUNT]);
            this.totalDeleteCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_DELETE_COUNT]);
            this.successDeleteCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_DELETE_COUNT]);
            this.totalDownloadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_DOWNLOAD_COUNT]);
            this.successDownloadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_DOWNLOAD_COUNT]);
            this.totalGetMetaCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_GET_META_COUNT]);
            this.successGetMetaCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_GET_META_COUNT]);
            this.totalCreateLinkCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_CREATE_LINK_COUNT]);
            this.successCreateLinkCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_CREATE_LINK_COUNT]);
            this.totalDeleteLinkCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_DELETE_LINK_COUNT]);
            this.successDeleteLinkCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_DELETE_LINK_COUNT]);
            this.totalUploadBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_UPLOAD_BYTES]);
            this.successUploadBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_UPLOAD_BYTES]);
            this.totalAppendBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_APPEND_BYTES]);
            this.successAppendBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_APPEND_BYTES]);
            this.totalModifyBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_MODIFY_BYTES]);
            this.successModifyBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_MODIFY_BYTES]);
            this.totalDownloadloadBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_DOWNLOAD_BYTES]);
            this.successDownloadloadBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_DOWNLOAD_BYTES]);
            this.totalSyncInBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_SYNC_IN_BYTES]);
            this.successSyncInBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_SYNC_IN_BYTES]);
            this.totalSyncOutBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_SYNC_OUT_BYTES]);
            this.successSyncOutBytes = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_SYNC_OUT_BYTES]);
            this.totalFileOpenCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_FILE_OPEN_COUNT]);
            this.successFileOpenCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_FILE_OPEN_COUNT]);
            this.totalFileReadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_FILE_READ_COUNT]);
            this.successFileReadCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_FILE_READ_COUNT]);
            this.totalFileWriteCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_FILE_WRITE_COUNT]);
            this.successFileWriteCount = longValue(bs, offset, fieldsArray[FIELD_INDEX_SUCCESS_FILE_WRITE_COUNT]);
            this.lastSourceUpdate = dateValue(bs, offset, fieldsArray[FIELD_INDEX_LAST_SOURCE_UPDATE]);
            this.lastSyncUpdate = dateValue(bs, offset, fieldsArray[FIELD_INDEX_LAST_SYNC_UPDATE]);
            this.lastSyncedTimestamp = dateValue(bs, offset, fieldsArray[FIELD_INDEX_LAST_SYNCED_TIMESTAMP]);
            this.lastHeartBeatTime = dateValue(bs, offset, fieldsArray[FIELD_INDEX_LAST_HEART_BEAT_TIME]);
            this.ifTrunkServer = booleanValue(bs, offset, fieldsArray[FIELD_INDEX_IF_TRUNK_FILE]);
        }
    }
}
