using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace robotjob.Common
{
    /// <summary>
    /// 此类通过反射的方式来填写项，比较花时间，个人建议之后直接通过SQL语法中强制定义位置来匹配，反射性能损耗比较大，前期可以简单运用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SqlDataReaderToModel<T> where T : class, new()
    {
        public static T DoTransferType(IDataReader dr)
        {
            T model = new T();
            int count = dr.FieldCount;

            PropertyInfo[] property_lst = model.GetType().GetProperties();
            foreach (PropertyInfo property in property_lst)
            {
                for (int i = 0; i < count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string name = dr.GetName(i).ToUpper();
                        if (name.Equals(property.Name.ToUpper()))  
                        {
                            try
                            {
                                property.SetValue(model, dr.GetValue(i), null);
                                break;
                            }
                            catch { }
                        }
                    }
                }
            }
            return model;
        }
    }
}
