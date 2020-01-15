
using org.csource.fastdfs.encapsulation;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
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
    /**
     * load test class
     *
     * @author Happy Fish / YuQing
     * @version Version 1.11
     */
    public class TestLoad
    {
        public static ConcurrentQueue<string> file_ids;
        public static int total_download_count = 0;
        public static int success_download_count = 0;
        public static int fail_download_count = 0;
        public static int total_upload_count = 0;
        public static int success_upload_count = 0;
        public static int upload_thread_count = 0;

        private TestLoad(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Xunit(output)
                .CreateLogger();
        }

        /**
         * entry point
         *
         * @param args comand arguments
         *             <ul><li>args[0]: config filename</li></ul>
         */
        public static void main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error: Must have 1 parameter: config filename");
                return;
            }

            Console.WriteLine("dotnetcore.version=" + typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString());

            try
            {
                ClientGlobal.init(args[0]);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                file_ids = new ConcurrentQueue<string>();

                for (int i = 0; i < 10; i++)
                {
                    new Thread(new ParameterizedThreadStart(UploadThread)).Start(i);
                }

                for (int i = 0; i < 20; i++)
                {
                    new Thread(new ParameterizedThreadStart(DownloadThread)).Start(i);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /**
         * discard file content callback class when download file
         *
         * @author Happy Fish / YuQing
         * @version Version 1.0
         */
        public class DownloadFileDiscard : DownloadCallback
        {
            public DownloadFileDiscard()
            {
            }

            public int recv(long file_size, byte[] data, int bytes)
            {
                return 0;
            }
        }

        /**
         * file uploader
         *
         * @author Happy Fish / YuQing
         * @version Version 1.0
         */
        public class Uploader
        {
            public TrackerClient tracker;
            public TrackerServer trackerServer;

            public Uploader()
            {
                this.tracker = new TrackerClient();
                this.trackerServer = tracker.getTrackerServer();
            }

            public int uploadFile()
            {
                StorageServer storageServer = null;
                StorageClient1 client = new StorageClient1(trackerServer, storageServer);
                byte[] file_buff;
                string file_id;

                file_buff = new byte[2 * 1024];
                Arrays.fill(file_buff, (byte)65);

                try
                {
                    file_id = client.upload_file1(file_buff, "txt", null);
                    if (file_id == null)
                    {
                        Console.WriteLine("upload file fail, error code: " + client.getErrorCode());
                        return -1;
                    }

                    file_ids.Enqueue(file_id);
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("upload file fail, error mesg: " + ex.Message);
                    return -1;
                }
            }
        }

        /**
         * file downloader
         *
         * @author Happy Fish / YuQing
         * @version Version 1.0
         */
        public class Downloader
        {
            public TrackerClient tracker;
            public TrackerServer trackerServer;
            public DownloadFileDiscard callback;

            public Downloader()
            {
                this.tracker = new TrackerClient();
                this.trackerServer = tracker.getTrackerServer();
                this.callback = new DownloadFileDiscard();
            }

            public int downloadFile(string file_id)
            {
                int errno;
                StorageServer storageServer = null;
                StorageClient1 client = new StorageClient1(trackerServer, storageServer);

                try
                {
                    errno = client.download_file1(file_id, this.callback);
                    if (errno != 0)
                    {
                        Console.WriteLine("Download file fail, file_id: " + file_id + ", error no: " + errno);
                    }
                    return errno;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Download file fail, error mesg: " + ex.Message);
                    return -1;
                }
            }
        }

        /**
         * upload file thread
         *
         * @author Happy Fish / YuQing
         * @version Version 1.0
         */
        public static void UploadThread(object obj)
        {
            int thread_index = (int)obj;
            try
            {
                TestLoad.upload_thread_count++;
                Uploader uploader = new Uploader();

                Console.WriteLine("upload thread " + thread_index + " start");

                for (int i = 0; i < 50000; i++)
                {
                    TestLoad.total_upload_count++;
                    if (uploader.uploadFile() == 0)
                    {
                        TestLoad.success_upload_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            finally
            {
                TestLoad.upload_thread_count--;
            }

            Console.WriteLine("upload thread " + thread_index
              + " exit, total_upload_count: " + TestLoad.total_upload_count
              + ", success_upload_count: " + TestLoad.success_upload_count
              + ", total_download_count: " + TestLoad.total_download_count
              + ", success_download_count: " + TestLoad.success_download_count);
        }

        private static object counter_lock = new object();
        /**
         * download file thread
         *
         * @author Happy Fish / YuQing
         * @version Version 1.0
         */
        public static void DownloadThread(object obj)
        {
            int thread_index = (int)obj;
            try
            {
                string file_id;
                Downloader downloader = new Downloader();

                Console.WriteLine("download thread " + thread_index + " start");

                file_id = "";
                while (TestLoad.upload_thread_count != 0 || file_id != null)
                {
                    file_ids.TryDequeue(out file_id);
                    if (file_id == null)
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    lock (counter_lock)
                    {
                        TestLoad.total_download_count++;
                    }
                    if (downloader.downloadFile(file_id) == 0)
                    {
                        lock (counter_lock)
                        {
                            TestLoad.success_download_count++;
                        }
                    }
                    else
                    {
                        TestLoad.fail_download_count++;
                    }
                }

                for (int i = 0; i < 3 && TestLoad.total_download_count < TestLoad.total_upload_count; i++)
                {
                    file_ids.TryDequeue(out file_id);
                    if (file_id == null)
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    lock (counter_lock)
                    {
                        TestLoad.total_download_count++;
                    }
                    if (downloader.downloadFile(file_id) == 0)
                    {
                        lock (counter_lock)
                        {
                            TestLoad.success_download_count++;
                        }
                    }
                    else
                    {
                        TestLoad.fail_download_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            Console.WriteLine("download thread " + thread_index
              + " exit, total_download_count: " + TestLoad.total_download_count
              + ", success_download_count: " + TestLoad.success_download_count
              + ", fail_download_count: " + TestLoad.fail_download_count);
        }
    }
}
