﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcFourPortlet_Flow.ascx.cs"
    Inherits="HTLBIWebApp2012.App.Dashboard.wcFourPortlet_Flow" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<link href="../../Content/CSS/Dashboard.css" rel="stylesheet" type="text/css" />
<div id="palParmams" class="boxed">
    <center class="title">
        Dashboard parameters</center>
    <div class="contentNoScroll">
        <div id="container_Dashboard_Param" runat="server" style="width: 100%">
        </div>
    </div>
</div>
<div id="palDashboards" class="box-4zone-container" style="margin-top: 5px;">
    <table width="100%">
        <colgroup>
            <col width="50%" />
            <col width="50%" />
        </colgroup>
        <tr>
            <td valign="top">
                <div id="portlet1" class="boxed" style="padding-right: 3px;">
                    <center class="title">
                        <asp:ImageButton runat="server" ID="imgBtnSetting_portlet1" ImageUrl="~/Content/Images/setting_16.png"
                            ToolTip="Setting." ImageAlign="Right" OnClientClick="Set_ProcessState('0');"
                            CommandArgument="1" CommandName="Setting" OnCommand="imgBtn_Command" />
                        <dx:ASPxLabel ID="lblTitle_portlet1" runat="server" Text="">
                        </dx:ASPxLabel>
                    </center>
                    <div class="content">
                        <div id="container_Pl1_Param" runat="server" style="width: 100%">
                        </div>
                        <center>
                            <div id="container_Pl1" runat="server" style="width: 100%">
                            </div>
                        </center>
                    </div>
                </div>
            </td>
            <td valign="top">
                <div id="portlet2" class="boxed">
                    <center class="title">
                        <asp:ImageButton runat="server" ID="imgBtnSetting_portlet2" ImageUrl="~/Content/Images/setting_16.png"
                            ToolTip="Setting." ImageAlign="Right" OnClientClick="Set_ProcessState('0');"
                            CommandArgument="1" CommandName="Setting" OnCommand="imgBtn_Command" />
                        <dx:ASPxLabel ID="lblTitle_portlet2" runat="server" Text="">
                        </dx:ASPxLabel>
                    </center>
                    <div class="content">
                        <div id="container_Pl2_Param" runat="server" style="width: 100%">
                        </div>
                        <center>
                            <div id="container_Pl2" runat="server" style="width: 100%">
                            </div>
                        </center>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding: 3px 3px 0 0">
                <div id="portlet3" class="boxed">
                    <center class="title">
                        <asp:ImageButton runat="server" ID="imgBtnSetting_portlet3" ImageUrl="~/Content/Images/setting_16.png"
                            ToolTip="Setting." ImageAlign="Right" OnClientClick="Set_ProcessState('0');"
                            CommandArgument="1" CommandName="Setting" OnCommand="imgBtn_Command" />
                        <dx:ASPxLabel ID="lblTitle_portlet3" runat="server" Text="">
                        </dx:ASPxLabel>
                    </center>
                    <div class="content">
                        <div id="container_Pl3_Param" runat="server" style="width: 100%">
                        </div>
                        <center>
                            <div id="container_Pl3" runat="server" style="width: 100%">
                            </div>
                        </center>
                    </div>
                </div>
            </td>
            <td valign="top">
                <div id="portlet4" class="boxed" style="padding: 3px 3px 0 0">
                    <center class="title">
                        <asp:ImageButton runat="server" ID="imgBtnSetting_portlet4" ImageUrl="~/Content/Images/setting_16.png"
                            ToolTip="Setting." ImageAlign="Right" OnClientClick="Set_ProcessState('0');"
                            CommandArgument="1" CommandName="Setting" OnCommand="imgBtn_Command" />
                        <dx:ASPxLabel ID="lblTitle_portlet4" runat="server" Text="">
                        </dx:ASPxLabel>
                    </center>
                    <div class="content">
                        <div id="container_Pl4_Param" runat="server" style="width: 100%">
                        </div>
                        <center>
                            <div id="container_Pl4" runat="server" style="width: 100%">
                            </div>
                        </center>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
