using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotjob.Common.Email
{
    public static class EmailHelper
    {
        private static readonly string emailsString = ConfigurationManager.AppSettings["FatalErrorsEmailRecipients"];
        public static void FatalError(Exception ex)
        {
            string[] emails = emailsString.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

            //string applicationType = ConfigurationManager.AppSettings["ApplicationType"];

            if (emails.Length > 0)
            {
                string subject = string.Format("Fatal Error in 51robotjob");
                const string body = @"
						<html>
							<head>
							</head>
							<body>
								<div>
									<b>Machine Name:</b>
									<br/>
									{0}
								</div>
								<div>
									<b>Short Message:</b>
									<br/>
									{1}
								</div>
								<br/>
								<div>
									<b>Inner Exception:</b>
									<br/>
									{2}
								</div>
								<br/>
								<div>
									<b>Stack Trace:</b>
									<br/>
									{3}
								</div>
							</body>
						</html>";
                string messageBody = string.Format(body, Environment.MachineName, ex.Message, ex.InnerException, ex.StackTrace);

                foreach (string email in emails)
                {
                    EmailSender.SendBy(email, subject, messageBody);
                }
            }
        }
    }
}
