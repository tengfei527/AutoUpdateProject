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
            catch {}

            return string.Empty;
        }
    }
}
