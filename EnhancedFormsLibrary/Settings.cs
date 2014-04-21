using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RDC.EnhancedForm
{
    public class Settings : Control
    {
        /*
         * Custom global settings specific to EnhancedFormLibrary
         */

        public string DefaultControlCssClass { get; set; }
        public string DefaultControlErrorCssClass { get; set; }
        public string DefaultLabelErrorCssClass { get; set; }

        /*
         * Custom global settings that apply to existing properties
         */

        public string DisplayValidationText { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            foreach(Control c in this.Page.Form.Controls)
            {
                Type classType = c.GetType();

                /*
                 * Get the underlying Web Control Class
                 */
                
                if(classType.Namespace.Equals("RDC.EnhancedForm"))
                {
                    // this.Page.Response.Write(classType.BaseType.BaseType.Name + "<br/>");

                    switch(classType.BaseType.BaseType.Name)
                    {
                        case "WebControl":

                            switch(classType.Name)
                            {
                                case "TextBox" :

                                    TextBox textbox = (TextBox)c;
                                    
                                    if(String.IsNullOrEmpty(textbox.CssClass))
                                    {
                                        textbox.CssClass = DefaultControlCssClass;
                                    }

                                    break;
                                
                                    /*

                                        case "Button" :


                                        break;
                                    */
                            }
                            
                            break;
                        
                        case "BaseValidator":
                            
                            IEnhancedFormValidator enhancedValidator = (IEnhancedFormValidator)c;
                            BaseValidator baseValidator = (BaseValidator)c;

                            if(baseValidator.Display == ValidatorDisplay.Static && String.IsNullOrEmpty(DisplayValidationText))
                            {
                                baseValidator.Display = (ValidatorDisplay)Enum.Parse(typeof(ValidatorDisplay), DisplayValidationText, true);
                            }
                            
                            if(String.IsNullOrEmpty(enhancedValidator.ControlErrorCssClass) && String.IsNullOrEmpty(DefaultControlErrorCssClass))
                            {
                                enhancedValidator.ControlErrorCssClass = DefaultControlErrorCssClass;
                            }

                            if(String.IsNullOrEmpty(enhancedValidator.ControlLabelErrorCssClass) && String.IsNullOrEmpty(DefaultLabelErrorCssClass))
                            {
                                enhancedValidator.ControlErrorCssClass = DefaultLabelErrorCssClass;
                            }

                            break;
                    }
                }
            }
        }
    }
}
