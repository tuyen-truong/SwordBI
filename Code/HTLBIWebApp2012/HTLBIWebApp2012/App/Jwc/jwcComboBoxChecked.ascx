<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="jwcComboBoxChecked.ascx.cs" Inherits="HTLBIWebApp2012.App.Jwc.jwcComboBoxChecked" %>

<input id="<%=this.MyID%>" type="button" value="<%=this.Caption%>&nbsp;&nbsp;&nbsp;&nbsp;" title="Click here to open <%=this.Caption%> selection panel." style="background-image:url('../../Images/Other/down.png');background-repeat:no-repeat; background-position:right; font-size:8pt; height:21px"/><%=this.HtmlSpace%>
<div id="<%=this.MyID%>_ParamDialog" style="padding:1px 2px 1px 2px; font-size:9pt" title="<%=this.Caption%> selection">
    <input id="<%=this.MyID%>_chkAll" type="checkbox" onchange="<%=this.MyID%>_SetCheckedAll();" style="text-align:left" />
    <label for="<%=this.MyID%>_chkAll" style="font-family:Arial; font-size:9pt; font-weight:bold">Select All</label>
    <hr />
    <div style="width:100%; height:300px; overflow-y:scroll; overflow-x:hidden;  border:1px solid #AECAF0">
        <asp:CheckBoxList ID="cblstParams" runat="server" DataTextField="Name" 
            DataValueField="Code" Font-Names="Arial" Font-Size="9pt" Width="137px"
            RepeatColumns="1" RepeatLayout="Flow" >
        </asp:CheckBoxList>
    </div>
</div>