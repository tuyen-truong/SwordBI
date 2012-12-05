<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPropChart.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcPropChart" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<table>
    <tr>
        <td rowspan="8" style="width:170px;vertical-align:top;">
            <dxchartsui:webchartcontrol ID="chartPreview" ClientInstanceName="Layout_PropChart_chartPreview" 
                runat="server" Height="135px" Width="170px" IndicatorsPaletteName="Default" ToolTip="The illustration of chart"
                oncustomcallback="chartPreview_CustomCallback" ShowLoadingPanel="False">
                <padding bottom="0" left="0" right="0" top="0" />
                <seriesserializable>
                    <cc1:series Name="S1">
                        <points>
                            <cc1:seriespoint ArgumentSerializable="Dim A" Values="5"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim B" Values="3"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim C" Values="7"></cc1:SeriesPoint>
                        </points>
                        <viewserializable>
                            <cc1:sidebysidebarseriesview BarWidth="0.3"></cc1:SideBySideBarSeriesView>
                        </viewserializable>
                        <labelserializable>
                            <cc1:sidebysidebarserieslabel LineVisible="True" Visible="False">
                                <fillstyle>
                                    <optionsserializable><cc1:solidfilloptions /></optionsserializable>
                                </fillstyle>
                            </cc1:SideBySideBarSeriesLabel>
                        </labelserializable>
                        <PointOptionsSerializable>
                        <cc1:pointoptions></cc1:PointOptions>
                        </PointOptionsSerializable>
                        <LegendPointOptionsSerializable>
                        <cc1:pointoptions></cc1:PointOptions>
                        </LegendPointOptionsSerializable>
                    </cc1:Series>
                    <cc1:series Name="S2">
                        <points>
                            <cc1:seriespoint ArgumentSerializable="Dim A" Values="8"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim B" Values="1"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim C" Values="9"></cc1:SeriesPoint>
                        </points>
                        <viewserializable>
                            <cc1:sidebysidebarseriesview BarWidth="0.3"></cc1:SideBySideBarSeriesView>
                        </viewserializable>
                        <labelserializable>
                            <cc1:sidebysidebarserieslabel LineVisible="True" Visible="False">
                                <fillstyle>
                                    <optionsserializable><cc1:solidfilloptions /></optionsserializable>
                                </fillstyle>
                            </cc1:SideBySideBarSeriesLabel>
                        </labelserializable>
                        <pointoptionsserializable>
                            <cc1:pointoptions></cc1:PointOptions>
                        </pointoptionsserializable>
                        <legendpointoptionsserializable>
                            <cc1:pointoptions></cc1:PointOptions>
                        </legendpointoptionsserializable>
                    </cc1:Series>
                    <cc1:series Name="S3">
                        <points>
                            <cc1:seriespoint ArgumentSerializable="Dim A" Values="9"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim B" Values="4"></cc1:SeriesPoint>
                            <cc1:seriespoint ArgumentSerializable="Dim C" Values="13"></cc1:SeriesPoint>
                        </points>
                        <viewserializable>
                            <cc1:sidebysidebarseriesview BarWidth="0.3"></cc1:SideBySideBarSeriesView>
                        </viewserializable>
                        <labelserializable>
                            <cc1:sidebysidebarserieslabel LineVisible="True" Visible="False">
                                <fillstyle>
                                    <optionsserializable><cc1:solidfilloptions /></optionsserializable>
                                </fillstyle>
                            </cc1:SideBySideBarSeriesLabel>
                        </labelserializable>
                        <pointoptionsserializable><cc1:pointoptions></cc1:PointOptions></pointoptionsserializable>
                        <legendpointoptionsserializable><cc1:pointoptions></cc1:PointOptions></legendpointoptionsserializable>
                    </cc1:Series>
                </seriesserializable>
                <diagramserializable>
                    <cc1:xydiagram>
                        <axisx visible="False" visibleinpanesserializable="-1">
                            <autoscalebreaks enabled="True" maxcount="1" />
                            <range sidemarginsenabled="True" />
                        </axisx>
                        <axisy visibleinpanesserializable="-1" visible="False">
                            <range sidemarginsenabled="True" />
                        </axisy>
                    </cc1:XYDiagram>
                </diagramserializable>
                <SeriesTemplate><ViewSerializable><cc1:sidebysidebarseriesview></cc1:SideBySideBarSeriesView></ViewSerializable>
                <LabelSerializable>
                <cc1:sidebysidebarserieslabel LineVisible="True">
                <FillStyle><OptionsSerializable><cc1:solidfilloptions></cc1:SolidFillOptions></OptionsSerializable></FillStyle>
                </cc1:SideBySideBarSeriesLabel>
                </LabelSerializable>
                <PointOptionsSerializable><cc1:pointoptions></cc1:PointOptions></PointOptionsSerializable>
                <LegendPointOptionsSerializable><cc1:pointoptions></cc1:PointOptions></LegendPointOptionsSerializable>
                </SeriesTemplate>
                <legend visible="False"></legend>
                <FillStyle><OptionsSerializable><cc1:solidfilloptions></cc1:SolidFillOptions></OptionsSerializable></FillStyle>
            </dxchartsui:WebChartControl>
        </td>
        <td style="width:20px"></td>
        <td>Appearance</td>
        <td style="width:150px;">
            <dx:ASPxComboBox ID="cboAppearance" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropChart_cboAppearance" Width="100%" ShowImageInEditBox="True">
                <ClientSideEvents ValueChanged="function(s,e){
                    Layout_PropChart_chartPreview.PerformCallback('cboAppearance');
                }" />
            </dx:ASPxComboBox>
        </td>
        <td>Palette</td>
        <td style="width:150px;">
            <dx:ASPxComboBox ID="cboPalette" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropChart_cboPalette" ShowImageInEditBox="True" Width="100%">
                <ClientSideEvents ValueChanged="function(s,e){
                    Layout_PropChart_chartPreview.PerformCallback('cboPalette');
                }" />
            </dx:ASPxComboBox>

        </td>
        <td style="width:150px;"></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>Level 1</td>
        <td></td>
        <td>Level 2</td>
        <td>Title</td>
    </tr>
    <tr>
        <td></td>
        <td>X - Axis</td>
        <td>
            <dx:ASPxComboBox ID="cboAxisXField1" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropChart_cboAxisXField1" Width="100%">
            </dx:ASPxComboBox>
        </td>
        <td style="width:20px;"></td>
        <td>
            <dx:ASPxComboBox ID="cboAxisXField2" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropChart_cboAxisXField2" Width="100%">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtTitleX" runat="server" Width="100%"></dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Y - Axis</td>
        <td rowspan="3">
            <dx:ASPxListBox ID="lbxAxisYField" runat="server" 
                ClientInstanceName="Layout_PropChart_lbxAxisYField" AutoPostBack="false"
                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                Height="70px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                Width="100%">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                </LoadingPanelImage>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
            </dx:ASPxListBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnIn" runat="server" Text="&gt;" OnClick="btn_Click">
            </dx:ASPxButton>
        </td>
        <td rowspan="3">
            <dx:ASPxListBox ID="lbxAxisYFieldSel" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropChart_lbxAxisYFieldSel" Height="70px"
                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"                                     
                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                </LoadingPanelImage>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
            </dx:ASPxListBox>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtTitleY" runat="server" Width="100%"></dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnOut" runat="server" Text="&lt;" OnClick="btn_Click">
            </dx:ASPxButton>
        </td>
        <td>Unit Of Value</td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td>
            <dx:ASPxTextBox ID="txtYUnit" runat="server" Width="100%"></dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <dx:ASPxCheckBox ID="chkRotatedXY" runat="server" AutoPostBack="false" 
                Font-Names="Arial" Font-Size="10pt" Text="Rotated">
                <ClientSideEvents CheckedChanged="function(s,e){
                    Layout_PropChart_chartPreview.PerformCallback('chkRotatedXY');
                }" />
            </dx:ASPxCheckBox>
        </td>
        <td>
            <dx:ASPxCheckBox ID="chkShowSeriesLabel" runat="server" AutoPostBack="false"
                Font-Names="Arial" Font-Size="10pt" Text="Show series label">
                <ClientSideEvents CheckedChanged="function(s,e){
                    Layout_PropChart_chartPreview.PerformCallback('chkShowSeriesLabel');
                }" />
            </dx:ASPxCheckBox>
        </td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Width</td>
        <td>
            <asp:TextBox ID="txtWidth" runat="server" ToolTip="Number value" CssClass="numericInput" Width="145px" Text="485" />
        </td>
        <td>Height</td>
        <td>
            <asp:TextBox ID="txtHeight" runat="server" ToolTip="Number value" CssClass="numericInput" Width="145px" Text="400" />
        </td>
        <td></td>
    </tr>
</table>