﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="FormaPago.aspx.cs" Inherits="FormaPago" Title="Captura de formas de pago" Theme="Theme1" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ccR" %>

<%@ Register Src="~/ControlesUsuario/wucConsultaCargoTarjetaCliente.ascx" TagPrefix="uc1" TagName="wucConsultaCargoTarjetaCliente" %>
<%@ Register Src="~/ControlesUsuario/wucDetalleFormaPago.ascx" TagPrefix="ucDetallePago" TagName="wucDetalleFormaPago" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <%--  --%>

   
    <script type="text/javascript">
        var Letra = '';
        var IndexBancoSeleccionado = 0;
       
    
       function SetContextKey() {
        $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=ddBancoTarjeta.ClientID %>").value+'-'+$get("<%=TxtAfiliacion.ClientID %>").value);
        }


        function integratorsPopulated(source, eventArgs)
        {
	        if (source._currentPrefix != null)
	        {
		        var list = source.get_completionList();
		        var search = source._currentPrefix.toLowerCase();
		        for (var i = 0; i < list.childNodes.length; i++)
		        {
			        var text = list.childNodes[i].innerHTML;
			        var index = text.toLowerCase().indexOf(search);
			        if (index != -1)
			        {
				        var value = text.substring(0, index);
				        value += '<span style="color:red;font-weight:bold">';
				        value += text.substr(index, search.length);
				        value += '</span>';
				        value += text.substring(index + search.length);
				        list.childNodes[i].innerHTML = value;
			        }
                }

	        }
        }



        function AutoCompletedClientItemSelected(source, eventArgs) {
           
            document.getElementById('<%= TxtAfiliacion.ClientID %>').value = eventArgs.get_value();
    }   
        function ValidaAfiliacion() {

            var afiliaciones=document.getElementById('<%=HiddenAfiliaciones.ClientID %>').value

            var afiliacion = document.getElementById('<%= TxtAfiliacion.ClientID %>').value;
            var n = afiliaciones.includes(afiliacion, 0);

            //alert(afiliaciones);
            //alert(afiliacion);
            //alert(n);

            if (n == -1) 
            {
                alert('La afiliación es invalida');
                return false;
            }
            else
            {
                return true;
               }

        }


function SeleccionBanco(e){
    var keynum;
    var inputtxt = '';

    var Banco = '';
    var arrBancos = [];



    $('#' + '<%= ddBancoTarjeta.ClientID %>' + ' > option').each(function () {
        arrBancos.push($(this).text());
     });


    if (window.event) { // IE     
        //alert('IE')
        keynum = e.keyCode;
    }
    else if (e.which) { // Netscape/Firefox/Opera                   
        keynum = e.which;
         //alert('CHROME')
    }



    inputtxt=String.fromCharCode(keynum)

   var letters = /^[A-Za-z]+$/;
   if(letters.test(inputtxt)   )
     {
       Letra = inputtxt;
   }
   else if(keynum == '40' || keynum =='38' )
   {
       SeleccionaUnBanco(Letra, keynum,  arrBancos);
   }
}

        function SeleccionaUnBanco(Letra, keynum, arrBancos) {
            var IndiceInicial = $('#' + '<%= ddBancoTarjeta.ClientID %>')[0].selectedIndex;
            var BancoSeleccionado = false;
            var Banco = ' ';

           
 
            

             if (keynum == '40' || keynum =='38'  || Letra != '')
             {   
                
                    if (IndiceInicial <=arrBancos.length)
                    {

                        if (keynum == '40' && IndiceInicial>=0)
                        {
                             BancoSeleccionado = false;
                            for (var i = IndiceInicial; i <=arrBancos.length; i++){
                                Banco = String(arrBancos[i]);

                                        if ( Banco.startsWith(Letra)==true && BancoSeleccionado==false)
                                        {
                                            IndexBancoSeleccionado = i;                                 
                                            
                                            BancoSeleccionado = true;
                                        }
                            }
                        }

                        else if (keynum == '38' && IndiceInicial>=0 && IndiceInicial<arrBancos.length )
                        {
                            BancoSeleccionado = false;
                              for (var i = IndiceInicial; i >=1; i--){
                                   
                                Banco = String(arrBancos[i]);

                                        if ( Banco.startsWith(Letra)==true && BancoSeleccionado==false)
                                        {
                                            IndexBancoSeleccionado = i;                                            
                                            BancoSeleccionado = true;
                                        }
                            }

                        }


                    }
            }
            if (IndexBancoSeleccionado > 0)
            {
                document.getElementById('<%= ddBancoTarjeta.ClientID %>').selectedIndex = IndexBancoSeleccionado.toString();
            }
        }
        



    function cierraCheque()
    { 
        document.getElementById('<%= txtNumCuenta.ClientID %>').focus();
        document.getElementById('ctl00_mostrando').value = '';
    }

    function cierraVale()
    {       
        document.getElementById('<%= ddlProveedor.ClientID %>').focus()
        document.getElementById('ctl00_mostrando').value = '';
    }

    function cierraTarjeta()
    {       
        document.getElementById('<%= ddlTAfiliacion.ClientID %>').focus();
        document.getElementById('ctl00_mostrando').value = '';
    }

    function muestraCalendario()
    {
        document.getElementById('ctl00_mostrando').value='x';
    }


    </script>

    <script type="text/javascript" language="javascript">  

        //Variables
        var respuesta = false;
        var smMensajeCargo = 'El cliente tiene un cargo, ¿quiere utilizarlo?'; // mcc 2018 05 10
        var ModalProgress = '<%= ModalProgress.ClientID %>';
        var HiddenInput = '<%= HiddenInput.Value %>';   // mcc 2018 05 10
        var HiddenInputPCT = '<%= HiddenInputPCT.Value %>';   // mcc 2018 05 10
        var NumCte = '<%= txtClienteTarjeta.Text %>'; // mcc 2018 05 10
        var NumPagos = '<%=Session["TDCdisponibles"]!=null?Session["TDCdisponibles"].ToString():"0" %>'; // mcc 2018 05 10
        var Ruta = '<%=Session["Ruta"]%>';
        var sTipoPago = '';
        var RegistroCobro = '<%= wucDetalleFormaPago1.RegistroCobro%>';
        var FolioPrimerReg ='<%= Session["PrimerRegTDC"] %>';
        var HiddenPagosOtraRuta='<%= HiddenPagosOtraRuta.Value %>';
        var HiddenTDCDupliado = '<%= HiddenTDCDupliado.Value %>';
        var segmento = '';
        var NombreClienteCheque='<%= HiddenNomCteCheque.Value %>'; 
        var NombreClienteVale='<%= HiddenNomCteVale.Value %>'; 
        var NombreClienteTrans='<%= wucDetalleFormaPago1.NombreClienteTrans %>'; 
        var NumCteTrans = '<%= wucDetalleFormaPago1.TxtIdCliente.Text %>';
        var NomCteAnticipo='<%=wucDetalleFormaPago1.NombreCteAnticipo != null ? wucDetalleFormaPago1.NombreCteAnticipo.Trim():"" %>';
        var NumCteAnticipo='<%=wucDetalleFormaPago1.TxtAntIdCliente.Text != null ? wucDetalleFormaPago1.TxtAntIdCliente.Text.ToString().Trim():"" %>';

        var NomCteTarjeta = '<%= txtNombreClienteTarjeta.Text.Trim() %>';
        var PostBack_trasferencia = '<%=wucDetalleFormaPago1.PostBack != null ? wucDetalleFormaPago1.PostBack:"" %>';

  
        

        //Validaciones  On load
        document.addEventListener("DOMContentLoaded", function ()
        { // mcc 2018 05 10

 

            if (RegistroCobro == 'Si')
            {
                window.location='RegistroPagos.aspx'
            }


            if (HiddenInput == 'ConsultaTPV' || HiddenInput == 'SeleccionaPago') {
                document.getElementById('tarjeta').style.display = 'inherit';
            }

            if (HiddenInput == 'ConsultaTPV-Trans' || HiddenInput == 'SeleccionaPago-Trans'  ) {
                document.getElementById('transferencia').style.display = 'inherit';
            }

            
            if (HiddenInputPCT == 'Si' && (HiddenInput == 'ConsultaTPV' || HiddenInput == 'ConsultaTPV-Trans') && FolioPrimerReg != '0') {
                var respuesta = confirm(smMensajeCargo);
                var bandera = document.getElementById('ctl00_MainPlaceHolder_hfCargoTarjetaEncontrado');
                bandera.value = respuesta;

              
                if (respuesta == false)
                {
                    document.getElementById('ctl00_MainPlaceHolder_txtFechaTarjeta').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtFechaTarjeta').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_imgCalendario0').disabled = false;
                    document.getElementById('ctl00_MainPlaceHolder_txtNoAutorizacionTarjeta').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtNoAutorizacionTarjeta').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_txtNoAutorizacionTarjetaConfirm').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtNoAutorizacionTarjetaConfirm').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_txtNumTarjeta').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtNumTarjeta').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_txtImporteTarjeta').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtImporteTarjeta').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_txtObservacionesTarjeta').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_txtObservacionesTarjeta').readOnly = false;
                    document.getElementById('ctl00_MainPlaceHolder_ddTipTarjeta').selectedIndex = "0";
                    document.getElementById('ctl00_MainPlaceHolder_ddTipTarjeta').disabled = false;
                    document.getElementById('ctl00_MainPlaceHolder_ddBancoTarjeta').selectedIndex = "0";
                    document.getElementById('ctl00_MainPlaceHolder_ddBancoTarjeta').disabled = false;
                    document.getElementById('ctl00_MainPlaceHolder_ddlBancoOrigen').selectedIndex = "0";
                    document.getElementById('ctl00_MainPlaceHolder_ddlBancoOrigen').disabled = false;
                    document.getElementById('ctl00_MainPlaceHolder_chkLocal').checked = false;
                    document.getElementById('ctl00_MainPlaceHolder_chkLocal').disabled = false;
                    //document.getElementById('ctl00_MainPlaceHolder_ddlTAfiliacion').selectedIndex = "0";
                    //document.getElementById('ctl00_MainPlaceHolder_ddlTAfiliacion').disabled = false;
                    document.getElementById('ctl00_MainPlaceHolder_TxtAfiliacion').value = '';
                    document.getElementById('ctl00_MainPlaceHolder_TxtAfiliacion').readOnly = false;
                    document.getElementById('<%=HiddenInputPCT.ClientID%>').value = "No";

                    
                    <%--                    <%Session["BancoTarjetaSeleccionado"] = "";%>
                    <%Session["NombreBancoTarjetaSeleccionado"] = "";%>
                    <%Session["AfiliacionSeleccionada"] = "";%>--%>

                   

                 
                }
                
                if (respuesta == true) {
                
                    ShowModalPopup();
                }


            }


            
