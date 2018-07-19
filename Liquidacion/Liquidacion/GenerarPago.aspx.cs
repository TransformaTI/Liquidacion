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
        imbGuardar.Attributes.Add("onclick", "return ValidaSaldo();");
        #endregion

        dtResumenLiquidacion = (DataTable)(Session["dtResumenLiquidacion"]);

        dsPagos = (DataSet)(Session["dsLiquidacion"]);

        //dtPagosGenerados = (DataTable)(Session["TablaCobro"]);
        gvPagoGenerado.DataSource = dsPagos.Tables["Cobro"];
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
        if (Cobro!=null)
        {
            HiddenSaldo.Value = Cobro.Compute("Sum(Saldo)","").ToString();
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


           rp.GuardaPagos(Convert.ToString(Session["Usuario"]), dsPagos.Tables["Pedidos"], dsPagos.Tables["Cobro"], dsPagos.Tables["CobroPedido"], dtResumenLiquidacion, dsPagos.Tables["LiqPagoAnticipado"]);
            Session["dsLiquidacion"] = null; // MCC  se limpia la session de liquidacion despues de registrar el pago  2018 05 31
            Session["CargoTarjeta"] = null;
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
