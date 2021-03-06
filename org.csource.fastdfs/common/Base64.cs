using org.csource.fastdfs.encapsulation;
using System;
using System.Text;

/// <summary>
/// Freeware from:
/// Roedy Green
/// Canadian Mind Products
/// #327 - 964 Heywood Avenue
/// Victoria, BC Canada V8V 2Y5
/// tel:(250) 361-9093
/// mailto:roedy@mindprod.com
/// </summary>
namespace org.csource.fastdfs.common
{

    /// <summary>
    ///Encode arbitrary binary into printable ASCII using BASE64 encoding.
    ///very loosely based on the Base64 Reader by
    ///Dr. Mark Thornton
    ///Optrak Distribution Software Ltd.
    ///http://www.optrak.co.uk
    ///and Kevin Kelley's  http://www.ruralnet.net/~kelley/java/Base64.java
    ///<p>
    ///Base64 is a way of encoding 8-bit characters using only ASCII printable
    ///characters similar to UUENCODE.  UUENCODE includes a filename where BASE64 does not.
    ///The spec is described in RFC 2045.  Base64 is a scheme where
    ///3 bytes are concatenated, then split to form 4 groups of 6-bits each; and
    ///each 6-bits gets translated to an encoded printable ASCII character, via a
    ///table lookup.  An encoded string is therefore longer than the original by
    ///about 1/3.  The "=" character is used to pad the end.  Base64 is used,
    ///among other things, to encode the user:password string in an
    ///Authorization: header for HTTP.  Don't confuse Base64 with
    ///x-www-form-urlencoded which is handled by
    ///Java.net.URLEncoder.encode/decode
    ///If you don't like this code, there is another implementation at http://www.ruffboy.com/download.htm
    ///Sun has an undocumented method called sun.misc.Base64Encoder.encode.
    ///You could use hex, simpler to code, but not as compact.
    ///<p>
    ///If you wanted to encode a giant file, you could do it in large chunks that
    ///are even multiples of 3 bytes, except for the last chunk, and append the outputs.
    ///<p>
    ///To encode a string, rather than binary data java.net.URLEncoder may be better. See
    ///printable characters in the Java glossary for a discussion of the differences.
    ///<p>
    ///version 1.4 2002 February 15  -- correct bugs with uneven line lengths,
    ///allow you to configure line separator.
    ///now need Base64 object and instance methods.
    ///new mailing address.
    ///version 1.3 2000 September 12 -- fix problems with estimating output length in encode
    ///version 1.2 2000 September 09 -- now handles decode as well.
    ///version 1.1 1999 December 04 -- more symmetrical encoding algorithm.
    ///more accurate StringBuffer allocation size.
    ///version 1.0 1999 December 03 -- posted in comp.lang.java.programmer.
    ///Futures Streams or files.
    /// </summary>
    public class Base64
    {

        /// <summary>
        /// Marker value for chars we just ignore, e.g. \n \r high ascii
        /// </summary>
        const int IGNORE = -1;

        /// <summary>
        /// Marker for = trailing pad
        /// </summary>
        const int PAD = -2;

        /// <summary>
        /// used to disable test driver
        /// </summary>
        private const bool debug = true;

        /// <summary>
        /// how we separate lines, e.g. \n, \r\n, \r etc.
        /// </summary> 
        private string lineSeparator = Environment.NewLine;

        /// <summary>
        /// max chars per line, excluding lineSeparator.  A multiple of 4.
        /// </summary>
        private int lineLength = 72;
        private char[] valueToChar = new char[64];

        /// <summary>
        /// binary value encoded by a given letter of the alphabet 0..63
        /// </summary>
        private int[] charToValue = new int[256];
        private int[] charToPad = new int[4];
        /* constructor */
        public Base64()
        {
            this.init('+', '/', '=');
        }
        /* constructor */
        public Base64(char chPlus, char chSplash, char chPad, int lineLength)
        {
            this.init(chPlus, chSplash, chPad);
            this.lineLength = lineLength;
        }
        public Base64(int lineLength)
        {
            this.lineLength = lineLength;
        }

        /// <summary>
        /// debug display array
        /// </summary>
        public static void show(byte[] b)
        {
            int count = 0;
            //int rows = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (count == 8)
                {
                    Console.Write("  ");
                }
                else if (count == 16)
                {
                    Console.WriteLine("");
                    count = 0;
                    continue;
                }
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// debug display array
        /// </summary>
        public static void display(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write((char)b[i]);
            }
            Console.WriteLine();
        }

