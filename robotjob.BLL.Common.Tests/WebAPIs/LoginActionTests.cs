using Microsoft.VisualStudio.TestTools.UnitTesting;
using robotjob.BLL.WebAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace robotjob.BLL.WebAPI.Tests
{
    [TestClass]
    public class LoginActionTests
    {
        //private string path = ConfigurationManager.AppSettings["URI"];
        [TestMethod]
        public void generalDoLoginTest()
        {

            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            var uri = new Uri(@"http://localhost:8001/WebAPI/login.ashx?userName=yilang&userPass=123123");

            string result = Encoding.UTF8.GetString(client.DownloadData(uri.ToString()));

            string expected = "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";

            Assert.AreEqual(expected, result);
        }
    }
}