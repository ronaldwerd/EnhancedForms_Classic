var oldValidatorUpdateDisplay;
var pageEnhancedValidators = new Array();

function newValidatorUpdateDisplay(val) {

    oldValidatorUpdateDisplay(val);

    for (i = 0; i < pageEnhancedValidators.length; i++) {
    
        /*
         * Apply our additional CSS styles over the regular ASP .NET validation
         */

        var validatorID = pageEnhancedValidators[i][0];
        var controlID = pageEnhancedValidators[i][1];
        var controlCssClassName = pageEnhancedValidators[i][2];
        var controlErrorCssClassName = pageEnhancedValidators[i][3];

        var errorCallFunctionFunction = pageEnhancedValidators[i][4];

        var controlCaptionID = pageEnhancedValidators[i][5];
        var controlCaptionCssClass = pageEnhancedValidators[i][6];
        var controlCaptionErrorCssClass = pageEnhancedValidators[i][7];

        if (val.id == validatorID && val.controltovalidate == controlID) {

            control = document.getElementById(controlID);
            controlCaption = document.getElementById(controlCaptionID);

            if (!val.isvalid) {
            
                control.className = controlErrorCssClassName;

                /*
                 * Affect the label control if applicable
                 */

                if (controlCaptionID != "" && controlCaptionErrorCssClass != "") {
                    controlCaption = document.getElementById(controlCaptionID);
                    controlCaption.className = controlCaptionErrorCssClass;

                    /*
                     * We need to update the array cache for the label focus events
                     */

                    if (controlCssCache != null) {
                        controlCssCache[controlCaptionID] = controlCaptionErrorCssClass;
                    }
                }
                    
                /*
                 * We need to update the array cache for the focus events
                 */
                
                if (controlCssCache != null) {
                    controlCssCache[controlID] = controlErrorCssClassName;
                }

            } else {

                control.className = controlCssClassName;

                if (controlCaptionID != "" && controlCaptionErrorCssClass != "") {
                    controlCaption = document.getElementById(controlCaptionID);
                    controlCaption.className = controlCaptionCssClass;

                    /*
                     * We need to update the array cache for the label focus events
                     */
                    
                    if (controlCssCache != null) {
                        controlCssCache[controlCaptionID] = controlCaptionCssClass;
                    }
                }
                
                /*
                 * We need to update the array cache for the focus events
                 */

                if (controlCssCache != null) {
                    controlCssCache[controlID] = controlCssClassName;
                }
            }
        }


        if (errorCallFunctionFunction != "" && eval("typeof(" + errorCallFunctionFunction + ")") == "function") {
        
            if (!val.isvalid) {
                eval(errorCallFunctionFunction + "(val)");
            }
        }
    }
}

window.onload = function() {
    oldValidatorUpdateDisplay = ValidatorUpdateDisplay;
    ValidatorUpdateDisplay = newValidatorUpdateDisplay;
}