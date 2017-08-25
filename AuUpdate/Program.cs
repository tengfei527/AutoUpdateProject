using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream ostrm = null;
            StreamWriter writer = null;
            try
            {
                ostrm = new FileStream("./update.log", FileMode.OpenOrCreate, FileAccess.Write);
                ostrm.Position = ostrm.Length;
                writer = new StreamWriter(ostrm);
                Console.SetOut(writer);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":" + "Cannot open update.txt for writing");
                Console.WriteLine(DateTime.Now.ToString() + ": " + e.Message);
            }


            //AuUpdateHelp
            Console.WriteLine(DateTime.Now.ToString() + ":" + "传入参数:" + string.Join(",", args));
            string runpath = string.Empty;
            //string lo = System.Reflection.Assembly.GetCallingAssembly().Location;
            //sw.WriteLine(lo);
            AuUpdateHelp ah = new AuUpdateHelp(AuUpdate.Properties.Resources.Core);
            try
            {
                switch (args?.Length > 0 ? args[0].ToLower() : "")
                {
                    case "-u":
                    case "/u":
                        {
                            //升级
                            string filepath = args?[1];

                            if (!System.IO.File.Exists(filepath))
                            {
                                Console.WriteLine(DateTime.Now.ToString() + ":" + filepath + "不存在");
                                break;
                            }

                            runpath = args?[2];
                            if (!System.IO.File.Exists(runpath))
                            {
                                Console.WriteLine(DateTime.Now.ToString() + ":" + runpath + "不存在");
                                break;
                            }
                            string err = string.Empty;
                            if (!ah.Upgrade(filepath, runpath))
                                Console.WriteLine(DateTime.Now.ToString() + ":" + "升级失败");
                        }
                        break;
                }
                if (!System.IO.File.Exists(runpath))
                {
                    runpath = Environment.CurrentDirectory + "\\" + AuUpdate.Properties.Resources.Core + ".exe";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":" + "升级失败！发生错误,详情：");
                Console.WriteLine(DateTime.Now.ToString() + ":" + e);
            }
            finally
            {
                //启动主进程
                Console.WriteLine(DateTime.Now.ToString() + ":" + "启动【AuClient.exe】= {0}", ah.CreateAuClient(runpath));
            }
            try
            {
                if (writer != null)
                    writer.Close();
                if (writer != null)
                    ostrm.Close();
            }
            catch { }
#if DEBUG//测试
            Console.ReadKey();
#endif
        }
    }
}