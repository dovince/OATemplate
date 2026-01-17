using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NWorkFlow_NFormTypeList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            BindTree();
        }
    }
    private void BindTree()
    {
        var nodelinkimg = "../Controls/TreeList/link.png";
        //先绑定所有分类，作为根节点
        DataSet MyDataSet1 = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPNFormType order by PaiXuStr asc,ID desc");
        for (int j = 0; j < MyDataSet1.Tables[0].Rows.Count; j++)
        {
            TreeNode MyNode = new TreeNode();
            MyNode.Text = MyDataSet1.Tables[0].Rows[j]["TypeName"].ToString();
            MyNode.Value = MyDataSet1.Tables[0].Rows[j]["ID"].ToString();
            MyNode.ToolTip = MyDataSet1.Tables[0].Rows[j]["BackInfo"].ToString();
            MyNode.ImageUrl = nodelinkimg;
            MyNode.NavigateUrl = "NForm.aspx?TypeID=" + MyDataSet1.Tables[0].Rows[j]["ID"].ToString();
            MyNode.Target = "viewFrame";
            this.ListTreeView.Nodes.Add(MyNode);
        }
    }
}
