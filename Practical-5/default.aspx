<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="Practical5.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Academic Calendar</title>

</head>

<body>

<form id="form1" runat="server">

<div>

    <h1>Academic Calendar & Leave Management</h1>

    <h2>Select Leave Date</h2>

    <asp:Calendar
        ID="Calendar1"
        runat="server"
        SelectionMode="Day">
    </asp:Calendar>

    <br />

    <asp:Label
        ID="lblSelectedDate"
        runat="server">
    </asp:Label>

    <br /><br />

    <asp:Button
        ID="btnApply"
        runat="server"
        Text="Apply"
        OnClick="btnApply_Click">
    </asp:Button>

</div>

</form>

</body>

</html>
