using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RDC.EnhancedForm
{
    public class RangeValidator : System.Web.UI.WebControls.RangeValidator, IEnhancedFormValidator
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
