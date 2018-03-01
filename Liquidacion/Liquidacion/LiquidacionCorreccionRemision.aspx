<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableSessionState="True" CodeFile="LiquidacionCorreccionRemision.aspx.cs" Inherits="LiquidacionCorreccionRemision" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">
	<table align=center>
		<tr>
			<td class="HeaderPrecio ClientTextBox" >
				Corrección de remisiones
			</td>
		</tr>
		<tr>
			<td>
				<table style="text-align:left">
					<tr>
						<td>
							<table>
								<tr>
									<td class="HeaderPrecio ClientTextBox" width="20%">
										Pedido:
									</td>
									<td>
										<asp:TextBox ID="txtPedido" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:ImageButton ID="btnBuscar" runat="server" Height="25px" 
											ImageUrl="~/Imagenes/buscar.JPG" onclick="btnBuscar_Click"/>
									</td>
								</tr>
								<tr>
									<td class="HeaderPrecio ClientTextBox">
										Remision:
									</td>
									<td>
										<asp:TextBox ID="txtRemision" runat="server"></asp:TextBox>
									</td>
								</tr>					
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table>
								<tr>
									<td class="HeaderPrecio ClientTextBox" width="30%">
										Folio Liquidación:
									</td>
									<td width="80%">
										<asp:Label ID="lblFolio" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="HeaderPrecio ClientTextBox" width="30%">
										Fecha Suministro:
									</td>
									<td width="80%">
										<asp:Label ID="lblFSuministro" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="HeaderPrecio ClientTextBox">
										Cliente:
									</td>
									<td>
										<asp:Label ID="lblCliente" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="ClientTextBox">
										Nombre:
									</td>
									<td>
										<asp:Label ID="lblNombre" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="ClientTextBox">
										Domicilio:
									</td>
									<td>
										<asp:Label ID="lblDomicilio" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="ClientTextBox">
										Litros:
									</td>
									<td>
										<asp:Label ID="lblLitros" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="ClientTextBox">
										Total:
									</td>
									<td>
										<asp:Label ID="lblTotal" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="ClientTextBox">
										Forma de pago:
									</td>
									<td>
										<asp:Label ID="lblFormaPago" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table>
								<tr>
									<td>
										<asp:ImageButton ID="btnAceptar" runat="server" Height="25px" 
											ImageUrl="~/Images/btnAceptarVerde.jpg" onclick="btnAceptar_Click"/>
									</td>
									<td>
										<asp:ImageButton ID="btnCancelar" runat="server" Height="25px" 
											ImageUrl="~/Images/btnCancelarVerde.jpg" onclick="btnCancelar_Click"/>				
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblError" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>