//HiddenInputPCT != 'Si' && HiddenInputPCT!= '' &&

            if (NumCte != '' && NomCteTarjeta == 'INACTIVO' && HiddenInput == 'ConsultaTPV')
              {
                 document.getElementById('<%= txtNombreClienteTarjeta.ClientID %>').value='' ;
                alert('¡Cliente inactivo!');
                document.getElementById('<%= txtClienteTarjeta.ClientID %>').value='' ;
            } 

            if ( HiddenInputPCT== 'No' && NumCte != '' && HiddenInput!='ConsultaCteAnticipo' && (HiddenTDCDupliado=='' || HiddenTDCDupliado=='No') && HiddenPagosOtraRuta=='' && NomCteTarjeta!='' && HiddenInput == 'ConsultaTPV') {
                alert('No se encontraron pagos de TPV para el cliente, por favor verifique con el área de tarjetas de crédito');
                document.getElementById('ctl00_MainPlaceHolder_ddBancoTarjeta').selectedIndex = "0";
                document.getElementById('ctl00_MainPlaceHolder_ddBancoTarjeta').disabled = false;
                segmento = 'tarjeta';  

            }
            else {
                segmento = 'tarjeta';                
            }

            if (HiddenPagosOtraRuta == 'true')
            {
                 alert('¡Existen cargos para el cliente que pertenecen a otra ruta . No se encontraron pagos de TPV que corresponda a la ruta y autotanque, porfavor verifique con el área de tarjetas de crédito.!');
            }

            if (HiddenInput == "ConsultaCteAnticipo" )
            {
                    document.getElementById('AnticipoUC').style.display = 'inherit';
                    document.getElementById('Anticipo').style.display = 'inherit'; 
                    document.getElementById('Transfer').style.display = 'none';
                    segmento='anticipo'
                
                    
            }     



           if (HiddenInput == "TarjetaClienteFalse")
           {
              document.getElementById('terjeta').style.display = 'inherit';
            }

            if (HiddenInput == "ConsultaCteTransferencia" ||  PostBack_trasferencia=='CuentasBancarias')
            {

                document.getElementById('Anticipo').style.display = 'none'; 
                document.getElementById('AnticipoUC').style.display = 'inherit';
                document.getElementById('Transfer').style.display = 'inherit';
                segmento = 'transferencia';

            }



            if (segmento == 'tarjeta') {
                document.getElementById('<%=txtFechaTarjeta.ClientID%>').focus();
            }
            
            if (segmento == 'anticipo') {
                document.getElementById('ctl00_MainPlaceHolder_wucDetalleFormaPago1_LstSaldos').focus();
            }



            if (segmento == 'transferencia' && PostBack_trasferencia=='') {
         
                document.getElementById('ctl00_MainPlaceHolder_wucDetalleFormaPago1_txtFecha').focus();
            }

            if (PostBack_trasferencia=='CuentasBancarias' && segmento == 'transferencia')
             {
         
                document.getElementById('ctl00_MainPlaceHolder_wucDetalleFormaPago1_ddlBancoDestino').focus();
            }

               if (NombreClienteCheque!='' )
            {        

              document.getElementById('cheque').style.display = 'inherit';
              document.getElementById('Transfer').style.display = 'none';  
              document.getElementById('AnticipoUC').style.display = 'none';
              document.getElementById('Transfer').style.display = 'none'; 
              document.getElementById('tarjeta').style.display = 'none'; 
             

              if (NombreClienteCheque != 'CTENOEXISTE' &&  NombreClienteCheque!= 'INACTIVO' )
                 {
                    document.getElementById('ctl00_MainPlaceHolder_txtNombreClienteCheque').value = NombreClienteCheque;
                    document.getElementById('ctl00_MainPlaceHolder_txtFechaChueque').focus();
              }
              else if (NombreClienteCheque == 'INACTIVO')
              {
                  alert('¡Cliente inactivo!');
                   document.getElementById('ctl00_MainPlaceHolder_txtClienteCheque').value = '';

                  document.getElementById('cheque').style.display = 'inherit';
              }

                 else
                {
                  alert('¡El cliente no existe!');
                  document.getElementById('ctl00_MainPlaceHolder_txtClienteCheque').value = '';
                  document.getElementById('ctl00_MainPlaceHolder_txtClienteCheque').focus();
                 
                 }

               NombreClienteCheque = '';

            }
            else
            {
                document.getElementById('cheque').style.display = 'none';
            }

         if (NombreClienteVale!='' )
            {
              
              document.getElementById('vale').style.display = 'inherit';
              document.getElementById('AnticipoUC').style.display = 'none';
              document.getElementById('Transfer').style.display = 'none'; 
             document.getElementById('tarjeta').style.display = 'none'; 
             document.getElementById('cheque').style.display = 'none';

                         if (NombreClienteVale != 'CTENOEXISTE'  &&  NombreClienteVale!= 'INACTIVO' )
                         {
                             document.getElementById('ctl00_MainPlaceHolder_txtValeNombre').value = NombreClienteVale;
                             document.getElementById('ctl00_MainPlaceHolder_txtValeFecha').focus();
                         }
                          else if (NombreClienteVale == 'INACTIVO')
                         {
                 
                    
                             //document.getElementById('vale').style.display = 'inherit';
                             document.getElementById('ctl00_MainPlaceHolder_txtClienteVale').value = '';
                             //document.getElementById('ctl00_MainPlaceHolder_txtClienteVale').focus();
                             alert('¡Cliente inactivo!');                         



                         }

                             else
                             {
                                 alert('¡El cliente no existe!');
               
                                 document.getElementById('ctl00_MainPlaceHolder_txtClienteVale').value = '';
                                 document.getElementById('ctl00_MainPlaceHolder_txtClienteVale').focus();


                             }
                             NombreClienteVale='';
            }
             else
            {
                document.getElementById('vale').style.display = 'none';
            }    

            if (NumCteAnticipo != '' && NomCteAnticipo == '' && HiddenInput=='ConsultaCteAnticipo')
            {
                alert('¡El cliente no existe!');
            }  

            if (NumCteAnticipo != '' && NomCteAnticipo == 'INACTIVO' && HiddenInput=='ConsultaCteAnticipo')
            {
                alert('¡Cliente inactivo!');
                document.getElementById('ctl00_MainPlaceHolder_wucDetalleFormaPago1_txtAntCliente').value = '';

            }  
            

            if (NumCteTrans != '' && NombreClienteTrans == '' && HiddenInput == 'ConsultaCteTransferencia')
            {
                alert('¡El cliente no existe!');
                
            }  

            if (NumCteTrans != '' && NombreClienteTrans == 'INACTIVO' && HiddenInput == 'ConsultaCteTransferencia')
            {
                alert('¡Cliente inactivo!');
                document.getElementById('ctl00_MainPlaceHolder_wucDetalleFormaPago1_txtCliente').value = '';
            }  


           if (NumCte != '' && NomCteTarjeta == '' && HiddenInput == 'ConsultaTPV')
            {
                  alert('¡El cliente no existe!');
            }  

              
        });        

         function onlyNumbers(evt) {
          evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
          if (charCode > 31 && (charCode < 48 || charCode > 57)) {

              return false;
          }
          return true;
             }

        function onlyNumbersDecimals(evt) {
          evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
          if (charCode > 31 && (charCode < 46  || charCode > 57 || charCode == 47 )) {

              return false;
          }
            return true;
        }

         function isAlphaNumeric(str) {
          var code, i, len;
        
          for (i = 0, len = str.length; i < len; i++) {
            code = str.charCodeAt(i);
            if (!(code > 47 && code < 58) && // numeric (0-9)
                !(code > 64 && code < 91) && // upper alpha (A-Z)
                !(code > 96 && code < 123)) { // lower alpha (a-z)
              return false;
            }
          }
          return true;
         };

         function imbResumenClick()
         {
             window.location = 'GenerarPago.aspx';

        }



    

         function toggle(display, activo, inactivo, inactivoA, control) {
             document.getElementById('ctl00_mostrando').value = ''
             
            document.getElementById('tarjeta').style.display = 'none';
            document.getElementById('cheque').style.display = 'none';
            document.getElementById('vale').style.display = 'none';
            document.getElementById('Transfer').style.display = 'none';
            document.getElementById('Anticipo').style.display = 'none';
            document.getElementById('AnticipoUC').style.display = 'none';


            document.getElementById(activo).style.display = (
                document.getElementById(activo).style.display == 'none') ? 'block' : 'none';

            document.getElementById(inactivo).style.display = 'none';
            document.getElementById(inactivoA).style.display = 'none';

            if (activo == 'Transfer') {
                document.getElementById('AnticipoUC').style.display = 'inherit';
                document.getElementById('Transfer').style.display = 'inherit';
                document.getElementById('Anticipo').style.display = 'none'; 
           
            }


            if (activo == 'Anticipo') {

                document.getElementById('AnticipoUC').style.display = 'inherit';
                   document.getElementById('Anticipo').style.display = 'inherit'; 
                document.getElementById('Transfer').style.display = 'none';     
            
            }

            if (activo = 'tarjeta') {
                document.getElementById('ctl00_MainPlaceHolder_txtClienteTarjeta').value = ""
                
                
            }

            $("input[type='text']:visible:enabled:first").focus();
                      
        }

    </script>
    
   

    <script type="text/javascript">
        function confirmar(button) {


            if (document.getElementById(button).disabled == true) {
                var answer = confirm('¿Desea agregar el Pago?')
                if (answer)
                    document.getElementById(button).disabled = false;

            }
        }
        //Consulta Pagos Con tarjeta mcc 2018 05 10
        function ConsultaPagosTPV(FormaPago) {

            if (document.getElementById('<%=txtClienteTarjeta.ClientID%>').value == "") {               
                HiddenInput == ''

            }
            else{
                if ((document.getElementById('<%=txtClienteTarjeta.ClientID%>').value != "" || document.getElementById('<%=TxtCteAfiliacion.ClientID%>').value != "")
                    && (HiddenInput == '' || HiddenInput == 'SeleccionaPago' || HiddenInput == 'ConsultaTPV')) {
                    javascript: __doPostBack(FormaPago, '');

                }
            }

        }

        function AgregarCargoTarjeta() {
            javascript: __doPostBack('AgregarCargoTarjeta', '');
        }

        function ConsultaPagosSeleccion(FormaPago, Llave) {
            javascript: __doPostBack(FormaPago + ' SeleccionaPago=' + Llave, '');


        }




        function MuestraAfiliacionesTodas() {
            
            <%--document.getElementById('<%=TxtAfiliacion.ClientID%>').focus();--%>

            SetContextKey();

              var autoComplete = $find('<%=AutoCompleteExtender1.ClientID%>');

                // To show the popup by JavaScript, following code is necessary.
                    // [NOTE] This trick is weak if the AutoCompleteExtender is updated.
                    autoComplete._cache = {};
                    autoComplete._currentPrefix = null;
                    autoComplete._textBoxHasFocus = true;


                    // save the old minimum prefix length, and set the value 0
                    var oldMinimumPrefixLength = autoComplete.get_minimumPrefixLength();
                    autoComplete.set_minimumPrefixLength(0);


                    // send the event to show the popup
                    autoComplete._onTimerTick(null, null);    

                    // restore the old minimum prefix length
                   autoComplete.set_minimumPrefixLength(oldMinimumPrefixLength);                      
        }



        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {

            document.getElementById('<%=TxtAfiliacion.ClientID%>').disabled = false;
            document.getElementById('<%=TxtAfiliacion.ClientID%>').readOnly = false;

            document.getElementById('<%=ddBancoTarjeta.ClientID%>').disabled = false;
            document.getElementById('<%=ddBancoTarjeta.ClientID%>').readOnly = false;

            document.getElementById('<%=txtNoAutorizacionTarjeta.ClientID%>').disabled = false;
            document.getElementById('<%=txtNoAutorizacionTarjeta .ClientID%>').readOnly = false;

            document.getElementById('<%=txtNoAutorizacionTarjetaConfirm.ClientID%>').disabled = false;
            document.getElementById('<%=txtNoAutorizacionTarjetaConfirm .ClientID%>').readOnly = false;

            document.getElementById('<%=txtNumTarjeta.ClientID%>').disabled = false;
            document.getElementById('<%=txtNumTarjeta .ClientID%>').readOnly = false;

            document.getElementById('<%=ddlBancoOrigen.ClientID%>').disabled = false;
            document.getElementById('<%=ddlBancoOrigen .ClientID%>').readOnly = false;

             document.getElementById('<%=txtClienteTarjeta.ClientID%>').value = '';
             document.getElementById('<%=txtNombreClienteTarjeta.ClientID%>').value = '';
            document.getElementById('<%=TxtAfiliacion.ClientID%>').value = '';
            document.getElementById('<%=txtNoAutorizacionTarjeta.ClientID%>').value = '';
            document.getElementById('<%=txtNoAutorizacionTarjetaConfirm.ClientID%>').value = '';
            document.getElementById('<%=txtNumTarjeta.ClientID%>').value = '';
            document.getElementById('<%= ddTipoTarjeta.ClientID%>').selectedIndex = "0"; 
            document.getElementById('<%= ddBancoTarjeta.ClientID%>').selectedIndex = "0"; 

            
            $find("mpe").hide();
            return false;
        }

        
        function ValidaCamposCheque() {
            
            var afiliacionValida = "1";
            if (document.getElementById('<%=txtClienteCheque.ClientID%>').value == "") {
                alert('Capture el número de cliente');
                return false;
            }             

            PostBack();
        }

        function PostBack() {
           
            javascript: __doPostBack('', '');         
        }

        function ValidaCamposTDC()
        {
            var afiliacionValida = "1";
            if (document.getElementById('<%=txtClienteTarjeta.ClientID%>').value == "")
            {
                alert('Capture el número de cliente');
                 return false;
            }

                if (document.getElementById('<%=txtNombreClienteTarjeta.ClientID%>').value == "")
                 {
                     alert('¡El cliente no existe, capture un número de cliente valido!');
                    return false;
                 }
        




            if (document.getElementById('<%=HiddenInputPCT.ClientID%>').value == "No" ||   document.getElementById('<%=HiddenInputPCT.ClientID%>').value== "")
            {


                var afiliacionSeleccionada = document.getElementById('<%=TxtAfiliacion.ClientID%>').value;

                var ddlBanco = document.getElementById('<%= ddBancoTarjeta.ClientID %>');
                var bancoSeleccionado = ddlBanco.options[ddlBanco.selectedIndex].value;
               

                if (bancoSeleccionado == "0")
                {
                    alert('Seleccione un banco de la tarjeta');
                    return false;
                }

                if (afiliacionSeleccionada == "")
                {
                    alert('Seleccione una afiliación');
                    return false;
                }

                $.ajax({
                type: "POST",
                url: "FormaPago.aspx/validaAfiliacion",
                data: '{bancoSeleccionado: "' + bancoSeleccionado + '",  afiliacionSeleccionada: "' + afiliacionSeleccionada + '" }',
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (response) {
                    
                        if (response.d=="0") 
                        {
                            alert('Seleccione una afiliación válida');
                            afiliacionValida = "0";
                        }

                    },
                error: function (r) {
                    alert(r.responseText);
                    afiliacionValida = "0";
                },
                failure: function (r) {
                    alert(r.responseText);
                    afiliacionValida = "0";
                }
            });

                if (afiliacionValida == "0")
                {
                    return false;
                }

                if (document.getElementById('<%=txtNoAutorizacionTarjeta.ClientID%>').value == "")
                {
                        alert('Capture el número de Autorizacion');
                        return false;
                }

                if (document.getElementById('<%=txtNoAutorizacionTarjetaConfirm.ClientID%>').value == "")
                {
                        alert('Confirme el número de autorización');
                        return false; 
                }
                if (document.getElementById('<%=txtNoAutorizacionTarjetaConfirm.ClientID%>').value != document.getElementById('<%=txtNoAutorizacionTarjeta.ClientID%>').value)
                {
                    alert('Los números de autorización no coinciden');
                    return false;
                }

               
                if (document.getElementById('<%=ddlBancoOrigen.ClientID%>').selectedIndex == "0")
                {
                    alert('Seleccione un banco origen');
                    return false;
                }
                if (document.getElementById('<%=txtImporteTarjeta.ClientID%>').value == "")
                {
                    alert('Capture el Importe');
                    return false;
                }

             


            }  

            function ValidaFechasCheque()
            {
                alert('VALIDACIONES');
    <%--             var FechaCheque = new Date('<%=Convert.ToDateTime(txtFechaChueque.Text).Year%>','<%=Convert.ToDateTime(txtFechaChueque.Text).Month%>', '<%=Convert.ToDateTime(txtFechaChueque.Text).Day%>');
                var FechaCobro = new Date('<%=Convert.ToDateTime(TxtFechaCobro.Text).Year%>', '<%=Convert.ToDateTime(TxtFechaCobro.Text).Month%>', '<%=Convert.ToDateTime(TxtFechaCobro.Text).Day%>'); //Year, Month, Date

             if (FechaCheque >= FechaCobro) {

                   return true;

                 }else {

                  alert('La fecha de cobro debe ser mayor igual que la fecha del documento');
                   return false;

                 }--%>

            }

        }

    </script>

    <script type="text/javascript" language="JavaScript">
        function HideContent(d) {
            if (d.length < 1) { return; }
            document.getElementById(d).style.display = "none";
        }
        function ShowContent(d) {
            if (d.length < 1) { return; }
            document.getElementById(d).style.display = "block";
        }
        function ReverseContentDisplay(d) {
            if (d.length < 1) { return; }
            if (document.getElementById(d).style.display == "none") { document.getElementById(d).style.display = "block"; }
            else { document.getElementById(d).style.display = "none"; }
        }
    </script>
    <script type="text/javascript"> 
        function txtCuentaDocumento() {

            var str = document.getElementById('<%=txtLectorCheque.ClientID%>').value;

            ctrCuenta = document.getElementById('<%=txtNumCuenta.ClientID%>');
            if (ctrCuenta != null) {
                ctrCuenta.value = str.substring(13, 24);
            }
            ctrDocumento = document.getElementById('<%=txtNumeroCheque.ClientID%>');
            if (ctrDocumento != null) {
                ctrDocumento.value = str.substring(24, 31);
            }
        }
        //function test(txtCliente) {
        //    var test = document.getElementById(txtCliente).value;
        //    alert(test);
        //}
    </script>
    <script type="text/javascript"> 
        function txtImagenTipoCobro(tipo) {
            var str = '';
            if (tipo == "TARJETA") {
                str = '~/Images/imgTarjetaCredito.ico';
            }

            if (tipo == "CHEQUE") {
                str = '~/Images/imgTipoCobro.gif';
            }

            if (tipo == "VALE") {
                str = '~/Images/imgMonto.bmp';
            }
            return str;
        }
    </script>


    <script type="text/javascript">
             function ConsultaClienteCheque(TipoPago) {

            if (TipoPago=='cheque')
            {
                javascript: __doPostBack('ConsultaClienteCheque', '');
                var fechaCheque = document.getElementById('ctl00_MainPlaceHolder_txtFechaChueque');
                fechaCheque.focus();
            }

            if (TipoPago=='vale')
            {
                javascript: __doPostBack('ConsultaClienteVale', '');
                var fechaCheque = document.getElementById('ctl00_MainPlaceHolder_txtValeFecha');
                fechaCheque.focus();
            }
        }
    </script>

    <script src="Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script src="Scripts/MiscFunctions.js" type="text/javascript"></script>


    <div style="text-align: center; vertical-align: top;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/GetCliente.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="~/WebServices/DatosCliente.asmx" />
            </Services>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
            <ContentTemplate>
                <asp:HiddenField ID="hfCargoTarjetaEncontrado" runat="server" Value="" />
                <asp:HiddenField ID="HiddenInput" runat="server" Value="" />
                <asp:HiddenField ID="HiddenInputPCT" runat="server" Value="" />
                <asp:HiddenField ID="HiddenInputNumPagos" runat="server" Value="" />
                <asp:HiddenField ID="HiddenTDCDupliado" runat="server" Value="" />
                <asp:HiddenField ID="HiddenPagosOtraRuta" runat="server" Value="" />
                <asp:HiddenField ID="HiddenNomCteCheque" runat="server" Value="" />
                <asp:HiddenField ID="HiddenNomCteVale" runat="server" Value="" />
                <asp:HiddenField ID="HiddenAfiliaciones" runat="server" Value="" />

                <div style="text-align: left; height: 650px; width: 1000px; vertical-align: top;">
                    <table style="vertical-align: top; height: 650px;">
                        <tr>
                            <td valign="top">
                                <table style="vertical-align: top;">
                                    <tr>
                                        <td>
                                            <asp:Image ID="imgCheque" runat="server" Height="26px" Width="137px" 
                                                ImageUrl="~/Images/imgCapturarCheque.png"  />

                                        </td>
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgTarjeta" runat="server" Height="26px" Width="137px" 
                                                    ImageUrl="~/Images/imgCapturarTarjeta.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Image ID="ImgTransferencia" runat="server" Height="26px" Width="137px" 
                                                    ImageUrl="~/Images/imgCapturarTrans.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgVale" runat="server" Height="26px" Width="137px" 
                                                    ImageUrl="~/Images/imgCapturarVale.png"  />
                                            </td>
                                        </tr>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="ImgAnticipo" runat="server" Height="26px" Width="137px" 
                                                ImageUrl="~/Images/imgCapturarAnticipo.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imbEfectivo" runat="server"  Height="26px" Width="137px" 
                                                ImageUrl="~/Images/imgPagoEfectivo.png" OnClick="imbEfectivo_Click"
                                                 />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imbCancelar" runat="server" Height="26px" Width="137px" 
                                                ImageUrl="~/Images/imgCancelarPagos.png" OnClick="imbCancelar_Click"
                                                />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="imbResumen" runat="server" Height="25px" 
                                                ImageUrl="~/Images/captPagos.png" 
                                                Width="74px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblError" runat="server" CssClass="labeltipopagoforma" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                
                        </tr>
                    </table>
                    </td>
                            <td valign="top">
                                <table style="vertical-align: top; width: 534px;">


                                    <tr>
                                        <td valign="top" style="vertical-align: top;">
                                            <div id="cheque" style="display: none; vertical-align: top;">
                                                <table style="background-color: #e1f8e2; height: 360px; width: 900px">
                                                    <tr>
                                                        <td colspan="2" class="HeaderMainStyle" align="center">
                                                            <asp:Label ID="lblChequeHeader" runat="server" CssClass="labeltipopagoheader"
                                                                Text="Cheque"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblLector" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Usar lector:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLectorCheque" runat="server" AutoPostBack="False"
                                                                CausesValidation="True" CssClass="textboxcaptura" Font-Size="11px"
                                                                ValidationGroup="NumCheque"
                                                                Width="220px"></asp:TextBox>


                                                            <ccR:FilteredTextBoxExtender ID="ftbLector" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtLectorCheque">
                                                            </ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblClienteCheque" runat="server" Text="Cliente:"
                                                                CssClass="labeltipopagoforma" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClienteCheque" runat="server" CssClass="textboxcaptura" ></asp:TextBox>
                                                            <ccR:FilteredTextBoxExtender ID="ftxCliente" runat="server" TargetControlID="txtClienteCheque" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblNombreClientCheque" runat="server" CssClass="labeltipopagoforma"
                                                                Font-Size="11px" Text="Nombre:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreClienteCheque" runat="server"
                                                                CssClass="textboxcaptura" ReadOnly="True" Width="200px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCliente" runat="server"
                                                                ControlToValidate="txtClienteCheque" Display="None"
                                                                ErrorMessage="Capturar No de Cliente y dar Click en Buscar" Font-Size="11px"
                                                                ValidationGroup="ChequeCliente"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceCliente" runat="server"
                                                                TargetControlID="rfvCliente">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblFechaCheque" runat="server" Text="Fecha documento:"
                                                                CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaChueque" runat="server" CssClass="textboxcaptura"
                                                                ReadOnly="False"></asp:TextBox>
                                                            <asp:ImageButton ID="imgCalendario" runat="server" ImageUrl="~/Imagenes/Calendar.png" />
                                                            <asp:RequiredFieldValidator ID="rfvFecha" runat="server"
                                                                ControlToValidate="txtFechaChueque" Display="None"
                                                                ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Cheque"></asp:RequiredFieldValidator>
                                                            <ccR:CalendarExtender ID="cpChequeFechaDocto_CalendarExtender" runat="server"
                                                                PopupButtonID="imgCalendario" OnClientShown="muestraCalendario" OnClientHidden="cierraCheque" TargetControlID="txtFechaChueque" Format="dd/MM/yyyy">
                                                            </ccR:CalendarExtender>
                                                            <ccR:ValidatorCalloutExtender ID="vceChequeFecha" runat="server"
                                                                TargetControlID="rfvFecha">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label18" runat="server" Text="Fecha Cobro:"
                                                                CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtFechaCobro" runat="server" CssClass="textboxcaptura"
                                                                ReadOnly="False"></asp:TextBox>
                                                            <asp:ImageButton ID="imgCalendarioCobro" runat="server" ImageUrl="~/Imagenes/Calendar.png" />
                                                            <asp:RequiredFieldValidator ID="rfvFechaCobro" runat="server"
                                                                ControlToValidate="txtFechaChueque" Display="None"
                                                                ErrorMessage="Capturar la Fecha Cobro" Font-Size="11px" ValidationGroup="Cheque"></asp:RequiredFieldValidator>
                                                            <ccR:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                PopupButtonID="imgCalendarioCobro" OnClientShown="muestraCalendario" OnClientHidden="cierraCheque" TargetControlID="TxtFechaCobro" Format="dd/MM/yyyy">
                                                            </ccR:CalendarExtender>
                                                            <ccR:ValidatorCalloutExtender ID="vceChequeFechaCobro" runat="server"
                                                                TargetControlID="rfvFechaCobro">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblNuCuentaCheque" runat="server" CssClass="labeltipopagoforma"
                                                                Text="No. Cuenta:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textboxcaptura" MaxLength="11"></asp:TextBox>
                                                            <ccR:FilteredTextBoxExtender ID="ftbNumCuenta" runat="server" TargetControlID="txtNumCuenta" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label3" runat="server" Text="No. Documento:" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumeroCheque" runat="server" CssClass="textboxcaptura" MaxLength="7"></asp:TextBox>
                                                            <ccR:FilteredTextBoxExtender ID="ftbNumDocto" runat="server" TargetControlID="txtNumeroCheque" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblBancoCheque" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Banco:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddChequeBanco" runat="server" CssClass="textboxcaptura"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBancoCheque" runat="server"
                                                                ControlToValidate="ddChequeBanco" Display="None"
                                                                ErrorMessage="Seleccione el Banco"
                                                                ValidationGroup="Cheque" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceBancoCheque" runat="server"
                                                                TargetControlID="rfvBancoCheque">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label8" runat="server" Text="Importe:"
                                                                CssClass="labeltipopagoforma"></asp:Label> 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtImporteCheque" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvImporte" runat="server"
                                                                ControlToValidate="txtImporteCheque" Display="None"
                                                                ErrorMessage="Capturar Importe mayor a 0" Font-Size="11px"
                                                                ValidationGroup="Cheque"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceImporteCheque" runat="server"
                                                                TargetControlID="rfvImporte">
                                                            </ccR:ValidatorCalloutExtender>
                                                            <ccR:FilteredTextBoxExtender ID="ftbImporteCheque" runat="server" TargetControlID="txtImporteCheque" FilterType="Custom" ValidChars="0123456789./"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label11" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Observaciones:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObservacionesChueque" runat="server" CssClass="textboxcaptura"
                                                                Height="75px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">&nbsp;</td>
                                                        <td>
                                                            <asp:ImageButton ID="imbAceptar" runat="server"  
                                                                SkinID="btnAceptar"  OnClick="imbAceptar_Click" 
                                                                ValidationGroup="Cheque" Height="25px" Width="25px" />

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <%-- =====      TARJETA     ===== --%>
                                            <div id="tarjeta" style="display: none; vertical-align: top">
                                                <table style="background-color: #e1f8e2; height: 360px; width: 900px">

                                                    <tr>
                                                        <td colspan="2" class="HeaderMainStyle" align="center">
                                                            <asp:Label ID="lblTarjetaHeader" runat="server" CssClass="labeltipopagoheader"
                                                                Text="Tarjeta"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%-- Cliente --%>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblCLiente" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Cliente:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClienteTarjeta" runat="server" CssClass="textboxcaptura"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfvCliente0" runat="server"
                                                                ControlToValidate="txtClienteTarjeta" Display="None"
                                                                ErrorMessage="Capturar número de cliente y clic en buscar" Font-Size="11px"
                                                                ValidationGroup="TarjetaCliente"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceTarjetaCliente" runat="server"
                                                                TargetControlID="rfvCliente0">
                                                            </ccR:ValidatorCalloutExtender>
                                                            <ccR:FilteredTextBoxExtender ID="ftbClienteTC" runat="server" TargetControlID="txtClienteTarjeta" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <%-- Nombre --%>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblNombre" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Nombre:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreClienteTarjeta" runat="server" CssClass="textboxcaptura"
                                                                Width="200px" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%-- Fecha documento --%>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblFechaCheque0" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Fecha documento:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaTarjeta" runat="server" CssClass="textboxcaptura"
                                                                ReadOnly="True" AutoPostBack="false"></asp:TextBox>
                                                            <ccR:CalendarExtender ID="txtFechaTarjeta_CalendarExtender" runat="server" OnClientHidden="cierraTarjeta"
                                                                Format="dd/MM/yyyy" PopupButtonID="imgCalendario0"  OnClientShown="muestraCalendario" 
                                                                TargetControlID="txtFechaTarjeta">
                                                            </ccR:CalendarExtender>
                                                            <asp:ImageButton ID="imgCalendario0" runat="server"
                                                                ImageUrl="~/Imagenes/Calendar.png" />
                                                            <asp:RequiredFieldValidator ID="rfvFecha0" runat="server"
                                                                ControlToValidate="txtFechaTarjeta" Display="None"
                                                                ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="rfvFecha0_ValidatorCalloutExtender"
                                                                runat="server" TargetControlID="rfvFecha0">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTCBanco" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Banco:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddBancoTarjeta" runat="server" CssClass="textboxcaptura" 
                                                                Width="200px" readonly="true" enabled="false"   >
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBancoTarjeta" runat="server"
                                                                ControlToValidate="ddBancoTarjeta" Display="None"
                                                                ErrorMessage="Seleccione el Banco"
                                                                ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceBancoTarjeta" runat="server"
                                                                TargetControlID="rfvBancoTarjeta">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>

                                                    </tr>
                                                    <%-- Afiliación --%>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label17" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Afiliación:"></asp:Label>
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="TxtAfiliacion" runat="server" CssClass="textboxcaptura"                                                                  
                                                                 onkeyup = "SetContextKey()"     AutoComplete="off"  ></asp:TextBox>
                                                            <ccR:autocompleteextender  
                                                                servicemethod="SearchAfiliaciones"  
                                                                minimumprefixlength="1"
                                                                completioninterval="100" 
                                                                enablecaching="false" 
                                                                completionsetcount="100"
                                                                targetcontrolid="TxtAfiliacion" 
                                                                id="AutoCompleteExtender1"
                                                                runat="server" 
                                                                firstrowselected="true"                                                                                                          
                                                                UseContextKey="true"
                                                                OnClientPopulated="integratorsPopulated"
                                                                OnClientItemSelected="AutoCompletedClientItemSelected"
                                                                
                                                                >
                                                  </ccR:autocompleteextender>
                                                            <asp:RequiredFieldValidator ID="rfvTDAfiliacion" runat="server"
                                                                ControlToValidate="TxtAfiliacion" Display="None" ErrorMessage="Capturar Afiliacion"
                                                                Font-Size="11px" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                                <ccR:ValidatorCalloutExtender ID="vceAfiliacionTarjeta" runat="server"
                                                                TargetControlID="rfvTDAfiliacion">
                                                                </ccR:ValidatorCalloutExtender>
                                                                <ccR:FilteredTextBoxExtender ID="ftbAfiliacionTC" runat="server" TargetControlID="txtAfiliacion" 
                                                                 FilterType="Custom, Numbers" ValidChars="."></ccR:FilteredTextBoxExtender>
                                                          
                                                            
                                                            <asp:DropDownList ID="ddlTAfiliacion" runat="server" CssClass="textboxcaptura"
                                                                Width="200px" readonly="true" Visible="false"  enabled="false" >
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvTAfiliacion" runat="server"
                                                                ControlToValidate="ddlTAfiliacion" Display="None"
                                                                ErrorMessage="Seleccione la afiliación"
                                                                ValidationGroup="Tarjeta" InitialValue="0"
                                                               
                                                                ></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceTAfiliacion" runat="server"
                                                                TargetControlID="rfvTAfiliacion">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <%-- Tipo tarjeta --%>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="Label16" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Tipo Tarjeta:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddTipTarjeta" runat="server" CssClass="textboxcaptura" enabled="false"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="ddTipTarjeta" Display="None"
                                                                ErrorMessage="Seleccione el Tipo Tarjeta"
                                                                ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                TargetControlID="rfvTipoTarjeta">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTCAutorizacion" runat="server" CssClass="labeltipopagoforma"
                                                                Text="No Autorización:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNoAutorizacionTarjeta" runat="server" CssClass="textboxcaptura" ReadOnly="true"
                                                                Width="100px" AutoPostBack="false"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTDAutorizacion" runat="server"
                                                                ControlToValidate="txtNoAutorizacionTarjeta" Display="None"
                                                                ErrorMessage="Capturar Número de Autorización"
                                                                ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceAutorizacion" runat="server"
                                                                TargetControlID="rfvTDAutorizacion">
                                                            </ccR:ValidatorCalloutExtender>
                                                            <ccR:FilteredTextBoxExtender ID="ftbTDAutorizacion" runat="server" TargetControlID="txtNoAutorizacionTarjeta" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars=""></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id ="ConfirmAut">
                                                    
                                                        <td class="style1">
                                                            <div id="titNoAut" runat="server">
                                                                <asp:Label ID="Label13" runat="server" CssClass="labeltipopagoforma"
                                                                    Text="No Autorización:"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div runat="server" id="titNoAutNum">
                                                                <asp:TextBox ID="txtNoAutorizacionTarjetaConfirm" runat="server" CssClass="textboxcaptura"
                                                                    Width="100px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtNoAutorizacionTarjeta" Display="None"
                                                                    ErrorMessage="Capturar Número de Autorización"
                                                                    ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="RequiredFieldValidator1">
                                                                </ccR:ValidatorCalloutExtender>
                                                                <ccR:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtNoAutorizacionTarjetaConfirm" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars=""></ccR:FilteredTextBoxExtender>
                                                            </div>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTCNoTarjeta" runat="server" CssClass="labeltipopagoforma"  ReadOnly="true" 
                                                                Text="No de Tarjeta:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumTarjeta" runat="server" CssClass="textboxcaptura" ReadOnly="true"
                                                                Width="100px"></asp:TextBox>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" 
                                                        ControlToValidate="txtNumTarjeta" Display="None" 
                                                        ErrorMessage="Capturar Número de Tarjeta" CssClass="textboxcaptura" 
                                                        ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                    <ccR:ValidatorCalloutExtender ID="vceNumTarjeta" runat="server" 
                                                        TargetControlID="rfvNumTarjeta"></ccR:ValidatorCalloutExtender>--%>
                                                            <ccR:FilteredTextBoxExtender ID="ftbNumTarjeta" runat="server" TargetControlID="txtNumTarjeta" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblBancoOrigen" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Banco Tarjeta:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBancoOrigen" runat="server" CssClass="textboxcaptura" readonly="true"
                                                                Width="200px" enabled="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBancoOrigen" runat="server"
                                                                ControlToValidate="ddlBancoOrigen" Display="None"
                                                                ErrorMessage="Seleccione el Banco Origen"
                                                                ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceBancoOrigen" runat="server"
                                                                TargetControlID="rfvBancoOrigen">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">

                                                            <asp:Label ID="lblTPV" runat="server" CssClass="labeltipopagoforma"
                                                                Text="TPV:"></asp:Label>



                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkLocal" runat="server" CssClass="textboxcaptura" Text="Local"  enabled="false"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTCImporte" runat="server" CssClass="labeltipopagoforma"
                                                                Text="Importe:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtImporteTarjeta" runat="server" CssClass="textboxcaptura"  ReadOnly="true" ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTDImporte" runat="server"
                                                                ControlToValidate="txtImporteTarjeta" Display="None" ErrorMessage="Capturar Importe"
                                                                Font-Size="11px" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceImporteTarjeta" runat="server"
                                                                TargetControlID="rfvTDImporte">
                                                            </ccR:ValidatorCalloutExtender>
                                                            <ccR:FilteredTextBoxExtender ID="ftbImporteTC" runat="server" TargetControlID="txtImporteTarjeta" FilterType="Custom, Numbers" ValidChars="."></ccR:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTCObservaciones" runat="server"
                                                                CssClass="labeltipopagoforma" Text="Observaciones:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObservacionesTarjeta" runat="server" CssClass="textboxcaptura"  ReadOnly="true" 
                                                                Height="75px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:ImageButton ID="imbAceptarTDC" runat="server" 
                                                                SkinID="btnAceptar"
                                                                ValidationGroup="Tarjeta"
                                                                causesvalidation="true"
                                                                OnClick="imbAceptarTDC_Click" Height="25px"
                                                                Width="25px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
