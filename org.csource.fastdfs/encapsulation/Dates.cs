using System;
using System.Collections.Generic;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    public class Dates
    {
        public static DateTime Get(long v)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(v);
        }
    }
}
