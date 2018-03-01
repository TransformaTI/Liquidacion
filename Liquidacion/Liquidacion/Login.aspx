<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Theme="Theme1"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
	
    <title>Liquidacion de Ruta</title>
    <link href="Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="text-align:center">    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="width:1100px;height:750px;text-align:left;vertical-align:middle">
            <table style="width:100%;height:100%;border-style:solid;border-color:Black">
                <tr style="height:auto">
                    <td style="width:7%">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ATRutaIniciada.png" Width="75px" Height="45px"/>
                    </td>
                    <td class="ApplicationTitle" style="width:85%">
                        Liquidación de ruta
                    </td>
                </tr>
                <tr style="height:85%;background-color:Silver">
                    <td colspan="2" style="text-align:center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>      
                                <table style="text-align:left;border-style:solid;border-width:thin">
                                    <tr>
                                        <td colspan="2" class="Caption">
                                            <asp:Label ID="lblCaption" runat="server" Text="Inicio de sesión"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Left">
                                            <asp:Label ID="lblTagUserName" runat="server" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td class="Right">
                                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Left">
                                            <asp:Label ID="lblTagContraseña" runat="server" Text="Contraseña:"></asp:Label>
                                        </td>
                                        <td class="Right">
                                            <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Left">
                                            <asp:Label ID="Label1" runat="server" Text="Conexión:" Visible="False"></asp:Label>
                                        </td>                        
                                        <td class="Right">
                                            <asp:DropDownList ID="ddlConexiones" runat="server" Font-Size="Smaller" 
                                                Width="150px" Visible="False" />
                                        </td>                            
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnAceptar" runat="server" SkinID="btnAceptar" 
                                                            onclick="btnAceptar_Click"/>
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnCancelar" runat="server" SkinID="btnCancelar" 
                                                            onclick="btnCancelar_Click"/>
                                                    </td>
                                                </tr>                                
                                            </table>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMensajeInicio" runat="server" Text=""></asp:Label>   
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="width:10%">
                    <td>
                        
                    </td>
                    <td style="text-align:right">
                        Sigamet
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
