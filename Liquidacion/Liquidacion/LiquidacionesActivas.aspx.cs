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

public partial class LiquidacionesActivas : System.Web.UI.Page
{
    AdministracionUsuarios au = new AdministracionUsuarios();
    DataTable dtUsuarios;
    DataTable dtLiqActivas;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ConsultaUsuarios();
            
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
    private void ConsultaLiquidaciones()
    {
        try
        {
            dtLiqActivas = au.ListaLiquidacionActiva(tvUsuarios.SelectedNode.Text, "INICIADO");
            gvLiquidacionesActivas.DataSource = dtLiqActivas;
            gvLiquidacionesActivas.DataBind();
        }
        catch
        {
            throw;
        }

    }
  
    
    protected void tvUsuarios_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ConsultaLiquidaciones();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void gvLiquidacionesActivas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int anio;
        int folio;
        
        try
        {
            anio = Convert.ToInt32(gvLiquidacionesActivas.SelectedDataKey.Value);
            folio = Convert.ToInt32(gvLiquidacionesActivas.SelectedRow.Cells[0].Text);

            au.ActualizaLiquidacionActiva(anio, folio);
            ConsultaLiquidaciones();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
        
    protected void gvLiquidacionesActivas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLiquidacionesActivas.PageIndex = e.NewPageIndex;
        ConsultaLiquidaciones();
    }
    protected void gvLiquidacionesActivas_PageIndexChanged(object sender, EventArgs e)
    {
      
    }
    
}
