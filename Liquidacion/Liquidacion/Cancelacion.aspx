<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cancelacion.aspx.cs" Inherits="Cancelacion" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
<asp:UpdatePanel ID="UPHeaderListaPedidos" runat="server">
    <ContentTemplate>    
<div style="text-align:center">
        <table style="text-align:left">
            <tr>
                <td colspan="2" class="HeaderMainStyle">
                    Cancelación de liquidación
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <table style="text-align:left">
                        <tr>
                            <td class="HeaderPrecio ClientTextBox">
                                Año:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAñoAtt" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ClientTextBox">
                                Folio:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>                
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:right">
                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="MiscFields" 
                                    onclick="btnBuscar_Click">Buscar</asp:LinkButton>                                     
                                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="MiscFields" 
                                    onclick="btnCancelar_Click">Cancelar</asp:LinkButton>                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="HeaderMainStyle">
                    Status: <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td class="TagLabel">
                                            Litros</td>
                                        <td class="TagLabel">
                                            Importe</td>
                                    </tr>
                                    <tr>
                                        <td class="ClientTextBox">
                                            Contado:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLitrosContado" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblImporteContado" runat="server"></asp:Label>
                                        </td>                                
                                    </tr>
                                    <tr>
                                        <td class="ClientTextBox">
                                            Crédito:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLitrosCredito" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblImporteCredito" runat="server"></asp:Label>
                                        </td>                                
                                    </tr>
                                    <tr>
                                        <td class="ClientTextBox">
                                            Total:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLitrosTotal" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblImporteTotal" runat="server"></asp:Label>
                                        </td>                                
                                    </tr>
                                </table>           
                            </td>
                        </tr>
                        <tr>
                            <td class="ClientTextBox">
                                Pagos capturados
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView runat="server" ID="grdCobros" AutoGenerateColumns="false" BorderStyle="None">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="ClientTextBox">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoCobro" runat="server" Text='<%# Eval("TipoCobro") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="TagLabel">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalCobro" runat="server" Text='<%# Eval("TotalAbono") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aplicado" HeaderStyle-CssClass="TagLabel">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalCobroAplicado" runat="server" Text='<%# Eval("TotalAplicado") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>                                   
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align:top">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnCancelarPagos" runat="server" CssClass="MiscFields" 
                                    Enabled="False" onclick="btnCancelarPagos_Click" Visible="False">Cancelar Pagos</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnCancelarPedidos" runat="server" CssClass="MiscFields" 
                                    Enabled="False" onclick="btnCancelarPedidos_Click" Visible="False">Cancelar Pedidos</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="HeaderMainStyle">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>                                
                </td>
            </tr>
        </table>
    </div>
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
</ContentTemplate> 
</asp:UpdatePanel>
</asp:Content>