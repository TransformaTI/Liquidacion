﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormaPago.aspx.cs" Inherits="FormaPago" Title="Untitled Page" Theme="Theme1"  UICulture="es" Culture="es-MX"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ccR" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">

 <script type="text/javascript" language="javascript">       
    var ModalProgress ='<%= ModalProgress.ClientID %>';        
 </script> 
 <script type="text/javascript">
       <!--
        function toggle(display, activo, inactivo, inactivoA, control) {
        
        document.getElementById(activo).style.display = (
        document.getElementById(activo).style.display == 'none')?'block':'none';
        
        document.getElementById(inactivo).style.display = 'none';
        document.getElementById(inactivoA).style.display = 'none';
        document.getElementById(control).focus();
        }
       
       //-->
</script>
 <script type="text/javascript">
       <!--
        function firstFocus(control) {
        
        document.getElementById(activo).focus();
        return true;
        }
       
       //-->
</script>

<script type="text/javascript">
function confirmar(button)
{
    if(document.getElementById(button).disabled = true)
    {
        var answer = confirm('¿Desea agregar el Pago?')
        if(answer)
          document.getElementById(button).disabled = false;
                   
    }
}

</script>

 <script type="text/javascript" language="JavaScript">
function HideContent(d) {
if(d.length < 1) { return; }
document.getElementById(d).style.display = "none";
}
function ShowContent(d) {
if(d.length < 1) { return; }
document.getElementById(d).style.display = "block";
}
function ReverseContentDisplay(d) {
if(d.length < 1) { return; }
if(document.getElementById(d).style.display == "none") { document.getElementById(d).style.display = "block"; }
else { document.getElementById(d).style.display = "none"; }
}
</script>
 <script type="text/javascript"> 
    function txtCuentaDocumento()
    {
    
    var str = document.getElementById('<%=txtLectorCheque.ClientID%>').value;

    ctrCuenta = document.getElementById('<%=txtNumCuenta.ClientID%>');
    if( ctrCuenta != null)
    {
    ctrCuenta.value = str.substring(13, 24);
    }
    ctrDocumento = document.getElementById('<%=txtNumeroCheque.ClientID%>');
    if( ctrDocumento != null)
    {
    ctrDocumento.value = str.substring(24, 31);
    }
    }
    function test(txtCliente)
    {
        var test = document.getElementById(txtCliente).value;
        alert(test);
    }   
</script>
 <script type="text/javascript"> 
    function txtImagenTipoCobro(tipo)
    {
        var str = '';
        if(tipo == "TARJETA")
        {
        str = '~/Images/imgTarjetaCredito.ico';
        }
        
        if(tipo == "CHEQUE")
        {
        str = '~/Images/imgTipoCobro.gif';
        }
        
        if(tipo == "VALE")
        {
        str = '~/Images/imgMonto.bmp';
        }
    return str;
    }
