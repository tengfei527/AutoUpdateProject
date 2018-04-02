using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common.Utility
{
    /// <summary>
    /*
    读注册表：
    string portName = RegistryHelper.GetRegistryData(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName");
    写注册表：        
    RegistryHelper.SetRegistryData(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName", portName);
    */
    /// </summary>
    public class RegistryHelper
    {
        /// <summary>
        /// 获取键下所有子节点
        /// </summary>
        /// <param name="root">主键</param>
        /// <param name="subkey">节点</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRegistrySubs(RegistryKey root, string subkey)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            // 打开子路径
            RegistryKey myKey = root.OpenSubKey(subkey, false);

            if (myKey != null)
            {
                String[] keyNameArray = myKey.GetValueNames();
                foreach (string keyName in keyNameArray)
                {
                    // 读取键值
                    string keyValue = (string)myKey.GetValue(keyName);
                    if (dic.ContainsKey(keyName))
                        dic[keyName] = keyValue;
                    else
                        dic.Add(keyName, keyValue);
                }
            }

            return dic;
        }
        /// <summary>
        /// 读取指定名称的注册表的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRegistryData(RegistryKey root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey != null)
            {
                var v = myKey.GetValue(name);
                if (v != null)
                    registData = v.ToString();
            }

            return registData;
        }

        /// <summary>
        /// 向注册表中写数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param> 
        public static void SetRegistryData(RegistryKey root, string subkey, string name, string value)
        {
            try
            {
                RegistryKey aimdir = root.CreateSubKey(subkey);
                aimdir.SetValue(name, value);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteRegist(RegistryKey root, string subkey, string name)
        {
            try
            {
                string[] subkeyNames;
                RegistryKey myKey = root.OpenSubKey(subkey, true);
                subkeyNames = myKey.GetSubKeyNames();
                foreach (string aimKey in subkeyNames)
                {
                    if (aimKey == name)
                        myKey.DeleteSubKeyTree(name);
                }
            }

            catch { }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsRegistryExist(RegistryKey root, string subkey, string name)
        {
            bool _exit = false;
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }

            return _exit;
        }
    }
}
