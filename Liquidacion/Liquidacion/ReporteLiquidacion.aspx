<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteLiquidacion.aspx.cs" Inherits="ReporteLiquidacion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
   
    <script type="text/javascript" language="javascript">   
function CallPrint(strid)
{

//var prtContent = document.getElementById(strid);
//var WinPrint =
window.open('Impresion.aspx','','left=0,top=0,toolbar=0,scrollbars=0,status=0, resizable=1, width=800, heigth=600');
//WinPrint.document.write(prtContent.innerHTML);
//WinPrint.document.close();
//WinPrint.focus();
//WinPrint.print();   
//WinPrint.close();

}
</script>



   <div style="text-align:center; width:1000px; height:1000px ">
	 <%--style="WIDTH: 116.9mm; HEIGHT: 180.4mm; border-color: Black; text-align:center;"--%>
      <div id = "divPrint">
  
            <cr:crystalreportviewer ID="crviewRep" runat="server" Height="180.4mm" 
                Width="116.9" Font-Bold="True" OnUnload="crviewRep_Unload" 
                PrintMode="ActiveX" EnableDrillDown="True" />
  
        </div>
        
    </div>
     
    <div style="text-align:center;">
        <table style="text-align:center;">
        <tr>
        <td align="left">
            <asp:ImageButton ID="ImageButton1" runat="server"            
            Visible="True" ImageUrl="~/Images/imgImprimir.png" 
                OnClientClick="javascript:CallPrint('divPrint')"/>
       </td>
        <td align="right">
            &nbsp;</td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="lblError" runat="server" CssClass="labeltipopago" ForeColor="Red">                                          
        </asp:Label>
        </td>
        </tr>
     </table>


</div>

</ContentTemplate>

     	
</asp:Content>