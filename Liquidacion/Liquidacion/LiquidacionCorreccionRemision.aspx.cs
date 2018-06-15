using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LiquidacionCorreccionRemision : System.Web.UI.Page
{
    private SigametLiquidacion.Pedido _pedido;
    private SigametLiquidacion.Cliente _cliente;
    SigametLiquidacion.Parametros _parametros;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        btnAceptar.Enabled = true;

        limpiaControles();

        try
        {
            if (txtPedido.Text.Trim().Length == 0)
            {
                DocumentosBSR.SerieDocumento.SeparaSerie(txtRemision.Text);
                _pedido = new SigametLiquidacion.Pedido(DocumentosBSR.SerieDocumento.Serie, DocumentosBSR.SerieDocumento.FolioNota);
            }
            else
            {
                _pedido = new SigametLiquidacion.Pedido(txtPedido.Text);
            }

            Session["PedidoCambioRemision"] = _pedido;

            txtPedido.Text = _pedido.PedidoReferencia;
            txtRemision.Text = _pedido.SerieRemision.Trim() + _pedido.FolioRemision.ToString().Trim();
            lblLitros.Text = _pedido.Litros.ToString();
            lblTotal.Text = _pedido.Importe.ToString("C");
            lblFormaPago.Text = (_pedido.FormaPago == 5) ? "CONTADO" : "CREDITO";
            lblFolio.Text = _pedido.AñoAtt + " - " + _pedido.FolioAtt;
            lblFSuministro.Text = _pedido.FechaSuministro.ToShortDateString();

            _cliente = new SigametLiquidacion.Cliente(_pedido.Cliente, 7, "ESPERA");
            _cliente.ConsultaDatosCliente();
            lblCliente.Text = _cliente.NumeroCliente.ToString();
            lblNombre.Text = _cliente.Nombre;
            lblDomicilio.Text = _cliente.Direccion;

            _parametros = new SigametLiquidacion.Parametros(1, 1, 22);

            if ((DateTime.Today.Date - _pedido.FechaSuministro.Date).Days > Convert.ToInt32(_parametros.ValorParametro("LimiteDiasModificacion")))
            {
                lblError.Text = "No puede modificar este pedido, ya concluyó el periodo permitido realizar cambios";
                btnAceptar.Enabled = false;
            }

            if (_pedido.Factura.Trim().Length > 0)
            {
                lblError.Text = "No puede modificar este pedido, ya fué facturado (Folio Factura: " + _pedido.Factura + ")";
                btnAceptar.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error:" + (char)13 + ex.Message;   
        }
    }

    protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DocumentosBSR.SerieDocumento.SeparaSerie(txtRemision.Text);
            _pedido = (SigametLiquidacion.Pedido)Session["PedidoCambioRemision"];

            _pedido.SerieRemision = DocumentosBSR.SerieDocumento.Serie;
            _pedido.FolioRemision = DocumentosBSR.SerieDocumento.FolioNota;


            //Validar que las remisiones nuevas no hayan sido usadas
            //Validar que las remisiones nuevas existan en la tabla nota

            SigametLiquidacion.ControlDeRemisiones _remisiones = new SigametLiquidacion.ControlDeRemisiones();

            if (!_remisiones.ValidarNota(_pedido.SerieRemision, _pedido.FolioRemision.ToString()))
            {
                lblError.Text = "El número de remisión que proporcionó no está registrado en el sistema.";
                return;
            }

            if (_remisiones.RemisionExistente(_pedido.Celula, _pedido.AñoPed, _pedido.NumeroPedido,
                    _pedido.SerieRemision, _pedido.FolioRemision.ToString()))
            {
                lblError.Text = "El número de remisión que proporcionó ya fué liquidado en otro suministro.";
                return;
            }

            _pedido.ActualizaRemision();
			
			lblError.Text = "Información actualizada correctamente";
        }
        catch (Exception ex)
        {
            lblError.Text = "Error:" + (char)13 + ex.Message;
        }
    }

    protected void limpiaControles()
    {
        lblLitros.Text = string.Empty;
        lblTotal.Text = string.Empty;
        lblFormaPago.Text = string.Empty;
        lblFolio.Text = string.Empty;

        lblCliente.Text = string.Empty;
        lblNombre.Text = string.Empty;
        lblDomicilio.Text = string.Empty;

        lblError.Text = string.Empty;
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtPedido.Text = string.Empty;
            txtRemision.Text = string.Empty;

            limpiaControles();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error:" + (char)13 + ex.Message;
        }
    }
}
