﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinLiquidacion.aspx.cs" Inherits="FinLiquidacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>       
        <table>            
            <tr>
                <td>
                    <asp:Label ID="lblMensaje1" runat="server" Text="Fin del proceso de liquidación"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Folio: "></asp:Label>
                    <asp:Label ID="lblAñoAtt" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblFolio" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
