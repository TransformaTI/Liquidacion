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

public partial class Liquidacion : System.Web.UI.Page
{
    DataTable dtOperadores;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillOperadores();
        }
        else
        {
        }
    }

    private void FillOperadores()
    {
        dtOperadores = new DataTable();
        Parameter[] param = new Parameter[4];
        DataRow dr;
        
        try
        {
            dtOperadores.Columns.Add("Operador");
            dtOperadores.Columns.Add("Nombre");
            dtOperadores.Columns.Add("FechaIncidencia");
            dtOperadores.Columns.Add("MotivoNoAsignacion");
            dtOperadores.Columns.Add("Autorizacion");
            dtOperadores.Columns.Add("Fecha");
            dtOperadores.Columns.Add("Usuario");

            dr = dtOperadores.NewRow();
            dr["Operador"] = "1234";
            dr["Nombre"] = "Operador 1";
            dr["FechaIncidencia"] = "22/09/2008 10:03";
            dr["MotivoNoAsignacion"] = "Suministro en posición inconsistente";
            dr["Autorizacion"] = "AUTORIZADO";
            dr["Fecha"] = "22/09/2008 10:03";
            dr["Usuario"] = "USSIPR";
            dtOperadores.Rows.Add(dr);

            dr = dtOperadores.NewRow();
            dr["Operador"] = "1234";
            dr["Nombre"] = "Operador 1";
            dr["FechaIncidencia"] = "22/09/2008 10:03";
            dr["MotivoNoAsignacion"] = "Suministro en posición inconsistente";
            dr["Autorizacion"] = "AUTORIZADO";
            dr["Fecha"] = "22/09/2008 10:03";
            dr["Usuario"] = "USSIPR";
            dtOperadores.Rows.Add(dr);

            dr = dtOperadores.NewRow();
            dr["Operador"] = "1234";
            dr["Nombre"] = "Operador 1";
            dr["FechaIncidencia"] = "22/09/2008 10:03";
            dr["MotivoNoAsignacion"] = "Suministro en posición inconsistente";
            dr["Autorizacion"] = "AUTORIZADO";
            dr["Fecha"] = "22/09/2008 10:03";
            dr["Usuario"] = "USSIPR";
            dtOperadores.Rows.Add(dr);

            dr = dtOperadores.NewRow();
            dr["Operador"] = "1234";
            dr["Nombre"] = "Operador 1";
            dr["FechaIncidencia"] = "22/09/2008 10:03";
            dr["MotivoNoAsignacion"] = "Suministro en posición inconsistente";
            dr["Autorizacion"] = "AUTORIZADO";
            dr["Fecha"] = "22/09/2008 10:03";
            dr["Usuario"] = "USSIPR";
            dtOperadores.Rows.Add(dr);

            gvOperadores.DataSource = dtOperadores;
            gvOperadores.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

}