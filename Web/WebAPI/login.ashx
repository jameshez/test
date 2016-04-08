<%@ WebHandler Language="C#" Class="login" %>


using System.Web;
using robotjob.Common;

public class login : IHttpHandler
{
    private robotjob.BLL.WebAPI.login loginBLL = new robotjob.BLL.WebAPI.login();

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string username = context.Request["userName"];
        string userPass = context.Request["userPass"];

        string strJson = "{\"code\":\"0001\",\"msg\":\"需要安全连接\"}";

        if (System.Web.HttpContext.Current.Request.IsSecureConnection)
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