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
    DataTable dtPedidos=new DataTable("Pedidos");
    DataSet dsLiq;
    DataTable dtLiq, dtCobroPedido;
    DataTable dt;



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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
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
                        
            this.lblTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Transferencia electrónica de fondos" : this.Titulo;
            this.lblAntTitulo.Text = string.IsNullOrEmpty(this.Titulo) ? "Aplicación de anticipo" : this.Titulo;

            if (this.TipoCobro == "22")
            {
             
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
            if (Request.Form["__EVENTTARGET"].ToString().Contains("ConsultaCteTransferencia"))
            {
                this.TipoCobro = "22";
                int ClienteID = 0;
                if (txtCliente.Text.Trim().Length > 0)
                {
                    ClienteID = Convert.ToInt32(txtCliente.Text.Trim());
                    txtNombre.Text = consultaNombreClienteTransferencia(ClienteID);
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

                dr["IdPago"] = ((Int32)(Session["idCobroConsec"] == null ? 0: Session["idCobroConsec"]) + 1); ; //Consecutivo
                dr["Referencia"] = this.txtNoDocumento.Text;
                dr["NumeroCuenta"] = this.txtNoDocumento.Text;

                dr["FechaCheque"] = this.txtFecha.Text;
                dr["Cliente"] = this.txtCliente.Text;
                dr["Banco"] = ddlBanco.SelectedValue; 

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

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "TRANSFERENCIA";

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtImporte.Text); ;

                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;

                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('RegistroPagos.aspx');", true);
            }

            else

            {
                //Genera Registro del Cobro con Cheque
                Session["idCobroConsec"] = 1;
                dtCobro = ds.Tables["Cobro"];
                DataRow dr;
                dr = dtCobro.NewRow();

                dr["IdPago"] = ((Int32)(Session["idCobroConsec"] == null ? 0 : Session["idCobroConsec"]) + 1); ; //Consecutivo
                dr["Referencia"] = this.txtNoDocumento.Text;
                dr["NumeroCuenta"] = this.txtNoDocumento.Text;

                dr["FechaCheque"] = this.txtFecha.Text;
                dr["Cliente"] = this.txtCliente.Text;
                dr["Banco"] = ddlBanco.SelectedValue;

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

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "TRANSFERENCIA";
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.transferencia);
                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtImporte.Text); ;

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtCliente.Text;
                Session["dsLiquidacion"] = dtCobro.DataSet;

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
        if (decimal.Parse(txtAntMonto.Text)== 0)
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
        }


        try
        {
            ConsultaPedidos();

            //ds = (DataSet)(Session["dsLiquidacion"]);

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

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                Session["PagoEnUsoAnticipo"] = LstSaldos.SelectedValue.ToString().Split('/')[0] + LstSaldos.SelectedValue.ToString().Split('/')[1];

                dtCobro.Rows.Add(dr);

                Session["idCliente"] = this.txtAntCliente.Text;

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
                dr["Usuario"] = Convert.ToString(Session["Usuario"]); ;

                dr["SaldoAFavor"] = 0;
                dr["TPV"] = 0;
                dr["FechaDeposito"] = "";
                dr["TipoCobro"] = (Int16)(RegistroPago.TipoPago.anticipo);

                dr["BancoOrigen"] = 0;
                dr["NombreTipoCobro"] = "ANTICIPO";

                Session["ImporteOperacion"] = Convert.ToDecimal(this.txtAntMonto.Text); ;

                Session["PagoEnUsoAnticipo"]= LstSaldos.SelectedValue.ToString().Split('/')[0]+ LstSaldos.SelectedValue.ToString().Split('/')[1]; 

                dtCobro.Rows.Add(dr);
                Session["idCliente"] = this.txtAntCliente.Text;         

            }

            if (ds.Tables["LiqPagoAnticipado"]!=null)
            dtLiqAnticipo = ds.Tables["LiqPagoAnticipado"];

            dtLiqAnticipo.Rows.Add(LstSaldos.SelectedValue.ToString().Split('/')[0], LstSaldos.SelectedValue.ToString().Split('/')[1], LstSaldos.SelectedValue.ToString().Split('/')[2], Convert.ToDecimal(this.txtAntMonto.Text),"");


            if (ds.Tables.Contains("Pedidos"))
            {
                ds.Tables.Remove("Pedidos");
            }
            dtPedidos.TableName = "Pedidos";
            ds.Tables.Add(dtPedidos);

            if (ds.Tables.Contains("LiqPagoAnticipado"))
            {
                ds.Tables.Remove("LiqPagoAnticipado");
            }
            dtLiqAnticipo.TableName = "LiqPagoAnticipado";
            ds.Tables.Add(dtLiqAnticipo);

            Session["dsLiquidacion"] = ds;


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

 private void ConsultaPedidos()
    {
        dtPedidos= rp.PedidosLiquidacion(int.Parse(txtAntCliente.Text));

    }



    private void ConsultaSaldos()
        {

        Cliente _datosCliente = new Cliente(0, 1);

        decimal NuevoSaldo=0;
        decimal TotalPedidos = 0;
        DataRow[] drSaldo = null;
        try
        {
            dsLiq = (DataSet)(Session["dsLiquidacion"]);
            _datosCliente.ConsultaSaldosAFavor(Convert.ToInt32(this.txtAntCliente.Text),"",0,0);
            
            if (_datosCliente!=null)
            {
                if (_datosCliente.SaldosCliente!=null)
                {
                    
                    if (dsLiq!=null)
                    {
                        dt = dsLiq.Tables["CobroPedido"];
                        dtLiq = dsLiq.Tables["LiqPagoAnticipado"];
                        dtCobroPedido= dsLiq.Tables["CobroPedido"];

                        foreach (DataRow dr in _datosCliente.SaldosCliente.Rows)
                            {
                            
                            if  (dtLiq != null)
                                {
                                drSaldo = dtLiq.Select("Folio=" + dr["FolioMovimiento"].ToString() + " AND AñoMovimiento=" + dr["AñoMovimiento"].ToString());
                                foreach (DataRow row in drSaldo)
                                {
                                  if (dsLiq.Tables["CobroPedido"]!=null)
                                {
                                    if (dsLiq.Tables["CobroPedido"].Rows.Count >0 )
                                    {
                                       foreach(DataRow rpedido in dtCobroPedido.Rows)
                                        {
                                            if (row["Pedidos"].ToString().Contains(rpedido["Pedido"].ToString()))
                                            {
                                                TotalPedidos = TotalPedidos + decimal.Parse(rpedido["Total"].ToString());                                               
                                                
                                            }
                                        }
                                        
                                    }
                                }
                                  
                                   NuevoSaldo = decimal.Parse(dr["MontoSaldo"].ToString()) - TotalPedidos; ;
                                if (NuevoSaldo >0)
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

    private string consultaNombreClienteTransferencia(int ClienteID)
    {
        string NombreCliente = "";
        try
        {
            Cliente objCliente = new Cliente(ClienteID);
            NombreCliente = objCliente.Nombre;
        }
        catch (Exception ex)
        {
            throw ex;
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
        txtAntMonto.Text = LstSaldos.SelectedItem.Text.Split(',')[0].ToString().Replace("$","");
        ClaveAnticipo = LstSaldos.SelectedValue.ToString();
    }




    protected void postback_Click(object sender, ImageClickEventArgs e)
    {
   
    }
}