<%--                                                <ajaxToolkit:CascadingDropDown
                                                    ID="ccdBancos"
                                                    runat="server"
                                                    ServicePath="ServiceCS.asmx"
                                                    ServiceMethod="GetBancoAfiliaciones"
                                                    TargetControlID="ddBancoTarjeta"                                                   
                                                    
                                                    Category="Banco"/>
 
                                                <ajaxToolkit:CascadingDropDown
                                                    ID="ccdAfiliaciones"
                                                    runat="server"
                                                   ServicePath="ServiceCS.asmx"
                                                    ServiceMethod="GetAfiliaciones"
                                                    TargetControlID="ddlTAfiliacion"
                                                    ParentControlID="ddBancoTarjeta"
                                                    
                                                    Category="Afiliacion"
                                                    EmptyText="- Seleccione -"/>--%>
                                                 
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <div id="vale" style="display: none;">
                                                <table style="background-color: #e1f8e2; height: 360px; width: 900px">
                                                    <tr>
                                                        <td colspan="2" class="HeaderMainStyle" align="center">
                                                            <asp:Label ID="lblValeHeader" runat="server" CssClass="labeltipopagoheader"
                                                                Text="Vale"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblValeCliente" runat="server" Text="Cliente" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClienteVale" runat="server"  CssClass="textboxcaptura" onKeypress="return onlyNumbers(event)" ></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfvClienteVale" runat="server"
                                                                ControlToValidate="txtClienteVale" Display="None"
                                                                ErrorMessage="Capturar el No. de Cliente" Font-Size="11px"
                                                                ValidationGroup="Vale"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceClienteVale" runat="server"
                                                                TargetControlID="rfvClienteVale">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblValeNombre" runat="server" Text="Nombre" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValeNombre" runat="server" Width="200px" CssClass="textboxcaptura" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblValeFecha" runat="server" Text="Fecha Documento" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValeFecha" runat="server" CssClass="WarningLabels"></asp:TextBox>
                                                            <ccR:CalendarExtender ID="txtValeFecha_CalendarExtender" runat="server" OnClientHidden="cierraVale"
                                                                PopupButtonID="imgValeCalendario"  OnClientShown="muestraCalendario"  TargetControlID="txtValeFecha">
                                                            </ccR:CalendarExtender>
                                                            <asp:Image ID="imgValeCalendario" runat="server"
                                                                ImageUrl="~/Imagenes/Calendar.png" />
                                                            <asp:RequiredFieldValidator ID="rfvFechaVale" runat="server"
                                                                ControlToValidate="txtValeFecha" Display="None"
                                                                ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Vale"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceValeFecha" runat="server"
                                                                TargetControlID="rfvFechaVale">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblProveedor" runat="server" Text="Proveedor" CssClass="labeltipopagoforma"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="textboxcaptura">
                                                                <asp:ListItem>Si Vale</asp:ListItem>
                                                                <asp:ListItem>Promoción 2</asp:ListItem>
                                                                <asp:ListItem>Promoción 3</asp:ListItem>
                                                                <asp:ListItem>Promoción 4</asp:ListItem>
                                                                <asp:ListItem>Promoción 5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblTipoVale" runat="server" Text="Tipo Vale" CssClass="labeltipopagoforma"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoVale" runat="server" CssClass="textboxcaptura">
                                                                <asp:ListItem>Despensa</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblValeImporte" runat="server" Text="Importe" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValeImporte" runat="server" onKeypress="return onlyNumbersDecimals(event)"  CssClass="textboxcaptura"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvImporteVale" runat="server"
                                                                ControlToValidate="txtValeImporte" Display="None"
                                                                ErrorMessage="Capturar Importe" Font-Size="11px" ValidationGroup="Vale"></asp:RequiredFieldValidator>
                                                            <ccR:ValidatorCalloutExtender ID="vceValeImporte" runat="server"
                                                                TargetControlID="rfvImporteVale">
                                                            </ccR:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Label ID="lblValeObs" runat="server" Text="Observaciones" CssClass="labeltipopagoforma"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValeObs" runat="server" Height="75px" TextMode="MultiLine"
                                                                Width="300px" CssClass="textboxcaptura"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:ImageButton ID="imbAceptarVale" runat="server"
                                                                SkinID="btnAceptar" OnClick="imbAceptarVale_Click"
                                                                ValidationGroup="Vale" Height="25px" Width="25px" />

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                            </div>
                                        </td>

                                    </tr>




                                </table>

                            </td>
                    </tr>

                    </table>
                    <table style="vertical-align: top; width: 534px;">
                        <tr>
                            <td valign="top">
                                <div id="transferenciaOld" style="display: none; vertical-align: top">
                                    <table style="background-color: #e1f8e2; height: 360px; width: 900px">

                                        <tr>
                                            <td colspan="2" class="HeaderMainStyle" align="center">
                                                <asp:Label ID="Label1" runat="server" CssClass="labeltipopagoheader"
                                                    Text="Transferencia"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label2" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Cliente:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtCteAfiliacion" runat="server" CssClass="textboxcaptura"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="rfvCteAfiliacion" runat="server"
                                                    ControlToValidate="TxtCteAfiliacion" Display="None"
                                                    ErrorMessage="Capturar No Cliente y Click en Buscar" Font-Size="11px"
                                                    ValidationGroup="TarjetaCliente"></asp:RequiredFieldValidator>
                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="rfvCteAfiliacion">
                                                </ccR:ValidatorCalloutExtender>
                                                <ccR:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtCteAfiliacion" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label4" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Nombre:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtNombreCteTrans" runat="server" CssClass="textboxcaptura"
                                                    Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label5" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Fecha documento:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtFechaDocTrans" runat="server" CssClass="textboxcaptura" 
                                                    ReadOnly="False" AutoPostBack="false"></asp:TextBox>

                                                <asp:ImageButton ID="ImageButtonCal" runat="server"
                                                    ImageUrl="~/Imagenes/Calendar.png" />
                                                <asp:RequiredFieldValidator ID="rfvFechaDocTrans" runat="server"
                                                    ControlToValidate="TxtFechaDocTrans" Display="None"
                                                    ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                <ccR:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImageButtonCal"
                                                    TargetControlID="TxtFechaDocTrans">
                                                </ccR:CalendarExtender>
                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                                    runat="server" TargetControlID="rfvFechaDocTrans">
                                                </ccR:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label6" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Afiliación:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddAfiliacion" runat="server" CssClass="textboxcaptura"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddAfiliacion" runat="server"
                                                    ControlToValidate="ddAfiliacion" Display="None"
                                                    ErrorMessage="Seleccione la afiliación"
                                                    ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="rfvddAfiliacion">
                                                </ccR:ValidatorCalloutExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label15" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Tipo Tarjeta:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddTipoTarjeta" runat="server" CssClass="textboxcaptura"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipoTarjeta" runat="server"
                                                    ControlToValidate="ddTipoTarjeta" Display="None"
                                                    ErrorMessage="Seleccione el Tipo Tarjeta"
                                                    ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                    TargetControlID="rfvTipoTarjeta">
                                                </ccR:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label7" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Tarjeta:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtTarjetaTranferencia" runat="server" CssClass="textboxcaptura"
                                                    Width="100px"></asp:TextBox>

                                                <ccR:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TxtTarjetaTranferencia" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label9" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Banco:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddBancoTrasferencia" runat="server" CssClass="textboxcaptura"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfBancoTrasferencia" runat="server"
                                                    ControlToValidate="ddBancoTrasferencia" Display="None"
                                                    ErrorMessage="Seleccione el Banco"
                                                    ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                <ccR:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                    TargetControlID="rfBancoTrasferencia">
                                                </ccR:ValidatorCalloutExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label10" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Autorización:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAutorizacionTrans" runat="server" CssClass="textboxcaptura"
                                                    Width="100px"></asp:TextBox>

                                                <ccR:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtAutorizacionTrans" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label12" runat="server" CssClass="labeltipopagoforma"
                                                    Text="Repetir Autorización:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtRepAutorizacionTrans" runat="server" CssClass="textboxcaptura"
                                                    Width="100px"></asp:TextBox>

                                                <ccR:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TxtRepAutorizacionTrans" FilterType="Numbers"></ccR:FilteredTextBoxExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label14" runat="server"
                                                    CssClass="labeltipopagoforma" Text="Observaciones:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtObervacionesTrans" runat="server" CssClass="textboxcaptura"
                                                    Height="75px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton2" runat="server"
                                                    SkinID="btnAceptar"
                                                    ValidationGroup="Tarjeta" OnClick="imbAceptarTDC_Click" Height="25px"
                                                    Width="25px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>


                                        <table style="vertical-align: top; width: 534px;">
                        <tr>
                            <td valign="top">
                                <div id="AnticipoUC" style="display: none; vertical-align: top">
                                    <ucDetallePago:wucDetalleFormaPago runat="server" id="wucDetalleFormaPago1" /> 
                                    <%--<asp:ImageButton ID="btnAntAceptar" runat="server"
                                        OnClick="btnAceptarAnticipo_Click" ImageUrl="~/Images/btnAceptar.png"
                                        SkinID="btnAceptar" ValidationGroup="GuardaAnt" />--%>
                                </div>
                            </td>
                        </tr>
                    </table>


                </div>



                <table style="background-color: Transparent; vertical-align: bottom;">
                    <tr>
                        <td align="center">

                            <%-- <div style="width:580px; height:150px; overflow:scroll;">--%>
                            <ccR:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                TargetControlID="ContentPanel"
                                CollapseControlID="TitlePanel"
                                ExpandControlID="TitlePanel"
                                Collapsed="false"
                                CollapsedImage="images/expand.jpg"
                                ExpandedText="Ocultar Cobros Capturados"
                                ImageControlID="imgExpandCollapse"
                                ExpandedImage="images/collapse.jpg"
                                TextLabelID="lblCobros"
                                CollapsedText="Mostrar Cobros Capturados"
                                SuppressPostBack="true">
                            </ccR:CollapsiblePanelExtender>
                            <asp:Panel ID="TitlePanel" runat="server"
                                CssClass="collapsePanelHeader" Visible="true" HorizontalAlign="Left">
                                <asp:Image ID="imgExpandCollapse" runat="server" ImageUrl="images/expand.jpg" Visible="false" />
                                &nbsp;&nbsp;
                        <asp:Label ID="lblCobros" runat="server" Text="       Cobros Capturados" Visible="false"></asp:Label>
                            </asp:Panel>

                            <asp:Panel ID="ContentPanel" runat="server"
                                CssClass="collapsePanel">

                                <asp:GridView ID="gvPagoGenerado" runat="server" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="gvPagoGenerado_SelectedIndexChanged">
                                    <Columns>

                                        <asp:BoundField DataField="IdPago" HeaderText="Pago"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cliente" HeaderText="Cliente"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Banco" HeaderText="Banco"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:C}"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaAlta" HtmlEncode="false"
                                            HeaderText="Fecha Alta" DataFormatString="{0:d}"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Referencia" HeaderText="Referencia"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreTipoCobro" HeaderText="Tipo Cobro"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowSelectButton="True" SelectText="Eliminar"
                                            SelectImageUrl="~/Images/imgEliminarGrid.png">
                                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />

                                        </asp:CommandField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>


                            </asp:Panel>
                            <%--   </div>--%>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </div>


    <asp:Panel runat="server" ID="panelUpdateProgress">
        <div style="border-style: solid; border-color: Black; border-width: 1px; background-color: White; text-align: center; vertical-align: middle; padding-top: 10px; padding-bottom: 10px; padding-left: 25px; padding-right: 25px">
            <img alt="" src="Images/updateProgress.gif" style="width: 32px" />
            <b>Procesando...</b>
        </div>
    </asp:Panel>

    <ccR:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" TargetControlID="panelUpdateProgress">
    </ccR:ModalPopupExtender>



    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <ccR:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </ccR:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; align-content: center">
        <div class="header">
        </div>
        <div class="body" style="align-content: center; background-color: aliceblue">
            <table style="width: 100%; align-content: center; background-color: aliceblue; border: thin">
                <tr style="align-content: center;">
                    <td style="text-align: center">
                        <br />
                        <br />
                    </td>

                </tr>

                 <tr style="align-content: center;">
                    <td style="text-align: right">
                 <asp:Button ID="btnHideSup" runat="server" width="100px"  Text="Cerrar" Font-Bold="true"  OnClientClick="return HideModalPopup()"  OnClick="btnHide_Click" BackColor="#E4EBAB"/>
                    </td>

                </tr>
                <tr style="align-content: center;">
                    <td style="text-align: center">
                        <b>CARGOS DEL MISMO CLIENTE </b>
                        <br />
                    </td>

                </tr>

            </table>
            <div style="height: 350px; overflow: auto;">
                <uc1:wucConsultaCargoTarjetaCliente runat="server" ID="wucConsultaCargoTarjetaCliente1" />
            </div>
            <table style="width: 100%; align-content: center; background-color: aliceblue; border: thin">
                <tr style="align-content: center;">
                    <td style="text-align: center">
                    </td>

                </tr>
                 <tr style="align-content: center;">
                    <td style="text-align: right">
                 <asp:Button ID="btnHideInf" runat="server" width="100px"  Text="Cerrar" Font-Bold="true"  OnClientClick="return HideModalPopup()"  OnClick="btnHide_Click" BackColor="#E4EBAB"/>
                    </td>

                </tr>
            </table>

            <br />
            <div style="align-content: center">
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">   



        var ModalProgress = '<%= ModalProgress.ClientID %>';



    </script>

</asp:Content>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <style type="text/css">
        .style1 {
            width: 113px;
            text-align: right;
        }
    </style>

</asp:Content>


