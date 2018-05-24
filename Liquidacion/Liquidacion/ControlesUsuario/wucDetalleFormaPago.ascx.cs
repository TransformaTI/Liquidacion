using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SigametLiquidacion;
using System.Data;
using SigametLiquidacion;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnCalFAsignacion.ImageUrl = this.ImgCal;
            this.btnbAceptar.ImageUrl = this.ImgBoton;
            this.btnAntAceptar.ImageUrl = this.ImgBoton;
            this.btnBuscarCliente.ImageUrl = this.imgBtnBuscar;

            if (this.TipoCobro == "22")
            {
                LlenaDropDowns();
                this.lblTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Transferencia electrónica de fondos" : this.Titulo;
                this.pnlTransferencia.Style.Add("display", "block");
                this.pnlAnticipo.Style.Add("display", "none");
            }
            else if (this.TipoCobro == "21")
            {
                this.pnlTransferencia.Style.Add("display", "none");
                this.pnlAnticipo.Style.Add("display", "block");
                this.lblAntTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Aplicación de anticipo" : this.Titulo;
            }
        }
    }

    private void LlenaDropDowns()
    {
        DataTable dtBancos = new DataTable();
        
        try
        {
            dtBancos = rp.ListaBancos();
            ddlBanco.DataSource = dtBancos;
            ddlBanco.DataTextField = "Nombre";
            ddlBanco.DataValueField = "Banco";
            ddlBanco.DataBind();
            ddlBanco.Items.Insert(0, new ListItem("- Seleccione -", "0"));
            ddlBanco.SelectedIndex = 0;

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
            if ((DataSet)(Session["dsLiquidacion"]) != null)
            {
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                
                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdCobro"] = 0; 
                dr["Referencia"] = this.txtNoDocumento.Text;
                dr["NumeroCuenta"] = this.txtNoDocumento.Text;

                dr["FechaCheque"] = this.txtFecha.Text;
                dr["Cliente"] = this.txtCliente.Text;
                dr["Banco"] = ddlBanco.SelectedValue; 

                dr["Importe"] = (Convert.ToDouble(this.txtImporte.Text) * rp.dbIVA);
                dr["Impuesto"] = (Convert.ToDouble(this.txtImporte.Text) * rp.dbIVA);
                dr["Total"] = txtImporte.Text; 

                dr["Saldo"] = 0;
                dr["Observaciones"] = txtObservaciones.Text;
                dr["Status"] = "ABIERTO"; 

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);
                dr["Usuario"] = "";

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = this.txtFecha.Text;

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "TRANSFERENCIA";

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnAceptarAnticipo_Click(object sender, EventArgs e)
    {
        try
        {
            if ((DataSet)(Session["dsLiquidacion"]) != null)
            {
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdCobro"] = 0;
                dr["Referencia"] =0;
                dr["NumeroCuenta"] = 0;

                dr["FechaCheque"] = "";
                dr["Cliente"] = this.txtAntCliente.Text;
                dr["Banco"] = "";

                dr["Importe"] = 0;
                dr["Impuesto"] = 0;
                dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text);

                dr["Saldo"] = Convert.ToDouble(this.txtAntSaldo.Text);
                dr["Observaciones"] = this.txtAntOnservaciones.Text;
                dr["Status"] = "ABIERTO";

                dr["FechaAlta"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo); 
                dr["Usuario"] = "";

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = "";

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "ANTICIPO";

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtAntCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;
                rp.InsertaMovimientoAConciliar(1, 2014, 2014, 1, Convert.ToInt32(this.txtAntMonto.Text), "EMITIDO");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnBuscarCliente_Click(object sender, EventArgs e)
    {
        Cliente _datosCliente = new Cliente(0,1);
        try
        {
            _datosCliente.ConsultaSaldosAFavor(Convert.ToInt32(this.txtAntCliente.Text),"",0,0);
            this.txtAntNombre.Text = _datosCliente.Nombre;
            this.txtAntSaldo.Text = _datosCliente.Saldo.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        
}