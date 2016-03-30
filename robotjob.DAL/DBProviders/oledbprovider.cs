using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// OleDb ��ʵ����
    /// </summary>
    internal class OleDbProvider:DefaultProvider
	{
		public OleDbProvider(string ConnStr)
		{
			this.Conn=new OleDbConnection(ConnStr);
		}
		#region DefaultProvider�� OleDb ʵ����
		public override IDbDataAdapter GetNullDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		protected override void SetParams(IDbCommand pCmd,Hashtable pParams)
		{
			//�������������Զ�������������Ϣ
			OleDbCommandBuilder.DeriveParameters((OleDbCommand)Cmd);
			//��ȡ�������ļ�ֵ�Խ��в�����ֵ
			IDictionaryEnumerator m_Params=pParams.GetEnumerator();
			while(m_Params.MoveNext())
			{
				((OleDbCommand)pCmd).Parameters[m_Params.Key.ToString()].Value=m_Params.Value;
			}
		}

		protected override object[] SetCommand(string[] SqlReturn)
		{
			OleDbCommand scmd=(OleDbCommand)(Cmd);
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
			OleDbCommand scmd=(OleDbCommand)(Cmd);
			OleDbDataAdapter da = new OleDbDataAdapter(scmd);
			da.Fill(dt);
			return dt;
		}

		protected override object[] SetCommandPro(ref DataTable dt,string[] SqlReturn)
		{
			OleDbCommand scmd=(OleDbCommand)(Cmd);
			object[] Temp=new object[SqlReturn.Length];
			OleDbDataAdapter da = new OleDbDataAdapter(scmd);
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
