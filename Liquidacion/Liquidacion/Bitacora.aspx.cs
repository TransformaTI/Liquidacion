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
    DataTable dtBitacora;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillBit();
        }
        else
        {
        }
    }

    private void FillBit()
    {
        dtBitacora = new DataTable();
        Parameter[] param = new Parameter[4];
        DataRow dr;
        
        try
        {
            dtBitacora.Columns.Add("Evento");
            dtBitacora.Columns.Add("Pedido");
            dtBitacora.Columns.Add("Usuario");
            dtBitacora.Columns.Add("Fecha");

            dr = dtBitacora.NewRow();
            dr["Evento"] = "Cierre de liquidación con diferencia de totalizador";
            dr["Pedido"] = "";
            dr["Usuario"] = "SIUSPR";
            dr["Fecha"] = "22/09/2008 10:03";
            dtBitacora.Rows.Add(dr);

            dr = dtBitacora.NewRow();
            dr["Evento"] = "Cambio del cliente 502316958 al cliente 60157932";
            dr["Pedido"] = "2005345543";
            dr["Usuario"] = "SIUSPR";
            dr["Fecha"] = "22/09/2008 10:03";
            dtBitacora.Rows.Add(dr);

            dr = dtBitacora.NewRow();
            dr["Evento"] = "Cambio de contado a crédito";
            dr["Pedido"] = "20053343434";
            dr["Usuario"] = "SIUSPR";
            dr["Fecha"] = "22/09/2008 10:03";
            dtBitacora.Rows.Add(dr);

            param[0] = new Parameter("Cierre de liquidación con diferencia de totalizador", TypeCode.String);
            param[1] = new Parameter("", TypeCode.String);
            param[2] = new Parameter("SIUSPR", TypeCode.String);
            param[3] = new Parameter("22/09/2008 10:03", TypeCode.String);

            dtBitacora.Rows.Add(param);

            param[0] = new Parameter("Cambio del cliente 502316958 al cliente 60157932", TypeCode.String);
            param[1] = new Parameter("2005345543", TypeCode.String);
            param[2] = new Parameter("SIUSPR", TypeCode.String);
            param[3] = new Parameter("22/09/2008 10:03", TypeCode.String);

            dtBitacora.Rows.Add(param);

            param[0] = new Parameter("Cambio de contado a crédito", TypeCode.String);
            param[1] = new Parameter("20053343434", TypeCode.String);
            param[2] = new Parameter("SIUSPR", TypeCode.String);
            param[3] = new Parameter("22/09/2008 10:03", TypeCode.String);

            dtBitacora.Rows.Add(param);

            gvBitacora.DataSource = dtBitacora;
            gvBitacora.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

}