using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robotjob.Common;
using System.Net;
using System.Xml;
using robotjob.Common.Aspects;

namespace robotjob.Common.SMS
{
    public static class SMSSender
    {
        private static string getVerifCode
        {
            get
            {
                Random random = new Random();
                return random.Next(100000, 999999).ToString();
            }
        }
        
        [DBAction]
        public static string SMSSend(string phone, string smsTemplate, int smsType)
        {
            if (!TextHandler.CheckMobile(phone))
                return "{\"code\":\"0001\",\"msg\":\"手机号格式有误\"}";
            string code = getVerifCode;
            try
            {
                string msg = string.Format(smsTemplate, code);
                string url = "http://xtx.telhk.cn:8888/sms.aspx?action=send&userid=5901&account=a10375&password=21541244&mobile=" + phone + "&content=" + msg + "&sendTime=&mobilenumber=1&countnumber=1&telephonenumber=0";
                string retMsg = WebSendMessage(url, "UTF-8", "post", msg);
                if (getXmlDocument(retMsg).Equals("ok"))
                {
                    return "{\"code\":\"0000\",\"msg\":\"验证码发送成功\"}";
                }
                else
                {
                    return "{\"code\":\"0001\",\"msg\":\"" + getXmlDocument(retMsg) + "\"}";
                }
            }
            catch
            {
                return "{\"code\":\"0001\",\"msg\":\"验证码发送失败\"}";
            }
        }

        private static string getXmlDocument(string retMsg)
        {
            string ret = string.Empty;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(retMsg);
            try
            {
                ret = xml.GetElementsByTagName("message").Item(0).InnerText.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        private static string WebSendMessage(string Url, string encode, string method, string strMessage)
        {
            string strResponse;
            // 初始化WebClient 
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Accept", "*/*");
            webClient.Headers.Add("Accept-Language", "zh-cn");
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            try
            {
                byte[] responseData = webClient.UploadData(Url, method, Encoding.GetEncoding(encode).GetBytes(strMessage));
                string srcString = Encoding.GetEncoding(encode).GetString(responseData);
                strResponse = srcString;
            }
            catch
            {
                return "-1";
            }
            finally
            {
                webClient.Dispose();
            }
            return strResponse;
        }
    }
}
