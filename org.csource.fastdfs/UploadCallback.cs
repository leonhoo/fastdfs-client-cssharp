using System.IO;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// upload file callback interface
    /// </summary>
    public interface UploadCallback
    {

        /// <summary>
        /// send file content callback function, be called only once when the file uploaded
        /// </summary>
        /// <param name="out">output stream for writing file content</param>
        /// <returns> 0 success, return none zero(errno) if fail</returns>
        int send(Stream outStream);
    }
}
