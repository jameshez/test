using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// Odbc 的实现类
    /// </summary>
    internal class OdbcProvider:DefaultProvider
	{
		public OdbcProvider(string ConnStr)
		{
			this.Conn=new OdbcConnection(ConnStr);
		}
		#region DefaultProvider的 Odbc 实现类
		public override IDbDataAdapter GetNullDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		protected override void SetParams(IDbCommand pCmd,Hashtable pParams)
		{
			//用命令生成器自动填充命令参数信息
			OdbcCommandBuilder.DeriveParameters((OdbcCommand)Cmd);
			//获取参数集的键值对进行参数赋值
			IDictionaryEnumerator m_Params=pParams.GetEnumerator();
			while(m_Params.MoveNext())
			{
				((OdbcCommand)pCmd).Parameters[m_Params.Key.ToString()].Value=m_Params.Value;
			}
		}

		protected override object[] SetCommand(string[] SqlReturn)
		{
			OdbcCommand scmd=(OdbcCommand)(Cmd);
			object[] Temp=new object[SqlReturn.Length];
			if(scmd.ExecuteNonQuery()!=0)
			{
				for(int i=0;i<SqlReturn.Length;i++)
				{
					Temp[i]=scmd.Parameters[SqlReturn[i]].Value;
				}
			}
			return Temp;
		}

		protected override DataTable SetCommandPro()
		{
			DataTable dt=new DataTable();
			OdbcCommand scmd=(OdbcCommand)(Cmd);
			OdbcDataAdapter da = new OdbcDataAdapter(scmd);
			da.Fill(dt);
			return dt;
		}

		protected override object[] SetCommandPro(ref DataTable dt,string[] SqlReturn)
		{
			OdbcCommand scmd=(OdbcCommand)(Cmd);
			object[] Temp=new object[SqlReturn.Length];
			OdbcDataAdapter da = new OdbcDataAdapter(scmd);
			da.Fill(dt);
			for(int i=0;i<SqlReturn.Length;i++)
			{
				Temp[i]=scmd.Parameters[SqlReturn[i]].Value;
			}
			
			return Temp;
		}

		#endregion
	}
}
