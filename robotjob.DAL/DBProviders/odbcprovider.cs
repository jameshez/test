using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// Odbc ��ʵ����
    /// </summary>
    internal class OdbcProvider:DefaultProvider
	{
		public OdbcProvider(string ConnStr)
		{
			this.Conn=new OdbcConnection(ConnStr);
		}
		#region DefaultProvider�� Odbc ʵ����
		public override IDbDataAdapter GetNullDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		protected override void SetParams(IDbCommand pCmd,Hashtable pParams)
		{
			//�������������Զ�������������Ϣ
			OdbcCommandBuilder.DeriveParameters((OdbcCommand)Cmd);
			//��ȡ�������ļ�ֵ�Խ��в�����ֵ
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
