<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebClient._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GIORP 5000 Total Purchase Totaller</title>
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <h1>GIORP 5000 Total Purchase Totaller</h1>
        <br />
        Total purchase price:
        <asp:TextBox ID="priceBox" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="priceBoxValidator" runat="server" ControlToValidate="priceBox" ErrorMessage="Price cannot be blank" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
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
        &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="provinceListValidator" runat="server" InitialValue="-Select Province-" ControlToValidate="provinceList" ErrorMessage="You need to select a province" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="clearButton" runat="server" OnClick="clearButton_Click" Text="Clear" UseSubmitBehavior="False" />
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
                <td class="auto-style1">
                    <asp:Label ID="subtotalAmount" runat="server" Text="subtotal"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="pstAmount" runat="server" Text="PST"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="hstAmount" runat="server" Text="HST"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="gstAmount" runat="server" Text="GST"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="totalPurchaseAmount" runat="server" Text="total purchase"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div id="alertDiv" runat="server"> 
        <asp:Label ID="alert" runat="server"></asp:Label>
    </div>        
    </form>
</body>
</html>
