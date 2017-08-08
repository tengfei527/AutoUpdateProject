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
    ///�ļ����������
    /// </summary>
    public class IO
    {
        /// <summary>
        /// Զ�̻�ȡ�ļ�
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
        ///  �����ļ�
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
        /// �����ļ�
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
        /// �����ļ�
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
        /// �ļ��������
        /// </summary>
        /// <param name="sender"></param>
        public static void EndTranFile(BaseCommunication sender)
        {
            BaseCode code = new BaseCode();
            code.Head = CodeHead.FILE_TRAN_END;
            sender.SendCode(code);
        }

        /// <summary>
        /// ��ȡ�ļ�����ϸ��Ϣ
        /// </summary>
        /// <param name="file">�ļ���</param>
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
                type = "δ֪";
            }
            try
            {
                fileSize = GetFileSize(file);
            }
            catch
            {
                fileSize = "δ֪";
            }
            try
            {
                updatedTime = File.GetLastWriteTime(file).ToString();
            }
            catch
            {
                updatedTime = "δ֪";
            }
            return "����:" + type + "�޸�����:" + updatedTime + "\t��С:" + fileSize;
        }

        /// <summary>
        /// ��ȡ�ļ��Ĵ�С
        /// </summary>
        /// <param name="file">�ļ���</param>
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
        /// ��ȡ�ļ�����(��λ:�ֽ�)
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
        /// �򿪸�Ŀ¼
        /// </summary>
        /// <param name="lView">��ʾ������б���ͼ�ؼ�</param>
        /// <param name="imageHashtable">�ļ�ͼ��ļ�ֵ(��ϣ��)</param>
        public static void OpenRoot(ListView lView, Hashtable imageHashtable)
        {
            DisksCode diskcode = IO.GetDisks();
            if (diskcode != null)
                ShowDisks(diskcode, lView, imageHashtable, false);
        }

        /// <summary>
        /// ��ʾ�����Ĵ���
        /// </summary>
        /// <param name="diskcode">����ָ��</param>
        /// <param name="lView">��ʾ������б���ͼ�ؼ�</param>
        /// <param name="imageHashtable">�ļ�ͼ��ļ�ֵ(��ϣ��)</param>
        /// <param name="serverDisk">�Ƿ�������Ĵ���</param>
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
                    //�ļ���ͼ��
                    dItems[i].ImageKey = (string)imageHashtable["Disk"];
                    dItems[i].Tag = disk[i];
                    dItems[i].SubItems.Add(disk[i].Size);
                    lView.Items.Add(dItems[i]);
                }
            }
        }

        /// <summary>
        /// ��ָ��·��
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="lView">��ʾ������б���ͼ�ؼ�</param>
        /// <param name="imageHashtable">�ļ�ͼ��ļ�ֵ(��ϣ��)</param>
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
        /// ��ʾ�����ϵ��ļ�
        /// </summary>
        /// <param name="explorer">����ָ��</param>
        /// <param name="lView">��ʾ������б���ͼ�ؼ�</param>
        /// <param name="imageHashtable">�ļ�ͼ��ļ�ֵ(��ϣ��)</param>
        public static void ShowHostDirectory(ExplorerCode explorer, ListView lView, Hashtable imageHashtable)
        {
            DirectoryStruct[] directorys;
            FileStruct[] files;
            if (!explorer.Available)
            {
                MessageBox.Show("��ǰ·���޷�����!");
                return;
            }
            lView.Items.Clear();
            directorys = explorer.Directorys;
            files = explorer.Files;

            lView.Tag = explorer.Path;

            //��ӻ��˹���
            string parentPath = IO.GetParentPath(explorer.Path);
            DirectoryStruct lastDirectory = new DirectoryStruct(parentPath);
            ListViewItem lastItem = new ListViewItem("�ϼ�Ŀ¼");
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
                    //�ļ���ͼ��
                    dItems[i].ImageKey = (string)imageHashtable["Directory"];
                    dItems[i].Tag = directorys[i];
                    dItems[i].SubItems.Add(directorys[i].LastUpdateTime);
                    dItems[i].SubItems.Add("�ļ���");
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
                    //�ļ�ͼ��
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
        /// ��ȡ��һ��·��
        /// </summary>
        /// <param name="fullname">ȫ��</param>
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
        /// ��ȡ�ļ����ļ��е�����(��ȫ��)
        /// </summary>
        /// <param name="fullname">�ļ����ļ���ȫ��</param>
        /// <returns></returns>
        public static string GetName(string fullname)
        {
            int index = fullname.LastIndexOf("\\");
            if (index + 1 >= fullname.Length || index == -1)
                return "";
            return fullname.Substring(index + 1);
        }

        /// <summary>
        ///��ȡ�ļ�����
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
        /// ��ȡ�����̷�
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
        /// Ϊ����ȡ���Ѻõ�����
        /// </summary>
        /// <param name="diskName">������</param>
        /// <param name="serverDisk">�Ƿ�������Ĵ���</param>
        /// <returns>(����: diskName="C:",serverDisk=true ,�򷵻�" Զ�̴���(C:)")</returns>
        public static string DiskToString(string diskName, bool serverDisk)
        {
            return (serverDisk ? "Զ�̴���(" + diskName + ")" : "���ش���(" + diskName + ")");
        }

        ///   
        /// ��ȡָ���������Ŀռ��ܴ�С(��λΪB) 
        ///   
        ///  ֻ�������������������ĸ���� ����д�� 
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
        /// ��ȡָ����������ʣ��ռ��ܴ�С(��λΪB) 
        ///   
        ///  ֻ�������������������ĸ����  
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
