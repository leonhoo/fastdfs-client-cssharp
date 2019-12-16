using org.csource.fastdfs.common;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Storage client for 1 field file id: combined group name and filename
    /// </summary>
    public class StorageClient1 : StorageClient
    {
        public const char SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR = '/';

        /// <summary>
        /// constructor
        /// </summary>
        public StorageClient1() : base()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="trackerServer">the tracker server, can be null</param>
        /// <param name="storageServer">the storage server, can be null</param>
        public StorageClient1(TrackerServer trackerServer, StorageServer storageServer) : base(trackerServer, storageServer)
        {
        }
        public static byte split_file_id(string file_id, string[] results)
        {
            int pos = file_id.IndexOf(SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR);
            if ((pos <= 0) || (pos == file_id.Length - 1))
            {
                return ProtoCommon.ERR_NO_EINVAL;
            }
            results[0] = file_id.Substring(0, pos); //group name
            results[1] = file_id.Substring(pos + 1); //file name
            return 0;
        }

        /// <summary>
        /// upload file to storage server (by file name)
        /// </summary>
        /// <param name="local_filename">local filename to upload</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.), null to extract ext name from the local filename</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string local_filename, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_file(local_filename, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file name)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="local_filename">local filename to upload</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.), null to extract ext name from the local filename</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string group_name, string local_filename, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_file(group_name, local_filename, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file buff)
        /// </summary>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(byte[] file_buff, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_file(file_buff, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file buff)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string group_name, byte[] file_buff, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_file(group_name, file_buff, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by callback)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="file_size">the file size</param>
        /// <param name="callback">the write data callback object</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string group_name, long file_size,
        UploadCallback callback, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_file(group_name, file_size, callback, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload appender file to storage server (by file name)
        /// </summary>
        /// <param name="local_filename">local filename to upload</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.), null to extract ext name from the local filename</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_appender_file1(string local_filename, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_appender_file(local_filename, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload appender file to storage server (by file name)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="local_filename">local filename to upload</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.), null to extract ext name from the local filename</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_appender_file1(string group_name, string local_filename, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_appender_file(group_name, local_filename, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload appender file to storage server (by file buff)
        /// </summary>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_appender_file1(byte[] file_buff, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_appender_file(file_buff, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload appender file to storage server (by file buff)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_appender_file1(string group_name, byte[] file_buff, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_appender_file(group_name, file_buff, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload appender file to storage server (by callback)
        /// </summary>
        /// <param name="group_name">the group name to upload file to, can be empty</param>
        /// <param name="file_size">the file size</param>
        /// <param name="callback">the write data callback object</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_appender_file1(string group_name, long file_size,
        UploadCallback callback, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = this.upload_appender_file(group_name, file_size, callback, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file name, slave file mode)
        /// </summary>
        /// <param name="master_file_id">the master file id to generate the slave file</param>
        /// <param name="prefix_name">the prefix name to generate the slave file</param>
        /// <param name="local_filename">local filename to upload</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.), null to extract ext name from the local filename</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string master_file_id, string prefix_name,
        string local_filename, string file_ext_name, NameValuePair[] meta_list)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(master_file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            parts = this.upload_file(parts[0], parts[1], prefix_name,
            local_filename, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file buff, slave file mode)
        /// </summary>
        /// <param name="master_file_id">the master file id to generate the slave file</param>
        /// <param name="prefix_name">the prefix name to generate the slave file</param>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string master_file_id, string prefix_name,
        byte[] file_buff, string file_ext_name, NameValuePair[] meta_list)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(master_file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            parts = this.upload_file(parts[0], parts[1], prefix_name, file_buff, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by file buff, slave file mode)
        /// </summary>
        /// <param name="master_file_id">the master file id to generate the slave file</param>
        /// <param name="prefix_name">the prefix name to generate the slave file</param>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string master_file_id, string prefix_name,
        byte[] file_buff, int offset, int length, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(master_file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            parts = this.upload_file(parts[0], parts[1], prefix_name, file_buff,
            offset, length, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file to storage server (by callback)
        /// </summary>
        /// <param name="master_file_id">the master file id to generate the slave file</param>
        /// <param name="prefix_name">the prefix name to generate the slave file</param>
        /// <param name="file_size">the file size</param>
        /// <param name="callback">the write data callback object</param>
        /// <param name="file_ext_name">file ext name, do not include dot(.)</param>
        /// <param name="meta_list">meta info array</param>
        /// <returns> file id(including group name and filename) if success, <br>return null if fail</returns>
        public string upload_file1(string master_file_id, string prefix_name, long file_size,
        UploadCallback callback, string file_ext_name,
        NameValuePair[] meta_list)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(master_file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            parts = this.upload_file(parts[0], parts[1], prefix_name, file_size, callback, file_ext_name, meta_list);
            if (parts != null)
            {
                return parts[0] + SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + parts[1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// append file to storage server (by file name)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="local_filename">local filename to append</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int append_file1(string appender_file_id, string local_filename)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.append_file(parts[0], parts[1], local_filename);
        }

        /// <summary>
        /// append file to storage server (by file buff)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_buff">file content/buff</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int append_file1(string appender_file_id, byte[] file_buff)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.append_file(parts[0], parts[1], file_buff);
        }

        /// <summary>
        /// append file to storage server (by file buff)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_buff">file content/buffer</param>
        /// <param name="offset">start offset of the buffer</param>
        /// <param name="length">the length of the buffer to append</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int append_file1(string appender_file_id, byte[] file_buff, int offset, int length)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.append_file(parts[0], parts[1], file_buff, offset, length);
        }

        /// <summary>
        /// append file to storage server (by callback)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_size">the file size</param>
        /// <param name="callback">the write data callback object</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int append_file1(string appender_file_id, long file_size, UploadCallback callback)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.append_file(parts[0], parts[1], file_size, callback);
        }

        /// <summary>
        /// modify appender file to storage server (by file name)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_offset">the offset of appender file</param>
        /// <param name="local_filename">local filename to append</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int modify_file1(string appender_file_id,
        long file_offset, string local_filename)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.modify_file(parts[0], parts[1], file_offset, local_filename);
        }

        /// <summary>
        /// modify appender file to storage server (by file buff)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_offset">the offset of appender file</param>
        /// <param name="file_buff">file content/buff</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int modify_file1(string appender_file_id,
        long file_offset, byte[] file_buff)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.modify_file(parts[0], parts[1], file_offset, file_buff);
        }

        /// <summary>
        /// modify appender file to storage server (by file buff)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_offset">the offset of appender file</param>
        /// <param name="file_buff">file content/buff</param>
        /// <param name="buffer_offset">start offset of the buff</param>
        /// <param name="buffer_length">the length of buff to modify</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int modify_file1(string appender_file_id,
        long file_offset, byte[] file_buff, int buffer_offset, int buffer_length)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.modify_file(parts[0], parts[1], file_offset,
            file_buff, buffer_offset, buffer_length);
        }

        /// <summary>
        /// modify appender file to storage server (by callback)
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="file_offset">the offset of appender file</param>
        /// <param name="modify_size">the modify size</param>
        /// <param name="callback">the write data callback object</param>
        /// <returns> 0 for success, != 0 for error (error no)</returns>
        public int modify_file1(string appender_file_id,
        long file_offset, long modify_size, UploadCallback callback)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.modify_file(parts[0], parts[1], file_offset, modify_size, callback);
        }

        /// <summary>
        /// delete file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> 0 for success, none zero for fail (error code)</returns>
        public int delete_file1(string file_id)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.delete_file(parts[0], parts[1]);
        }

        /// <summary>
        /// truncate appender file to size 0 from storage server
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <returns> 0 for success, none zero for fail (error code)</returns>
        public int truncate_file1(string appender_file_id)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.truncate_file(parts[0], parts[1]);
        }

        /// <summary>
        /// truncate appender file from storage server
        /// </summary>
        /// <param name="appender_file_id">the appender file id</param>
        /// <param name="truncated_file_size">truncated file size</param>
        /// <returns> 0 for success, none zero for fail (error code)</returns>
        public int truncate_file1(string appender_file_id, long truncated_file_size)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(appender_file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.truncate_file(parts[0], parts[1], truncated_file_size);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> file content/buffer, return null if fail</returns>
        public byte[] download_file1(string file_id)
        {
            const long file_offset = 0;
            const long download_bytes = 0;
            return this.download_file1(file_id, file_offset, download_bytes);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="file_offset">the start offset of the file</param>
        /// <param name="download_bytes">download bytes, 0 for remain bytes from offset</param>
        /// <returns> file content/buff, return null if fail</returns>
        public byte[] download_file1(string file_id, long file_offset, long download_bytes)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.download_file(parts[0], parts[1], file_offset, download_bytes);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="local_filename">the filename on local</param>
        /// <returns> 0 success, return none zero errno if fail</returns>
        public int download_file1(string file_id, string local_filename)
        {
            const long file_offset = 0;
            const long download_bytes = 0;
            return this.download_file1(file_id, file_offset, download_bytes, local_filename);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="file_offset">the start offset of the file</param>
        /// <param name="download_bytes">download bytes, 0 for remain bytes from offset</param>
        /// <param name="local_filename">the filename on local</param>
        /// <returns> 0 success, return none zero errno if fail</returns>
        public int download_file1(string file_id, long file_offset, long download_bytes, string local_filename)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.download_file(parts[0], parts[1], file_offset, download_bytes, local_filename);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="callback">the callback object, will call callback.recv() when data arrive</param>
        /// <returns> 0 success, return none zero errno if fail</returns>
        public int download_file1(string file_id, DownloadCallback callback)
        {
            const long file_offset = 0;
            const long download_bytes = 0;
            return this.download_file1(file_id, file_offset, download_bytes, callback);
        }

        /// <summary>
        /// download file from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="file_offset">the start offset of the file</param>
        /// <param name="download_bytes">download bytes, 0 for remain bytes from offset</param>
        /// <param name="callback">the callback object, will call callback.recv() when data arrive</param>
        /// <returns> 0 success, return none zero errno if fail</returns>
        public int download_file1(string file_id, long file_offset, long download_bytes, DownloadCallback callback)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.download_file(parts[0], parts[1], file_offset, download_bytes, callback);
        }

        /// <summary>
        /// get all metadata items from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> meta info array, return null if fail</returns>
        public NameValuePair[] get_metadata1(string file_id)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.get_metadata(parts[0], parts[1]);
        }

        /// <summary>
        /// set metadata items to storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <param name="meta_list">meta item array</param>
        /// <param name="op_flag">flag, can be one of following values: <br><ul><li> ProtoCommon.STORAGE_SET_METADATA_FLAG_OVERWRITE: overwrite all oldmetadata items</li></ul><ul><li> ProtoCommon.STORAGE_SET_METADATA_FLAG_MERGE: merge, insert whenthe metadata item not exist, otherwise update it</li></ul></param>
        /// <returns> 0 for success, !=0 fail (error code)</returns>
        public int set_metadata1(string file_id, NameValuePair[] meta_list, byte op_flag)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return this.errno;
            }
            return this.set_metadata(parts[0], parts[1], meta_list, op_flag);
        }

        /// <summary>
        /// get file info from storage server
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> FileInfo object for success, return null for fail</returns>
        public FileInfo query_file_info1(string file_id)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.query_file_info(parts[0], parts[1]);
        }

        /// <summary>
        /// get file info decoded from filename
        /// </summary>
        /// <param name="file_id">the file id(including group name and filename)</param>
        /// <returns> FileInfo object for success, return null for fail</returns>
        public FileInfo get_file_info1(string file_id)
        {
            string[] parts = new string[2];
            this.errno = split_file_id(file_id, parts);
            if (this.errno != 0)
            {
                return null;
            }
            return this.get_file_info(parts[0], parts[1]);
        }
    }
}
