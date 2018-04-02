using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace AuClient
{
    /// <summary>
    /// 服务控制类
    /// </summary>
    public class MyServiceControll
    {

        public string ServerName { get; protected set; }
        public string ServerPath { get; protected set; }
        public MyServiceControll(string servername = "AuGuardService")
        {
            ServerName = servername;
            ServerPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), AuClient.Properties.Resources.ApplicationService);
            //安装服务
            InstallService();

        }

        #region 安装服务  
        /// <summary>  
        /// 安装服务  
        /// </summary>  
        public bool InstallService()
        {
            bool flag = true;
            if (!IsServiceInstalled())
            {
                try
                {
                    if (System.IO.File.Exists(ServerPath))
                        InstallmyService(null, ServerPath);
                    else
                        flag = false;
                }
                catch
                {
                    flag = false;
                }

            }
            return flag;
        }
        #endregion

        #region 卸载服务  
        /// <summary>  
        /// 卸载服务  
        /// </summary>  
        public bool UninstallService()
        {
            bool flag = true;
            if (IsServiceInstalled())
            {
                try
                {
                    if (System.IO.File.Exists(ServerPath))
                        UnInstallmyService(ServerPath);
                    else
                        flag = false;
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region 检查服务存在的存在性  
        public bool IsServiceInstalled()
        {
            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();
            if (services == null)
                return false;

            var ser = services.FirstOrDefault(s => ServerName.Equals(s.ServiceName, StringComparison.InvariantCultureIgnoreCase));
            if (ser == null)
                return false;
            return true;
        }
        #endregion

        #region 安装Windows服务  
        /// <summary>  
        /// 安装Windows服务  
        /// </summary>  
        /// <param name="stateSaver">集合</param>  
        /// <param name="filepath">程序文件路径</param>  
        public void InstallmyService(IDictionary stateSaver, string filepath)
        {
            AssemblyInstaller AssemblyInstaller1 = new AssemblyInstaller();
            AssemblyInstaller1.UseNewContext = true;
            AssemblyInstaller1.Path = filepath;
            AssemblyInstaller1.Install(stateSaver);
            AssemblyInstaller1.Commit(stateSaver);
            AssemblyInstaller1.Dispose();
        }
        #endregion

        #region 卸载Windows服务  
        /// <summary>  
        /// 卸载Windows服务  
        /// </summary>  
        /// <param name="filepath">程序文件路径</param>  
        public void UnInstallmyService(string filepath)
        {
            AssemblyInstaller AssemblyInstaller1 = new AssemblyInstaller();
            AssemblyInstaller1.UseNewContext = true;
            AssemblyInstaller1.Path = filepath;
            AssemblyInstaller1.Uninstall(null);
            AssemblyInstaller1.Dispose();
        }
        #endregion

        #region 判断window服务是否启动  
        /// <summary>  
        /// 判断某个Windows服务是否启动  
        /// </summary>  
        /// <returns></returns>  
        public bool IsServiceStart(string serviceName)
        {
            ServiceController psc = new ServiceController(serviceName);
            bool bStartStatus = false;
            try
            {
                if (!psc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    bStartStatus = true;
                }

                return bStartStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  修改服务的启动项  
        /// <summary>    
        /// 修改服务的启动项 2为自动,3为手动    
        /// </summary>    
        /// <param name="startType"></param>    
        /// <param name="serviceName"></param>    
        /// <returns></returns>    
        public bool ChangeServiceStartType(int startType, string serviceName)
        {
            try
            {
                RegistryKey regist = Registry.LocalMachine;
                RegistryKey sysReg = regist.OpenSubKey("SYSTEM");
                RegistryKey currentControlSet = sysReg.OpenSubKey("CurrentControlSet");
                RegistryKey services = currentControlSet.OpenSubKey("Services");
                RegistryKey servicesName = services.OpenSubKey(serviceName, true);
                servicesName.SetValue("Start", startType);
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;


        }
        #endregion



        /// <summary>
        /// 正常启动
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public bool Start(string path)
        {
            try
            {
                ServiceController ServiceController = new ServiceController(ServerName);
                ServiceController.Start(new string[] {
                    "-s",
                    path,
                });
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 启动 升级
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public bool Start(string filepath, string runpath)
        {
            try
            {
                ServiceController ServiceController = new ServiceController(ServerName);
                ServiceController.Start(new string[] {
                    "-u",
                    filepath,
                    runpath,
                });
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            try
            {
                ServiceController ServiceController = new ServiceController(ServerName);
                ServiceController.Stop();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
