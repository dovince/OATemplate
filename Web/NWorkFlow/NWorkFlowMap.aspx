<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWorkFlowMap.aspx.cs" Inherits="NWorkFlow_NWorkFlowMap" %>

<%@ Import Namespace="System.Linq" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no" />
    <link href="../CSS/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../JS/jsplumb-1.7.5/css/jsplumb.css" rel="stylesheet" />
    <link href="../JS/jsplumb-1.7.5/demo/flowchart/demo.css" rel="stylesheet" />
    <style>
        .selected {
            background-color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <!-- demo -->
            <div class="demo flowchart-demo" id="flowchart-demo">
                <%if (NodeList.Any())%>
                <%{ %>
                <% var start = NodeList.FirstOrDefault(r => r.NodeAddr == "开始");  %>
                <% var end = NodeList.FirstOrDefault(r => r.NodeAddr == "结束");  %>
                <% var source = NodeList.Where(t => t.NodeAddr != "开始" && t.NodeAddr != "结束").OrderBy(r => int.Parse(r.NodeSerils));  %>
                <% var groupIndex = 0; var rootLeft = 30; var rootTop = 80;%>
                <div class="itemGroup" data-col="<%=groupIndex %>"">
                    <div class='window <%=start.ID==CurrentNodeId?"selected":"" %>' id="flowchartWindow<%=start.NodeSerils%>" data-nodeserils="<%=start.NodeSerils%>" data-nodeaddr="<%=start.NodeAddr %>" data-nextnode="<%=start.NextNode %>" style='left: <%=rootLeft%>px; top: <%=rootTop%>px;'>
                        <strong><%=start.NodeSerils+","+start.NodeName %></strong>
                    </div>
                </div>
                <%for (var i = 0; i < source.Count(); i++)%>
                <%{ %>
                <% var item = source.ElementAt(i);  %>
                <%if (i % 5 == 0) %>
                <%{ %>
                    <% groupIndex = groupIndex+1; %>
                    <div class="itemGroup" data-col="<%=groupIndex %>">
                        <% rootLeft = int.Parse(groupIndex.ToString()) * 250 + 30; %>
                <%} %>
                        <% rootTop = i * 130 + 30; %>
                    <div class='window <%=item.ID==CurrentNodeId?"selected":"" %>' id="flowchartWindow<%=item.NodeSerils%>" data-nodeserils="<%=item.NodeSerils%>" data-nodeaddr="<%=item.NodeAddr %>" data-nextnode="<%=item.NextNode %>" style='left: <%=rootLeft%>px; top: <%=rootTop%>px;'>
                        <strong><%=item.NodeSerils+","+item.NodeName %></strong>
                    </div>
                    <%if (i % 5 == 4 || (i == source.Count() - 1)) %>
                    <%{ %>
                        </div>
                    <%} %>
                <%} %>
                <% rootLeft = (groupIndex+1) * 250 + 30; rootTop = 80;%>
                <div class="itemGroup" data-col="<%=groupIndex+1 %>">
                    <div class='window <%=end.ID==CurrentNodeId?"selected":"" %>' id="flowchartWindow<%=end.NodeSerils%>" data-nodeserils="<%=end.NodeSerils%>" data-nodeaddr="<%=end.NodeAddr %>" data-nextnode="<%=end.NextNode %>" style='left: <%=rootLeft%>px; top: <%=rootTop%>px;'>
                        <strong><%=end.NodeSerils+","+end.NodeName %></strong>
                    </div>
                </div>
                <%} %>
            </div>
            <!-- /demo -->
        </div>

        <!-- JS -->
        <!-- support lib for bezier stuff -->
        <script src="../JS/jsplumb-1.7.5/lib/jsBezier-0.7.js"></script>
        <!-- event adapter -->
        <script src="../JS/jsplumb-1.7.5/lib/mottle-0.6.js"></script>
        <!-- geometry functions -->
        <script src="../JS/jsplumb-1.7.5/lib/biltong-0.2.js"></script>
        <!-- drag -->
        <script src="../JS/jsplumb-1.7.5/lib/katavorio-0.6.js"></script>
        <!-- jsplumb util -->
        <script src="../JS/jsplumb-1.7.5/src/util.js"></script>
        <script src="../JS/jsplumb-1.7.5/src/browser-util.js"></script>
        <!-- main jsplumb engine -->
        <script src="../JS/jsplumb-1.7.5/src/jsPlumb.js"></script>
        <!-- base DOM adapter -->
        <script src="../JS/jsplumb-1.7.5/src/dom-adapter.js"></script>
        <script src="../JS/jsplumb-1.7.5/src/overlay-component.js"></script>
        <!-- endpoint -->
        <script src="../JS/jsplumb-1.7.5/src/endpoint.js"></script>
        <!-- connection -->
        <script src="../JS/jsplumb-1.7.5/src/connection.js"></script>
        <!-- anchors -->
        <script src="../JS/jsplumb-1.7.5/src/anchors.js"></script>
        <!-- connectors, endpoint and overlays  -->
        <script src="../JS/jsplumb-1.7.5/src/defaults.js"></script>
        <!-- bezier connectors -->
        <script src="../JS/jsplumb-1.7.5/src/connectors-bezier.js"></script>
        <!-- state machine connectors -->
        <script src="../JS/jsplumb-1.7.5/src/connectors-statemachine.js"></script>
        <!-- flowchart connectors -->
        <script src="../JS/jsplumb-1.7.5/src/connectors-flowchart.js"></script>
        <!-- SVG renderer -->
        <script src="../JS/jsplumb-1.7.5/src/renderers-svg.js"></script>


        <!-- vml renderer -->
        <script src="../JS/jsplumb-1.7.5/src/renderers-vml.js"></script>

        <!-- no library jsPlumb adapter -->
        <script src="../JS/jsplumb-1.7.5/src/base-library-adapter.js"></script>
        <script src="../JS/jsplumb-1.7.5/src/dom.jsPlumb.js"></script>
        <!-- /JS -->

        <!--  demo code -->
        <script src="../JS/jsplumb-1.7.5/do/workflowmap.js"></script>
    </form>
</body>
</html>
