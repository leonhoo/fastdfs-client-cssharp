using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using Serilog;
using System;
using Xunit.Abstractions;

namespace org.csource.fastdfs
{
    public class Test1
    {

        private Test1(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Xunit(output)
                .CreateLogger();
        }

        public static void main(string[] args)
        {
            try
            {

                if (args.Length < 1)
                {
                    Console.WriteLine("Usage: 2 parameters, one is config filename, "
                            + "the other is the local filename to upload");
                    return;
                }

                string conf_filename = args[0];
                string local_filename;
                string ext_name;
                if (args.Length > 1)
                {
                    local_filename = args[1];
                    ext_name = null;
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    local_filename = "c:/windows/system32/notepad.exe";
                    ext_name = "exe";
                }
                else
                {
                    local_filename = "/etc/hosts";
                    ext_name = "";
                }

                ClientGlobal.init(conf_filename);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                TrackerGroup tg = new TrackerGroup(new InetSocketAddress[] { new InetSocketAddress("47.95.221.159", 22122) });
                TrackerClient tc = new TrackerClient(tg);

                TrackerServer ts = tc.getTrackerServer();
                if (ts == null)
                {
                    Console.WriteLine("getTrackerServer return null");
                    return;
                }

                StorageServer ss = tc.getStoreStorage(ts);
                if (ss == null)
                {
                    Console.WriteLine("getStoreStorage return null");
                }

                StorageClient1 sc1 = new StorageClient1(ts, ss);

                NameValuePair[] meta_list = null;  //new NameValuePair[0];
                string fileid = sc1.upload_file1(local_filename, ext_name, meta_list);
                Console.WriteLine("Upload local file " + local_filename + " ok, fileid: " + fileid);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}