        /* initialise defaultValueToChar and defaultCharToValue tables */
        private void init(char chPlus, char chSplash, char chPad)
        {
            int index = 0;
            // build translate this.valueToChar table only once.
            // 0..25 -> 'A'..'Z'
            for (int i = 'A'; i <= 'Z'; i++)
            {
                this.valueToChar[index++] = (char)i;
            }
            // 26..51 -> 'a'..'z'
            for (int i = 'a'; i <= 'z'; i++)
            {
                this.valueToChar[index++] = (char)i;
            }
            // 52..61 -> '0'..'9'
            for (int i = '0'; i <= '9'; i++)
            {
                this.valueToChar[index++] = (char)i;
            }
            this.valueToChar[index++] = chPlus;
            this.valueToChar[index++] = chSplash;
            // build translate defaultCharToValue table only once.
            for (int i = 0; i < 256; i++)
            {
                this.charToValue[i] = IGNORE;  // default is to ignore
            }
            for (int i = 0; i < 64; i++)
            {
                this.charToValue[this.valueToChar[i]] = i;
            }
            this.charToValue[chPad] = PAD;
            Arrays.fill(this.charToPad, chPad);
        }

        /// <summary>
        /// Encode an arbitrary array of bytes as Base64 printable ASCII.
        /// It will be broken into lines of 72 chars each.The last line is not
        /// terminated with a line separator.
        /// The output will always have an even multiple of data characters,
        /// exclusive of \n.It is padded out with =.
        /// </summary>
        public string encode(byte[] b)
        {
            // Each group or partial group of 3 bytes becomes four chars
            // covered quotient
            int outputLength = ((b.Length + 2) / 3) * 4;
            // account for trailing newlines, on all but the very last line
            if (lineLength != 0)
            {
                int lines = (outputLength + lineLength - 1) / lineLength - 1;
                if (lines > 0)
                {
                    outputLength += lines * lineSeparator.Length;
                }
            }
            // must be local for recursion to work.
            StringBuilder sb = new StringBuilder(outputLength);
            // must be local for recursion to work.
            int linePos = 0;
            // first deal with even multiples of 3 bytes.
            int len = (b.Length / 3) * 3;
            int leftover = b.Length - len;
            for (int i = 0; i < len; i += 3)
            {
                // Start a new line if next 4 chars won't fit on the current line
                // We can't encapsulete the following code since the variable need to
                // be local to this incarnation of encode.
                linePos += 4;
                if (linePos > lineLength)
                {
                    if (lineLength != 0)
                    {
                        sb.Append(lineSeparator);
                    }
                    linePos = 4;
                }
                // get next three bytes in unsigned form lined up,
                // in big-endian order
                int combined = b[i + 0] & 0xff;
                combined <<= 8;
                combined |= b[i + 1] & 0xff;
                combined <<= 8;
                combined |= b[i + 2] & 0xff;
                // break those 24 bits into a 4 groups of 6 bits,
                // working LSB to MSB.
                int c3 = combined & 0x3f;
                //combined >>>= 6;
                combined = Bytes.RightMove(combined, 6);
                int c2 = combined & 0x3f;
                //combined >>>= 6;
                combined = Bytes.RightMove(combined, 6);
                int c1 = combined & 0x3f;
                //combined >>>= 6;
                combined = Bytes.RightMove(combined, 6);
                int c0 = combined & 0x3f;
                // Translate into the equivalent alpha character
                // emitting them in big-endian order.
                sb.Append(valueToChar[c0]);
                sb.Append(valueToChar[c1]);
                sb.Append(valueToChar[c2]);
                sb.Append(valueToChar[c3]);
            }
            // deal with leftover bytes
            switch (leftover)
            {
                case 0:
                default:
                    // nothing to do
                    break;
                case 1:
                    // One leftover byte generates xx==
                    // Start a new line if next 4 chars won't fit on the current line
                    linePos += 4;
                    if (linePos > lineLength)
                    {
                        if (lineLength != 0)
                        {
                            sb.Append(lineSeparator);
                        }
                        linePos = 4;
                    }
                    // Handle this recursively with a faked complete triple.
                    // Throw away last two chars and replace with ==
                    sb.Append(encode(new byte[] { b[len], 0, 0 }
                    ).Substring(0, 2));
                    sb.Append("==");
                    break;
                case 2:
                    // Two leftover bytes generates xxx=
                    // Start a new line if next 4 chars won't fit on the current line
                    linePos += 4;
                    if (linePos > lineLength)
                    {
                        if (lineLength != 0)
                        {
                            sb.Append(lineSeparator);
                        }
                        linePos = 4;
                    }
                    // Handle this recursively with a faked complete triple.
                    // Throw away last char and replace with =
                    sb.Append(encode(new byte[] { b[len], b[len + 1], 0 }
                    ).Substring(0, 3));
                    sb.Append("=");
                    break;
            } // end switch;
            if (outputLength != sb.Length)
            {
                Console.WriteLine("oops: minor program flaw: output length mis-estimated");
                Console.WriteLine("estimate:" + outputLength);
                Console.WriteLine("actual:" + sb.Length);
            }
            return sb.ToString();
        }// end encode

