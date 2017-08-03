using AU.Common;
using AU.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuClient
{
    public class PeculiarHelp
    {
        public static LoginModel GetProject(string url = "http://localhost:60009/api/SystemServiceDate/info")
        {
            LoginModel lm = new LoginModel();
            try
            {
                System.Net.HttpStatusCode cod;
                var rs = HttpHelper.SendGetRequest<string>(url, out cod, 5000);
                if (cod == System.Net.HttpStatusCode.OK)
                {
                    dynamic record = Newtonsoft.Json.Linq.JObject.Parse(rs);
                    Newtonsoft.Json.Linq.JObject jb = (record != null && record.State != null) ? record.State : null;
                    if (jb != null && jb["Code"].ToString() != "2")
                    {
                        lm.ProjectVer = jb["Describe"].ToString();
                    }
                    if (jb != null && jb["Code"].ToString() == "0")
                    {
                        Newtonsoft.Json.Linq.JArray jmdoel = (record != null && record.Models != null) ? record.Models : null;

                        if (jmdoel != null && jmdoel.Count > 0)
                        {
                            lm.ProjectNo = jmdoel[0]["ProjectNo"]?.ToString();
                            lm.ProjectName = jmdoel[0]["Name"]?.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return lm;
        }
    }
}
