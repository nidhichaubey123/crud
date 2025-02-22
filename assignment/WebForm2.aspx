<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="assignment.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 20px;
        }

        .container {
            width: 80%;
            margin: auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        #DropDownList1{
            padding:10px;
        }
        h3 {
            color: #333;
            text-align: center;
            margin-bottom: 15px;
        }

        table {
            width: 100%;
            margin-top: 10px;
            border-collapse: collapse;
        }

        td {
            padding: 10px;
        }

        .btn {
            padding: 6px 12px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-size: 14px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .grid-container {
            margin-top: 20px;
        }

        .grid-container th {
            background-color: #007bff;
            color: white;
            padding: 10px;
            text-align: left;
        }

        .grid-container td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }

        .action-buttons {
            display: flex;
            gap: 5px;
            justify-content: center;
        }

        .search-panel {
            background: #f8f9fa;
            padding: 10px;
            border-radius: 5px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
        }

        .auto-style8 {
            width: 200px;
            font-weight: bold;
        }

        .auto-style1 {
            width: 100%;
            margin-top: 10px;
        }

        asp:TextBox {
            padding: 5px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .grid-container .btn-xs {
            font-size: 12px;
            padding: 4px 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3>Employee Management</h3>

            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click" Text="Add New Employee" />

            <br /><br />

            <div class="search-panel">
                <h4>Search Panel</h4>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style8">Employee Name</td>
                        <td class="auto-style8">Roles</td>
                        <td>Search</td>
                    </tr>
                    <tr>
                        <td class="auto-style8">
                            <asp:TextBox ID="TextBox1" runat="server" Height="31px" Width="195px"></asp:TextBox>
                        </td>
                        <td class="auto-style8">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click2" Text="Search" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="grid-container">
                <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="True" 
    OnRowCommand="grdData_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <div class="action-buttons">
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteFile"
                        OnClientClick="return confirm('Are you sure you want to delete this file?');"
                        CommandArgument='<%# Eval("Id") %>' CssClass="btn-xs btn-primary" />

                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditFile"
                        CommandArgument='<%# Eval("Id") %>' CssClass="btn-xs btn-primary" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
