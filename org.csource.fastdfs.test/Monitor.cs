
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
    /**
     * load test class
     *
     * @author Happy Fish / YuQing
     * @version Version 1.20
     */
    public class Monitor
    {
        private Monitor(ITestOutputHelper output)
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

            try
            {
                ClientGlobal.init(args[0]);
                Console.WriteLine("network_timeout=" + ClientGlobal.g_network_timeout + "ms");
                Console.WriteLine("charset=" + ClientGlobal.g_charset);

                TrackerClient tracker = new TrackerClient();

                /*
              Console.WriteLine("delete storage return: " + tracker.deleteStorage("group1", "192.168.0.192"));
                Console.WriteLine("delete storage errno: " + tracker.getErrorCode());
                */

                TrackerServer trackerServer = tracker.getTrackerServer();
                if (trackerServer == null)
                {
                    return;
                }

                int count;
                StructGroupStat[] groupStats = tracker.listGroups(trackerServer);
                if (groupStats == null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("ERROR! list groups error, error no: " + tracker.getErrorCode());
                    Console.WriteLine("");
                    return;
                }

                Console.WriteLine("group count: " + groupStats.Length);

                count = 0;
                foreach (StructGroupStat groupStat in groupStats)
                {
                    count++;
                    Console.WriteLine("Group " + count + ":");
                    Console.WriteLine("group name = " + groupStat.getGroupName());
                    Console.WriteLine("disk total space = " + groupStat.getTotalMB() + "MB");
                    Console.WriteLine("disk free space = " + groupStat.getFreeMB() + " MB");
                    Console.WriteLine("trunk free space = " + groupStat.getTrunkFreeMB() + " MB");
                    Console.WriteLine("storage server count = " + groupStat.getStorageCount());
                    Console.WriteLine("active server count = " + groupStat.getActiveCount());
                    Console.WriteLine("storage server port = " + groupStat.getStoragePort());
                    Console.WriteLine("storage HTTP port = " + groupStat.getStorageHttpPort());
                    Console.WriteLine("store path count = " + groupStat.getStorePathCount());
                    Console.WriteLine("subdir count per path = " + groupStat.getSubdirCountPerPath());
                    Console.WriteLine("current write server index = " + groupStat.getCurrentWriteServer());
                    Console.WriteLine("current trunk file id = " + groupStat.getCurrentTrunkFileId());

                    StructStorageStat[] storageStats = tracker.listStorages(trackerServer, groupStat.getGroupName());
                    if (storageStats == null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("ERROR! list storage error, error no: " + tracker.getErrorCode());
                        Console.WriteLine("");
                        break;
                    }

                    var format = "yyyy-MM-dd HH:mm:ss";
                    int stroageCount = 0;
                    foreach (var storageStat in storageStats)
                    {
                        stroageCount++;
                        Console.WriteLine("\tStorage " + stroageCount + ":");
                        Console.WriteLine("\t\tstorage id = " + storageStat.getId());
                        Console.WriteLine("\t\tip_addr = " + storageStat.getIpAddr() + "  " + ProtoCommon.getStorageStatusCaption(storageStat.getStatus()));
                        Console.WriteLine("\t\thttp domain = " + storageStat.getDomainName());
                        Console.WriteLine("\t\tversion = " + storageStat.getVersion());
                        Console.WriteLine("\t\tjoin time = " + storageStat.getJoinTime().ToString(format));
                        Console.WriteLine("\t\tup time = " + (storageStat.getUpTime().Ticks == 0 ? "" : storageStat.getUpTime().ToString(format)));
                        Console.WriteLine("\t\ttotal storage = " + storageStat.getTotalMB() + "MB");
                        Console.WriteLine("\t\tfree storage = " + storageStat.getFreeMB() + "MB");
                        Console.WriteLine("\t\tupload priority = " + storageStat.getUploadPriority());
                        Console.WriteLine("\t\tstore_path_count = " + storageStat.getStorePathCount());
                        Console.WriteLine("\t\tsubdir_count_per_path = " + storageStat.getSubdirCountPerPath());
                        Console.WriteLine("\t\tstorage_port = " + storageStat.getStoragePort());
                        Console.WriteLine("\t\tstorage_http_port = " + storageStat.getStorageHttpPort());
                        Console.WriteLine("\t\tcurrent_write_path = " + storageStat.getCurrentWritePath());
                        Console.WriteLine("\t\tsource ip_addr = " + storageStat.getSrcIpAddr());
                        Console.WriteLine("\t\tif_trunk_server = " + storageStat.isTrunkServer());
                        Console.WriteLine("\t\tconntion.alloc_count  = " + storageStat.getConnectionAllocCount());
                        Console.WriteLine("\t\tconntion.current_count  = " + storageStat.getConnectionCurrentCount());
                        Console.WriteLine("\t\tconntion.max_count  = " + storageStat.getConnectionMaxCount());
                        Console.WriteLine("\t\ttotal_upload_count = " + storageStat.getTotalUploadCount());
                        Console.WriteLine("\t\tsuccess_upload_count = " + storageStat.getSuccessUploadCount());
                        Console.WriteLine("\t\ttotal_append_count = " + storageStat.getTotalAppendCount());
                        Console.WriteLine("\t\tsuccess_append_count = " + storageStat.getSuccessAppendCount());
                        Console.WriteLine("\t\ttotal_modify_count = " + storageStat.getTotalModifyCount());
                        Console.WriteLine("\t\tsuccess_modify_count = " + storageStat.getSuccessModifyCount());
                        Console.WriteLine("\t\ttotal_truncate_count = " + storageStat.getTotalTruncateCount());
                        Console.WriteLine("\t\tsuccess_truncate_count = " + storageStat.getSuccessTruncateCount());
                        Console.WriteLine("\t\ttotal_set_meta_count = " + storageStat.getTotalSetMetaCount());
                        Console.WriteLine("\t\tsuccess_set_meta_count = " + storageStat.getSuccessSetMetaCount());
                        Console.WriteLine("\t\ttotal_delete_count = " + storageStat.getTotalDeleteCount());
                        Console.WriteLine("\t\tsuccess_delete_count = " + storageStat.getSuccessDeleteCount());
                        Console.WriteLine("\t\ttotal_download_count = " + storageStat.getTotalDownloadCount());
                        Console.WriteLine("\t\tsuccess_download_count = " + storageStat.getSuccessDownloadCount());
                        Console.WriteLine("\t\ttotal_get_meta_count = " + storageStat.getTotalGetMetaCount());
                        Console.WriteLine("\t\tsuccess_get_meta_count = " + storageStat.getSuccessGetMetaCount());
                        Console.WriteLine("\t\ttotal_create_link_count = " + storageStat.getTotalCreateLinkCount());
                        Console.WriteLine("\t\tsuccess_create_link_count = " + storageStat.getSuccessCreateLinkCount());
                        Console.WriteLine("\t\ttotal_delete_link_count = " + storageStat.getTotalDeleteLinkCount());
                        Console.WriteLine("\t\tsuccess_delete_link_count = " + storageStat.getSuccessDeleteLinkCount());
                        Console.WriteLine("\t\ttotal_upload_bytes = " + storageStat.getTotalUploadBytes());
                        Console.WriteLine("\t\tsuccess_upload_bytes = " + storageStat.getSuccessUploadBytes());
                        Console.WriteLine("\t\ttotal_append_bytes = " + storageStat.getTotalAppendBytes());
                        Console.WriteLine("\t\tsuccess_append_bytes = " + storageStat.getSuccessAppendBytes());
                        Console.WriteLine("\t\ttotal_modify_bytes = " + storageStat.getTotalModifyBytes());
                        Console.WriteLine("\t\tsuccess_modify_bytes = " + storageStat.getSuccessModifyBytes());
                        Console.WriteLine("\t\ttotal_download_bytes = " + storageStat.getTotalDownloadloadBytes());
                        Console.WriteLine("\t\tsuccess_download_bytes = " + storageStat.getSuccessDownloadloadBytes());
                        Console.WriteLine("\t\ttotal_sync_in_bytes = " + storageStat.getTotalSyncInBytes());
                        Console.WriteLine("\t\tsuccess_sync_in_bytes = " + storageStat.getSuccessSyncInBytes());
                        Console.WriteLine("\t\ttotal_sync_out_bytes = " + storageStat.getTotalSyncOutBytes());
                        Console.WriteLine("\t\tsuccess_sync_out_bytes = " + storageStat.getSuccessSyncOutBytes());
                        Console.WriteLine("\t\ttotal_file_open_count = " + storageStat.getTotalFileOpenCount());
                        Console.WriteLine("\t\tsuccess_file_open_count = " + storageStat.getSuccessFileOpenCount());
                        Console.WriteLine("\t\ttotal_file_read_count = " + storageStat.getTotalFileReadCount());
                        Console.WriteLine("\t\tsuccess_file_read_count = " + storageStat.getSuccessFileReadCount());
                        Console.WriteLine("\t\ttotal_file_write_count = " + storageStat.getTotalFileWriteCount());
                        Console.WriteLine("\t\tsuccess_file_write_count = " + storageStat.getSuccessFileWriteCount());
                        Console.WriteLine("\t\tlast_heart_beat_time = " + storageStat.getLastHeartBeatTime().ToString(format));
                        Console.WriteLine("\t\tlast_source_update = " + storageStat.getLastSourceUpdate().ToString(format));
                        Console.WriteLine("\t\tlast_sync_update = " + storageStat.getLastSyncUpdate().ToString(format));
                        Console.WriteLine("\t\tlast_synced_timestamp = " + storageStat.getLastSyncedTimestamp().ToString(format) + getSyncedDelayString(storageStats, storageStat));
                    }
                }
                 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        protected static string getSyncedDelayString(StructStorageStat[] storageStats, StructStorageStat currentStorageStat)
        {
            long maxLastSourceUpdate = 0;
            foreach (var storageStat in storageStats)
            {
                if (storageStat != currentStorageStat && storageStat.getLastSourceUpdate().Ticks > maxLastSourceUpdate)
                {
                    maxLastSourceUpdate = storageStat.getLastSourceUpdate().Ticks;
                }
            }

            if (maxLastSourceUpdate == 0)
            {
                return "";
            }

            if (currentStorageStat.getLastSyncedTimestamp().Ticks == 0)
            {
                return " (never synced)";
            }

            int delaySeconds = (int)((maxLastSourceUpdate - currentStorageStat.getLastSyncedTimestamp().Ticks) / 1000);
            int day = delaySeconds / (24 * 3600);
            int remainSeconds = delaySeconds % (24 * 3600);
            int hour = remainSeconds / 3600;
            remainSeconds %= 3600;
            int minute = remainSeconds / 60;
            int second = remainSeconds % 60;
            string delayTimeStr;
            if (day != 0)
            {
                delayTimeStr = string.Format("%1$d days %2$02dh:%3$02dm:%4$02ds", day, hour, minute, second);
            }
            else if (hour != 0)
            {
                delayTimeStr = string.Format("%1$02dh:%2$02dm:%3$02ds", hour, minute, second);
            }
            else if (minute != 0)
            {
                delayTimeStr = string.Format("%1$02dm:%2$02ds", minute, second);
            }
            else
            {
                delayTimeStr = string.Format("%1$ds", second);
            }

            return " (" + delayTimeStr + " delay)";
        }
    }
}