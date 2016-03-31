using robotjob.DAL;
using System.Data;
using robotjob.DAL.User;
using robotjob.Model.Sys;


namespace robotjob.BLL.WebAPI
{
    public class login
    {
        private User user = new User();
        private DataSet ds;

        public string clientAppDoLogin(string username, string userpass)
        {
            //_PageRecords = myConn.load_Query(string.Format("SELECT CustomerId,CustomerName,CustomerPass,Phone,CustomerType FROM Sys_Customer where CustomerName='{0}' OR Phone='{0}' OR Email='{0}'", username));

            Sys_Customer customer = new Sys_Customer()
            {
                CustomerName = username,
                Phone = username,
                NickName = username,
                Email = username
            };
            
            ds = user.LoginUser(customer);

            user.AddUser(customer);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return "{\"code\":\"0001\",\"msg\":\"不存在该用户\"}";
            }
            if (ds.Tables[0].Rows[0]["CustomerPass"].ToString().Equals(userpass))
            {
                return "{\"code\":\"0001\",\"msg\":\"正确\"}";
            }
            return "{\"code\":\"0001\",\"msg\":\"用户名或密码错误\"}";
        }
    }
}
