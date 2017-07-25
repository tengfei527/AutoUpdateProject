using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            //AuUpdateHelp
            Console.WriteLine("传入参数:" + string.Join(",", args));
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
                                Console.WriteLine(filepath + "不存在");
                                break;
                            }

                            runpath = args?[2];
                            if (!System.IO.File.Exists(runpath))
                            {
                                Console.WriteLine(runpath + "不存在");
                                break;
                            }
                            string err = string.Empty;
                            if (!ah.Upgrade(filepath, runpath))
                                Console.WriteLine("升级失败");
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
                Console.WriteLine("升级失败！发生错误,详情：");
                Console.WriteLine(e);
            }
            finally
            {
                //启动主进程
                Console.WriteLine("启动【AuClient.exe】= {0}", ah.CreateAuClient(runpath));
            }
#if DEBUG//测试
            Console.ReadKey();
#endif
        }
    }
}