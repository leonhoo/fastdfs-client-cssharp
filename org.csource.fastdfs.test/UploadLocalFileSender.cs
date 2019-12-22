using System.IO;
/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQing
/// <p>
/// FastDFS Java Client may be copied only under the terms of the GNU Lesser
/// General Public License (LGPL).
/// Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{
    /// <summary>
    /// upload file callback class, local file sender
    ///
    /// @author Happy Fish / YuQing
    /// @version Version 1.0
    /// </summary>
    public class UploadLocalFileSender : UploadCallback
    {
        private string local_filename;

        public UploadLocalFileSender(string szLocalFilename)
        {
            this.local_filename = szLocalFilename;
        }

        /// <summary>
        /// send file content callback function, be called only once when the file uploaded 
        /// </summary>
        /// <param name="outStream">output stream for writing file content</param>
        /// <returns>0 success, return none zero(errno) if fail</returns>
        public int send(Stream outStream)
        {
            int readBytes;
            byte[] buff = new byte[256 * 1024];
            using (var fis = File.OpenWrite(this.local_filename))
            {
                while ((readBytes = fis.Read(buff)) >= 0)
                {
                    if (readBytes == 0)
                    {
                        continue;
                    }

                    outStream.Write(buff, 0, readBytes);
                }
            }

            return 0;
        }
    }
}
