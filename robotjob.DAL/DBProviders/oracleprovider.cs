using System.Data;
using System.Data.OracleClient;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// Oracle ��ʵ���ࡣ
    /// </summary>
    internal class OracleProvider:DefaultProvider
	{
		public OracleProvider(string ConnStr)
		{
			this.Conn=new OracleConnection(ConnStr);
		}
		#region DefaultProvider�� Oracle ʵ����
		public override IDbDataAdapter GetNullDataAdapter()
		{
			return new OracleDataAdapter();
		}

		protected override void SetParams(IDbCommand pCmd,Hashtable pParams)
		{
			//�������������Զ�������������Ϣ
			OracleCommandBuilder.DeriveParameters((OracleCommand)Cmd);
			//��ȡ�������ļ�ֵ�Խ��в�����ֵ
			IDictionaryEnumerator m_Params=pParams.GetEnumerator();
			while(m_Params.MoveNext())
			{
				((OracleCommand)pCmd).Parameters[m_Params.Key.ToString()].Value=m_Params.Value;
			}
		}

		protected override object[] SetCommand(string[] SqlReturn)
		{
			OracleCommand scmd=(OracleCommand)(Cmd);
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
			OracleCommand scmd=(OracleCommand)(Cmd);
			OracleDataAdapter da = new OracleDataAdapter(scmd);
			da.Fill(dt);
			return dt;
		}

		protected override object[] SetCommandPro(ref DataTable dt,string[] SqlReturn)
		{
			OracleCommand scmd=(OracleCommand)(Cmd);
			object[] Temp=new object[SqlReturn.Length];
			OracleDataAdapter da = new OracleDataAdapter(scmd);
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
