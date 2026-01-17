using System;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.DBUtility;



namespace ZWL.Common
{
    /// <summary>
    /// 对象处理类
    /// Author：关健鹏
    /// 2017年 *..
    /// </summary>
    public class Pojo
    {
        /// <summary>
        /// 获取对象的属性名返回string数组
        /// <param name="t">传入对象.</param>
        /// </summary>
        public static string[] GetProperties<T>(T t)
        {
            string[] ret = null;

            if (t == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return null;
            }
            ret = new string[properties.Length];
            int i=0;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;                                                  //实体类字段名称
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                   
                    ret[i]=name;        //在此可转换value的类型
                    i++;
                }
            }

            return ret;
        }

        /// <summary>
        /// 得到一个对象列表
        /// <param name="sql">传入sql语句.</param>
        /// </summary>
        public static T[] GetModelList<T>(string sql)
        {
            DataSet ds = DBUtility.DbHelperSQL.GetDataSet(sql);
            return GetModelList<T>(ds);
        }

        /// <summary>
        /// 得到一个对象列表
        ///  /// <param name="ds">传入一个DATASET.</param>
        /// </summary>
        public static T[] GetModelList<T>(DataSet ds)
        {
            T[] m = new T[ds.Tables[0].Rows.Count];
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    m[i] =System.Activator.CreateInstance<T>();
                    var type = m[i].GetType();
                    var method=type.GetMethod("GetModel", new Type[] { typeof(int) });
                    int id = int.Parse(ds.Tables[0].Rows[i]["ID"].ToString());
                    Object[] parametors = new Object[] { id };
                    method.Invoke(m[i], parametors);
                }
                return m;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象
        ///<param name="id">传入一个整型id.</param>
        /// </summary>
        public static T GetModel<T>(int id)
        {
            T m = Activator.CreateInstance<T>();
            var type = m.GetType();
            var method = type.GetMethod("GetModel", new Type[] { typeof(int) });
            Object[] parametors = new Object[] { id };
            method.Invoke(m, parametors);
            return m;
        }
         /// <summary>
        /// 增加一组数据
        ///<param name="t">传入一个实例数组.</param>
        /// </summary>
        public static void GetModelList<T>(T[] t)
        {

            if (t.Length > 0)
            {
                for (int i = 0; i < t.Length; i++)
                {
                    t[i] = System.Activator.CreateInstance<T>();
                    var type = t[i].GetType();
                    var method = type.GetMethod("Add");
                    method.Invoke(t[i], null);
                }

            }
        }
        /// <summary>
        /// 获取该对象的所有属性值并设置到相应textbox
        ///<param name="t">传入一个实例数组.</param>
        /// ///<param name="b">传入一个wc.</param>
        /// </summary>
        public static void SetText<T,B>(T t,B b)
        {
            string[] f1=GetProperties<T>(t);
            for(int i=0;i<f1.Length;i++){
                b.GetType().GetProperty("txt" + f1[i]).SetValue(b, t.GetType().GetProperty(f1[i]).GetValue(t, null), null);
            }
        }
        /// <summary>
        /// 获取相应textbox并设置到该对象的属性值
       ///<param name="b">传入一个wc.</param>
        /// </summary>
        public static T SetField<T,B>(T t,B b)
        {

            string[] f1 = GetProperties<T>(t);


            Type type = typeof(TextBox);
            FieldInfo[] infos = b.GetType().GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < f1.Length; i++)
            {
                string fieldstr="";
                for (int k = 0; k < infos.Length; k++)
                {
                    if (infos[k].FieldType.Name == type.Name)
                    {
                        if (((TextBox)infos[k].GetValue(b)).ID == "txt" + f1[i])
                            fieldstr = ((TextBox)infos[k].GetValue(b)).Text;
                    }
                }
                if (fieldstr!="")
                {
                   
                    if (t.GetType().GetProperty(f1[i]).PropertyType == typeof(string))
                    {
                        t.GetType().GetProperty(f1[i]).SetValue(t, fieldstr == null ? "" : fieldstr, null);
                    }
                    else if (t.GetType().GetProperty(f1[i]).PropertyType == typeof(decimal))
                    {
                        t.GetType().GetProperty(f1[i]).SetValue(t, fieldstr == null ? 0.00M : decimal.Parse(fieldstr), null);
                    }
                    else if (t.GetType().GetProperty(f1[i]).PropertyType == typeof(float))
                    {
                        t.GetType().GetProperty(f1[i]).SetValue(t, fieldstr == null ? 0 : float.Parse(fieldstr), null);
                    }
                    else if (t.GetType().GetProperty(f1[i]).PropertyType == typeof(double))
                    {
                        t.GetType().GetProperty(f1[i]).SetValue(t, fieldstr == null ? 0 : double.Parse(fieldstr), null);
                    }
                    else if (t.GetType().GetProperty(f1[i]).PropertyType == typeof(int))
                    {
                        t.GetType().GetProperty(f1[i]).SetValue(t, fieldstr == null ? 0 : int.Parse(fieldstr), null);
                    }
                }
            }
            return t;
        }

        public static DataSet GetTable(string tablename, string top, string where)
        {
            var SqlString = "SELECT " + top + " * FROM " + tablename;
            if (where.Trim() != "")
            {
                SqlString += " where " + where;
            }
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(SqlString);
            return ds;
        }

        public static DataSet GetTable(string tablename, string top, string where, string order)
        {
            var SqlString = "SELECT " + top + " * FROM " + tablename;
            if (where.Trim() != "")
            {
                SqlString += " where " + where;
            }
            if (order.Trim() != "")
            {
                SqlString += " order by " + order;
            }
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(SqlString);
            return ds;
        }

        public static DataSet GetColnameTable(string colname , string tablename, string where, string order)
        {
            var SqlString = "SELECT " + colname + "  FROM " + tablename;
            if (where.Trim() != "")
            {
                SqlString += " where " + where;
            }
            if (order.Trim() != "")
            {
                SqlString += " order by " + order;
            }
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(SqlString);
            return ds;
        }

        public static DataSet GetTablePager(string tablename, string startindex,string endindex, string querysql, string order)
        {
            var SqlString = " SELECT * from (";
            SqlString += " SELECT ROW_NUMBER() over(order by " + order + ") row,* FROM " + tablename;
            if (querysql.Trim() != "")
            {
                SqlString += " where " + querysql;
            }
            SqlString += ")t where row between " + startindex + " and  " + endindex; ;
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(SqlString);
            return ds;
        }

        public static DataSet GetTablePager(string colname, string tablename, string startindex, string endindex, string querysql, string order)
        {
            var SqlString = " SELECT * from (";
            SqlString += " SELECT ROW_NUMBER() over(order by " + order + ") row,"+ colname + " FROM " + tablename;
            if (querysql.Trim() != "")
            {
                SqlString += " where " + querysql;
            }
            SqlString += ")t where row between " + startindex + " and  " + endindex; ;
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(SqlString);
            return ds;
        }

    }

}
