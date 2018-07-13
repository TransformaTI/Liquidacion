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
using System.Collections.Generic;
using System.Web.Script.Serialization;

public partial class FormaPago : System.Web.UI.Page
{
    #region variables
    DataTable dtPedidos = new DataTable();
    RegistroPago rp = new RegistroPago();
    DataTable dtCobro = new DataTable();
    Decimal importeOperacion;
    DataSet ds = new DataSet();
    DataTable dtPagos;
    DataTable dtCobros;
    DataTable dtMovimientos;
    DataTable dtPagosConTarjeta; // mcc 2018 05 10
    DataRow[] dtPagosConTarjetaSelec;
    string[] clave;
    DataTable dtAfiliaciones;
    DataTable dtProveedores;
    DataTable dtTipoVale;




    string pagoActivo;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LlenaDropDowns();
        }

        #region validaciones postback 
        if (Page.IsPostBack)
        {
            if (Request.Form["__EVENTTARGET"] == "ConsultaTPV")
            {


                HiddenInput.Value = "ConsultaTPV";
                if (txtClienteTarjeta.Text != string.Empty)
                {
                    LimpiarCampos("tarjeta");
                    ConsultarCargoTarjeta(int.Parse(txtClienteTarjeta.Text), "tarjeta", int.Parse(Session["Ruta"].ToString()), int.Parse(Session["Autotanque"].ToString()));
                    txtNoAutorizacionTarjeta.ReadOnly = false;
                    txtFechaTarjeta.ReadOnly = false;
                    txtNumTarjeta.ReadOnly = false;
                    txtImporteTarjeta.ReadOnly = false;
                    ddBancoTarjeta.Enabled = true;
                    ddlBancoOrigen.Enabled = true;
                    ddTipTarjeta.Enabled = true;
                    chkLocal.Enabled = true;
                    txtObservacionesTarjeta.ReadOnly = false;
                    imgCalendario0.Enabled = true;
                }

        
            }

            if (Request.Form["__EVENTTARGET"] == "ConsultaTPV-Trans")
            {
                HiddenInput.Value = "ConsultaTPV-Trans";
                if (TxtCteAfiliacion.Text != string.Empty)
                {
                    LimpiarCampos("transferencia");
                    ConsultarCargoTarjeta(int.Parse(TxtCteAfiliacion.Text), "transferencia", int.Parse(Session["Ruta"].ToString()), int.Parse(Session["Autotanque"].ToString()));
                }
            }

            else if (Request.Form["__EVENTTARGET"].ToString().Contains("SeleccionaPago"))
            {
                LimpiarCampos("tarjeta");
                LimpiarCampos("transferencia");
                if (hfCargoTarjetaEncontrado.Value == "false")
                {
                    LimpiarCampos("tarjeta");
                }
                else
                {
                    MuestraPagoSeleccionado(Request.Form["__EVENTTARGET"].ToString());
                }
            }

            if (Request.Form["__EVENTTARGET"] == "ConsultaCteAnticipo")
            {
                HiddenInput.Value = "ConsultaCteAnticipo";
            }


            if (Request.Form["__EVENTTARGET"] == "ConsultaCteTransferencia")
            {
                HiddenInput.Value = "ConsultaCteTransferencia";
            }
            
        }
        else
        {
            titNoAut.Visible = false;
            titNoAutNum.Visible = false;
            HiddenInput.Value = "";
            HiddenInputPCT.Value = "";
            LimpiarCampos("tarjeta");
            LimpiarCampos("transferencia");
        }
        #endregion


        #region "Propiedades Controles"
        imbAceptar.Attributes.Add("onclick", "return confirmar(" + (char)39 + imbAceptar.UniqueID + (char)39 + ")");
        imbAceptarTDC.Attributes.Add("onclick", "return confirmar(" + (char)39 + imbAceptarTDC.UniqueID + (char)39 + ")");
        imbAceptarVale.Attributes.Add("onclick", "return confirmar(" + (char)39 + imbAceptarVale.UniqueID + (char)39 + ")");
        //imbAceptarVale.Attributes.Add("onclick", "return confirmar(" + (char)39 + 'imbAceptarVale + (char)39 + ")");



        txtLectorCheque.Attributes.Add("onkeyup", "return txtCuentaDocumento();");
        // txtClienteCheque.Attributes.Add("onblur", "ObtenerCliente(" + (char)39 + txtClienteCheque.UniqueID + (char)39 + "," + (char)39 + txtNombreClienteCheque.UniqueID + (char)39 + ")");
        txtClienteCheque.Attributes.Add("onblur", "ConsultaClienteCheque(" + (char)39 + "cheque" + (char)39 + ")");
        // txtClienteTarjeta.Attributes.Add("onblur", "ObtenerCliente(" + (char)39 + txtClienteTarjeta.UniqueID + (char)39 + "," + (char)39 + txtNombreClienteTarjeta.UniqueID + (char)39 + ")");
        //txtClienteVale.Attributes.Add("onblur", "ObtenerCliente(" + (char)39 + txtClienteVale.UniqueID + (char)39 + "," + (char)39 + txtValeNombre.UniqueID + (char)39 + ")");

        txtClienteVale.Attributes.Add("onblur", "ConsultaClienteCheque(" + (char)39 + "vale" + (char)39 + ")");

        imgCheque.Attributes.Add("onclick", "toggle('display',  'cheque', 'tarjeta', 'vale','Transferencia','Anticipo', " + (char)39 + txtLectorCheque.UniqueID + (char)39 + ")");
        imgTarjeta.Attributes.Add("onclick", "toggle('display', 'tarjeta', 'cheque', 'vale','Transferencia','Anticipo', " + (char)39 + txtClienteTarjeta.UniqueID + (char)39 + ")");
        imgVale.Attributes.Add("onclick", "toggle('display',    'vale', 'cheque', 'tarjeta','Transferencia','Anticipo', " + (char)39 + txtClienteVale.UniqueID + (char)39 + ")");
        //imgTransferencia.Attributes.Add("onclick", "toggle('display','Transferencia', 'vale','cheque', 'tarjeta','Anticipo', " + (char)39 + this.ucTransferencia.TxtIdCliente.UniqueID + (char)39 + ")");
        //ImgAnticipo.Attributes.Add("onclick", "toggle('display','Anticipo', 'cheque','vale', 'tarjeta','Transferencia', " + (char)39 + this.wucDetalleFormaPago.TxtAntIdCliente.UniqueID + (char)39 + ")");

        txtLectorCheque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtClienteCheque.UniqueID + (char)39 + ")");
        txtClienteCheque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtClienteCheque.UniqueID + (char)39 + ")");
        imgCalendario.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtFechaChueque.UniqueID + (char)39 + ")");
        txtFechaChueque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtNumCuenta.UniqueID + (char)39 + ")");
        txtNumCuenta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtNumeroCheque.UniqueID + (char)39 + ")");
        txtNumeroCheque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + ddChequeBanco.UniqueID + (char)39 + ")");

        ddChequeBanco.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtImporteCheque.UniqueID + (char)39 + ")");
        txtImporteCheque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtObservacionesChueque.UniqueID + (char)39 + ")");
        txtObservacionesChueque.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + imbAceptar.UniqueID + (char)39 + ")");

        txtClienteTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + imgCalendario0.UniqueID + (char)39 + ")");

        imgCalendario0.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtFechaTarjeta.UniqueID + (char)39 + ")");

        txtFechaTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtNoAutorizacionTarjeta.UniqueID + (char)39 + ")");
       // txtNoAutorizacionTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtNumTarjeta.UniqueID + (char)39 + ")");
        txtNumTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + ddBancoTarjeta.UniqueID + (char)39 + ")");

        ddBancoTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + ddlBancoOrigen.UniqueID + (char)39 + ")");
        ddlBancoOrigen.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtImporteTarjeta.UniqueID + (char)39 + ")");

        txtImporteTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtObservacionesTarjeta.UniqueID + (char)39 + ")");
        txtObservacionesTarjeta.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + imbAceptarTDC.UniqueID + (char)39 + ")");


        // txtClienteVale.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtClienteVale.UniqueID + (char)39 + ")");

        //imgValeCalendario.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtFolioVale.UniqueID + (char)39 + ")");

        ///txtFolioVale.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtValeFecha.UniqueID + (char)39 + ")");
        txtValeFecha.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtValeImporte.UniqueID + (char)39 + ")");
        //txtValeImporte.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + txtValeImporte.UniqueID + (char)39 + ")");
        txtValeObs.Attributes.Add("onkeypress", "return NumeroRemisionKeyPress(event, " + (char)39 + imbAceptarVale.UniqueID + (char)39 + ")");

        imbEfectivo.Attributes.Add("onclick", "return confirm('¿Desea enviar todos los pedidos a Pago en Efectivo?')");
        imbCancelar.Attributes.Add("onclick", "return confirm('¿Desea Cancelar los Pagos Capturados?')");

        txtClienteTarjeta.Attributes.Add("onblur", "return ConsultaPagosTPV('ConsultaTPV')");

        ImgTransferencia.Attributes.Add("onclick", "toggle('display', 'Transfer', 'cheque', 'tarjeta', " + (char)39 + txtClienteTarjeta.UniqueID + (char)39 + ")");

        ImgAnticipo.Attributes.Add("onclick", "toggle('display', 'Anticipo', 'cheque', 'tarjeta', " + (char)39 + txtClienteTarjeta.UniqueID + (char)39 + ")");

        TxtCteAfiliacion.Attributes.Add("onblur", "return ConsultaPagosTPV('ConsultaTPV-Trans')");

        imbAceptarTDC.Attributes.Add("onclick", "return ValidaCamposTDC()");

        txtNoAutorizacionTarjeta.Attributes.Add("onkeypress", "return isAlphaNumeric(event)");

        txtNoAutorizacionTarjetaConfirm.Attributes.Add("onkeypress", "return isAlphaNumeric(event)");



        #endregion




        if (Session["dsLiquidacion"] == null)
        {
            string path = Server.MapPath("");
            ds.ReadXml(path + "/App_Code/dsLiquidacion.xsd");
        }
        else
        {
            ds = (DataSet)(Session["dsLiquidacion"]);
            if (((DataSet)(Session["dsLiquidacion"])).Tables.Contains("Cobro"))
                if (ds.Tables["Cobro"].Rows.Count > 0)
            {
                lblCobros.Visible = true;
                imgExpandCollapse.Visible = true;
                imbResumen.Visible = true;

                TitlePanel.Visible = true;
                ContentPanel.Visible = true;

                gvPagoGenerado.DataSource = ds.Tables["Cobro"];
                gvPagoGenerado.DataBind();
            }
            else
            {
                lblCobros.Visible = false;
                imgExpandCollapse.Visible = false;

                TitlePanel.Visible = false;
                ContentPanel.Visible = false;

                gvPagoGenerado.DataSource = null;
                gvPagoGenerado.DataBind();

                imbResumen.Visible = false;
            }
        }

        if ((!Page.IsPostBack) && (Session["FechaAsignacion"] != null))//09/07/2011 ERROR DE CAPTURA DE FECHA
        {
            txtFechaChueque.Text = Session["FechaAsignacion"].ToString();
            txtFechaTarjeta.Text = Session["FechaAsignacion"].ToString();
        }

    }



    #region "Funcs and Subs"
    private void CargaPedidos()
    {
        DataTable dtPedidos = new DataTable();
        try
        {
            dtPedidos = rp.ListaPedidos();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LlenaDropDowns()
    {
        DataTable dtBancos = new DataTable();
        DataTable dtPromocion = new DataTable();
        Dictionary<string, string> TipoTarjeta = new Dictionary<string, string>();



        try
        {
            dtBancos = rp.ListaBancos();
            dtAfiliaciones = rp.Afiliaciones(int.Parse(Session["Ruta"].ToString()));
            dtProveedores = rp.Proveedores();
            dtTipoVale = rp.TipoVale();

            ddBancoTarjeta.DataSource = dtBancos;
            ddBancoTarjeta.DataTextField = "Nombre";
            ddBancoTarjeta.DataValueField = "Banco";
            ddBancoTarjeta.DataBind();
            ddBancoTarjeta.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddBancoTarjeta.SelectedIndex = 0;

            ddChequeBanco.DataSource = dtBancos;
            ddChequeBanco.DataTextField = "Nombre";
            ddChequeBanco.DataValueField = "Banco";
            ddChequeBanco.DataBind();
            ddChequeBanco.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddChequeBanco.SelectedIndex = 0;

            ddlBancoOrigen.DataSource = dtBancos;
            ddlBancoOrigen.DataTextField = "Nombre";
            ddlBancoOrigen.DataValueField = "Banco";
            ddlBancoOrigen.DataBind();
            ddlBancoOrigen.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlBancoOrigen.SelectedIndex = 0;

            //ddlValePromocion.DataSource = dtPromocion;
            //ddlValePromocion.DataTextField = "ValePromocion";
            //ddlValePromocion.DataValueField = "Descripcion";
            //ddlValePromocion.DataBind();

            ddBancoTrasferencia.DataSource = dtBancos;
            ddBancoTrasferencia.DataTextField = "Nombre";
            ddBancoTrasferencia.DataValueField = "Banco";
            ddBancoTrasferencia.DataBind();
            ddBancoTrasferencia.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddBancoTrasferencia.SelectedIndex = 0;

            // Tipo Tarjeta

            TipoTarjeta.Add("0", "Seleccione");
            TipoTarjeta.Add("19", "Tarjeta de Debito");
            TipoTarjeta.Add("6", "Tarjeta de Crédito");

            ddTipoTarjeta.DataSource = TipoTarjeta;
            ddTipoTarjeta.DataTextField = "Value";
            ddTipoTarjeta.DataValueField = "Key";
            ddTipoTarjeta.DataBind();



            ddTipTarjeta.DataSource = TipoTarjeta;
            ddTipTarjeta.DataTextField = "Value";
            ddTipTarjeta.DataValueField = "Key";
            ddTipTarjeta.DataBind();

            ddAfiliacion.DataSource = dtAfiliaciones;
            ddAfiliacion.DataTextField = "NumeroAfiliacion";
            ddAfiliacion.DataValueField = "Afiliacion";
            ddAfiliacion.DataBind();
            ddAfiliacion.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddAfiliacion.SelectedIndex = 0;


            ddlProveedor.DataSource = dtProveedores;
            ddlProveedor.DataTextField = "NombreProveedor";
            ddlProveedor.DataValueField = "ValeProveedor";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlProveedor.SelectedIndex = 0;


            ddlTipoVale.DataSource = dtTipoVale;
            ddlTipoVale.DataTextField = "Descripcion";
            ddlTipoVale.DataValueField = "ValeTipo";
            ddlTipoVale.DataBind();
            ddlTipoVale.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlTipoVale.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
    #region "Handlers"
    protected void imbAceptarVale_Click(object sender, EventArgs e)
    {
        int idConsecutivo;

        try
        {
            idConsecutivo = Session["idCobroConsec"] != null ? ((Int32)(Session["idCobroConsec"]) + 1) : 1;

            if ((DataSet)(Session["dsLiquidacion"]) == null)
            {
                //CreateTableCobro();
                //Genera Registro del Cobro con Vale

                dtCobro = ds.Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                //dr["IdCobro"] = 0; //Consecutivo
                //dr["Referencia"] = txtFolioVale.Text;
                //dr["NumeroCuenta"] = txtFolioVale.Text;


         

                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["FechaCheque"] = txtValeFecha.Text; 
                dr["Cliente"] = txtClienteVale.Text;
                dr["Banco"] = " ";
                dr["Referencia"] = 0;
                dr["NumeroCuenta"] = 0;

                dr["Importe"] = (Convert.ToDouble(txtValeImporte.Text) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble(txtValeImporte.Text) * rp.dbIVA);
                dr["Total"] = txtValeImporte.Text; //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtValeObs.Text;
                dr["Status"] = "ABIERTO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.tipoVale);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = txtValeFecha.Text;

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "VALE";
                dr["Banco"] = 0;

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtValeImporte.Text);
               // dr["IdCobro"] = 0;

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = txtClienteVale.Text;
                //Session["TablaCobro"] = dtCobro;
                Session["dsLiquidacion"] = dtCobro.DataSet;

                Session["idCobroConsec"] = idConsecutivo;

                Session["FormaPago"] = "Vale";
            }
            else
            {
                //Genera Registro del Cobro con Vale

                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();


                //dr["IdCobro"] = 0; //Consecutivo
                //dr["Referencia"] = txtFolioVale.Text;
                //dr["NumeroCuenta"] = txtFolioVale.Text;

                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["FechaCheque"] = txtValeFecha.Text;
                dr["Cliente"] = txtClienteVale.Text;
                dr["Banco"] = " ";
                dr["Referencia"] = 0;
                dr["NumeroCuenta"] = 0;

                dr["Importe"] = (Convert.ToDouble(txtValeImporte.Text) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble(txtValeImporte.Text) * rp.dbIVA);
                dr["Total"] = txtValeImporte.Text; //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtValeObs.Text;
                dr["Status"] = "ABIERTO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.tipoVale);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = txtValeFecha.Text;

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "VALE";
                dr["Banco"] = 0;
                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtValeImporte.Text);

                dtCobro.Rows.Add(dr);
                //Session["TablaCobro"] = dtCobro;
                Session["idCliente"] = txtClienteVale.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;

                Session["idCobroConsec"] = idConsecutivo;
                Session["FormaPago"] = "Vale";
            }

        

            Response.Redirect("RegistroPagos.aspx");

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void imbAceptar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if ((DataSet)(Session["dsLiquidacion"]) == null)
            {

                //CreateTableCobro();
                //Genera Registro del Cobro con Cheque

                dtCobro = ds.Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdPago"] = 1; //Consecutivo
                dr["Referencia"] = txtNumeroCheque.Text;
                dr["NumeroCuenta"] = txtNumCuenta.Text;

                dr["FechaCheque"] = txtFechaChueque.Text;
                dr["Cliente"] = txtClienteCheque.Text;
                dr["Banco"] = ddChequeBanco.SelectedValue;

                dr["Importe"] = (Convert.ToDouble((txtImporteCheque.Text.ToString().Replace("$", ""))) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble((txtImporteCheque.Text.ToString().Replace("$", ""))) * rp.dbIVA);
                dr["Total"] = txtImporteCheque.Text; //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservacionesChueque.Text;
                dr["Status"] = "EMITIDO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.tipoCheque);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = txtFechaChueque.Text;

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "CHEQUE";

                dtCobro.Rows.Add(dr);
                //Subo a Session la tabla creada
                //Session["TablaCobro"] = dtCobro;
                Session["dsLiquidacion"] = dtCobro.DataSet;

                //Subo a Session el consecutivo actual para posteriores capturas de pagos
                Session["idCobroConsec"] = 1;
                importeOperacion = Convert.ToDecimal(txtImporteCheque.Text);
                Session["ImporteOperacion"] = importeOperacion;

                Session["idCliente"] = txtClienteCheque.Text;
                Session["FormaPago"] = "cheque";
            }
            else
            {
                //Genera Registro del Cobro con Cheque
                DataRow dr;
                int idConsecutivo;
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                dr = dtCobro.NewRow();

                idConsecutivo = ((Int32)(Session["idCobroConsec"]) + 1);

                dr["IdPago"] = idConsecutivo; //Consecutivo
                Session["idCobroConsec"] = idConsecutivo;
                dr["Referencia"] = txtNumeroCheque.Text;
                dr["NumeroCuenta"] = txtNumCuenta.Text;

                dr["FechaCheque"] = txtFechaChueque.Text;
                dr["Cliente"] = txtClienteCheque.Text;
                dr["Banco"] = ddChequeBanco.SelectedValue;

                dr["Importe"] = (Convert.ToDouble(txtImporteCheque.Text.ToString().Replace("$", "")) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble(txtImporteCheque.Text.ToString().Replace("$", "")) * rp.dbIVA);
                dr["Total"] = txtImporteCheque.Text; //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservacionesChueque.Text;
                dr["Status"] = "EMITIDO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.tipoCheque);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = txtFechaChueque.Text;

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "CHEQUE";

                dtCobro.Rows.Add(dr);

                importeOperacion = Convert.ToDecimal(txtImporteCheque.Text);
                Session["ImporteOperacion"] = importeOperacion;

                Session["idCliente"] = txtClienteCheque.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;
                Session["FormaPago"] = "cheque";
            }
            Response.Redirect("RegistroPagos.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbAceptarTDC_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

              
            if ((DataSet)(Session["dsLiquidacion"]) == null)
            {
                //Genera Registro del Cobro con Cheque
                dtCobro = ds.Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdPago"] = 1; //Consecutivo
                dr["Referencia"] = txtNoAutorizacionTarjeta.Text;
                dr["NumeroCuenta"] = txtNoAutorizacionTarjeta.Text;

                dr["FechaCheque"] = txtFechaTarjeta.Text;
                dr["Cliente"] = txtClienteTarjeta.Text;
                //dr["Banco"] = ddBancoTarjeta.SelectedItem.Text;
                dr["Banco"] = ddBancoTarjeta.SelectedValue;

                dr["Importe"] = (Convert.ToDouble((txtImporteTarjeta.Text.ToString().Replace("$",""))) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble((txtImporteTarjeta.Text.ToString().Replace("$", ""))) * rp.dbIVA);
                dr["Total"] = txtImporteTarjeta.Text.ToString().Replace("$", ""); //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservacionesTarjeta.Text;
                dr["Status"] = "EMITIDO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(int.Parse(ddTipTarjeta.SelectedValue.ToString())); 
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                if (chkLocal.Checked) { dr["TPV"] = 1; } else { dr["TPV"] = 0; }
                dr["FechaDeposito"] = DateTime.Now.Date;

                dr["BancoOrigen"] = ddlBancoOrigen.SelectedItem.Text;
                dr["NombreTipoCobro"] = "TARJETA";

                dtCobro.Rows.Add(dr);
                //Subo a Session la tabla creada
                //Session["TablaCobro"] = dtCobro;
                Session["dsLiquidacion"] = dtCobro.DataSet;
                Session["idCliente"] = txtClienteTarjeta.Text;
                //Subo a Session el consecutivo actual para posteriores capturas de pagos
                Session["idCobroConsec"] = 1;
                importeOperacion = Convert.ToDecimal(txtImporteTarjeta.Text);
                Session["ImporteOperacion"] = importeOperacion;
                Session["FormaPago"] = "TDC";
            }
            else
            {
                //Genera Registro del Cobro con Cheque
                DataRow dr;
                int idConsecutivo;
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                dr = dtCobro.NewRow();

                idConsecutivo = ((Int32)(Session["idCobroConsec"]) + 1);

                dr["IdPago"] = idConsecutivo; //Consecutivo
                Session["idCobroConsec"] = idConsecutivo;
                dr["Referencia"] = txtNoAutorizacionTarjeta.Text;
                dr["NumeroCuenta"] = txtNoAutorizacionTarjeta.Text;

                dr["FechaCheque"] = txtFechaTarjeta.Text;
                dr["Cliente"] = txtClienteTarjeta.Text;
                dr["Banco"] = ddBancoTarjeta.SelectedValue;

                dr["Importe"] = (Convert.ToDouble((txtImporteTarjeta.Text.ToString().ToString().Replace("$",""))) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble((txtImporteTarjeta.Text.ToString().Replace("$", ""))) * rp.dbIVA);
                dr["Total"] = txtImporteTarjeta.Text.ToString().Replace("$", ""); //CHECK THIS

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservacionesTarjeta.Text;
                dr["Status"] = "EMITIDO"; //CHECK THIS 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(int.Parse(ddTipTarjeta.SelectedValue.ToString()));
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                if (chkLocal.Checked) { dr["TPV"] = 1; } else { dr["TPV"] = 0; }
                dr["FechaDeposito"] = DateTime.Now.Date;

                dr["BancoOrigen"] = ddlBancoOrigen.SelectedItem.Text;
                dr["NombreTipoCobro"] = "TARJETA";

                dtCobro.Rows.Add(dr);

                importeOperacion = Convert.ToDecimal(txtImporteTarjeta.Text);
                Session["idCliente"] = txtClienteTarjeta.Text;
                Session["ImporteOperacion"] = importeOperacion;
                Session["FormaPago"] = "TDC";
            }
            Response.Redirect("RegistroPagos.aspx");
                // your code here.



        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbEfectivo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtPedidosEf = new DataTable();
            dtPedidosEf = ((DataTable)(Session["dtPedidos"]));

            rp.GuardaPagos(Convert.ToString(Session["Usuario"]), dtPedidosEf, null, null, (DataTable)(Session["dtResumenLiquidacion"]));
            Response.Redirect("ReporteLiquidacion.aspx");
            Session["FormaPago"] = "Efectivo";
            //Parametros param = new Parametros(1, 1, 22);
            // Reporte

            //Ruta en disco
            //string strReporte = Request.PhysicalApplicationPath + "/rptLiquidacion.rpt";

            //if (File.Exists(strReporte))
            //{
            //    try
            //    {

            //        string strServer = param.ValorParametro("Server").ToString();
            //        string strDatabase = param.ValorParametro("Database").ToString();
            //        string strUsuario = param.ValorParametro("Usuario").ToString();
            //        string strPW = param.ValorParametro("Password").ToString();
            //        string añoAtt;
            //        string folio;

            //        string strError = "";
            //        //Parametros
            //        añoAtt = Session["AñoAtt"].ToString();
            //        folio = Session["Folio"].ToString();
            //        ArrayList Par = new ArrayList();
            //        //FIX THIS
            //        Par.Add("@añoAtt=" + añoAtt);
            //        Par.Add("@Folio=" + folio);

            //        Clase_Reporte Reporte = new Clase_Reporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            //        strError = "Clase";
            //        Session["RepDoc"] = Reporte.RepDoc;
            //        Session["Parametros"] = Par;
            //        Response.Redirect("ReporteLiquidacion.aspx");
            //        if (Reporte.Hay_Error)
            //        {
            //            strError = Reporte.Error;
            //            throw new Exception("strError");
            //        }
            //        Reporte = null;
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = ex.Message;
            //}
            //}
            //else
            //{
            //    lblError.Text = "El archivo de Reporte no fue encontrado";
            //}
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ds.Tables["Pedidos"].Clear();
            ds.Tables["Pedidos"].Merge((DataTable)(Session["dtPedidos"]));
            ds.Tables["Cobro"].Clear();
            ds.Tables["CobroPedido"].Clear();
            Response.Redirect("Liquidacion.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    #endregion
    //protected void dlPagosCapturados_DeleteCommand(object source, DataListCommandEventArgs e)
    //{
    //    //string idPago;
    //    //DataRow[] drArray;

    //    //dlPagosCapturados.SelectedIndex = e.Item.ItemIndex;
    //    //idPago = dlPagosCapturados.DataKeyField[e.Item.ItemIndex];

    //    string refPago;
    //    //int pagoActivo = Convert.ToInt32(Session["idCobroConsec"]);
    //    string pagoActivo;
    //    decimal importeAbono;
    //    DataTable dtEliminar = new DataTable();
    //    DataRow[] drArray;
    //    Decimal impTotal;

    //    try
    //    {
    //        pagoActivo = dlPagosCapturados.DataKeyField[e.Item.ItemIndex].ToString();
    //        impTotal = Convert.ToDecimal(((Label)(dlPagosCapturados.FindControl("lblMonto"))).Text);
    //       // dtPagos = ds.Tables["CobroPedido"];
    //        //dtPedidos = ds.Tables["Pedidos"];
    //        //Busca los pagos capturados en la tabla de pedidos y actualizo saldos
    //       // for (int i = 0; i <= dtPagos.Rows.Count - 1; i++)
    //        for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
    //        {
    //           // refPago = dtPagos.Rows[i]["Anio"].ToString().TrimEnd() + dtPagos.Rows[i]["Celula"].ToString().TrimEnd() + dtPagos.Rows[i]["Pedido"].ToString().TrimEnd();
    //            refPago = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();

    //            for (int j = 0; j <= ds.Tables["Pedidos"].Rows.Count - 1; j++)
    //            {
    //                if (refPago == ds.Tables["Pedidos"].Rows[j]["Pedido"].ToString() && pagoActivo.ToString() == ds.Tables["CobroPedido"].Rows[i]["IdPago"].ToString())
    //                {
    //                    importeAbono = Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"]);

    //                    ActualizaSaldo(refPago, importeAbono, pagoActivo,  impTotal);
    //                }
    //            }
    //        }

    //        // Eliminar de la tabla del Detalle los Rows del Detalle Cancelado           
    //        drArray = ds.Tables["CobroPedido"].Select("IdPago = " + pagoActivo.ToString(), null);
    //        foreach (DataRow dr in drArray)
    //        {
    //            dtPagos.Rows.Remove(dr);
    //        }
    //        drArray = null;
    //        //Eliminar de la tabla de Cobro el cobro capturado y actualizar tabla
    //        //dtCobros = (DataTable)(Session["TablaCobro"]);
    //        //dtCobros = ds.Tables["Cobro"];
    //        drArray = ds.Tables["Cobro"].Select("IdPago = " + pagoActivo.ToString(), null);

    //        foreach (DataRow dr in drArray)
    //        {
    //            ds.Tables["Cobro"].Rows.Remove(dr);
    //        }

    //        // Session["TablaCobro"] = dtCobros;

    //        //   ds.Tables["Cobro"].Clear();
    //        //  ds.Tables["Cobro"].Merge(dtCobros);

    //        //DataView vistaPagoActivo = new DataView(dtPagos);
    //        //vistaPagoActivo.RowFilter = "IdPago = " + pagoActivo;
    //        //gvRelacionCobro.DataSource = vistaPagoActivo;
    //        //gvRelacionCobro.DataBind();

    //        //pagoActivo = pagoActivo - 1;
    //        //Session["idCobroConsec"] = pagoActivo;
    //        Session["dsLiquidacion"] = dtCobros.DataSet;
    //    }
    //    catch { throw; }
    //}

    protected bool ActualizaSaldo(string referencia, decimal monto, string IdPago, Decimal importe)
    {
        decimal saldoActual;
        DataRow dr;
        bool valid = false;
        DataRow[] drArrayMov;
        Decimal importeMovto;

        try
        {
            //importeMovto = importe;

            for (int i = 0; i <= ds.Tables["Pedidos"].Rows.Count - 1; i++)
            {
                //Encuentro la Referencia del Pedido

                if (ds.Tables["Pedidos"].Rows[i]["pedidoreferencia"].ToString().TrimEnd() == referencia.ToString())
                {
                    {

                        //dtPedidos = ds.Tables["Pedidos"];

                        saldoActual = (Convert.ToDecimal(ds.Tables["Pedidos"].Rows[i]["saldo"].ToString()) + monto);


                        dr = ds.Tables["Pedidos"].Rows[i];
                        dr.BeginEdit();
                        dr["saldo"] = Convert.ToDecimal(saldoActual);
                        dr.EndEdit();

                        valid = true;
                    }
                }
            }
            return valid;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void btnBorrar_Click(object sender, ImageClickEventArgs e)
    //{
    //    string refPago;
    //    //int pagoActivo = Convert.ToInt32(Session["idCobroConsec"]);
    //    string pagoActivo;
    //    decimal importeAbono;
    //    DataTable dtEliminar = new DataTable();
    //    DataRow[] drArray;
    //    Decimal impTotal;

    //    try
    //    {
    //        //pagoActivo = ((Label)(dlPagosCapturados.SelectedItem.FindControl("lblMonto"))).Text;
    //        pagoActivo = ((Label)(dlPagosCapturados.SelectedItem.FindControl("lblMonto"))).Text;
    //        impTotal = Convert.ToDecimal(((Label)(dlPagosCapturados.SelectedItem.FindControl("lblMonto"))).Text);
    //        dtPagos = ds.Tables["CobroPedido"];
    //        dtPedidos = ds.Tables["Pedidos"];
    //        //Busca los pagos capturados en la tabla de pedidos y actualizo saldos
    //        for (int i = 0; i <= dtPagos.Rows.Count - 1; i++)
    //        {
    //            refPago = dtPagos.Rows[i]["Anio"].ToString().TrimEnd() + dtPagos.Rows[i]["Celula"].ToString().TrimEnd() + dtPagos.Rows[i]["Pedido"].ToString().TrimEnd();

    //            for (int j = 0; j <= dtPedidos.Rows.Count - 1; j++)
    //            {
    //                if (refPago == dtPedidos.Rows[j]["Pedido"].ToString() && pagoActivo.ToString() == dtPagos.Rows[i]["IdPago"].ToString())
    //                {
    //                    importeAbono = Convert.ToDecimal(dtPagos.Rows[i]["Importe"]);

    //                    ActualizaSaldo(refPago, importeAbono, pagoActivo, impTotal);
    //                }
    //            }
    //        }

    //        // Eliminar de la tabla del Detalle los Rows del Detalle Cancelado           
    //        drArray = dtPagos.Select("IdPago = " + pagoActivo.ToString(), null);
    //        foreach (DataRow dr in drArray)
    //        {
    //            dtPagos.Rows.Remove(dr);
    //        }
    //        drArray = null;
    //        //Eliminar de la tabla de Cobro el cobro capturado y actualizar tabla
    //        //dtCobros = (DataTable)(Session["TablaCobro"]);
    //        dtCobros = ds.Tables["Cobro"];
    //        drArray = dtCobros.Select("IdPago = " + pagoActivo.ToString(), null);

    //        foreach (DataRow dr in drArray)
    //        {
    //            dtCobros.Rows.Remove(dr);
    //        }

    //        // Session["TablaCobro"] = dtCobros;

    //        //   ds.Tables["Cobro"].Clear();
    //        //  ds.Tables["Cobro"].Merge(dtCobros);

    //        //DataView vistaPagoActivo = new DataView(dtPagos);
    //        //vistaPagoActivo.RowFilter = "IdPago = " + pagoActivo;
    //        //gvRelacionCobro.DataSource = vistaPagoActivo;
    //        //gvRelacionCobro.DataBind();

    //        //pagoActivo = pagoActivo - 1;
    //        //Session["idCobroConsec"] = pagoActivo;
    //        Session["dsLiquidacion"] = dtCobros.DataSet;
    //    }
    //    catch { throw; }
    //}
    protected void gvPagoGenerado_SelectedIndexChanged(object sender, EventArgs e)
    {

        string refPago;
        string refPed;
        //int pagoActivo = Convert.ToInt32(Session["idCobroConsec"]);
        string pagoActivo;
        decimal importeAbono;
        DataTable dtEliminar = new DataTable();
        DataRow[] drArray;
        Decimal impTotal;

        try
        {
            pagoActivo = gvPagoGenerado.SelectedRow.Cells[0].Text;
            impTotal = Convert.ToDecimal(gvPagoGenerado.SelectedRow.Cells[3].Text.Replace("$", ""));
            //Busca los pagos capturados en la tabla de pedidos y actualizo saldos
            for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
            {
                refPago = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();

                //refPed = dtPagos.Rows[i]["Pedido"].ToString().TrimEnd();

                for (int j = 0; j <= ds.Tables["Pedidos"].Rows.Count - 1; j++)
                {
                    if (refPago == ds.Tables["Pedidos"].Rows[j]["PedidoReferencia"].ToString().TrimEnd() && pagoActivo.ToString() == ds.Tables["CobroPedido"].Rows[i]["IdPago"].ToString())
                    {
                        importeAbono = Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"]);

                        ActualizaSaldo(refPago, importeAbono, pagoActivo, impTotal);
                    }
                }
            }

            // Eliminar de la tabla del Detalle los Rows del Detalle Cancelado           
            drArray = ds.Tables["CobroPedido"].Select("IdPago = '" + pagoActivo.ToString() + "'", null);
            foreach (DataRow dr in drArray)
            {
                ds.Tables["CobroPedido"].Rows.Remove(dr);
            }
            drArray = null;
            //Eliminar de la tabla de Cobro el cobro capturado y actualizar tabla
            drArray = ds.Tables["Cobro"].Select("IdPago = '" + pagoActivo.ToString() + "'", null);

            foreach (DataRow dr in drArray)
            {
                ds.Tables["Cobro"].Rows.Remove(dr);
            }


            Session["dsLiquidacion"] = ds;
            if (ds.Tables["Cobro"].Rows.Count > 0)
            {
                //   Response.Redirect("GenerarPago.aspx");

                lblCobros.Visible = true;
                //imgExpandCollapse.Visible = true;

                TitlePanel.Visible = true;
                ContentPanel.Visible = true;

                gvPagoGenerado.DataSource = ds.Tables["Cobro"];
                gvPagoGenerado.DataBind();
                imbResumen.Visible = true;
            }
            else
            {
                lblCobros.Visible = false;
                //imgExpandCollapse.Visible = false;

                TitlePanel.Visible = false;
                ContentPanel.Visible = false;

                gvPagoGenerado.DataSource = null;
                gvPagoGenerado.DataBind();

                imbResumen.Visible = false;
            }

        }
        catch { throw; }
    }
    protected void imbResumen_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("GenerarPago.aspx");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sTipoConsulta"></param>
    private void MuestraPagoSeleccionado(string sTipoConsulta)
    {

        if (Request.Form["__EVENTTARGET"].ToString().Contains("tarjeta"))
        {
            HiddenInput.Value = "SeleccionaPago";
            clave = Request.Form["__EVENTTARGET"].ToString().Split('=');
            dtPagosConTarjeta = rp.PagosConTarjeta(int.Parse(txtClienteTarjeta.Text), int.Parse(Session["Ruta"].ToString()), int.Parse(Session["Autotanque"].ToString()));
            dtPagosConTarjetaSelec = dtPagosConTarjeta.Select("Registro=" + clave[1].ToString());

            txtNombreClienteTarjeta.Text = dtPagosConTarjetaSelec[0]["NombreCliente"].ToString();
            txtNoAutorizacionTarjeta.Text = dtPagosConTarjetaSelec[0]["Autorizacion"].ToString();
            txtNumTarjeta.Text = dtPagosConTarjetaSelec[0]["NumeroTarjeta"].ToString();
            ddBancoTarjeta.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjetaSelec[0]["Nombrebanco"].ToString().Trim()));
            ddlBancoOrigen.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjetaSelec[0]["Nombrebanco"].ToString().Trim()));
            txtImporteTarjeta.Text = dtPagosConTarjetaSelec[0]["Importe"].ToString().ToString().Replace("$", "");
            txtObservacionesTarjeta.Text = dtPagosConTarjetaSelec[0]["Observacion"].ToString();
            txtNoAutorizacionTarjeta.ReadOnly = true;
            ddTipTarjeta.SelectedIndex = int.Parse(dtPagosConTarjetaSelec[0]["TipoTarjeta"].ToString());
            chkLocal.Checked = dtPagosConTarjeta.Rows[0]["Local"].ToString() == "True" ? true : false;
            txtFechaTarjeta.Text = DateTime.Parse(dtPagosConTarjetaSelec[0]["FAlta"].ToString()).ToShortDateString();


            txtNoAutorizacionTarjeta.ReadOnly = txtNoAutorizacionTarjeta.Text == "" ? false : true;
            txtFechaTarjeta.ReadOnly = txtFechaTarjeta.Text == "" ? false : true;
            txtNumTarjeta.ReadOnly = txtNumTarjeta.Text == "" ? false : true;
            txtImporteTarjeta.ReadOnly = txtImporteTarjeta.Text == "" ? false : true;
            ddBancoTarjeta.Enabled = ddBancoTarjeta.SelectedIndex == 0 ? true : false;
            ddlBancoOrigen.Enabled = ddlBancoOrigen.SelectedIndex == 0 ? true : false;
            ddTipTarjeta.Enabled = ddTipTarjeta.SelectedIndex == 0 ? true : false;
            chkLocal.Enabled = dtPagosConTarjeta.Rows[0]["Local"].ToString() == "" ? true : false;
            txtObservacionesTarjeta.ReadOnly = txtNoAutorizacionTarjeta.Text == "" ? false : true;
            imgCalendario0.Enabled = txtNoAutorizacionTarjeta.Text == "" ? true : false;
        }



        if (Request.Form["__EVENTTARGET"].ToString().Contains("transferencia"))
        {
            HiddenInput.Value = "SeleccionaPago-Trans";
            clave = Request.Form["__EVENTTARGET"].ToString().Split('=');
            dtPagosConTarjeta = rp.PagosConTarjeta(int.Parse(TxtCteAfiliacion.Text), int.Parse(Session["Ruta"].ToString()), int.Parse(Session["Autotanque"].ToString()));
            dtPagosConTarjetaSelec = dtPagosConTarjeta.Select("Registro=" + clave[1].ToString());

            TxtNombreCteTrans.Text = dtPagosConTarjetaSelec[0]["NombreCliente"].ToString();
            TxtAutorizacionTrans.Text = dtPagosConTarjetaSelec[0]["Autorizacion"].ToString();
            ddBancoTrasferencia.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjetaSelec[0]["Nombrebanco"].ToString().Trim()));
            TxtTarjetaTranferencia.Text = dtPagosConTarjetaSelec[0]["NumeroTarjeta"].ToString();
            ddAfiliacion.SelectedIndex = ddAfiliacion.Items.IndexOf(ddAfiliacion.Items.FindByValue(dtPagosConTarjetaSelec[0]["Afiliacion"].ToString().Trim()));
            TxtRepAutorizacionTrans.Text = dtPagosConTarjetaSelec[0]["Autorizacion"].ToString();
            TxtObervacionesTrans.Text = dtPagosConTarjetaSelec[0]["Observacion"].ToString();
        }



    }



    /// <summary>
    /// Consulta Pagos Con Tarjeta del Cliente
    /// </summary>
    /// <param name="NumCliente"></param>
    private void ConsultarCargoTarjeta(int NumCliente, string sFormaPago, int Ruta, int Autotanque)
    {
        DataTable dtDatosControlUsuario = new DataTable();


        dtDatosControlUsuario.Columns.Add("TipoCobro", typeof(string));
        dtDatosControlUsuario.Columns.Add("Tarjeta", typeof(string));
        dtDatosControlUsuario.Columns.Add("Banco", typeof(string));
        dtDatosControlUsuario.Columns.Add("Autorizacion", typeof(string));
        dtDatosControlUsuario.Columns.Add("Importe", typeof(string));
        dtDatosControlUsuario.Columns.Add("Observacion", typeof(string));
        dtDatosControlUsuario.Columns.Add("Anio", typeof(string));
        dtDatosControlUsuario.Columns.Add("Folio", typeof(string));

        if (sFormaPago == "tarjeta")
            dtPagosConTarjeta = rp.PagosConTarjeta(int.Parse(txtClienteTarjeta.Text), Ruta, Autotanque);

        if (sFormaPago == "transferencia")
            dtPagosConTarjeta = rp.PagosConTarjeta(int.Parse(TxtCteAfiliacion.Text), Ruta, Autotanque);



        if (dtPagosConTarjeta.Rows.Count > 0)
        {
            CargaPrimerRegistro(sFormaPago);



            HiddenInputPCT.Value = "Si";

            HiddenInputNumPagos.Value = dtPagosConTarjeta.Rows.Count.ToString();

            foreach (DataRow row in dtPagosConTarjeta.Rows)
            {
                dtDatosControlUsuario.Rows.Add(row["TipoCobroDescripcion"].ToString(), row["NumeroTarjeta"].ToString(), row["NombreBanco"].ToString(), row["Autorizacion"].ToString(), row["Importe"].ToString(), row["Observacion"].ToString(), row["Año"].ToString(), row["Folio"].ToString());
            }
            wucConsultaCargoTarjetaCliente1.sFormaPago = sFormaPago;
            wucConsultaCargoTarjetaCliente1.dtPagosContarjeta = dtDatosControlUsuario;
            //}
            titNoAut.Visible = false;
            titNoAutNum.Visible = false;

            if (dtPagosConTarjeta.Rows[0]["Folio"].ToString()=="")
            {
                HiddenInputPCT.Value = "No";
                titNoAut.Visible = true;
                titNoAutNum.Visible = true;
                txtNoAutorizacionTarjeta.ReadOnly = false;
            }

        }
        else
        {
            HiddenInputPCT.Value = "No";
            titNoAut.Visible = true;
            titNoAutNum.Visible = true;
            



        }
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="sFormaPago"></param>
    private void CargaPrimerRegistro(string sFormaPago)
    {
        switch (sFormaPago)
        {
            case "tarjeta":
                txtNombreClienteTarjeta.Text = dtPagosConTarjeta.Rows[0]["NombreCliente"].ToString();
                txtNoAutorizacionTarjeta.Text = dtPagosConTarjeta.Rows[0]["Autorizacion"].ToString();
                txtFechaTarjeta.Text= dtPagosConTarjeta.Rows[0]["FAlta"].ToString()!=""? DateTime.Parse(dtPagosConTarjeta.Rows[0]["FAlta"].ToString()).ToShortDateString():"";
                txtNumTarjeta.Text = dtPagosConTarjeta.Rows[0]["NumeroTarjeta"].ToString();
                ddBancoTarjeta.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjeta.Rows[0]["Nombrebanco"].ToString().Trim()));
                ddlBancoOrigen.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjeta.Rows[0]["Nombrebanco"].ToString().Trim()));
                txtImporteTarjeta.Text = dtPagosConTarjeta.Rows[0]["Importe"].ToString().Replace("$", "");
                txtObservacionesTarjeta.Text = dtPagosConTarjeta.Rows[0]["Observacion"].ToString();
                ddTipTarjeta.SelectedIndex= dtPagosConTarjeta.Rows[0]["TipoTarjeta"].ToString()!=""?int.Parse(dtPagosConTarjeta.Rows[0]["TipoTarjeta"].ToString()):0;
                chkLocal.Checked = dtPagosConTarjeta.Rows[0]["Local"].ToString() == "True" ? true : false;


                txtNoAutorizacionTarjeta.ReadOnly = txtNoAutorizacionTarjeta.Text == "" ? false : true;
                txtFechaTarjeta.ReadOnly= txtFechaTarjeta.Text == "" ? false : true;
                txtNumTarjeta.ReadOnly= txtNumTarjeta.Text == "" ? false : true;
                txtImporteTarjeta.ReadOnly= txtImporteTarjeta.Text == "" ? false : true;
                ddBancoTarjeta.Enabled = ddBancoTarjeta.SelectedIndex == 0 ? true : false;
                ddlBancoOrigen.Enabled = ddlBancoOrigen.SelectedIndex == 0 ? true:false;
                ddTipTarjeta.Enabled = ddTipTarjeta.SelectedIndex == 0 ? true : false;
                chkLocal.Enabled = dtPagosConTarjeta.Rows[0]["Local"].ToString() == "" ? true : false;
                txtObservacionesTarjeta.ReadOnly= txtNoAutorizacionTarjeta.Text == "" ? false : true;
                imgCalendario0.Enabled = txtNoAutorizacionTarjeta.Text == "" ? true : false;
                //ddBancoTarjeta.Enabled=
                break;

            case "transferencia":
                TxtNombreCteTrans.Text = dtPagosConTarjeta.Rows[0]["NombreCliente"].ToString();
                TxtAutorizacionTrans.Text = dtPagosConTarjeta.Rows[0]["Autorizacion"].ToString();
                ddBancoTrasferencia.SelectedIndex = ddBancoTarjeta.Items.IndexOf(ddBancoTarjeta.Items.FindByText(dtPagosConTarjeta.Rows[0]["Nombrebanco"].ToString().Trim()));
                TxtTarjetaTranferencia.Text = dtPagosConTarjeta.Rows[0]["NumeroTarjeta"].ToString();
                ddAfiliacion.SelectedIndex = ddAfiliacion.Items.IndexOf(ddAfiliacion.Items.FindByValue(dtPagosConTarjeta.Rows[0]["Afiliacion"].ToString().Trim()));
                TxtRepAutorizacionTrans.Text = dtPagosConTarjeta.Rows[0]["Autorizacion"].ToString();
                TxtObervacionesTrans.Text = dtPagosConTarjeta.Rows[0]["Observacion"].ToString();

                break;

            default:
                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void LimpiarCampos(string FormaPago)
    {
        switch (FormaPago)
        {
            case "tarjeta":
                txtNombreClienteTarjeta.Text = string.Empty;
                txtNoAutorizacionTarjeta.Text = string.Empty;
                txtFechaTarjeta.Text = string.Empty;
                txtNumTarjeta.Text = string.Empty;
                ddlBancoOrigen.SelectedIndex = -1;
                ddlBancoOrigen.SelectedIndex = -1;
                txtImporteTarjeta.Text = string.Empty;
                txtObservacionesTarjeta.Text = string.Empty;
                ddBancoTarjeta.SelectedIndex = -1;

                break;

            case "transferencia":
                TxtNombreCteTrans.Text = string.Empty;
                TxtRepAutorizacionTrans.Text = string.Empty;
                break;
            default:
                break;
        }



    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="NumCte"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static string ConsultaClienteCheque(string NumCte)
    {
        DataTable dt = null;
        if (NumCte != string.Empty)
        {
            RegistroPago RegPago = new RegistroPago();
            dt = RegPago.DatosCliente(int.Parse(NumCte));

        }

        return DataTableToJSONWithJavaScriptSerializer(dt);


    }



    public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);



    }

    void SubscribForm_ButtonClicked(object sender, EventArgs e)
    {
        Response.Redirect("WebForm2.aspx");
    }





    protected void ddTipTarjeta_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}   