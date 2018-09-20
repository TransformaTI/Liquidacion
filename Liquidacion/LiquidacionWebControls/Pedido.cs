// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.Pedido
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using SigametLiquidacion;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace SigametLiquidacion.WebControls
{
    [ToolboxData("<{0}:Pedido runat=server></{0}:Pedido>")]
    [DefaultProperty("Text")]
    public class Pedido : CompositeControl
    {
        private TipoOperacionPedido _tipoOperacionCaptura = TipoOperacionPedido.CapturaNuevoPedido;
        private TipoLayoutControl _tipoLayOutControl = TipoLayoutControl.LayoutHorizontal;
        private LinkButton lblTagNumeroCliente = new LinkButton();
        private TextBox txtNumeroCliente = new TextBox();
        private ImageButton btnConsultaCliente = new ImageButton();
        private LinkButton lnkCambiarCliente = new LinkButton();
        private Label lblTagNombreCliente = new Label();
        private Label lblNombreCliente = new Label();
        private Label lblTagDireccionCliente = new Label();
        private Label lblDireccionCliente = new Label();
        private Label lblCelulaRuta = new Label();
        private Label lblMensajeRutaDiferente = new Label();
        private Label lblDatosCredito = new Label();
        private Label lblTagCarteraCredito = new Label();
        private Label lblCarteraCredito = new Label();
        private Label lblTagLimiteDeCredito = new Label();
        private Label lblLimiteDeCredito = new Label();
        private Label lblMensajeLimiteCredito = new Label();
        private Label lblMensajeConciliacion = new Label();
        private ImageButton imgDecoration = new ImageButton();
        private Label lblDatosPedido = new Label();
        private Label lblTagNumeroPedido = new Label();
        private Label lblNumeroPedido = new Label();
        private Label lblTipoPedido = new Label();
        private Label lblTagLitros = new Label();
        private TextBox txtLitros = new TextBox();
        private Label lblTagPrecio = new Label();
        private DropDownList ddpPrecio = new DropDownList();
        private Label lblTagImporte = new Label();
        private TextBox txtImporte = new TextBox();
        private Label lblTagIva = new Label();
        private DropDownList ddpPorcentajeIva = new DropDownList();
        private Label lblIva = new Label();
        private Label lblTagFormaPago = new Label();
        private DropDownList ddpFormaPago = new DropDownList();
        private Label lblTagDescuento = new Label();
        private Label lblDescuento = new Label();
        private Label lblTagNumeroRemision = new Label();
        private TextBox txtNumeroRemision = new TextBox();
        private Label lblTagValorDescuento = new Label();
        private Label lblValorDescuento = new Label();
        private ImageButton btnAceptar = new ImageButton();
        private ImageButton btnCancelar = new ImageButton();
        private ImageButton btnRemover = new ImageButton();
        private string _mensajeCapturaCliente = "Debe proporcionar el número de contrato.";
        private string _mensajeCapturaLitros = "Debe capturar la cantidad de litros surtidos.";
        private string _imagenBotonBusquedaCliente;
        private byte _claveCreditoAutorizado;
        private Parametros _parametros;
        private DateTime _fechaSuministro;
        private short _ruta;
        private short _celula;
        private short _añoAtt;
        private int _folio;
        private int _autoTanque;
        private DataTable dtListaPrecios;
        private DataTable dtListaFormasPago;
        private DataTable dtListaTiposCobro;
        private DataTable dtListaTipoPedido;
        private int _sourceRow;
        private DataRow _editingRow;
        private ParametrosLiquidacion _LIQparams;
        private int _origenInformacion;
        private int _consecutivoOrigen;
        private bool _errorDeConsistencia;
        private string _serieRemisionRuta;
        private string _usuario;
        private bool _validarRemisionLiquidada;
        private bool _validarRemisionExistente;
        private bool _permitirCaptura;
        private SigametLiquidacion.Cliente _cliente;
        private SigametLiquidacion.Pedido _pedido;
        private Decimal _precioMaximo;
        private Decimal _precioMinimo;
        private Decimal _descuento;
        private string _imageButtonEnviarURL;


        private byte _longitudSerie;
        private byte _longitudRemision;
        
        public string URLImagenBotonBusquedaCliente
        {
            get
            {
                return this._imagenBotonBusquedaCliente;
            }
            set
            {
                this._imagenBotonBusquedaCliente = value;
            }
        }
        
        public byte ClaveCreditoAutorizado
        {
            get
            {
                return this._claveCreditoAutorizado;
            }
            set
            {
                this._claveCreditoAutorizado = value;
            }
        }
        
        public short Ruta
        {
            get
            {
                return this._ruta;
            }
            set
            {
                this._ruta = value;
            }
        }
        
        public short Celula
        {
            get
            {
                return this._celula;
            }
            set
            {
                this._celula = value;
            }
        }
        
        public short CelulaPedido
        {
            get
            {
                if (this._pedido != null)
                {
                    return this._pedido.Celula;
                }
                return (short)0;
            }
        }
        
        public short AñoPedido
        {
            get
            {
                if (this._pedido != null)
                {
                    return this._pedido.AñoPed;
                }
                return (short) 0;
            }
        }
        
        public int NumeroPedido
        {
            get
            {
                if (this._pedido != null)
                {
                    return this._pedido.NumeroPedido;
                }
                return 0;
            }
        }
        
        public short AñoAtt
        {
            get
            {
                return this._añoAtt;
            }
            set
            {
                this._añoAtt = value;
            }
        }
        
        public int Folio
        {
            get
            {
                return this._folio;
            }
            set
            {
                this._folio = value;
            }
        }
        
        public int AutoTanque
        {
            get
            {
                return this._autoTanque;
            }
            set
            {
                this._autoTanque = value;
            }
        }
        
        public DateTime FechaSuministro
        {
            get
            {
                return this._fechaSuministro;
            }
            set
            {
                this._fechaSuministro = value;
            }
        }
        
        public DataTable ListaFormasPago
        {
            get
            {
                return this.dtListaFormasPago;
            }
            set
            {
                this.dtListaFormasPago = value;
            }
        }
        
        public DataTable ListaTiposCobro
        {
            get
            {
                return this.dtListaTiposCobro;
            }
            set
            {
                this.dtListaTiposCobro = value;
            }
        }
        
        public DataTable ListaPrecios
        {
            set
            {
                this.dtListaPrecios = value;
            }
        }
        
        public DataTable ListaTipoPedido
        {
            set
            {
                this.dtListaTipoPedido = value;
            }
        }
        
        public string PedidoReferencia
        {
            get
            {
                return this._pedido.PedidoReferencia;
            }
        }
        
        public string FolioRemision
        {
            get
            {
                return this.txtNumeroRemision.Text;
            }
        }
        
        public int Cliente
        {
            get
            {
                return this._cliente.NumeroCliente;
            }
        }
        
        public string Nombre
        {
            get
            {
                return this._cliente.Nombre;
            }
        }
        
        public double Litros
        {
            get
            {
                return this._pedido.Litros;
            }
        }
        
        public Decimal Precio
        {
            get
            {
                return this._pedido.Precio;
            }
        }
        
        public Decimal Importe
        {
            get
            {
                return this._pedido.Importe;
            }
        }
        
        public byte FormaPago
        {
            get
            {
                return this._pedido.FormaPago;
            }
        }
        
        public byte TipoPedido
        {
            get
            {
                return this._pedido.TipoPedido;
            }
        }
        
        public TipoOperacionPedido TipoOperacion
        {
            get
            {
                return this._tipoOperacionCaptura;
            }
        }
        
        public int SourceRow
        {
            get
            {
                return this._sourceRow;
            }
        }
        
        public TipoLayoutControl LayOutControl
        {
            get
            {
                return this._tipoLayOutControl;
            }
            set
            {
                this._tipoLayOutControl = value;
            }
        }
        
        public ParametrosLiquidacion ParametrosRuta
        {
            set
            {
                this._LIQparams = value;
            }
        }
        
        public bool PermitirCaptura
        {
            get
            {
                return this._permitirCaptura;
            }
            set
            {
                this._permitirCaptura = value;
            }
        }
        
        public string SerieRemisionRuta
        {
            set
            {
                this._serieRemisionRuta = value;
            }
        }
        
        public Parametros Parametros
        {
            set
            {
                this._parametros = value;
            }
        }
        
        public string Usuario
        {
            set
            {
                this._usuario = value;
            }
        }
        
        public bool ValidarRemisionCapturada
        {
            get
            {
                return this._validarRemisionLiquidada;
            }
            set
            {
                this._validarRemisionLiquidada = value;
            }
        }
        
        public bool ValidarRemisionExistente
        {
            get
            {
                return this._validarRemisionExistente;
            }
            set
            {
                this._validarRemisionExistente = value;
            }
        }
        
        public Decimal Descuento
        {
            get
            {
                return this._descuento;
            }
            set
            {
                this._descuento = value;
            }
        }
        
        public byte LongitudRemision
        {
            get
            {
                return _longitudRemision;
            }
            set
            {
                _longitudRemision = value;
            }
        }
        
        public byte LongitudSerie
        {
            get
            {
                return _longitudSerie;
            }
            set
            {
                _longitudSerie = value;
            }
        }

        public string ImageButtonEnviarURL
        {
            get
            {
                return this._imageButtonEnviarURL;
            }
            set
            {
                this._imageButtonEnviarURL = value;
            }
        }

        private bool consultaCteOnChange;

        public bool ConsultaCteOnChange
        {
            get { return consultaCteOnChange; }
            set { consultaCteOnChange = value; }
        }


        public event EventHandler ClickAceptar;
        
        public event EventHandler ClickCancelar;
        
        public event EventHandler DesasignarPedido;
        
        public event EventHandler CambiarClientePedidoLiquidado;
        
        public event EventHandler ActualizarCliente;
        
        public event EventHandler Error;
        
        protected virtual void OnClick(EventArgs e)
        {
            if (this.ClickAceptar == null)
            {
                return;
            }
            this.ClickAceptar((object) this, e);
        }
        
        protected virtual void OnClickCancelar(EventArgs e)
        {
            if (this.ClickCancelar == null)
            {
                return; 
            }
            this.ClickCancelar((object) this, e);
        }
        
        protected virtual void OnDesasignar(EventArgs e)
        {
            if (this.DesasignarPedido == null)
            {
                return;
            }
            this.DesasignarPedido((object) this, e);
        }
        
        protected virtual void OnCambiarClientePedidoLiquidado(EventArgs e)
        {
            if (this.CambiarClientePedidoLiquidado == null)
            {
                return;
            }
            this.CambiarClientePedidoLiquidado((object) this, e);
        }
        
        protected virtual void OnActualizarCliente(EventArgs e)
        {
            if (this.ActualizarCliente == null)
            {
                return;
            }
            this.ActualizarCliente((object) this, e);
        }
        
        protected virtual void OnError(EventArgs e)
        {
            if (this.Error == null)
            {
                return;
            }
            this.Error((object)this, e);
        }
        
        protected override object SaveViewState()
        {
            this.EnsureChildControls();
            object[] objArray = new object[23];
            object obj = base.SaveViewState();
            objArray[0] = obj;
            objArray[1] = (object) this._claveCreditoAutorizado;
            objArray[2] = (object) this.dtListaPrecios;
            objArray[3] = (object) this.dtListaFormasPago;
            objArray[4] = (object) this.dtListaTiposCobro;
            objArray[5] = (object) this._LIQparams;
            objArray[6] = (object) this._parametros;
            //objArray[7] = (object) (bool) (this._permitirCaptura ? 1 : 0);
            objArray[7] = (object)(bool)(this._permitirCaptura ? true : false);
            objArray[8] = (object) this._origenInformacion;
            objArray[9] = (object) this.dtListaTipoPedido;
            objArray[10] = (object) this._consecutivoOrigen;
            objArray[11] = (object) this._pedido;
            objArray[12] = (object) this._cliente;
            objArray[13] = (object) this._tipoOperacionCaptura;
            objArray[14] = (object) this._sourceRow;
            objArray[15] = (object) this._serieRemisionRuta;
            objArray[16] = (object) this._autoTanque;
            objArray[17] = (object) this._usuario;
            objArray[18] = (object) this._descuento;
            //objArray[19] = (object) (bool) (this._validarRemisionExistente ? 1 : 0);
            objArray[19] = (object)(bool)(this._validarRemisionExistente ? true : false);
            //objArray[20] = (object) (bool) (this._validarRemisionLiquidada ? 1 : 0);
            objArray[20] = (object)(bool)(this._validarRemisionLiquidada ? true : false);
            objArray[21] = this._longitudRemision;
            objArray[22] = this._longitudSerie;
            return (object) objArray;
        }
        
        protected override void LoadViewState(object savedState)
        {
            object[] objArray = (object[]) savedState;
            base.LoadViewState(objArray[0]);
            this._claveCreditoAutorizado = (byte) objArray[1];
            this.dtListaPrecios = (DataTable) objArray[2];
            this.dtListaFormasPago = (DataTable) objArray[3];
            this.dtListaTiposCobro = (DataTable) objArray[4];
            this._LIQparams = (ParametrosLiquidacion) objArray[5];
            this._parametros = (Parametros) objArray[6];
            this._permitirCaptura = (bool) objArray[7];
            this._origenInformacion = (int) objArray[8];
            this.dtListaTipoPedido = (DataTable) objArray[9];
            this._consecutivoOrigen = (int) objArray[10];
            this._pedido = (SigametLiquidacion.Pedido) objArray[11];
            this._cliente = (SigametLiquidacion.Cliente) objArray[12];
            this._tipoOperacionCaptura = (TipoOperacionPedido) objArray[13];
            this._sourceRow = (int) objArray[14];
            this._serieRemisionRuta = (string) objArray[15];
            this._autoTanque = (int) objArray[16];
            this._usuario = (string) objArray[17];
            this._descuento = (Decimal) objArray[18];
            this._validarRemisionExistente = (bool) objArray[19];
            this._validarRemisionLiquidada = (bool) objArray[20];
            this._fechaSuministro = this._LIQparams.Fecha;
            this._añoAtt = this._LIQparams.AñoAtt;
            this._folio = this._LIQparams.Folio;
            this._ruta = this._LIQparams.Ruta;
            this._celula = this._LIQparams.Celula;
            this._longitudRemision = (byte)objArray[21];
            this._longitudSerie = (byte)objArray[22];
            this.EnsureChildControls();
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
        
        public void ConsultaDetallePedido(TipoOperacionPedido TipoOperacion, DataRow ExistingRow)
        {
            this._editingRow = ExistingRow;
            this._sourceRow = (int) ExistingRow["ID"];
            this.clearInfo();
            switch (Convert.ToString(ExistingRow["Status"]).ToUpper().Trim())
            {
                case "CONCILIADO":
                    this._pedido = new SigametLiquidacion.Pedido((short) ExistingRow["Celula"], (short) ExistingRow["Añoped"], (int) ExistingRow["Pedido"]);
                    this._tipoOperacionCaptura = TipoOperacionPedido.EdicionPedidoConciliado;
                    if (ExistingRow["Origen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._origenInformacion = Convert.ToInt32(ExistingRow["Origen"]);
                    }
                    if (ExistingRow["ConsecutivoOrigen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._consecutivoOrigen = Convert.ToInt32(ExistingRow["ConsecutivoOrigen"]);
                    }
                    this.asignarDatosClientePedidoLiquidado(this._pedido.Cliente);
                    this.asignarDatosPedidoLiquidado();
                    this.lnkCambiarCliente.Visible = true;
                    break;
                case "PENDIENTE":
                    this._tipoOperacionCaptura = TipoOperacionPedido.EdicionNuevoPedido;
                    if (ExistingRow["Origen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._origenInformacion = Convert.ToInt32(ExistingRow["Origen"]);
                    }
                    if (ExistingRow["ConsecutivoOrigen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._consecutivoOrigen = Convert.ToInt32(ExistingRow["ConsecutivoOrigen"]);
                    }
                    this.txtNumeroCliente.Text = string.Empty;
                    this.txtNumeroCliente.Enabled = true;
                    this.txtLitros.Text = string.Empty;
                    this.txtNumeroRemision.Text = (string) ExistingRow["FolioRemision"];
                    this.mostrarDatos((double) ExistingRow["Litros"], (Decimal) ExistingRow["Precio"], (byte) ExistingRow["FormaPago"], (Decimal) ExistingRow["Importe"]);
                    break;
                case "ERROR":
                    this._tipoOperacionCaptura = TipoOperacionPedido.EdicionPedidoInconsistente;
                    this._errorDeConsistencia = true;
                    if (ExistingRow["Origen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._origenInformacion = Convert.ToInt32(ExistingRow["Origen"]);
                    }
                    if (ExistingRow["ConsecutivoOrigen"] != null && ExistingRow["Origen"] != DBNull.Value)
                    {
                        this._consecutivoOrigen = Convert.ToInt32(ExistingRow["ConsecutivoOrigen"]);
                    }
                    this.asignarDatosClientePedidoLiquidado(Convert.ToInt32(ExistingRow["Cliente"]));
                    this.lblMensajeConciliacion.Text = Convert.ToString(ExistingRow["ObservacionesConciliacion"]);
                    double Litros = (double) ExistingRow["Litros"];
                    byte FormaPago = (byte) ExistingRow["FormaPago"];
                    Decimal Importe = (Decimal) ExistingRow["Importe"];
                    if (!this._cliente.CreditoAutorizado)
                    {
                        this.initComboFormasPago();
                        this.formaPagoContado();
                        FormaPago = (byte) 5;
                    }
                    Decimal Precio = (Decimal) ExistingRow["Precio"];
                    if (Precio == ControlDeDescuento.Instance.PrecioValido(this.dtListaPrecios, Precio))
                    {
                        Decimal num = ControlDeDescuento.Instance.PrecioAutorizado(this.dtListaPrecios, this._cliente.Descuento, this._cliente.ZonaEconomica);
                        if (Precio < num)
                        {
                            Precio = ControlDeDescuento.Instance.PrecioAutorizado(this.dtListaPrecios, new Decimal(0), this._cliente.ZonaEconomica);
                        }
                    }
                    else
                    {
                        Precio = ControlDeDescuento.Instance.PrecioValido(this.dtListaPrecios, Precio);
                    }
                    this.mostrarDatos(Litros, Precio, FormaPago, Importe);
                    this.lnkCambiarCliente.Visible = true;
                    break;
            }
            if (this._permitirCaptura)
            {
 
                return;
            }
            this.enableCaptureControls(false);
        }
        
        private void asignarDatosPedidoLiquidado()
        {
            this.publicarPedido();
            this.mostrarDatos(this._pedido.Litros, this._pedido.Precio, this._pedido.FormaPago, this._pedido.Importe);
        }
        
        private void mostrarDatos(double Litros, Decimal Precio, byte FormaPago, Decimal Importe)
        {
            this.txtLitros.Text = Litros.ToString();

            //22-07-2015 - Selección del precio de acuerdo a la zona económica del cliente
            bool precioEncontrado = false;
            foreach (ListItem precio in this.ddpPrecio.Items)
            {
                if (Convert.ToDecimal(precio.Value) == Precio)
                {
                    precioEncontrado = true;
                    break;
                }
            }
            if (!precioEncontrado)
            {
                this.ddpPrecio.Items.Add(new ListItem(string.Format("{0:0.0000}", Precio), string.Format("{0:0.0000}", Precio)));
            }

            this.ddpPrecio.SelectedValue = Precio.ToString("#.0000");
            this.asignaFormaDePagoPedidoLiquidado(FormaPago);
            this.txtImporte.Text = Importe.ToString("#.0000");
        }

        private void asignaFormaDePagoPedidoLiquidado(byte FormaPago)
        {
            this.initComboFormasPago();
            foreach (ListItem listItem in this.ddpFormaPago.Items)
            {
                if (listItem.Value.Trim().ToUpper() == Convert.ToString(this.dtListaTiposCobro.Rows.Find((object) FormaPago)["TipoPago"]).Trim().ToUpper())
                {
                    listItem.Selected = true;
                    break;
                }
            }
        }
        
        private void asignarDatosClientePedidoLiquidado(int Cliente)
        {
            this.txtNumeroCliente.Text = Cliente.ToString();
            this.btnConsultaCliente_Click((object) null, (ImageClickEventArgs) null);
        }
        
        protected override void CreateChildControls()
        {
            switch (this._tipoLayOutControl)
            {
                case TipoLayoutControl.LayoutHorizontal:
                    this.horizontalLayOut();
                    break;
                case TipoLayoutControl.LayoutVertical:
                    this.verticalLayOut();
                    break;
            }
            this.btnConsultaCliente.Click += new ImageClickEventHandler(this.btnConsultaCliente_Click);
            this.btnAceptar.Click += new ImageClickEventHandler(this.btnAceptar_Click);
            this.btnCancelar.Click += new ImageClickEventHandler(this.btnCancelar_Click);
            this.btnRemover.Click += new ImageClickEventHandler(this.btnDesasignar_Click);
            this.txtNumeroCliente.TextChanged += new EventHandler(txtNumeroCliente_TextChanged);
            this.ConfiguracionTabOrder();
        }
        
        private void generalInfoSection()
        {
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td colspan='2'>"));
            this.Controls.Add((Control) this.lblMensajeConciliacion);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagNumeroCliente.Enabled = false;
            this.lblTagNumeroCliente.Text = "Cliente:";
            this.lblTagNumeroCliente.Click += new EventHandler(this.lblTagNumeroCliente_Click);
            this.Controls.Add((Control) this.lblTagNumeroCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.Controls.Add((Control) new LiteralControl("<div style='vertical-align:middle'>"));

            this.txtNumeroCliente.Attributes.Add("onfocus", "SetSelected(" + this.txtNumeroCliente.ClientID + ");");
            this.txtNumeroCliente.CssClass = "ClientTextBox";
            
            //if (ConsultaCteOnChange==true)
            //{
                this.txtNumeroCliente.Attributes.Add("onchange", "return DoPostback();");
            //}

            this.Controls.Add((Control) this.txtNumeroCliente);

            this.btnConsultaCliente.Attributes.Add("onclick", "return doNumeroClienteSubmit(" + '\'' + this.txtNumeroCliente.ClientID + '\'' + "," + '\'' + this._mensajeCapturaCliente + '\'' + ");");
            this.btnConsultaCliente.SkinID = "btnBuscarCliente";
            this.btnConsultaCliente.AlternateText = "BUSCAR";
            this.btnConsultaCliente.ID = "btnConsultaCliente";
            this.Controls.Add((Control) this.btnConsultaCliente);
            this.txtNumeroCliente.Attributes.Add("onkeypress", "return keyPressNumeroCliente(event, " + '\'' + this.txtNumeroCliente.ClientID + '\'' + ", " + '\'' + this.btnConsultaCliente.ClientID + '\'' + ");");
            this.Controls.Add((Control) new LiteralControl("</div>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td style=" + '\'' + "font-weight: bold;" + '\'' + ">"));
            this.lnkCambiarCliente.Text = "Cambiar cliente";
            this.lnkCambiarCliente.Visible = false;
            this.lnkCambiarCliente.Attributes.Add("onclick", "return doNumeroClienteSubmit(" + '\'' + this.txtNumeroCliente.ClientID + '\'' + "," + '\'' + this._mensajeCapturaCliente + '\'' + ");");
            this.lnkCambiarCliente.Click += new EventHandler(this.lnkCambiarCliente_click);
            this.Controls.Add((Control) this.lnkCambiarCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td style=" + '\'' + "font-weight: bold;" + '\'' + ">"));
            this.Controls.Add((Control) this.lblCelulaRuta);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='WarningLabels'>"));
            this.Controls.Add((Control) this.lblMensajeRutaDiferente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagNombreCliente.Text = "Nombre:";
            this.Controls.Add((Control) this.lblTagNombreCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblNombreCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagDireccionCliente.Text = "Dirección:";
            this.Controls.Add((Control) this.lblTagDireccionCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblDireccionCliente);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td colspan=2 class='LocalHeaders'>"));
            this.lblDatosCredito.Text = "Información de crédito del cliente";
            this.Controls.Add((Control) this.lblDatosCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));      
            this.lblTagCarteraCredito.Text = "Autorización:";      
            this.Controls.Add((Control) this.lblTagCarteraCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblCarteraCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.Controls.Add((Control) this.lblTagLimiteDeCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblLimiteDeCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblMensajeLimiteCredito);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagValorDescuento.Text = "Descuento:";
            this.Controls.Add((Control) this.lblTagValorDescuento);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='MiscFields'>"));
            this.Controls.Add((Control) this.lblValorDescuento);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td colspan=2 class='LocalHeaders'>"));
            this.lblDatosPedido.Text = "Información del pedido";
            this.Controls.Add((Control) this.lblDatosPedido);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagNumeroPedido.Text = "Pedido:";
            this.Controls.Add((Control) this.lblTagNumeroPedido);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='FieldNumeroPedido'>"));
            this.Controls.Add((Control) this.lblNumeroPedido);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            if (this._parametros == null || !Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("CapturaRemision"))))
            {
                return;
            }
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td class='TagLabel'>"));
            this.lblTagNumeroRemision.Text = "Remisión:";
            this.Controls.Add((Control) this.lblTagNumeroRemision);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td class='FieldNumeroPedido'>"));
            this.Controls.Add((Control) this.txtNumeroRemision);
            this.txtNumeroRemision.Attributes.Add("onfocus", "SetSelected(" + this.txtNumeroRemision.ClientID + ");");
            //this.txtNumeroRemision.Attributes.Add("onkeyup", "TextToUpper(" + '\'' + this.txtNumeroRemision.UniqueID + '\'' + ");");
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
        }
        
        private void lblTagNumeroCliente_Click(object sender, EventArgs e)
        {
            this.OnActualizarCliente(e);
        }
        
        private void escribirEtiquetaLitros(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.lblTagLitros.CssClass = CssClass;
            this.lblTagLitros.Text = "Litros:";
            this.Controls.Add((Control) this.lblTagLitros);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void escribirEtiquetaPrecio(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.lblTagPrecio.CssClass = CssClass;
            this.lblTagPrecio.Text = "Precio:";
            this.Controls.Add((Control) this.lblTagPrecio);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void escribirEtiquetaImporte(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.lblTagImporte.CssClass = CssClass;
            this.lblTagImporte.Text = "Importe:";
            this.Controls.Add((Control) this.lblTagImporte);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void escribirEtiquetaFormaPago(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.lblTagFormaPago.CssClass = CssClass;
            this.lblTagFormaPago.Text = "Forma de pago:";
            this.Controls.Add((Control) this.lblTagFormaPago);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void controlCapturaLitros(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.txtLitros.CssClass = CssClass;
            this.txtLitros.Text = "0";
            this.Controls.Add((Control) this.txtLitros);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
            if (this._parametros == null || !Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("CapturaRemision"))))
            {
                return;
            }
            this.txtNumeroRemision.Attributes.Add("onkeypress", "return validarRemision(" + '\'' + this._serieRemisionRuta.Trim().ToUpper() + '\'' + ", " + '\'' + this.txtNumeroRemision.UniqueID + '\'' + ", " + Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("ValidarSerieRemision"))).ToString().ToLower() + ", event, " + '\'' + this.txtLitros.ClientID + '\'' + ");");
        }
        
        private void controlCapturaPrecio(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.ddpPrecio.CssClass = CssClass;
            if (this.dtListaPrecios != null)
            {
                this.ddpPrecio.Dispose();
                this.ddpPrecio.Items.Clear();
                this.ddpPrecio.DataSource = (object) this.dtListaPrecios;
                this.ddpPrecio.DataValueField = "Precio";
                this.ddpPrecio.DataTextField = "Precio";
                this.ddpPrecio.DataBind();
            }
            this.Controls.Add((Control) this.ddpPrecio);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void controlCapturaImporte(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.txtImporte.Enabled = false;
            this.txtImporte.ReadOnly = true;
            this.txtImporte.CssClass = CssClass;
            this.txtImporte.Text = "0";
            this.Controls.Add((Control) this.txtImporte);
            this.txtLitros.Attributes.Add("onchange", "return calcularImporte(" + '\'' + this.txtLitros.ClientID + '\'' + "," + '\'' + this.ddpPrecio.ClientID + '\'' + "," + '\'' + this.txtImporte.ClientID + '\'' + ");");
            this.ddpPrecio.Attributes.Add("onchange", "return calcularImporte(" + '\'' + this.txtLitros.ClientID + '\'' + "," + '\'' + this.ddpPrecio.ClientID + '\'' + "," + '\'' + this.txtImporte.ClientID + '\'' + ");");
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void controlCapturaFormaPago(string CssClass)
        {
            this.Controls.Add((Control) new LiteralControl("<TD>"));
            this.ddpFormaPago.CssClass = CssClass;
            if (this.dtListaFormasPago != null)
            {
                this.ddpFormaPago.DataSource = (object) this.dtListaFormasPago;
                this.ddpFormaPago.DataValueField = "TipoPago";
                this.ddpFormaPago.DataTextField = "TipoPago";
                this.ddpFormaPago.DataBind();
            }
            this.Controls.Add((Control) this.ddpFormaPago);
            this.Controls.Add((Control) new LiteralControl("</TD>"));
        }
        
        private void controlAceptarCancelar(string CssClass)
        {
            this.btnAceptar.Visible = this._permitirCaptura;
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.btnAceptar.Enabled = false;
            this.btnAceptar.AlternateText = "ACEPTAR";
            this.btnAceptar.SkinID = "btnAceptar";
            this.Controls.Add((Control) this.btnAceptar);
            if (this._parametros != null)
            {
                this.btnAceptar.Attributes.Add("onclick", "return validacionCamposRequeridos(" + '\'' + this.txtLitros.ClientID + '\'' + "," + '\'' + this._mensajeCapturaLitros + '\'' + ", " + '\'' + this.btnAceptar.ClientID + '\'' + ", " + Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("CapturaRemision"))).ToString().ToLower() + ", " + '\'' + this.txtNumeroRemision.ClientID + '\'' + ", " + '\'' + "Debe capturar el número de remisión." + '\'' + ", " + '\'' + this.imgDecoration.ClientID + '\'' + ");");
            }
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.btnCancelar.AlternateText = "CANCELAR";
            this.btnCancelar.SkinID = "btnCancelar";
            this.Controls.Add((Control) this.btnCancelar);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.btnRemover.Visible = this._permitirCaptura;
            this.Controls.Add((Control) new LiteralControl("<td>"));
            this.btnRemover.AlternateText = "REMOVER";
            this.btnRemover.SkinID = "btnRemover";
            this.Controls.Add((Control) this.btnRemover);
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.btnRemover.Attributes.Add("onclick", "return confirm('¿Desea Eliminar el Pedido?')");
            this.txtLitros.Attributes.Add("onkeypress", "return ComboKeyPress(event, " + '\'' + this.ddpFormaPago.ClientID + '\'' + ", " + '\'' + this.btnAceptar.UniqueID + '\'' + ");");
            this.ddpFormaPago.Attributes.Add("onkeypress", "return ComboKeyPress(event, " + '\'' + this.btnAceptar.UniqueID + '\'' + ", " + '\'' + this.btnAceptar.UniqueID + '\'' + ");");
        }
        
        private void controlRemover(string CssClass)
        {
        }
        
        private void horizontalLayOut()
        {
            this.Controls.Add((Control) new LiteralControl("<table class='TableStyle'>"));
            this.generalInfoSection();
            this.Controls.Add((Control) new LiteralControl("<td colspan='2'>"));
            this.Controls.Add((Control) new LiteralControl("<table class='SubDetailBackGround'>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.escribirEtiquetaLitros("TagLabel HorizontalLayoutCapturable");
            this.escribirEtiquetaPrecio("TagLabel HorizontalLayoutCapturable");
            this.escribirEtiquetaImporte("TagLabel HorizontalLayoutCapturable");
            this.escribirEtiquetaFormaPago("TagLabel HorizontalLayoutCapturable");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.controlCapturaLitros("HorizontalLayoutCapturable");
            this.txtLitros.Attributes.Add("onkeypress", "return onKeyPress_OnlyDecimalDigits(event," + '\'' + this.txtLitros.ClientID + '\'' + ", " + '\'' + this.ddpPrecio.ClientID + '\'' + ");");
            this.txtLitros.Attributes.Add("onfocus", "SetSelected(" + this.txtLitros.ClientID + ");");
            this.controlCapturaPrecio("HorizontalLayoutCapturable");
            this.controlCapturaImporte("HorizontalLayoutCapturable");
            this.controlCapturaFormaPago("HorizontalLayoutCapturable");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</table>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td colspan='2' align='left'>"));
            this.Controls.Add((Control) new LiteralControl("<table>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.controlAceptarCancelar("");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.controlRemover("");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</table>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</table>"));
        }
        
        private void verticalLayOut()
        {
            this.Controls.Add((Control) new LiteralControl("<table class='TableStyle'>"));
            this.generalInfoSection();
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.escribirEtiquetaLitros("TagFieldLabels");
            this.controlCapturaLitros("MediumNumericText");
            this.imgDecoration.Width = (Unit) 20;
            this.imgDecoration.Width = (Unit) 20;
            this.imgDecoration.ImageUrl = this._imageButtonEnviarURL;            
            this.imgDecoration.Attributes.Add("onkeypress", "return btnDecorationUnSubmit();");
            this.Controls.Add((Control) this.imgDecoration);
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.escribirEtiquetaPrecio("TagFieldLabels");
            this.controlCapturaPrecio("MediumNumericText");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.txtLitros.Attributes.Add("onkeypress", "return onKeyPress_OnlyDecimalDigits(event," + '\'' + this.txtLitros.UniqueID + '\'' + ", " + '\'' + this.ddpPrecio.UniqueID + '\'' + ");");
            this.txtLitros.Attributes.Add("onfocus", "SetSelected(" + this.txtLitros.ClientID + ");");
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.escribirEtiquetaImporte("TagFieldLabels");
            this.controlCapturaImporte("MediumNumericText");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.escribirEtiquetaFormaPago("TagFieldLabels");
            this.controlCapturaFormaPago("MediumNumericText");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.Controls.Add((Control) new LiteralControl("<td colspan='2' align='left'>"));
            this.Controls.Add((Control) new LiteralControl("<table width='100%'>"));      
            this.controlAceptarCancelar("");
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            this.controlRemover("");
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</table>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.Controls.Add((Control) new LiteralControl("</table>"));
        }
        
        public void ConfiguracionTabOrder()
        {
            this.txtNumeroCliente.TabIndex = (short) 0;
            this.btnConsultaCliente.TabIndex = (short) 1;
            this.txtNumeroRemision.TabIndex = (short) 2;
            this.txtLitros.TabIndex = (short) 3;
            this.ddpPrecio.TabIndex = (short) 4;
            this.ddpFormaPago.TabIndex = (short) 5;
            this.btnAceptar.TabIndex = (short) 6;
            this.btnCancelar.TabIndex = (short) 7;
            this.btnRemover.TabIndex = (short) 8;
        }
        
        private void btnConsultaCliente_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtNumeroCliente.Text.Length <= 0)
            {
                return;
            }

            this.cargaDatosCliente(Convert.ToInt32(this.txtNumeroCliente.Text), sender, e);

        }

        protected void txtNumeroCliente_TextChanged(object sender, EventArgs e)
        {
            
            
            if (this.txtNumeroCliente.Text.Length <= 0)
            {
                return;
            }
            this.cargaDatosCliente(Convert.ToInt32(this.txtNumeroCliente.Text), sender, e);
        }

        private void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this._pedido != null)
                {
                    this._pedido.ReinitDataComp();
                }

            }
            catch (Exception ex)
            {
                this.lblMensajeConciliacion.Text = "Error " + ex.Message;
                return;
            }
            if (this._tipoOperacionCaptura != TipoOperacionPedido.EdicionPedidoConciliado)
            {
                try
                {
                    this._pedido.ConsultaPedido();
                }
                catch (Exception ex)
                {
                    this.lblMensajeConciliacion.Text = "Error " + ex.Message;
                    return;
                }
                if (this._pedido.Status.ToUpper().Trim() != "ACTIVO")
                {
                    this.lblMensajeConciliacion.Text = "Este pedido ya no está activo.";
                    this.Error((object) this, (EventArgs) e);
                    return;
                }
            }
            this._pedido.AñoAtt = this._añoAtt;
            this._pedido.FolioAtt = this._folio;
            this._pedido.Litros = Convert.ToDouble(this.txtLitros.Text);
            this._pedido.FechaSuministro = this._fechaSuministro;
            this._pedido.Precio = Convert.ToDecimal(this.ddpPrecio.SelectedValue);
            this._pedido.RutaSuministro = this._ruta;
            this._pedido.AutoTanque = this._autoTanque;
            if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LimiteCreditoOperador"))))
            {
                if ((int) this.asignarFormaPago() == 9)
                {
                    try
                    {
                        if (this._pedido.LimiteDisponibleOperador(this._añoAtt, this._folio) - this._pedido.Importe < new Decimal(0))
                        {
                            this.lblMensajeConciliacion.Text = "Límite de crédito del operador excedido, liquide de contado";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.lblMensajeConciliacion.Text = "Error " + ex.Message;
                        this.Error((object) this, (EventArgs) e);
                        return;
                    }
                }
            }
            if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("CapturaRemision"))))
            {
                this._pedido.TipoLiquidacion = "S";
                try
                {
                    this._pedido.SepararFolioRemision(this.txtNumeroRemision.Text, this.LongitudSerie);
                }
                catch (Exception ex)
                {
                    this.lblMensajeConciliacion.Text = ex.Message;
                    return;
                }
            }
            this._pedido.ConsecutivoOrigen = this._consecutivoOrigen;
            this._pedido.TipoDescarga = (TipoOperacionDescarga) this._origenInformacion;
            this._pedido.FormaPago = this.asignarFormaPago();
            this.txtImporte.Text = this._pedido.Importe.ToString();
            this.btnAceptar.Enabled = false;
            this.lblValorDescuento.Text = this._cliente.Descuento.ToString("C") + " " + this._cliente.DescripcionDescuento;
            if ((int) Convert.ToInt16(this._parametros.ValorParametro("LiqPrecioNeto")) == 0)
            {
                this._precioMinimo = ControlDeDescuento.Instance.PrecioAutorizado(this.dtListaPrecios, this._cliente.Descuento, this._cliente.ZonaEconomica);
                this._descuento = !(this._pedido.Precio == this._precioMinimo) ? (!Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("DescuentoProntoPago"))) ? new Decimal(0) : Convert.ToDecimal(this._pedido.Litros) * this._cliente.Descuento) : new Decimal(0);
            }
            this._pedido.ImporteDescuentoAplicado = Convert.ToDecimal(this._pedido.Litros) * this._cliente.Descuento;
            this._pedido.DescuentoAplicado = this._descuento == new Decimal(0) && this._pedido.ImporteDescuentoAplicado > new Decimal(0);
            ControlDeRemisiones controlDeRemisiones = new ControlDeRemisiones();
            bool notaValida;
            bool notaExistente;
            try
            {
                notaValida = controlDeRemisiones.ValidarNota(this._pedido.SerieRemision, this._pedido.FolioRemision.ToString());
                notaExistente = controlDeRemisiones.RemisionExistente((int) this._pedido.Celula, (int) this._pedido.AñoPed, this._pedido.NumeroPedido, this._pedido.SerieRemision, this._pedido.FolioRemision.ToString());
            }
            catch (Exception ex)
            {
                this.lblMensajeConciliacion.Text = "Error " + ex.Message;
                this.btnAceptar.Enabled = true;
                return;
            }
            if (this._validarRemisionExistente && !notaValida)
            {
                this.lblMensajeConciliacion.Text = "El número de remisión que proporcionó no está registrado en el sistema.";
                this.btnAceptar.Enabled = true;
            }
            else
            {
                if (this._validarRemisionLiquidada)
                {
                    if (notaExistente)
                    {
                        this.lblMensajeConciliacion.Text = "El número de remisión que proporcionó ya fué liquidado.";
                        this.btnAceptar.Enabled = true;
                        return;
                    }
                }
                try
                {
                    this._pedido.LiquidaPedido();
                    this._pedido = new SigametLiquidacion.Pedido(this._pedido.Celula, this._pedido.AñoPed, this._pedido.NumeroPedido);
                    this.OnClick((EventArgs) e);
                }
                catch (Exception ex)
                {
                    this.lblMensajeConciliacion.Text = ex.Message;
                    this.Error((object) this, (EventArgs) e);
                }
            }
        }
        
        private void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            this.RestoreComponent();
            this.OnClickCancelar((EventArgs) e);
        }
        
        private void btnDesasignar_Click(object sender, ImageClickEventArgs e)
        {
            this.OnDesasignar((EventArgs) e);
        }
        
        private byte asignarFormaPago()
        {
            if (this.ddpFormaPago.SelectedValue.Trim().ToUpper() == "CREDITO")
            {
                return (byte)6; //this._cliente.TipoCreditoCliente;
            }
            return (byte) 5;
        }
        
        private void cargaDatosCliente(int Cliente, object sender, EventArgs e)
        {

            string _buscando = Convert.ToString(System.Web.HttpContext.Current.Session["buscandoCliente"]);

            if (_buscando == "x")
            {
                this.btnCancelar_Click(sender, null);

                return;
            }


            if (this._tipoOperacionCaptura != TipoOperacionPedido.EdicionPedidoConciliado)
            {
                string text = this.txtNumeroRemision.Text;
                this.clearInfo();
                this.txtNumeroRemision.Text = text;
            }
            this._cliente = new SigametLiquidacion.Cliente(Cliente, this._claveCreditoAutorizado);

            this._cliente.FSuministro = FechaSuministro;//21-07-15 Consulta del precio de acuerdo a la zona económica del cliente

            try
            {
                this._cliente.ConsultaDatosCliente();
            }
            catch (Exception ex)
            {
                this.lblMensajeConciliacion.Text = "Error " + ex.Message;
                return;
            }
            if (this._tipoOperacionCaptura != TipoOperacionPedido.EdicionPedidoConciliado)
            {
                this._pedido = (SigametLiquidacion.Pedido)null;
            }

          



            if (this._cliente.Encontrado)
            {
                this.enableCaptureControls(true);
                this.lblNombreCliente.Text = this._cliente.Nombre!=null? this._cliente.Nombre:"";
                this.lblDireccionCliente.Text = this._cliente.Direccion;
                this.txtNumeroCliente.Enabled = false;
                this.lblCelulaRuta.Text = "Célula: " + this._cliente.Celula.ToString() + " Ruta: " + this._cliente.Ruta.ToString();
                if ((int) this._ruta != (int) this._cliente.Ruta)
                {
                    this.lblMensajeRutaDiferente.ForeColor = Color.Red;
                    this.lblMensajeRutaDiferente.Text = "EL CLIENTE NO PERTENECE A LA RUTA QUE ESTÁ LIQUIDANDO";
                }
                if (this._tipoOperacionCaptura != TipoOperacionPedido.EdicionPedidoConciliado)
                {
                    try
                    {
                        if (this._cliente.ClienteLiquidado(this._añoAtt, this._folio, this._cliente.NumeroCliente))
                        {
                            this.lblMensajeRutaDiferente.Text += "ESTE CLIENTE YA SE ENCUENTRA CAPTURADO EN LA LIQUIDACIÓN.";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.lblMensajeConciliacion.Text = ex.Message;
                    }
                }
                this.lblValorDescuento.Text = this._cliente.Descuento.ToString("C") + " " + this._cliente.DescripcionDescuento;
                if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("ValidarDescuentoPorPrecio"))))
                {
                    this.ddpPrecio.Items.Clear();
                    this.ddpPrecio.DataSource = (object) this.dtListaPrecios;
                    this.ddpPrecio.DataValueField = "Precio";
                    this.ddpPrecio.DataTextField = "Precio";
                    this.ddpPrecio.DataBind();
                    this._precioMinimo = ControlDeDescuento.Instance.PrecioAutorizado(this.dtListaPrecios, this._cliente.Descuento, this._cliente.ZonaEconomica);

                    if (this._cliente.Descuento > new Decimal(0))
                    {
                        this._precioMinimo = ControlDeDescuento.Instance.PrecioAutorizado(this.dtListaPrecios, this._cliente.Descuento, this._cliente.ZonaEconomica);
                        this.ddpPrecio.Items.Add(new ListItem(string.Format("{0:0.0000}", (object) this._precioMinimo), string.Format("{0:0.0000}", (object) this._precioMinimo)));
                        if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LiqSelPrecioDescuento"))))
                        {
                            this.ddpPrecio.SelectedValue = string.Format("{0:0.0000}", (object)this._precioMinimo);
                        }
                    }

                    //Carga del precio de acuerdo a la zona económica
                    if ((this._cliente.PrecioCliente > 0) &&
                        (this._precioMinimo != this._cliente.PrecioCliente))
                    {
                        this._precioMinimo = this._cliente.PrecioCliente;

                        System.Collections.Generic.List<ListItem> preciosInvalidos = new System.Collections.Generic.List<ListItem>();
                        
                        foreach (ListItem precioAux in this.ddpPrecio.Items)
                        {
                            if (Convert.ToDecimal(precioAux.Value) < this._precioMinimo)
                            {
                                preciosInvalidos.Add(precioAux);
                            }
                        }

                        foreach (ListItem precioAux in preciosInvalidos)
                        {
                            this.ddpPrecio.Items.Remove(precioAux);
                        }

                        this.ddpPrecio.Items.Add(new ListItem(string.Format("{0:0.0000}", (object)this._precioMinimo), string.Format("{0:0.0000}", (object)this._precioMinimo)));

                        if (this._cliente.Descuento > new Decimal(0))
                        {
                            this.ddpPrecio.SelectedValue = string.Format("{0:0.0000}", (object)this._precioMinimo);
                        }
                    }

                    this.ddpPrecio.Attributes.Add("onchange", "validarDescuento(" + '\'' + this.txtLitros.ClientID + '\'' + ", " + '\'' +
                        this.txtImporte.ClientID + '\'' + ", " + '\'' + this.ddpPrecio.ClientID + '\'' + ", " + this.ddpPrecio.ClientID + ", " +
                        this._precioMinimo.ToString() + ", " + (this._cliente.Descuento > new Decimal(0)).ToString().ToLower() + ", " + '\'' +
                        "El precio seleccionado no corresponde al descuento del cliente, Verifique." + '\'' + ", " + '\'' +
                        "Este cliente no tiene descuento autorizado, debe liquidar con el precio vigente." + '\'' + ");");
                }
                this.lblCarteraCredito.Text = this._cliente.DescripcionTipoCartera != null ? this._cliente.DescripcionTipoCartera.ToUpper().Trim():"" ;
                if (!this._cliente.CreditoAutorizado)
                {
                    this.lblCarteraCredito.ForeColor = Color.Red;
                    this.lblCarteraCredito.Text += ", LIQUIDACIÓN DE CONTADO";
                    if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("AutorizacionCredito"))))
                    {
                        this.formaPagoContado();
                    }
                }
                else
                {
                    this.lblTagLimiteDeCredito.Text = "Límite:";
                    Label label = this.lblCarteraCredito;
                    string str = label.Text + " A CARGO DE " + this._cliente.ClasificacionCartera;
                    label.Text = str;
                    this.lblLimiteDeCredito.Text = " MÁXIMO: " + this._cliente.LimiteCredito.ToString("C") + '\r' + " SALDO: " + this._cliente.Saldo.ToString("C") + '\r' + " DISPONIBLE: " + this._cliente.Disponible.ToString("C");
                    if (this._cliente.LimiteCreditoExcedido)
                    {
                        this.lblMensajeLimiteCredito.ForeColor = Color.Red;
                        this.lblMensajeLimiteCredito.Text = "LÍMITE EXCEDIDO, LIQUIDACIÓN DE CONTADO";
                        if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LimiteCredito"))))
                        {
                            this.formaPagoContado();
                        }
                    }
                }
                if (this._tipoOperacionCaptura != TipoOperacionPedido.EdicionPedidoConciliado)
                {
                    this._pedido = new SigametLiquidacion.Pedido(this._cliente.NumeroCliente);
                    bool flag;
                    try
                    {
                        flag = this._pedido.ConsultaPedidoActivo();
                    }
                    catch (Exception ex)
                    {
                        this.lblMensajeConciliacion.Text = ex.Message;
                        return;
                    }
                    if (flag)
                    {
                        this.publicarPedido();
                    }
                    else
                    {
                        this.altaPedidoNotaBlanca();
                    }
                    this.SetAlternateFocus();
                    if (this.txtLitros.Text.Trim().Length > 0)
                    {
                        this.txtImporte.Text = (Convert.ToDecimal(this.txtLitros.Text) * Convert.ToDecimal(this.ddpPrecio.SelectedValue)).ToString();
                    }
                }
                System.Web.HttpContext.Current.Session["buscandoCliente"] = "x";
                this.btnAceptar.Enabled = true;
                //if (this._cliente.TipoPago.Trim().ToUpper() == "SIN CRÉDITO")
                //{
                //    this.enableCaptureControls(false);
                //}
                //else
                //{
                //    this.enableCaptureControls(true);
                //}

                this.enableCaptureControls(true);
                this.ddpFormaPago.Enabled = this._cliente.CreditoAutorizado;
                

            }
            else
            {
                this.lblMensajeRutaDiferente.ForeColor = Color.Red;
                this.lblMensajeRutaDiferente.Text = "NO SE ENCONTRÓ EL CLIENTE ESPECIFICADO";
            }


            

        }
        
        private void clearInfo()
        {
            this.RestoreCliente();
            this.lblMensajeConciliacion.Text = string.Empty;
            this.txtImporte.Text = string.Empty;
            this.lblIva.Text = string.Empty;
            this.lblNumeroPedido.Text = string.Empty;
            this.txtNumeroRemision.Text = string.Empty;
            this.lnkCambiarCliente.Visible = false;
            this.initComboFormasPago();
            this.enableCaptureControls(false);
        }
        
        private void RestoreCliente()
        {
            this._cliente = (SigametLiquidacion.Cliente) null;
            this.lblCarteraCredito.Text = string.Empty;
            this.lblCelulaRuta.Text = string.Empty;
            this.lblDescuento.Text = string.Empty;
            this.lblDireccionCliente.Text = string.Empty;
            this.lblLimiteDeCredito.Text = string.Empty;
            this.lblMensajeLimiteCredito.Text = string.Empty;
            this.lblMensajeRutaDiferente.Text = string.Empty;
            this.lblNombreCliente.Text = string.Empty;
            this.lblTagNumeroCliente.Enabled = false;
        }
        
        public void RestoreComponent()
        {
            this.clearInfo();
            this._tipoOperacionCaptura = TipoOperacionPedido.CapturaNuevoPedido;
            this.txtNumeroCliente.Text = string.Empty;
            this.txtNumeroCliente.Enabled = true;
            this.lblValorDescuento.Text = string.Empty;
            this.txtLitros.Text = string.Empty;
            this.txtNumeroRemision.Text = string.Empty;
            this.lnkCambiarCliente.Visible = false;
            this._cliente = (SigametLiquidacion.Cliente) null;
            this._pedido = (SigametLiquidacion.Pedido) null;
            this.btnAceptar.Enabled = false;
        }
        
        private void formaPagoContado()
        {
            foreach (ListItem listItem in this.ddpFormaPago.Items)
            {
                if (listItem.Value.Trim().ToUpper() == "CONTADO")
                {
                    listItem.Selected = true;
                    break;
                }
            }
            this.ddpFormaPago.Enabled = false;
        }
        
        private void enableCaptureControls(bool Enable)
        {
            this.lblTagNumeroCliente.Enabled = true;
            this.txtLitros.Enabled = Enable;
            this.ddpPrecio.Enabled = Enable;
            this.ddpFormaPago.Enabled = Enable;
            this.btnAceptar.Enabled= Enable;
        }
        
        private string consultaTipoPedido(byte TipoPedido)
        {
            return this.dtListaTipoPedido.Rows.Find((object) TipoPedido)["Descripcion"].ToString().ToUpper();
        }
        
        private void altaPedidoNotaBlanca()
        {
            try
            {
                this._pedido.AltaPedido(this._añoAtt, this._folio, this._cliente.Celula, this._cliente.Ruta, this._fechaSuministro, this._usuario);
                this._pedido.ConsultaPedidoActivo();
                this.publicarPedido();
            }
            catch (Exception ex)
            {
                this.lblMensajeConciliacion.Text = "Error " + ex.Message;
            }
        }
        
        private void initComboFormasPago()
        {
            foreach (ListItem listItem in this.ddpFormaPago.Items)
            {
                if (listItem.Selected)
                {
                    listItem.Selected = false;
                }
            }
        }
        
        public void publicarPedido()
        {
            this.lblNumeroPedido.Text = this._pedido.PedidoReferencia + " " + this.consultaTipoPedido(this._pedido.TipoPedido);
            string folioRemision = string.Empty;
            if (this._pedido.FolioRemision != 0)
            {
                //TODO: Parametrizar relleno y tamaño si se usa remisión SGC
                folioRemision = this._pedido.FolioRemision.ToString().PadLeft(_longitudRemision, '0');
            }
            if (folioRemision.Trim().Length <= 0)
            {
                return;
            }
            this.txtNumeroRemision.Text = this._pedido.SerieRemision + folioRemision;
        }
        
        public void DesasignaPedido(DataRow CurrentRow)
        {
            this._editingRow = CurrentRow;
            try
            {
                this._sourceRow = (int) CurrentRow["ID"];
                if (Convert.ToString(this._editingRow["Status"]).ToUpper().Trim() == "CONCILIADO")
                {
                    this._pedido = new SigametLiquidacion.Pedido((short) this._editingRow["Celula"], (short) this._editingRow["AñoPed"], (int) this._editingRow["Pedido"]);
                    this._pedido.DesasignaPedido();
                }
                else
                {
                    if (!(Convert.ToString(this._editingRow["Status"]).ToUpper().Trim() == "PENDIENTE"))
                    {
                        return;
                    }
                    this._pedido = new SigametLiquidacion.Pedido((TipoOperacionDescarga) this._editingRow["Origen"], Convert.ToInt32(this._editingRow["ConsecutivoOrigen"]), this._añoAtt, this._folio);
                    this._pedido.PedidoActualizaRampac();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override void Focus()
        {
            base.Focus();
            if (this._tipoOperacionCaptura == TipoOperacionPedido.EdicionPedidoConciliado || this._tipoOperacionCaptura == TipoOperacionPedido.EdicionPedidoInconsistente)
            {
                this.SetAlternateFocus();
            }
            else
            {
                this.txtNumeroCliente.Focus();
            }
        }

        public void SetAlternateFocus()
        {
            if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("CapturaRemision"))))
            {
                this.txtNumeroRemision.Focus();
            }
            else
            {
                this.txtLitros.Focus();
            }
        }
        
        public void lnkCambiarCliente_click(object sender, EventArgs e)
        {
            this.RestoreCliente();
            if (this._tipoOperacionCaptura == TipoOperacionPedido.EdicionPedidoConciliado)
            {
                this.OnCambiarClientePedidoLiquidado((EventArgs) null);
                this._pedido = (SigametLiquidacion.Pedido) null;
                this.lblNumeroPedido.Text = string.Empty;
            }
            this._tipoOperacionCaptura = TipoOperacionPedido.CapturaNuevoPedido;
            this.txtNumeroCliente.Enabled = true;
            this.txtNumeroCliente.Text = string.Empty;
            this.txtNumeroCliente.Focus();
        }
    }
}
