using System;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using AU.Common.Utility;
using AU.Common;
using System.Collections.Generic;
using System.Diagnostics;

namespace AU.Common
{
    /// <summary>
    /// updater ��ժҪ˵����
    /// </summary>
    public class AppUpdater : IDisposable
    {
        #region ��Ա���ֶ�����
        private bool disposed = false;
        private IntPtr handle;
        private Component component = new Component();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        /// <summary>
        /// ��Ϣ֪ͨ
        /// </summary>
        public event EventHandler<NotifyMessage> Notify;
        /// <summary>
        /// �°汾����Ϣ
        /// </summary>
        public AuPackage UpdateAuPackage { get; set; }
        /// <summary>
        /// Ŀ�����������Ϣ
        /// </summary>
        public AuPackage TargetAuPackage { get; set; }
        /// <summary>
        /// ����·��
        /// </summary>
        public string AuBackupPath { get; set; }
        /// <summary>
        /// ϵͳ��ַ
        /// </summary>
        public string SystemPath { get; set; }
        #endregion

        /// <summary>
        ///  AppUpdater���캯��
        /// </summary>
        /// <param name="targetPath">����������Ϣ</param>
        /// <param name="updatePath">�°汾����Ϣ</param>
        public AppUpdater(string targetPath, string updatePath, string aubackupPath, string systemPath, string subsystem)
        {
            this.SystemPath = systemPath;
            this.AuBackupPath = aubackupPath;
            //this.handle = handle;
            this.UpdateAuPackage = new AuPackage(updatePath, subsystem);

            this.TargetAuPackage = new AuPackage(targetPath, subsystem);
        }
        /// <summary>
        /// ����
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    component.Dispose();
                }
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }
            disposed = true;
        }
        /// <summary>
        /// ��������
        /// </summary>
        ~AppUpdater()
        {
            Dispose(false);
        }

        /// <summary>
        /// �������ļ�
        /// </summary>
        /// <param name="serverXmlFile"></param>
        /// <param name="localXmlFile"></param>
        /// <param name="updateFileList"></param>
        /// <returns></returns>
        public int CheckForUpdate(string subsystem, out AuPackage updatePackage)
        {
            updatePackage = null;
            if (this.UpdateAuPackage.LocalAuList == null)
                return -1;
            if (this.TargetAuPackage.LocalAuList == null)
            {
                updatePackage = this.UpdateAuPackage;
                return updatePackage.LocalAuList.Files.Count;
            }
            //�Ƚϴ���ڵ�
            var no = AU.Common.Utility.RegistryHelper.GetRegistryData(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7\\AuError\\", subsystem);
            if (this.UpdateAuPackage.LocalAuList.No == no)
                return -1;

            int k = 0;
            List<AuFile> updateFileList = new List<AuFile>();
            //���� �޸�
            foreach (var m in this.UpdateAuPackage.LocalAuList.Files)
            {
                var index = this.TargetAuPackage.LocalAuList.Files.FindIndex(d => d.No == m.No);
                if (index == -1)//�����ļ�
                {
                    updateFileList.Add(m);
                    k++;
                }//�������汾�߸���
                else
                {
                    var tempversion = new Version(m.Version);
                    var localversion = new Version(this.TargetAuPackage.LocalAuList.Files[index].Version);
                    string localFile = this.TargetAuPackage.LocalPath + "\\" + this.TargetAuPackage.LocalAuList.Files[index].WritePath;
                    if (!System.IO.File.Exists(localFile) || tempversion > localversion || (tempversion == localversion && !this.FilterExtension(localFile) && ToolsHelp.ComputeSHA256(localFile).ToLower() != m.SHA256.ToLower()))
                    {
                        updateFileList.Add(m);
                        k++;
                    }
                }
            }
            if (k > 0)
            {
                updatePackage = this.UpdateAuPackage;
                updatePackage.LocalAuList.Files = updateFileList;
            }
            return k;
        }
        public bool FilterExtension(string file)
        {
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"\.(log|config|db|dat|txt|json)$|(unins000.exe)", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

            return rg.IsMatch(file);
        }
        /// <summary>
        /// ֪ͨ��Ϣ
        /// </summary>
        /// <param name="state"></param>
        private void NotifyMessage(NotifyMessage state)
        {
            if (Notify != null)
            {
                Notify.Invoke(this, state);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public bool IsUpgrade = false;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        public void Upgrade(object obj)
        {
            AuPackage upgradeFiles = obj as AuPackage;
            if (upgradeFiles == null || upgradeFiles.LocalAuList == null)
            {
                this.IsUpgrade = false;
                return;
            }

            NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "��ʼ�����ļ�"));

            try
            {
                AuApplication auapplication = this.TargetAuPackage.LocalAuList != null ? this.TargetAuPackage.LocalAuList.Application : this.UpdateAuPackage.LocalAuList.Application;

                if (auapplication != null && !string.IsNullOrEmpty(auapplication.EntryPoint))
                {
                    do
                    {
                        NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "׼���ر�Ӧ��: " + auapplication.EntryPoint + "���Ժ�"));
                        if (auapplication.CloseType == 0)
                            AU.Common.Utility.ToolsHelp.CloseApplication(auapplication.EntryPoint);
                        else
                        {
                            string p = this.TargetAuPackage.LocalPath + "\\" + auapplication.Location + "\\" + auapplication.ApplicationId;
                            if (System.IO.File.Exists(p))
                            {
                                Process pro = System.Diagnostics.Process.Start(p, auapplication.CloseArgs);
                                pro.WaitForExit(2000);
                                //��ֹδ�رս��̹ر�һ��
                                AU.Common.Utility.ToolsHelp.CloseApplication(auapplication.EntryPoint);
                                //�����Ϣ������˳��ر�MQ����
                                if (SystemType.coreserver.ToString() == upgradeFiles.SubSystem)
                                {
                                    do
                                    {
                                        AU.Common.Utility.ToolsHelp.CloseApplication("MQ.BrokerServer.exe");
                                    } while (AU.Common.Utility.ToolsHelp.IsRunApplication("MQ.BrokerServer.exe"));
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(2000);

                    } while (AU.Common.Utility.ToolsHelp.IsRunApplication(auapplication.EntryPoint));

                    NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "�ѳɹ��ر�Ӧ��:" + auapplication.EntryPoint));
                }
                //�������·��
                if (System.IO.Directory.Exists(this.AuBackupPath))
                {
                    System.IO.Directory.Move(this.AuBackupPath, this.AuBackupPath.TrimEnd('\\') + "_" + DateTime.Now.ToString("yyyyMMddHHmm"));
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "��ⱸ����Ϣ����"));
                }
                int index = 0;
                NotifyMessage(new Common.NotifyMessage(NotifyType.Process, "���Ժ�...", upgradeFiles.LocalAuList.Files.Count));
                foreach (var m in upgradeFiles.LocalAuList.Files)
                {

                    NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "���ڴ���[" + m.No + "]�ļ�,���Ժ�...", upgradeFiles.LocalAuList.Files.Count));
                    //����·��
                    string path = this.TargetAuPackage.LocalPath + "\\" + m.WritePath;
                    switch (m.FileType)
                    {//�ļ����� 0=DLL 1=exe 2=SQL 3=Image 4����
                        case 2://sql ����
                            {
                                string config = this.SystemPath + "\\Core\\Web.config";
                                //���������������ִֹ�г�ʼ���ű�
                                if (System.IO.File.Exists(config) && !m.WritePath.Contains("Core\\bin\\db\\"))
                                {
                                    string con = ConfigUtility.GetApiDbConnect(config);
                                    if (!string.IsNullOrEmpty(con))
                                        AuDataBase.RunScriptFile(path, con);
                                }
                            }
                            break;
                        default:
                            if (System.IO.File.Exists(path))
                            {
                                //����
                                string bak = this.AuBackupPath + m.WritePath;
                                ToolsHelp.CreateDirtory(bak);
                                System.IO.File.Copy(path, bak, true);
                            }
                            break;
                    }
                    NotifyMessage(new Common.NotifyMessage(NotifyType.UpProcess, index + ":100%", index));
                    index++;

                }
                //����Ŀ�����Ϣ
                if (System.IO.File.Exists(this.TargetAuPackage.PackagePath))
                {
                    ToolsHelp.CreateDirtory(this.AuBackupPath + "\\" + AuPackage.PackageName);
                    System.IO.File.Copy(this.TargetAuPackage.PackagePath, this.AuBackupPath + "\\" + AuPackage.PackageName, true);
                }
                else
                {
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "����:δ��Ŀ�����Ϣ��"));
                }
                //���� �����ļ�����ѡ��������
                try
                {
                    AU.Common.Utility.ToolsHelp.CopyFile(upgradeFiles.LocalPath, this.SystemPath);
                }
                catch (Exception e)
                {
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "�����ļ�ʧ��," + e.Message, e));
                    //ɾ������Ϣ
                    try
                    {
                        AU.Common.Utility.ToolsHelp.DeleteFile(this.TargetAuPackage.PackagePath);
                    }
                    catch (Exception ex)
                    {
                        NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "����:ɾ��Ŀ�����Ϣʧ��,����:" + ex.Message, e));
                    }
                    //��ԭ
                    this.Rollback();
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "�ѳɹ�ִ�������ع���"));
                    throw e;
                }
                System.IO.Directory.Delete(upgradeFiles.LocalPath, true);
                this.IsUpgrade = true;
                //����д����ʶɾ�������ʶ
                AU.Common.Utility.RegistryHelper.DeleteRegist(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7\\AuError\\", upgradeFiles.SubSystem);
            }
            catch (Exception e)
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "����ʧ��," + e.Message, e));
                //д�����ʶ
                AU.Common.Utility.RegistryHelper.SetRegistryData(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7\\AuError\\", upgradeFiles.SubSystem, upgradeFiles.LocalAuList.No);
                this.IsUpgrade = false;
                return;
            }
            finally
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.StopDown, "�������"));
            }

            return;
        }
        /// <summary>
        /// ��ԭϵͳ
        /// </summary>
        /// <returns>��ԭ�Ƿ�ɹ�</returns>
        public bool Rollback()
        {
            if (System.IO.Directory.Exists(this.AuBackupPath))
            {
                if (this.TargetAuPackage.LocalAuList != null && this.TargetAuPackage.LocalAuList.Application.StartType == 1)
                {
                    do
                    {
                        try
                        {
                            AU.Common.Utility.ToolsHelp.CloseApplication(this.TargetAuPackage.LocalAuList.Application.EntryPoint);
                        }
                        catch (Exception e)
                        {
                            NotifyMessage(new Common.NotifyMessage(NotifyType.Normal, "����:�ع��汾�رճ���" + this.TargetAuPackage.LocalAuList.Application.EntryPoint + "ʧ��,����:" + e.Message, e));
                        }
                        System.Threading.Thread.Sleep(1000);
                    } while (AU.Common.Utility.ToolsHelp.IsRunApplication(this.TargetAuPackage.LocalAuList.Application.EntryPoint));
                }

                AU.Common.Utility.ToolsHelp.CopyFile(this.AuBackupPath, this.SystemPath);

                return true;
            }

            return false;
        }
        /*
    public void DownUpdateFile(object obj)
    {
        AuPackage upgradeFiles = obj as AuPackage;
        if (upgradeFiles == null || upgradeFiles.LocalAuList == null)
        {
            IsDownLoad = false;
            return;
        }

        NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "��ʼ�����ļ�"));

        try
        {
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {

                if (p.ProcessName.ToLower() + ".exe" == MyAuPackage.LocalAuList.Application.EntryPoint.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                }
            }
            int index = 0;
            foreach (var m in upgradeFiles.LocalAuList.Files)
            {
                try
                {

                    string down = upgradeFiles.LocalAuList.Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? upgradeFiles.LocalAuList.Url + "/" + m.DownPath : "http://" + upgradeFiles.LocalAuList.Url + "/" + m.DownPath;
                    WebRequest webReq = WebRequest.Create(down);
                    WebResponse webRes = webReq.GetResponse();
                    long fileLength = webRes.ContentLength;
                    if (fileLength < 0)
                        continue;


                    NotifyMessage(new Common.NotifyMessage(NotifyType.Process, "��������[" + m.No + "]�ļ�,���Ժ�...", fileLength));

                    Stream srm = webRes.GetResponseStream();
                    StreamReader srmReader = new StreamReader(srm);
                    byte[] bufferbyte = new byte[fileLength];
                    int allByte = (int)bufferbyte.Length;
                    int startByte = 0;
                    while (fileLength > 0)
                    {
                        int downByte = srm.Read(bufferbyte, startByte, allByte);
                        if (downByte == 0) { break; };
                        startByte += downByte;
                        allByte -= downByte;

                        float part = (float)startByte / 1024;
                        float total = (float)bufferbyte.Length / 1024;
                        int percent = Convert.ToInt32((part / total) * 100);


                        NotifyMessage(new Common.NotifyMessage(NotifyType.UpProcess, index + ":" + percent.ToString() + "%", downByte));
                        //this.Invoke((MethodInvoker)delegate ()
                        //{
                        //    pbDownFile.Value += downByte;
                        //    this.lvUpdateList.Items[i].SubItems[2].Text = percent.ToString() + "%";
                        //});

                    }

                    string tempPath = upgradeFiles.LocalPath + "\\" + m.WritePath;
                    ToolsHelp.CreateDirtory(tempPath);
                    FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(bufferbyte, 0, bufferbyte.Length);
                    srm.Close();
                    srmReader.Close();
                    fs.Close();
                    index++;
                }
                catch (WebException ex)
                {
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "�����ļ�����ʧ��", ex));
                    IsDownLoad = false;
                    return;
                }
            }
        }
        catch (Exception e)
        {
            NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "�ر����߳�ʧ��", e));

            IsDownLoad = false;
            return;
        }
        finally
        {
            NotifyMessage(new Common.NotifyMessage(NotifyType.StopDown, "�������"));
            //this.Invoke((MethodInvoker)delegate ()
            //{
            //    InvalidateControl();
            //    this.Cursor = Cursors.Default;
            //});
        }
        //isDownLoad = true;
        IsDownLoad = true;
        return;
    }
    */


    }
}
