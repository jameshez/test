using robotjob.Common;
using robotjob.IDAL;

namespace robotjob.BLL.WebAPI
{
    /// <summary>
    /// 登录相关的类
    /// </summary>
    public class LoginAction
    {
        private static IUser dal = DALFactory.DataAccess.CreateUser();
        /// <summary>
        /// 普通登录，任意条件均可，type指定，1为企业，2为个人，all为所有
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpass"></param>
        /// <returns></returns>
        private string generalDoLogin(string username, string userpass, string type = "all")
        {
            var user = dal.GetUser(username);
            if(user == null)
            {
                return "{\"code\":\"0001\",\"msg\":\"不存在该用户\"}";
            }
            if(user.CustomerPass.Equals(TextHandler.PasswordMD5(userpass)))
            {                
                if(!user.CustomerType.Equals(type) && type!="all")
                {
                    return "{\"code\":\"0001\",\"msg\":\"正确\"}";
                }
                return "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";
            }
            return "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpass"></param>
        /// <returns></returns>
        public string clientLogin(string username, string userpass)
        {
            return generalDoLogin(username, userpass,"2");
        }

        /// <summary>
        /// 企业登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpass"></param>
        /// <returns></returns>
        public string enterpriseLogin(string username, string userpass)
        {
            return generalDoLogin(username, userpass, "1");
        }
    }
}
