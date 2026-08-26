<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Leave.aspx.cs"
    Inherits="Practical5.Leave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Leave Application</title>
</head>

<body>

<form id="form1" runat="server">

<div>

    <h1>Leave Application</h1>

    <table>

        <!-- Employee Name -->
        <tr>
            <td>Employee Name:</td>
            <td>
                <asp:TextBox
                    ID="txtEmployeeName"
                    runat="server">
                </asp:TextBox>
            </td>
        </tr>

        <!-- Leave Date -->
        <tr>
            <td>Leave Date:</td>
            <td>
                <asp:Label
                    ID="lblLeaveDate"
                    runat="server">
                </asp:Label>
            </td>
        </tr>

        <!-- Leave Type -->
        <tr>
            <td>Leave Type:</td>
            <td>

                <asp:DropDownList
                    ID="ddlLeaveType"
                    runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">

                    <asp:ListItem
                        Text="Select Leave Type"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Sick Leave"
                        Value="Sick Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Casual Leave"
                        Value="Casual Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Emergency Leave"
                        Value="Emergency Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Other"
                        Value="Other">
                    </asp:ListItem>

                </asp:DropDownList>

            </td>
        </tr>

        <!-- Other Leave Type -->
        <tr id="trOtherLeave" runat="server" visible="false">

            <td>Enter Leave Type:</td>

            <td>
                <asp:TextBox
                    ID="txtOtherLeave"
                    runat="server">
                </asp:TextBox>
            </td>

        </tr>

        <!-- Reason -->
        <tr>
            <td>Reason:</td>

            <td>
                <asp:TextBox
                    ID="txtReason"
                    runat="server"
                    TextMode="MultiLine"
                    Rows="5"
                    Columns="30">
                </asp:TextBox>
            </td>
        </tr>

        <!-- Remember Name -->
        <tr>
            <td>Remember Name:</td>

            <td>
                <asp:CheckBox
                    ID="chkRemember"
                    runat="server"
                    Text="Remember my name">
                </asp:CheckBox>
            </td>
        </tr>

        <!-- Submit -->
        <tr>
            <td></td>

            <td>
                <asp:Button
                    ID="btnSubmit"
                    runat="server"
                    Text="Submit Leave"
                    OnClick="btnSubmit_Click">
                </asp:Button>
            </td>
        </tr>

    </table>

    <br />

    <!-- Submission Details -->
    <asp:Panel
        ID="pnlResult"
        runat="server"
        Visible="false">

        <h2>Leave Application Details</h2>

        <table>

            <tr>
                <td>Name:</td>
                <td>
                    <asp:Label
                        ID="lblResultName"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td>Leave Date:</td>
                <td>
                    <asp:Label
                        ID="lblResultDate"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td>Leave Type:</td>
                <td>
                    <asp:Label
                        ID="lblResultType"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td>Reason:</td>
                <td>
                    <asp:Label
                        ID="lblResultReason"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td>Status:</td>
                <td>
                    <asp:Label
                        ID="lblResultStatus"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>

        </table>

    </asp:Panel>

</div>

</form>

</body>

</html>
