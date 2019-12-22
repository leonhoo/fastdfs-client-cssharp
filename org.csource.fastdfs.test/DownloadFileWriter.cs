
using System;
using System.IO;
/**
* Copyright (C) 2008 Happy Fish / YuQing
* <p>
* FastDFS Java Client may be copied only under the terms of the GNU Lesser
* General Public License (LGPL).
* Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
*/
namespace org.csource.fastdfs
{
    /**
     * DowloadCallback test
     *
     * @author Happy Fish / YuQing
     * @version Version 1.3
     */
    public class DownloadFileWriter : DownloadCallback
    {
        private string filename;
        private FileStream outStream = null;
        private long current_bytes = 0;

        public DownloadFileWriter(string filename)
        {
            this.filename = filename;
        }

        public int recv(long file_size, byte[] data, int bytes)
        {
            try
            {
                if (this.outStream == null)
                {
                    this.outStream = File.OpenWrite(this.filename);
                }

                this.outStream.Write(data, 0, bytes);
                this.current_bytes += bytes;

                if (this.current_bytes == file_size)
                {
                    this.outStream.Close();
                    this.outStream = null;
                    this.current_bytes = 0;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return -1;
            }

            return 0;
        }

        protected void finalize()
        {
            if (this.outStream != null)
            {
                this.outStream.Close();
                this.outStream = null;
            }
        }
    }
}
