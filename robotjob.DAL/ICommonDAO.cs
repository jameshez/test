using System.Collections;
using System.Data;

namespace robotjob.DAL
{
    public interface ICommonDAO
    {
        /// <summary>
        /// 命令字符串属性
        /// </summary>
        string CommandString
        {
            get; set;
        }
        /// <summary>
        /// 命令参数集合属性
        /// </summary>
        Hashtable CommandParams
        {
            get; set;
        }
        /// <summary>
        /// 是否是存储过程属性
        /// </summary>
        bool IsStoredProcedure
        {
            get; set;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTrans();
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTrans();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTrans();
        /// <summary>
        /// 打开数据库的连接，得到命令对象
        /// </summary>
        void Open();
        /// <summary>
        /// 关闭数据连接
        /// </summary>
        void Close();
        /// <summary>
        /// 得到数据库连接对象
        /// </summary>
        /// <returns>连接实例</returns>
        IDbConnection GetConnection();
        /// <summary>
        /// 得到打开连接的命令对象
        /// </summary>
        /// <returns>命令实例</returns>
        IDbCommand GetCommand();
        /// <summary>
        /// 得到空数据适配对象
        /// </summary>
        /// <returns>空数据适配实例</returns>
        IDbDataAdapter GetNullDataAdapter();
        /// <summary>
        /// 执行命令，返回受影响行数
        /// </summary>
        /// <returns>受影响行数</returns>
        int RunSql();
        /// <summary>
        /// 执行命令，存储过程用，执行带返回值的存储过程
        /// </summary>
        /// <param name="sqlreturn">返回的参数名称</param>
        /// <returns>返回值</returns>
        object[] RunSql(string[] sqlreturn);
        /// <summary>
        /// 执行命令，存储过程用,执行带返回值的存储过程,返回 string[]、DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sqlreturn"></param>
        /// <returns></returns>
        object[] RunPro(ref DataTable dt, string[] sqlreturn);
        /// <summary>
        /// 执行命令，存储过程用,执行带返回值的存储过程,返回 DataTable
        /// </summary>
        /// <returns></returns>
        DataTable RunPro();
        /// <summary>
        /// 执行命令,得到结果集第一行第一列的值
        /// </summary>
        /// <returns>结果集第一行第一列的值</returns>
        object GetScalar();
        /// <summary>
        /// 执行命令，得到数据流对象
        /// </summary>
        /// <returns>数据流实例</returns>
        IDataReader GetDataReader();
        /// <summary>
        /// 执行命令，得到DataSet
        /// </summary>
        /// <param name="mTableName">表名</param>
        /// <returns>数据集</returns>
        DataSet GetDataSet(string mTableName);
        /// <summary>
        /// 执行命令，得到DataSet
        /// </summary>
        /// <returns>数据集</returns>
        DataSet GetDataSet();
        /// <summary>
        /// 执行命令，得到DataTable
        /// </summary>
        /// <param name="mTableName">表名</param>
        /// <returns>数据表</returns>
        DataTable GetDataTable(string mTableName);
        /// <summary>
        /// 执行命令，得到DataTable
        /// </summary>
        /// <returns>数据表</returns>
        DataTable GetDataTable();
    }
}
