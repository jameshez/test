using System.Data;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// 通用数据访问接口的默认实现类
    /// </summary>
    internal abstract class DefaultProvider:ICommonDAO
	{
		//下面定义三个保护变量，由子类实例
		/// <summary>
		/// 连接对象变量，由子类实例
		/// </summary>
		protected IDbConnection		Conn=null;
		/// <summary>
		/// 命令对象变量，由连接对象生成
		/// </summary>
		protected IDbCommand		Cmd;
		/// <summary>
		/// 事务对象变量，由连接对象生成
		/// </summary>
		protected IDbTransaction	Trans;

		//下面的三个变量用于维持相应属性值
		private string		m_CommandString		="";
		private Hashtable	m_CommandParams		=null;
		private bool		m_IsStoredProcedure	=false;

		#region ICommonDAO 成员

		#region //命令字符串属性
		/// <summary>
		/// 命令字符串属性
		/// </summary>
		public string CommandString
		{
			get{return this.m_CommandString;}
			set{this.m_CommandString=value;}
		}
		#endregion 

		#region //命令参数集合属性
		/// <summary>
		/// 命令参数集合属性
		/// </summary>
		public Hashtable CommandParams
		{
			get{return this.m_CommandParams;}
			set{this.m_CommandParams=value;}
		}
		#endregion 

		#region //是否是存储过程属性
		/// <summary>
		/// 是否是存储过程属性
		/// </summary>
		public bool IsStoredProcedure
		{
			get{return this.m_IsStoredProcedure;}
			set{this.m_IsStoredProcedure=value;}
		}
		#endregion 

		#region //开始事务BeginTrans()
		/// <summary>
		/// 开始事务
		/// </summary>
		public void BeginTrans()
		{
			this.Open();
			Trans=Conn.BeginTransaction();
			Cmd.Transaction=Trans;
		}
		#endregion 

		#region //提交事务CommitTrans()
		/// <summary>
		/// 提交事务
		/// </summary>
		public void CommitTrans()
		{
			Trans.Commit();
		}
		#endregion 

		#region //回滚事务RollbackTrans()
		/// <summary>
		/// 回滚事务
		/// </summary>
		public void RollbackTrans()
		{
			Trans.Rollback();
		}
		#endregion 

		#region //打开数据库的连接，得到命令对象Open()
		/// <summary>
		/// 打开数据库的连接，得到命令对象
		/// </summary>
		public void Open()
		{
			if(Conn.State==ConnectionState.Closed)
			{
				Conn.Open();
				Cmd=Conn.CreateCommand();
			}
		}
		#endregion 

		#region //关闭数据连接Close()
		/// <summary>
		/// 关闭数据连接
		/// </summary>
		public void Close()
		{
			if(Conn.State!=ConnectionState.Closed)
				Conn.Close();
		}
		#endregion 

		#region //得到数据库连接对象GetConnection()
		/// <summary>
		/// 得到数据库连接对象
		/// </summary>
		/// <returns>连接实例</returns>
		public IDbConnection GetConnection()
		{
			this.Open();
			return Conn;
		}
		#endregion 

		#region //得到打开连接的命令对象GetCommand()
		/// <summary>
		/// 得到打开连接的命令对象
		/// </summary>
		/// <returns>命令实例</returns>
		public IDbCommand GetCommand()
		{
			this.Open();
			return Cmd;
		}
		#endregion 

		#region //（抽象方法）得到空数据适配对象，由于数据适配对象与具体实现有关，由子类实现GetNullDataAdapter()
		/// <summary>
		/// （抽象方法）得到空数据适配对象，由于数据适配对象与具体实现有关，由子类实现
		/// </summary>
		/// <returns>空数据适配实例</returns>
		public abstract IDbDataAdapter GetNullDataAdapter();
		#endregion 

		#region //（抽象方法）把参数填入命令对象，由于不同子类操作是不同的，由子类实现SetParams()
		/// <summary>
		/// （抽象方法）把参数填入命令对象，由于不同子类操作是不同的，由子类实现
		/// </summary>
		/// <param name="pCmd"></param>
		/// <param name="pParams"></param>
		protected abstract void SetParams(IDbCommand pCmd,Hashtable pParams);

		#endregion 
	
		#region //（抽象方法）执行带返回值的存储过程“object[]”，由于不同子类操作是不同的，由子类实现SetCommand()
		/// <summary>
		/// （抽象方法）执行带返回值的存储过程，由于不同子类操作是不同的，由子类实现
		/// </summary>
		/// <param name="SqlReturn"></param>
		/// <returns></returns>
		protected abstract object[] SetCommand(string[] SqlReturn);
		#endregion

		#region //（抽象方法）执行带返回值的存储过程“DataTable”，由于不同子类操作是不同的，由子类实现SetCommandPro()
		/// <summary>
		/// （抽象方法）执行带返回值的存储过程“DataTable”，由于不同子类操作是不同的，由子类实现
		/// </summary>
		/// <returns></returns>
		protected abstract DataTable SetCommandPro();
		#endregion 

		#region //（抽象方法）执行带返回值的存储过程“object[]、DataTable”，由于不同子类操作是不同的，由子类实现SetCommandPro()
		/// <summary>
		/// （抽象方法）执行带返回值的存储过程“object[]、DataTable”，由于不同子类操作是不同的，由子类实现
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="SqlReturn"></param>
		/// <returns></returns>
		protected abstract object[] SetCommandPro(ref DataTable dt,string[] SqlReturn);
		#endregion

		#region //用命令信息（相关属性值）填充命令对象，准备命令操作FillCommand()
		/// <summary>
		/// 用命令信息（相关属性值）填充命令对象，准备命令操作
		/// </summary>
		private void FillCommand()
		{
			this.Open();
			//填充命令文本
			Cmd.CommandText=m_CommandString;
			//设定命令类型
			if(m_IsStoredProcedure)
				Cmd.CommandType=CommandType.StoredProcedure;
			else
				Cmd.CommandType=CommandType.Text;
			//填充参数
			if(m_CommandParams!=null)
			{
				//把参数填入命令对象，由子类实现
				this.SetParams(Cmd,m_CommandParams);
			}
		}
		#endregion 

		#region //执行命令，返回受影响行数RunSql()
		/// <summary>
		/// 执行命令，返回受影响行数
		/// </summary>
		/// <returns>受影响行数</returns>
		public int RunSql()
		{
			this.FillCommand();
			return Cmd.ExecuteNonQuery();
		}
		#endregion 

		#region //执行命令，存储过程用，执行带返回值的存储过程，返回“object[]”数组, RunSql()
		/// <summary>
		/// 执行命令，存储过程用，执行带返回值的存储过程，返回“object[]”数组
		/// </summary>
		/// <param name="sqlreturn">要返回的参数名</param>
		/// <returns>返回值</returns>
		public object[] RunSql(string[] sqlreturn)
		{
			this.FillCommand();
			return this.SetCommand(sqlreturn);
		}
		#endregion 

		#region //执行命令，存储过程用，执行带返回值的存储过程，返回“DataTable”, RunPro()
		/// <summary>
		/// 执行命令，存储过程用，执行带返回值的存储过程，返回“DataTable”
		/// </summary>
		/// <returns></returns>
		public DataTable RunPro()
		{
			this.FillCommand();
			return this.SetCommandPro();
		}
		#endregion 

		#region //执行命令，存储过程用，执行带返回值的存储过程，返回“object[]、DataTable”, RunPro()
		/// <summary>
		/// 执行命令，存储过程用，执行带返回值的存储过程,返回“object[]、DataTable”
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="sqlreturn"></param>
		/// <returns></returns>
		public object[] RunPro(ref DataTable dt,string[] sqlreturn)
		{
			this.FillCommand();
			return this.SetCommandPro(ref dt,sqlreturn);
		}
		#endregion

		#region //执行命令,得到结果集第一行第一列的值GetScalar()
		/// <summary>
		/// 执行命令,得到结果集第一行第一列的值
		/// </summary>
		/// <returns>结果集第一行第一列的值</returns>
		public object GetScalar()
		{
			this.FillCommand();
			return Cmd.ExecuteScalar();
		}
		#endregion 

		#region //执行命令，得到数据流对象GetDataReader()
		/// <summary>
		/// 执行命令，得到数据流对象
		/// </summary>
		/// <returns>数据流实例</returns>
		public IDataReader GetDataReader()
		{
			this.FillCommand();
			return Cmd.ExecuteReader();
		}
		#endregion 

		#region //执行命令，得到DataSet GetDataSet()
		/// <summary>
		/// 执行命令，得到DataSet
		/// </summary>
		/// <param name="mTableName">表名</param>
		/// <returns>数据集</returns>
		public DataSet GetDataSet(string mTableName)
		{
			//定义空数据集
			DataSet ds=new DataSet();
			//获取空适配对象
			IDbDataAdapter da=this.GetNullDataAdapter();
			//准备命令对象，赋给适配对象的SelectCommand属性
			this.FillCommand();
			da.SelectCommand=Cmd;
			//实现填充
			da.Fill(ds);
			//如果有名字，取名
			if(mTableName!=null)
				ds.Tables[0].TableName=mTableName;

			return ds;
		}
		#endregion 

		#region //执行命令，得到DataSet  GetDataSet()
		/// <summary>
		/// 执行命令，得到DataSet
		/// </summary>
		/// <returns>数据集</returns>
		public DataSet GetDataSet()
		{
			return this.GetDataSet(null);
		}
		#endregion 

		#region //执行命令，得到DataTable GetDataTable()
		/// <summary>
		/// 执行命令，得到DataTable
		/// </summary>
		/// <param name="mTableName">表名</param>
		/// <returns>数据表</returns>
		public DataTable GetDataTable(string mTableName)
		{
//			return this.GetDataSet(mTableName).Tables[0];
			//定义空数据集
			DataTable dt=new DataTable();
			DataSet ds=new DataSet();
			//获取空适配对象
			IDbDataAdapter da=this.GetNullDataAdapter();
			//准备命令对象，赋给适配对象的SelectCommand属性
			this.FillCommand();
			da.SelectCommand=Cmd;
			//实现填充
			da.Fill(ds);
			dt=ds.Tables[0];
			//移除DS中的母表
			ds.Tables.RemoveAt(0);
			//如果有名字，取名
			if(mTableName!=null)
				dt.TableName=mTableName;

			return dt;
		}
		#endregion 

		#region //执行命令，得到DataTable  GetDataTable()
		/// <summary>
		/// 执行命令，得到DataTable
		/// </summary>
		/// <returns>数据表</returns>
		public DataTable GetDataTable()
		{
			return this.GetDataTable(null);
		}
		#endregion 

		#endregion
	}
}
