<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Liquidacion.aspx.cs" Inherits="Liquidacion" Culture="es-MX" UICulture="es" Theme="Theme1" EnableSessionState="True" MasterPageFile="~/MasterPage.master"%>

<%@ Register Assembly="LiquidacionWebControls" Namespace="SigametLiquidacion.WebControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">

<script type="text/jscript" src="Scripts/MiscControlFunctions.js"></script>
<script type="text/jscript" src="Scripts/MiscFunctions.js"></script>
<script type="text/jscript" src="Scripts/ValidacionConciliacion.js"></script>
<%--<script type="text/jscript">
function SeleccionarControlSiguiente(charCode, nextControlName)
{    
    if (charCode == 13)
    {   
        
        document.getElementById(nextControlName).focus();
        return true;
    }
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


</script>--%>

<div style="text-align:center">
      
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/ValidacionRemision.js" />
    </Scripts>
    <Services>
        <asp:ServiceReference  Path="~/WebServices/ValidacionRemision.asmx" />
    </Services>
</asp:ScriptManager>

<asp:UpdatePanel ID="UPHeaderListaPedidos" runat="server">
    <ContentTemplate>
        <table cellspacing="0" width="100%" style="height:675px">
            <tr>
                <td class="rightColumn" style="height:65px;vertical-align:top">
                    <cc2:AutoTanqueTurno ID="AutoTanqueTurno1" runat="server"/>
                </td>
                <td class="leftColum">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left:5px">
                                <asp:ImageButton ID="btnTerminar" runat="server" 
                                    ImageUrl="~/Images/finSesLiq.png" AlternateText="Terminar liquidación" 
                                    onclick="btnTerminar_Click" TabIndex="-1"/>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnPagos" runat="server" 
                                    ImageUrl="~/Images/captPagos.png" AlternateText="Capturar pagos"
                                    onclick="btnPagos_Click"
                                    TabIndex="-1"/>
                            </td>
                              <td>
                                <asp:ImageButton ID="imbReporte" runat="server" 
                                    ImageUrl="~/Images/imgReporte.png" AlternateText="Vista Previa Reporte"
                                    onclick="imbReporte_Click" Visible="false" 
                                    TabIndex="-1"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;height:500px">
                    <table>
                        <tr>
                            <td style="height:450px;width:100%;vertical-align:top">
                                <cc2:ListaPedidos ID="ListaPedidos1" runat="server" 
                                    ImageButtonURL="~/images/edit.jpg"
                                    ConfigFile="~/configListaPedidos.xml" 
                                    TipoOperacion="Conciliacion" 
                                    oneditarelemento="ListaPedidos1_EditarElemento"
                                    OnCargarDatos="ListaPedidos1_CargaDatos" onerror="ListaPedidos1_Error" 
                                    onsortlist="ListaPedidos1_SortList" onfiltrar="ListaPedidos1_Filtrar"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="EtiquetaCelulaRuta">
                                <asp:CheckBox ID="chkAutoRecorrido" runat="server" Text="Recorrido automático de lista"/>
                                &nbsp
                                | <asp:Label ID="lblMessageCenter" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc2:ResumenLiquidacion ID="ResumenLiquidacion1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align:top">
                    <table>
                        <tr>
                            <td class="HeaderMainStyle">
                                <div style="vertical-align:middle; text-align:center">
                                    <asp:Label ID="lblControlPedido" runat="server" Text="" 
                                        EnableTheming="False" EnableViewState="False"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <cc2:Pedido ID="nuevoPedido" runat="server" LayOutControl="LayoutVertical" 
                                                onclickaceptar="nuevoPedido_ClickAceptar" 
                                                onclickcancelar="nuevoPedido_ClickCancelar" 
                                                ondesasignarpedido="nuevoPedido_DesasignarPedido" 
                                                OnCambiarClientePedidoLiquidado="nuevoPedido_CambiarCliente"
                                                OnActualizarCliente="nuevoPedido1_Actualizar"
                                                PermitirCaptura="True" onerror="ListaPedidos1_Error"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>             
                </td>
            </tr>
            <tr>
                <td class="EtiquetaCelulaRuta" colspan="2" style="height:25px">
            </td>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<asp:Panel ID="pnlPopup" runat="server" Width="500px" style="display:block">
    <asp:UpdatePanel ID="updPnlFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button id="btnShowPopup" runat="server" style="display:none" />
            <cc1:ModalPopupExtender ID="modalFilter" runat="server" PopupControlID="pnlPopup" 
                    BackgroundCssClass="modalBackground" TargetControlID="btnShowPopup" CancelControlID="btnCerrarPopUp"/>  
            <div style="border-style:solid;border-color:Black;border-width:1px;background-color:White;
                text-align:center;vertical-align:middle;padding-top:10px;padding-bottom:10px;padding-left:25px;
                padding-right:25px">
                <table>
                    <tr class="HeaderMainStyle">
                        <td>
                            <asp:Label ID="lblFiltro" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCerrarPopUp" runat="server" SkinID="btnCerrarPopUp"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="middle">
                            <div style="vertical-align:middle">
                                <asp:Label CssClass="miscLabel" ID="lblTagFecha" runat="server" Text="Buscar:"></asp:Label>                                                
                                <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="btnCustomFilter" runat="server" Height="16px" 
                                    ImageUrl="~/Images/search_start.png" AlternateText="Consultar rutas" 
                                    style="width: 16px"
                                    OnCommand="btnCustomFilter_Command"/>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"">
                            <asp:CheckBox ID="chkFullMatch" runat="server" Text="Contenido exacto" Checked="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Repeater ID="lstFilter" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnbFilterChoice" runat="server" Text='<%# Bind("Value") %>'
                                        CommandName= '<%# Bind("Key") %>' CommandArgument='<%# Bind("Value") %>' 
                                        OnCommand="btnFiltrar_Command"></asp:LinkButton>                                       
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <span>|</span>
                                </SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>                
    </asp:UpdatePanel>
</asp:Panel>

<asp:Panel runat="server" ID="panelUpdateProgress">   
    <div style="border-style:solid;border-color:Black;border-width:1px;background-color:White;
        text-align:center;vertical-align:middle;padding-top:10px;padding-bottom:10px;padding-left:25px;
        padding-right:25px">
        <img alt="" src="Images/updateProgress.gif" style="width: 32px"/> <b> Procesando...</b>       
    </div>
</asp:Panel>

<cc1:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelUpdateProgress" 
    BackgroundCssClass="modalBackground" TargetControlID="panelUpdateProgress">
</cc1:ModalPopupExtender>

<script type="text/javascript" language="javascript">       
    var ModalProgress ='<%= ModalProgress.ClientID %>';        
</script>  

<script src="Scripts/jsUpdateProgress.js" type="text/javascript"></script>
</asp:Content>