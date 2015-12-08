<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebClient._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GIORP 5000 Total Purchase Totaller</title>
    <style>
    table, td, th {    
        border: 1px solid #ddd;
        text-align: center;
    }

    table {
    border-collapse: collapse;
    }

    th, td {
    padding: 15px;
    }
    </style>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }

        function isDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 1 && charCode == 46)
                return false;
            else {
                if (charCode == 46 || (charCode >= 48 && charCode <= 57))
                    return true;
                return false;
            }
        }
    </script>
</head>
<body>
    <center>
    <form id="form1" runat="server">
    <div>
        <h1>GIORP 5000 Total Purchase Totaller</h1>
        <br />
        <h2 id="teamNameID" runat="server"></h2>
        &nbsp;&nbsp;&nbsp;
        <h2 id="serviceNameID" runat="server"></h2>
        <br />
        <center>
        Total purchase price:
        <asp:TextBox ID="priceBox" runat="server" onkeypress="return isDecimal(event);"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        </center>
        <asp:RequiredFieldValidator ID="priceBoxValidator" runat="server" ControlToValidate="priceBox" ErrorMessage="Price cannot be blank" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <center>
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
        </center>
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


        <!--
        <table>
            <tr>
                <th>Subtotal Amount</th>
                <th>PST Amount</th>
                <th>HST Amount</th>
                <th>GST Amount</th>
                <th>Total Purchase Amount</th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="subtotalAmount" runat="server" Text="subtotal"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="pstAmount" runat="server" Text="PST"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="hstAmount" runat="server" Text="HST"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="gstAmount" runat="server" Text="GST"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="totalPurchaseAmount" runat="server" Text="total purchase"></asp:Label>
                </td>
            </tr>
        </table>
        -->
        <asp:Label ID="response1" runat="server" Text="Response 1"></asp:Label>
        <br />
        <br />
        <asp:Label ID="response2" runat="server" Text="Response 2"></asp:Label>
        <br />
        <br />
        <asp:Label ID="response3" runat="server" Text="Response 3"></asp:Label>
        <br />
        <br />
        <asp:Label ID="response4" runat="server" Text="Response 4"></asp:Label>
        <br />
        <br />
        <asp:Label ID="response5" runat="server" Text="Response 5"></asp:Label>
        <br />
    </div>

    <div id="alertDiv" runat="server"> 
        <asp:Label ID="alert" runat="server" ForeColor="Red"></asp:Label>
    </div>        
    </form>
    </center>
</body>
</html>
