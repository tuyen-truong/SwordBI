<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcSubMenu_bak.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcSubMenu_bak" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register src="wcCurrentPath.ascx" tagname="wcCurrentPath" tagprefix="uc1" %>

<dx:ASPxCallbackPanel ID="cbkpMnuSubH" ClientInstanceName="cbkpMnuSubH" 
    runat="server" >
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxMenu 
                ID="mnuSubH" runat="server" AutoSeparators="All" BackColor="#F1F4F9"
                BorderBetweenItemAndSubMenu="HideAll" AppearAfter="1" DisappearAfter="1"                
                Font-Bold="true" Font-Names="Arial" Font-Size="8"
                EnableAnimation="false" EnableTheming="false" RenderMode="Lightweight" 
                ShowPopOutImages="False" ShowSubMenuShadow="False" 
                ItemLinkMode="TextOnly" ItemSpacing="0px" >                                
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F1F4F9" PopOutImageSpacing="0" ToolbarPopOutImageSpacing="0">
                    <SelectedStyle BackColor="#F1F4F9">
                        <Border BorderStyle="None"></Border>
                    </SelectedStyle>
                    <HoverStyle BackColor="#F1F4F9">
                        <Border BorderStyle="None"></Border>
                    </HoverStyle>
                    <Border BorderStyle="None"></Border>
                </ItemStyle>
                <SubMenuStyle BackColor="#F1F4F9">
                </SubMenuStyle>
                <SubMenuItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#F1F4F9" >
                    <SelectedStyle BackColor="#F1F4F9">
                        <Border BorderStyle="None"></Border>
                    </SelectedStyle>
                    <HoverStyle BackColor="#F1F4F9">
                        <Border BorderStyle="None"></Border>
                    </HoverStyle>                    
                    <Border BorderStyle="None"></Border>
                </SubMenuItemStyle>
                <Border BorderStyle="None" />
            </dx:ASPxMenu>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>