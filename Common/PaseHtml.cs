using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Data;

namespace ZWL.Common
{
    /// <summary>
    /// 解析xml表单的类
    /// </summary>
    public class ParseHtml
    {
        public ParseHtml()
        {
        }
        public ParseHtml(string content)
        {
            HTMLDOC = new HtmlAgilityPack.HtmlDocument();
            HTMLDOC.LoadHtml(content);
        }
        private Hashtable myHastlist = new Hashtable();
        private HtmlAgilityPack.HtmlDocument HTMLDOC;
        public string getValue(string attname)
        {
            string strvalue = "";
            if (this.myHastlist == null || myHastlist.Count <= 0)
            {
                return strvalue;
            }
            else
            {
                return Convert.ToString(myHastlist[attname]);
            }

        }
        public Hashtable GetAttList()
        {
            if (this.myHastlist != null || myHastlist.Count > 0)
            {
                return myHastlist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据元素id获取值
        /// 用来获取textarea中的内容
        /// </summary>
        /// <param name="strElenentid"></param>
        /// <returns></returns>
        public string GetElementByID(string strElenentid)
        {
            string strnodetext = "";
            if (!string.IsNullOrEmpty(strElenentid) && HTMLDOC.DocumentNode != null)//必须是已经加载了html
            {
                HtmlNode singlenode = HTMLDOC.GetElementbyId(strElenentid);
                if (singlenode != null)
                {
                    strnodetext = singlenode.InnerText;
                }
            }
            return strnodetext;
        }

        /// <summary>
        /// 提取所有(intput/select)
        /// </summary>
        /// <param name="strcontent">html</param>
        public void GetAttListFormHTMLall(string strcontent)
        {
            var content = strcontent;
            var namevaluelist = new Hashtable();
            var htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;
            var inputnodelist = htmldoc.DocumentNode.SelectNodes("//input");
            if (inputnodelist != null)
            {
                foreach (HtmlNode inputnode in inputnodelist)
                {
                    HtmlAttributeCollection attlist = inputnode.Attributes;
                    string strattname = "";
                    string strattvalue = "";
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("alt"))
                        {
                            strattname = att.Value;
                        }
                    }
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("value"))
                        {
                            strattvalue = att.Value;
                        }
                    }
                    if (strattname != string.Empty && !namevaluelist.ContainsKey(strattname))
                    {
                        namevaluelist.Add(strattname, strattvalue);
                    }
                }
            }
            string strselect = "";
            string stroption = "";
            if (content.Contains("<select"))
            {
                strselect = "select";
                stroption = "option";
            }
            else if (content.Contains("<SELECT"))
            {
                strselect = "SELECT";
                stroption = "OPTION";
            }
            if (!string.IsNullOrEmpty(strselect) && !string.IsNullOrEmpty(stroption))
            {
                var selectnodelist = htmldoc.DocumentNode.SelectNodes("//select");
                if (selectnodelist != null)
                {
                    foreach (HtmlNode selectnode in selectnodelist)
                    {
                        HtmlAttributeCollection attlist = selectnode.Attributes;
                        HtmlAgilityPack.HtmlDocument htmldocopyion = new HtmlAgilityPack.HtmlDocument();
                        htmldocopyion.LoadHtml(selectnode.InnerHtml);
                        HtmlNodeCollection optionlist = htmldocopyion.DocumentNode.SelectNodes("//option");
                        string strattname = "";
                        string strattvalue = "";
                        //从select的属性中找到该select的名称
                        strattname = selectnode.GetAttributeValue("alt", "");
                        //找到option中被选中的，从被选中的option中找到value
                        foreach (HtmlNode optionnode in optionlist)
                        {
                            HtmlAttributeCollection optionattlist = optionnode.Attributes;
                            bool selected = false;
                            string strselectedvalue = "";
                            foreach (HtmlAttribute att in optionattlist)
                            {
                                if (att.Name.Equals("selected"))
                                {
                                    selected = true;
                                }
                                if (att.Name.Equals("value"))
                                {
                                    strselectedvalue = att.Value;
                                }
                            }
                            if (selected)
                            {
                                strattvalue = strselectedvalue;
                            }
                            if (strattname != string.Empty && strattvalue != string.Empty && !namevaluelist.ContainsKey(strattname))
                            {
                                namevaluelist.Add(strattname, strattvalue);
                                break;
                            }
                        }
                    }
                }
            }
            if (namevaluelist.Count > 0)
            {
                myHastlist = namevaluelist;
            }
        }

        /// <summary>
        /// 提取input
        /// </summary>
        /// <param name="strcontent">html</param>
        public void GetAttListFormHTMLinput(string strcontent)
        {
            string content = strcontent;
            Hashtable namevaluelist = new Hashtable();
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;
            var inputnodelist = htmldoc.DocumentNode.SelectNodes("//input");
            if (inputnodelist != null)
            {
                foreach (HtmlNode inputnode in inputnodelist)
                {
                    HtmlAttributeCollection attlist = inputnode.Attributes;
                    string strattname = "";
                    string strattvalue = "";
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("alt"))
                        {
                            strattname = att.Value;
                        }
                    }
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("value"))
                        {
                            strattvalue = att.Value;
                        }
                    }
                    if (strattname != string.Empty && !namevaluelist.ContainsKey(strattname))
                    {
                        namevaluelist.Add(strattname, strattvalue);
                    }
                }
            }
            if (namevaluelist.Count > 0)
            {
                myHastlist = namevaluelist;
            }
        }

        /// <summary>
        /// 提取textarea
        /// </summary>
        /// <param name="strcontent">html</param>
        public void GetAttListFormHTMLtextarea(string strcontent)
        {
            string content = strcontent;
            Hashtable namevaluelist = new Hashtable();
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;
            var textareanodelist = htmldoc.DocumentNode.SelectNodes("//textarea");
            if (textareanodelist != null)
            {
                foreach (HtmlNode textareanode in textareanodelist)
                {
                    HtmlAttributeCollection attlist = textareanode.Attributes;
                    string strattname = "";
                    string strattvalue = "";
                    string textareaid = "";
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("alt"))
                        {
                            strattname = att.Value;
                        }
                    }
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.Equals("id"))
                        {
                            textareaid = att.Value;
                        }
                    }
                    HtmlNode singlenode = HTMLDOC.GetElementbyId(textareaid);
                    if (singlenode != null)
                    {
                        strattvalue = singlenode.InnerText;
                    }

                    if (strattname != string.Empty && !namevaluelist.ContainsKey(strattname))
                    {
                        namevaluelist.Add(strattname, strattvalue);
                    }
                }
            }
            if (namevaluelist.Count > 0)
            {
                myHastlist = namevaluelist;
            }
        }
        public DataSet GetDataSetFormHTML(string strcontent)
        {
            return GetDataSetFormHTML(strcontent,false);
        }
        /// <summary>
        /// 提取表单的数据
        /// </summary>
        /// <param name="strcontent"></param>
        /// <returns></returns>
        public DataSet GetDataSetFormHTML(string strcontent, bool spinput = false)
        {
            DataSet ds = new DataSet();
            DataTable Dtt = new DataTable();
            Dtt.TableName = "Pages";
            ds.Tables.Add(Dtt);
            ds.Tables[0].Columns.Add("ID");
            ds.Tables[0].Columns.Add("Name");
            ds.Tables[0].Columns.Add("InputId");
            ds.Tables[0].Columns.Add("Value");
            ds.Tables[0].Columns.Add("Cols");

            string content = strcontent;
            Hashtable namevaluelist = new Hashtable();
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;

            GetChildNode(htmldoc.DocumentNode.ChildNodes, ref ds, ref namevaluelist, spinput);

            return ds;
        }
        private void GetChildNode(HtmlNodeCollection nodelist, ref DataSet ds, ref Hashtable namevaluelist, bool spinput = false)
        {
            foreach (HtmlNode item in nodelist)
            {
                if (item.ChildNodes.Count > 0)
                {
                    GetChildNode(item.ChildNodes, ref ds, ref namevaluelist, spinput);
                }
                if (item.NodeType == HtmlNodeType.Element)
                {
                    if (item.Name == "input")
                        NodeHandle(item, ref ds, ref namevaluelist, "input", spinput);
                    else if (item.Name == "textarea")
                        NodeHandle(item, ref ds, ref namevaluelist, "textarea", spinput);
                    else if (item.Name == "select")
                        NodeHandle(item, ref ds, ref namevaluelist, "select", spinput);
                }
            }
        }
        public void NodeHandle(HtmlNode nodelist, ref DataSet ds, ref Hashtable namevaluelist, string type, bool spinput = false)
        {
            var list = new HtmlAgilityPack.HtmlNodeCollection(nodelist);
            list.Add(nodelist);
            NodeHandle(list, ref ds, ref namevaluelist, type, spinput);
        }

        public void NodeHandle(HtmlNodeCollection nodelist, ref DataSet ds, ref Hashtable namevaluelist, string type, bool spinput = false)
        {
            if (nodelist != null)
            {
                foreach (HtmlNode inputnode in nodelist)
                {
                    HtmlAttributeCollection attlist = inputnode.Attributes;
                    var strattname = "";
                    var strattvalue = "";
                    var strattid = "";
                    var colspan = 0;
                    foreach (HtmlAttribute att in attlist)
                    {
                        if (att.Name.ToLower().Equals("alt") || att.Name.ToLower().Equals("title"))
                        {
                            strattname = att.Value;
                        }
                        if (att.Name.ToLower().Equals("id"))
                        {
                            strattid = att.Value;
                        }
                    }

                    if (type.Equals("textarea"))
                    {
                        string textareaid = "";
                        foreach (HtmlAttribute att in attlist)
                        {
                            if (att.Name.Equals("id"))
                            {
                                textareaid = att.Value;
                            }
                            if (att.Name.Equals("rebuildcols"))
                            {
                                colspan = PublicMethod.GetInt(att.Value);
                            }
                        }
                        HtmlNode singlenode = HTMLDOC.GetElementbyId(textareaid);
                        if (singlenode != null)
                        {
                            strattvalue = singlenode.InnerText;
                        }
                    }
                    else if (type.Equals("select"))
                    {
                        var selectedItem = false;
                        var firstItem = "";
                        var optionlist = inputnode.ChildNodes;
                        foreach (HtmlNode optionnode in optionlist)
                        {
                            if (optionnode.Name != "option") continue;
                            var optionattlist = optionnode.Attributes;
                            var currentoptionvalue = "";
                            for (int i = 0; i < optionattlist.Count; i++)
                            {
                                var att = optionattlist[i];
                                if (att.Name.Equals("selected"))
                                {
                                    selectedItem = true;
                                }
                                else if (att.Name == "value" && att.Value != "")
                                {
                                    firstItem = att.Value;
                                    currentoptionvalue = att.Value;
                                }
                                if (selectedItem && currentoptionvalue != "")
                                {
                                    strattvalue = currentoptionvalue;
                                }
                            }
                            if (selectedItem) break;
                        }
                        if (!selectedItem)
                        {
                            strattvalue = firstItem;
                        }
                    }
                    else
                    {
                        foreach (HtmlAttribute att in attlist)
                        {
                            if (att.Name.Equals("value"))
                            {
                                strattvalue = att.Value;
                            }
                            if (att.Name.Equals("rebuildcols"))
                            {
                                colspan = PublicMethod.GetInt(att.Value);
                            }
                        }
                    }

                    var spflag = spinput;
                    if (!spinput)
                    {
                        spflag = !strattname.Contains("签名") && !strattname.Contains("意见");
                    }
                    if (strattname != string.Empty && spflag && !strattvalue.Contains("用户自定义控件"))
                    {
                        //相同的key，加数字结尾重命名
                        if (namevaluelist.ContainsKey(strattname))
                        {
                            var length = 0;
                            foreach (var item in namevaluelist.Keys)
                            {
                                if (item.ToString().Contains(strattname))
                                    length++;
                            }
                            strattname += length++;
                        }
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["ID"] = (ds.Tables[0].Rows.Count + 1).ToString();
                        dr["Name"] = strattname;
                        dr["InputId"] = strattid;
                        try
                        {
                            dr["Value"] = strattname.Contains("金额") && !string.IsNullOrEmpty(strattvalue) ? PublicMethod.FormatMoney(decimal.Parse(strattvalue)) : strattvalue;
                        }
                        catch
                        {
                            dr["Value"] = strattvalue;
                        }
                        dr["Cols"] = colspan > 0 ? colspan.ToString() : (strattvalue.Length > 26 ? "1" : "2");
                        ds.Tables[0].Rows.Add(dr);
                        if (!namevaluelist.ContainsKey(strattname))
                            namevaluelist.Add(strattname, strattvalue);
                        if (!myHastlist.ContainsKey(strattname))
                            myHastlist.Add(strattname, strattvalue);
                    }
                }
            }
        }

        /// <summary>
        /// 提取input
        /// </summary>
        /// <param name="strcontent">html</param>
        public string GetHTMLinputByName(string strcontent, string name)
        {
            string result = string.Empty;
            Hashtable namevaluelist = new Hashtable();
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(strcontent);
            this.HTMLDOC = htmldoc;
            bool IsBreak = false;
            bool IsSetValue = false;
            HtmlNodeCollection inputnodelist = htmldoc.DocumentNode.SelectNodes("//input");
            foreach (HtmlNode inputnode in inputnodelist)
            {
                HtmlAttributeCollection attlist = inputnode.Attributes;
                string strattname = "";
                string strattvalue = "";
                foreach (HtmlAttribute att in attlist)
                {
                    if (att.Name.Equals("alt") && att.Value.Equals(name))
                    {
                        strattname = att.Value;
                        break;
                    }
                }
                foreach (HtmlAttribute att in attlist)
                {
                    if (att.Name.Equals("value"))
                    {
                        strattvalue = att.Value;
                        break;
                    }
                }

                if (strattname != string.Empty && strattname.Equals(name))
                {
                    result = strattvalue;
                    IsBreak = true;
                }

                if (IsBreak)
                {
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 提取所有(intput/select)
        /// </summary>
        /// <param name="strcontent">html</param>
        public Hashtable GetimgListFormHTMLall(string strcontent)
        {
            string content = strcontent;
            Hashtable namevaluelist = new Hashtable();
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;
            HtmlNodeCollection inputnodelist = htmldoc.DocumentNode.SelectNodes("//img");
            if (inputnodelist == null)
                return namevaluelist;
            foreach (HtmlNode inputnode in inputnodelist)
            {
                HtmlAttributeCollection attlist = inputnode.Attributes;
                string strattname = "";
                string strattvalue = "";
                foreach (HtmlAttribute att in attlist)
                {
                    if (att.Name.Equals("name"))
                    {
                        strattname = att.Value;
                        break;
                    }
                }
                foreach (HtmlAttribute att in attlist)
                {
                    if (att.Name.Equals("src"))
                    {
                        strattvalue = att.Value;
                        break;
                    }
                }
                if (strattname != string.Empty && !namevaluelist.ContainsKey(strattname))
                {
                    namevaluelist.Add(strattname, strattvalue);
                }
            }
            return namevaluelist;
        }
        public string SetSelectValue(string strcontent, string name, string value)
        {
            var content = strcontent;
            var htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(content);
            this.HTMLDOC = htmldoc;
            string strselect = "";
            string stroption = "";
            if (content.Contains("<select"))
            {
                strselect = "select";
                stroption = "option";
            }
            else if (content.Contains("<SELECT"))
            {
                strselect = "SELECT";
                stroption = "OPTION";
            }
            if (!string.IsNullOrEmpty(strselect) && !string.IsNullOrEmpty(stroption))
            {
                HtmlNodeCollection selectnodelist = htmldoc.DocumentNode.SelectNodes("//select");
                foreach (HtmlNode selectnode in selectnodelist)
                {
                    HtmlAttributeCollection attlist = selectnode.Attributes;
                    HtmlAgilityPack.HtmlDocument htmldocopyion = new HtmlAgilityPack.HtmlDocument();
                    htmldocopyion.LoadHtml(selectnode.InnerHtml);
                    string strattname = "";
                    //从select的属性中找到该select的名称
                    strattname = selectnode.GetAttributeValue("alt", "");
                    //找到option中被选中的，从被选中的option中找到value
                    if (strattname != name) continue;
                    var html = "";
                    var optionlist = htmldocopyion.DocumentNode.SelectNodes("//option");
                    foreach (HtmlNode optionnode in optionlist)
                    {
                        var optionattlist = optionnode.Attributes;
                        var currentoptionvalue = "";
                        for (int i = 0; i < optionattlist.Count; i++)
                        {
                            var att = optionattlist[i];
                            if (att.Name.Equals("selected"))
                            {
                                att.Remove();
                            }
                            if (att.Name == "value" && att.Value != "")
                            {
                                currentoptionvalue = att.Value;
                            }
                        }
                        if (currentoptionvalue == value)
                            optionnode.Attributes.Add("selected", "selected");
                        html += optionnode.OuterHtml + currentoptionvalue + "</option>";
                    }
                    selectnode.InnerHtml = html;
                }
            }
            return htmldoc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">字段名称或在ID</param>
        /// <param name="value">字段值</param>
        /// <param name="type">标记读取字段的名称或在ID</param>
        /// <returns></returns>
        public bool SetInputValue(string name, string value)
        {
            var result = false;
            if (HTMLDOC != null)
            {
                var namelist = new List<string> { "id", "name", "alt", "title" };
                var inputnodelist = HTMLDOC.DocumentNode.SelectNodes("//input");
                if (inputnodelist != null)
                    foreach (HtmlNode inputnode in inputnodelist)
                    {
                        var attlist = inputnode.Attributes;
                        HtmlAttribute currentAttr = null;
                        foreach (HtmlAttribute att in attlist)
                        {
                            if (namelist.Contains(att.Name.ToLower()) && currentAttr == null)
                            {
                                var val = att.Value;
                                if (val == name)
                                {
                                    currentAttr = att;
                                    break;
                                }
                            }
                        }
                        if (currentAttr != null)
                        {
                            inputnode.SetAttributeValue("value", value);
                            result = true;
                            break;
                        }
                    }

                var textareanodelist = HTMLDOC.DocumentNode.SelectNodes("//textarea");
                if (textareanodelist != null)
                    foreach (HtmlNode inputnode in textareanodelist)
                    {
                        var attlist = inputnode.Attributes;
                        HtmlAttribute currentAttr = null;
                        foreach (HtmlAttribute att in attlist)
                        {
                            if (namelist.Contains(att.Name.ToLower()) && currentAttr == null)
                            {
                                var val = att.Value;
                                if (val == name)
                                {
                                    currentAttr = att;
                                    break;
                                }
                            }
                        }
                        if (currentAttr != null)
                        {
                            inputnode.InnerHtml = value;
                            result = true;
                            break;
                        }
                    }

                var imgnodelist = HTMLDOC.DocumentNode.SelectNodes("//img");
                if (imgnodelist != null)
                    foreach (HtmlNode inputnode in imgnodelist)
                    {
                        var attlist = inputnode.Attributes;
                        HtmlAttribute currentAttr = null;
                        foreach (HtmlAttribute att in attlist)
                        {
                            if (namelist.Contains(att.Name.ToLower()) && currentAttr == null)
                            {
                                var val = att.Value;
                                if (val == name)
                                {
                                    currentAttr = att;
                                    break;
                                }
                            }
                        }
                        if (currentAttr != null)
                        {
                            inputnode.SetAttributeValue("src", value);
                            result = true;
                            break;
                        }
                    }
            }
            return result;
        }
        public string GetOuterHtml()
        {
            var result = string.Empty;
            if (HTMLDOC != null)
            {
                result = HTMLDOC.DocumentNode.OuterHtml;
            }
            return result;
        }

        #region 旧的提取方法
        /// <summary>
        /// 保留原有的提取方法，在此基础上重载
        /// </summary>该方法有缺陷，1：如果值中间有空格，只能提取出空格前边的内容。2：如果一个<td></td>中有多个控件则只提取第二个控件的内容
        /// <param name="strcontent"></param>
        //public void GetAttListFormHTML(string strcontent)
        //{
        //    string content = strcontent;
        //    Hashtable namevaluelist = new Hashtable();
        //    ArrayList tdlist = new ArrayList();
        //    try
        //    {
        //        string regstr = @"<td.+?>(?<content>.+?)</td>";   //获取TD之间所有的内容
        //        Regex reg = new Regex(regstr, RegexOptions.IgnoreCase);
        //        MatchCollection mc = reg.Matches(content);
        //        foreach (Match m in mc)
        //        {
        //            string strtd = m.Groups[0].ToString();
        //            if (!string.IsNullOrEmpty(strtd) && strtd.Contains("id=") && strtd.Contains("alt="))
        //            {
        //                tdlist.Add(strtd);
        //            }
        //        }
        //        if (tdlist.Count > 0)
        //        {
        //            foreach (string strtd in tdlist)
        //            {
        //                if (strtd != string.Empty)
        //                {
        //                    string strattname = "";
        //                    string strattvalue = "";
        //                    string svalue = strtd.Substring(strtd.IndexOf("id="));
        //                    string[] attlist = svalue.Split(' ');
        //                    if (attlist.Length > 0)
        //                    {
        //                        string strtd1 = strtd;
        //                        if (strtd1.Contains("<SELECT"))
        //                        {
        //                            int begino = strtd1.IndexOf("selected value=", 0);
        //                            int endo = strtd1.IndexOf(">", begino + 1);
        //                            strattvalue = strtd1.Substring(begino + 15, endo - begino - 15);
        //                            int i = 0;
        //                            foreach (string stratt in strtd.Split(' '))
        //                            {

        //                                if (stratt.Contains("alt="))
        //                                {
        //                                    strattname = stratt.Substring(stratt.IndexOf("=") + 1, stratt.IndexOf(">") - (stratt.IndexOf("=") + 1));
        //                                    break;
        //                                }
        //                                i++;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            foreach (string stratt in attlist)
        //                            {
        //                                if (stratt.Contains("alt="))
        //                                {
        //                                    strattname = stratt.Substring(stratt.IndexOf("=") + 1);
        //                                }
        //                                if (stratt.Contains("value="))
        //                                {
        //                                    strattvalue = stratt.Substring(stratt.IndexOf("=") + 1);
        //                                }
        //                            }
        //                        }
        //                        if (strattname != string.Empty && strattvalue != string.Empty && !namevaluelist.ContainsKey(strattname))
        //                        {
        //                            if (strattname != "")
        //                            {
        //                                strattname = strattname.Trim();
        //                                if (strattname.Contains("\""))
        //                                {
        //                                    strattname = strattname.Replace("\"", "");
        //                                }
        //                                //对于下拉列表框，包含>，截取
        //                                if (strattname.Contains(">"))
        //                                {
        //                                    strattname = strattname.Substring(0, strattname.IndexOf('>'));
        //                                }
        //                            }
        //                            if (strattvalue != "")
        //                            {
        //                                strattvalue = strattvalue.Trim();
        //                                if (strattvalue.Contains("\""))
        //                                {
        //                                    strattvalue = strattvalue.Replace("\"", "");
        //                                }
        //                                if (strattvalue.Contains(">"))
        //                                {
        //                                    strattvalue = strattvalue.Substring(0, strattvalue.IndexOf('>'));
        //                                }
        //                            }

        //                            namevaluelist.Add(strattname, strattvalue);

        //                        }

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string strex = ex.Message.ToString();
        //    }
        //    if (namevaluelist.Count > 0)
        //    {
        //        myHastlist = namevaluelist;
        //    }

        //}
        #endregion
    }
}
