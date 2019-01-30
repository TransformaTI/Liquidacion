using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SigametLiquidacion;
using System.Data;
using System.Web.Script.Serialization;
using FormasPago;
using System.IO;

public partial class UserControl_DetalleFormaPago_wucDetalleFormaPago : System.Web.UI.UserControl
{

    string imgCal;
    string imgBtnBuscar;
    string imgBoton;
    string titulo;
    string tipo;
    TextBox txtIdCliente;
    RegistroPago rp = new RegistroPago();
    DataTable dtCobro = new DataTable();
    DataSet ds = new DataSet();
    ImageClickEventArgs evt ;
    object sender;
    string registroCobro;
    DataTable dtLiqAnticipo=new DataTable("LiqPagoAnticipado");
    string ClaveAnticipo = string.Empty;
    DataTable dtPedidos=new DataTable("Pedidos");
    DataSet dsLiq;
    DataTable dtLiq, dtCobroPedido;
    DataTable dt;
    DataTable dtCobroLiq;
     string _PostBack;
    private bool _CtaOrigenValida;
    private Cuenta Ctaorigen = new FormasPago.Cuenta();
    private string conexion = string.Empty;



    #region Propiedades del control
    public string ImgCal
    {
        get
        {
            return imgCal;
        }

        set
        {
            imgCal = value;
        }
    }

    public string ImgBoton
    {
        get
        {
            return imgBoton;
        }

        set
        {
            imgBoton = value;
        }
    }

    public string Titulo
    {
        get
        {
            return titulo;
        }

        set
        {
            titulo = value;
        }
    }

    public string TipoCobro
    {
        get
        {
            return tipo;
        }

        set
        {
            tipo = value;
        }
    }

    public TextBox TxtIdCliente
    {
        get
        {
            return this.txtCliente;
        }
    }

    public TextBox TxtAntIdCliente
    {
        get
        {
            return this.txtAntCliente;
        }
    }

    public string ImgBtnBuscar
    {
        get
        {
            return imgBtnBuscar;
        }

        set
        {
            imgBtnBuscar = value;
        }
    }


    public string RegistroCobro
    {
        get { return registroCobro; }
        set { registroCobro = value; }
    }

    private string nombreclientetrans;

    public string NombreClienteTrans
    {
        get { return nombreclientetrans; }
        set { nombreclientetrans = value; }
    }

    private string nombreCteAnticipo;

    public string NombreCteAnticipo
    {
        get { return nombreCteAnticipo; }
        set { nombreCteAnticipo = value; }
    }
    public string PostBack
    {
        get { return _PostBack; }
        set { _PostBack = value; }
    }

    

