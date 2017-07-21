using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDomin
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRuned;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(Program).FullName, out isRuned);
            try
            {
                if (isRuned)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    while (true)
                    {

                        string path = System.IO.Path.Combine(Application.StartupPath, AuShell.Properties.Resources.Core);
                        Assembly a = Program.LoadAssembly(path, true);
                        Type t = a.GetType(AuShell.Properties.Resources.CoreName);
                        Object o = Activator.CreateInstance(t);
                        Application.Run((System.Windows.Forms.Form)o);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }
        /// <summary>
        /// 加载DLL文件
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="dynamicLoad"></param>
        /// <returns></returns>
        public static Assembly LoadAssembly(string sFilePath, bool dynamicLoad)
        {
            Assembly assemblyObj = null;
            if (!dynamicLoad)
            {
                #region 方法一：直接从DLL路径加载  
                assemblyObj = Assembly.LoadFrom(sFilePath);
                #endregion
            }
            else
            {
                #region 方法二：先把DLL加载到内存，再从内存中加载（可在程序运行时动态更新dll文件，比借助AppDomain方便多了！）  
                using (FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bFile = br.ReadBytes((int)fs.Length);
                        br.Close();
                        fs.Close();
                        assemblyObj = Assembly.Load(bFile);
                    }
                }
                #endregion
            }

            return assemblyObj;
        }
    }
}
