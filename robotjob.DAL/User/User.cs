using robotjob.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using robotjob.Model;

namespace robotjob.DAL.User
{
    [FlexibleAOP]
    public class User : ContextBoundObject
    {
        public DataSet LoginUser(Sys_Customer customer)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Customer ");
            strSql.Append("where CustomerName=@CustomerName ");
            strSql.Append("or Phone=@Phone ");
            strSql.Append("or NickName=@NickName ");
            strSql.Append("or Email=@Email ");
            strSql.Append("or Phone=@Phone");
            SqlParameter[] parameters = {
                    new SqlParameter("@CustomerName", SqlDbType.NChar,50),
                    new SqlParameter("@Phone", SqlDbType.NChar,20),
                    new SqlParameter("@NickName", SqlDbType.NChar,50),
                    new SqlParameter("@Email", SqlDbType.NChar,50),
                        };
            parameters[0].Value = customer.CustomerName;
            parameters[1].Value = customer.Phone;
            parameters[2].Value = customer.NickName;
            parameters[3].Value = customer.Email;

            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            DataSet ds = null;
            return ds;
        }

        public int AddUser(Sys_Customer customer)
        {
            return 1;
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * from Sys_Customer ");
            //strSql.Append("where CustomerName=@CustomerName ");
            //strSql.Append("or Phone=@Phone ");
            //strSql.Append("or NickName=@NickName ");
            //strSql.Append("or Email=@Email ");
            //strSql.Append("or Phone=@Phone");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CustomerName", SqlDbType.NChar,50),
            //        new SqlParameter("@Phone", SqlDbType.NChar,20),
            //        new SqlParameter("@NickName", SqlDbType.NChar,50),
            //        new SqlParameter("@Email", SqlDbType.NChar,50),
            //            };
            //parameters[0].Value = customer.CustomerName;
            //parameters[1].Value = customer.Phone;
            //parameters[2].Value = customer.NickName;
            //parameters[3].Value = customer.Email;

            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //return ds;
        }
    }
}
