using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class ControlesUsuario_wucConsultaCargoTarjetaClienta : System.Web.UI.UserControl
{
    public DataTable dtPagosContarjeta = null;
    protected DataSet ds = null;
    public string sFormaPago = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (dtPagosContarjeta!=null)
        {
            if (dtPagosContarjeta.Rows.Count >0)
            
             if ((Session["dsLiquidacion"])!=null)
                {
                    ds = (DataSet)(Session["dsLiquidacion"]);
                }

            GrdPagosConTarjeta.DataSource = dtPagosContarjeta;
            GrdPagosConTarjeta.DataBind();
            GrdPagosConTarjeta.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        }

        if (Page.IsPostBack == true)
        {
            
        }



    }

    private void AsignaLlaves()
    {
        foreach (GridViewRow row in GrdPagosConTarjeta.Rows)
        {
            if (row.RowIndex > -1)
                row.Attributes.Add("onclick", "return ConsultaPagosTPV("+ row.Cells[7].ToString()+")"); 
                row.ToolTip = "Seleccione un Registro!.";

        }

    }
    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
       

        if (e.Row.RowIndex > -1)
        {            
            e.Row.Attributes.Add("onclick", "return ConsultaPagosSeleccion('"+ sFormaPago.ToString()+"','" + e.Row.RowIndex.ToString()+ "')");
            e.Row.ToolTip = "Click en el registro.";
            e.Row.Attributes["style"] = "cursor:pointer";

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E4EBAB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            e.Row.BackColor = Color.FromName("#FAFAFA");

        }
    }


    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdPagosConTarjeta.Rows)
        {
            if (row.RowIndex == GrdPagosConTarjeta.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click para selecionar un registro.";
            }
        }
    }
}