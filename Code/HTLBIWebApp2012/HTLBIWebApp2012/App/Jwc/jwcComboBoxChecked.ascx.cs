using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;

namespace HTLBIWebApp2012.App.Jwc
{
    public partial class jwcComboBoxChecked : jwcControlBase
    {
        public override string Render_JS()
        {
            var ret =
            "<script>" + this.NewLine +
                "function " + this.MyID + "_SetCheckedAll() {" + this.NewLine +
                    "try {"+
                        "var isChecked = $('#" + this.MyID + "_chkAll').is(':checked');" + this.NewLine +
                        "var chk = $('#" + cblstParams.ClientID + "')[0];" + this.NewLine +
                        "var checkbox = chk.getElementsByTagName('input');" + this.NewLine +
                        "for (var i = 0; i < checkbox.length; i++) {" + this.NewLine +
                            "checkbox[i].checked = isChecked;" + this.NewLine +
                        "}" + this.NewLine +
                    "} catch(ex){ }"+
                    "return true;" + this.NewLine +
                "}" + this.NewLine +
                "function " + this.FunctionJSGetValue + "() {" + this.NewLine +
                    "try {"+
                        "var scopeVal = '[';" + this.NewLine +
                        "var chk = $('#" + cblstParams.ClientID + "')[0];" + this.NewLine +
                        "var checkbox = chk.getElementsByTagName('input');" + this.NewLine +
                        "var label = chk.getElementsByTagName('label');" + this.NewLine +
                        "for (var i = 0; i < checkbox.length; i++) {" + this.NewLine +
                            "if (checkbox[i].checked) {" + this.NewLine +
                                "scopeVal += \"'\" + label[i].innerHTML + \"'\" + ',';" + this.NewLine +
                            "}" + this.NewLine +
                        "}" + this.NewLine +
                        "scopeVal += ']';" + this.NewLine +
                        "scopeVal = scopeVal.replace(',]', ']');" + this.NewLine +
                        "if(scopeVal=='[]') return '';" + this.NewLine +
                        "return \"{'Name':'" + this.KeyField + "','LimitType':'Scope','ScopeValue':\"+scopeVal+\"}\";" + this.NewLine +
                    "} catch(ex){ return ''; }" +
                "}" + this.NewLine +
                "$(document).ready(function() {" + this.NewLine +
                    "$('#" + this.MyID + "_ParamDialog').dialog({" + this.NewLine +
                        "resizable: false," + this.NewLine +
                        "autoOpen: false," + this.NewLine +
                        "show: { effect: 'drop', direction: 'up' }," + this.NewLine +
                        "hide: { effect: 'drop', direction: 'up' }," + this.NewLine +
                        "width: 500" + this.NewLine +
                    "});" + this.NewLine +
                    "$('#" + this.MyID + "').click(function(e) {" + this.NewLine +
                        "$('#" + this.MyID + "_ParamDialog').dialog( 'open' );" + this.NewLine +
                    "});" + this.NewLine +
                    "$('#" + this.MyID + "').button();" + this.NewLine +
                "});" + this.NewLine +
            "</script>";
            return ret;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.cblstParams.DataSource = this.DataSource.Select(p => new ListItem(p, p)).ToList();
                this.cblstParams.DataTextField = "Text";
                this.cblstParams.DataValueField = "Value";
                this.cblstParams.DataBind();
            }
            catch { }
        }
    }
}