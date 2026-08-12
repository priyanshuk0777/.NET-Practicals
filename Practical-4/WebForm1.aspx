<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="P4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 380px;
        }
        .auto-style3 {
            width: 380px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            width: 444px;
            margin-left: 120px;
        }
        .auto-style6 {
            height: 26px;
            width: 444px;
        }
        .auto-style7 {
            width: 380px;
            height: 34px;
        }
        .auto-style8 {
            width: 444px;
            height: 34px;
        }
        .auto-style9 {
            height: 34px;
        }
        .auto-style10 {
            width: 444px;
            margin-left: 120px;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>ONLINE EVENT REGISTRATION PORTAL</h1>
        <p>
        <p>

        <p>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label2" runat="server" style="font-weight: 700" Text="NAME:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:TextBox ID="TextBox1" runat="server" style="margin-bottom: 0px; width: 168px;"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="TextBox1"
                ErrorMessage="Name is Required"
                ForeColor="Red"
                Display="Dynamic">Name is Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label3" runat="server" style="font-weight: 700" Text="EMAIL:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="TextBox2"
                ErrorMessage="Email is required."
                ForeColor="Red"
                Display="Dynamic">Email is required.</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="TextBox2"
                ErrorMessage="Enter a valid email address."
                ForeColor="Red"
                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                Display="Dynamic">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label4" runat="server" style="font-weight: 700" Text="GR NO:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:TextBox ID="TextBox3" runat="server" Width="158px"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                ControlToValidate="TextBox3"
                ErrorMessage="GR number is required."
                ForeColor="Red"
                Display="Dynamic">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                ControlToValidate="TextBox3"
                ErrorMessage="GR number must contain only digits."
                ForeColor="Red"
                ValidationExpression="^\d+$"
                Display="Dynamic">GR number must contain only digits.</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label7" runat="server" style="font-weight: 700" Text="CLASS:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="">-- Select Class --</asp:ListItem>
                <asp:ListItem>EC1</asp:ListItem>
                <asp:ListItem>EC2</asp:ListItem>
                <asp:ListItem>EC3</asp:ListItem>
                <asp:ListItem>EC4</asp:ListItem>
            </asp:DropDownList>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                ControlToValidate="DropDownList1"
                InitialValue=""
                ErrorMessage="Please select a class."
                ForeColor="Red"
                Display="Dynamic">Please select a class.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label5" runat="server" style="font-weight: 700" Text="ENROLLMENT NO:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                ControlToValidate="TextBox4"
                ErrorMessage="Enrollment number is required."
                ForeColor="Red"
                Display="Dynamic">Enrollment number is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label6" runat="server" style="font-weight: 700" Text="PHONE NO:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                ControlToValidate="TextBox5"
                ErrorMessage="Phone number is required."
                ForeColor="Red"
                Display="Dynamic">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                ControlToValidate="TextBox5"
                ErrorMessage="Phone number must be 10 digits."
                ForeColor="Red"
                ValidationExpression="^\d{10}$"
                Display="Dynamic">Phone number must be 10 digits.</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="GENDER" runat="server" style="font-weight: 700" Text="GENDER:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:RadioButton ID="MALE" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged" Text="MALE" />
            <asp:RadioButton ID="FEMALE" runat="server" Text="FEMALE" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="Label8" runat="server" style="font-weight: 700" Text="COURSE:"></asp:Label>
                    </td>
                    <td class="auto-style5">
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="">-- Select Course --</asp:ListItem>
                <asp:ListItem>CE</asp:ListItem>
                <asp:ListItem>CSE</asp:ListItem>
                <asp:ListItem>CSE-CS</asp:ListItem>
                <asp:ListItem>AI&amp;ML</asp:ListItem>
            </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DropDownList3" Display="Dynamic" ErrorMessage="Select Course" ForeColor="Red">Select Course</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">
            <asp:Label ID="Label9" runat="server" style="font-weight: 700" Text="EVENT LIST:"></asp:Label>
                    </td>
                    <td class="auto-style8">
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="">-- Select Event --</asp:ListItem>
                <asp:ListItem>Hackathon</asp:ListItem>
                <asp:ListItem>Blood Castle</asp:ListItem>
                <asp:ListItem>Devil Square</asp:ListItem>
                <asp:ListItem>Chaos Castle</asp:ListItem>
                <asp:ListItem>Castle Siege</asp:ListItem>
                <asp:ListItem>Doppelganger</asp:ListItem>
            </asp:DropDownList>
                    </td>
                    <td class="auto-style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                ControlToValidate="DropDownList2"
                InitialValue=""
                ErrorMessage="Please select an event."
                ForeColor="Red"
                Display="Dynamic">Please select an event.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style6"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style3">
            <asp:Label ID="Label10" runat="server" style="font-weight: 700" Text="PASSWORD:"></asp:Label>
                    </td>
                    <td class="auto-style6">
            <asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox6" ErrorMessage="Password must be with Unique Characters" ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&amp;^#()_+\-=\[\]{};':&quot;\\|,.&lt;&gt;\/?])[A-Za-z\d@$!%*?&amp;^#()_+\-=\[\]{};':&quot;\\|,.&lt;&gt;\/?]{8,}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style5">
            <asp:Button ID="Button1" runat="server" Text="SUBMIT" Width="86px" CausesValidation="true" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="RESET" Width="123px" CausesValidation="false" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p style="margin-left: 360px">
            &nbsp;</p>
    </form>
</body>
</html>
