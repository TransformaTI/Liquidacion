<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutorizacionOperadores.aspx.cs" Inherits="Liquidacion" Culture="auto" UICulture="auto" Theme="Theme1" EnableSessionState="True" MasterPageFile="~/MasterPage.master"%>

<%@ Register Assembly="LiquidacionWebControls" Namespace="SigametLiquidacion.WebControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">
  
    <script type="text/jscript" src="Scripts/MiscControlFunctions.js"></script>
    <script type="text/jscript" src="Scripts/MiscFunctions.js"></script>

    <div style="text-align:center">
          
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0">
        <tr>
           <td style="font-family:Tahoma;font-size:large;font-weight:bold" colspan="6">
                Autorización de Operadores
             </td>
        
        </tr>
        <tr>
            <td style="font-family:Tahoma;font-size:small;font-weight:bold" align="left" 
                class="style6">
                
                
              <asp:Panel ID="pnlFechaRuta" runat="server" Width="395px">                                   
                                            <div style="padding:5px; cursor: pointer; vertical-align: middle; height: 22px; width: 392px;">
                                            <div style="float: left; vertical-align:middle">
                                                <asp:Label ID="lblTagFecha" runat="server" Text="Fecha:"></asp:Label>
                                            </div>
                                            <div style="float: left; margin-left: 10px;">
                                                &nbsp;<asp:TextBox ID="txtFAsignacion" runat="server" Text="" 
                                                    CssClass="calendarTextBox" Enabled="false" Width="88px"></asp:TextBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle">
                                                &nbsp;<asp:ImageButton runat="Server" ID="btnCalFAsignacion" ImageUrl="~/images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" />
                                            </div>
                                            
                                        </div>                                            
              </asp:Panel>
            </td>
            <td>
             <asp:Label ID="Label1" runat="server" Text="Autorizar:" style="font-weight: 700"></asp:Label>
                     &nbsp;<asp:ImageButton ID="btnAutorizaOperador" runat="server" 
                     ImageUrl="~/Images/verify_mark.png" Height="34px" Width="38px" />
            </td>
        </tr>
      
        <tr>
            <td class="style1" style="vertical-align: top" align=center colspan = "5">
                
                <asp:GridView ID="gvOperadores" runat="server" AutoGenerateColumns="False" 
                    Width="758px">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccione">
                            <ItemTemplate>
                                <asp:CheckBox ID="chOperador" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Operador" HeaderText="Operador" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="FechaIncidencia" HeaderText="Fecha de Incidencia" />
                        <asp:BoundField DataField="MotivoNoAsignacion" HeaderText="Motivo No Asignacion" />
                        <asp:BoundField DataField="Autorizacion" HeaderText="Autorizacion" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />    
                    </Columns>
                    <HeaderStyle BackColor="#009933" ForeColor="White" />
                </asp:GridView>
                  <cc2:CalendarExtender ID="txtFAsignacion_CalendarExtender" runat="server" TargetControlID="txtFAsignacion" Format="dd/MM/yyyy" PopupButtonID="btnCalFAsignacion">
                  </cc2:CalendarExtender>
            </td>
        </tr>
       
    </table>
    </div>
    <cc2:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnAutorizaOperador" PopupControlID="pnlWindowOperador"
        CancelControlID="btnCancelar" BackgroundCssClass="modalBackground"></cc2:ModalPopupExtender>
    <asp:Panel ID="pnlWindowOperador" runat="server" style="display:none;" 
        CssClass="modalPopup">
        <table>
            <tr>
                <td colspan="2">
                    Autorización de Asignación a Operador
                </td>
            </tr>
                 
            <tr>
                <td class="leftElements tagOperador">
                    Operador:
                </td>
                <td class="rightElements Operador">
                    1234 - Operador 1
                </td>
            </tr>
            <tr>
                <td class="leftElements tagNoAsignacion">
                    Motivo de no asignación:</td>
                <td class="rightElements NoAsignacion">
                    Suministro en posición incorrecta    
                </td>
            </tr>
            <tr>
                <td class="leftElements tagFecha">
                    Fecha:</td>
                <td class="rightElements Fecha">
                    22/09/08 16:09  
                </td>
            </tr>
        </table>
        <div style="text-align: right; width: 100%; margin-top: 5px;">
            <asp:ImageButton ID="btnAceptar" runat="server" SkinID="btnAceptar" AlternateText=""/>
            <asp:ImageButton ID="btnRechazar" runat="server" SkinID="btnRechazar"/>
            <asp:ImageButton ID="btnCancelar" runat="server" SkinID="btnCancelar"/>
        </div>
    </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            width: 764px;
        }
        .style6
        {
            width: 248px;
        }
    </style>

</asp:Content>
