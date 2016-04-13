using robotjob.Common.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace robotjob.Common.Email
{
    /// <summary>
    /// 发邮件貌似还有点小问题，会超时
    /// </summary>
    internal class EmailSender
    {
        public static void SendBy(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress("zktd@51robotjob.com");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            //需要读取配置
            SmtpClient client = new SmtpClient("smtp.exmail.qq.com", 465)
                                            { EnableSsl = true };
            client.Credentials = new NetworkCredential("zktd@51robotjob.com", "ZKTD51robotjob");
                //ConfigurationManager.AppSettings["user"].ToString(),
                //ConfigurationManager.AppSettings["password"].ToString());

            try
            {
                client.Send(mailMessage);
                LoggerHelper.Writelog(
                    string.Format("Email发送成功，发送给：{0}，发送标题为：{1}，发送内容为：{2}", to, subject, body), 
                    LogLevel.Info);
            }
            catch (Exception ex)
            {
                LoggerHelper.Writelog(ex.Message, LogLevel.Error);
            }
        }
    }
}
