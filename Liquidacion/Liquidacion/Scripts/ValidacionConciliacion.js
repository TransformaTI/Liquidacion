function ValidarConciliacion()
{
    PageMethods.ValidarConciliacionDeRemisiones(ValidarConciliacionOK);
}
   
function ValidarConciliacionOK(result)
{
    // Page element to display feedback.
    if (result != true)
    {
        alert("No ha conciliado todos los suministros, no puede continuar.");
        Sys.WebForms.PageRequestManager.getInstance().abortPostBack();
    }
}


