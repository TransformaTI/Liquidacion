using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class Login : System.Web.UI.Page
{
    SigametLiquidacion.Seguridad _seguridad;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargaConexiones();
        }
    }

    protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
    {
        Session["Conexion"] = ddlConexiones.SelectedValue.ToString();

        _seguridad = new SigametLiquidacion.Seguridad(22, txtUserName.Text, txtContrasenia.Text);

        if (!_seguridad.SesionIniciada)
        {
            lblMensajeInicio.Text = _seguridad.MensajeAcceso;
            return;
        }

        Session["Iniciada"] = _seguridad.SesionIniciada;
        Session["Privilegios"] = _seguridad;
        Session["Usuario"] = txtUserName.Text;
        Session["dsLiquidacion"] = null;
        Session["CargoTarjeta"] = null;
        Session["corporativo"] = _seguridad.Usuario.Rows[0]["corporativo"]; ;

        //Response.Redirect("selFolio.aspx");
        Response.Redirect("SeleccionRutaLiquidacionDina.aspx");
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        _seguridad = null;
        txtContrasenia.Text = string.Empty;
        txtUserName.Text = string.Empty;
        lblMensajeInicio.Text = string.Empty;
    }
    private void CargaConexiones()
    {
        ListItem dropList = new ListItem();
        string nombre;
        string con;

        try
        {

            Configuration conf = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Liquidacion");

            for (int i = 1; i <= conf.ConnectionStrings.ConnectionStrings.Count; i++)
            {
                nombre = conf.ConnectionStrings.ConnectionStrings[i].Name;
                con = conf.ConnectionStrings.ConnectionStrings[i].ToString();
                conf.ConnectionStrings.ConnectionStrings[i].ToString();
                ddlConexiones.Items.Add(new ListItem(nombre, con));
            }
        }   
        catch (Exception ex)
        {

        }
    }
}
