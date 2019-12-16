using System;
using System.IO;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQing
/// <p>
/// FastDFS Java Client may be copied only under the terms of the GNU Lesser
/// General Public License (LGPL).
/// Please visit the FastDFS Home Page https://github.com/happyfish100/fastdfs for more detail.
/// </summary>
namespace org.csource.fastdfs
{
    /// <summary>
    /// C struct body decoder
    /// 
    /// @author Happy Fish / YuQing
    /// @version Version 1.17
    /// </summary>
    public class ProtoStructDecoder<T> where T : StructBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ProtoStructDecoder()
        {
        }

        /// <summary>
        /// decode byte buffer
        /// </summary>
        public T[] decode(byte[] bs, int fieldsTotalSize)
        {
            if (bs.Length % fieldsTotalSize != 0)
            {
                throw new IOException("byte array length: " + bs.Length + " is invalid!");
            }
            int count = bs.Length / fieldsTotalSize;
            int offset;
            T[] results = new T[count];
            offset = 0;
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = Activator.CreateInstance<T>();
                results[i].setFields(bs, offset);
                offset += fieldsTotalSize;
            }
            return results;
        }
    }
}
