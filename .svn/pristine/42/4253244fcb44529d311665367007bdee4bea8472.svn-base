using Microsoft.VisualStudio.TestTools.UnitTesting;
using AU.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU.Common.Utility.Tests
{
    [TestClass()]
    public class HttpHelperTests
    {
        [TestMethod()]
        public void SendPostRequestTest()
        {
            System.Net.HttpStatusCode cod;
            var rs = HttpHelper.SendGetRequest<string>("http://192.168.5.112:60009/api/SystemServiceDate/info", out cod, 5000);
            if (cod == System.Net.HttpStatusCode.OK)
            {
                dynamic record = Newtonsoft.Json.Linq.JObject.Parse(rs);
                Newtonsoft.Json.Linq.JObject jb = (record != null && record.State != null) ? record.State : null;

                if (jb != null && jb["Code"].ToString() == "0")
                {
                    Newtonsoft.Json.Linq.JArray jmdoel = (record != null && record.Models != null) ? record.Models : null;

                    if (jmdoel != null && jmdoel.Count > 0)
                    {
                        var Project = jmdoel[0]["projectNo"];

                    }
                }
            }

            Assert.Fail();
        }
    }
}