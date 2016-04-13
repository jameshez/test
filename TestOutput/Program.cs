using Microsoft.VisualStudio.TestTools.UnitTesting;
using robotjob.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace TestOutput
{
    public static class Program
    {
        public static string sKey = "zktd1234";
        static string connectionstr = "Server=115.28.35.193;User id=sa;pwd=abc123,./;database=robottest";

        static void Main(string[] args)
        {
            //WebClient client = new WebClient();
            //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            //var uri = new Uri(@"http://localhost:8001/WebAPI/login.ashx?userName=yilang&userPass=123123");

            //string result = Encoding.UTF8.GetString(client.DownloadData(uri.ToString()));

            //string expected = "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";

            //Assert.AreEqual(expected, result);

            SendBy("84388928@qq.com", "ssssss", "dddddddd");
        }

        public static void SendBy(string to, string subject, string body)
        {
            CDO.Message oMsg = new CDO.Message();
            CDO.IConfiguration iConfg = oMsg.Configuration;
            ADODB.Fields oFields = iConfg.Fields;

            ADODB.Field oField = oFields["http://schemas.microsoft.com/cdo/configuration/sendusing"];
            oField.Value = CDO.CdoSendUsing.cdoSendUsingPort;

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"];
            oField.Value = CDO.CdoProtocolsAuthentication.cdoBasic;

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/smtpserver"];
            oField.Value = "smtp.exmail.qq.com";

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"];
            oField.Value = 465;

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/sendusername"];
            oField.Value = "zktd@51robotjob.com";

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/sendpassword"];
            oField.Value = "ZKTD51robotjob";

            oField = oFields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"];
            oField.Value = "true";

            

            oMsg.TextBody = body;
            oMsg.Subject = subject;
            oMsg.From = "zktd@51robotjob.com";
            oMsg.To = to;
            try
            {
                oMsg.Send();
            }
            catch (Exception)
            {
                throw;
            }
            //MailMessage mailMessage = new MailMessage();
            //mailMessage.To.Add("84388928@qq.com");
            //mailMessage.From = new MailAddress("zktd@51robotjob.com");
            //mailMessage.Subject = subject;
            //mailMessage.Body = body;
            //mailMessage.IsBodyHtml = true;

            ////需要读取配置
            //SmtpClient client = new SmtpClient("smtp.exmail.qq.com", 465)
            //{
            //    EnableSsl = true
            //};
            //client.Credentials = new NetworkCredential("zktd@51robotjob.com", "ZKTD51robotjob");
            ////ConfigurationManager.AppSettings["user"].ToString(),
            ////ConfigurationManager.AppSettings["password"].ToString());

            //try
            //{
            //    client.Send(mailMessage);
            //    //LoggerHelper.Writelog(
            //    //    string.Format("Email发送成功，发送给：{0}，发送标题为：{1}，发送内容为：{2}", to, subject, body),
            //    //    LogLevel.Info);
            //}
            //catch (Exception ex)
            //{
            //    //LoggerHelper.Writelog(ex.Message, LogLevel.Error);
            //}
        }
        public static void getJobInfo()
        {
            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();


            //using (SqlConnection conn = new SqlConnection(connectionstr))//创建连接对象
            //{
            //    conn.Open();//打开连接

            string strSQL = "select * from [robottest].dbo.[qy_Position] "
                        + "where PositionRequire like '%[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]%'";
            try
            {
                DataSet reader = null;
                Console.WriteLine(reader.Tables[0].ToJson());
                Console.WriteLine(Encrypt(reader.Tables[0].ToJson()));
                Console.WriteLine(TextHandler.PasswordMD5((Encrypt(reader.Tables[0].ToJson()) + "95968")));
            }
            catch (Exception ex)
            {

            }
            // }
        }

        public static string GetTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 + "";
        }


        public static string DesDecrypt(string pToDecrypt)
        {
            //转义特殊字符
            pToDecrypt = pToDecrypt.Replace("-", "+");
            pToDecrypt = pToDecrypt.Replace("_", "/");
            pToDecrypt = pToDecrypt.Replace("~", "=");
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //des.Key = UTF8Encoding.ASCII.GetByteCount
                des.Key = UTF8Encoding.UTF8.GetBytes(sKey);
                des.IV = UTF8Encoding.UTF8.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        public static string Encrypt(string sourceString)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(sKey);
            byte[] btIV = Encoding.UTF8.GetBytes(sKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        public static string ToJson(this DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToStr());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }

        public static string ToStr(this object s, string format = "")
        {
            string result = "";
            try
            {
                if (format == "")
                {
                    result = s.ToString();
                }
                else
                {
                    result = string.Format("{0:" + format + "}", s);
                }
            }
            catch
            {
            }
            return result;
        }

        public static DataTable ToDataTable(this string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
    }
}
