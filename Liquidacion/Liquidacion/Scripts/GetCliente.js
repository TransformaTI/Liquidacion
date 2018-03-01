// This function calls the Web service method and 
// passes the event callback function.  
function ObtenerCliente(txtCliente, txtNombre)
{    

    
    var num = document.getElementById(txtCliente).value;
    
    
     
    if (num != '')
    {
    control = document.getElementById(txtNombre);
    DatosCliente.GetCliente(num, SucceededCallback);
    }
    else
    {
         //alert('No se encontró el Cliente');
    }
}

// This is the callback function invoked if the Web service
// succeeded.
// It accepts the result object as a parameter.
function SucceededCallback(result, eventArgs)
{
    // Page element to display feedback.
    
          ctrNombreCheque = document.getElementById('ctl00$MainPlaceHolder$txtNombreClienteCheque');      
          ctrNombreTarjeta = document.getElementById('ctl00$MainPlaceHolder$txtNombreClienteTarjeta');
          ctrNombreVale = document.getElementById('ctl00$MainPlaceHolder$txtValeNombre');
               
                        if(result != '')
                        {
                            ctrNombreCheque.value = result;
                            ctrNombreTarjeta.value = result;
                            ctrNombreVale.value = result;
                            
                        }
                        else
                        {
                            ctrNombreCheque.value = '';
                            alert('No se encontró el Cliente');
                            
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