using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace robotjob.DALFactory
{
    public class DataAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["SQLDALPath"];

        private DataAccess(){ }

        /// <summary>
        /// 实例化 User DAL
        /// </summary>
        /// <returns></returns>
        public static IDAL.IUser CreateUser()
        {
            string className = path + ".User";
            return (IDAL.IUser)Assembly.Load(path).CreateInstance(className);
        }
    }
}
