var controlCssCache = new Array();

/*
 * This function changes the focus color and makes sure that it saves the last
 * CSS state class applied so, the label style is always set back to its 
 * last style.
 */

function changeFocusCss(control, cssClass, controlLabelID, controlLabelCss) {

    if (cssClass != null) {

        controlCssCache[control.id] = control.className;
        control.className = cssClass;
        
        /*
         *  Alternative Label Color
         */
         
        
        if (controlLabelID != "" && controlLabelCss != "") {

            controlLabel = document.getElementById(controlLabelID);

            controlCssCache[controlLabel.id] = controlLabel.className;
            controlLabel.className = controlLabelCss;
        }
        
        
    } else {

        control.className = controlCssCache[control.id];


        /*
         *  Alternative Label Color
         */
        
        
        if (controlLabel != "" && controlLabelCss != "") {
            controlLabel.className = controlCssCache[controlLabel.id];
        }
    }
}