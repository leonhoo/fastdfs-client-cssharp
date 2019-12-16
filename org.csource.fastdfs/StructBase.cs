using org.csource.fastdfs.encapsulation;
using System;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// C struct body decoder
    /// </summary>
    public abstract class StructBase
    {

        /// <summary>
        /// set fields
        /// </summary>
        /// <param name="bs">byte array</param>
        /// <param name="offset">start offset</param>
        public abstract void setFields(byte[] bs, int offset);
        protected string stringValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            try
            {
                return (Strings.Get(bs, offset + filedInfo.offset, filedInfo.size)).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected long longValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return ProtoCommon.buff2long(bs, offset + filedInfo.offset);
        }
        protected int intValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return (int)ProtoCommon.buff2long(bs, offset + filedInfo.offset);
        }
        protected int int32Value(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return ProtoCommon.buff2int(bs, offset + filedInfo.offset);
        }
        protected byte byteValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return bs[offset + filedInfo.offset];
        }
        protected bool booleanValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return bs[offset + filedInfo.offset] != 0;
        }
        protected DateTime dateValue(byte[] bs, int offset, FieldInfo filedInfo)
        {
            return Dates.Get(ProtoCommon.buff2long(bs, offset + filedInfo.offset) * 1000);
        }
        protected class FieldInfo
        {
            public string name;
            public int offset;
            public int size;
            public FieldInfo(string name, int offset, int size)
            {
                this.name = name;
                this.offset = offset;
                this.size = size;
            }
        }
    }
}
