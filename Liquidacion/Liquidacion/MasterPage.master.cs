using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
	if (Session["dsLiquidacion"] != null)
        {
            if (((DataSet)(Session["dsLiquidacion"])).Tables.Contains("Cobro")) 
            if (((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"].Rows.Count > 0)
            {
                btnSelFolio.Attributes.Add("onclick", "return confirm('Al regresar a Seleccion de Ruta los Pagos Capturados se perderán. ¿Desea Continuar?')");
            }
	
        }
	else
        {
            btnSelFolio.Attributes.Add("onclick", "return confirm('¿Desea regresar a Selección de Ruta?')");
        }
    }

    protected void btnCerrarSesion_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
    protected void btnSelFolio_Click(object sender, ImageClickEventArgs e)
    {
        Session["dsLiquidacion"] = null;
        Response.Redirect("SeleccionRutaLiquidacionDina.aspx");
    }
    protected void btnAdministracion_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Asignacion.aspx");
    }
    protected void MainMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {

    }
    protected void menuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
}
