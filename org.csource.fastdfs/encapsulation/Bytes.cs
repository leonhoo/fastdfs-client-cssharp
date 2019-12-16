using System;
using System.Collections.Generic;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    /// <summary>
    ///
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        /// 无符号右移, 相当于java里的 value>>>pos
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int RightMove(this int value, int pos)
        {
            //移动 0 位时直接返回原值
            if (pos != 0)
            {
                // int.MaxValue = 0x7FFFFFFF 整数最大值
                int mask = int.MaxValue;
                //无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
                value = value >> 1;
                //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
                value = value & mask;
                //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
                value = value >> pos - 1;
            }
            return value;
        }
    }
}
