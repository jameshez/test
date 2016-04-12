<%@ WebHandler Language="C#" Class="login" %>


using System.Web;
using robotjob.Common;

public class login : IHttpHandler
{
    private robotjob.BLL.WebAPI.LoginAction loginBLL = new robotjob.BLL.WebAPI.LoginAction();

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string username = context.Request["userName"];
        string userPass = context.Request["userPass"];

        string strJson = "{\"code\":\"0001\",\"msg\":\"未知错误\"}";

        //条件允许时可以检查是否SSL连接开启
        //if (System.Web.HttpContext.Current.Request.IsSecureConnection)
        //{
        //}
        strJson = loginBLL.enterpriseLogin(username,userPass);

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