<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bitacora.aspx.cs" Inherits="Liquidacion" Culture="auto" UICulture="auto" Theme="Theme1" EnableSessionState="True" MasterPageFile="~/MasterPage.master"%>

<%@ Register Assembly="LiquidacionWebControls" Namespace="SigametLiquidacion.WebControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <script type="text/jscript" src="Scripts/MiscControlFunctions.js"></script>
    <script type="text/jscript" src="Scripts/MiscFunctions.js"></script>

    <div style="text-align:center">
          
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
             <td style="font-family:Tahoma;font-size:large;font-weight:bold" class="style2">
                 Bitácora</td>
              
        
        </tr>
        <tr>
            <td class="style2">
            
                <table class="style3">
                    <tr>
                         <td style="font-family:Tahoma;font-size:small;font-weight:bold" class="style4" colspan="3">
              <asp:Panel ID="pnlFechaRuta" runat="server" Width="600px">                                   
                                            <div style="padding:5px; cursor: pointer; vertical-align: middle; width: 400px;">
                                            <div style="float: left; vertical-align:middle">
                                                <asp:Label ID="lblTagFecha" runat="server" Text="Fecha:"></asp:Label>
                                            </div>
                                            <div style="float: left; margin-left: 10px;">
                                                <asp:TextBox ID="txtFAsignacion" runat="server" Text="" 
                                                    CssClass="calendarTextBox" Enabled="false" Width="88px"></asp:TextBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle">
                                                <asp:ImageButton runat="Server" ID="btnCalFAsignacion" ImageUrl="~/images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" />
                                            </div>
                                            <cc2:CalendarExtender ID="txtFAsignacion_CalendarExtender" runat="server" 
                                                    TargetControlID="txtFAsignacion" Format="dd/MM/yyyy" PopupButtonID="btnCalFAsignacion">
                                             </cc2:CalendarExtender>
                                            </div>                                            
              </asp:Panel>
            </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="lblCelula" runat="server" Text="Célula:" style="font-weight: 700"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCelula" runat="server" Font-Size="Smaller">
                            <asp:ListItem Text=" - Seleccione -" Value = "0"/> 
                            <asp:ListItem Text=" 1" Value = "1" /> 
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="lblRuta" runat="server" Text="Ruta:" style="font-weight: 700"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRuta" runat="server" Font-Size="Smaller" Height="16px">
                            <asp:ListItem Text=" - Seleccione -" Value = 0 /> 
                            <asp:ListItem Text=" 2" Value = 2 /> 
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                    <asp:Label ID="lblFolio" runat="server" Text="Folio:" style="font-weight: 700"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFolio" runat="server" Font-Size="Smaller" 
                                Height="16px">
                              <asp:ListItem Text=" - Seleccione -" Value = 0 /> 
                            <asp:ListItem Text="2008-12345" Value = 1 /> 
                            </asp:DropDownList>
                        </td>
                    </tr>
                 
                </table>
            
            </td>        
        </tr>
        
        <tr>
            <td align="center" colspan=2>
                <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" 
                    Width="545px">
                    <Columns>
                        <asp:BoundField DataField="Evento" HeaderText="Evento" />
                        <asp:BoundField DataField="Pedido" HeaderText="Pedido" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    </Columns>
                    <HeaderStyle BackColor="#009933" ForeColor="White" />
                </asp:GridView>
                
            </td>
        </tr>
    </table>
    </div>
    
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style2
        {
            width: 327px;
        }
        .style3
        {
            width: 48%;
        }
        .style4
        {
            width: 519px;
        }
        .style6
        {
            width: 59px;
        }
    </style>

</asp:Content>
