<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebClient._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GIORP 5000 Total Purchase Totaller</title>
    <style type="text/css">
        .auto-style1 {
            width: 102px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>GIORP 5000 Total Purchase Totaller</h1>


        <br />
        Total purchase price:
        <asp:TextBox ID="priceBox" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="provinceList" runat="server">
            <asp:ListItem Selected="True">-Select Province-</asp:ListItem>
            <asp:ListItem>AB</asp:ListItem>
            <asp:ListItem>BC</asp:ListItem>
            <asp:ListItem>MB</asp:ListItem>
            <asp:ListItem>NB</asp:ListItem>
            <asp:ListItem>NL</asp:ListItem>
            <asp:ListItem>NS</asp:ListItem>
            <asp:ListItem>NT</asp:ListItem>
            <asp:ListItem>NU</asp:ListItem>
            <asp:ListItem>ON</asp:ListItem>
            <asp:ListItem>PE</asp:ListItem>
            <asp:ListItem>QC</asp:ListItem>
            <asp:ListItem>SK</asp:ListItem>
            <asp:ListItem>YT</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="clearButton" runat="server" OnClick="clearButton_Click" Text="Clear" />


        <br />


        <br />


    </div>

    <div id="results" runat="server">
        <table border="1">
            <tr>
                <th>Subtotal Amount</th>
                <th>PST Amount</th>
                <th>HST Amount</th>
                <th>GST Amount</th>
                <th>Total Purchase Amount</th>
            </tr>
            <tr>
                <td>400</td>
                <td>3</td>
                <td>2</td>
                <td>1</td>
                <td>12441</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
