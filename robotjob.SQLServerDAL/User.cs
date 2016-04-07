using robotjob.Common;
using robotjob.DBUtility;
using robotjob.IDAL;
using robotjob.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace robotjob.SQLServerDAL
{
    public class User : IAOPInterface, IUser
    {
        private const string PARM_ITEM_ID = "@CustomerName";
        private const string SQL_SELECT_ITEM = "select * from Sys_Customer where CustomerName = @CustomerName or Phone = @CustomerName or NickName = @CustomerName or Email = @CustomerName ";
        SqlDataReaderToModel<Sys_Customer> translator = new SqlDataReaderToModel<Sys_Customer>();

        public Sys_Customer GetUser(string userName)
        {
            Sys_Customer user = null;
            SqlParameter parm = new SqlParameter(PARM_ITEM_ID, SqlDbType.VarChar, 50);
            parm.Value = userName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM, parm))
            {
                if (rdr.Read())
                {
                    user = translator.DoTransferType(rdr);
                }
            }
            return user;
        }
    }
}
