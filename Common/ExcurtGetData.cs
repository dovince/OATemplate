//////////////////////////////////////////////////
/*By : LiuHao
 *Date: 2018.1.10
 *Descript:该类为常用方法类(static)主要实现①从数据库中取出的值对Lable、Image、TextBox、DropDownList等常用服务器控件的自动赋值
 *          ②常用控件对Model属性进行赋值
 *Point Out：①服务器端控件ID必须与数据库属性一致；
 *           ②不要使用RadioButton、CheckBox等需要多个ID的控件，要使用RadioButtonList、CheckBoxList、DropDownList等组合的单选、多选、下拉框控件；
 *           ③该功能不识别HTML；
 * 
 * 改功能尚未经过检验测试，可能会存在问题
 * 
*/
////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;

namespace ZWL.Common
{

    //仅实现了Lable的赋值，后面可继续完善使其可对常用的服务器控件进行赋值
    public class ExcurtGetData
    {
        public static List<Control> myAllCon = new List<Control>();

        private static void GetControls(Control c)
        {
            foreach (Control con in c.Controls)
            {
                if (!con.HasControls())
                {
                    myAllCon.Add(con);
                    continue;
                }
                else
                {
                    //CheckBoxList为复选控件，包含多个子控件，故增加判断是否为CheckBoxList类，解决CheckBoxList识别成CheckBox的问题
                    if (con is CheckBoxList)
                    {
                        myAllCon.Add(con);
                    }
                    else
                    {
                        GetControls(con);
                    }
                }
            }


            //if (c.HasControls() == false)
            //{
            //    myAllCon.Add(c);
            //}
            //if (c.HasControls())
            //{
            //    foreach (Control con in c.Controls)
            //    {
            //        GetControls(con);//函数重载
            //    }
            //}
        }
        public static void forData<T>(T t, Control c)
        {
            forData<T>(t, c, false);
        }
        /// <summary>
        /// 获取Model的属性值并赋值给前台页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">Model</param>
        /// <param name="c">Control页面</param>
        public static void forData<T>(T t, Control c, bool selectedtext)
        {
            //myNewLable = null;
            myAllCon.Clear();
            GetControls(c);//调用该函数为Control的List赋值
            var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (t != null && properties.Length > 0)
            {
                foreach (var item in properties)
                {
                    var name = item.Name;
                    var value = item.GetValue(t, null);
                    #region 循环为不同服务器控件赋值
                    for (int i = 0; i < myAllCon.Count; i++)
                    {
                        #region 服务器控件赋值
                        if (myAllCon[i] is Label)
                        {
                            var myLable = (Label)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    myLable.Text = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is Image)
                        {
                            Image myImage = (Image)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    myImage.ImageUrl = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is TextBox)
                        {
                            TextBox myTextBox = (TextBox)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    myTextBox.Text = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is DropDownList)
                        {
                            DropDownList myDropDownList = (DropDownList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    var val = Convert.ToString(value);
                                    if (selectedtext)
                                    {
                                        var sitem = myDropDownList.Items.FindByText(val);
                                        if (sitem != null)
                                            myDropDownList.SelectedValue = sitem.Value;
                                    }
                                    else
                                    {
                                        var sitem = myDropDownList.Items.FindByValue(val);
                                        if (sitem != null)
                                            myDropDownList.SelectedValue = sitem.Value;
                                    }
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is RadioButtonList)
                        {
                            RadioButtonList myRadioButtonList = (RadioButtonList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    var val = Convert.ToString(value);
                                    if (selectedtext)
                                    {
                                        var sitem = myRadioButtonList.Items.FindByText(val);
                                        if (sitem != null)
                                            myRadioButtonList.SelectedValue = sitem.Value;
                                    }
                                    else
                                    {
                                        myRadioButtonList.SelectedValue = val;
                                    }
                                    //myDropDownList.Text = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }

                        //获取CheckBoxList存在问题
                        else if (myAllCon[i] is CheckBoxList)
                        {
                            CheckBoxList myCheckBoxList = (CheckBoxList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    string[] myData = Convert.ToString(value).Split(',');
                                    for (int j = 0; j < myData.Length; j++)
                                    {
                                        myCheckBoxList.SelectedValue = myData[j];
                                    }

                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is CheckBox)
                        {
                            CheckBox myCheckBox = (CheckBox)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    string[] myData = Convert.ToString(value).Split(',');
                                    for (int j = 0; j < myData.Length; j++)
                                    {
                                        myCheckBox.Checked = true;
                                    }

                                    //myCheckBoxList.SelectedValue = Convert.ToString(value);
                                    //myDropDownList.Text = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        } 
                        #endregion
                    }
                    #endregion
                }
            }
        }


        public static List<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化   
            List<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值   
                        if (pi.Name.Equals(p_Data.Columns[i].ColumnName))
                        {
                            //  DataRowCollection dataRowCollection = new DataRowCollection();  
                            //dataRowCollection.Add(  
                            // p_Data.Columns[i].DataType = pi.GetType();   

                            // 数据库NULL值单独处理   
                            if (p_Data.Rows[j][i] != DBNull.Value)
                            {
                                try { pi.SetValue(_t, p_Data.Rows[j][i], null); }
                                catch { pi.SetValue(_t, int.Parse(p_Data.Rows[j][i].ToString()), null); }
                            }
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        public static void toData<T>(T t, Control c)
        {
            toData<T>(t, c, false, false);
        }

        /// <summary>
        /// 将前台输入数据转换成Model的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">Model</param>
        /// <param name="c">Control页面</param>
        public static void toData<T>(T t, Control c, bool selectedtext, bool defaultdate)
        {
            myAllCon.Clear();
            GetControls(c);//调用该函数为Control的List赋值
            var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (t != null && properties.Length > 0)
            {
                foreach (var item in properties)
                {
                    var name = item.Name;
                    var value = item.GetValue(t, null);
                    #region 循环为不同服务器控件赋值
                    for (int i = 0; i < myAllCon.Count; i++)
                    {
                        #region 服务器控件赋值
                        if (myAllCon[i] is TextBox)
                        {
                            TextBox myTextBox = (TextBox)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    if (item.PropertyType.Name.StartsWith("String"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            item.SetValue(t, null, null);
                                        }
                                        else
                                        {
                                            item.SetValue(t, myTextBox.Text, null);
                                        }
                                    }
                                    else if (item.PropertyType.FullName.Contains("Decimal"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            item.SetValue(t, null, null);
                                        }
                                        else
                                        {
                                            item.SetValue(t, Decimal.Parse(myTextBox.Text), null);
                                        }
                                    }
                                    else if (item.PropertyType.FullName.Contains("Double"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            item.SetValue(t, null, null);
                                        }
                                        else
                                        {
                                            item.SetValue(t, Double.Parse(myTextBox.Text), null);
                                        }
                                    }
                                    else if (item.PropertyType.FullName.Contains("Float"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            item.SetValue(t, null, null);
                                        }
                                        else
                                        {
                                            item.SetValue(t, float.Parse(myTextBox.Text), null);
                                        }
                                    }
                                    else if (item.PropertyType.FullName.Contains("DateTime"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            if (defaultdate)
                                            {
                                                item.SetValue(t, PublicMethod.GetDefaultTime(), null);
                                            }
                                            else
                                            {
                                                item.SetValue(t, null, null);
                                            }
                                        }
                                        else
                                        {
                                            item.SetValue(t, DateTime.Parse(myTextBox.Text), null);
                                        }
                                    }
                                    else if (item.PropertyType.FullName.Contains("Int"))
                                    {
                                        if (string.IsNullOrEmpty(myTextBox.Text.ToString()))
                                        {
                                            item.SetValue(t, null, null);
                                        }
                                        else
                                        {
                                            item.SetValue(t, int.Parse(myTextBox.Text), null);
                                        }
                                    }
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is Image)
                        {
                            Image myImage = (Image)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    item.SetValue(t, myImage.ImageUrl, null);
                                    //myImage.ImageUrl = Convert.ToString(value);
                                }
                                else
                                {
                                    forData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is DropDownList)

                        {
                            DropDownList myDropDownList = (DropDownList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    if (selectedtext)
                                    {
                                        item.SetValue(t, myDropDownList.SelectedItem.Text.ToString(), null);
                                    }
                                    else
                                    {
                                        item.SetValue(t, myDropDownList.SelectedValue.ToString(), null);
                                    }
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is RadioButtonList)
                        {
                            RadioButtonList myRadioButtonList = (RadioButtonList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    if (selectedtext)
                                    {
                                        item.SetValue(t, myRadioButtonList.SelectedItem.Text.ToString(), null);
                                    }
                                    else
                                    {
                                        item.SetValue(t, DataTableHelper.ChangeType(myRadioButtonList.SelectedValue.ToString(), item.PropertyType), null);
                                    }
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        }
                        //获取CheckBoxList存在问题
                        else if (myAllCon[i] is CheckBoxList)
                        {
                            CheckBoxList myCheckBoxList = (CheckBoxList)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {

                                    string myData = string.Empty;
                                    for (int j = 0; j < myCheckBoxList.Items.Count; j++)
                                    {
                                        if (myCheckBoxList.Items[j].Selected)
                                        {
                                            if (selectedtext)
                                            {
                                                myData += myCheckBoxList.Items[j].Text.ToString() + ",";
                                            }
                                            else
                                            {
                                                myData += myCheckBoxList.Items[j].Value.ToString() + ",";
                                            }
                                        }
                                    }
                                    item.SetValue(t, myData.TrimEnd(','), null);
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is CheckBox)
                        {
                            CheckBox myCheckBox = (CheckBox)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {
                                    string[] myData = Convert.ToString(value).Split(',');
                                    for (int j = 0; j < myData.Length; j++)
                                    {
                                        myCheckBox.Checked = true;
                                    }
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        }
                        else if (myAllCon[i] is FileUpload)
                        {
                            FileUpload myFileUpload = (FileUpload)myAllCon[i];
                            if (myAllCon[i].ID.ToString() == name)
                            {
                                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                                {

                                    //string a = myFileUpload.PostedFile.FileName;//本地上传路径
                                    //增加文件存储在服务器中位置的实现代码
                                }
                                else
                                {
                                    toData(value, c);
                                }
                            }
                        } 
                        #endregion
                    }
                    #endregion
                }
            }
        }

        public static void formatDate()
        {
            foreach (var item in myAllCon)
            {
                if (item is System.Web.UI.WebControls.TextBox)
                {
                    var input = ((System.Web.UI.WebControls.TextBox)item);
                    if (input.HasAttributes && input.Text != "")
                    {
                        var bags = PublicMethod.GetPrivateField<System.Web.UI.StateBag>(input.Attributes, "_bag");
                        if (bags != null && bags.Count > 0)
                        {
                            var temp = PublicMethod.GetPrivateField<System.Collections.Specialized.HybridDictionary>(bags, "bag");
                            foreach (System.Collections.DictionaryEntry bg in temp)
                            {
                                if (bg.Key.ToString() == "class")
                                {
                                    var val = PublicMethod.GetPrivateField<string>(bg.Value, "value");
                                    if (val.ToString().Contains("input_cxcalendar") && !string.IsNullOrEmpty(input.Text))
                                    {
                                        var defaultdate = new DateTime();
                                        PublicMethod.GetDefaultTime(out defaultdate);
                                        if (TimeParser.GetFormatDate(DateTime.Parse(input.Text)).Value.Subtract(defaultdate).Days == 0)
                                        {
                                            input.Text = "";
                                        }
                                        else
                                        {
                                            input.Text = TimeParser.GetFormatDateString(input.Text);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