    public bool CtaOrigenValida
    {
        get { return _CtaOrigenValida; }
        set { _CtaOrigenValida = value; }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((!Page.IsPostBack) && (Session["FechaAsignacion"] != null))//09/07/2011 ERROR DE CAPTURA DE FECHA
        {
            txtFecha.Text = Session["FechaAsignacion"].ToString();
            ddlBancoDestino.Attributes.Add("onchange", "return CuentasBancarias();");
        }

        if (Session["dsLiquidacion"] == null)
        {
            string path = Server.MapPath("");
            ds.ReadXml(path + "/App_Code/dsLiquidacion.xsd");
        }
        else
        {
            ds = (DataSet)(Session["dsLiquidacion"]);
        }

        if (!Page.IsPostBack)
        {
            LlenaDropDowns();

            //TxtCtaOrigen.Attributes.Add("onblur", "return OnblurCtaOrigen();");
            
            this.lblTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Transferencia electrónica de fondos" : this.Titulo;
            this.lblAntTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Aplicación de anticipo" : this.Titulo;

            if (this.TipoCobro == "22")
            {
             
                this.lblTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Transferencia electrónica de fondos" : this.Titulo;
                this.pnlTransferencia.Style.Add("display", "block");
                this.pnlAnticipo.Style.Add("display", "none");
                LimpiarControles();
            }
            else if (this.TipoCobro == "21")
            {
                this.pnlTransferencia.Style.Add("display", "none");
                this.pnlAnticipo.Style.Add("display", "block");
                this.lblAntTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Aplicación de anticipo" : this.Titulo;
            }
        }
        else
        {
            if (Request.Form["__EVENTTARGET"].ToString().Contains("ConsultaCteAnticipo"))
            {
                LimpiarControles();
                ConsultaSaldos();

            }
            if (Request.Form["__EVENTTARGET"].ToString().Contains("ConsultaCteTransferencia"))
            {
                this.TipoCobro = "22";
                int ClienteID = 0;
                if (txtCliente.Text.Trim().Length > 0)
                {
                    ClienteID = Convert.ToInt32(txtCliente.Text.Trim());
                    txtNombre.Text = consultaNombreClienteTransferencia(ClienteID);
                    NombreClienteTrans = txtNombre.Text;
                    txtNombre.Focus();

                }


            }


            //if (Request.Form["__EVENTTARGET"].ToString().Contains("OnblurCtaOrigen"))
            //{
            //    CargaCadenaConexion();

            //    HiddenCtaOrigenValida.Value = Ctaorigen.validarExpresionRegular(3, TxtCtaOrigen.Text, conexion).ToString();
            //    PostBack = "CuentasBancarias";
            //}



            if (Request.Form["__EVENTTARGET"].ToString().Contains("CuentasBancarias"))
            {
                DataTable dtCtasBanco = new DataTable();
                DataTable DtCuentasBanco = new DataTable();

                try
                {
                   

                    dtCtasBanco = rp.ListaCtasBanco(0);
                     DtCuentasBanco = dtCtasBanco.Select("Banco=" + "'" + ddlBancoDestino.SelectedValue + "'").CopyToDataTable();



                    if (DtCuentasBanco.Rows.Count > 0)
                    {

                        ddlCtaDestino.DataSource = DtCuentasBanco;
                        ddlCtaDestino.DataTextField = "CuentaBanco";
                        ddlCtaDestino.DataValueField = "CuentaBanco";
                        ddlCtaDestino.DataBind();
                        ddlCtaDestino.Items.Insert(0, new ListItem("- Seleccione -", "0"));
                        ddlCtaDestino.SelectedIndex = 0;
                    }

                    else

                    {
                        ddlCtaDestino.Items.Clear();
                    }


                }
                catch (Exception ex)
                {
                    if (ex.Source.Contains("System.Data.DataSetExtensions"))
                    {
                        ddlCtaDestino.Items.Clear();
                        
                    }

                    else
                    {
                        throw;
                    }


                }

               
            }

        }
    }





    private void LimpiarControles()
        {
        LstSaldos.Items.Clear();
        txtAntNombre.Text = string.Empty; ;
        txtAntMonto.Text = string.Empty;
        txtAntOnservaciones.Text = string.Empty;
        txtCliente.Text = string.Empty;
        txtNombre.Text = string.Empty;
    }

    //private string CargaCadenaConexion()
    //{    
    //    if ( conexion==string.Empty)
    //    {
    //        TextReader textReader = (TextReader)new StreamReader(HttpContext.Current.Server.MapPath("Conexion.txt"));
    //    conexion = textReader.ReadLine();
    //    }

    //    return conexion;
    //}

    //public bool ValidaCtaOrigen()
    //{
    //    CargaCadenaConexion();
    //    return Ctaorigen.validarExpresionRegular(3, TxtCtaOrigen.Text, conexion);
    //}

    




