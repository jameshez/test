using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace robotjob.Common
{
    /// <summary>
    /// 此类通过反射的方式来填写项，比较花时间，个人建议之后直接通过SQL语法中强制定义位置来匹配
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlDataReaderToModel<T> where T : class, new()
    {
        public T DoTransferType(IDataReader dr)
        {
            T model = new T();
            int count = dr.FieldCount;

            PropertyInfo[] property_lst = model.GetType().GetProperties();
            foreach (PropertyInfo property in property_lst)
            {
                for (int i = 0; i < count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))//判断值是否为空  
                    {
                        string name = dr.GetName(i).ToUpper();//字段名  
                        if (name.Equals(property.Name.ToUpper()))//判断字段名是否和model里的相等  
                        {
                            try
                            {
                                property.SetValue(model, dr.GetValue(i), null);//为model赋值  
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
