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

using System.Web.Services;

using SigametLiquidacion;
using System.Data.SqlClient;

public partial class Liquidacion : System.Web.UI.Page
{
    Catalogos _catalogos;
    Parametros _parametros;
    Precio _precios;

    static bool _conciliacion;
    bool _cuadrado = false;

    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Convert.ToBoolean(Session["Iniciada"]))
        {
            Response.Redirect("Login.aspx");
        }

        if (!Page.IsPostBack)
        {
            
                //RAMPACRRR
                if (Request.QueryString["FormaLiquidacion"] != null)
                {
                    AutoTanqueTurno1.FormaLiquidacion = Request.QueryString["FormaLiquidacion"].ToString();
                }

                _catalogos = new Catalogos();
                Session["Catalogos"] = _catalogos;

                //Carga los parámetros generales para la liquidacion 
                _parametros = new Parametros(1, 1, 22);
                Session["Parametros"] = _parametros;

                CargaPedidos(Convert.ToInt16(Session["AñoAtt"]), Convert.ToInt32(Session["Folio"]));

                nuevoPedido.Parametros = _parametros;

                nuevoPedido.ListaFormasPago = _catalogos.ListaFormaPago;

                nuevoPedido.ListaTipoPedido = _catalogos.ListaTipoPedido;
                nuevoPedido.ListaTiposCobro = _catalogos.ListaTipoCobro;
                nuevoPedido.ClaveCreditoAutorizado = Convert.ToByte(_parametros.ValorParametro("ClaveCreditoAutorizado"));

                nuevoPedido.SerieRemisionRuta = AutoTanqueTurno1.SerieRemision;

                nuevoPedido.ValidarRemisionExistente = AutoTanqueTurno1.ValidarRemisionExistente;
                nuevoPedido.ValidarRemisionCapturada = AutoTanqueTurno1.ValidarRemisionLiquidada;

                nuevoPedido.LongitudSerie = AutoTanqueTurno1.LongitudSerie;
                nuevoPedido.LongitudRemision = AutoTanqueTurno1.LongitudRemision;

                btnTerminar.Attributes.Add("onclick", "return confirm('¿Desea finalizar la captura de la liquidación?')");
            // btnPagos.Attributes.Add("onclick", "return confirm('¿Desea Continuar?')");

               nuevoPedido.ConsultaCteOnChange = true;



        }
        else
        {
            _catalogos = (Catalogos)Session["Catalogos"];
            _parametros = (Parametros)Session["Parametros"];
        }
        if (AutoTanqueTurno1.OperadorAsignado == true)
        {
            ConsultaResumenLiquidacion();
        }
        else
        {
            nuevoPedido.Enabled = false;
        }
        lblMensaje.Text = string.Empty;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (AutoTanqueTurno1.LitrosLiquidados != AutoTanqueTurno1.TotalLitros)
                _cuadrado = false;
            else
                _cuadrado = true;

            if (!_cuadrado)
            {
                btnPagos.Attributes.Add("onclick", "return confirm('La liquidacion tiene diferencia con el totalizador. ¿Desea Continuar?')");
            }
            else
            {
                btnPagos.Attributes.Remove("onclick");
            }
        }
    }

    #region Carga de la lista de pedidos
    private void CargaPedidos(short AñoAtt, int Folio)
    {
        string _usuario = Convert.ToString(Session["Usuario"]);
        
        AutoTanqueTurno1.AñoAtt = AñoAtt;
        AutoTanqueTurno1.Folio = Folio;
        AutoTanqueTurno1.CargaDatosFolio();

        if (!AutoTanqueTurno1.OperadorAsignado)
            return;
        
        AutoTanqueTurno1.Usuario = _usuario;

        nuevoPedido.AutoTanque = AutoTanqueTurno1.Autotanque;
        Session["Autotanque"] = AutoTanqueTurno1.Autotanque; // mcc 2018 09-09-2018
        nuevoPedido.Usuario = _usuario;

        //Carga de precios de acuerdo a los datos de pedido
        _precios = new Precio(AutoTanqueTurno1.ClaseRuta, AutoTanqueTurno1.Fecha, AutoTanqueTurno1.PreciosMultiples);

        if (_precios.ListaPrecios().Rows.Count > 0)
        {
            nuevoPedido.ListaPrecios = _precios.ListaPrecios();

            //TODO: Revisar como controlar para liquidacion ri en línea
            if (AutoTanqueTurno1.Status.Trim().ToUpper() == "CIERRE")
            {
                AutoTanqueTurno1.AltaInicioLiquidacionFolio();
            }

            AutoTanqueTurno1.CargarListaPedidos();

            if (!(AutoTanqueTurno1.LiquidacionIniciada(ref _usuario,
                Convert.ToInt16(Session["AñoAtt"]), Convert.ToInt32(Session["Folio"]))))
            {
                lblMensaje.Text = "Esta liquidación fué iniciada por " + _usuario;
                btnTerminar.Visible = false;
                nuevoPedido.PermitirCaptura = false;
            }
            else
            {
                btnTerminar.Visible = true;
                nuevoPedido.PermitirCaptura = true;
            }

            //TODO: Revisar como controlar para liquidacion ri en línea
            if (!(AutoTanqueTurno1.Status.Trim().ToUpper() == "CIERRE"))
            {
                nuevoPedido.PermitirCaptura = false;
                btnTerminar.Visible = false;
                btnPagos.Visible = false;
            }

            if ((AutoTanqueTurno1.Status.Trim().ToUpper() == "LIQCAJA" || AutoTanqueTurno1.Status.Trim().ToUpper() == "LIQUIDADO"))
            {
                imbReporte.Visible = true;
            }

            ParametrosLiquidacion _params = new ParametrosLiquidacion();

            _params.AñoAtt = AutoTanqueTurno1.AñoAtt;
            _params.Folio = AutoTanqueTurno1.Folio;
            _params.Celula = AutoTanqueTurno1.Celula;
            _params.Ruta = AutoTanqueTurno1.Ruta;
            _params.Fecha = AutoTanqueTurno1.Fecha;

            ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;

            nuevoPedido.ParametrosRuta = _params;
            nuevoPedido.FechaSuministro = _params.Fecha;
            //
        }
        else
        {
            lblMensaje.Text = "No es posible cargar la lista de Precios";
        }
    }
    #endregion

    #region Edición de pedidos
    protected void ListaPedidos1_EditarElemento(object sender, EventArgs e)
    {
        try
        {
            AutoTanqueTurno1.RecorridoListaPedidos(AutoTanqueTurno1.CurrentRow(ListaPedidos1.ClickedRow));

            lblControlPedido.Text = "Edición del pedido " +
                Convert.ToString(AutoTanqueTurno1.CurrentRow(ListaPedidos1.ClickedRow)["PedidoReferencia"]);

            nuevoPedido.ConsultaDetallePedido(TipoOperacionPedido.EdicionPedidoConciliado,
                AutoTanqueTurno1.CurrentRow(ListaPedidos1.ClickedRow));
            ListaPedidos1.Remark = true;
            ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;

            nuevoPedido.Focus();
        }
        catch (Exception ex)
        {
            lblMessageCenter.Text = ex.Message;
        }
    }

    protected void nuevoPedido1_Actualizar(object sender, EventArgs e)
    {
        //modularizar con listapedidos1_editarelemento
        lblControlPedido.Text = "Edición del pedido " +
            Convert.ToString(AutoTanqueTurno1.CurrentRow(nuevoPedido.SourceRow)["PedidoReferencia"]);
        nuevoPedido.ConsultaDetallePedido(TipoOperacionPedido.EdicionPedidoConciliado, AutoTanqueTurno1.CurrentRow(nuevoPedido.SourceRow));
        ConsultaResumenLiquidacion();
        nuevoPedido.Focus();
    }

    protected void nuevoPedido_ClickAceptar(object sender, EventArgs e)
    {
        try
        {
            //foreach (DataRow dr in AutoTanqueTurno1.ListaPedidos.Rows)
            //{
            //    if (dr["Cliente"].ToString() == nuevoPedido.Cliente.ToString())
            //    {
            //        Page.RegisterClientScriptBlock("Confirmacion", "confirm('Existen pedidos capturados para este cliente. ¿Desea Continuar?')");
            //    }
            //}

            if (nuevoPedido.TipoOperacion == TipoOperacionPedido.EdicionPedidoConciliado)
            {
                AutoTanqueTurno1.EdicionPedido(nuevoPedido.SourceRow, nuevoPedido.Cliente, nuevoPedido.Nombre,
                    nuevoPedido.PedidoReferencia, nuevoPedido.Litros, nuevoPedido.Precio, nuevoPedido.Importe,
                    nuevoPedido.FormaPago, nuevoPedido.FolioRemision, nuevoPedido.Descuento);
            }
            else if (nuevoPedido.TipoOperacion == TipoOperacionPedido.CapturaNuevoPedido)
            {
                AutoTanqueTurno1.AltaPedido(nuevoPedido.Cliente, nuevoPedido.CelulaPedido, nuevoPedido.AñoPedido, nuevoPedido.NumeroPedido,
                    nuevoPedido.Nombre, nuevoPedido.PedidoReferencia, nuevoPedido.Litros, nuevoPedido.Precio, nuevoPedido.Importe,
                    nuevoPedido.FormaPago, nuevoPedido.TipoPedido, "CONCILIADO", nuevoPedido.FolioRemision, nuevoPedido.Descuento);
            }
            else if (nuevoPedido.TipoOperacion == TipoOperacionPedido.EdicionNuevoPedido ||
                nuevoPedido.TipoOperacion == TipoOperacionPedido.EdicionPedidoInconsistente)
            {
                //22-06-2015
                //Error de asignación de pedido incorrecto cuando el cliente pertenece a otra célula, se cambia nuevoPedido.Celula por nuevoPedido.CelulaPedido
                AutoTanqueTurno1.EdicionNuevoPedido(nuevoPedido.SourceRow, nuevoPedido.Cliente, nuevoPedido.Nombre,
                    nuevoPedido.PedidoReferencia, nuevoPedido.CelulaPedido, nuevoPedido.AñoPedido, nuevoPedido.NumeroPedido,
                    nuevoPedido.Litros, nuevoPedido.Precio, nuevoPedido.Importe,
                    nuevoPedido.FormaPago, nuevoPedido.TipoPedido, "CONCILIADO", nuevoPedido.FolioRemision, nuevoPedido.Descuento);
            }

            int pedidoActual = nuevoPedido.SourceRow;

            ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;
            nuevoPedido.RestoreComponent();
            lblControlPedido.Text = string.Empty;

            if (!chkAutoRecorrido.Checked)
            {
                pedidoActual = 0;
            }

            if (!(ListaPedidos1.SiguientePedido(pedidoActual)))
            {
                nuevoPedido.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMessageCenter.Text = "ERROR: " + ex.Message;
            AutoTanqueTurno1.CargarListaPedidos();
            ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;
            nuevoPedido.RestoreComponent();
        }
    }

    protected void nuevoPedido_ClickCancelar(object sender, EventArgs e)
    {
        ListaPedidos1.Restablecer();
    }

    protected void nuevoPedido_DesasignarPedido(object sender, EventArgs e)
    {
        try
        {
            //Si es un pedido programado verifica que no haya notas blancas para el mismo cliente
            if ((nuevoPedido.TipoPedido.ToString() == "2") || (nuevoPedido.TipoPedido.ToString() == "1")) 
            {
                //Reviso en los pedidos capturados que no hay notas blancas para el mismo cliente que el del pedido a eliminar
                foreach (DataRow dr in AutoTanqueTurno1.ListaPedidos.Rows)
                {
                    if (dr["TipoPedido"].ToString() == "3" && dr["Cliente"].ToString() == nuevoPedido.Cliente.ToString())
                    {
                        
                        lblMensaje.Text = "El cliente tiene pedidos con notas blancas, desasigne estos primero.";
                        return;
                    }
                }
            }

            nuevoPedido.DesasignaPedido(AutoTanqueTurno1.CurrentRow(nuevoPedido.SourceRow));
            AutoTanqueTurno1.DesasignacionPedido(nuevoPedido.SourceRow);

        }

        catch (SqlException sqlEx)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "La cobranza del pedido ha sido programada y no puede ser eliminado. Verifique";

        }

        catch (Exception ex)
        {
            lblMessageCenter.Text = "ERROR: " + ex.Message;
            //lblMensaje.Text = ex.Message;
            //AutoTanqueTurno1.CargarListaPedidos();
        }

        ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;
        nuevoPedido.RestoreComponent();
        ConsultaResumenLiquidacion();
    }

    protected void nuevoPedido_CambiarCliente(object sender, EventArgs e)
    {
        try
        {
            nuevoPedido.DesasignaPedido(AutoTanqueTurno1.CurrentRow(nuevoPedido.SourceRow));
            AutoTanqueTurno1.DesasignacionPedido(nuevoPedido.SourceRow);
            ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;
            ConsultaResumenLiquidacion();
        }
        catch (Exception ex)
        {
            lblMessageCenter.Text = ex.Message;
        }
    }
    #endregion

    #region Control de la lista de pedidos
    protected void ListaPedidos1_CargaDatos(object sender, EventArgs e)
    {
        lblMessageCenter.Text = string.Empty;

        ConsultaResumenLiquidacion();

        _conciliacion = AutoTanqueTurno1.ConciliacionCompleta;

        if (AutoTanqueTurno1.LitrosLiquidados != AutoTanqueTurno1.TotalLitros)
        {
            lblMessageCenter.Text = "Diferencia contra totalizador: " + (AutoTanqueTurno1.LitrosLiquidados - AutoTanqueTurno1.TotalLitros).ToString();
        }

        if (_conciliacion && Convert.ToBoolean(Convert.ToByte(_parametros.ValorParametro("CapturaRemision"))))
        {
            _conciliacion = AutoTanqueTurno1.CapturaRemisionCompleta;
        }
    }
 
    private void ConsultaResumenLiquidacion()
    {
        DataTable resumenPedidos = AutoTanqueTurno1.SuministrosPorFormaDePago("CONTADO");
        if (resumenPedidos.Rows.Count > 0)
        {
            ResumenLiquidacion1.LitrosContado = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", ""));
            ResumenLiquidacion1.ImporteContado = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", ""));
        }
        else
        {
            lblMessageCenter.Text = "Cierre de liquidación";
        }

        resumenPedidos = AutoTanqueTurno1.SuministrosPorFormaDePago("CREDITO");
        if (resumenPedidos.Rows.Count > 0)
        {
            ResumenLiquidacion1.LitrosCredito = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", ""));
            ResumenLiquidacion1.ImporteCredito = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", ""));
        }

        resumenPedidos = AutoTanqueTurno1.SuministrosPorFormaDePago("OTROS");
        if (resumenPedidos.Rows.Count > 0)
        {
            ResumenLiquidacion1.LitrosOtros = Convert.ToDouble(resumenPedidos.Compute("SUM(Litros)", ""));
            ResumenLiquidacion1.ImporteOtros = Convert.ToDecimal(resumenPedidos.Compute("SUM(Importe)", ""));
        }

        if (AutoTanqueTurno1.ListaPedidos.Rows.Count > 0)
        {
            ResumenLiquidacion1.LitrosTotal = Convert.ToDouble(AutoTanqueTurno1.ListaPedidos.Compute("SUM(Litros)", ""));
            ResumenLiquidacion1.ImporteTotal = Convert.ToDecimal(AutoTanqueTurno1.ListaPedidos.Compute("SUM(Importe)", ""));
        }

        if (AutoTanqueTurno1.PedidosFiltrados("CONCILIADO").Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosConciliados = AutoTanqueTurno1.PedidosFiltrados("CONCILIADO").Rows.Count;
        }

        if (AutoTanqueTurno1.PedidosFiltrados("ERROR").Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosInconsistentes = AutoTanqueTurno1.PedidosFiltrados("ERROR").Rows.Count;
        }

        if (AutoTanqueTurno1.PedidosFiltrados("PENDIENTE").Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosPendientes = AutoTanqueTurno1.PedidosFiltrados("PENDIENTE").Rows.Count;
        }

        if (AutoTanqueTurno1.SuministrosPorTipoPedido(1).Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosTelefonicos = AutoTanqueTurno1.SuministrosPorTipoPedido(1).Rows.Count;
        }

        if (AutoTanqueTurno1.SuministrosPorTipoPedido(2).Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosProgramados = AutoTanqueTurno1.SuministrosPorTipoPedido(2).Rows.Count;
        }

        if (AutoTanqueTurno1.SuministrosPorTipoPedido(3).Rows.Count > 0)
        {
            ResumenLiquidacion1.PedidosNotaBlanca = AutoTanqueTurno1.SuministrosPorTipoPedido(3).Rows.Count;
        }
    }

    protected void ReordenarLista(string SortBy)
    {
        AutoTanqueTurno1.ReordenarListaPedidos(SortBy);
        ListaPedidos1.DataSource = AutoTanqueTurno1.ListaPedidos;
        nuevoPedido.RestoreComponent();
        lblControlPedido.Text = string.Empty;
    }

    protected void ListaPedidos1_SortList(object sender, CommandEventArgs e)
    {
        ReordenarLista(e.CommandArgument.ToString());
    }

    protected void ListaPedidos1_Error(object sender, EventArgs e)
    {
        try
        {
            CargaPedidos(Convert.ToInt16(Session["AñoAtt"]), Convert.ToInt32(Session["Folio"]));
        }
        catch (Exception ex)
        {
            lblMessageCenter.Text = ex.Message;
        }
    }
    #endregion

    protected void ListaPedidos1_Filtrar(object sender, CommandEventArgs e)
    {
        txtBuscar.Text = string.Empty;
        txtBuscar.Focus();
        lblFiltro.Text = "Filtro por " + e.CommandName;
        lstFilter.DataSource = AutoTanqueTurno1.Filter(e.CommandArgument.ToString());
        lstFilter.DataBind();
        btnCustomFilter.CommandName = e.CommandName;
        updPnlFiltros.Update();
        modalFilter.Show();
    }

    protected void btnCustomFilter_Command(object sender, CommandEventArgs e)
    {
        modalFilter.Hide();
        ReordenarLista(e.CommandName);

        if (!(ListaPedidos1.SiguientePedido(AutoTanqueTurno1.ApplyCustomFilter(e.CommandName, txtBuscar.Text, !chkFullMatch.Checked) - 1)))
        {
            nuevoPedido.Focus();
        }
    }

    protected void btnFiltrar_Command(object sender, CommandEventArgs e)
    {
        modalFilter.Hide();
        ReordenarLista(e.CommandName);
                        
        if (!(ListaPedidos1.SiguientePedido(AutoTanqueTurno1.ApplyFilter(e.CommandName, e.CommandArgument.ToString()) - 1)))
        {
            nuevoPedido.Focus();
        }
    }

    #region Captura de pagos y cierre de la liquidación
    protected void btnPagos_Click(object sender, ImageClickEventArgs e)
    {
        //if (AutoTanqueTurno1.LitrosLiquidados != AutoTanqueTurno1.TotalLitros)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Diferencia", "<script language=javascript> confirm('Hay una diferencia de litraje. ¿Desea continuar?'); </script>");
        //    //Page.RegisterStartupScript("Diferencia de Litraje", "<script language=javascript> alert('Hay una diferencia de litraje. ¿Desea continuar?'); </script>");
        //}
        //if (AutoTanqueTurno1.LitrosLiquidados != AutoTanqueTurno1.TotalLitros)
        //    _cuadrado = false;
        //else
        //    _cuadrado = true;


        //if (!_cuadrado)
        //{
        //    btnPagos.Attributes.Add("onclick", "return confirm('La liquidacion tiene diferencia con el totalizador. ¿Desea Continuar?')");
        //}

        if (_conciliacion)
        {
            if (AutoTanqueTurno1.ListaPedidos.Rows.Count > 0)
            {
                if (!(AutoTanqueTurno1.SuministrosPorFormaDePago("CONTADO").Rows.Count > 0))
                {
                    DatosRegistroPago _datos = new DatosRegistroPago();
                    _datos.ActualizaTerminado(AutoTanqueTurno1.ResumenLiquidacionFinal(Convert.ToString(Session["Usuario"])));
                    btnPagos.Visible = false;
                    imbReporte_Click(sender, e);
                    return;
                }

                Session["dtPedidos"] = AutoTanqueTurno1.PedidosContado;
                Session["dtResumenLiquidacion"] = AutoTanqueTurno1.ResumenLiquidacionFinal(Convert.ToString(Session["Usuario"]));
                Response.Redirect("FormaPago.aspx");
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No puede Finalizar la Liquidación sin Pagos capturados. Verifique!";
            }
        }
		else
        {
            if (Convert.ToBoolean(Convert.ToByte(_parametros.ValorParametro("CapturaRemision"))))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No ha capturado todos los números de remisión. Verifique!";
            }
        }
    }   

    protected void btnTerminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (AutoTanqueTurno1.LitrosLiquidados != AutoTanqueTurno1.TotalLitros)
            //    {
            //        Page.RegisterClientScriptBlock("Diferencia de Litraje", "<script language=javascript> alert('Hay una diferencia de litraje. ¿Desea continuar?'); </script>");
            //    }
          
      

            if (AutoTanqueTurno1.ListaPedidos.Rows.Count > 0)
            {
                    AutoTanqueTurno1.FinalizarCapturaLiquidacion();
                    Response.Redirect("SeleccionRutaLiquidacionDina.aspx");
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No puede Finalizar la Liquidación sin Pagos capturados. Verifique!";
            }
           
        }
        catch (Exception ex)
        {
            lblMessageCenter.Text = ex.Message;
        }
    }

    //Impresión del reporte de liquidación
    protected void imbReporte_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("ReporteLiquidacion.aspx");
        }
        catch (Exception ex)
        {
            //lblError.Text = ex.Message;
        }
    }
    #endregion
    
    [WebMethod]
    public static bool ValidarConciliacionDeRemisiones()
    {
        return _conciliacion;
    }
}
