<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucConsultaCargoTarjetaCliente.ascx.cs" Inherits="ControlesUsuario_wucConsultaCargoTarjetaClienta"   %>
<asp:GridView ID="GrdPagosConTarjeta" runat="server" AutoGenerateColumns="False"  CellPadding="4" GridLines="None" ForeColor="#333333"  OnRowDataBound="OnRowDataBound" onselectchange = "OnSelectedIndexChanged" Width="319px"  
    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Justify"  >
    <AlternatingRowStyle BackColor="White" />
  <Columns>
        <asp:BoundField DataField="TipoCobro" HeaderText="Tipo Cobro" ItemStyle-Width="500" ItemStyle-HorizontalAlign="Left" >
<ItemStyle Width="500px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Tarjeta" HeaderText="Tarjeta" ItemStyle-Width="300" >
<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-Width="300" >
<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>
      <asp:BoundField DataField="Autorizacion" HeaderText="Autorización" ItemStyle-Width="300" >
<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>
       <asp:BoundField DataField="Importe" HeaderText="Importe" ItemStyle-Width="300" >
<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>
          <asp:BoundField DataField="Observacion" HeaderText="Observación" ItemStyle-Width="300" >

<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="Anio" HeaderText="Año" Visible="False" />
        <asp:BoundField DataField="Folio" HeaderText="Folio" Visible="False" />

    </Columns>


    <EditRowStyle BackColor="#7C6F57" />
    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#E3EAEB" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F8FAFA" />
    <SortedAscendingHeaderStyle BackColor="#246B61" />
    <SortedDescendingCellStyle BackColor="#D4DFE1" />
    <SortedDescendingHeaderStyle BackColor="#15524A" />



</asp:GridView>

<p>
    &nbsp;</p>

 <script type="text/javascript" >  

 </script>





