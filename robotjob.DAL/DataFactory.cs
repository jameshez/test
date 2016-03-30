using robotjob.DAL.DBProviders;

namespace robotjob.DAL
{
    public class DataFactory
    {
        /// <summary>
        /// 定认构造函数为私有，可以防止用户直接创建工厂实例来生产产品
        /// </summary>
        private DataFactory() { }

        //这样只能通过提供静态方法来生产产品
        /// <summary>
        /// (静态方法)用于向用户提供通用访问接口的实例
        /// </summary>
        /// <param name="pType">需要的提供者类型</param>
        /// <param name="ConnString">连接字符串</param>
        /// <returns>通用访问接口的实例</returns>
        public static ICommonDAO GetInstance(ProviderType pType, string ConnString)
        {
            ICommonDAO dao = null;
            switch ((int)pType)
            {
                default:
                case 1:
                    dao = new SqlProvider(ConnString);
                    break;
                case 2:
                    dao = new OracleProvider(ConnString);
                    break;
                case 3:
                    dao = new OleDbProvider(ConnString);
                    break;
                case 4:
                    dao = new OdbcProvider(ConnString);
                    break;
            }

            return dao;
        }

        /// <summary>
        /// 默认数据库连接方式
        /// </summary>
        /// <param name="ConnString">连接字符串</param>
        /// <returns></returns>
        public static ICommonDAO GetInstance(string ConnString)
        {
            return GetInstance(DataConfig.DefaultProviderType, ConnString);
        }

        /// <summary>
        /// 默认数据库连接方式
        /// </summary>
        /// <returns></returns>
        public static ICommonDAO GetInstance()
        {
            return GetInstance(DataConfig.DefaultProviderType, DataConfig.DefaultConnString);
        }
    }
}
