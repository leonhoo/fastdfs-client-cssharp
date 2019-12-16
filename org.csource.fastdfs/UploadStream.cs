using System.IO;
namespace org.csource.fastdfs
{

    /// <summary>
    /// Upload file by stream
    /// </summary>
    public class UploadStream : UploadCallback
    {
        private Stream inputStream; //input stream for reading
        private long fileSize = 0;  //size of the uploaded file

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="inputStream">input stream for uploading</param>
        /// <param name="fileSize">size of uploaded file</param>
        public UploadStream(Stream inputStream, long fileSize) : base()
        {
            this.inputStream = inputStream;
            this.fileSize = fileSize;
        }

        /// <summary>
        /// send file content callback function, be called only once when the file uploaded
        /// </summary>
        /// <param name="out">output stream for writing file content</param>
        /// <returns> 0 success, return none zero(errno) if fail</returns>
        public int send(Stream outStream)
        {
            long remainBytes = fileSize;
            byte[] buff = new byte[256 * 1024];
            int bytes;
            while (remainBytes > 0)
            {
                try
                {
                    if ((bytes = inputStream.Read(buff, 0, remainBytes > buff.Length ? buff.Length : (int)remainBytes)) < 0)
                    {
                        return -1;
                    }
                }
                catch (IOException ex)
                {
                    throw ex;
                }
                outStream.Write(buff, 0, bytes);
                remainBytes -= bytes;
            }
            return 0;
        }
    }
}
