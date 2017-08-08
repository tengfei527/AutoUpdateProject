using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Management;
using AU.Common.Codes;

namespace AU.Common.Utility
{
    /// <summary>
    ///文件传输管理类
    /// </summary>
    public class IO
    {
        /// <summary>
        /// 远程获取文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void DownloadFile(BaseCommunication sender, string fileName, string savepath)
        {
            FileCode fileMng = new FileCode(fileName);
            fileMng.Head = CodeHead.GET_FILE;
            fileMng.SavePath = savepath;
            if (sender != null)
                sender.SendCode(fileMng);
        }

        /// <summary>
        ///  发送文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void ReadyUploadFile(BaseCommunication sender, string fileName, string savepath)
        {
            FileCode fileMng = new FileCode(fileName);
            fileMng.Head = CodeHead.SEND_FILE;
            fileMng.SavePath = savepath;
            fileMng.readFile();
            if (sender != null)
                sender.SendCode(fileMng);
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="code"></param>
        public static void UploadFile(BaseCommunication sender, FileCode code)
        {
            code.Head = CodeHead.SEND_FILE;
            code.readFile();
            if (sender != null)
                sender.SendCode(code);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileMng"></param>
        public static void SaveFile(BaseCommunication sender, FileCode fileMng)
        {
            fileMng.SaveFile();
            if (sender != null)
            {
                BaseCode code = new BaseCode();
                code.Head = CodeHead.FILE_TRAN_END;
                sender.SendCode(code);
            }
        }

        /// <summary>
        /// 文件传输完成
        /// </summary>
        /// <param name="sender"></param>
        public static void EndTranFile(BaseCommunication sender)
        {
            BaseCode code = new BaseCode();
            code.Head = CodeHead.FILE_TRAN_END;
            sender.SendCode(code);
        }

        /// <summary>
        /// 获取文件的详细信息
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns></returns>
        public static string GetFileDetial(string file)
        {
            string type;
            string fileSize;
            string updatedTime;
            try
            {
                int index = file.LastIndexOf('.');
                type = file.Substring(index + 1);
            }
            catch
            {
                type = "未知";
            }
            try
            {
                fileSize = GetFileSize(file);
            }
            catch
            {
                fileSize = "未知";
            }
            try
            {
                updatedTime = File.GetLastWriteTime(file).ToString();
            }
            catch
            {
                updatedTime = "未知";
            }
            return "类型:" + type + "修改日期:" + updatedTime + "\t大小:" + fileSize;
        }

        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns></returns>
        public static string GetFileSize(string file)
        {
            double KB = 1024;
            double MB = 1024 * KB;
            double GB = 1024 * MB;
            long byteCount = GetFileLength(file);
            if (byteCount < KB)
                return byteCount + "B";
            else if (byteCount < MB)
                return Math.Round(byteCount / KB, 2) + "KB";
            else if (byteCount < GB)
                return Math.Round(byteCount / MB, 2) + "MB";
            else
                return Math.Round(byteCount / GB, 2) + "GB";
        }

        /// <summary>
        /// 获取文件长度(单位:字节)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static long GetFileLength(string file)
        {
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            long fileLength = fileStream.Length;
            fileStream.Close();
            return fileLength;
        }

        /// <summary>
        /// 打开根目录
        /// </summary>
        /// <param name="lView">显示结果的列表视图控件</param>
        /// <param name="imageHashtable">文件图标的键值(哈希表)</param>
        public static void OpenRoot(ListView lView, Hashtable imageHashtable)
        {
            DisksCode diskcode = IO.GetDisks();
            if (diskcode != null)
                ShowDisks(diskcode, lView, imageHashtable, false);
        }

        /// <summary>
        /// 显示主机的磁盘
        /// </summary>
        /// <param name="diskcode">磁盘指令</param>
        /// <param name="lView">显示结果的列表视图控件</param>
        /// <param name="imageHashtable">文件图标的键值(哈希表)</param>
        /// <param name="serverDisk">是否服务器的磁盘</param>
        public static void ShowDisks(DisksCode diskcode, ListView lView, Hashtable imageHashtable, bool serverDisk)
        {
            DiskStruct[] disk = diskcode.Disks;
            if (disk != null && disk.Length != 0)
            {
                lView.Items.Clear();
                ListViewItem[] dItems = new ListViewItem[disk.Length];
                string name;
                lView.Tag = "";
                for (int i = 0; i < disk.Length; i++)
                {
                    name = IO.DiskToString(disk[i].Name, serverDisk);
                    dItems[i] = new ListViewItem(name);
                    //文件夹图标
                    dItems[i].ImageKey = (string)imageHashtable["Disk"];
                    dItems[i].Tag = disk[i];
                    dItems[i].SubItems.Add(disk[i].Size);
                    lView.Items.Add(dItems[i]);
                }
            }
        }

        /// <summary>
        /// 打开指定路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="lView">显示结果的列表视图控件</param>
        /// <param name="imageHashtable">文件图标的键值(哈希表)</param>
        public static void OpenDirectory(string path, ListView lView, Hashtable imageHashtable)
        {
            if (path == "")
                OpenRoot(lView, imageHashtable);
            else
            {
                ExplorerCode explorer = new ExplorerCode();
                explorer.Enter(path);
                ShowHostDirectory(explorer, lView, imageHashtable);
            }
        }

        /// <summary>
        /// 显示主机上的文件
        /// </summary>
        /// <param name="explorer">主机指令</param>
        /// <param name="lView">显示结果的列表视图控件</param>
        /// <param name="imageHashtable">文件图标的键值(哈希表)</param>
        public static void ShowHostDirectory(ExplorerCode explorer, ListView lView, Hashtable imageHashtable)
        {
            DirectoryStruct[] directorys;
            FileStruct[] files;
            if (!explorer.Available)
            {
                MessageBox.Show("当前路径无法访问!");
                return;
            }
            lView.Items.Clear();
            directorys = explorer.Directorys;
            files = explorer.Files;

            lView.Tag = explorer.Path;

            //添加回退功能
            string parentPath = IO.GetParentPath(explorer.Path);
            DirectoryStruct lastDirectory = new DirectoryStruct(parentPath);
            ListViewItem lastItem = new ListViewItem("上级目录");
            lastItem.ImageKey = (string)imageHashtable["LastPath"];
            lastItem.Tag = lastDirectory;
            lView.Items.Add(lastItem);


            ListViewItem[] dItems = new ListViewItem[directorys.Length];
            string name;
            for (int i = 0; i < directorys.Length; i++)
            {
                name = IO.GetName(directorys[i].Name);
                if (name != "")
                {
                    dItems[i] = new ListViewItem(name);
                    //文件夹图标
                    dItems[i].ImageKey = (string)imageHashtable["Directory"];
                    dItems[i].Tag = directorys[i];
                    dItems[i].SubItems.Add(directorys[i].LastUpdateTime);
                    dItems[i].SubItems.Add("文件夹");
                    dItems[i].SubItems.Add(directorys[i].Size);
                    lView.Items.Add(dItems[i]);
                }
            }

            ListViewItem[] fItems = new ListViewItem[files.Length];
            string type;
            for (int i = 0; i < files.Length; i++)
            {
                name = IO.GetName(files[i].Name);
                if (name != "")
                {
                    fItems[i] = new ListViewItem(name);
                    //文件图标
                    type = IO.GetFileType(files[i].Name).ToLower();
                    if (imageHashtable.Contains(type))
                        fItems[i].ImageKey = (string)imageHashtable[type];
                    else
                        fItems[i].ImageKey = (string)imageHashtable["Unknown"];
                    fItems[i].Tag = files[i];

                    fItems[i].SubItems.Add(files[i].LastUpdateTime);
                    fItems[i].SubItems.Add(type);
                    fItems[i].SubItems.Add(files[i].Size);
                    lView.Items.Add(fItems[i]);
                }
            }
        }

        /// <summary>
        /// 获取上一层路径
        /// </summary>
        /// <param name="fullname">全名</param>
        /// <returns></returns>
        public static string GetParentPath(string fullname)
        {
            int index = fullname.LastIndexOf("\\");
            if (index + 1 >= fullname.Length || index <= 0)
                return "";
            if (index == 2)
                return fullname.Substring(0, index + 1);
            return fullname.Substring(0, index);
        }

        /// <summary>
        /// 获取文件或文件夹的名字(非全名)
        /// </summary>
        /// <param name="fullname">文件或文件夹全名</param>
        /// <returns></returns>
        public static string GetName(string fullname)
        {
            int index = fullname.LastIndexOf("\\");
            if (index + 1 >= fullname.Length || index == -1)
                return "";
            return fullname.Substring(index + 1);
        }

        /// <summary>
        ///获取文件类型
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileType(string file)
        {
            int index = file.LastIndexOf('.');
            if (index + 1 >= file.Length || index == -1)
                return "/";
            return file.Substring(index + 1);
        }

        /// <summary>
        /// 获取所有盘符
        /// </summary>
        /// <returns></returns>
        public static DisksCode GetDisks()
        {
            //string[] diskslist = Directory.GetLogicalDrives();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            DisksCode diskcode = new DisksCode();
            diskcode.Disks = new DiskStruct[drives.Length];
            for (int i = 0; i < drives.Length; i++)
                diskcode.Disks[i] = new DiskStruct(drives[i].Name.Substring(0, 2), drives[i].TotalFreeSpace, drives[i].TotalSize, drives[i].DriveType);
            return diskcode;
        }

        /// <summary>
        /// 为磁盘取个友好的名字
        /// </summary>
        /// <param name="diskName">磁盘名</param>
        /// <param name="serverDisk">是否服务器的磁盘</param>
        /// <returns>(例如: diskName="C:",serverDisk=true ,则返回" 远程磁盘(C:)")</returns>
        public static string DiskToString(string diskName, bool serverDisk)
        {
            return (serverDisk ? "远程磁盘(" + diskName + ")" : "本地磁盘(" + diskName + ")");
        }

        ///   
        /// 获取指定驱动器的空间总大小(单位为B) 
        ///   
        ///  只需输入代表驱动器的字母即可 （大写） 
        ///    
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = new long();
            try
            {
                str_HardDiskName = str_HardDiskName + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                    }
                }
            }
            catch { }

            return totalSize;
        }

        ///   
        /// 获取指定驱动器的剩余空间总大小(单位为B) 
        ///   
        ///  只需输入代表驱动器的字母即可  
        ///    
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            try
            {
                str_HardDiskName = str_HardDiskName + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    }
                }
            }
            catch { }

            return freeSpace;
        }
    }
}
