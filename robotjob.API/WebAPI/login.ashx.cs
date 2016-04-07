using System.Web;
using robotjob.BLL;
using robotjob.Common;

namespace robotjob.API.WebAPI
{
    /// <summary>
    /// Summary description for login
    /// </summary>
    public class login : IHttpHandler
    {
        private BLL.WebAPI.login loginBLL = new BLL.WebAPI.login();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string username = context.Request["userName"];
            string userPass = context.Request["userPass"];

            string strJson = "{\"code\":\"0001\",\"msg\":\"需要安全连接\"}";

            if(System.Web.HttpContext.Current.Request.IsSecureConnection)
            {
                strJson = loginBLL.clientAppDoLogin(username, TextHandler.MD5(userPass));
            }
            context.Response.Write(strJson);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        
    }
}