</script>

 <script src="Scripts/jsUpdateProgress.js" type="text/javascript"></script>
 <script src="Scripts/MiscFunctions.js" type="text/javascript"></script>
 
 <div style="text-align:center; vertical-align:top;">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
 <Scripts>
        <asp:ScriptReference Path="~/Scripts/GetCliente.js" />
    </Scripts>
 <Services>
        <asp:ServiceReference  Path="~/WebServices/DatosCliente.asmx" />
    </Services>        
 </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
  <ContentTemplate>       
 
 <div style="text-align:left; height:650px; width:1000px; vertical-align:top;">
   <table style="vertical-align:top; height:650px;"> 
    <tr>
       <td valign="top">
        <table style="vertical-align:top;">
            <tr>
               <td>
                      <asp:Image ID="imgCheque" runat="server" 
                        ImageUrl="~/Images/imgCapturarCheque.png"/>
       
       </td>
           <tr>
               <td>
                       <asp:Image ID="imgTarjeta" runat="server" 
                           ImageUrl="~/Images/imgCapturarTarjeta.png" />
               </td>
           </tr>
            <tr>
               <td>
                       <asp:Image ID="imgVale" runat="server" 
                           ImageUrl="~/Images/imgVale.png" Height="26px" Width="138px" />
               </td>
           </tr>
           <tr>
               <td>
                  <asp:ImageButton ID="imbEfectivo" runat="server" Height="26px" 
                       ImageUrl="~/Images/imgEfectivo.png" onclick="imbEfectivo_Click" 
                       Width="138px" />
               </td>
           </tr>
           <tr>
                <td>
                   <asp:ImageButton ID="imbCancelar" runat="server" Height="25px" 
                       ImageUrl="~/Images/imgCancelarPagos.png" onclick="imbCancelar_Click" 
                       Width="137px" />
                       
                </td>
           </tr>
           <tr>
           <td>
               <asp:ImageButton ID="imbResumen" runat="server" Height="25px" 
                   ImageUrl="~/Images/captPagos.png" onclick="imbResumen_Click" Visible="False" 
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
     <table style="vertical-align:top;">         
        <tr>
            <td valign="top" style="vertical-align:top;">
              <div id="cheque" style="display:none; vertical-align:top;">
                   <table style="background-color: #e1f8e2; height:360px; width:900px">
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
                                                    <asp:TextBox ID="txtClienteCheque" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                                                    <ccR:FilteredTextBoxExtender ID="ftxCliente" runat="server" TargetControlID="txtClienteCheque" FilterType="Numbers" ></ccR:FilteredTextBoxExtender>
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
                                                    <asp:ImageButton ID="imgCalendario" runat="server" 
                                                        ImageUrl="~/Imagenes/Calendar.png" />
                                                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server" 
                                                        ControlToValidate="txtFechaChueque" Display="None" 
                                                        ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Cheque"></asp:RequiredFieldValidator>
                                                    <ccR:CalendarExtender ID="cpChequeFechaDocto_CalendarExtender" runat="server" 
                                                        PopupButtonID="imgCalendario" TargetControlID="txtFechaChueque" Format="dd/MM/yyyy"></ccR:CalendarExtender>
                                                    <ccR:ValidatorCalloutExtender ID="vceChequeFecha" runat="server" 
                                                        TargetControlID="rfvFecha"></ccR:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblNuCuentaCheque" runat="server" CssClass="labeltipopagoforma" 
                                                Text="No. Cuenta:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textboxcaptura" MaxLength="11"></asp:TextBox>
                                                     <ccR:FilteredTextBoxExtender ID="ftbNumCuenta" runat="server" TargetControlID="txtNumCuenta" FilterType="Numbers"  ></ccR:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label3" runat="server" Text="No. Documento:" CssClass="labeltipopagoforma"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumeroCheque" runat="server" CssClass="textboxcaptura" MaxLength="7"></asp:TextBox>
                                                    <ccR:FilteredTextBoxExtender ID="ftbNumDocto" runat="server" TargetControlID="txtNumeroCheque" FilterType="Numbers" ></ccR:FilteredTextBoxExtender>
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
                                                        TargetControlID="rfvBancoCheque"></ccR:ValidatorCalloutExtender>
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
                                                        TargetControlID="rfvImporte"></ccR:ValidatorCalloutExtender>
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
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:ImageButton ID="imbAceptar" runat="server" 
                                                        SkinID="btnAceptar" onclick="imbAceptar_Click" 
                                                        ValidationGroup="Cheque" Height="25px" Width="25px" />
                                                   
                                                </td>
                                            </tr>
                   </table>
              </div>
            </td>
            
            <tr>
            <td valign="top">
            <div id="tarjeta" style="display:none; vertical-align:top">
                     <table style="background-color: #e1f8e2; height:360px; width:900px">           
                     
                                            <tr>
                                                <td colspan="2" class="HeaderMainStyle" align="center">
                                                <asp:Label ID="lblTarjetaHeader" runat="server" CssClass="labeltipopagoheader" 
                                                        Text="Tarjeta"></asp:Label>
                                                    </td>
                                            </tr>                                                                             
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblCLiente" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="Cliente:" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtClienteTarjeta" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                                                    
                                                    <asp:RequiredFieldValidator ID="rfvCliente0" runat="server" 
                                                        ControlToValidate="txtClienteTarjeta" Display="None" 
                                                        ErrorMessage="Capturar No Cliente y Click en Buscar" Font-Size="11px" 
                                                        ValidationGroup="TarjetaCliente"></asp:RequiredFieldValidator>
                                                    <ccR:ValidatorCalloutExtender ID="vceTarjetaCliente" runat="server" 
                                                        TargetControlID="rfvCliente0"></ccR:ValidatorCalloutExtender>
                                                     <ccR:FilteredTextBoxExtender ID="ftbClienteTC" runat="server" TargetControlID="txtClienteTarjeta" FilterType="Numbers" ></ccR:FilteredTextBoxExtender>    
                                                </td>
                                            </tr>
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
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblFechaCheque0" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="Fecha documento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFechaTarjeta" runat="server" CssClass="textboxcaptura" 
                                                        ReadOnly="False" AutoPostBack="false"></asp:TextBox>
                                                    <ccR:CalendarExtender ID="txtFechaTarjeta_CalendarExtender" runat="server" 
                                                        Format="dd/MM/yyyy" PopupButtonID="imgCalendario0" 
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
                                                    <asp:Label ID="lblTCAutorizacion" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="No Autorización:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoAutorizacionTarjeta" runat="server" CssClass="textboxcaptura" 
                                                        Width="100px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTDAutorizacion" runat="server" 
                                                        ControlToValidate="txtNoAutorizacionTarjeta" Display="None" 
                                                        ErrorMessage="Capturar Número de Autorización" 
                                                        ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                    <ccR:ValidatorCalloutExtender ID="vceAutorizacion" runat="server" 
                                                        TargetControlID="rfvTDAutorizacion"></ccR:ValidatorCalloutExtender>
                                                        <ccR:FilteredTextBoxExtender ID="ftbTDAutorizacion" runat="server" TargetControlID="txtNoAutorizacionTarjeta" FilterType="Numbers" ></ccR:FilteredTextBoxExtender>   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblTCNoTarjeta" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="No de Tarjeta:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumTarjeta" runat="server" CssClass="textboxcaptura" 
                                                        Width="100px"></asp:TextBox>
                                                 <%--   <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" 
                                                        ControlToValidate="txtNumTarjeta" Display="None" 
                                                        ErrorMessage="Capturar Número de Tarjeta" CssClass="textboxcaptura" 
                                                        ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                    <ccR:ValidatorCalloutExtender ID="vceNumTarjeta" runat="server" 
                                                        TargetControlID="rfvNumTarjeta"></ccR:ValidatorCalloutExtender>--%>
                                                        <ccR:FilteredTextBoxExtender ID="ftbNumTarjeta" runat="server" TargetControlID="txtNumTarjeta" FilterType="Numbers" ></ccR:FilteredTextBoxExtender>   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblTCBanco" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="Banco:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddBancoTarjeta" runat="server" CssClass="textboxcaptura" 
                                                        Width="200px" >
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvBancoTarjeta" runat="server" 
                                                        ControlToValidate="ddBancoTarjeta" Display="None" 
                                                        ErrorMessage="Seleccione el Banco" 
                                                        ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                     <ccR:ValidatorCalloutExtender ID="vceBancoTarjeta" runat="server" 
                                                        TargetControlID="rfvBancoTarjeta"></ccR:ValidatorCalloutExtender>
                                                </td>
                                                 
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                <asp:Label ID="lblBancoOrigen" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="Banco Tarjeta:"></asp:Label>
                                                
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBancoOrigen" runat="server" CssClass="textboxcaptura" 
                                                        Width="200px" >
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvBancoOrigen" runat="server" 
                                                        ControlToValidate="ddlBancoOrigen" Display="None" 
                                                        ErrorMessage="Seleccione el Banco Origen" 
                                                        ValidationGroup="Tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                     <ccR:ValidatorCalloutExtender ID="vceBancoOrigen" runat="server" 
                                                        TargetControlID="rfvBancoOrigen"></ccR:ValidatorCalloutExtender>
                                                </td>
                                            </tr>  
							  <tr>
                                            <td class="style1">
                                            
                                                <asp:Label ID="lblTPV" runat="server" CssClass="labeltipopagoforma" 
                                                    Text="TPV:"></asp:Label>
                                            
                                            
                                            
                                            </td>
                                            <td>
                                                    <asp:CheckBox ID="chkLocal" runat="server" CssClass="textboxcaptura" Text="Local"/>
                                                </td>
                                            </tr>                                                                               
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblTCImporte" runat="server" CssClass="labeltipopagoforma" 
                                                        Text="Importe:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtImporteTarjeta" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTDImporte" runat="server" 
                                                        ControlToValidate="txtImporteTarjeta" Display="None" ErrorMessage="Capturar Importe" 
                                                        Font-Size="11px" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                                    <ccR:ValidatorCalloutExtender ID="vceImporteTarjeta" runat="server" 
                                                        TargetControlID="rfvTDImporte"></ccR:ValidatorCalloutExtender>
                                                          <ccR:FilteredTextBoxExtender ID="ftbImporteTC" runat="server" TargetControlID="txtImporteTarjeta" FilterType="Custom, Numbers" ValidChars="."></ccR:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="lblTCObservaciones" runat="server" 
                                                        CssClass="labeltipopagoforma" Text="Observaciones:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtObservacionesTarjeta" runat="server" CssClass="textboxcaptura"
                                                        Height="75px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:ImageButton ID="imbAceptarTDC" runat="server" 
                                                        SkinID="btnAceptar" 
                                                        ValidationGroup="Tarjeta" onclick="imbAceptarTDC_Click" Height="25px" 
                                                        Width="25px" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            </tr>
                     </table>
            </div>
            </td>   
            </tr>
           <tr>  
            <td valign="top">
              <div id="vale" style="display:none;"> 
              <table style="background-color: #e1f8e2; height:360px; width:900px">
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
                            <asp:TextBox ID="txtClienteVale" runat="server" CssClass="textboxcaptura" ></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvClienteVale" runat="server" 
                                ControlToValidate="txtClienteVale" Display="None" 
                                ErrorMessage="Capturar el No. de Cliente" Font-Size="11px" 
                                ValidationGroup="Vale"></asp:RequiredFieldValidator>
                            <ccR:ValidatorCalloutExtender ID="vceClienteVale" runat="server" 
                                                        TargetControlID="rfvClienteVale"></ccR:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                        <td class="style1">
                            <asp:Label ID="lblValeNombre" runat="server" Text="Nombre" CssClass="labeltipopagoforma"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="txtValeNombre" runat="server" Width="200px" CssClass="textboxcaptura"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td class="style1">
                            <asp:Label ID="lblValeFolio" runat="server" Text="Folio" CssClass="labeltipopagoforma"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="txtFolioVale" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFolioVale" runat="server" 
                                ControlToValidate="txtFolioVale" Display="None" 
                                ErrorMessage="Capturar el Folio" Font-Size="11px" ValidationGroup="Vale"></asp:RequiredFieldValidator>
                            <ccR:ValidatorCalloutExtender ID="vceFolioVale" runat="server" 
                                                        TargetControlID="rfvFolioVale"></ccR:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                        <td class="style1">
                            <asp:Label ID="lblValeFecha" runat="server" Text="Fecha" CssClass="labeltipopagoforma"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="txtValeFecha" runat="server" CssClass="WarningLabels"></asp:TextBox>
                            <ccR:CalendarExtender ID="txtValeFecha_CalendarExtender" runat="server" 
                                PopupButtonID="imgValeCalendario" TargetControlID="txtValeFecha"></ccR:CalendarExtender>
                            <asp:Image ID="imgValeCalendario" runat="server" 
                                ImageUrl="~/Imagenes/Calendar.png" />
                            <asp:RequiredFieldValidator ID="rfvFechaVale" runat="server" 
                                ControlToValidate="txtValeFecha" Display="None" 
                                ErrorMessage="Capturar la Fecha" Font-Size="11px" ValidationGroup="Vale"></asp:RequiredFieldValidator>
                             <ccR:ValidatorCalloutExtender ID="vceValeFecha" runat="server" 
                                                        TargetControlID="rfvFechaVale"></ccR:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                        <td class="style1">
                            <asp:Label ID="lblValePromocion" runat="server" Text="Promoción" CssClass="labeltipopagoforma"> </asp:Label>
                            </td>
                        <td>
                            <asp:DropDownList ID="ddlValePromocion" runat="server" CssClass="textboxcaptura"> 
                                <asp:ListItem>Promoción 1</asp:ListItem>
                                <asp:ListItem>Promoción 2</asp:ListItem>
                                <asp:ListItem>Promoción 3</asp:ListItem>
                                <asp:ListItem>Promoción 4</asp:ListItem>
                                <asp:ListItem>Promoción 5</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td class="style1">
                            <asp:Label ID="lblValeImporte" runat="server" Text="Importe" CssClass="labeltipopagoforma"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="txtValeImporte" runat="server" CssClass="textboxcaptura"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvImporteVale" runat="server" 
                                ControlToValidate="txtValeImporte" Display="None" 
                                ErrorMessage="Capturar Importe" Font-Size="11px" ValidationGroup="Vale"></asp:RequiredFieldValidator>
                            <ccR:ValidatorCalloutExtender ID="vceValeImporte" runat="server" 
                                                        TargetControlID="rfvImporteVale"></ccR:ValidatorCalloutExtender>
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
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbAceptarVale" runat="server" 
                                        SkinID="btnAceptar"
                                        ValidationGroup="Vale" Height="25px" Width="25px" />
                                  
                                </td>
                            </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            </tr>
               </div>
             </td>
         </tr>                       
         </table>
             
        </td>            
    </tr>   
    
      </table>    
  
 </div>
 
 
 
