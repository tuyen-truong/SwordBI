<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BILogin.aspx.cs" Inherits="HTLBIWebApp2012.Account.BILogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/Content/CSS/Login.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="~/Content/Images/index.jpg" /> 
    <title>HTLBI - Login</title>
</head>
<body>
<form runat="server" id="form1"> 
    <div id="additional"> 
    </div> 
    <div id="formWrapper"> 
	    <div id="formCasing"> 
	        <h1 >HTL Business Analysis</h1>
	        <div id="loginForm">
  	            <dl> 
		            <dt><asp:Label ID="PWDLabel" runat="server" AssociatedControlID="txtUserName">UserName</asp:Label></dt> 		            
		            <dd>
		                <asp:TextBox ID="txtUserName" runat="server" CssClass="input" Width="280px" MaxLength="250" ValidationGroup="Login1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" SetFocusOnError="True"
                            ControlToValidate="txtUserName" ErrorMessage="User Name is required." 
                            ToolTip="User Name is required." ValidationGroup="Login1" >*
                        </asp:RequiredFieldValidator>
		            </dd>
		            <dt><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtPassword">Password</asp:Label></dt> 		            
		            <dd>
		                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" Width="280" MaxLength="250" ValidationGroup="Login1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" SetFocusOnError="True"
                            ControlToValidate="txtPassword" ErrorMessage="Password is required." 
                            ToolTip="Password is required." ValidationGroup="Login1" >*
                        </asp:RequiredFieldValidator>
		            </dd> 
		            <dd><asp:CheckBox ID="RememberMe" runat="server" Text=" Remember me on this computer." /></dd> 
		            <dd><asp:ImageButton ID="LoginButton" runat="server" Width="93px" Height="40px" ImageUrl="~/Content/Images/LoginButton1.gif" CommandName="Login" ValidationGroup="Login1" /></dd> 
	            </dl>   	            
	        </div>
	    </div>
	    <div id="formFooter"></div>
	    <div style="text-align:left; margin-left:15px">
	        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login1" />
	    </div>
    </div> 
</form> 
<script language ="javascript" type ="text/javascript">
    function FillRememberMe() {
        document.getElementById("<%=txtUserName.ClientID%>").value = '<%=RUserName%>';
        document.getElementById("<%=txtPassword.ClientID%>").value = '<%=RPassword%>';
        document.getElementById("<%=RememberMe.ClientID%>").checked = <%=RCheck%>;
    }        
    onload = FillRememberMe;
</script>
</body>
</html>
