using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SigametLiquidacion;
using System.Data;
using SigametLiquidacion;
using System.Web.Script.Serialization;

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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnCalFAsignacion.ImageUrl = this.ImgCal;
            //this.btnbAceptar.ImageUrl = this.ImgBoton;
            //this.btnAntAceptar.ImageUrl = this.ImgBoton;
            //this.btnBuscarCliente.ImageUrl = this.imgBtnBuscar;

            this.lblTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Transferencia electrónica de fondos" : this.Titulo;
            this.lblAntTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Aplicación de anticipo" : this.Titulo;

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

        else
        {
            if (Request.Form["__EVENTTARGET"].ToString().Contains("ConsultaCteAnticipo"))
            {
                    LimpiarControles();
                    ConsultaSaldos();

            }

        }

    }

    private void LimpiarControles()
        {
        LstSaldos.Items.Clear();
        txtAntNombre.Text = string.Empty; ;
        txtAntMonto.Text = string.Empty;
        txtAntOnservaciones.Text = string.Empty;
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

        if (dtLiqAnticipo.Columns.Count ==0)
        {
            dtLiqAnticipo.Columns.Add("Folio", typeof(String));
            dtLiqAnticipo.Columns.Add("AñoMovimiento", typeof(String));
            dtLiqAnticipo.Columns.Add("AñoCobro", typeof(String));
            dtLiqAnticipo.Columns.Add("Monto", typeof(decimal));
        }


        try
        {
            if (Session["dsLiquidacion"] == null)
            {
                string path = Server.MapPath("");
                ds.ReadXml(path + "/App_Code/dsLiquidacion.xsd");
            }

            if ((DataSet)(Session["dsLiquidacion"]) != null)
            {
                Session["idCobroConsec"] = ((Int32)(Session["idCobroConsec"]) + 1);
                dtCobro = ((DataSet)(Session["dsLiquidacion"])).Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                //dr["IdCobro"] = 0;
                dr["IdPago"] = ((Int32)(Session["idCobroConsec"]) + 1); ; //Consecutivo
                dr["Referencia"] =0;
                dr["NumeroCuenta"] = 0;

                dr["FechaCheque"] = LstSaldos.SelectedValue.ToString().Split('/')[3];
                dr["Cliente"] = this.txtAntCliente.Text;
                dr["Banco"] = "";

                dr["Importe"] = 0;
                dr["Impuesto"] = 0;
                dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text);

                dr["Saldo"] = 0;
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

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                dtCobro.Rows.Add(dr);

                //ds.Tables.Add(dtCobro);
               // ds.Tables.Add(dtLiqAnticipo);

                Session["idCliente"] = this.txtAntCliente.Text;
                // Session["dsLiquidacion"] = dtCobro.DataSet;
               // Session["dsLiquidacion"] = ds;

                // rp.InsertaMovimientoAConciliar(1, 2014, 2014, 1, Convert.ToInt32(this.txtAntMonto.Text), "EMITIDO");

                //ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');;", true);
            }


            else
            {
                //Genera Registro del Cobro con Cheque
                Session["idCobroConsec"] = 1;
                dtCobro = ds.Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                //dr["IdCobro"] = 0;
                dr["IdPago"] = 1; //Consecutivo
                dr["Referencia"] = 0;
                dr["NumeroCuenta"] = 0;

                dr["FechaCheque"] = LstSaldos.SelectedValue.ToString().Split('/')[3]; ;
                dr["Cliente"] = this.txtAntCliente.Text;
                dr["Banco"] = "0";

                dr["Importe"] = Convert.ToDouble(this.txtAntMonto.Text);
                dr["Impuesto"] = Convert.ToDouble(this.txtAntMonto.Text) * rp.dbIVA;
                dr["Total"] = Convert.ToDouble(this.txtAntMonto.Text)+(Convert.ToDouble(this.txtAntMonto.Text) * rp.dbIVA);

                dr["Saldo"] = 0;
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

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtAntCliente.Text;

                //ds.Tables.Add(dtCobro);
       

                // Session["dsLiquidacion"] = dtCobro.DataSet;

         

            }



            dtLiqAnticipo.Rows.Add(LstSaldos.SelectedValue.ToString().Split('/')[0], LstSaldos.SelectedValue.ToString().Split('/')[1], LstSaldos.SelectedValue.ToString().Split('/')[2], Convert.ToDecimal(this.txtAntMonto.Text));
            ds.Tables.Add(dtLiqAnticipo);
            Session["dsLiquidacion"] = ds;

            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');;", true);


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
    private void ConsultaSaldos()
        {

            Cliente _datosCliente = new Cliente(0, 1);
        try
        {
            _datosCliente.ConsultaSaldosAFavor(Convert.ToInt32(this.txtAntCliente.Text),"",0,0);
            
            if (_datosCliente!=null)
            {
                if (_datosCliente.SaldosCliente!=null)
                {
                    this.txtAntNombre.Text = _datosCliente.Nombre;
                    LstSaldos.DataSource = _datosCliente.SaldosCliente;
                    LstSaldos.DataTextField = "Saldo";
                    LstSaldos.DataValueField = "Clave";
                    LstSaldos.DataBind();
                    pnlAnticipo.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
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
        txtAntMonto.Text = LstSaldos.SelectedItem.Text.Split(',')[0].ToString().Replace("$","");
        ClaveAnticipo = LstSaldos.SelectedValue.ToString();
    }




    protected void postback_Click(object sender, ImageClickEventArgs e)
    {
        var x = 1;
    }
}