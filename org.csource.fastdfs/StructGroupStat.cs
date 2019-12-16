
/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// C struct body decoder
    /// </summary>
    public class StructGroupStat : StructBase
    {
        protected const int FIELD_INDEX_GROUP_NAME = 0;
        protected const int FIELD_INDEX_TOTAL_MB = 1;
        protected const int FIELD_INDEX_FREE_MB = 2;
        protected const int FIELD_INDEX_TRUNK_FREE_MB = 3;
        protected const int FIELD_INDEX_STORAGE_COUNT = 4;
        protected const int FIELD_INDEX_STORAGE_PORT = 5;
        protected const int FIELD_INDEX_STORAGE_HTTP_PORT = 6;
        protected const int FIELD_INDEX_ACTIVE_COUNT = 7;
        protected const int FIELD_INDEX_CURRENT_WRITE_SERVER = 8;
        protected const int FIELD_INDEX_STORE_PATH_COUNT = 9;
        protected const int FIELD_INDEX_SUBDIR_COUNT_PER_PATH = 10;
        protected const int FIELD_INDEX_CURRENT_TRUNK_FILE_ID = 11;
        protected static int fieldsTotalSize;
        protected static StructBase.FieldInfo[] fieldsArray = new StructBase.FieldInfo[12];
        static StructGroupStat()
        {
            int offset = 0;
            fieldsArray[FIELD_INDEX_GROUP_NAME] = new StructBase.FieldInfo("groupName", offset, ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + 1);
            offset += ProtoCommon.FDFS_GROUP_NAME_MAX_LEN + 1;
            fieldsArray[FIELD_INDEX_TOTAL_MB] = new StructBase.FieldInfo("totalMB", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_FREE_MB] = new StructBase.FieldInfo("freeMB", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_TRUNK_FREE_MB] = new StructBase.FieldInfo("trunkFreeMB", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORAGE_COUNT] = new StructBase.FieldInfo("storageCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORAGE_PORT] = new StructBase.FieldInfo("storagePort", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORAGE_HTTP_PORT] = new StructBase.FieldInfo("storageHttpPort", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_ACTIVE_COUNT] = new StructBase.FieldInfo("activeCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_CURRENT_WRITE_SERVER] = new StructBase.FieldInfo("currentWriteServer", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_STORE_PATH_COUNT] = new StructBase.FieldInfo("storePathCount", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_SUBDIR_COUNT_PER_PATH] = new StructBase.FieldInfo("subdirCountPerPath", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsArray[FIELD_INDEX_CURRENT_TRUNK_FILE_ID] = new StructBase.FieldInfo("currentTrunkFileId", offset, ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE);
            offset += ProtoCommon.FDFS_PROTO_PKG_LEN_SIZE;
            fieldsTotalSize = offset;
        }
        protected string groupName;  //name of this group
        protected long totalMB;      //total disk storage in MB
        protected long freeMB;       //free disk space in MB
        protected long trunkFreeMB;  //trunk free space in MB
        protected int storageCount;  //storage server count
        protected int storagePort;   //storage server port
        protected int storageHttpPort; //storage server HTTP port
        protected int activeCount;     //active storage server count
        protected int currentWriteServer; //current storage server index to upload file
        protected int storePathCount;     //store base path count of each storage server
        protected int subdirCountPerPath; //sub dir count per store path
        protected int currentTrunkFileId; //current trunk file id

        /// <summary>
        /// get fields total size
        /// </summary>
        /// <returns> fields total size</returns>
        public static int getFieldsTotalSize()
        {
            return fieldsTotalSize;
        }

        /// <summary>
        /// get group name
        /// </summary>
        /// <returns> group name</returns>
        public string getGroupName()
        {
            return this.groupName;
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
        /// get trunk free space in MB
        /// </summary>
        /// <returns> trunk free space in MB</returns>
        public long getTrunkFreeMB()
        {
            return this.trunkFreeMB;
        }

        /// <summary>
        /// get storage server count in this group
        /// </summary>
        /// <returns> storage server count in this group</returns>
        public int getStorageCount()
        {
            return this.storageCount;
        }

        /// <summary>
        /// get active storage server count in this group
        /// </summary>
        /// <returns> active storage server count in this group</returns>
        public int getActiveCount()
        {
            return this.activeCount;
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
        /// get current storage server index to upload file
        /// </summary>
        /// <returns> current storage server index to upload file</returns>
        public int getCurrentWriteServer()
        {
            return this.currentWriteServer;
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
        /// get current trunk file id
        /// </summary>
        /// <returns> current trunk file id</returns>
        public int getCurrentTrunkFileId()
        {
            return this.currentTrunkFileId;
        }

        /// <summary>
        /// set fields
        /// </summary>
        /// <param name="bs">byte array</param>
        /// <param name="offset">start offset</param>
        public override void setFields(byte[] bs, int offset)
        {
            this.groupName = stringValue(bs, offset, fieldsArray[FIELD_INDEX_GROUP_NAME]);
            this.totalMB = longValue(bs, offset, fieldsArray[FIELD_INDEX_TOTAL_MB]);
            this.freeMB = longValue(bs, offset, fieldsArray[FIELD_INDEX_FREE_MB]);
            this.trunkFreeMB = longValue(bs, offset, fieldsArray[FIELD_INDEX_TRUNK_FREE_MB]);
            this.storageCount = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORAGE_COUNT]);
            this.storagePort = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORAGE_PORT]);
            this.storageHttpPort = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORAGE_HTTP_PORT]);
            this.activeCount = intValue(bs, offset, fieldsArray[FIELD_INDEX_ACTIVE_COUNT]);
            this.currentWriteServer = intValue(bs, offset, fieldsArray[FIELD_INDEX_CURRENT_WRITE_SERVER]);
            this.storePathCount = intValue(bs, offset, fieldsArray[FIELD_INDEX_STORE_PATH_COUNT]);
            this.subdirCountPerPath = intValue(bs, offset, fieldsArray[FIELD_INDEX_SUBDIR_COUNT_PER_PATH]);
            this.currentTrunkFileId = intValue(bs, offset, fieldsArray[FIELD_INDEX_CURRENT_TRUNK_FILE_ID]);
        }
    }
}
