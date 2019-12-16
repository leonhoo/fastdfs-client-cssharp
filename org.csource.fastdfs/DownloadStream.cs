using System.IO;
namespace org.csource.fastdfs
{

    /// <summary>
    /// Download file by stream (download callback class)
    /// </summary>
    public class DownloadStream : DownloadCallback
    {
        private Stream stream;
        private long currentBytes = 0;
        public DownloadStream(Stream os) : base()
        {
            this.stream = os;
        }

        /// <summary>
        /// recv file content callback function, may be called more than once when the file downloaded
        /// </summary>
        /// <param name="fileSize">file size</param>
        /// <param name="data">data buff</param>
        /// <param name="bytes">data bytes</param>
        /// <returns> 0 success, return none zero(errno) if fail</returns>
        public int recv(long fileSize, byte[] data, int bytes)
        {
            try
            {
                stream.Write(data, 0, bytes);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            currentBytes += bytes;
            if (this.currentBytes == fileSize)
            {
                this.currentBytes = 0;
            }
            return 0;
        }
    }
}
