<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Errorrequest.aspx.cs" Inherits="Errorrequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<style type="text/css">
body {
	background-color: #C6E6FF;
	color: #000066;
}
    .auto-style1 {
        width: 59px;
        height: 58px;
    }
</style>
</head>
<body bgcolor="#FFFFFF">
    <form id="form1" runat="server">
   
<div style="font-size: 60px; font-style: normal; font-weight: bold; text-align: center; color: #06C;">
    <img alt="" class="auto-style1" longdesc="logo、" src="file://liuhao-pc/temp/广东地质logo1.png" /><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></div>
   
    </form>
<div>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
<span style="color: #00C; font-size: 36px; text-align: center;"> 》》异常的请求 ，请求包含非法字符</span></div>
</body>
</html>
