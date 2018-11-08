﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucDetalleFormaPago.ascx.cs" Inherits="UserControl_DetalleFormaPago_wucDetalleFormaPago" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>


<script type="text/javascript">

    function cierraTransferencia()
    {       
       <%-- document.getElementById('<%= txtNoCuenta.ClientID %>').focus();--%>
        document.getElementById('ctl00_mostrando').value = '';
    }
    function muestraCalendario1() {
        document.getElementById('ctl00_mostrando').value = 'x';
    }
</script>

<script type="text/javascript">
<%--    document.getElementById("<%= txtCliente.ClientID%>").focus--%>

    function ValidaMontoSaldo()
    {
        var listBox = document.getElementById("<%= LstSaldos.ClientID%>");
        var Monto = listBox.options[listBox.selectedIndex].text.split(",");

        if (parseFloat(document.getElementById("<%= txtAntMonto.ClientID%>").value) > parseFloat(Monto[0].replace('$', '')))
        {
            alert('¡El Monto debe ser menor o igual al saldo seleccionado!');
            document.getElementById("<%= txtAntMonto.ClientID%>").value = Monto[0].replace('$', '')
            return false;
        }        
    }


     function RegistroPago() {
            javascript: __doPostBack('RegistroPago', '');

    }

    function ConsultaCteAnticipo(IdCliente) {
        if (document.getElementById("<%=txtAntCliente.ClientID %>").value != '') {
            javascript: __doPostBack('ConsultaCteAnticipo');
        }
    }

    function ConsultaCteTransferencia(IdCliente) {
        if (document.getElementById("<%=txtCliente.ClientID %>").value != '') {
            javascript: __doPostBack('ConsultaCteTransferencia');
        }
    }



    function MontoSaldo() {

        var listBox = document.getElementById("<%= LstSaldos.ClientID%>");
        var Monto = listBox.options[listBox.selectedIndex].text.split(",");

        if (parseInt(Monto[0].replace('$',''))<=0)
        {
            alert('¡El saldo debe ser mayor a cero!');
            document.getElementById("<%= txtAntMonto.ClientID%>").value = '';
        }

        else

        {
             document.getElementById("<%= txtAntMonto.ClientID%>").value = Monto[0];
        }
    }

    function CuentasBancarias() {
         javascript: __doPostBack('CuentasBancarias');
    }


</script>

 

