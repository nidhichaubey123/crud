<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="assignment.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        form {
            background: white;
            padding: 100px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 600px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

       

        input, select {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .button-container {
            text-align: center;
            margin-top: 20px;
        }

        .btn {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .btn-cancel {
            background-color: #dc3545;
        }

        .btn-cancel:hover {
            background-color: #c82333;
        }

        .error-message {
            color: red;
            font-size: 12px;
        }
        .auto-style1 {
            height: 35px;
        }
        .auto-style2 {
            height: 59px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2 style="text-align: center;">Employee Registration</h2>
        <table>
            <tr>
                <td class="auto-style1">Employee Name <span style="color:red;font-weight:bold">*</span></td>
                <td class="auto-style1"><asp:TextBox ID="Nametextbox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Nametextbox" ErrorMessage="Name required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Login Id<span style="color:red;font-weight:bold">*</span></td>
                <td><asp:TextBox ID="logintextbox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="logintextbox" ErrorMessage="Login Id Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Contact No<span style="color:red;font-weight:bold">*</span></td>
                <td>
                    <asp:TextBox ID="contacttextbox" runat="server" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="contacttextbox" ErrorMessage="Contact Required" ForeColor="Red" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="contacttextbox" ErrorMessage="Enter a valid number" ValidationExpression="^[6-9]\d{9}$" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Gender<span style="color:red;font-weight:bold">*</span></td>
                <td>
                    <asp:DropDownList ID="genderDropDownList1" runat="server">
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="genderDropDownList1" ErrorMessage="Gender Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Email<span style="color:red;font-weight:bold">*</span></td>
                <td><asp:TextBox ID="emailtextbox" runat="server" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="emailtextbox" ErrorMessage="Email required" ForeColor="Red" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="emailtextbox" Display="Dynamic" ErrorMessage="Enter Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Roles<span style="color:red;font-weight:bold">*</span></td>
                <td class="auto-style2"><asp:CheckBoxList ID="roleCheckBoxList1" runat="server" Height="32px"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td>Status<span style="color:red;font-weight:bold">*</span></td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
         <tr>
    <td>Date of Resignation<span style="color:red;font-weight:bold">*</span></td>
    <td>
        <asp:TextBox ID="DorTextbox" runat="server" TextMode="Date"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
            ControlToValidate="DorTextbox" 
            ErrorMessage="Date of resignation is required!" 
            ForeColor="Red" 
            Display="Dynamic">
        </asp:RequiredFieldValidator>
    </td>
</tr>

        </table>

        <div class="button-container">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" CssClass="btn" />
            <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-cancel" OnClick="Button2_Click" />
        </div>
    </form>
</body>
</html>