<table style="background-color:Transparent; vertical-align:bottom;" >
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
                        ExpandedImage ="images/collapse.jpg"
                        TextLabelID="lblCobros" 
                        CollapsedText="Mostrar Cobros Capturados"
                        SuppressPostBack="true">
                    </ccR:CollapsiblePanelExtender>
                    <asp:Panel ID="TitlePanel" runat="server" 
                    CssClass="collapsePanelHeader" Visible="true" HorizontalAlign="Left">
                       <asp:Image ID="imgExpandCollapse" runat="server" ImageUrl="images/expand.jpg" Visible="false"/>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblCobros" runat="server" Text="       Cobros Capturados" Visible="false"></asp:Label>
                    </asp:Panel>
                    
                    <asp:Panel ID="ContentPanel" runat="server" 
                   CssClass="collapsePanel">
                        
                    <asp:GridView ID="gvPagoGenerado" runat="server" AutoGenerateColumns="False" 
                 onselectedindexchanged="gvPagoGenerado_SelectedIndexChanged">
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
                        SelectImageUrl="~/Images/imgEliminarGrid.png" >
                        <ItemStyle HorizontalAlign="Center" ForeColor="Black"/>
                        
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
    <div style="border-style:solid;border-color:Black;border-width:1px;background-color:White;
        text-align:center;vertical-align:middle;padding-top:10px;padding-bottom:10px;padding-left:25px;
        padding-right:25px">
        <img alt="" src="Images/updateProgress.gif" style="width: 32px"/> <b> Procesando...</b>       
    </div>
</asp:Panel>

<ccR:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelUpdateProgress" 
    BackgroundCssClass="modalBackground" TargetControlID="panelUpdateProgress">
</ccR:ModalPopupExtender>

<script type="text/javascript" language="javascript">       
    var ModalProgress ='<%= ModalProgress.ClientID %>';        
</script> 

</asp:Content>


<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            width: 113px;
            text-align: right;
        }
    </style>

</asp:Content>


