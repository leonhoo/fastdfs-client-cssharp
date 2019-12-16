using org.csource.fastdfs.encapsulation;
using System;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Server Info
    /// </summary>
    public class FileInfo
    {
        protected string source_ip_addr;
        protected long file_size;
        protected DateTime create_timestamp;
        protected int crc32;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file_size">the file size</param>
        /// <param name="create_timestamp">create timestamp in seconds</param>
        /// <param name="crc32">the crc32 signature</param>
        /// <param name="source_ip_addr">the source storage ip address</param>
        public FileInfo(long file_size, int create_timestamp, int crc32, string source_ip_addr)
        {
            this.file_size = file_size;
            //this.create_timestamp = new Date(create_timestamp * 1000L);
            this.create_timestamp = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(create_timestamp);
            this.crc32 = crc32;
            this.source_ip_addr = source_ip_addr;
        }

        /// <summary>
        /// get the source ip address of the file uploaded to
        /// </summary>
        /// <returns> the source ip address of the file uploaded to</returns>
        public string getSourceIpAddr()
        {
            return this.source_ip_addr;
        }

        /// <summary>
        /// set the source ip address of the file uploaded to
        /// </summary>
        /// <param name="source_ip_addr">the source ip address</param>
        public void setSourceIpAddr(string source_ip_addr)
        {
            this.source_ip_addr = source_ip_addr;
        }

        /// <summary>
        /// get the file size
        /// </summary>
        /// <returns> the file size</returns>
        public long getFileSize()
        {
            return this.file_size;
        }

        /// <summary>
        /// set the file size
        /// </summary>
        /// <param name="file_size">the file size</param>
        public void setFileSize(long file_size)
        {
            this.file_size = file_size;
        }

        /// <summary>
        /// get the create timestamp of the file
        /// </summary>
        /// <returns> the create timestamp of the file</returns>
        public DateTime getCreateTimestamp()
        {
            return this.create_timestamp;
        }

        /// <summary>
        /// set the create timestamp of the file
        /// </summary>
        /// <param name="create_timestamp">create timestamp in seconds</param>
        public void setCreateTimestamp(int create_timestamp)
        {
            //this.create_timestamp = new Date(create_timestamp * 1000L);
            this.create_timestamp = Dates.Get(create_timestamp * 1000L);
        }

        /// <summary>
        /// get the file CRC32 signature
        /// </summary>
        /// <returns> the file CRC32 signature</returns>
        public long getCrc32()
        {
            return this.crc32;
        }

        /// <summary>
        /// set the create timestamp of the file
        /// </summary>
        /// <param name="crc32">the crc32 signature</param>
        public void setCrc32(int crc32)
        {
            this.crc32 = crc32;
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns> string</returns>
        public string toString()
        {
            var format = ("yyyy-MM-dd HH:mm:ss");
            return "source_ip_addr = " + this.source_ip_addr + ", " +
            "file_size = " + this.file_size + ", " +
            "create_timestamp = " + this.create_timestamp.ToString(format) + ", " +
            "crc32 = " + this.crc32;
        }
    }
}