<asp:HiddenField ID="TipoPagoOculto" runat="server" Value="" />
<div style="text-align: right" id ="Transfer" >
<asp:Panel ID="pnlTransferencia" runat="server" >
<table style="background-color: #e1f8e2; height: 360px; width: 900px">
    <tr>
        <td  colspan="2" class="HeaderMainStyle" align="center">
            <asp:Label ID="lblTitulo" runat="server" CssClass="labeltipopagoheader" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblCliente" runat="server" CssClass="labeltipopagoforma"
                    Text="Cliente:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            
            <asp:TextBox ID="txtCliente" Width="150px" runat="server" CssClass="textboxcaptura" onblur="return ConsultaCteTransferencia();"></asp:TextBox>
            <cc2:FilteredTextBoxExtender ID="ftbLector" runat="server" FilterType="Numbers"
                TargetControlID="txtCliente">
            </cc2:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="rfvCliente" runat="server"
                ControlToValidate="txtCliente" Display="None"
                ErrorMessage="Capturar el No. de Cliente"
                ValidationGroup="Guarda"></asp:RequiredFieldValidator>
            <cc2:ValidatorCalloutExtender ID="vceCliente" runat="server"
                TargetControlID="rfvCliente">
            </cc2:ValidatorCalloutExtender>
           
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div >
                <asp:Label ID="lblNombre" runat="server" CssClass="labeltipopagoforma"
                    Text="Nombre:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtNombre" Width="200px" runat="server" CssClass="textboxcaptura" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblFecha" runat="server" CssClass="labeltipopagoforma"
                    Text="Fecha documento:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <div style="float: left;">
                <asp:TextBox ID="txtFecha" Width="150px" runat="server" Text="" CssClass="calendarTextBox" ></asp:TextBox>
                <asp:ImageButton runat="Server" ID="btnCalFAsignacion" AlternateText="Clic para mostrar el calendario" ImageUrl="~/Imagenes/Calendar.png" Height="16px" Width="16px" />
                <cc2:CalendarExtender ID="txtFecha_CalendarExtender" runat="server"  OnClientShown="muestraCalendario1" OnClientHidden="cierraTransferencia" TargetControlID="txtFecha" Format="dd/MM/yyyy" PopupButtonID="btnCalFAsignacion"></cc2:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvFechaDoc" runat="server" ControlToValidate="txtFecha" Display="None" ErrorMessage="Capturar la Fecha" ValidationGroup="Guarda"></asp:RequiredFieldValidator>
                <cc2:ValidatorCalloutExtender ID="vceFecha" runat="server" TargetControlID="rfvFechaDoc"></cc2:ValidatorCalloutExtender>
            </div>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="lblFecha0" runat="server" CssClass="labeltipopagoforma" Text="Banco Origen:"></asp:Label>
        </td>
        <td style="text-align: left">
            <asp:DropDownList ID="ddlBancoOrigen" runat="server" Height="25" Width="200px">
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
            <asp:Label ID="lblFecha1" runat="server" CssClass="labeltipopagoforma" Text="Cuenta Origen:"></asp:Label>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="TxtCtaOrigen" runat="server" CssClass="TxtCtaOrigen" onblur="return ConsultaCteTransferencia();" Width="150px"></asp:TextBox>
            <cc2:FilteredTextBoxExtender ID="TxtCtaOrigen_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="TxtCtaOrigen">
            </cc2:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div >
                <asp:Label ID="lblNoDocumento" runat="server" CssClass="labeltipopagoforma"
                    Text="No. Referencia:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtNoDocumento" Width="150px" runat="server" CssClass="textboxcaptura"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="rfvNoDocumento" runat="server"
                ControlToValidate="txtNoDocumento" Display="None"
                ErrorMessage="Capturar el No. Documento"
                ValidationGroup="Guarda"></asp:RequiredFieldValidator>
            <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNoDocumento" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"></cc2:FilteredTextBoxExtender>
            <cc2:ValidatorCalloutExtender ID="vceNoDocumento" runat="server"
                TargetControlID="rfvNoDocumento">
            </cc2:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblBanco" runat="server" CssClass="labeltipopagoforma"
                    Text="Banco Destino:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:DropDownList ID="ddlBancoDestino" Width="200px" runat="server"  Height="25" OnSelectedIndexChanged="ddlBancoDestino_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            &nbsp;<asp:RequiredFieldValidator ControlToValidate="ddlBancoDestino" 
                    ValidationGroup="Guarda" 
                    InitialValue="0" 
                    Display="dynamic" 
                    ErrorMessage='* Seleccione un Banco Destino' 
                    runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="style1">
            Cuenta Destino</td>
        <td style="text-align: left">

            <asp:DropDownList ID="ddlCtaDestino" runat="server" Height="25" Width="200px">

            </asp:DropDownList>
            &nbsp;<asp:RequiredFieldValidator ControlToValidate="ddlCtaDestino" 
                    ValidationGroup="Guarda" 
                    InitialValue="0" 
                    Display="dynamic" 
                    ErrorMessage='* Seleccione una cuenta Destino' 
                    runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblImporte" runat="server" CssClass="labeltipopagoforma" Text="Importe:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtImporte" runat="server" CssClass="textboxcaptura" Width="150px"></asp:TextBox>
            <cc2:FilteredTextBoxExtender ID="ftbImporte" runat="server" FilterType="Custom" TargetControlID="txtImporte" ValidChars="0123456789./">
            </cc2:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="rfvImporte" runat="server" ControlToValidate="txtImporte" Display="None" ErrorMessage="Capturar Importe" ValidationGroup="Guarda"></asp:RequiredFieldValidator>
            <cc2:ValidatorCalloutExtender ID="vceImporte" runat="server" TargetControlID="rfvImporte">
            </cc2:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblObservaciones" runat="server" CssClass="labeltipopagoforma"
                    Text="Observaciones:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtObservaciones" runat="server" Width="300px" Height="75px"  TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:ImageButton ID="btnbAceptar" runat="server"
                OnClick="btnAceptar_Click"
                SkinID="btnAceptar" ValidationGroup="Guarda" style=" margin-left: 0px;" ImageUrl="~/Images/btnAceptar.png" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Label ID="lblError" runat="server" CssClass="labeltipopagoforma"
                ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
