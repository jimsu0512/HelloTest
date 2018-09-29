using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Common
{
    public static class HelperCommon
    {
        //public static string JSONSerializeObject(object oObject)
        //{
        //    return JsonConvert.SerializeObject(oObject);
        //}

        public static List<T> toList<T>(this DataTable dt) where T : class, new()
        {
            List<T> oList = new List<T>();
            Type type = typeof(T);
            string tempName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                oList.Add(t);
            }
            return oList;
        }

        public static T rowToList<T>(this DataRow dr) where T : class, new()
        {
            T oList = new T();
            Type type = typeof(T);
            string tempName = string.Empty;

            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;
                if (dr.Table.Columns.Contains(tempName))
                {
                    if (!pi.CanWrite) continue;
                    object value = dr[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
                oList = t;
            }
            return oList;
        }
    }
}
