using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

[assembly: WebResource("RDC.EnhancedForm.EnhancedClientValidation.js", "text/javascript")]
namespace RDC.EnhancedForm
{
    [ToolboxData("<{0}:MyLabel Text='MyLabel' BorderColor='Yellow' BackColor='Magenta' BorderWidth = '10'  runat='server'></{0}:MyLabel>")]
    public class RequiredFieldValidator : System.Web.UI.WebControls.RequiredFieldValidator, IEnhancedFormValidator
    {
        public string ControlErrorCssClass
        {
            get;
            set;
        }

        public string ControlLabel
        {
            set;
            get;
        }

        public string ControlLabelErrorCssClass
        {
            set;
            get;
        }

        public string ErrorCallbackFunction
        {
            get;
            set;
        }

        public string ErrorClientCallbackFunction
        {
            get;
            set;
        }

        public StateBag PageViewState
        {
            get { return ViewState; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EventHelper.OnLoad(this);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            EventHelper.OnPreRender(this);
        }
    }
}