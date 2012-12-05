using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;

namespace HTLBIWebApp2012.App.Jwc
{
    public partial class jwcComboBox : jwcControlBase
    {
        public override string Render_JS()
        {
            var ret = "";
            var getparamFunc = "";
            if (this.IsEnableRange)
            {
                ret =
                "$('#" + this.Get_IDFrom() + "').change(function() {" +
                    "var valF = $('#" + this.Get_IDFrom() + " option:selected').val();" +
                    "var valT = $('#" + this.Get_IDTo() + " option:selected').val();" +
                    "if(valT==null || valT.toString().length==0) {" +
                        "$('#" + this.Get_IDTo() + "').val(valF);" +
                    "}" +
                    "else if(valF > valT) {" +
                        "$('#" + this.Get_IDTo() + "').val(valF);" +
                    "}" +
                "});" +
                "$('#" + this.Get_IDTo() + "').change(function() {" +
                    "var valF = $('#" + this.Get_IDFrom() + " option:selected').val();" +
                    "var valT = $('#" + this.Get_IDTo() + " option:selected').val();" +
                    "if(valF==null || valF.toString().length==0) {" +
                        "$('#" + this.Get_IDFrom() + "').val(valT);" +
                    "}" +
                    "else if(valF > valT) {" +
                        "$('#" + this.Get_IDFrom() + "').val(valT);" +
                    "}" +
                "});";
                getparamFunc =
                "function " + this.FunctionJSGetValue + "() {" +
                    "var valF = $('#" + this.Get_IDFrom() + " option:selected').val();" +
                    "var valT = $('#" + this.Get_IDTo() + " option:selected').val();" +
                    "if((valF==null||valF.toString().length==0) && (valT==null||valT.toString().length==0)) {" +
                        "return '';" +
                    "}" +
                    "return \"{'Name':'" + this.KeyField + "','LimitType':'Range','ValueFrom':'\" + valF + \"','ValueTo':'\" + valT + \"'}\";" +
                "}";
            }
            else
            {
                getparamFunc =
                "function " + this.FunctionJSGetValue + "() {" +
                    "var val = $('#" + this.MyID + " option:selected').val();" +
                    "if(val==null||val.toString().length==0) {" +
                        "return '';" +
                    "}" +
                    "return \"{'Name':'" + this.KeyField + "','LimitType':'Single','Value':'\" + val + \"'}\";" +
                "}";
            }
            return "<script>" + getparamFunc + "$(document).ready(function() {" + ret + "});</script>";
        }
        public override string Render_HTML()
        {
            var ret = this.Caption + ":&nbsp;";
            var optionDataStr = this.Get_OptionDataStr();
            if (this.IsEnableRange)
            {
                ret +=
                "<select id=\"" + this.Get_IDFrom() + "\">" + optionDataStr + "</select>" +
                "To\r\n" +
                "<select id=\"" + this.Get_IDTo() + "\">" + optionDataStr + "</select>";
            }
            else
            {
                ret += "<select id=\"" + this.MyID + "\">" + optionDataStr + "</select>";
            }
            return ret + this.HtmlSpace;            
        }
        private string Get_OptionDataStr()
        {
            var ret = "<option value=\"\"></option>\r\n";
            if (this.DataSource != null && this.DataSource.Count > 0)
            {
                var orderDS = this.DataSource.OrderBy(p => p);
                foreach (var val in orderDS)
                {
                    //if (item.Selected && !item.Enabled)
                    //    ret += "<option value=\"" + item.Value + "\" selected=\"selected\" disabled=\"disabled\">" + item.Text + "</option>\r\n";
                    //else if (item.Selected)
                    //    ret += "<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>\r\n";
                    //else if (!item.Enabled)
                    //    ret += "<option value=\"" + item.Value + "\" disabled=\"disabled\">" + item.Text + "</option>\r\n";
                    //else
                    ret += "<option value=\"" + val + "\">" + val + "</option>\r\n";
                }
            }
            return ret;
        }
    }
}