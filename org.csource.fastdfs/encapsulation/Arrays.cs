using System;
using System.Collections.Generic;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    /// <summary>
    ///
    /// </summary>
    public class Arrays
    {
        public static void fill<T>(T[] charToPad, T chPad)
        {
            for (int i = 0; i < charToPad.Length; i++)
            {
                charToPad[i] = chPad;
            }
        }
    }
}
