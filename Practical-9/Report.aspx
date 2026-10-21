<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs"
    Inherits="PlacementDashboard.Report" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Placement Report</title>
</head>

<body>
    <form id="form1" runat="server">

        <h2>Student Placement Report</h2>

        <CR:CrystalReportViewer
            ID="CrystalReportViewer1"
            runat="server"
            AutoDataBind="true" />

    </form>
</body>
</html>
