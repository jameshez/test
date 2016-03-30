using System.Data;
using System.Collections;

namespace robotjob.DAL.DBProviders
{
    /// <summary>
    /// ͨ�����ݷ��ʽӿڵ�Ĭ��ʵ����
    /// </summary>
    internal abstract class DefaultProvider:ICommonDAO
	{
		//���涨����������������������ʵ��
		/// <summary>
		/// ���Ӷ��������������ʵ��
		/// </summary>
		protected IDbConnection		Conn=null;
		/// <summary>
		/// �����������������Ӷ�������
		/// </summary>
		protected IDbCommand		Cmd;
		/// <summary>
		/// �����������������Ӷ�������
		/// </summary>
		protected IDbTransaction	Trans;

		//�����������������ά����Ӧ����ֵ
		private string		m_CommandString		="";
		private Hashtable	m_CommandParams		=null;
		private bool		m_IsStoredProcedure	=false;

		#region ICommonDAO ��Ա

		#region //�����ַ�������
		/// <summary>
		/// �����ַ�������
		/// </summary>
		public string CommandString
		{
			get{return this.m_CommandString;}
			set{this.m_CommandString=value;}
		}
		#endregion 

		#region //���������������
		/// <summary>
		/// ���������������
		/// </summary>
		public Hashtable CommandParams
		{
			get{return this.m_CommandParams;}
			set{this.m_CommandParams=value;}
		}
		#endregion 

		#region //�Ƿ��Ǵ洢��������
		/// <summary>
		/// �Ƿ��Ǵ洢��������
		/// </summary>
		public bool IsStoredProcedure
		{
			get{return this.m_IsStoredProcedure;}
			set{this.m_IsStoredProcedure=value;}
		}
		#endregion 

		#region //��ʼ����BeginTrans()
		/// <summary>
		/// ��ʼ����
		/// </summary>
		public void BeginTrans()
		{
			this.Open();
			Trans=Conn.BeginTransaction();
			Cmd.Transaction=Trans;
		}
		#endregion 

		#region //�ύ����CommitTrans()
		/// <summary>
		/// �ύ����
		/// </summary>
		public void CommitTrans()
		{
			Trans.Commit();
		}
		#endregion 

		#region //�ع�����RollbackTrans()
		/// <summary>
		/// �ع�����
		/// </summary>
		public void RollbackTrans()
		{
			Trans.Rollback();
		}
		#endregion 

		#region //�����ݿ�����ӣ��õ��������Open()
		/// <summary>
		/// �����ݿ�����ӣ��õ��������
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

		#region //�ر���������Close()
		/// <summary>
		/// �ر���������
		/// </summary>
		public void Close()
		{
			if(Conn.State!=ConnectionState.Closed)
				Conn.Close();
		}
		#endregion 

		#region //�õ����ݿ����Ӷ���GetConnection()
		/// <summary>
		/// �õ����ݿ����Ӷ���
		/// </summary>
		/// <returns>����ʵ��</returns>
		public IDbConnection GetConnection()
		{
			this.Open();
			return Conn;
		}
		#endregion 

		#region //�õ������ӵ��������GetCommand()
		/// <summary>
		/// �õ������ӵ��������
		/// </summary>
		/// <returns>����ʵ��</returns>
		public IDbCommand GetCommand()
		{
			this.Open();
			return Cmd;
		}
		#endregion 

		#region //�����󷽷����õ����������������������������������ʵ���йأ�������ʵ��GetNullDataAdapter()
		/// <summary>
		/// �����󷽷����õ����������������������������������ʵ���йأ�������ʵ��
		/// </summary>
		/// <returns>����������ʵ��</returns>
		public abstract IDbDataAdapter GetNullDataAdapter();
		#endregion 

		#region //�����󷽷����Ѳ�����������������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��SetParams()
		/// <summary>
		/// �����󷽷����Ѳ�����������������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��
		/// </summary>
		/// <param name="pCmd"></param>
		/// <param name="pParams"></param>
		protected abstract void SetParams(IDbCommand pCmd,Hashtable pParams);

		#endregion 
	
		#region //�����󷽷���ִ�д�����ֵ�Ĵ洢���̡�object[]�������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��SetCommand()
		/// <summary>
		/// �����󷽷���ִ�д�����ֵ�Ĵ洢���̣����ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��
		/// </summary>
		/// <param name="SqlReturn"></param>
		/// <returns></returns>
		protected abstract object[] SetCommand(string[] SqlReturn);
		#endregion

		#region //�����󷽷���ִ�д�����ֵ�Ĵ洢���̡�DataTable�������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��SetCommandPro()
		/// <summary>
		/// �����󷽷���ִ�д�����ֵ�Ĵ洢���̡�DataTable�������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected abstract DataTable SetCommandPro();
		#endregion 

		#region //�����󷽷���ִ�д�����ֵ�Ĵ洢���̡�object[]��DataTable�������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��SetCommandPro()
		/// <summary>
		/// �����󷽷���ִ�д�����ֵ�Ĵ洢���̡�object[]��DataTable�������ڲ�ͬ��������ǲ�ͬ�ģ�������ʵ��
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="SqlReturn"></param>
		/// <returns></returns>
		protected abstract object[] SetCommandPro(ref DataTable dt,string[] SqlReturn);
		#endregion

