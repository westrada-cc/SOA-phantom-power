<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WebClient.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SET Registry</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="serviceSelectorDiv" runat="server"> 
        <h1>SET Registry</h1>
        <h2>Please select a service:</h2>
        <asp:DropDownList ID="serviceList" runat="server"></asp:DropDownList>        
        <br />
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
    </div>
    <div id="noServiceDiv" runat="server"> 
            <h1 style="color:red">There are currently no services available in the SET Registry.</h1>
    </div>
    </form>
</body>
</html>
