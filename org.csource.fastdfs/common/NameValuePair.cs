/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQing
/// FastDFS Java Client may be copied only under the terms of the GNU Lesser
/// General Public License(LGPL).
/// Please visit the FastDFS Home Page https://github.com/happyfish100/fastdfs for more detail.
/// </summary>
namespace org.csource.fastdfs.common
{

    /// <summary>
    /// name(key) and value pair model
    /// 
    /// @author Happy Fish / YuQing
    /// @version Version 1.0
    /// </summary>
    public class NameValuePair
    {
        protected string name;
        protected string value;
        public NameValuePair()
        {
        }
        public NameValuePair(string name)
        {
            this.name = name;
        }
        public NameValuePair(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public string getName()
        {
            return this.name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getValue()
        {
            return this.value;
        }
        public void setValue(string value)
        {
            this.value = value;
        }
    }
}
