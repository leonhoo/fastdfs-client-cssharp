using org.csource.fastdfs.common;
using org.csource.fastdfs.encapsulation;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail./namespace org.csource.fastdfs{protocol common functions
/// </summary>
public class ProtoCommon
{
    public const byte FDFS_PROTO_CMD_QUIT = 82;
    public const byte TRACKER_PROTO_CMD_SERVER_LIST_GROUP = 91;
    public const byte TRACKER_PROTO_CMD_SERVER_LIST_STORAGE = 92;
    public const byte TRACKER_PROTO_CMD_SERVER_DELETE_STORAGE = 93;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITHOUT_GROUP_ONE = 101;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_FETCH_ONE = 102;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_UPDATE = 103;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITH_GROUP_ONE = 104;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_FETCH_ALL = 105;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITHOUT_GROUP_ALL = 106;
    public const byte TRACKER_PROTO_CMD_SERVICE_QUERY_STORE_WITH_GROUP_ALL = 107;
    public const byte TRACKER_PROTO_CMD_RESP = 100;
    public const byte FDFS_PROTO_CMD_ACTIVE_TEST = 111;
    public const byte STORAGE_PROTO_CMD_UPLOAD_FILE = 11;
    public const byte STORAGE_PROTO_CMD_DELETE_FILE = 12;
    public const byte STORAGE_PROTO_CMD_SET_METADATA = 13;
    public const byte STORAGE_PROTO_CMD_DOWNLOAD_FILE = 14;
    public const byte STORAGE_PROTO_CMD_GET_METADATA = 15;
    public const byte STORAGE_PROTO_CMD_UPLOAD_SLAVE_FILE = 21;
    public const byte STORAGE_PROTO_CMD_QUERY_FILE_INFO = 22;
    public const byte STORAGE_PROTO_CMD_UPLOAD_APPENDER_FILE = 23;  //create appender file
    public const byte STORAGE_PROTO_CMD_APPEND_FILE = 24;  //append file
    public const byte STORAGE_PROTO_CMD_MODIFY_FILE = 34;  //modify appender file
    public const byte STORAGE_PROTO_CMD_TRUNCATE_FILE = 36;  //truncate appender file
    public const byte STORAGE_PROTO_CMD_RESP = TRACKER_PROTO_CMD_RESP;
    public const byte FDFS_STORAGE_STATUS_INIT = 0;
    public const byte FDFS_STORAGE_STATUS_WAIT_SYNC = 1;
    public const byte FDFS_STORAGE_STATUS_SYNCING = 2;
    public const byte FDFS_STORAGE_STATUS_IP_CHANGED = 3;
    public const byte FDFS_STORAGE_STATUS_DELETED = 4;
    public const byte FDFS_STORAGE_STATUS_OFFLINE = 5;
    public const byte FDFS_STORAGE_STATUS_ONLINE = 6;
    public const byte FDFS_STORAGE_STATUS_ACTIVE = 7;
    public const byte FDFS_STORAGE_STATUS_NONE = 99;

    /// <summary>
    /// for overwrite all old metadata
    /// </summary>
    public const char STORAGE_SET_METADATA_FLAG_OVERWRITE = 'O';

    /// <summary>
    /// for replace, insert when the meta item not exist, otherwise update it
    /// </summary>
    public const char STORAGE_SET_METADATA_FLAG_MERGE = 'M';
    public const int FDFS_PROTO_PKG_LEN_SIZE = 8;
    public const int FDFS_PROTO_CMD_SIZE = 1;
    public const int FDFS_GROUP_NAME_MAX_LEN = 16;
    public const int FDFS_IPADDR_SIZE = 16;
    public const int FDFS_DOMAIN_NAME_MAX_SIZE = 128;
    public const int FDFS_VERSION_SIZE = 6;
    public const int FDFS_STORAGE_ID_MAX_SIZE = 16;
    public const char FDFS_RECORD_SEPERATOR = '\u0001';
    public const char FDFS_FIELD_SEPERATOR = '\u0002';
    public const int TRACKER_QUERY_STORAGE_FETCH_BODY_LEN = FDFS_GROUP_NAME_MAX_LEN
    + FDFS_IPADDR_SIZE - 1 + FDFS_PROTO_PKG_LEN_SIZE;
    public const int TRACKER_QUERY_STORAGE_STORE_BODY_LEN = FDFS_GROUP_NAME_MAX_LEN
    + FDFS_IPADDR_SIZE + FDFS_PROTO_PKG_LEN_SIZE;
    public const byte FDFS_FILE_EXT_NAME_MAX_LEN = 6;
    public const byte FDFS_FILE_PREFIX_MAX_LEN = 16;
    public const byte FDFS_FILE_PATH_LEN = 10;
    public const byte FDFS_FILENAME_BASE64_LENGTH = 27;
    public const byte FDFS_TRUNK_FILE_INFO_LEN = 16;
    public const byte ERR_NO_ENOENT = 2;
    public const byte ERR_NO_EIO = 5;
    public const byte ERR_NO_EBUSY = 16;
    public const byte ERR_NO_EINVAL = 22;
    public const byte ERR_NO_ENOSPC = 28;
    public const byte ECONNREFUSED = 61;
    public const byte ERR_NO_EALREADY = 114;
    public const long INFINITE_FILE_SIZE = 256 * 1024L * 1024 * 1024 * 1024 * 1024L;
    public const long APPENDER_FILE_SIZE = INFINITE_FILE_SIZE;
    public const long TRUNK_FILE_MARK_SIZE = 512 * 1024L * 1024 * 1024 * 1024 * 1024L;
    public const long NORMAL_LOGIC_FILENAME_LENGTH = FDFS_FILE_PATH_LEN + FDFS_FILENAME_BASE64_LENGTH + FDFS_FILE_EXT_NAME_MAX_LEN + 1;
    public const long TRUNK_LOGIC_FILENAME_LENGTH = NORMAL_LOGIC_FILENAME_LENGTH + FDFS_TRUNK_FILE_INFO_LEN;
    protected const int PROTO_HEADER_CMD_INDEX = FDFS_PROTO_PKG_LEN_SIZE;
    protected const int PROTO_HEADER_STATUS_INDEX = FDFS_PROTO_PKG_LEN_SIZE + 1;
    private ProtoCommon()
    {
    }
    public static string getStorageStatusCaption(byte status)
    {
        switch (status)
        {
            case FDFS_STORAGE_STATUS_INIT:
                return "INIT";
            case FDFS_STORAGE_STATUS_WAIT_SYNC:
                return "WAIT_SYNC";
            case FDFS_STORAGE_STATUS_SYNCING:
                return "SYNCING";
            case FDFS_STORAGE_STATUS_IP_CHANGED:
                return "IP_CHANGED";
            case FDFS_STORAGE_STATUS_DELETED:
                return "DELETED";
            case FDFS_STORAGE_STATUS_OFFLINE:
                return "OFFLINE";
            case FDFS_STORAGE_STATUS_ONLINE:
                return "ONLINE";
            case FDFS_STORAGE_STATUS_ACTIVE:
                return "ACTIVE";
            case FDFS_STORAGE_STATUS_NONE:
                return "NONE";
            default:
                return "UNKOWN";
        }
    }

    /// <summary>
    /// pack header by FastDFS transfer protocol
    /// </summary>
    /// <param name="cmd">which command to send</param>
    /// <param name="pkg_len">package body length</param>
    /// <param name="errno">status code, should be (byte)0</param>
    /// <returns> packed byte buffer</returns>
    public static byte[] packHeader(byte cmd, long pkg_len, byte errno)
    {
        byte[] header;
        byte[] hex_len;
        header = new byte[FDFS_PROTO_PKG_LEN_SIZE + 2];
        Arrays.fill(header, (byte)0);
        hex_len = ProtoCommon.long2buff(pkg_len);
        Array.Copy(hex_len, 0, header, 0, hex_len.Length);
        header[PROTO_HEADER_CMD_INDEX] = cmd;
        header[PROTO_HEADER_STATUS_INDEX] = errno;
        return header;
    }

    /// <summary>
    /// receive pack header
    /// </summary>
    /// <param name="in">input stream</param>
    /// <param name="expect_cmd">expect response command</param>
    /// <param name="expect_body_len">expect response package body length</param>
    /// <returns> RecvHeaderInfo: errno and pkg body length</returns>
    public static RecvHeaderInfo recvHeader(Stream stream, byte expect_cmd, long expect_body_len)
    {
        byte[] header;
        int bytes;
        long pkg_len;
        header = new byte[FDFS_PROTO_PKG_LEN_SIZE + 2];
        if ((bytes = stream.Read(header, 0, header.Length)) != header.Length)
        {
            throw new IOException("recv package size " + bytes + " != " + header.Length);
        }
        if (header[PROTO_HEADER_CMD_INDEX] != expect_cmd)
        {
            throw new IOException("recv cmd: " + header[PROTO_HEADER_CMD_INDEX] + " is not correct, expect cmd: " + expect_cmd);
        }
        if (header[PROTO_HEADER_STATUS_INDEX] != 0)
        {
            return new RecvHeaderInfo(header[PROTO_HEADER_STATUS_INDEX], 0);
        }
        pkg_len = ProtoCommon.buff2long(header, 0);
        if (pkg_len < 0)
        {
            throw new IOException("recv body length: " + pkg_len + " < 0!");
        }
        if (expect_body_len >= 0 && pkg_len != expect_body_len)
        {
            throw new IOException("recv body length: " + pkg_len + " is not correct, expect length: " + expect_body_len);
        }
        return new RecvHeaderInfo(0, pkg_len);
    }

    /// <summary>
    /// receive whole pack
    /// </summary>
    /// <param name="in">input stream</param>
    /// <param name="expect_cmd">expect response command</param>
    /// <param name="expect_body_len">expect response package body length</param>
    /// <returns> RecvPackageInfo: errno and reponse body(byte buff)</returns>
    public static RecvPackageInfo recvPackage(Stream stream, byte expect_cmd, long expect_body_len)
    {
        RecvHeaderInfo header = recvHeader(stream, expect_cmd, expect_body_len);
        if (header.errno != 0)
        {
            return new RecvPackageInfo(header.errno, null);
        }
        byte[] body = new byte[(int)header.body_len];
        int totalBytes = 0;
        int remainBytes = (int)header.body_len;
        int bytes;
        while (totalBytes < header.body_len)
        {
            if ((bytes = stream.Read(body, totalBytes, remainBytes)) < 0)
            {
                break;
            }
            totalBytes += bytes;
            remainBytes -= bytes;
        }
        if (totalBytes != header.body_len)
        {
            throw new IOException("recv package size " + totalBytes + " != " + header.body_len);
        }
        return new RecvPackageInfo((byte)0, body);
    }

    /// <summary>
    /// split metadata to name value pair array
    /// </summary>
    /// <param name="meta_buff">metadata</param>
    /// <returns> name value pair array</returns>
    public static NameValuePair[] split_metadata(string meta_buff)
    {
        return split_metadata(meta_buff, FDFS_RECORD_SEPERATOR, FDFS_FIELD_SEPERATOR);
    }

    /// <summary>
    /// split metadata to name value pair array
    /// </summary>
    /// <param name="meta_buff">metadata</param>
    /// <param name="recordSeperator">record/row seperator</param>
    /// <param name="filedSeperator">field/column seperator</param>
    /// <returns> name value pair array</returns>
    public static NameValuePair[] split_metadata(string meta_buff,
    char recordSeperator, char filedSeperator)
    {
        string[] rows;
        string[] cols;
        NameValuePair[] meta_list;
        rows = meta_buff.Split(recordSeperator);
        meta_list = new NameValuePair[rows.Length];
        for (int i = 0; i < rows.Length; i++)
        {
            cols = rows[i].Split(filedSeperator + "", 2);
            meta_list[i] = new NameValuePair(cols[0]);
            if (cols.Length == 2)
            {
                meta_list[i].setValue(cols[1]);
            }
        }
        return meta_list;
    }

    /// <summary>
    /// pack metadata array to string
    /// </summary>
    /// <param name="meta_list">metadata array</param>
    /// <returns> packed metadata</returns>
    public static string pack_metadata(NameValuePair[] meta_list)
    {
        if (meta_list.Length == 0)
        {
            return "";
        }
        StringBuilder sb = new StringBuilder(32 * meta_list.Length);
        sb.Append(meta_list[0].getName()).Append(FDFS_FIELD_SEPERATOR).Append(meta_list[0].getValue());
        for (int i = 1; i < meta_list.Length; i++)
        {
            sb.Append(FDFS_RECORD_SEPERATOR);
            sb.Append(meta_list[i].getName()).Append(FDFS_FIELD_SEPERATOR).Append(meta_list[i].getValue());
        }
        return sb.ToString();
    }

    /// <summary>
    /// send quit command to server and close socket
    /// </summary>
    /// <param name="sock">the JavaSocket object</param>
    public static void closeSocket(JavaSocket sock)
    {
        byte[] header;
        header = packHeader(FDFS_PROTO_CMD_QUIT, 0, (byte)0);
        sock.GetStream().Write(header, 0, header.Length);
        sock.Close();
    }

    /// <summary>
    /// send ACTIVE_TEST command to server, test if network is ok and the server is alive
    /// </summary>
    /// <param name="sock">the JavaSocket object</param>
    public static bool activeTest(JavaSocket sock)
    {
        byte[] header;
        header = packHeader(FDFS_PROTO_CMD_ACTIVE_TEST, 0, 0);
        sock.GetStream().Write(header, 0, header.Length);
        RecvHeaderInfo headerInfo = recvHeader(sock.GetStream(), TRACKER_PROTO_CMD_RESP, 0);
        return headerInfo.errno == 0 ? true : false;
    }

    /// <summary>
    /// long convert to buff (big-endian)
    /// </summary>
    /// <param name="n">long number</param>
    /// <returns> 8 bytes buff</returns>
    public static byte[] long2buff(long n)
    {
        byte[] bs;
        bs = new byte[8];
        bs[0] = (byte)((n >> 56) & 0xFF);
        bs[1] = (byte)((n >> 48) & 0xFF);
        bs[2] = (byte)((n >> 40) & 0xFF);
        bs[3] = (byte)((n >> 32) & 0xFF);
        bs[4] = (byte)((n >> 24) & 0xFF);
        bs[5] = (byte)((n >> 16) & 0xFF);
        bs[6] = (byte)((n >> 8) & 0xFF);
        bs[7] = (byte)(n & 0xFF);
        return bs;
    }

    /// <summary>
    /// buff convert to long
    /// </summary>
    /// <param name="bs">the buffer (big-endian)</param>
    /// <param name="offset">the start position based 0</param>
    /// <returns> long number</returns>
    public static long buff2long(byte[] bs, int offset)
    {
        return (((long)(bs[offset] >= 0 ? bs[offset] : 256 + bs[offset])) << 56) |
        (((long)(bs[offset + 1] >= 0 ? bs[offset + 1] : 256 + bs[offset + 1])) << 48) |
        (((long)(bs[offset + 2] >= 0 ? bs[offset + 2] : 256 + bs[offset + 2])) << 40) |
        (((long)(bs[offset + 3] >= 0 ? bs[offset + 3] : 256 + bs[offset + 3])) << 32) |
        (((long)(bs[offset + 4] >= 0 ? bs[offset + 4] : 256 + bs[offset + 4])) << 24) |
        (((long)(bs[offset + 5] >= 0 ? bs[offset + 5] : 256 + bs[offset + 5])) << 16) |
        (((long)(bs[offset + 6] >= 0 ? bs[offset + 6] : 256 + bs[offset + 6])) << 8) |
        ((long)(bs[offset + 7] >= 0 ? bs[offset + 7] : 256 + bs[offset + 7]));
    }

    /// <summary>
    /// buff convert to int
    /// </summary>
    /// <param name="bs">the buffer (big-endian)</param>
    /// <param name="offset">the start position based 0</param>
    /// <returns> int number</returns>
    public static int buff2int(byte[] bs, int offset)
    {
        return (((int)(bs[offset] >= 0 ? bs[offset] : 256 + bs[offset])) << 24) |
        (((int)(bs[offset + 1] >= 0 ? bs[offset + 1] : 256 + bs[offset + 1])) << 16) |
        (((int)(bs[offset + 2] >= 0 ? bs[offset + 2] : 256 + bs[offset + 2])) << 8) |
        ((int)(bs[offset + 3] >= 0 ? bs[offset + 3] : 256 + bs[offset + 3]));
    }

    /// <summary>
    /// buff convert to ip address
    /// </summary>
    /// <param name="bs">the buffer (big-endian)</param>
    /// <param name="offset">the start position based 0</param>
    /// <returns> ip address</returns>
    public static string getIpAddress(byte[] bs, int offset)
    {
        if (bs[0] == 0 || bs[3] == 0) //storage server ID
        {
            return "";
        }
        int n;
        StringBuilder sbResult = new StringBuilder(16);
        for (int i = offset; i < offset + 4; i++)
        {
            n = (bs[i] >= 0) ? bs[i] : 256 + bs[i];
            if (sbResult.Length > 0)
            {
                sbResult.Append(".");
            }
            sbResult.Append(n);
        }
        return sbResult.ToString();
    }

    /// <summary>
    /// md5 function
    /// </summary>
    /// <param name="source">the input buffer</param>
    /// <returns> md5 string</returns>
    public static string md5(byte[] source)
    {
        char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        MD5 md5 = MD5.Create();
        byte[] tmp = md5.ComputeHash(source);
        char[] str = new char[32];
        int k = 0;
        for (int i = 0; i < 16; i++)
        {
            str[k++] = hexDigits[Bytes.RightMove(tmp[i], 4) & 0xf];
            str[k++] = hexDigits[tmp[i] & 0xf];
        }
        return new string(str);
    }

    /// <summary>
    /// get token for file URL
    /// </summary>
    /// <param name="remote_filename">the filename return by FastDFS server</param>
    /// <param name="ts">unix timestamp, unit: second</param>
    /// <param name="secret_key">the secret key</param>
    /// <returns> token string</returns>
    public static string getToken(string remote_filename, int ts, string secret_key)
    {
        var encoding = ClientGlobal.g_charset;
        byte[] bsFilename = encoding.GetBytes(remote_filename);
        byte[] bsKey = encoding.GetBytes(secret_key);
        byte[] bsTimestamp = encoding.GetBytes(ts.ToString());
        byte[] buff = new byte[bsFilename.Length + bsKey.Length + bsTimestamp.Length];
        Array.Copy(bsFilename, 0, buff, 0, bsFilename.Length);
        Array.Copy(bsKey, 0, buff, bsFilename.Length, bsKey.Length);
        Array.Copy(bsTimestamp, 0, buff, bsFilename.Length + bsKey.Length, bsTimestamp.Length);
        return md5(buff);
    }

    /// <summary>
    /// generate slave filename
    /// </summary>
    /// <param name="master_filename">the master filename to generate the slave filename</param>
    /// <param name="prefix_name">the prefix name to generate the slave filename</param>
    /// <param name="ext_name">the extension name of slave filename, null for same as the master extension name</param>
    /// <returns> slave filename string</returns>
    public static string genSlaveFilename(string master_filename, string prefix_name, string ext_name)
    {
        string true_ext_name;
        int dotIndex;
        if (master_filename.Length < 28 + FDFS_FILE_EXT_NAME_MAX_LEN)
        {
            throw new MyException("master filename \"" + master_filename + "\" is invalid");
        }
        dotIndex = master_filename.IndexOf('.', master_filename.Length - (FDFS_FILE_EXT_NAME_MAX_LEN + 1));
        if (ext_name != null)
        {
            if (ext_name.Length == 0)
            {
                true_ext_name = "";
            }
            else if (ext_name[0] == '.')
            {
                true_ext_name = ext_name;
            }
            else
            {
                true_ext_name = "." + ext_name;
            }
        }
        else
        {
            if (dotIndex < 0)
            {
                true_ext_name = "";
            }
            else
            {
                true_ext_name = master_filename.Substring(dotIndex);
            }
        }
        if (true_ext_name.Length == 0 && prefix_name == "-m")
        {
            throw new MyException("prefix_name \"" + prefix_name + "\" is invalid");
        }
        if (dotIndex < 0)
        {
            return master_filename + prefix_name + true_ext_name;
        }
        else
        {
            return master_filename.Substring(0, dotIndex) + prefix_name + true_ext_name;
        }
    }

    /// <summary>
    /// receive package info
    /// </summary>
    public class RecvPackageInfo
    {
        public byte errno;
        public byte[] body;
        public RecvPackageInfo(byte errno, byte[] body)
        {
            this.errno = errno;
            this.body = body;
        }
    }

    /// <summary>
    /// receive header info
    /// </summary>
    public class RecvHeaderInfo
    {
        public byte errno;
        public long body_len;
        public RecvHeaderInfo(byte errno, long body_len)
        {
            this.errno = errno;
            this.body_len = body_len;
        }
    }
}
