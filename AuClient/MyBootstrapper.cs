using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuClient
{
    /// <summary>
    /// 配置
    /// </summary>
    public class MyBootstrapper : Nancy.DefaultNancyBootstrapper
    {

        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("package"));
        }
    }
}
