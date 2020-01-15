using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.csource.fastdfs.common;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;

namespace org.csource.fastdfs
{
    /**
     * @author chengdu
     * @date 2019/7/13.
     */
    public class FdfsTest
    {
        private const string CONF_NAME = "fdfstest.conf";

        private StorageClient storageClient;

        private TrackerServer trackerServer;

        public FdfsTest(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Xunit(output)
                .CreateLogger();
        }

        [TestInitialize]
        public void initStorageClient()
        {
            ClientGlobal.init(CONF_NAME);
            Log.Information("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
            Log.Information("charset=" + ClientGlobal.g_charset);
            TrackerClient tracker = new TrackerClient();
            trackerServer = tracker.getTrackerServer();
            StorageServer storageServer = null;
            storageClient = new StorageClient(trackerServer, storageServer);
        }
        [TestCleanup]
        public void closeClient()
        {
            Log.Information("close connection");
            if (storageClient != null)
            {
                try
                {
                    storageClient.close();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message + ex.StackTrace);
                }
            }
        }

        public void writeByteToFile(byte[] fbyte, string fileName)
        {
            using (var fos = File.OpenWrite(fileName))
            {
                fos.Write(fbyte);
                fos.Flush();
            }
        }

        [TestMethod]
        public void upload()
        {
            NameValuePair[]
            metaList = new NameValuePair[1];
            string local_filename = "build.PNG";
            metaList[0] = new NameValuePair("fileName", local_filename);
            Stream inputStream = File.OpenRead("C:/Users/chengdu/Desktop/build.PNG");
            long Length = inputStream.Length;
            byte[] bytes = new byte[Length];
            inputStream.Read(bytes);
            string[] result = storageClient.upload_file(bytes, null, metaList);
            Log.Information("result {0}", string.Join(',', result.ToList()));
            Assert.Equals(2, result.Length);
        }

        [TestMethod]
        public void download()
        {
            string[] uploadresult = { "group1", "M00/00/00/wKgBZV0phl2ASV1nAACk1tFxwrM3814331" };
            byte[] result = storageClient.download_file(uploadresult[0], uploadresult[1]);
            string local_filename = "build.PNG";
            writeByteToFile(result, local_filename);
            Assert.IsTrue(File.Exists(local_filename));
        }

        [TestMethod]
        public void testUploadDownload()
        {
            NameValuePair[] metaList = new NameValuePair[1];
            string local_filename = "build.PNG";
            metaList[0] = new NameValuePair("fileName", local_filename);
            Stream inputStream = File.OpenWrite("C:/Users/chengdu/Desktop/build.PNG");
            long Length = inputStream.Length;
            byte[] bytes = new byte[Length];
            inputStream.Read(bytes);
            string[] result = storageClient.upload_file(bytes, null, metaList);
            Assert.IsTrue(storageClient.isConnected());
            // pool testOnborrow  isAvaliable
            Assert.IsTrue(storageClient.isAvaliable());
            Log.Information("result {0}", string.Join(',', result.ToList()));
            byte[] resultbytes = storageClient.download_file(result[0], result[1]);
            writeByteToFile(resultbytes, local_filename);
            Assert.IsTrue(File.Exists(local_filename));
        }
    }

}
