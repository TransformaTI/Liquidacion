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
using System.IO;
using SigametLiquidacion;

public partial class GenerarPago : System.Web.UI.Page
{

    DataTable dtPagosGenerados = new DataTable();
    DataTable dtDetallePago = new DataTable();
    DataTable dtResumenLiquidacion = new DataTable();
    DataSet dsPagos = new DataSet();
    RegistroPago rp = new RegistroPago();


    protected void Page_Load(object sender, EventArgs e)
    {
        #region eventos
        //imbGuardar.Attributes.Add("onclick", "return confirm('¿Desea guardar y Cerrar la liquidacion?')");
        //imbGuardar.Attributes.Add("onclick", "return ValidaSaldo();");
        #endregion

        dtResumenLiquidacion = (DataTable)(Session["dtResumenLiquidacion"]);

        dsPagos = (DataSet)(Session["dsLiquidacion"]);

        //dtPagosGenerados = (DataTable)(Session["TablaCobro"]);

        if (dsPagos != null && dsPagos.Tables["Cobro"] != null) {

            DataRow[] drp = dsPagos.Tables["Cobro"].Select("TipoCobro = 'Tarjeta de Débito' or TipoCobro = 'Tarjeta de Crédito' or TipoCobro = 'Tarjeta de Servicio'");
            //DataTable DtTarjeta = drp.CopyToDataTable();
            //if (DtTarjeta.Rows.Count >0)
            //{
            //    gvPagoGenerado.Columns[8].Visible = true;
            //}
            //else
            //{
            //    gvPagoGenerado.Columns[8].Visible = false;
            //}


            DataTable cobrosTemp = dsPagos.Tables["Cobro"].Copy();

            DataRow[] cobrosTempRows = cobrosTemp.Select();

            int tipoPago;
            foreach (DataRow fila in cobrosTemp.Rows)
            {
                tipoPago = Convert.ToInt32(fila["TipoCobro"]);

                switch(tipoPago)
                {
                    case (Int16)(RegistroPago.TipoPago.tipoCheque):
                        fila["NumCheque"] = fila["NumeroCuenta"];
                        break;
                    case (Int16)(RegistroPago.TipoPago.transferencia):
                        fila["NumCheque"] = fila["NumeroCuenta"];
                        break;
                }
            }            

            gvPagoGenerado.DataSource = cobrosTemp;
            gvPagoGenerado.DataBind();

            //dtDetallePago = (DataTable)(Session["dtPagos"]);
            //Va a Mostrar por Default el Detalle del Pago Activo
            string pagoSeleccionado = Session["idCobroConsec"].ToString();
            //DataView vistaPagoActivo = new DataView(dtDetallePago);
            DataView vistaPagoActivo = new DataView(dsPagos.Tables["CobroPedido"]);
            vistaPagoActivo.RowFilter = "IdPago = " + pagoSeleccionado;


            dlDetallePago.DataSource = vistaPagoActivo;
            dlDetallePago.DataBind();

            DataTable Cobro = dsPagos.Tables["Cobro"];
            if (Cobro != null)
            {
                HiddenSaldo.Value = Cobro.Compute("Sum(Saldo)", "").ToString();
            }
        }
        else {
            Response.Write("<script>alert('No hay pagos a mostrar')</script>");

        } 
       
        



    }
    #region "Handlers"
    protected void gvPagoGenerado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string pagoSeleccionado = gvPagoGenerado.SelectedRow.Cells[1].Text;
            //DataView vistaPagoActivo = new DataView(dtDetallePago);
            DataView vistaPagoActivo = new DataView(dsPagos.Tables["CobroPedido"]);
            vistaPagoActivo.RowFilter = "IdPago = " + pagoSeleccionado;
            
            dlDetallePago.DataSource = vistaPagoActivo;
            dlDetallePago.DataBind();

        }
        catch(Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void lnkNuevoPago_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormaPago.aspx");
    }
    protected void lnkGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            rp.GuardaPagos(Convert.ToString(Session["Usuario"]), dsPagos.Tables["Pedidos"], dsPagos.Tables["Cobro"], dsPagos.Tables["CobroPedido"], dtResumenLiquidacion);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
    {    

        try
        {

           DataTable dtPedidosEf = ((DataTable)(Session["dtPedidos"]));

            DataTable dtPedidosParientes = (DataTable)(Session["PedidosParientes"]);

            if (dtPedidosParientes != null)
            {
                foreach (DataRow item in dtPedidosEf.Rows)
                {
                    foreach (DataRow row in dtPedidosParientes.Rows)
                    {
                        if (item["Pedido"].ToString().Trim() == row["Pedido"].ToString().Trim())
                        {
                            item.BeginEdit();
                            item["Saldo"] = row["Saldo"];
                            item.EndEdit();
                        }

                    }
                }
            }


            rp.GuardaPagos(Convert.ToString(Session["Usuario"]), dtPedidosEf, dsPagos.Tables["Cobro"], dsPagos.Tables["CobroPedido"], dtResumenLiquidacion, dsPagos.Tables["LiqPagoAnticipado"]);
            Session["dsLiquidacion"] = null; // MCC  se limpia la session de liquidacion despues de registrar el pago  2018 05 31
            Session["CargoTarjeta"] = null;
            Session["TDCdisponibles"] = null;
            Session["PrimerRegTDC"] = null;
            Session["PedidosParientes"] = null;
            Response.Redirect("ReporteLiquidacion.aspx");
            

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbNuevoPago_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FormaPago.aspx");
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["dtPagos"] = null;
            Session["TablaCobro"] = null;
            Session["PedidosOrden"] = null;
            Response.Redirect("FormaPago.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbCancelarPagos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dsPagos.Tables["Pedidos"].Clear();
            dsPagos.Tables["Pedidos"].Merge((DataTable)(Session["dtPedidos"]));
            dsPagos.Tables["CobroPedido"].Clear();
            Session["CargoTarjeta"] = null;
            Response.Redirect("Liquidacion.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    #endregion
}
