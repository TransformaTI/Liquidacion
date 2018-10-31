<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerarPago.aspx.cs" Inherits="GenerarPago" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
  <script type="text/javascript" language="javascript">       
      var ModalProgress = '<%= ModalProgress.ClientID %>'; 
      var Saldo = '<%= HiddenSaldo.Value %>'; 
      var sMensajeSaldos = 'El cliente tiene saldos a favor ¿ Desea continuar?';
      var sMensajeConfirmacion='¿Desea guardar y Cerrar la liquidacion?'
      
      
      function ValidaSaldo() {     
          

          var respuestaConfirm = confirm(sMensajeConfirmacion);
          if (respuestaConfirm == false)
               {
                 return false;
               }
          else
          {
            
              if (parseFloat(Saldo.toString()) > 0)
              {
                     var respuesta = confirm(sMensajeSaldos);

                       if (respuesta == false)
                       {
                           return false;
                       }

              
              }
          }
      }


 </script> 
 <script src="Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
  <ContentTemplate> 
      <asp:HiddenField ID="HiddenSaldo" runat="server" Value="" />
    <div style="text-align:center">
   <table style="width:1000px;">
   <tr>
    <td class="EtiquetaCelulaRuta" colspan="2">
   
        Pagos Generados</td>
   
   </tr>
   
   <tr>
        <td style="text-align: center" colspan="2">
            <div style="width:900px; height:300px; overflow:scroll;"> 
            <asp:GridView ID="gvPagoGenerado" runat="server" AutoGenerateColumns="False" 
                 onselectedindexchanged="gvPagoGenerado_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="" ButtonType="Image" 
                        SelectImageUrl="~/Images/imgSeleccioneGrid.png" >
                        <ItemStyle HorizontalAlign="Center" ForeColor="Black"/>
                        
                    </asp:CommandField>
                    <asp:BoundField DataField="IdPago" HeaderText="Pago" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaAlta" HtmlEncode="false" HeaderText="Fecha Alta" DataFormatString="{0:d}"  ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreTipoCobro" HeaderText="Tipo Cobro" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumCheque" HeaderText="Autorización" />
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
        <td class="EtiquetaCelulaRuta" colspan="2">
        
            Detalle</td>
        
        <tr>
            <td align="center" colspan="2">
           <div style="width:400px; height:150px; overflow:scroll;">   
                <asp:DataList ID="dlDetallePago" runat="server" BackColor="#E0E0F8">
                <ItemTemplate>
              <asp:Image runat="server" ID="imgRetPedido" ImageUrl="~/Images/editing.png"/> <asp:Label ID="lbRetPedido" runat="server" Text="Pedido: " CssClass="labeltipopagoforma" /> <asp:Label ID="lbAnio" runat="server" Text='<%# Eval("Anio") %>' CssClass="labeltipopago" /> <asp:Label ID="lblCelula" runat="server" Text='<%# Eval("Celula") %>' CssClass="labeltipopago" /> <asp:Label ID="lblPedido" runat="server" Text='<%# Eval("Pedido") %>' CssClass="labeltipopago" /> 
               <asp:Image runat="server" ID="imgRetImporte" ImageUrl="~/Images/cash16_16.gif"/> <asp:Label ID="lblRetImporte" runat="server" Text="Importe Abonado: " CssClass="labeltipopagoforma" /> <asp:Label ID="Label1" runat="server" Text='<%# Eval("Importe", "{0:C}")%>' CssClass="labeltipopago" /><br /> 
                </ItemTemplate>
                </asp:DataList>
            </div>
            </td>
         
        </tr>
       
       
        <tr>
            <td >
                <asp:ImageButton ID="imbNuevoPago" runat="server" Height="25px" 
                    ImageUrl="~/Images/imgNuevoPago.png" onclick="imbNuevoPago_Click" 
                    ValidationGroup="Cheque" />
            </td>
            <td align="right">
            
                <asp:ImageButton ID="imbCancelarPagos" runat="server" CausesValidation="False" 
                    SkinId="btnCancelar"  Visible ="false"
                    onclick="imbCancelarPagos_Click" ImageUrl="~/Images/imgCancelarPagos.png"/>
            
            </td>
        </tr>
       
     </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <asp:ImageButton ID="imbGuardar" runat="server" Height="27px" 
                        ImageUrl="~/Images/imgGuardarPagos.png" onclick="imbGuardar_Click" />
                </td>
       </tr>
       <tr>
           <td style="text-align: left" colspan="2">
               <asp:Label ID="lblError" runat="server" CssClass="labeltipopago" 
                   ForeColor="Red"></asp:Label>
           </td>
       </tr>
       </table>
       </div>
   </ContentTemplate>
      </asp:UpdatePanel>
             
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
</asp:Content>


