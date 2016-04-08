using robotjob.IDAL;

namespace robotjob.BLL.WebAPI
{
    public class login
    {
        private static IUser dal = DALFactory.DataAccess.CreateUser();
        public string clientAppDoLogin(string username, string userpass)
        {
            var user = dal.GetUser(username);
            if(user == null)
            {
                return "{\"code\":\"0001\",\"msg\":\"不存在该用户\"}";
            }
            if(user.CustomerPass.Equals(userpass))
            {
                return "{\"code\":\"0001\",\"msg\":\"正确\"}";
            }
            return "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";
        }
    }
}
