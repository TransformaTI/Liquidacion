<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegistroPagos.aspx.cs" Inherits="RegistroPagos" Title="Untitled Page" Theme="Theme1" enableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
          <asp:HiddenField ID="HiddenSaldo" runat="server" Value="" />


      <script type="text/javascript" language="javascript"> 
        var sMensajeSaldos = 'El cliente tiene saldos a favor ¿ Desea continuar?';
          var Saldo = 0;

          function ValidaSaldos() { 

                      if (parseFloat(Saldo.toString()) > 0)
                      {
                             var respuesta = confirm(sMensajeSaldos);

                               if (respuesta == false)
                               {
                                   return false;
                               }

              
                      }
          
      }      
          
        
      
    

     </script> 
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
  <ContentTemplate> 
   <div style="text-align:center">
   <table style="width:1003px;">
       <tr>
           <td colspan="3"align="right">

               <asp:CheckBox ID="chbTodos" runat="server" OnCheckedChanged="chbTodos_CheckedChanged" Text="Todos" AutoPostBack="true"  />
           </td>
       </tr>
  <tr>
    <td colspan="3" width="555px" class="EtiquetaCelulaRuta">
    
        Pedidos :</td>
   
  </tr>
  
  <tr>
        <td align="center" colspan="3">
         <div style="width:1000px; height:300px; overflow:scroll;">                    
            
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False"
                onselectedindexchanged="gvPedidos_SelectedIndexChanged">
                <Columns>
                   <asp:CommandField ShowSelectButton="True" SelectText="Seleccione >>>"
                        ButtonType="Link">
                        <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    </asp:CommandField>
               <%--<asp:TemplateField ShowHeader="false">
                  <ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>
                        <asp:ImageButton ID="btnSeleccionar" runat="server" 
                        CausesValidation="False" CommandName="Select"
                        ImageUrl="~/Images/imgSeleccioneGrid.png" ToolTip="Selecciona" 
                          />
                       </ItemTemplate>
                  </asp:TemplateField>--%>
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" >
                      <ItemStyle HorizontalAlign="Center"/>
                      <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="pedidoreferencia" HeaderText="Referencia">
                        <ItemStyle HorizontalAlign="Center"/>
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="pedido" HeaderText="Pedido">
                        <ItemStyle HorizontalAlign="Center"/>   
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Celula" HeaderText="Celula">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="añoped" HeaderText="Año">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" >
                        <ItemStyle Width="350px" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Litros" HeaderText="Litros" >
                        <ItemStyle Width="100px" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:C}">
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:C}">
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Descuento" DataFormatString="{0:C}" HeaderText="Descuento" >
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign ="Center" /> 
                    </asp:BoundField>
                </Columns>
               
                <RowStyle CssClass="RowStyle" />
                <PagerStyle CssClass="PagerStyle" /> 
                <SelectedRowStyle CssClass="SelectedRowStyle" /> 
                <HeaderStyle CssClass="HeaderStyle" /> 
                <EditRowStyle CssClass="EditRowStyle" /> 
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
            </div>
        </td>
   </tr>
   
   <tr>
   
    <td class="EtiquetaCelulaRuta">
    
        Captura de Abono a Pedido</td>
       <td  class="EtiquetaCelulaRuta">
           Abonos</td>
    </tr>
    <tr>
       <td colspan="" align="left">
           <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblImporteTotal" runat="server" CssClass="labeltipopago" 
                            Font-Size="14px" Text="Total: "></asp:Label>
                        <asp:Label ID="lblImporteTotalA" runat="server" CssClass="labeltipopago" 
                            Font-Size="14px"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblImportePago" runat="server" CssClass="labeltipopago" 
                            Font-Size="14px" ForeColor="#3366CC" Text="Saldo: "></asp:Label>
                        <asp:Label ID="lblImportePagoA" runat="server" CssClass="labeltipopago" 
                            Font-Size="14px" ForeColor="#3366CC"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblImporteDocumento" runat="server" CssClass="labeltipopago" 
                            Text="Importe del Documento: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImporteDocto" runat="server" CssClass="textboxcaptura" 
                            ReadOnly="True" Font-Bold="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSaldoMovimiento" runat="server" CssClass="labeltipopago" 
                            Text="Saldo en el Movimiento: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSaldoMovimiento" runat="server" CssClass="textboxcaptura" 
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblImporteAbono" runat="server" CssClass="labeltipopago" 
                            Text="Importe para el Abono: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImporteAbono" runat="server" CssClass="textboxcaptura" 
                            Text="0.00"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImporteAbono" runat="server" 
                            ControlToValidate="txtImporteAbono" Display="None" 
                            ErrorMessage="Seleccione un Pedido y Capture el Importe del Abono" 
                            ValidationGroup="CapturaAbono"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvImporteAbonoCero" runat="server" 
                            ControlToValidate="txtImporteAbono" Display="None" 
                            ErrorMessage="El importe del Abono debe ser mayor a 0" InitialValue="0.00" 
                            ValidationGroup="CapturaAbono"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceImporteAbono" runat="server" 
                            TargetControlID="rfvImporteAbono">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="vceImporteAbonoCero" runat="server" 
                            TargetControlID="rfvImporteAbonoCero">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblDescuento" runat="server" CssClass="labeltipopago" 
                            ForeColor="#000099" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton ID="imbAceptarAbono" runat="server" Height="26px" 
                            ImageUrl="~/Images/btnAceptar.png" onclick="imbAceptarAbono_Click" 
                            ValidationGroup="CapturaAbono" Width="90px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" CssClass="textboxcaptura" 
                            Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td align="center">
        <div style="width:400px; height:170px; overflow:scroll;">  
            <asp:GridView ID="gvRelacionCobro" runat="server" AutoGenerateColumns="False" 
                CssClass="gridcontainer" 
                onselectedindexchanged="gvRelacionCobro_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Pedido" HeaderText="Pedido" />
                    <asp:BoundField DataField="Importe" HeaderText="Importe Abonado" DataFormatString="{0:C}"/>
                    <%--<asp:TemplateField ShowHeader="false">
                         <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" 
                                CausesValidation="False" CommandName="Select"
                                ImageUrl="~/Images/imgEliminarGrid.png" ToolTip="Eliminar" 
                                    onclick="btnEliminar_Click" />
                            </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:CommandField SelectText="Borrar" ShowSelectButton="True" 
                        ButtonType="Link">
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
       </div>
            
        </td>
        </tr>
        <tr>
            <td align="center" colspan="" style="">
                <asp:ImageButton ID="imbRedirAbonos" runat="server" CausesValidation="False" 
                    ImageUrl="~/Images/imgAceptarAbonos.png" onclick="imbRedirAbonos_Click" 
                    Height="28px" Width="128px" />
            </td>
            <td align="right">
                <asp:ImageButton ID="imbCancelarAbonos" runat="server" CausesValidation="False" 
                    ImageUrl="~/Images/imgCancelarAbonos.png" onclick="imbCancelarAbonos_Click" />
            </td>
       </tr>
        <tr>
        <td colspan="2" align="center">
            
            &nbsp;</td>
        <td>
            &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
           
        </tr>
        </table>
        </td>
        <td align="center">
        </td>
        
   </tr>
   <tr>
   <td>
   </td>
   <td colspan=2 style="text-align: center">
   
   </td>
   </tr>
   </table>    
    </div>
         </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>


