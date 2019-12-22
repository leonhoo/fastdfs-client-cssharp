
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.csource.fastdfs.common;
using Serilog;
using System;
using Xunit.Abstractions;
/**
* Copyright (C) 2008 Happy Fish / YuQing
* <p>
* FastDFS Java Client may be copied only under the terms of the GNU Lesser
* General Public License (LGPL).
* Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
**/
namespace org.csource.fastdfs
{
    /// <summary>
    /// client test
    ///
    /// @author Happy Fish / YuQing
    /// @version Version 1.18
    /// </summary>
    [TestClass]
    public class Test
    {
        private Test(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Xunit(output)
                .CreateLogger();
        }

        /// <summary>
        /// entry point 
        /// </summary>
        /// <param name="args">comand arguments; args[0]: config filename, args[1]: local filename to upload</param>
        [TestMethod]
        public void main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Must have 2 parameters, one is config filename, "
                  + "the other is the local filename to upload");
                return;
            }

            string conf_filename = args[0];
            string local_filename = args[1];

            try
            {
                ClientGlobal.init(conf_filename);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                TrackerClient tracker = new TrackerClient();
                TrackerServer trackerServer = tracker.getConnection();
                StorageServer storageServer = null;
                StorageClient1 client = new StorageClient1(trackerServer, storageServer);

                NameValuePair[] metaList = new NameValuePair[1];
                metaList[0] = new NameValuePair("fileName", local_filename);
                string fileId = client.upload_file1(local_filename, null, metaList);
                Console.WriteLine("upload success. file id is: " + fileId);

                int i = 0;
                while (i++ < 10)
                {
                    byte[] result = client.download_file1(fileId);
                    Console.WriteLine(i + ", download result is: " + result.Length);
                }

                trackerServer.close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}
