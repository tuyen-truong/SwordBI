
namespace HTLBIWebApp2012.App.Jwc
{
    public partial class jwcCommandPost : jwcControlBase
    {
        public override string Render_JS()
        {
            return
            "<script>" + this.NewLine +
                "$(document).ready(function() {" + this.NewLine +
                    "$('#" + this.MyID + "').button();" + this.NewLine +
                    "$('#" + this.MyID + "').click(function(e) {" + this.NewLine +
                        this.JsOnApply + this.NewLine +
                    "});" + this.NewLine +
                "});" + this.NewLine +
            "</script>";
        }
    }
}