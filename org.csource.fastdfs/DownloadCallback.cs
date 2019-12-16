
/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail.
/// </summary>
namespace org.csource.fastdfs
{

    /// <summary>
    /// Download file callback interface
    /// </summary>
    public interface DownloadCallback
    {

        /// <summary>
        /// recv file content callback function, may be called more than once when the file downloaded
        /// </summary>
        /// <param name="file_size">file size</param>
        /// <param name="data">data buff</param>
        /// <param name="bytes">data bytes</param>
        /// <returns> 0 success, return none zero(errno) if fail</returns>
        int recv(long file_size, byte[] data, int bytes);
    }
}
