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

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="application">程序名称</param>
        /// <returns></returns>
        public static string GetVersion(string path, string application = "OneCardSystem.VehicleManageWPF.exe")
        {
            string temp = path + "\\" + application;
            string result = string.Empty;
            if (!System.IO.File.Exists(temp))
                return result;
            try
            {
                System.Diagnostics.FileVersionInfo m_fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(temp);

                result = string.Format("{0}.{1}.{2}.{3}", m_fvi.FileMajorPart, m_fvi.FileMinorPart, m_fvi.FileBuildPart, m_fvi.FilePrivatePart);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
