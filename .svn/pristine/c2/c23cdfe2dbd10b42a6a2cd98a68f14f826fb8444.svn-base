using Domain.Model;
using Domain.Repositories;
using Nancy;
using Nancy.ModelBinding;
using Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuWriter.Modules
{
    public class ProjectModule : NancyModule
    {

        public EntityFrameworkRepository<Project> project = new EntityFrameworkRepository<Project>(Infrastructure.ServiceLocator.Instance.GetService<Domain.Repositories.IRepositoryContext>());
        public ProjectModule()
        {
            Get["/Api/Project/GetAll"] = args =>
            {
                try
                {
                    var sr = project.GetAll();
                    return Response.AsJson<IEnumerable<Project>>(sr);
                }
                catch (Exception e)
                {
                    return Response.AsJson<string>(e.Message);
                }
            };
            Get["/Api/Project/GetByRid"] = args =>
            {
                try
                {
                    Guid rid = new Guid(this.Request.Query["Rid"]);
                    var sr = project.Get(Domain.Specifications.Specification<Project>.Eval(d => d.Rid == rid));
                    return Response.AsJson<Project>(sr);
                }
                catch (Exception e)
                {
                    return Response.AsJson<string>(e.Message);
                }
            };
            Get["/Api/Project/GetByGid"] = args =>
            {
                try
                {
                    string gid = this.Request.Query["Gid"];
                    var sr = project.Get(Domain.Specifications.Specification<Project>.Eval(d => d.ProjectNo == gid));
                    return Response.AsJson<Project>(sr);
                }
                catch (Exception e)
                {
                    return Response.AsJson<string>(e.Message);
                }
            };
        }
    }
}
