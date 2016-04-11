using robotjob.Common;
using robotjob.IDAL;

namespace robotjob.BLL.WebAPI
{
    /// <summary>
    /// For login, register or other login related stuff
    /// </summary>
    public class LoginAction
    {
        private static IUser dal = DALFactory.DataAccess.CreateUser();
        /// <summary>
        /// 普通登录，任意条件均可
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpass"></param>
        /// <returns></returns>
        public string generalDoLogin(string username, string userpass, string type = "all")
        {
            var user = dal.GetUser(username);
            if(user == null)
            {
                return "{\"code\":\"0001\",\"msg\":\"不存在该用户\"}";
            }
            if(user.CustomerPass.Equals(TextHandler.PasswordMD5(userpass)))
            {
                if ((type.Equals("all")) || 
                    (type.Equals("1") && user.CustomerType.Equals(type))||
                    (type.Equals("2") && user.CustomerType.Equals(type))
                    )
                {
                    return "{\"code\":\"0001\",\"msg\":\"正确\"}";
                }
            }
            return "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";
        }

        //1为企业，2为个人
        public string clientLogin(string username, string userpass)
        {
            return generalDoLogin(username, userpass,"2");
        }
        //1为企业，2为个人
        public string enterpriseLogin(string username, string userpass)
        {
            return generalDoLogin(username, userpass, "1");
        }
    }
}