		#region //��������Ϣ���������ֵ������������׼���������FillCommand()
		/// <summary>
		/// ��������Ϣ���������ֵ������������׼���������
		/// </summary>
		private void FillCommand()
		{
			this.Open();
			//��������ı�
			Cmd.CommandText=m_CommandString;
			//�趨��������
			if(m_IsStoredProcedure)
				Cmd.CommandType=CommandType.StoredProcedure;
			else
				Cmd.CommandType=CommandType.Text;
			//������
			if(m_CommandParams!=null)
			{
				//�Ѳ��������������������ʵ��
				this.SetParams(Cmd,m_CommandParams);
			}
		}
		#endregion 

		#region //ִ�����������Ӱ������RunSql()
		/// <summary>
		/// ִ�����������Ӱ������
		/// </summary>
		/// <returns>��Ӱ������</returns>
		public int RunSql()
		{
			this.FillCommand();
			return Cmd.ExecuteNonQuery();
		}
		#endregion 

		#region //ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢���̣����ء�object[]������, RunSql()
		/// <summary>
		/// ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢���̣����ء�object[]������
		/// </summary>
		/// <param name="sqlreturn">Ҫ���صĲ�����</param>
		/// <returns>����ֵ</returns>
		public object[] RunSql(string[] sqlreturn)
		{
			this.FillCommand();
			return this.SetCommand(sqlreturn);
		}
		#endregion 

		#region //ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢���̣����ء�DataTable��, RunPro()
		/// <summary>
		/// ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢���̣����ء�DataTable��
		/// </summary>
		/// <returns></returns>
		public DataTable RunPro()
		{
			this.FillCommand();
			return this.SetCommandPro();
		}
		#endregion 

		#region //ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢���̣����ء�object[]��DataTable��, RunPro()
		/// <summary>
		/// ִ������洢�����ã�ִ�д�����ֵ�Ĵ洢����,���ء�object[]��DataTable��
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

		#region //ִ������,�õ��������һ�е�һ�е�ֵGetScalar()
		/// <summary>
		/// ִ������,�õ��������һ�е�һ�е�ֵ
		/// </summary>
		/// <returns>�������һ�е�һ�е�ֵ</returns>
		public object GetScalar()
		{
			this.FillCommand();
			return Cmd.ExecuteScalar();
		}
		#endregion 

		#region //ִ������õ�����������GetDataReader()
		/// <summary>
		/// ִ������õ�����������
		/// </summary>
		/// <returns>������ʵ��</returns>
		public IDataReader GetDataReader()
		{
			this.FillCommand();
			return Cmd.ExecuteReader();
		}
		#endregion 

		#region //ִ������õ�DataSet GetDataSet()
		/// <summary>
		/// ִ������õ�DataSet
		/// </summary>
		/// <param name="mTableName">����</param>
		/// <returns>���ݼ�</returns>
		public DataSet GetDataSet(string mTableName)
		{
			//��������ݼ�
			DataSet ds=new DataSet();
			//��ȡ���������
			IDbDataAdapter da=this.GetNullDataAdapter();
			//׼��������󣬸�����������SelectCommand����
			this.FillCommand();
			da.SelectCommand=Cmd;
			//ʵ�����
			da.Fill(ds);
			//��������֣�ȡ��
			if(mTableName!=null)
				ds.Tables[0].TableName=mTableName;

			return ds;
		}
		#endregion 

		#region //ִ������õ�DataSet  GetDataSet()
		/// <summary>
		/// ִ������õ�DataSet
		/// </summary>
		/// <returns>���ݼ�</returns>
		public DataSet GetDataSet()
		{
			return this.GetDataSet(null);
		}
		#endregion 

		#region //ִ������õ�DataTable GetDataTable()
		/// <summary>
		/// ִ������õ�DataTable
		/// </summary>
		/// <param name="mTableName">����</param>
		/// <returns>���ݱ�</returns>
		public DataTable GetDataTable(string mTableName)
		{
//			return this.GetDataSet(mTableName).Tables[0];
			//��������ݼ�
			DataTable dt=new DataTable();
			DataSet ds=new DataSet();
			//��ȡ���������
			IDbDataAdapter da=this.GetNullDataAdapter();
			//׼��������󣬸�����������SelectCommand����
			this.FillCommand();
			da.SelectCommand=Cmd;
			//ʵ�����
			da.Fill(ds);
			dt=ds.Tables[0];
			//�Ƴ�DS�е�ĸ��
			ds.Tables.RemoveAt(0);
			//��������֣�ȡ��
			if(mTableName!=null)
				dt.TableName=mTableName;

			return dt;
		}
		#endregion 

		#region //ִ������õ�DataTable  GetDataTable()
		/// <summary>
		/// ִ������õ�DataTable
		/// </summary>
		/// <returns>���ݱ�</returns>
		public DataTable GetDataTable()
		{
			return this.GetDataTable(null);
		}
		#endregion 

		#endregion
	}
}
