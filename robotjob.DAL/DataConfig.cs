namespace robotjob.DAL
{
    public class DataConfig
    {
        private DataConfig() { }
        /// <summary>
        /// 默认数据库连接方式
        /// </summary>
        public static ProviderType DefaultProviderType = ProviderType.Sql;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string DefaultConnString = "";
    }
}