    private void LlenaDropDowns()
    {
        DataTable dtBancos = new DataTable();
        
        
        try
        {            


            dtBancos = rp.ListaBancos();
            

            ddlBancoOrigen.DataSource = dtBancos;
            ddlBancoOrigen.DataTextField = "Nombre";
            ddlBancoOrigen.DataValueField = "Banco";
            ddlBancoOrigen.DataBind();
            ddlBancoOrigen.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlBancoOrigen.SelectedIndex = 0;


            ddlBancoDestino.DataSource = dtBancos;
            ddlBancoDestino.DataTextField = "Nombre";
            ddlBancoDestino.DataValueField = "Banco";
            ddlBancoDestino.DataBind();
            ddlBancoDestino.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlBancoDestino.SelectedIndex = 0;

  

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["FormaPago"] = "Transferencia";
            int idConsecutivo = Session["idCobroConsec"] != null ? ((Int32)(Session["idCobroConsec"]) + 1) : 1;

            if ((DataSet)(Session["dsLiquidacion"]) != null)
            {
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];



                if (!dtCobro.Columns.Contains("FechaCobro"))
                {

                    dtCobro.Columns.Add("FechaCobro", typeof(System.DateTime));
                }


                if (!dtCobro.Columns.Contains("NumCheque"))
                {

                    dtCobro.Columns.Add("NumCheque", typeof(System.String));
                }

                if (!dtCobro.Columns.Contains("CtaDestino"))
                {

                    dtCobro.Columns.Add("CtaDestino", typeof(System.String));
                }


                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["Referencia"] = this.txtNoDocumento.Text;
                dr["NumeroCuenta"] = ddlCtaDestino.SelectedValue.ToString();

                dr["FechaCheque"] = this.txtFecha.Text;
                dr["Cliente"] = this.txtCliente.Text;
                dr["Banco"] = ddlBancoDestino.SelectedValue; 

                dr["Importe"] = (Convert.ToDouble(this.txtImporte.Text));
                dr["Impuesto"] = (Convert.ToDouble(this.txtImporte.Text) * rp.dbIVA);
                dr["Total"] = txtImporte.Text; 

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservaciones.Text;
                dr["Status"] = "ABIERTO"; 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); 

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = this.txtFecha.Text;

                dr["BancoOrigen"] = ddlBancoOrigen.SelectedValue.ToString();
                dr["NombreTipoCobro"] = "TRANSFERENCIA";
                dr["ProveedorNombre"] = "";
                dr["TipoValeDescripcion"] = "";
                dr["CtaDestino"] = TxtCtaOrigen.Text.ToString();
                //dr["NumCheque"] = this.txtNoCuenta.Text;
                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtImporte.Text); ;

                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;
                Session["idCobroConsec"] = idConsecutivo;

                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');", true);
            }
            else
            {
                //Genera Registro del Cobro con Cheque
                //Session["idCobroConsec"] = 1;
                dtCobro = ds.Tables["Cobro"];


                dtCobro.Columns.Add("FechaCobro", typeof(System.DateTime));
                dtCobro.Columns.Add("NumCheque", typeof(System.String));
                dtCobro.Columns.Add("CtaDestino", typeof(System.String));



                DataRow dr;
                dr = dtCobro.NewRow();

                //dr["IdPago"] = ((Int32)(Session["idCobroConsec"] == null ? 0 : Session["idCobroConsec"]) + 1); ; //Consecutivo
                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["Referencia"] = this.txtNoDocumento.Text;
                dr["NumeroCuenta"] = ddlCtaDestino.SelectedValue.ToString();

                dr["FechaCheque"] = this.txtFecha.Text;
                dr["Cliente"] = this.txtCliente.Text;
                dr["Banco"] = ddlBancoDestino.SelectedValue;

                dr["Importe"] = (Convert.ToDouble(this.txtImporte.Text) );
                dr["Impuesto"] = (Convert.ToDouble(this.txtImporte.Text) * rp.dbIVA);
                dr["Total"] = txtImporte.Text;

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservaciones.Text;
                dr["Status"] = "ABIERTO";

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = this.txtFecha.Text;

                dr["BancoOrigen"] = ddlBancoOrigen.SelectedValue.ToString();
                dr["NombreTipoCobro"] = "TRANSFERENCIA";
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);
                dr["ProveedorNombre"] = "";
                dr["TipoValeDescripcion"] = "";
		        dr["CtaDestino"] = TxtCtaOrigen.Text.ToString();
		        //dr["NumCheque"] = this.txtNoCuenta.Text;
                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtImporte.Text); ;

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;
                Session["idCobroConsec"] = idConsecutivo;

                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');", true);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnAceptarAnticipo_Click(object sender, EventArgs e)
    {
        DataTable dtLiqAnticipoTmp = new DataTable("LiqPagoAnticipado");
        if (decimal.Parse(txtAntMonto.Text != "" ? txtAntMonto.Text:"0") == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "saldo", "alert('El saldo debe ser mayor a cero');", true);
            return;
        }
        
        if (dtLiqAnticipo.Columns.Count ==0)
        {
            dtLiqAnticipo.Columns.Add("Folio", typeof(String));
            dtLiqAnticipo.Columns.Add("AñoMovimiento", typeof(String));
            dtLiqAnticipo.Columns.Add("AñoCobro", typeof(String));
            dtLiqAnticipo.Columns.Add("Monto", typeof(decimal));
            dtLiqAnticipo.Columns.Add("Pedidos", typeof(String));
            dtLiqAnticipo.Columns.Add("IdPago", typeof(String));
        }
        
        try
        {

            int idConsecutivo = Session["idCobroConsec"] != null ? ((Int32)(Session["idCobroConsec"]) + 1) : 1;

            if ((DataSet)(Session["dsLiquidacion"]) != null)
            {

                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];



                if (!dtCobro.Columns.Contains("FechaCobro"))
                {

                    dtCobro.Columns.Add("FechaCobro", typeof(System.DateTime));
                }


                if (!dtCobro.Columns.Contains("NumCheque"))
                {

                    dtCobro.Columns.Add("NumCheque", typeof(System.String));
                }

                DataRow dr;
                dr = dtCobro.NewRow();


                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["Referencia"] =0;
                dr["NumeroCuenta"] = 0;

                dr["FechaCheque"] = LstSaldos.SelectedValue.ToString().Split('/')[3];
                dr["Cliente"] = this.txtAntCliente.Text;
                dr["Banco"] = "0";

                dr["Importe"] = 0;
                dr["Impuesto"] = 0;
                dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text);

                dr["Saldo"] = 0;
                dr["Observaciones"] = this.txtAntOnservaciones.Text;
                dr["Status"] = "ABIERTO";

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = "";

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "ANTICIPO";
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo);
                dr["ProveedorNombre"] = "";
                dr["TipoValeDescripcion"] = "";

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                Session["PagoEnUsoAnticipo"] = LstSaldos.SelectedValue.ToString().Split('/')[0] + LstSaldos.SelectedValue.ToString().Split('/')[1];

                dtCobro.Rows.Add(dr);

                Session["idCliente"] = this.txtAntCliente.Text;
                Session["idCobroConsec"] = idConsecutivo;
            }
            else
            {
                //Genera Registro del Cobro con Cheque

                dtCobro = ds.Tables["Cobro"];

                dtCobro.Columns.Add("FechaCobro", typeof(System.DateTime));
                dtCobro.Columns.Add("NumCheque", typeof(System.String));

                DataRow dr;
                dr = dtCobro.NewRow();

                //dr["IdCobro"] = 0;
                //dr["IdPago"] = 1; //Consecutivo
                dr["IdPago"] = idConsecutivo; //Consecutivo
                dr["Referencia"] = 0;
                dr["NumeroCuenta"] = 0;

                dr["FechaCheque"] = LstSaldos.SelectedValue.ToString().Split('/')[3]; ;
                dr["Cliente"] = this.txtAntCliente.Text;
                dr["Banco"] = "0";

                dr["Importe"] = Convert.ToDouble(this.txtAntMonto.Text);
                dr["Impuesto"] = Convert.ToDouble(this.txtAntMonto.Text) * rp.dbIVA;
                // dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text)+(Convert.ToDouble(this.txtAntMonto.Text) * rp.dbIVA);
                dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text);

                dr["Saldo"] = 0;
                dr["Observaciones"] = this.txtAntOnservaciones.Text;
                dr["Status"] = "ABIERTO";

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo);
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = "";
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo);

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "ANTICIPO";
                dr["ProveedorNombre"] = "";
                dr["TipoValeDescripcion"] = "";

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                Session["PagoEnUsoAnticipo"]= LstSaldos.SelectedValue.ToString().Split('/')[0]+ LstSaldos.SelectedValue.ToString().Split('/')[1]; 

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtAntCliente.Text;         
            }

            if (ds.Tables["LiqPagoAnticipado"]!=null)
                dtLiqAnticipo = ds.Tables["LiqPagoAnticipado"];

            dtLiqAnticipo.Rows.Add(LstSaldos.SelectedValue.ToString().Split('/')[0], LstSaldos.SelectedValue.ToString().Split('/')[1], LstSaldos.SelectedValue.ToString().Split('/')[2], Convert.ToDecimal(this.txtAntMonto.Text),"",idConsecutivo);


            ActualizarSaldoPedidos();

            if (ds.Tables.Contains("LiqPagoAnticipado"))
            {
                ds.Tables.Remove("LiqPagoAnticipado");
            }
            dtLiqAnticipo.TableName = "LiqPagoAnticipado";
            ds.Tables.Add(dtLiqAnticipo);

            Session["dsLiquidacion"] = ds;
            Session["idCobroConsec"] = idConsecutivo;
            Session["FormaPago"] = "Anticipo";
            Session["SalMovto"] = 0;

            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');", true);  
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //protected void btnBuscarCliente_Click(object sender, EventArgs e)   {
    //    {


    //        Cliente _datosCliente = new Cliente(0,1);
    //    try
    //    {
    //        _datosCliente.ConsultaSaldosAFavor(Convert.ToInt32(this.txtAntCliente.Text),"",0,0);
    //        this.txtAntNombre.Text = _datosCliente.Nombre;
    //        // this.txtAntSaldo.Text = _datosCliente.Saldo.ToString();

    //        LstSaldos.DataSource = _datosCliente.SaldosCliente;
    //        LstSaldos.DataTextField = "Saldo";
    //        LstSaldos.DataValueField = "AñoMovimiento";
    //        LstSaldos.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //}

    /// <summary>
    /// Consulta los pedidos que correspondan al cliente y folio seleccionados
    /// </summary>
    private void ConsultaPedidosCliente()
    {
        int folio;
        int añoatt;

        if (Session["Folio"] != null && Session["AñoAtt"]!=null)
        {
            folio = (Int32)Session["Folio"];
            añoatt =Convert.ToInt32(Session["AñoAtt"].ToString());

            dtPedidos = rp.PedidosLiquidacion(int.Parse(txtAntCliente.Text), folio,añoatt);
            dtPedidos.TableName = "Pedidos";

            (Session["PedidosParientes"]) = dtPedidos;
        }        
    }
    
    private void ConsultaSaldos()
        {

        Cliente _datosCliente = new Cliente(0, 1);

        decimal NuevoSaldo=0;
        decimal TotalPedidos = 0;
        DataRow[] drSaldo = null;
        DataRow[] drCobro = null;
        try
        {
            dsLiq = (DataSet)(Session["dsLiquidacion"]);
            _datosCliente.ConsultaSaldosAFavor(Convert.ToInt32(this.txtAntCliente.Text),"",0,0);
            
            if (_datosCliente!=null)
            {
                if (_datosCliente.SaldosCliente != null)
                {

                    if (dsLiq != null)
                    {
                        dt = dsLiq.Tables["CobroPedido"];
                        dtLiq = dsLiq.Tables["LiqPagoAnticipado"];
                        dtCobroPedido = dsLiq.Tables["CobroPedido"];
                        dtCobroLiq = dsLiq.Tables["Cobro"];

                        foreach (DataRow dr in _datosCliente.SaldosCliente.Rows)
                        {
                            TotalPedidos = 0;
                            if (dtLiq != null)
                            {
                                drSaldo = dtLiq.Select("Folio='" + dr["FolioMovimiento"].ToString().Trim() + "' AND AñoMovimiento='" + dr["AñoMovimiento"].ToString().Trim()+"'");
                                foreach (DataRow row in drSaldo)
                                {
                                    if (dsLiq.Tables["CobroPedido"] != null)
                                    {
                                        if (dsLiq.Tables["CobroPedido"].Rows.Count > 0)
                                        {
                                            foreach (DataRow rpedido in dtCobroPedido.Rows)
                                            {
                                                //DataTable dt = new DataTable();
                                                drCobro = dtCobroLiq.Select("IDPAGO='" + rpedido["IdPago"] + "' AND NombreTipoCobro='ANTICIPO'" );
                                                //DataTable dtPagosAnticipo = drCobro.Count() > 0 ? drCobro.CopyToDataTable():null ;

                                                foreach (DataRow rcobro in drCobro)
                                                {
                                                    if (row["Pedidos"].ToString().Contains(rpedido["Pedido"].ToString()) && row["IdPago"].ToString().Trim()== rpedido["IdPago"].ToString().Trim())
                                                    {
                                                        TotalPedidos = TotalPedidos + decimal.Parse(rpedido["Total"].ToString());

                                                    }
                                                }
                                            }

                                        }
                                    }

                                    NuevoSaldo = decimal.Parse(dr["MontoSaldo"].ToString()) - TotalPedidos; ;
                                    if (NuevoSaldo > 0)
                                    {
                                        dr["Saldo"] = "$" + NuevoSaldo.ToString() + ", Año " + dr["AñoMovimiento"] + " Folio " + dr["FolioMovimiento"];
                                    }
                                    else
                                    {
                                        dr.Delete();
                                    }
                                }
                            }
                        }
                    }

                    try
                    {

                        Cliente objCliente = new Cliente(Convert.ToInt32(this.txtAntCliente.Text), 0);
                        objCliente.ConsultaNombreCliente();


                        foreach (DataRow row in _datosCliente.SaldosCliente.Rows)
                        {
                            row["Nombre"] = objCliente.Nombre;
                        }


                        this.txtAntNombre.Text = objCliente.Nombre;
                        NombreCteAnticipo = objCliente.Nombre;

                        LstSaldos.DataSource = _datosCliente.SaldosCliente;
                        LstSaldos.DataTextField = "Saldo";
                        LstSaldos.DataValueField = "Clave";
                        LstSaldos.DataBind();
                        pnlAnticipo.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Page page = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "MyScript", "alert('" + ex.Message + "');", true);
                    }

                }
            }

           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    private string consultaNombreClienteTransferencia(int ClienteID)
    {
        string NombreCliente = "";
        try
        {
            Cliente objCliente = new Cliente(ClienteID,0);
            objCliente.ConsultaNombreCliente();
            NombreCliente = objCliente.Nombre;
        }
        catch (Exception ex)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "MyScript", "alert('" + ex.Message + "');", true);
        } 
        finally
        {
            
        }
        return NombreCliente;
    }


    protected void txtNoCuenta_TextChanged(object sender, EventArgs e)
    {

    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
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
    
    protected void LstSaldos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LstSaldos.Items.Count >0)
        ClaveAnticipo = LstSaldos.SelectedValue.ToString();
    }
    
    protected void postback_Click(object sender, ImageClickEventArgs e)
    {
   
    }
    
    private void ActualizarSaldoPedidos()
    {
        DataTable dtPedidosActual;
        string pedidoReferencia, pedidoReferenciaCliente;
        decimal saldoActual;

        ConsultaPedidosCliente();
        if (ds.Tables.Contains("Pedidos") && dtPedidos != null)
        {
            // Respaldar pedidos
            dtPedidosActual = ds.Tables["Pedidos"];

            foreach (DataRow rowPedido in dtPedidosActual.Rows)
            {
                pedidoReferencia = ((string)rowPedido["PedidoReferencia"]).Trim();
                saldoActual = (decimal)rowPedido["Saldo"];

                foreach (DataRow rowPedidoCliente in dtPedidos.Rows)
                {
                    pedidoReferenciaCliente = ((string)rowPedidoCliente["PedidoReferencia"]).Trim();
                    //saldoCliente = (decimal)rowPedido["Saldo"];

                    if (pedidoReferencia == pedidoReferenciaCliente)
                    {
                        rowPedidoCliente.BeginEdit();
                        rowPedidoCliente["Saldo"] = saldoActual;
                        rowPedidoCliente.EndEdit();
                        break;
                    }
                }
            }

            ds.Tables.Remove("Pedidos");
            ds.Tables.Add(dtPedidos);
        }
    }

    protected void ddlBancoDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        PostBack = "CuentasBancarias";
    }

    protected void TxtCtaOrigen_TextChanged(object sender, EventArgs e)
    {
        //CargaCadenaConexion();
        //HiddenCtaOrigenValida.Value = Ctaorigen.validarExpresionRegular(3, TxtCtaOrigen.Text, conexion).ToString();
    }
}
