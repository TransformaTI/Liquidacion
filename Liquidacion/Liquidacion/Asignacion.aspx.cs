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
using SigametLiquidacion;

public partial class Asignacion : System.Web.UI.Page
{
    AdministracionUsuarios au = new AdministracionUsuarios();
    DataTable dtUsuarios;
    DataTable dtCelulas;
    DataTable dtRelacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ConsultaUsuarios();
            ConsultaCelulas();
        }
    }

    private void ConsultaUsuarios()
    {
        try
        {

            dtUsuarios = au.ListaUsuarios();
            TreeNode node;
            for (int i = 0; i < dtUsuarios.Rows.Count; i++)
            {
                node = new TreeNode(dtUsuarios.Rows[i][0].ToString());
                tvUsuarios.Nodes.Add(node);
                node = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void ConsultaCelulas()
    {
        try
        {
            dtCelulas = au.ListaCelulas();
            ddlCelulas.DataSource = dtCelulas;
            ddlCelulas.DataValueField = dtCelulas.Columns["Celula"].ToString();
            ddlCelulas.DataTextField = dtCelulas.Columns["Descripcion"].ToString();
            ddlCelulas.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void ConsultaRelacion()
    {
        try
        {
            dtRelacion = au.ListaRelacion(DateTime.Now.ToString(), tvUsuarios.SelectedNode.Text);
            gvRelacionUsuarioRuta.DataSource = dtRelacion;
            gvRelacionUsuarioRuta.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool ValidaRelacion(int Celula)
    {
        bool exists = false;
        try
        {
            dtRelacion = new DataTable();
            dtRelacion = au.ListaRelacion(DateTime.Now.ToString(), tvUsuarios.SelectedNode.Text);

            for (int i = 0; i <= dtRelacion.Rows.Count - 1; i++)
            {
                if (Convert.ToUInt32(dtRelacion.Rows[i]["Celula"].ToString()) == Celula)
                {
                    lblError.ForeColor = System.Drawing.Color.LightBlue;
                    lblError.Text = "La Celula ya está asignada al Usuario";
                    return exists;
                }
            }
            lblError.Text = "";
            dtRelacion = null;
            exists = true;
            return exists;
        }
        catch
        {
            throw;
        }
    }


    protected void imbAceptar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (ValidaRelacion(Convert.ToUInt16(ddlCelulas.SelectedValue)))
            {
            
            dtRelacion = new DataTable();
            dtRelacion.Columns.Add(new DataColumn("FAsignacion"));
            dtRelacion.Columns.Add(new DataColumn("Usuario"));
            dtRelacion.Columns.Add(new DataColumn("Status"));

            dtRelacion.Columns.Add(new DataColumn("FAlta"));
            dtRelacion.Columns.Add(new DataColumn("FModificacion"));
            dtRelacion.Columns.Add(new DataColumn("UsuarioAsignacion"));

            dtRelacion.Columns.Add(new DataColumn("TipoAsignacion"));
            dtRelacion.Columns.Add(new DataColumn("GrupoAsignado"));

            DataRow dr;
            dr = dtRelacion.NewRow();
            dr["FAsignacion"] = "01/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            dr["Usuario"] = txtUsuario.Text;
            dr["Status"] = "VIGENTE"; //TO DO
            dr["FAlta"] = DateTime.Now.ToString();
            dr["FModificacion"] = DateTime.Now.ToString();
            dr["UsuarioAsignacion"] = tvUsuarios.SelectedNode.Text;
            dr["TipoAsignacion"] = "CELULA"; //TO DO
            dr["GrupoAsignado"] = ddlCelulas.SelectedValue;
            dtRelacion.Rows.Add(dr);

            au.GuardaUsuarioCelula(dtRelacion);
            dtRelacion = null;

            ConsultaRelacion();
            txtUsuario.Text = "";
            //txtCelula.Text = "";
            //txtFechaAsignacion.Text = "";
            ddlCelulas.SelectedIndex = 0;
            txtUsuario.Text = tvUsuarios.SelectedNode.Text;
          }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void tvUsuarios_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            txtUsuario.Text = tvUsuarios.SelectedNode.Text;
            ConsultaRelacion();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
   
    protected void gvRelacionUsuarioRuta_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            au.EliminaUsuarioCelula(Convert.ToInt32(gvRelacionUsuarioRuta.SelectedDataKey.Value), "CANCELADO", DateTime.Now.ToString("dd/MM/yyyy")); //TO DO - HARD CODE
            ConsultaRelacion();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    
}
