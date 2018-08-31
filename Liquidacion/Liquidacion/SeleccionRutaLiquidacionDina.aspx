<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SeleccionRutaLiquidacionDina.aspx.cs" Inherits="SeleccionRutaLiquidacionDina" Theme="Theme1" UICulture="es" Culture="es-MX" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True"></asp:ScriptManager>
    <table style="text-align:left;vertical-align:top;width:100%;height:675px">
        <tr>
            <td style="height:100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>           
                        <table style="width:100%;height:100%" cellspacing="0">
                            <tr>
                                <td style="height:25px">
                                    <table cellspacing="0">
                                        <tr>
                                            <td valign="middle">
                                                <asp:Label CssClass="miscLabel" ID="lblTagFecha" runat="server" Text="Fecha de inicio:"></asp:Label>                                                
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFAsignacion" runat="server" Text="" CssClass="calendarTextBox" Enabled="false"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFAsignacion_CalendarExtender" runat="server" 
                                                    TargetControlID="txtFAsignacion" Format="dd/MM/yyyy" PopupButtonID="btnCalFAsignacion" >
                                                </cc1:CalendarExtender>
                                                <asp:ImageButton runat="Server" ID="btnCalFAsignacion" ImageUrl="~/images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" />
                                                <asp:ImageButton ID="btnCargaRutas" runat="server" Height="16px" 
                                                    ImageUrl="~/Images/search_start.png" AlternateText="Consultar rutas" 
                                                     style="width: 16px" OnClick="btnCargaRutas_Click"/>
                                                <asp:Label ID="lblError" runat="server" CssClass="textboxcaptura" 
                                                    ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>                 
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height:650px">
                                    <table cellspacing="0" style="width:100%;height:100%;vertical-align:top">
                                        <tr style="height:24px">
                                            <td class="EtiquetaCelulaRuta" style="vertical-align:middle;width:20%">
                                                <asp:Label runat="server" ID="lblCelulas" Text="Células:" ForeColor="White" 
                                                    Font-Bold="true" style="color: #000000"></asp:Label>
                                            </td>
                                            <td class="EtiquetaCelulaRuta" style="width:80%">
                                                <asp:Label runat="server" ID="lblRutas" Text="Autotanques asignados:" 
                                                    ForeColor="White" Font-Bold="true" style="color: #000000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="height:600px">
                                                <asp:Panel ID="pnlCels" runat="server" ScrollBars="Vertical" Height="100%">
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Table ID="tbCelulas" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td valign="top" style="height:600px">
                                                <asp:Panel ID="pnlFolios" runat="server" ScrollBars="Vertical" Height="100%">
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td style="text-align:center">
                                                                <asp:Table ID="tbRutas" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaCelulaRuta" colspan="2" style="height:25px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </td>
        </tr>
    </table>
    <%--Ventana de confirmación para liquidación automática--%>
    <asp:Panel ID="pnlPopup" runat="server" Width="500px" style="display:none">
        <asp:UpdatePanel ID="updPnlSeleccionFormaLiquidacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button id="btnShowPopup" runat="server" style="display:none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlPopup" 
                    BackgroundCssClass="modalBackground" TargetControlID="btnShowPopup" CancelControlID="btnCerrarPopUp"/>  
                    <table style="border-style:solid;border-color:Black;border-width:thin">
                        <tr>
                            <td>
                                <table>
                                    <tr class="HeaderMainStyle">
                                        <td>
                                            <span>Liquidación de ruta</span>
                                        </td>
                                        <td>
                                            <div style="vertical-align:middle">
                                                <asp:ImageButton ID="btnCerrarPopUp" runat="server" SkinID="btnCerrarPopUp"/>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <span><br />Se recuperaron registros del medidor RI/tarjeta rampac:<br /><br /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="Liquidacion.aspx?FormaLiquidacion=AUTOMATICA">
                                                Haga click aquí para realizar la liquidación automática<br /></a>
                                        </td>            
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="Liquidacion.aspx?FormaLiquidacion=MANUAL">
                                                Haga click aquí para realizar la liquidación manual<br /><br /></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>                
        </asp:UpdatePanel>
    </asp:Panel>
        <%--<asp:Panel runat="server" ID="panelUpdateProgress">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" > 
                <ProgressTemplate>
                    <img alt="" src="Images/updateProgress.gif"/><b>  Procesando... </b>
                </ProgressTemplate> 
            </asp:UpdateProgress>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelUpdateProgress" 
            BackgroundCssClass="modalBackground" TargetControlID="panelUpdateProgress">
        </cc1:ModalPopupExtender>    
    </div>--%>
</asp:Content>