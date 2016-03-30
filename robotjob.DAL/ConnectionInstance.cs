using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using robotjob.Common;

namespace robotjob.DAL
{
    /// <summary>
    /// global 的摘要说明。
    /// </summary>
    public class ConnectionInstance
    {
        private ICommonDAO Conn = DataFactory.GetInstance();
        private DataTable _FileValue;

        public ConnectionInstance()
        {
            //获取配置文件中的数据库连接
            DataConfig.DefaultConnString = ConfigurationManager.AppSettings["StrConn"].ToString();
        }

        public DataTable FileValue
        {
            get
            {
                return _FileValue;
            }

        }

        //#region 数据库操作 - Insert
        //public void dbInsert(string strSql)
        //{
        //    try
        //    {
        //        Conn.CommandString = strSql;
        //        Conn.RunSql();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        LoggerHelper.WriteLog(ex.GetType(),"");
        //    }
            
        //    Conn.Close();

        //}
        //#endregion





        public void dbRecord(string tblName, string strGetFields,
                            string strColumn, string fldName, int PageSize, int PageIndex, sbyte doCount, sbyte OrderType, string strWhere)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = "PageSplit_in";
            Conn.IsStoredProcedure = true;
            Hashtable hs = new Hashtable();

            hs.Add("@tblName", tblName);            // 表名
            hs.Add("@strGetFields", strGetFields);// 需要返回的列 
            hs.Add("@strColumn", strColumn);        // 唯一的ID
            hs.Add("@fldName", fldName);            // 排序的字段名
            hs.Add("@PageSize", PageSize);      // 页尺寸
            hs.Add("@PageIndex", PageIndex);        // 页码
            hs.Add("@doCount", doCount);            // 返回记录总数, 非 0 值则返回
            hs.Add("@OrderType", OrderType);        // 设置排序类型, 非 0 值则降序
            hs.Add("@strWhere", strWhere);      // 查询条件 (注意: 不要加 where)
            Conn.CommandParams = hs;
            int x = Conn.RunSql();
            _FileValue = Conn.GetDataTable();
            Conn.Close();
        }
        public object[] dbproc(string strsql, Hashtable hs, string[] returns)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = strsql;
            Conn.IsStoredProcedure = true;
            Conn.CommandParams = hs;
            object[] obj = Conn.RunSql(returns);
            Conn.Close();
            return obj;
        }

        public int dbRunSql(string strsql, Hashtable hs)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = strsql;
            Conn.IsStoredProcedure = true;
            Conn.CommandParams = hs;
            int obj = Conn.RunSql();
            Conn.Close();
            return obj;
        }
        public void dbQuery(string strSql)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = strSql;
            Conn.Open();
            _FileValue = Conn.GetDataTable();
            Conn.Close();
        }

        public void dbInsert(string strSql)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = strSql;
            Conn.RunSql();
            Conn.Close();

        }

        public int dbdelete(string strSql)
        {
            Conn = DataFactory.GetInstance();
            Conn.CommandString = strSql;
            Conn.Open();
            int result = Conn.RunSql();
            Conn.Close();
            return result;
        }

        public int GetExexScalar(string strSql)
        {
            int k = 0;
            try
            {
                Conn = DataFactory.GetInstance();
                Conn.CommandString = strSql;
                k = (int)Conn.GetScalar();
            }
            catch
            { }
            finally
            {
                Conn.Close();
            }
            return k;
        }

        public string GetRowScalar(string strSql)
        {
            string getStr = string.Empty;
            try
            {
                Conn = DataFactory.GetInstance();
                Conn.CommandString = strSql;
                getStr = Conn.GetScalar().ToString();
            }
            catch
            { }
            finally
            {
                Conn.Close();
            }
            return getStr;
        }

        public DataTable load_Query(string strSql)
        {
            _FileValue = new DataTable();
            Conn = DataFactory.GetInstance();
            Conn.Open();
            SqlCommand cmd = (SqlCommand)Conn.GetCommand();
            cmd.CommandText = strSql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(_FileValue);

            Conn.Close();
            return _FileValue;
        }
    }
}
