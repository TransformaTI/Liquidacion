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

public partial class Cancelacion : System.Web.UI.Page
{
    SigametLiquidacion.FolioLiquidacion _folio;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        txtAñoAtt.Text = DateTime.Now.Year.ToString();
        }
    }
    
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        short _añoAtt = 0;
        int _numeroFolio = 0;

        if (txtAñoAtt.Text.Trim().Length == 0 || txtFolio.Text.Trim().Length == 0)
        {
            return;
        }

        try
        {
            _añoAtt = Convert.ToInt16(txtAñoAtt.Text);
            _numeroFolio = Convert.ToInt32(txtFolio.Text);

            _folio = new SigametLiquidacion.FolioLiquidacion(_añoAtt, _numeroFolio);
            _folio.ConsultaPedidos();
            _folio.ConfigurarLista();

            this.ViewState["Folio"] = _folio;

            lblStatus.Text += _folio.Status;
            ConsultaResumenLiquidacion();

            switch (_folio.Status.Trim().ToUpper())
            {
                case "LIQUIDADO":
                    btnCancelarPagos.Enabled = true;
                    btnCancelarPagos.Visible = true;
                    break;
                case "CIERRE":
                    if (_folio.ListaPedidos.Rows.Count > 0)
                    {
                        btnCancelarPedidos.Enabled = true;
                        btnCancelarPedidos.Visible = true;
                        lblStatus.Text = "STATUS: Liquidación iniciada";
                    }
                    break;
                default:
                    lblMensaje.Text = "Con este status no se puede cancelar la liquidación";
                    break;
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtAñoAtt.Text = string.Empty;
        txtFolio.Text = string.Empty;

        lblLitrosContado.Text = string.Empty;
        lblImporteContado.Text = string.Empty;
        lblLitrosCredito.Text = string.Empty;
        lblImporteCredito.Text = string.Empty;
        lblLitrosTotal.Text = string.Empty;
        lblImporteTotal.Text = string.Empty;

        lblMensaje.Text = string.Empty;

        grdCobros.DataSource = null;
        grdCobros.DataBind();

        btnCancelarPagos.Visible = false;
        btnCancelarPagos.Enabled = false;
        btnCancelarPedidos.Visible = false;
        btnCancelarPedidos.Enabled = false;
    }
    
    private void ConsultaResumenLiquidacion()
    {
        DataTable resumenPedidos = _folio.SuministrosPorFormaPago("CONTADO");
        if (resumenPedidos.Rows.Count > 0)
        {
            lblLitrosContado.Text = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", "")).ToString();
            lblImporteContado.Text = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", "")).ToString();
        }

        resumenPedidos = _folio.SuministrosPorFormaPago("CREDITO");
        if (resumenPedidos.Rows.Count > 0)
        {
            lblLitrosCredito.Text = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", "")).ToString();
            lblImporteCredito.Text = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", "")).ToString();
        }

        if (_folio.ListaPedidos.Rows.Count > 0)
        {
            lblLitrosTotal.Text = Convert.ToDouble(_folio.ListaPedidos.Compute("SUM(Litros)", "")).ToString();
            lblImporteTotal.Text = Convert.ToDecimal(_folio.ListaPedidos.Compute("SUM(Importe)", "")).ToString();
        }

        grdCobros.DataSource = _folio.ConsultaTotalCobros(_folio.AñoAtt, _folio.Folio);
        grdCobros.DataBind();
        
        //resumenPedidos = AutoTanqueTurno1.SuministrosPorFormaDePago("OTROS");
        //if (resumenPedidos.Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.LitrosOtros = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", ""));
        //    ResumenLiquidacion1.ImporteOtros = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", ""));
        //}

        //if (AutoTanqueTurno1.PedidosFiltrados("CONCILIADO").Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosConciliados = AutoTanqueTurno1.PedidosFiltrados("CONCILIADO").Rows.Count;
        //}

        //if (AutoTanqueTurno1.PedidosFiltrados("ERROR").Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosInconsistentes = AutoTanqueTurno1.PedidosFiltrados("ERROR").Rows.Count;
        //}

        //if (AutoTanqueTurno1.PedidosFiltrados("PENDIENTE").Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosPendientes = AutoTanqueTurno1.PedidosFiltrados("PENDIENTE").Rows.Count;
        //}

        //if (AutoTanqueTurno1.SuministrosPorTipoPedido(1).Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosTelefonicos = AutoTanqueTurno1.SuministrosPorTipoPedido(1).Rows.Count;
        //}

        //if (AutoTanqueTurno1.SuministrosPorTipoPedido(2).Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosProgramados = AutoTanqueTurno1.SuministrosPorTipoPedido(2).Rows.Count;
        //}

        //if (AutoTanqueTurno1.SuministrosPorTipoPedido(3).Rows.Count > 0)
        //{
        //    ResumenLiquidacion1.PedidosNotaBlanca = AutoTanqueTurno1.SuministrosPorTipoPedido(3).Rows.Count;
        //}
    }

    protected void btnCancelarPagos_Click(object sender, EventArgs e)
    {
        _folio = (SigametLiquidacion.FolioLiquidacion)ViewState["Folio"];
        try
        {
            _folio.CancelarCobros(_folio.AñoAtt, _folio.Folio);

            btnCancelarPagos.Visible = false;
            btnCancelarPedidos.Visible = true;
            lblMensaje.Text = "Abonos cancelados correctamente";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnCancelarPedidos_Click(object sender, EventArgs e)
    {
        _folio = (SigametLiquidacion.FolioLiquidacion)ViewState["Folio"];

        try
        {
            _folio = new SigametLiquidacion.FolioLiquidacion(Convert.ToInt16(txtAñoAtt.Text), Convert.ToInt32(txtFolio.Text));
            _folio.ConsultaPedidos();
            _folio.ConfigurarLista();

            _folio.CancelarPedidos(_folio.AñoAtt, _folio.Folio);
            btnCancelarPedidos.Visible = false;
            lblMensaje.Text = "Pedidos cancelados correctamente";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}
