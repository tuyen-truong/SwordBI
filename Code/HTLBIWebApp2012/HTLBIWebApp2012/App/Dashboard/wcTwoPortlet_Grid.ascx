﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcTwoPortlet_Grid.ascx.cs"
    Inherits="HTLBIWebApp2012.App.Dashboard.wcTwoPortlet_Grid" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<link href="../../Content/CSS/Dashboard.css" rel="stylesheet" type="text/css" />
<div class="container">
    <div id="palParmams" class="boxed">
        <center class="title">Dashboard parameters</center>
        <div class="contentNoScroll"><div id="container_Dashboard_Param" runat="server" style="width: 100%"></div></div>
    </div>
    <div class="top" style="padding-top: 4px">
        <div id="portlet1" class="boxed">
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
    </div>
    <div class="bottom" style="padding-top: 4px">
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
    </div>
</div>