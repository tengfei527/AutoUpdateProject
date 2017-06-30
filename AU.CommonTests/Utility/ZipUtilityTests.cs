using Microsoft.VisualStudio.TestTools.UnitTesting;
using AU.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace AU.Common.Utility.Tests
{
    [TestClass()]
    public class ZipUtilityTests
    {
        public string password = "123";
        [TestMethod()]
        public void ComTest()
        {
            int count = ZipUtility.Compress(@"C:\Users\ftf\Desktop\F6", @"C:\Users\ftf\Desktop\F6.zip", ZipUtility.CompressLevel.Level6, password: password);
            Assert.IsTrue(count > 0);
        }


        [TestMethod()]
        public void DecompressTest()
        {

            bool result = ZipUtility.Decompress(@"C:\Users\ftf\Desktop\F6.zip", @"C:\Users\ftf\Desktop\F62", password);
            Assert.IsTrue(result);
        }
    }
}