/*
 * Check for JQuery and alert the user if it is not loaded.
 */

if (typeof jQuery == 'undefined') {
    alert('In order to use ef:button, you must have jquery loaded.');
}

var groupStatus = new Object();


function validateGroups(groupNames) {

    for (var i = 0; i < groupNames.length;  i++) {

        /*
         *  Tell ASP .NET on the client side to validate all groups but one at a time.
         */

        var obj = groupNames[i];

        if (!Page_ClientValidate(obj.Key)) {
            return false;
        } else {

            /*
             * We use jquery to make the nice smooth animation to show the next form panel.
             */

            if (obj.Value != "!END!") {

                if (!groupStatus[obj.Key]) {
                
                    $('#' + obj.Value).animate({ height: "show", opacity: "show" });

                    /*
                     * Keep track of previous panels that have been validated so that we validate all open panels.
                     */
                    
                    groupStatus[obj.Key] = true;
                    return false;
                }
                
                    
            } else {
                return true;
            }
        }
    }
    
    return true;
}