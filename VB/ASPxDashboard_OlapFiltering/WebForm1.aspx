<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="WebForm1.aspx.vb" 
         Inherits="ASPxDashboard_OlapFiltering.WebForm1" %>

<%@ Register Assembly="DevExpress.Dashboard.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; top: 0; left: 0; right: 0; bottom: 0;">
        <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Width="100%" Height="100%" 
            onconfiguredataconnection="ASPxDashboard1_ConfigureDataConnection">
        </dx:ASPxDashboard>
    </div>
    </form>
</body>
</html>