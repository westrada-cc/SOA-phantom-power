<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WebClient.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SET Registry</title>
    
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
    </script>
</head>
<body>
    <center>
    <form id="form1" runat="server">
    <div id="serviceSelectorDiv" runat="server"> 
        <h1>SET Registry</h1>
        <h2>Please enter your team name:</h2>       
        <center><asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox></center>
        &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="teamNameRequiredValidator" runat="server" ErrorMessage="Enter a team name" ForeColor="Red" ControlToValidate="nameTextBox"></asp:RequiredFieldValidator>
        <br />
        <br />
        <h2>Please enter your team ID:</h2>       
        <center><asp:TextBox ID="idTextBox"  onkeypress="return isNumberKey(event);" runat="server"></asp:TextBox></center>
        &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="teamIDRequiredValildator" runat="server" ControlToValidate="idTextBox" ErrorMessage="Enter your team ID" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
    </div>
    <div id="errorServiceDiv" runat="server"> 
            <h1 id="ErrorMessageID" style="color:red" runat="server"></h1>
    </div>
    <div id="noServiceDiv" runat="server"> 
            <h1 style="color:red">There are currently no services available in the SET Registry.</h1>
    </div>        
    </form>
    </center>
</body>
</html>
