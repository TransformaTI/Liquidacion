using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Reflection;

public partial class ControlesUsuario_wucConsultaCargoTarjetaClienta : System.Web.UI.UserControl
{
    public DataTable dtPagosContarjeta = null;
    protected DataSet ds = null;
    public string sFormaPago = "";
    protected DataTable dtPagosContarjetaDel=new DataTable("dtPagosContarjetaDel") ;
    protected void Page_Load(object sender, EventArgs e)
    {



      

        if (dtPagosContarjeta!=null)
        {
           // dtPagosContarjetaDel = dtPagosContarjeta;
            if (dtPagosContarjeta.Rows.Count >0)
            {
                if ((Session["dsLiquidacion"])!=null )
                {
                    ds = (DataSet)(Session["dsLiquidacion"]);
                    if (ds.Tables["Cobro"] != null && dtPagosContarjeta != null && ds.Tables["Cobro"].Columns.Count > 0)
                    {
                        var PagosConTarjeta = dtPagosContarjeta.AsEnumerable();
                        var Cobros = ds.Tables["Cobro"].AsEnumerable();

                        var Registros = (
                                                    from c in PagosConTarjeta
                                                    join b in Cobros
                                                        on
                                                              c.Field<string>("Autorizacion") equals b.Field<string>("referencia")

                                                    into j
                                                    from x in j.DefaultIfEmpty()
                                                    where x == null
                                                    select c
                                                );

                        if (Registros.ToList().Count > 0)
                        {

                            dtPagosContarjetaDel =  (
                                                    from c in PagosConTarjeta
                                                    join b in Cobros
                                                        on 
                                                              c.Field<string>("Autorizacion") equals b.Field<string>("referencia")
  
                                                    into j
                                                    from x in j.DefaultIfEmpty()
                                                    where x == null
                                                    select c
                                                ).CopyToDataTable();

                            Session["TDCdisponibles"] = dtPagosContarjetaDel.Rows.Count;
                            Session["PrimerRegTDC"] = dtPagosContarjetaDel.Rows[0]["Folio"].ToString();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Hidepopup", " HideModalPopup();", true);
                           
                            //dtPagosContarjetaDel = dtPagosContarjeta;

                        }



                    }
                    else
                    {
                        dtPagosContarjetaDel = dtPagosContarjeta;
                        Session["TDCdisponibles"] = dtPagosContarjetaDel.Rows.Count;
                        Session["PrimerRegTDC"] = dtPagosContarjetaDel.Rows[0]["Folio"].ToString();
                    }


                }
                else
                    {
                    dtPagosContarjetaDel = dtPagosContarjeta;
                    Session["TDCdisponibles"] = dtPagosContarjetaDel.Rows.Count;
                    Session["PrimerRegTDC"] = dtPagosContarjetaDel.Rows[0]["Folio"].ToString();
                }
            }
    }



        GrdPagosConTarjeta.DataSource = dtPagosContarjetaDel;
        GrdPagosConTarjeta.DataBind();
        GrdPagosConTarjeta.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
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
            //e.Row.Attributes.Add("onclick", "return ConsultaPagosSeleccion('"+ sFormaPago.ToString()+"','" + e.Row.RowIndex.ToString()+ "')");
            e.Row.Attributes.Add("onclick", "return ConsultaPagosSeleccion('" + sFormaPago.ToString() + "','" + e.Row.Cells[7].Text.Trim() + "')");
            
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