using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace org.csource.fastdfs.encapsulation
{
    public class Properties
    {
        Dictionary<string, string> paramTable = null;
        public void load(Stream stream)
        {
            this.paramTable = new Dictionary<string, string>();
            if (stream == null) return;
            string line;
            string[] parts;
            string name;
            string value;
            string obj;
            using (var inReader = new StreamReader(stream))
            {
                while ((line = inReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0 || line[0] == '#')
                    {
                        continue;
                    }
                    parts = line.Split("=", 2);
                    if (parts.Length != 2)
                    {
                        continue;
                    }
                    name = parts[0].Trim();
                    value = parts[1].Trim();
                    this.paramTable.TryGetValue(name, out obj);
                    if (obj == null)
                    {
                        this.paramTable.Add(name, value);
                    }
                    else
                    {
                        this.paramTable[name] = value;
                    }
                }
            }
        }
        public string getProperty(string key)
        {
            if (paramTable.TryGetValue(key, out string obj))
            {
                return obj;
            }
            return null;
        }
    }
}
