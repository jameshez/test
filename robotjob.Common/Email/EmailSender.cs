using robotjob.Common.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 备注：
/// .NET里面不支持465端口的发送，原因是加密顺序有问题，1.0版本貌似EmailMessage里面有支持过，现在不行
/// 如果通过CDO的方式来发送又会有问题，需要IIS中开启SMTP功能。
/// 25端口为普通加密端口
/// 587为TSL加密方式，465为SSL加密方式
/// </summary>

namespace robotjob.Common.Email
{

    internal class EmailSender
    {

        /// <summary>
        /// 邮件通过465端口发送会不成功，微软的System.Net.Mail类只支持 “Explicit SSL”，目前通过CDO的方式来实现，需要在IIS上做对应配置
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
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
        }



        //public static void SendBy(string to, string subject, string body)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.To.Add("84388928@qq.com");
        //    mailMessage.From = new MailAddress("zktd@51robotjob.com");
        //    mailMessage.Subject = subject;
        //    mailMessage.Body = body;
        //    mailMessage.IsBodyHtml = true;

        //    //需要读取配置
        //    SmtpClient client = new SmtpClient("smtp.exmail.qq.com", 465)
        //    {
        //        EnableSsl = true
        //    };
        //    client.Credentials = new NetworkCredential("zktd@51robotjob.com", "ZKTD51robotjob");
        //    //ConfigurationManager.AppSettings["user"].ToString(),
        //    //ConfigurationManager.AppSettings["password"].ToString());

        //    try
        //    {
        //        client.Send(mailMessage);
        //        LoggerHelper.Writelog(
        //            string.Format("Email发送成功，发送给：{0}，发送标题为：{1}，发送内容为：{2}", to, subject, body),
        //            LogLevel.Info);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerHelper.Writelog(ex.Message, LogLevel.Error);
        //    }
        //}
    }
}
