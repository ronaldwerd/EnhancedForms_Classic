using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("RDC.EnhancedForm.EnhancedClientTextBox.js", "text/javascript")]
namespace RDC.EnhancedForm
{
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        public String ClientFocusCssClass
        {
            get; 
            set; 
        }

        public String ClientFocusLabel
        { 
            get;
            set;
        }

        public String ClientFocusLabelCssClass 
        { 
            get;
            set; 
        }
        
        protected override void OnLoad(EventArgs e)
        {
            if(!string.IsNullOrEmpty(ClientFocusCssClass))
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterClientScriptResource(typeof(TextBox), "RDC.EnhancedForm.EnhancedClientTextBox.js");
            }

            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if(String.IsNullOrEmpty(ClientFocusLabel) && String.IsNullOrEmpty(ClientFocusLabelCssClass)) {

                writer.AddAttribute("onfocus", "changeFocusCss(this, '" + ClientFocusCssClass + "', null, null)");
                writer.AddAttribute("onblur", "changeFocusCss(this, null, null, null)");

            } else {

                /*
                 * Add the additional label focus
                 */

                writer.AddAttribute("onfocus", "changeFocusCss(this, '" + ClientFocusCssClass + "', '" + ClientFocusLabel + "', '" + ClientFocusLabelCssClass + "')");
                writer.AddAttribute("onblur", "changeFocusCss(this, null, '" + ClientFocusLabel + "', '" + ClientFocusLabelCssClass + "')");
            }
            
            base.Render(writer);
        }
    }
}
