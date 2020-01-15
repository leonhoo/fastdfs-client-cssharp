
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using Serilog;
using System;
using System.Reflection;
using Xunit.Abstractions;
/// <summary>
/// Copyright(C) 2008 Happy Fish / YuQing
/// <p>
/// FastDFS Java Client may be copied only under the terms of the GNU Lesser
/// General Public License(LGPL).
/// Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{
    /// <summary>
    /// client test
    ///
    /// @author Happy Fish / YuQing
    /// @version Version 1.16
    /// </summary>
    [TestClass]
    public class TestClient1
    {
        private TestClient1(ITestOutputHelper output)
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
         *             <ul><li>args[1]: local filename to upload</li></ul>
         */
        [TestMethod]
        public static void main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Must have 2 parameters, one is config filename, "
                  + "the other is the local filename to upload");
                return;
            }

            Console.WriteLine("dotnetcore.version=" + typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString());

            string conf_filename = args[0];
            string local_filename = args[1];
            string group_name;

            try
            {
                ClientGlobal.init(conf_filename);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                string file_id;

                TrackerClient tracker = new TrackerClient();
                TrackerServer trackerServer = tracker.getTrackerServer();

                StorageServer storageServer = null;
                /*
                storageServer = tracker.getStoreStorage(trackerServer);
                  if (storageServer == null)
                  {
                      Console.WriteLine("getStoreStorage fail, error code: " + tracker.getErrorCode());
                      return;
                  }
                  */
                StorageClient1 client = new StorageClient1(trackerServer, storageServer);
                byte[] file_buff;
                NameValuePair[] meta_list;
                string master_file_id;
                string prefix_name;
                string file_ext_name;
                string slave_file_id;
                string generated_slave_file_id;
                int errno;

                group_name = "group1";
                StorageServer[] storageServers = tracker.getStoreStorages(trackerServer, group_name);
                if (storageServers == null)
                {
                    Log.Error("get store storage servers fail, error code: " + tracker.getErrorCode());
                }
                else
                {
                    Log.Error("store storage servers count: " + storageServers.Length);
                    for (int k = 0; k < storageServers.Length; k++)
                    {
                        Log.Error((k + 1) + ". " + storageServers[k].getInetSocketAddress().Address + ":" + storageServers[k].getInetSocketAddress().Port);
                    }
                    Log.Error("");
                }

                meta_list = new NameValuePair[4];
                meta_list[0] = new NameValuePair("width", "800");
                meta_list[1] = new NameValuePair("heigth", "600");
                meta_list[2] = new NameValuePair("bgcolor", "#FFFFFF");
                meta_list[3] = new NameValuePair("author", "Mike");

                file_buff = ClientGlobal.g_charset.GetBytes("this is a test");
                Console.WriteLine("file Length: " + file_buff.Length);

                file_id = client.upload_file1(file_buff, "txt", meta_list);
                /*
                group_name = "group1";
                file_id = client.upload_file1(group_name, file_buff, "txt", meta_list);
                */
                if (file_id == null)
                {
                    Log.Error("upload file fail, error code: " + client.getErrorCode());
                    return;
                }
                else
                {
                    Log.Error("file_id: " + file_id);
                    Log.Error(client.get_file_info1(file_id).ToString());

                    ServerInfo[] servers = tracker.getFetchStorages1(trackerServer, file_id);
                    if (servers == null)
                    {
                        Log.Error("get storage servers fail, error code: " + tracker.getErrorCode());
                    }
                    else
                    {
                        Log.Error("storage servers count: " + servers.Length);
                        for (int k = 0; k < servers.Length; k++)
                        {
                            Log.Error((k + 1) + ". " + servers[k].getIpAddr() + ":" + servers[k].getPort());
                        }
                        Log.Error("");
                    }

                    meta_list = new NameValuePair[4];
                    meta_list[0] = new NameValuePair("width", "1024");
                    meta_list[1] = new NameValuePair("heigth", "768");
                    meta_list[2] = new NameValuePair("bgcolor", "#000000");
                    meta_list[3] = new NameValuePair("title", "Untitle");

                    if ((errno = client.set_metadata1(file_id, meta_list, (byte)ProtoCommon.STORAGE_SET_METADATA_FLAG_MERGE)) == 0)
                    {
                        Log.Error("set_metadata success");
                    }
                    else
                    {
                        Log.Error("set_metadata fail, error no: " + errno);
                    }

                    meta_list = client.get_metadata1(file_id);
                    if (meta_list != null)
                    {
                        for (int i = 0; i < meta_list.Length; i++)
                        {
                            Console.WriteLine(meta_list[i].getName() + " " + meta_list[i].getValue());
                        }
                    }

                    //Thread.sleep(30000);

                    file_buff = client.download_file1(file_id);
                    if (file_buff != null)
                    {
                        Console.WriteLine("file Length:" + file_buff.Length);
                        Console.WriteLine(ClientGlobal.g_charset.GetString(file_buff));
                    }

                    master_file_id = file_id;
                    prefix_name = "-part1";
                    file_ext_name = "txt";
                    file_buff = ClientGlobal.g_charset.GetBytes("this is a slave buff.");
                    slave_file_id = client.upload_file1(master_file_id, prefix_name, file_buff, file_ext_name, meta_list);
                    if (slave_file_id != null)
                    {
                        Log.Error("slave file_id: " + slave_file_id);
                        Log.Error(client.get_file_info1(slave_file_id).ToString());

                        generated_slave_file_id = ProtoCommon.genSlaveFilename(master_file_id, prefix_name, file_ext_name);
                        if (generated_slave_file_id != slave_file_id)
                        {
                            Log.Error("generated slave file: " + generated_slave_file_id + "\n != returned slave file: " + slave_file_id);
                        }
                    }

                    //Thread.sleep(10000);
                    if ((errno = client.delete_file1(file_id)) == 0)
                    {
                        Log.Error("Delete file success");
                    }
                    else
                    {
                        Log.Error("Delete file fail, error no: " + errno);
                    }
                }

                if ((file_id = client.upload_file1(local_filename, null, meta_list)) != null)
                {
                    int ts;
                    string token;
                    string file_url;
                    InetSocketAddress inetSockAddr;

                    Log.Error("file_id: " + file_id);
                    Log.Error(client.get_file_info1(file_id).ToString());

                    inetSockAddr = trackerServer.getInetSocketAddress();
                    file_url = "http://" + inetSockAddr.Address;
                    if (ClientGlobal.g_tracker_http_port != 80)
                    {
                        file_url += ":" + ClientGlobal.g_tracker_http_port;
                    }
                    file_url += "/" + file_id;
                    if (ClientGlobal.g_anti_steal_token)
                    {
                        ts = (int)(DateTime.Now.Ticks / 1000);
                        token = ProtoCommon.getToken(file_id, ts, ClientGlobal.g_secret_key);
                        file_url += "?token=" + token + "&ts=" + ts;
                    }
                    Log.Error("file url: " + file_url);

                    errno = client.download_file1(file_id, 0, 100, "c:\\" + file_id.Replace("/", "_"));
                    if (errno == 0)
                    {
                        Log.Error("Download file success");
                    }
                    else
                    {
                        Log.Error("Download file fail, error no: " + errno);
                    }

                    errno = client.download_file1(file_id, new DownloadFileWriter("c:\\" + file_id.Replace("/", "-")));
                    if (errno == 0)
                    {
                        Log.Error("Download file success");
                    }
                    else
                    {
                        Log.Error("Download file fail, error no: " + errno);
                    }

                    master_file_id = file_id;
                    prefix_name = "-part2";
                    file_ext_name = null;
                    slave_file_id = client.upload_file1(master_file_id, prefix_name, local_filename, file_ext_name, meta_list);
                    if (slave_file_id != null)
                    {
                        Log.Error("slave file_id: " + slave_file_id);
                        Log.Error(client.get_file_info1(slave_file_id).ToString());

                        generated_slave_file_id = ProtoCommon.genSlaveFilename(master_file_id, prefix_name, file_ext_name);
                        if (generated_slave_file_id != slave_file_id)
                        {
                            Log.Error("generated slave file: " + generated_slave_file_id + "\n != returned slave file: " + slave_file_id);
                        }
                    }
                }

                System.IO.FileInfo f;
                f = new System.IO.FileInfo(local_filename);
                int nPos = local_filename.LastIndexOf('.');
                if (nPos > 0 && local_filename.Length - nPos <= ProtoCommon.FDFS_FILE_EXT_NAME_MAX_LEN + 1)
                {
                    file_ext_name = local_filename.Substring(nPos + 1);
                }
                else
                {
                    file_ext_name = null;
                }

                file_id = client.upload_file1(null, f.Length, new UploadLocalFileSender(local_filename), file_ext_name, meta_list);
                if (file_id != null)
                {
                    Console.WriteLine("file id: " + file_id);
                    Console.WriteLine(client.get_file_info1(file_id));
                    master_file_id = file_id;
                    prefix_name = "-part3";
                    slave_file_id = client.upload_file1(master_file_id, prefix_name, f.Length, new UploadLocalFileSender(local_filename), file_ext_name, meta_list);
                    if (slave_file_id != null)
                    {
                        Log.Error("slave file_id: " + slave_file_id);
                        generated_slave_file_id = ProtoCommon.genSlaveFilename(master_file_id, prefix_name, file_ext_name);
                        if (generated_slave_file_id != slave_file_id)
                        {
                            Log.Error("generated slave file: " + generated_slave_file_id + "\n != returned slave file: " + slave_file_id);
                        }
                    }
                }
                else
                {
                    Log.Error("Upload file fail, error no: " + errno);
                }

                storageServer = tracker.getFetchStorage1(trackerServer, file_id);
                if (storageServer == null)
                {
                    Console.WriteLine("getFetchStorage fail, errno code: " + tracker.getErrorCode());
                    return;
                }

                /* for test only */
                Console.WriteLine("active test to storage server: " + storageServer.getConnection().activeTest());

                /* for test only */
                Console.WriteLine("active test to tracker server: " + trackerServer.getConnection().activeTest());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}
