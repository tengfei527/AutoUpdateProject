using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PackageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:12345";
            using (var nancySelfHost = new NancyHost(new Uri(url), new MyBootstrapper()))
            {
                try
                {
                    nancySelfHost.Start();
                    Console.WriteLine("NancySelfHost已启动。。");
                    System.Diagnostics.Process.Start(url);
                    Console.WriteLine("监听地址：" + url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.Read();
            }
            Console.WriteLine("已经停止 \n NancySelfHost已关闭。。");
        }
    }

    public class MyBootstrapper : DefaultNancyBootstrapper
    {

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("package"));
        }
    }
}
