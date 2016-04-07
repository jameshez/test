using robotjob.DAL;
using System.Data;
using robotjob.DAL.User;
using robotjob.Model;
using robotjob.IDAL;

namespace robotjob.BLL.WebAPI
{
    public class login
    {
        private User user = new User();
        private DataSet ds;

        public string clientAppDoLogin(string username, string userpass)
        {
            IUser dal = new SQLServerDAL.User();
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
