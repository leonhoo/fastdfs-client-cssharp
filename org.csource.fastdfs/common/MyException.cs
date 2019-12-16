/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQing
/// FastDFS Java Client may be copied only under the terms of the GNU Lesse
/// General Public License(LGPL).
/// Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
using System;
namespace org.csource.fastdfs.common
{

    /// <summary>
    /// My Exception
    /// </summary>
    public class MyException : Exception
    {
        public MyException()
        {
        }
        public MyException(string message) : base(message)
        {
        }
    }
}