</asp:Panel>
</div>


<div style="text-align: right" id ="Anticipo">
<asp:Panel ID="pnlAnticipo" runat="server">
<table style="background-color: #e1f8e2; height: 360px; width: 900px">
    <tr>
        <td  colspan="2" class="HeaderMainStyle" align="center">
            <asp:Label ID="lblAntTitulo" runat="server" CssClass="labeltipopagoheader" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblAntCliente" CssClass="labeltipopagoforma" runat="server" 
                    Text="Cliente:"></asp:Label>
            </div>
        </td>
        <td>
            <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                TargetControlID="txtAntCliente"></cc2:FilteredTextBoxExtender>
            <asp:TextBox ID="txtAntCliente" runat="server" CssClass="textboxcaptura" Width="150px" ValidChars="0123456789" onblur="return ConsultaCteAnticipo()" ></asp:TextBox >
            <asp:RequiredFieldValidator ID="rfvAntCliente" runat="server"
                ControlToValidate="txtAntCliente" Display="None"
                ErrorMessage="Capturar el No. de Cliente"
                ValidationGroup="GuardaAnt"></asp:RequiredFieldValidator>
            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                TargetControlID="rfvAntCliente"></cc2:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblAntNombre" runat="server" CssClass="labeltipopagoforma"
                    Text="Nombre:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAntNombre"  ReadOnly ="true" Width="200px" runat="server" CssClass="textboxcaptura"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblAntSaldo" runat="server" CssClass="labeltipopagoforma"
                    Text="Saldo:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:ListBox ID="LstSaldos" runat="server"  Height="90px"  style="overflow-x:auto;margin-left: 0px" Width="222px" onchange ="return MontoSaldo()" OnSelectedIndexChanged="LstSaldos_SelectedIndexChanged" AutoPostBack="True" OnTextChanged="LstSaldos_SelectedIndexChanged" ></asp:ListBox>
            <asp:RequiredFieldValidator ID="rfvAntSaldo" runat="server"
                ControlToValidate="LstSaldos" Display="None"
                ErrorMessage="Capturar el Saldo"
                ></asp:RequiredFieldValidator>
            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                TargetControlID="rfvAntSaldo"></cc2:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblAntMonto" runat="server" CssClass="labeltipopagoforma"
                    Text="Monto:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAntMonto" runat="server" Width="150px" CssClass="textboxcaptura" ReadOnly="False"></asp:TextBox>
            <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAntMonto" FilterType="Custom" ValidChars="0123456789./"></cc2:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="rfvAntMonto" runat="server"
                ControlToValidate="txtAntMonto" Display="None"
                ErrorMessage="Capturar el Monto" ValidationGroup="GuardaAnt"
                ></asp:RequiredFieldValidator>
            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                TargetControlID="rfvAntMonto"></cc2:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <div>
                <asp:Label ID="lblAntObservaciones" runat="server" CssClass="labeltipopagoforma"
                    Text="Observaciones:"></asp:Label>
            </div>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAntOnservaciones" runat="server" Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>

        </td>       
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:ImageButton ID="btnAntAceptar" runat="server" OnClientClick="return ValidaMontoSaldo()"
                OnClick="btnAceptarAnticipo_Click" ImageUrl="~/Images/btnAceptar.png" 
                SkinID="btnAceptar" ValidationGroup="GuardaAnt" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Label ID="lblAntError" runat="server" CssClass="labeltipopagoforma"
                ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
</asp:Panel>
    <asp:HiddenField ID="HiddenInputUC" runat="server" Value="" />
      <asp:HiddenField ID="HiddenInputRegPago" runat="server" Value="" />  
    <br />
 </div>
