
using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using Serilog;
using System;
using System.Reflection;
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
     * client test
     *
     * @author Happy Fish / YuQing
     * @version Version 1.18
     */
    public class TestClient
    {
        private TestClient(ITestOutputHelper output)
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

            try
            {
                ClientGlobal.init(conf_filename);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                long startTime;
                string group_name;
                string remote_filename;
                ServerInfo[] servers;
                TrackerClient tracker = new TrackerClient();
                TrackerServer trackerServer = tracker.getConnection();

                StorageServer storageServer = null;

                /*
              storageServer = tracker.getStoreStorage(trackerServer);
                if (storageServer == null)
                {
                    Console.WriteLine("getStoreStorage fail, error code: " + tracker.getErrorCode());
                    return;
                }
                */

                StorageClient client = new StorageClient(trackerServer, storageServer);
                byte[] file_buff;
                NameValuePair[] meta_list;
                string[] results;
                string master_filename;
                string prefix_name;
                string file_ext_name;
                string generated_slave_filename;
                int errno;

                meta_list = new NameValuePair[4];
                meta_list[0] = new NameValuePair("width", "800");
                meta_list[1] = new NameValuePair("heigth", "600");
                meta_list[2] = new NameValuePair("bgcolor", "#FFFFFF");
                meta_list[3] = new NameValuePair("author", "Mike");

                file_buff = ClientGlobal.g_charset.GetBytes("this is a test");
                Console.WriteLine("file Length: " + file_buff.Length);

                group_name = null;
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

                startTime = DateTime.Now.Ticks;
                results = client.upload_file(file_buff, "txt", meta_list);
                Console.WriteLine("upload_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");

                /*
                group_name = "";
                results = client.upload_file(group_name, file_buff, "txt", meta_list);
                */
                if (results == null)
                {
                    Log.Error("upload file fail, error code: " + client.getErrorCode());
                    return;
                }
                else
                {
                    group_name = results[0];
                    remote_filename = results[1];
                    Log.Error("group_name: " + group_name + ", remote_filename: " + remote_filename);
                    Log.Error(client.get_file_info(group_name, remote_filename).ToString());

                    servers = tracker.getFetchStorages(trackerServer, group_name, remote_filename);
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

                    startTime = DateTime.Now.Ticks;
                    errno = client.set_metadata(group_name, remote_filename, meta_list, (byte)ProtoCommon.STORAGE_SET_METADATA_FLAG_MERGE);
                    Console.WriteLine("set_metadata time used: " + (DateTime.Now.Ticks - startTime) + " ms");
                    if (errno == 0)
                    {
                        Log.Error("set_metadata success");
                    }
                    else
                    {
                        Log.Error("set_metadata fail, error no: " + errno);
                    }

                    meta_list = client.get_metadata(group_name, remote_filename);
                    if (meta_list != null)
                    {
                        for (int i = 0; i < meta_list.Length; i++)
                        {
                            Console.WriteLine(meta_list[i].getName() + " " + meta_list[i].getValue());
                        }
                    }

                    //Thread.sleep(30000);

                    startTime = DateTime.Now.Ticks;
                    file_buff = client.download_file(group_name, remote_filename);
                    Console.WriteLine("download_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");

                    if (file_buff != null)
                    {
                        Console.WriteLine("file Length:" + file_buff.Length);
                        Console.WriteLine(ClientGlobal.g_charset.GetString(file_buff));
                    }

                    file_buff = ClientGlobal.g_charset.GetBytes("this is a slave buff");
                    master_filename = remote_filename;
                    prefix_name = "-part1";
                    file_ext_name = "txt";
                    startTime = DateTime.Now.Ticks;
                    results = client.upload_file(group_name, master_filename, prefix_name, file_buff, file_ext_name, meta_list);
                    Console.WriteLine("upload_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");
                    if (results != null)
                    {
                        Log.Error("slave file group_name: " + results[0] + ", remote_filename: " + results[1]);

                        generated_slave_filename = ProtoCommon.genSlaveFilename(master_filename, prefix_name, file_ext_name);
                        if (generated_slave_filename != results[1])
                        {
                            Log.Error("generated slave file: " + generated_slave_filename + "\n != returned slave file: " + results[1]);
                        }

                        Log.Error(client.get_file_info(results[0], results[1]).ToString());
                    }

                    startTime = DateTime.Now.Ticks;
                    errno = client.delete_file(group_name, remote_filename);
                    Console.WriteLine("delete_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");
                    if (errno == 0)
                    {
                        Log.Error("Delete file success");
                    }
                    else
                    {
                        Log.Error("Delete file fail, error no: " + errno);
                    }
                }

                results = client.upload_file(local_filename, null, meta_list);
                if (results != null)
                {
                    string file_id;
                    int ts;
                    string token;
                    string file_url;
                    InetSocketAddress inetSockAddr;

                    group_name = results[0];
                    remote_filename = results[1];
                    file_id = group_name + StorageClient1.SPLIT_GROUP_NAME_AND_FILENAME_SEPERATOR + remote_filename;

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

                    Log.Error("group_name: " + group_name + ", remote_filename: " + remote_filename);
                    Log.Error(client.get_file_info(group_name, remote_filename).ToString());
                    Log.Error("file url: " + file_url);

                    errno = client.download_file(group_name, remote_filename, 0, 0, "c:\\" + remote_filename.Replace("/", "_"));
                    if (errno == 0)
                    {
                        Log.Error("Download file success");
                    }
                    else
                    {
                        Log.Error("Download file fail, error no: " + errno);
                    }

                    errno = client.download_file(group_name, remote_filename, 0, 0, new DownloadFileWriter("c:\\" + remote_filename.Replace("/", "-")));
                    if (errno == 0)
                    {
                        Log.Error("Download file success");
                    }
                    else
                    {
                        Log.Error("Download file fail, error no: " + errno);
                    }

                    master_filename = remote_filename;
                    prefix_name = "-part2";
                    file_ext_name = null;
                    startTime = DateTime.Now.Ticks;
                    results = client.upload_file(group_name, master_filename, prefix_name, local_filename, null, meta_list);
                    Console.WriteLine("upload_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");
                    if (results != null)
                    {
                        Log.Error("slave file group_name: " + results[0] + ", remote_filename: " + results[1]);

                        generated_slave_filename = ProtoCommon.genSlaveFilename(master_filename, prefix_name, file_ext_name);
                        if (generated_slave_filename != results[1])
                        {
                            Log.Error("generated slave file: " + generated_slave_filename + "\n != returned slave file: " + results[1]);
                        }

                        Log.Error(client.get_file_info(results[0], results[1]).ToString());
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

                results = client.upload_file(null, f.Length,
                  new UploadLocalFileSender(local_filename), file_ext_name, meta_list);
                if (results != null)
                {
                    group_name = results[0];
                    remote_filename = results[1];

                    Console.WriteLine("group name: " + group_name + ", remote filename: " + remote_filename);
                    Console.WriteLine(client.get_file_info(group_name, remote_filename));

                    master_filename = remote_filename;
                    prefix_name = "-part3";
                    startTime = DateTime.Now.Ticks;
                    results = client.upload_file(group_name, master_filename, prefix_name, f.Length, new UploadLocalFileSender(local_filename), file_ext_name, meta_list);
                    Console.WriteLine("upload_file time used: " + (DateTime.Now.Ticks - startTime) + " ms");
                    if (results != null)
                    {
                        Log.Error("slave file group_name: " + results[0] + ", remote_filename: " + results[1]);

                        generated_slave_filename = ProtoCommon.genSlaveFilename(master_filename, prefix_name, file_ext_name);
                        if (generated_slave_filename != results[1])
                        {
                            Log.Error("generated slave file: " + generated_slave_filename + "\n != returned slave file: " + results[1]);
                        }

                        Log.Error(client.get_file_info(results[0], results[1]).ToString());
                    }
                }
                else
                {
                    Log.Error("Upload file fail, error no: " + errno);
                }

                storageServer = tracker.getFetchStorage(trackerServer, group_name, remote_filename);
                if (storageServer == null)
                {
                    Console.WriteLine("getFetchStorage fail, errno code: " + tracker.getErrorCode());
                    return;
                }
                /* for test only */
                Console.WriteLine("active test to storage server: " + ProtoCommon.activeTest(storageServer.getSocket()));

                storageServer.close();

                /* for test only */
                Console.WriteLine("active test to tracker server: " + ProtoCommon.activeTest(trackerServer.getSocket()));

                trackerServer.close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}
