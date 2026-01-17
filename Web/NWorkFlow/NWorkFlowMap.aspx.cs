using System;
using System.Collections.Generic;
using System.Linq;
using ZWL.Common;

public partial class NWorkFlow_NWorkFlowMap : BasePage
{
    public List<ZWL.BLL.ERPNWorkFlowNode> NodeList = new List<ZWL.BLL.ERPNWorkFlowNode>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
            if (workId > 0)
            {
                var model = new ZWL.BLL.ERPNWorkToDo();
                model.GetModel(workId);
                WorkFlowId = model.WorkFlowID.Value;
                CurrentNodeId = model.JieDianID.Value;
            }
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            NodeList = node.GetListModel("WorkFlowID=" + WorkFlowId);
        }
    }
}