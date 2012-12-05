using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Jwc
{
    public partial class jwcDateEdit : jwcControlBase
    {
        public override string Render_JS()
        {
            var ret = "";
            var getparamFunc = "";
            if (!this.Is_FormatDate())
            {
                var vMax = "9999";
                if (this.DisplayFormat == "q") vMax = "4";
                if (this.IsEnableRange)
                {
                    ret =
                    "$('#" + this.Get_IDFrom() + "').autoNumeric({ wEmpty: 'zero', aSep: '', aDec: '.', mDec: '0', vMax: '" + vMax + "' });" + this.NewLine +
                    "$('#" + this.Get_IDFrom() + "').css('text-align', 'center');" + this.NewLine +
                    "$('#" + this.Get_IDFrom() + "').change(function() {" + this.NewLine +
                        "var valF = $('#" + this.Get_IDFrom() + "').val();" + this.NewLine +
                        "var valT = $('#" + this.Get_IDTo() + "').val();" + this.NewLine +
                        "if(valT==null || valT.toString().length==0) {" + this.NewLine +
                            "$('#" + this.Get_IDTo() + "').val(valF)" + this.NewLine +
                        "}" + this.NewLine +
                        "else {" + this.NewLine +
                            "if(valF > valT) {" + this.NewLine +
                                "$('#" + this.Get_IDTo() + "').val(valF)" + this.NewLine +
                            "}" + this.NewLine +
                        "}" + this.NewLine +
                    "});" + this.NewLine +
                    "$('#" + this.Get_IDTo() + "').autoNumeric({ wEmpty: 'zero', aSep: '', aDec: '.', mDec: '0', vMax: '" + vMax + "' });" + this.NewLine +
                    "$('#" + this.Get_IDTo() + "').css('text-align', 'center');" +
                    "$('#" + this.Get_IDTo() + "').change(function() {" + this.NewLine +
                        "var valF = $('#" + this.Get_IDFrom() + "').val();" + this.NewLine +
                        "var valT = $('#" + this.Get_IDTo() + "').val();" + this.NewLine +
                        "if(valF==null || valF.toString().length==0) {" + this.NewLine +
                            "$('#" + this.Get_IDFrom() + "').val(valT)" + this.NewLine +
                        "}" + this.NewLine +
                        "else {" + this.NewLine +
                            "if(valF > valT) {" + this.NewLine +
                                "$('#" + this.Get_IDFrom() + "').val(valT)" + this.NewLine +
                            "}" + this.NewLine +
                        "}" + this.NewLine +
                    "});";

                    getparamFunc =
                    "function " + this.FunctionJSGetValue + "() {" + this.NewLine +
                        "var valF = $('#" + this.Get_IDFrom() + "').val();" + this.NewLine +
                        "var valT = $('#" + this.Get_IDTo() + "').val();" + this.NewLine +
                        "if( ((valF == null || valF.toString().length == 0) && (valT == null || valT.toString().length == 0)) || (valF.toString() == '0' && valT.toString() == '0') ) {" + this.NewLine +
                            "return '';" + this.NewLine +
                        "}" + this.NewLine +
                        "return \"{'Name':'" + this.KeyField + "','LimitType':'Range','ValueFrom':'\"+ valF +\"','ValueTo':'\"+ valT +\"'}\";" + this.NewLine +
                    "}";
                }
                else
                {
                    ret =
                    "$('#" + this.MyID + "').autoNumeric({ wEmpty: 'zero', aSep: '', aDec: '.', mDec: '0', vMax: '" + vMax + "' });" + this.NewLine +
                    "$('#" + this.MyID + "').css('text-align', 'right');";
                    getparamFunc =
                    "function " + this.FunctionJSGetValue + "() {" + this.NewLine +
                        "var val = $('#" + this.MyID + "').val();" + this.NewLine +
                        "if(val == null || val.toString().length == 0 || val.toString() == '0') {" + this.NewLine +
                            "return '';" + this.NewLine +
                        "}" + this.NewLine +
                        "return \"{'Name':'" + this.KeyField + "','LimitType':'Single','Value':'\"+ val +\"'}\";" + this.NewLine +
                    "}";
                }
            }
            else
            {
                var commonSett =
                    "monthNamesShort: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12']," + this.NewLine +
                    "dateFormat: '" + (string.IsNullOrEmpty(this.DisplayFormat) ? "dd/mm/yy" : this.DisplayFormat) + "'," + this.NewLine +
                    "showOtherMonths: true," + this.NewLine +
                    "selectOtherMonths: true," + this.NewLine +
                    "showButtonPanel: true," + this.NewLine +
                    "changeMonth: true," + this.NewLine +
                    "changeYear: true," + this.NewLine +
                    "autoSize: true," + this.NewLine +
                    "showAnim: 'fold',";
                if (this.IsEnableRange)
                {
                    ret =
                    "$('#" + this.Get_IDFrom() + "').datepicker({" + this.NewLine +
                        commonSett + this.NewLine +
                        "onSelect: function(dateText, inst) {" + this.NewLine +
                            "var arrF = dateText.split('/');" + this.NewLine +
                            "var valT = $('#" + this.Get_IDTo() + "').val();" + this.NewLine +
                            "if(valT==null || valT.toString().length==0) {" + this.NewLine +
                                "$('#" + this.Get_IDTo() + "').val(dateText)" + this.NewLine +
                            "}" + this.NewLine +
                            "else {" + this.NewLine +
                                "var arrT = valT.split('/');" + this.NewLine +
                                "if(" + this.Get_JsCompareDate_F_Larger_T("arrF", "arrT") + ") {" + this.NewLine +
                                    "$('#" + this.Get_IDTo() + "').val(dateText)" + this.NewLine +
                                "}" + this.NewLine +
                            "}" + this.NewLine +
                        "}" + this.NewLine +
                    "});" + this.NewLine +
                    "$('#" + this.Get_IDTo() + "').datepicker({" + this.NewLine +
                        commonSett + this.NewLine +
                        "onSelect: function(dateText, inst) {" + this.NewLine +
                            "var arrT = dateText.split('/');" + this.NewLine +
                            "var valF = $('#" + this.Get_IDFrom() + "').val();" + this.NewLine +
                            "if(valF==null || valF.toString().length==0) {" + this.NewLine +
                                "$('#" + this.Get_IDFrom() + "').val(dateText)" + this.NewLine +
                            "}" + this.NewLine +
                            "else {" + this.NewLine +
                                "var arrF = valF.split('/');" + this.NewLine +
                                "if(" + this.Get_JsCompareDate_F_Larger_T("arrF", "arrT") + ") {" + this.NewLine +
                                    "$('#" + this.Get_IDFrom() + "').val(dateText)" + this.NewLine +
                                "}" + this.NewLine +
                            "}" + this.NewLine +
                        "}" + this.NewLine +
                    "});";
                    getparamFunc =
                    "function " + this.FunctionJSGetValue + "() {" + this.NewLine +
                        "var valF = $('#" + this.Get_IDFrom() + "').val();" + this.NewLine +
                        "var valT = $('#" + this.Get_IDTo() + "').val();" + this.NewLine +
                        "if( ((valF == null || valF.toString().length == 0) && (valT == null || valT.toString().length == 0)) || (valF.toString() == '0' && valT.toString() == '0') ) {" + this.NewLine +
                            "return '';" + this.NewLine +
                        "}" + this.NewLine +
                        "var arrF = valF.split('/');" + this.NewLine +
                        "var arrT = valT.split('/');" + this.NewLine +
                        "return \"{'Name':'" + this.KeyField + "','LimitType':'Range','ValueFrom':'\"+" + this.Get_JsGetDateValue("arrF") + "+\"','ValueTo':'\"+" + this.Get_JsGetDateValue("arrT") + "+\"'}\";" + this.NewLine +
                    "}";
                }
                else
                {
                    ret = "$('#" + this.MyID + "').datepicker({" + commonSett + "});";
                    getparamFunc =
                    "function " + this.FunctionJSGetValue + "() {" + this.NewLine +
                        "var val = $('#" + this.MyID + "').val();" + this.NewLine +
                        "if(val == null || val.toString().length == 0 || val.toString() == '0') {" + this.NewLine +
                            "return '';" + this.NewLine +
                        "}" + this.NewLine +
                        "var arr = val.split('/');" + this.NewLine +
                        "return \"{'Name':'" + this.KeyField + "','LimitType':'Single','Value':'\"+" + this.Get_JsGetDateValue("arr") + "+\"'}\";" + this.NewLine +
                    "}";
                }
            }
            return "<script>" + getparamFunc + "$(document).ready(function() {" + ret + "});</script>";
        }
        public override string Render_HTML()
        {
            var ret = this.Caption + ":&nbsp;";
            if (this.Is_FormatDate())
            {
                if (IsEnableRange)
                {
                    ret +=
                        "<input type=\"text\" id=\"" + this.Get_IDFrom() + "\" style=\"text-align:center\" />" + this.NewLine +
                        "To" + this.NewLine +
                        "<input type=\"text\" id=\"" + this.Get_IDTo() + "\" style=\"text-align:center\" />";
                }
                else
                    ret += "<input type=\"text\" id=\"" + this.MyID + "\" style=\"text-align:center\" />";
            }
            else
            {
                if (IsEnableRange)
                {
                    ret +=
                        "<input type=\"text\" id=\"" + this.Get_IDFrom() + "\" style=\"width:35px\" />" + this.NewLine +
                        "To" + this.NewLine +
                        "<input type=\"text\" id=\"" + this.Get_IDTo() + "\" style=\"width:35px\" />";
                }
                else
                    ret += "<input type=\"text\" id=\"" + this.MyID + "\" style=\"width:35px\" />";
            }
            return ret + this.HtmlSpace;
        }

        /// <summary>
        /// Kiểm tra FormatString hiện hành có phải là kiểu ngày hay không.
        /// </summary>
        private bool Is_FormatDate()
        {
            var fStr = this.DisplayFormat;
            return
                fStr == "dd/mm/yy" || fStr == "dd/yy/mm" ||
                fStr == "mm/dd/yy" || fStr == "mm/yy/dd" ||
                fStr == "yy/mm/dd" || fStr == "yy/dd/mm" ||
                fStr == "yy/mm" || fStr == "mm/yy";
        }
        /// <summary>
        /// VD: arrF[2] + arrF[1] + arrF[0] > arrT[2] + arrT[1] + arrT[0]
        /// </summary>
        private string Get_JsCompareDate_F_Larger_T(string arrF, string arrT)
        {
            var tmpl = arrF + "[{0}] + " + arrF + "[{1}] + " + arrF + "[{2}] > " + arrT + "[{0}] + " + arrT + "[{1}] + " + arrT + "[{2}]";
            var formatStr = this.DisplayFormat;
            if (formatStr == "dd/mm/yy") return string.Format(tmpl, 2, 1, 0);
            if (formatStr == "dd/yy/mm") return string.Format(tmpl, 1, 2, 0);
            if (formatStr == "mm/dd/yy") return string.Format(tmpl, 2, 0, 1);
            if (formatStr == "mm/yy/dd") return string.Format(tmpl, 1, 0, 2);
            if (formatStr == "yy/mm/dd") return string.Format(tmpl, 0, 1, 2);
            if (formatStr == "yy/dd/mm") return string.Format(tmpl, 0, 2, 1);
            tmpl = arrF + "[{0}] + " + arrF + "[{1}] > " + arrT + "[{0}] + " + arrT + "[{1}]";
            if (formatStr == "mm/yy") return string.Format(tmpl, 1, 0);
            if (formatStr == "yy/mm") return string.Format(tmpl, 0, 1);
            return "true";
        }
        /// <summary>
        /// Lấy giá trị ngày hợp lệ cho dù formatString có thay đổi.
        /// </summary>
        private string Get_JsGetDateValue(string arr)
        {
            var tmpl = arr + "[{0}] + '/' + " + arr + "[{1}] + '/' + " + arr + "[{2}]";
            var formatStr = this.DisplayFormat;
            if (formatStr == "dd/mm/yy") return string.Format(tmpl, 2, 1, 0);
            if (formatStr == "dd/yy/mm") return string.Format(tmpl, 1, 2, 0);
            if (formatStr == "mm/dd/yy") return string.Format(tmpl, 2, 0, 1);
            if (formatStr == "mm/yy/dd") return string.Format(tmpl, 1, 0, 2);
            if (formatStr == "yy/mm/dd") return string.Format(tmpl, 0, 1, 2);
            if (formatStr == "yy/dd/mm") return string.Format(tmpl, 0, 2, 1);
            tmpl = arr + "[{0}] + '/' + " + arr + "[{1}]";
            if (formatStr == "mm/yy") return string.Format(tmpl, 1, 0);
            if (formatStr == "yy/mm") return string.Format(tmpl, 0, 1);
            return "true";
        }
    }
}