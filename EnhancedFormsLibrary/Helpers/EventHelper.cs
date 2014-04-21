using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RDC.EnhancedForm
{
    public class EventHelper
    {
        public static void OnLoad(BaseValidator validator)
        {
            ClientScriptManager cs = validator.Page.ClientScript;
            IEnhancedFormValidator enhancedFormControl = (IEnhancedFormValidator)validator;

            if(validator.EnableClientScript == true)
            {
                cs.RegisterClientScriptResource(typeof(IEnhancedFormValidator), "RDC.EnhancedForm.EnhancedClientValidation.js");

                Control control = validator.Parent.FindControl(validator.ControlToValidate);
                string controlToAffectID = "";

                if (control != null)
                {
                    if (enhancedFormControl.ControlLabel != null)
                    {
                        WebControl controlToAffect = (WebControl)validator.Parent.FindControl(enhancedFormControl.ControlLabel);
                        
                        if (controlToAffect != null)
                        {
                            controlToAffectID = controlToAffect.ClientID;
                        }
                    }

                    
                    if (!cs.IsClientScriptBlockRegistered(validator.GetType(), String.Format("HighlightValidation_{0}", validator.ClientID)))
                    {
                        /*
                         * Get the underlying original CSS settings for the controls so we can remember 
                         * the original CSS state before we change to error CSS classes.
                         */

                        WebControl controlToValidate = (WebControl)validator.Parent.FindControl(validator.ControlToValidate);
                        WebControl controlLabel = (WebControl)validator.Parent.FindControl(enhancedFormControl.ControlLabel);

                        string controlToAffectIDCssClass = "";
                        string controlToAffectIDErrorCssClass = "";

                        if(controlLabel != null)
                        {
                            controlToAffectIDCssClass = controlLabel.CssClass;
                            controlToAffectIDErrorCssClass = enhancedFormControl.ControlLabelErrorCssClass;
                        }
                        
                        cs.RegisterClientScriptBlock(validator.GetType(),
                                                     String.Format("pageEnhancedValidators{0}", validator.ClientID),
                                                     String.Format("pageEnhancedValidators.push(new Array('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'));",
                                                                   validator.ClientID,
                                                                   control.ClientID,
                                                                   controlToValidate.CssClass,
                                                                   enhancedFormControl.ControlErrorCssClass,
                                                                   enhancedFormControl.ErrorClientCallbackFunction,
                                                                   controlToAffectID,
                                                                   controlToAffectIDCssClass,
                                                                   controlToAffectIDErrorCssClass),

                                                     true);
                    }   
                }
            } 
        }


        /*
         * If java script is not enabbled, change CSS classes on post back.
         */

        public static void OnPreRender(BaseValidator validator)
        {
            IEnhancedFormValidator enhancedFormControl = (IEnhancedFormValidator)validator;

            StateBag pageViewState = enhancedFormControl.PageViewState;
            WebControl control = (WebControl)validator.Parent.FindControl(validator.ControlToValidate);

            if (control != null)
            {
                if (!validator.IsValid)
                {
                    control.ToolTip = validator.ToolTip;

                    if (pageViewState[control.ID + "CssClass"] == null)
                    {
                        pageViewState[control.ID + "CssClass"] = control.CssClass;
                        control.CssClass = enhancedFormControl.ControlErrorCssClass;
                    }
                }
                else
                {
                    if(pageViewState[control.ID + "CssClass"] != null)
                    {
                        control.CssClass = pageViewState[control.ID + "CssClass"].ToString();
                        pageViewState[control.ID + "CssClass"] = null;
                    }
                }
            }
            
            
            if (enhancedFormControl.ControlLabel != null && enhancedFormControl.ControlLabelErrorCssClass != null)
            {
                WebControl controltoAffect = (WebControl)validator.Parent.FindControl(enhancedFormControl.ControlLabel);

                if (controltoAffect != null)
                {
                    if (!validator.IsValid)
                    {
                        control.ToolTip = validator.ToolTip;

                        if (pageViewState[controltoAffect.ID + "CssClass"] == null)
                        {
                            pageViewState[controltoAffect.ID + "CssClass"] = controltoAffect.CssClass;
                            controltoAffect.CssClass = enhancedFormControl.ControlLabelErrorCssClass;
                        }
                    }
                    else
                    {
                        if (pageViewState[controltoAffect.ID + "CssClass"] != null)
                        {
                            controltoAffect.CssClass = pageViewState[controltoAffect.ID + "CssClass"].ToString();
                            pageViewState[controltoAffect.ID + "CssClass"] = null;
                        }
                    }
                }
            }
        }
    }
}