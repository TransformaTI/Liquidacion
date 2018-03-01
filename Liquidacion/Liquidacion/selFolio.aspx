<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selFolio.aspx.cs" Inherits="selFolio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtAñoAtt" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
        <asp:Button ID="btnLoad" runat="server" onclick="btnLoad_Click" Text="Cargar Folio"/>
    </div>
    </form>
</body>
</html>
