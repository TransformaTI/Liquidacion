
function onKeyPress_OnlyDigits(evt)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    {
        return false;
    }
    else
    {
        return true;
    }
}

function keyPressNumeroCliente(evt, txtNumeroCliente, btnConsultaCliente)
{
    if (onKeyPress_OnlyDigits(evt) == true)
    {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 13)
        {

            
            if (document.getElementById(txtNumeroCliente).value.length > 0)
            {
                
                SeleccionarControlSiguiente(charCode, btnConsultaCliente);
            }
            else
            {
                return false;
            }
        }
    }
    else
    {
        return false;
    }
}

function SeleccionarControlSiguiente(charCode, nextControlName)
{    
    if (charCode == 13)
    {   
       
        console.log(nextControlName);
        document.getElementById(nextControlName).focus();
        return true;
    }
}

function NumeroRemisionKeyPress(evt, nextControlName)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    
    if (charCode == 13)
    {
        evt.keyCode = 27;
    }
    

    return SeleccionarControlSiguiente(charCode, nextControlName);
}

function ComboKeyPress(evt, nextControlName, alternateControlName)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    
    if (charCode == 13)
    {
        evt.keyCode = 27;
    }
    
    if (document.getElementById(nextControlName).disabled == true)
    {
        nextControlName = alternateControlName;
    }
            
    return SeleccionarControlSiguiente(charCode, nextControlName);
}

function onKeyPress_OnlyDecimalDigits(evt, name, nextControlName)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    
    if (charCode == 13)
    {
        evt.keyCode = 27;
    }
    
    if (SeleccionarControlSiguiente(charCode, nextControlName) == true)
    {
        return true;
    }
    
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    {
        if (charCode == 46)
        {
            if (document.getElementById(name).value.indexOf(".") >= 0)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    else
    {
        return true;
    }
}


function getObj(name)
{
    if (document.getElementById)
    {
        this.obj = document.getElementById(name);
        this.style = document.getElementById(name).style;
    }
    else if (document.all)
    {
        this.obj = document.all[name];
        this.style = document.all[name].style;
    }
    else if (document.layers)
    {
        this.obj = document.layers[name];
        this.style = document.layers[name].style;
    }
}

function DoPostback()
{

    javascript: __doPostBack('ConsultaPedido', '');
}



function doNumeroClienteSubmit(name, message)
{
  

    if (document.getElementById(name).value.length > 0)
    {
        return true;
    }
    else
    {
        alert(message);
        return false;
    }
}

function validacionCamposRequeridos(TextBoxLitros, MensajeLitros, BtnAceptar, ValidarRemision, TextBoxRemision, MensajeRemision)
{
    if (doNumeroClienteSubmit(TextBoxLitros, MensajeLitros) == true)
    {
        if (ValidarRemision == true)
        {
            return doNumeroClienteSubmit(TextBoxRemision, MensajeRemision);
        }
        else
        {
            //document.getElementById(BtnAceptar).disabled = true;
            return true;
        }
    }
    else
    {
        return false;
    }
}

function calcularImporte(ctrlLitros, ctrlPrecio, ctrlImporte)
{

	
    if (document.getElementById(ctrlLitros).value != ".")
    {
        document.getElementById(ctrlImporte).value = (document.getElementById(ctrlLitros).value * document.getElementById(ctrlPrecio).value);
    }
}

function validarDescuento(CtrlLitros, CtrlImporte, IDCtrlPrecio, CtrlPrecio, PrecioBase, TieneDescuento, Message1, Message2)
{
    var selNum  = CtrlPrecio.selectedIndex;
    var selText = CtrlPrecio.options[selNum].text;
    
    var precioMaximo = 0;
    var precioIteracion = 0;
    var indicePrecioMaximo = -1;

    for (var i=0; i < CtrlPrecio.length; i++)
    {
        if (precioMaximo == 0)
        {
            precioMaximo = Number(CtrlPrecio.options[i].text);
        }
        precioIteracion = Number(CtrlPrecio.options[i].text);
        if (precioIteracion >= precioMaximo)
        {
           precioMaximo = precioIteracion;
           indicePrecioMaximo = i;
        }
    }

    if (Number(selText) < PrecioBase)
    {
        CtrlPrecio.selectedIndex = indicePrecioMaximo;
        if (Boolean(TieneDescuento) == true)
        {
            alert(Message1);
        }
        else
        {
            alert(Message2);
        }
        return false;
    }
    else
    {
        calcularImporte(CtrlLitros, IDCtrlPrecio, CtrlImporte);
        return true;
    }
}

function TextToUpper(FolioRemision)
{
    var _folioRemision = document.getElementById(FolioRemision).value;
    document.getElementById(FolioRemision).value = _folioRemision.toString().toUpperCase();
}

function validarRemision(SerieRuta, FolioRemision, ValidarSerie, evt, nextControlName)
{   
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    
    if (charCode == 13)
    {
        evt.keyCode = 27;     
        
        var _folioRemision = document.getElementById(FolioRemision).value;
        var SerieRemision = "";
    
        if (_folioRemision.toString().length <= 0)
        {
            return SeleccionarControlSiguiente(charCode, nextControlName);
            return true;
        }
    
        if (ValidarSerie == true)
        {
            for (var myVariable = 0; myVariable <= _folioRemision.toString().length - 1; myVariable++ )
            {
                if (IsNumeric(_folioRemision.toString().charAt(myVariable)) == false)
                {
                    SerieRemision = SerieRemision + _folioRemision.toString().charAt(myVariable);
                }
                else
                {
                    break;
                }
            }
        
            if (SerieRemision.toString().toUpperCase() != SerieRuta.toString().toUpperCase())
            {
                alert("La serie de la ruta no corresponde con la serie de la remisión que capturó.");
                return false;
            }
        }
    
        //ValidarRemision(_folioRemision);    
        return SeleccionarControlSiguiente(charCode, nextControlName);
        return true;
    }
}

function IsNumeric(sText)
{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;
 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
            IsNumber = false;
         }
      }
   return IsNumber;
}

function ValidacionErrorConsistencia(Validar, CreditoAutorizado, CtrlCredito)
{
    if (Boolean(Validar) != true)
    {
        return true;
    }
    
    //Crédito
    if (Boolean(CreditoAutorizado) != true)
    {
        if (CtrlCredito[CtrlCredito.selectedIndex].value.toString().trim().toUpperCase() == "CREDITO")
        {
            alert("Debe liquidar de contado");
            return false;
        }
    }
}

function SetSelected(TextBox)
{
    if (TextBox != null)
    {
        if (document.getElementById(TextBox.id).value.length > 0)
        {
            TextBox.select();
        }
    }
}

function NumeroRemisionKeyPress(evt, nextControlName)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    
    if (charCode == 13)
    {
        evt.keyCode = 27;
    }
    

    return SeleccionarControlSiguiente(charCode, nextControlName);
}





//function AutoScrollContainer()
//{
//    //var objDiv = document.getElementById('ctl00_MainPlaceHolder_pnlPedidos');
//    var objDiv = document.getElementsByName("ContainerPedidos");
//    objDiv.scrollTop = 600;
//}