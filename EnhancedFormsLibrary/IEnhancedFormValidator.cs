using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RDC.EnhancedForm
{
    /// <summary>
    /// Interface to provide additional features over regular regular ASP .NET forms
    /// </summary>
    interface IEnhancedFormValidator
    {
        /// <summary>
        /// The error CSS class for the control that is being validated
        /// </summary>
        string ControlErrorCssClass
        {
            get;
            set;
        }

        /// <summary>
        /// The the control's label that is being validated
        /// </summary>
        string ControlLabel
        {
            set;
            get;
        }

        /// <summary>
        /// The the control's label css class when a validation error occurs
        /// </summary>
        string ControlLabelErrorCssClass
        {
            set;
            get;
        }


        /// <summary>
        /// Callback function to execute when a validation error occurs
        /// </summary>
        string ErrorCallbackFunction
        {
            get;
            set;
        }

        /// <summary>
        /// Javascript client callback function function to execute when a validation error occurs
        /// </summary>
        string ErrorClientCallbackFunction
        {
            get;
            set;
        }

        StateBag PageViewState
        {
            get;
        }
    }
}