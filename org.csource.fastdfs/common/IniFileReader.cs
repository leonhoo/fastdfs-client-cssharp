using org.csource.fastdfs.encapsulation;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Copyright (C) 2008 Happy Fish / YuQingFastDFS Java Client may be copied only under the terms of the GNU LesserGeneral Public License (LGPL).Please visit the FastDFS Home Page http://www.csource.org/ for more detail./namespace org.csource.fastdfs.common{ini file reader / parser
/// </summary>
public class IniFileReader
{
    private Dictionary<string, object> paramTable;
    private string conf_filename;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conf_filename">config filename</param>
    public IniFileReader(string conf_filename)
    {
        this.conf_filename = conf_filename;
        loadFromFile(conf_filename);
    }
    public static Stream loadFromOsFileSystemOrClasspathAsStream(string filePath)
    {
        Stream stream = null;
        try
        {
            // 优先从文件系统路径加载
            if (File.Exists(filePath))
            {
                stream = new FileStream(filePath, FileMode.Open);
                //Console.WriteLine("loadFrom...file path done");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return stream;
    }

    /// <summary>
    /// get the config filename
    /// </summary>
    /// <returns> config filename</returns>
    public string getConfFilename()
    {
        return this.conf_filename;
    }

    /// <summary>
    /// get string value from config file
    /// </summary>
    /// <param name="name">item name in config file</param>
    /// <returns> string value</returns>
    public string getStrValue(string name)
    {
        this.paramTable.TryGetValue(name, out object obj);
        if (obj == null)
        {
            return null;
        }
        if (obj is string)
        {
            return (string)obj;
        }
        return (string)((List<string>)obj)[0];
    }

    /// <summary>
    /// get int value from config file
    /// </summary>
    /// <param name="name">item name in config file</param>
    /// <param name="default_value">the default value</param>
    /// <returns> int value</returns>
    public int getIntValue(string name, int default_value)
    {
        string szValue = this.getStrValue(name);
        if (szValue == null)
        {
            return default_value;
        }
        return int.Parse(szValue);
    }

    /// <summary>
    /// get bool value from config file
    /// </summary>
    /// <param name="name">item name in config file</param>
    /// <param name="default_value">the default value</param>
    /// <returns> bool value</returns>
    public bool getBoolValue(string name, bool default_value)
    {
        string szValue = this.getStrValue(name)?.ToLower();
        if (szValue == null)
        {
            return default_value;
        }
        return szValue == "yes" || szValue == "on" || szValue == "true" || szValue == "1";
    }

    /// <summary>
    /// get all values from config file
    /// </summary>
    /// <param name="name">item name in config file</param>
    /// <returns> string values (array)</returns>
    public string[] getValues(string name)
    {
        string[] values;
        this.paramTable.TryGetValue(name, out object obj);
        if (obj == null)
        {
            return null;
        }
        if (obj is string)
        {
            values = new string[1];
            values[0] = (string)obj;
            return values;
        }
        object[] objs = ((List<string>)obj).ToArray();
        values = new string[objs.Length];
        Array.Copy(objs, 0, values, 0, objs.Length);
        return values;
    }
    private void loadFromFile(string confFilePath)
    {
        using (Stream stream = loadFromOsFileSystemOrClasspathAsStream(confFilePath))
        {
            readToParamTable(stream);
        }
    }
    private void readToParamTable(Stream stream)
    {
        this.paramTable = new Dictionary<string, object>();
        if (stream == null) return;
        string line;
        string[] parts;
        string name;
        string value;
        object obj;
        List<string> valueList;
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
                else if (obj is string)
                {
                    valueList = new List<string>();
                    valueList.Add(obj + "");
                    valueList.Add(value);
                    this.paramTable.Add(name, valueList);
                }
                else
                {
                    valueList = (List<string>)obj;
                    valueList.Add(value);
                }
            }
        }
    }
}
