using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// Sql Server 的实现类
    /// </summary>
    internal class SqlProvider : DefaultProvider
	{
		public SqlProvider(string ConnStr)
		{
			this.Conn=new SqlConnection(ConnStr);
		}
		#region DefaultProvider的 Sql 实现类
		public override IDbDataAdapter GetNullDataAdapter()
		{
			return new SqlDataAdapter();
		}

		protected override void SetParams(IDbCommand pCmd,Hashtable pParams)
		{
			//用命令生成器自动填充命令参数信息
			SqlCommandBuilder.DeriveParameters((SqlCommand)Cmd);
			//获取参数集的键值对进行参数赋值
			IDictionaryEnumerator m_Params=pParams.GetEnumerator();
			while(m_Params.MoveNext())
			{
				((SqlCommand)pCmd).Parameters[m_Params.Key.ToString()].Value=m_Params.Value;
			}
		}

		protected override object[] SetCommand(string[] SqlReturn)
		{
			SqlCommand scmd=(SqlCommand)(Cmd);
            object[] Temp = new object[SqlReturn.Length];

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
			SqlCommand scmd=(SqlCommand)(Cmd);
			SqlDataAdapter da = new SqlDataAdapter(scmd);
			da.Fill(dt);
			return dt;
		}
		
		protected override object[] SetCommandPro(ref DataTable dt,string[] SqlReturn)
		{
			SqlCommand scmd=(SqlCommand)(Cmd);
			object[] Temp=new object[SqlReturn.Length];
			SqlDataAdapter da = new SqlDataAdapter(scmd);
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
