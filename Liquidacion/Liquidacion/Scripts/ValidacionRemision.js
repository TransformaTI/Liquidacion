// This function calls the Web service method and 
// passes the event callback function.  
function ValidarRemision(Remision)
{
    ValidacionRemision.RemisionExistente(Remision, SucceededCallback);
}

// This is the callback function invoked if the Web service
// succeeded.
// It accepts the result object as a parameter.
function SucceededCallback(result, eventArgs)
{
    // Page element to display feedback.
    if (result == true)
    {
        alert("Esta remisión ya se capturó.");
    }
}

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

function EndRequestHandler(sender, args)
{
   if (args.get_error() != undefined)
   {
       var errorMessage = args.get_error().message;
       args.set_errorHandled(true);
       ToggleAlertDiv('visible');
       $get(messageElem).innerHTML = errorMessage;
   }
   
   /*var objDiv = document.getElementById('ctl00_MainPlaceHolder_pnlPedidos');
   objDiv.scrollTop = 600;*/

}