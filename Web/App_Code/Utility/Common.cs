using Newtonsoft.Json;
using NPOI.HPSF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using ZWL.Common;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{
    public static T ConverToTEntity<T>(object value)
    {
        Type t = typeof(T);
        PropertyInfo[] t_propinfos = t.GetProperties();
        T obj = (T)t.Assembly.CreateInstance(t.FullName);
        if (value != null)
        {
            Type val_t = value.GetType();

            PropertyInfo[] val_t_propinfos = val_t.GetProperties();

            foreach (PropertyInfo vp in val_t_propinfos)
            {
                PropertyInfo tp = t_propinfos.FirstOrDefault(m => m.Name == vp.Name);
                if (tp != null && tp.CanWrite)
                {
                    object val = vp.GetValue(value, null);
                    tp.SetValue(obj, DataTableHelper.ChangeType(val, tp.PropertyType), null);
                }
            }
        }
        return obj;
    }
    
}