using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace AU.Common.Utility
{
    public static class ConfigUtility
    {
        /// <summary>
        /// Gets the connection string that matches connectionStringName from the specified web.config.
        /// </summary>
        /// <param name="webConfigPath">Path to the web.config</param>
        /// <param name="connectionStringName">Name attribute of the connectionString in the web.config</param>
        /// <returns></returns>
        public static string GetConnectionStringFromWebConfig(string webConfigPath, string connectionStringName)
        {
            try
            {
                var webConfigContents = File.ReadAllText(webConfigPath);
                using (XmlReader reader = XmlReader.Create(new StringReader(webConfigContents)))
                {
                    reader.ReadToFollowing("connectionStrings");
                    using (XmlReader rdr = reader.ReadSubtree())
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                reader.MoveToAttribute("name");
                                if (reader.Value.ToLower() == connectionStringName.ToLower())
                                {
                                    reader.MoveToAttribute("connectionString");
                                    return reader.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return string.Empty;
        }
        public static string GetApiDbConnect(string webConfigPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(webConfigPath);
                XmlNode node = doc.SelectSingleNode(@"//add[@key='IsCiphertext']");
                bool IsCiphertext = "true".Equals(((XmlElement)node).GetAttribute("value"));
                node = doc.SelectSingleNode(@"//add[@key='DbHelperConnectionString']");
                string DbHelperConnectionString = ((XmlElement)node).GetAttribute("value");
                if (IsCiphertext)
                {
                    DbHelperConnectionString = CryptoHelp.DesDecrypt(DbHelperConnectionString, "ftf");
                }

                return DbHelperConnectionString;
            }
            catch { return ""; }
        }


        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAddValueFromWebConfig(string webConfigPath, string name)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(webConfigPath);
                XmlNode node = doc.SelectSingleNode(@"//add[@key='" + name + "']");
                XmlElement ele = (XmlElement)node;
                return ele.GetAttribute("value");
            }
            catch { return ""; }
        }
        /// <summary>
        /// 更新配置文件信息
        /// </summary>
        /// <param name="name">配置文件字段名称</param>
        /// <param name="Xvalue">值</param>
        public static void UpdateAddValueFromWebConfig(string webConfigPath, string name, string Xvalue)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(webConfigPath);
            XmlNode node = doc.SelectSingleNode(@"//add[@key='" + name + "']");
            XmlElement ele = (XmlElement)node;
            ele.SetAttribute("value", Xvalue);
            doc.Save(webConfigPath);
        }
    }
}