        /// <summary>
        /// decode a well-formed complete Base64 string back into an array of bytes.It must have an even multiple of 4 data characters (not counting \n),padded out with = as needed.
        /// </summary>
        public byte[] decodeAuto(string s)
        {
            int nRemain = s.Length % 4;
            if (nRemain == 0)
            {
                return this.decode(s);
            }
            else
            {
                // java 语法
                // new string(char[] source, int index, int length)
                // 从数组source里面index下标开始，拿length个元素出来
                //return this.decode(s + new string(this.charToPad, 0, 4 - nRemain));
                return this.decode(s + Strings.Get(this.charToPad, 0, 4 - nRemain));
            }
        }

        /// <summary>
        /// decode a well-formed complete Base64 string back into an array of bytes.It must have an even multiple of 4 data characters (not counting \n),padded out with = as needed.
        /// </summary>
        public byte[] decode(string s)
        {
            // estimate worst case size of output array, no embedded newlines.
            byte[] b = new byte[(s.Length / 4) * 3];
            // tracks where we are in a cycle of 4 input chars.
            int cycle = 0;
            // where we combine 4 groups of 6 bits and take apart as 3 groups of 8.
            int combined = 0;
            // how many bytes we have prepared.
            int j = 0;
            // will be an even multiple of 4 chars, plus some embedded \n
            int len = s.Length;
            int dummies = 0;
            for (int i = 0; i < len; i++)
            {
                int c = s[i];
                int value = (c <= 255) ? charToValue[c] : IGNORE; 
                if (value == PAD)
                {
                    value = 0;
                    dummies++;
                }
                if (value != IGNORE)
                {
                    // fallthrough
                    /* regular value character */
                    switch (cycle)
                    {
                        case 0:
                            combined = value;
                            cycle = 1;
                            break;
                        case 1:
                            combined <<= 6;
                            combined |= value;
                            cycle = 2;
                            break;
                        case 2:
                            combined <<= 6;
                            combined |= value;
                            cycle = 3;
                            break;
                        case 3:
                            combined <<= 6;
                            combined |= value;
                            // we have just completed a cycle of 4 chars.
                            // the four 6-bit values are in combined in big-endian order
                            // peel them off 8 bits at a time working lsb to msb
                            // to get our original 3 8-bit bytes back
                            b[j + 2] = (byte)combined; 
                            combined = Bytes.RightMove(combined, 8);
                            b[j + 1] = (byte)combined; 
                            combined = Bytes.RightMove(combined, 8);
                            b[j] = (byte)combined;
                            j += 3;
                            cycle = 0;
                            break;
                    }
                }
            } // end for
            if (cycle != 0)
            {
                throw new Exception("Input to decode not an even multiple of 4 characters; pad with =.");
            }
            j -= dummies;
            if (b.Length != j)
            {
                byte[] b2 = new byte[j]; 
                Array.Copy(b, 0, b2, 0, j);
                b = b2;
            }
            return b;
        }// end decode

        /// <summary>
        /// determines how long the lines are that are generated by encode.Ignored by decode.
        /// </summary>
        /// <param name="length">0 means no newlines inserted. Must be a multiple of 4.</param>
        public void setLineLength(int length)
        {
            this.lineLength = (length / 4) * 4;
        }

        /// <summary>
        /// How lines are separated.Ignored by decode.
        /// </summary>
        /// <param name="lineSeparator">may be "" but not null.Usually contains only a combination of chars \n and \r.Could be any chars not in set A-Z a-z 0-9 + /.</param>
        public void setLineSeparator(string lineSeparator)
        {
            this.lineSeparator = lineSeparator;
        }
    } // end Base64
}
