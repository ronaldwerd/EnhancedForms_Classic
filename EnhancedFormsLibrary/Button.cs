using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;

[assembly: WebResource("RDC.EnhancedForm.EnhancedClientButton.js", "text/javascript")]
namespace RDC.EnhancedForm
{
    public class Button : System.Web.UI.WebControls.Button
    {
        public string ClientCallbackFuncOnValidationPass { get; set; }
        public string ValidationGroupOrder { get; set; }
        public string ValidationGroupEnd { get; set; }
        public bool ValidationGroupStepping { get; set; }

        /*
        protected override void Render(HtmlTextWriter writer)
        {
            if(!string.IsNullOrEmpty(ClientCallbackFuncOnValidationPass))
            {

            }

            base.Render(writer);
        }
        */

        private string findControlClientID(string controlName)
        {
            Control control = this.Page.FindControl(controlName);
            
            if(control == null)
            {
                throw new Exception("Invalid control ID: " + controlName);
            }
            else
            {
                return control.ClientID;
            }
        }

        private ArrayList parseKeyValues(string keyValues)
        {
            /*
             * We use an array list to make sure that everything happens in the correct order.
             * Using a hashtable does not gaurantee order.
             */

            ArrayList keyPairs = new ArrayList();

            string[] pairs = keyValues.Split(',');

            foreach(string pair in pairs)
            {
                string[] keyPair = pair.Split('=');
                
                Pair keyValue = new Pair();
                keyValue.Key = keyPair[0].Trim();
                keyValue.Value = findControlClientID(keyPair[1].Trim());
                keyPairs.Add(keyValue);
            }

            if(!string.IsNullOrEmpty(ValidationGroupEnd))
            {
                Pair keyValue = new Pair();
                keyValue.Key = ValidationGroupEnd;
                keyValue.Value = "!END!";
                keyPairs.Add(keyValue);
            }

            return keyPairs;
        }

        protected override void OnLoad(EventArgs e)
        {
            ClientScriptManager cs = this.Page.ClientScript;
            cs.RegisterClientScriptResource(typeof(Button), "RDC.EnhancedForm.EnhancedClientButton.js");

            if(ValidationGroupStepping == true && !string.IsNullOrEmpty(ValidationGroupOrder))
            {
                this.CausesValidation = false;

                string json = JsonConvert.SerializeObject(parseKeyValues(ValidationGroupOrder), Formatting.Indented);
                this.OnClientClick = "return validateGroups(" + json + ");";
            }

            base.OnLoad(e);
        }
    }
}
