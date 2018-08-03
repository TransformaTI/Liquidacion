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
using SigametLiquidacion;

public partial class RegistroPagos : System.Web.UI.Page
{
    RegistroPago rp = new RegistroPago();
    DataTable dtPedidos = new DataTable();
    DataTable dtPagos;
    DataTable dtCobros;
    DataTable dtMovimientos;
    DataSet ds = new DataSet();
    Decimal importeMovto;
    string pagoActivo;
    int idCliente;



    protected void Page_Load(object sender, EventArgs e)
    {
        imbRedirAbonos.Attributes.Add("onclick", "return ValidaSaldos();");

       

        if (!Page.IsPostBack && Session["ImporteOperacion"].ToString()!=null)
        {
            lblImporteTotalA.Text = String.Format("{0:C}", decimal.Parse(Session["ImporteOperacion"].ToString()));

        }
        
        //dsLiquidacion es el dataset creado y obtenido por esquema, validamos que esté inicializado
        
        if (Session["dsLiquidacion"] != null)
        {
            // if (!Page.IsPostBack)
            if (((DataSet)(Session["dsLiquidacion"])).Tables["Pedidos"].Rows.Count == 0)
            {
                ds = (DataSet)(Session["dsLiquidacion"]);
                dtPedidos = ((DataTable)(Session["dtPedidos"]));
                dtPedidos.TableName = "Pedidos";

                if (Session["FormaPago"] != null && (string)Session["FormaPago"] != "Anticipo")
                {
                    ds.Tables.Remove("Pedidos");
                    ds.Tables.Add(dtPedidos);
                }
            }
            else
            {
                ds = (DataSet)(Session["dsLiquidacion"]);

                if ( Session["FormaPago"]!=null)
                {
                    if (Session["FormaPago"].ToString()!="Anticipo")
                    {
                        DataTable dtPedidosNoParientes = (DataTable)Session["dtPedidos"];
                        ds.Tables.Remove("Pedidos");
                        dtPedidosNoParientes.TableName = "Pedidos";
                        DataTable LiqPagoAnticipado = ds != null ? ds.Tables["LiqPagoAnticipado"] : null;

      
                        DataTable dtPedidosParientes = (DataTable)(Session["PedidosParientes"]);

                        // actualiza saldos 
                        if (dtPedidosNoParientes != null &&  !Page.IsPostBack)
                        {
                            foreach (DataRow item in dtPedidosNoParientes.Rows)
                            {
                                decimal Saldo = decimal.Parse(item["Total"].ToString());
          
                                if (ds!=null)
                                {
                                    if (ds.Tables["CobroPedido"]!=null)
                                    {
                                            foreach (DataRow row in ds.Tables["CobroPedido"].Rows)
                                        {
                                            if (row["Pedido"].ToString().Trim() == item["Pedido"].ToString().Trim())
                                            {
                                                Saldo = Saldo - decimal.Parse(row["total"].ToString());
                                            }

                                        }
                                    }

                                }

                                item.BeginEdit();
                                item["Saldo"] = Saldo;                                    
                                item.EndEdit();


                            }
                        }


                        ds.Tables.Add(dtPedidosNoParientes);
                        Session["dsLiquidacion"] = ds;
                    }
                }
            }
          
            idCliente = Convert.ToInt32(Session["idCliente"]);
          
            CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
        }

        if (ds.Tables["CobroPedido"] != null)
        {
            if (ds.Tables["CobroPedido"].Rows.Count > 0)
            {
                pagoActivo = Session["idCobroConsec"].ToString();

                DataView vistaPagoActivo = new DataView(ds.Tables["CobroPedido"]);
                vistaPagoActivo.RowFilter = "IdPago = " + pagoActivo;
                gvRelacionCobro.DataSource = vistaPagoActivo;
                gvRelacionCobro.DataBind();
            }
        }
    }
    #region "Functs and Subs"
    protected void CargaPedidos(DataTable dtPedidosCarga, int idCliente, bool orden)
    {
        int i = 1;
        try
        {
            if (!ds.Tables["Pedidos"].Columns.Contains("IdOrder"))
            {
                ds.Tables["Pedidos"].Columns.Add("IdOrder");
            }

            foreach (DataRow dr in ds.Tables["Pedidos"].Rows)
            {
                if (Convert.ToInt32(dr["Cliente"]) == idCliente)
                {
                    dr.BeginEdit();
                    dr["IdOrder"] = i;
                    dr.EndEdit();

                    i++;
                }
            }
            DataView vistaPedidos = new DataView(ds.Tables["Pedidos"]);
            vistaPedidos.Sort = "IdOrder DESC";
            gvPedidos.DataSource = vistaPedidos;
            gvPedidos.DataBind();
            lblImportePagoA.Text = String.Format("{0:C}", Convert.ToDecimal(Session["ImporteOperacion"]));
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool ActualizaSaldo(string referencia, decimal monto, bool cargoAbono)
    {
        decimal saldoActual;
        DataRow dr;
        bool valid = false;
        DataRow[] drArrayMov;
        
        try
        {
            importeMovto = (Decimal)(Session["ImporteOperacion"]);

            for (int i = 0; i <= ds.Tables["Pedidos"].Rows.Count - 1; i++)
            {
                //Encuentro la Referencia del Pedido
                if (ds.Tables["Pedidos"].Rows[i]["pedidoreferencia"].ToString().TrimEnd() == referencia.ToString())
                {
                    //Valido si es un cargo o un abono y actualizo
                    if (!cargoAbono)
                    {
                        //Valido que el monto no sobrepase mi disponible total
                        if (monto <= importeMovto)
                        {
                            saldoActual = (Convert.ToDecimal(ds.Tables["Pedidos"].Rows[i]["saldo"].ToString()) - monto);
                            //Actualizo Importe Movimiento
                            importeMovto = importeMovto - monto;
                            lblImportePagoA.Text = String.Format("{0:C}", Convert.ToDecimal(importeMovto.ToString()));
                            Session["ImporteOperacion"] = importeMovto;
                            //Valido que el cargo al pedido no exceda su disponible
                            if (saldoActual < 0)
                            {
                                lblError.Text = "El monto del Abono es mayor al saldo por cubrir!";
                                valid = false;
                            }   
                            else
                            {
                                dr = ds.Tables["Pedidos"].Rows[i];
                                dr.BeginEdit();
                                dr["saldo"] = Convert.ToDecimal(saldoActual);
                                dr.EndEdit();

                                //Actualiza el saldo en los Cobros
                                pagoActivo = Session["idCobroConsec"].ToString();
                                drArrayMov = ds.Tables["Cobro"].Select("IdPago = '" + pagoActivo.ToString() + "'", null);
                                foreach (DataRow drMov in drArrayMov)
                                {
                                    if (drMov["IdPago"].ToString() == pagoActivo.ToString())
                                    {
                                        drMov.BeginEdit();
                                        drMov["saldo"] = importeMovto;
                                        if (importeMovto > 0)
                                        {
                                            drMov["SaldoAFavor"] = 1;
                                        }
                                        else
                                        {
                                            drMov["SaldoAFavor"] = 0;
                                        }
                                        drMov.EndEdit();
                                    }
                                }
                                drArrayMov = null;
                                //Actualizo Grid
                                CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
                                valid = true;
                                lblError.Text = "";

            

                                Session["dsLiquidacion"] = ds;


                            }


                        }
                        else
                        {
                            lblError.Text = "El monto del Abono es mayor al Disponible Total!";
                            valid = false;
                        }
                    }

                        //Para el abono (cancelación) no valido montos ya que serán sumados al saldo del pedido
                    else
                    {

                        saldoActual = (Convert.ToDecimal(ds.Tables["Pedidos"].Rows[i]["saldo"].ToString()) + monto);
                        //Actulizo Importe Movimiento
                        importeMovto = importeMovto + monto;

                        lblImportePagoA.Text = String.Format("{0:C}", Convert.ToDecimal(importeMovto.ToString()));
                        Session["ImporteOperacion"] = importeMovto;

                        dr = ds.Tables["Pedidos"].Rows[i];
                        dr.BeginEdit();
                        dr["saldo"] = Convert.ToDecimal(saldoActual);
                        dr.EndEdit();

                        pagoActivo = Session["idCobroConsec"].ToString();
                        drArrayMov = ds.Tables["Cobro"].Select("IdPago = '" + pagoActivo.ToString() + "'", null);
                        foreach (DataRow drMov in drArrayMov)
                        {
                            if (drMov["IdPago"].ToString() == pagoActivo.ToString())
                            {
                                drMov.BeginEdit();
                                drMov["saldo"] = importeMovto;
                                drMov.EndEdit();
                            }
                        }
                        drArrayMov = null;
                            
                        //Actualizo Grid
                        CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
                        valid = true;
                        lblError.Text = "";
                        Session["dsLiquidacion"] = ds;
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
    protected bool ValidaCaptura(string referencia, string pagoActivo)
    {
        bool val = true;
        try
        {
            for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
            {
                //Valida si ya existe un abono para ese pedido con el mismo Pago
                if (referencia == ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString() && pagoActivo == ds.Tables["CobroPedido"].Rows[i]["IdPago"].ToString())
                {
                    val = false;
                }
            }
            return val;
        }
        catch
        {
            throw;
        }
    }
    protected void CancelarAbonos()
    {
        string refPago;
        int pagoActivo = Convert.ToInt32(Session["idCobroConsec"]);
        decimal importeAbono;
        DataTable dtEliminar = new DataTable();
        DataRow[] drArray;
        DataTable Pedidos = new DataTable();

        try
        {
            for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
            {
                refPago = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();
                if (ds.Tables.Contains("Pedidos"))
                {
                    for (int j = 0; j <= ds.Tables["Pedidos"].Rows.Count - 1; j++)
                    {

                        if (refPago == ds.Tables["Pedidos"].Rows[j]["PedidoReferencia"].ToString().TrimEnd() && pagoActivo.ToString() == ds.Tables["CobroPedido"].Rows[i]["IdPago"].ToString())
                         {
                            importeAbono = Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"]);
                            ActualizaSaldo(refPago, importeAbono, true);



                         }
                    }

                }


            }

            // Eliminar de la tabla del Detalle los Rows del Cobro Cancelado           
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
         
            DataView vistaPagoActivo = new DataView(dtPagos = ds.Tables["CobroPedido"]);
            vistaPagoActivo.RowFilter = "IdPago = " + pagoActivo;
            gvRelacionCobro.DataSource = vistaPagoActivo;
            gvRelacionCobro.DataBind();

            pagoActivo = pagoActivo - 1;
            Session["idCobroConsec"] = pagoActivo;



         
        }
        catch { throw; }
    }

    protected void CancelaRelacionPagoPedido()
    {
        foreach (DataRow pedidos in ds.Tables["CobroPedido"].Rows)
        {
            DataTable dtLiqAnticipo = ds.Tables["LiqPagoAnticipado"];

            if (dtLiqAnticipo!=null)
            {
                foreach (DataRow row in dtLiqAnticipo.Rows)
                {
                    if (Session["PagoEnUsoAnticipo"].ToString() == row["Folio"].ToString() + row["AñoMovimiento"].ToString() && row["Pedidos"].ToString().Contains(pedidos["Pedido"].ToString()))
                    {
                        row.BeginEdit();
                        row["Pedidos"] = row["Pedidos"].ToString().Replace(pedidos["Pedido"].ToString(), "");
                        row.EndEdit();
                    }
                }
                ds.Tables.Remove("LiqPagoAnticipado");
                ds.Tables.Add(dtLiqAnticipo);
            }
        }
    }
    #endregion
    #region "Handlers"
    protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal disponible = Convert.ToDecimal(Session["ImporteOperacion"]);
        try
        {
            Decimal saldo;
                
            txtImporteDocto.Text = String.Format("{0:0.00}", gvPedidos.SelectedRow.Cells[8].Text.Replace("$", ""));
            txtSaldoMovimiento.Text = String.Format("{0:0.00}", gvPedidos.SelectedRow.Cells[9].Text.Replace("$", ""));
            Session["SaldoMtvo"] = Convert.ToDecimal(Session["ImporteOperacion"]) - Convert.ToDecimal(txtImporteDocto.Text);

            ScriptManager.RegisterStartupScript(this, GetType(), "saldo", "Saldo="+ Session["SaldoMtvo"].ToString()+"; ", true);



            //Valido si el pedido tiene descuento y solo le digo de donde va a tomar el valor
            //if (gvPedidos.SelectedRow.Cells[10].Text != "$0.00")
            if ((gvPedidos.SelectedRow.Cells[10].Text.Trim().Length > 0) && (gvPedidos.SelectedRow.Cells[10].Text != "$0.00"))
            {
                //Sacamos el saldo con el descuento 
                try
                {
                    saldo = Convert.ToDecimal(gvPedidos.SelectedRow.Cells[8].Text.Replace("$", "")) - Convert.ToDecimal(gvPedidos.SelectedRow.Cells[10].Text.Replace("$", ""));
                    lblDescuento.Text = "El pedido tiene un descuento por: " + String.Format("{0:0.00}", gvPedidos.SelectedRow.Cells[10].Text);
                    lblDescuento.Visible = true;
                }
                catch (Exception ex)
                {
                    saldo = Convert.ToDecimal(gvPedidos.SelectedRow.Cells[9].Text.Replace("$", ""));
                    lblDescuento.Visible = false;
                }
            }
            else
            {
                saldo = Convert.ToDecimal(gvPedidos.SelectedRow.Cells[9].Text.Replace("$", ""));
                lblDescuento.Visible = false;
            }

            //Si el disponible es mayor al monto total del pedido entonces pone el total en el txt
            //if (disponible >= Convert.ToDecimal(gvPedidos.SelectedRow.Cells[7].Text.Replace("$", "")))
            if (disponible >= saldo)
            {
                txtImporteAbono.Text = saldo.ToString();
            }
            else if (disponible > 0)
            {
                txtImporteAbono.Text = String.Format("{0:0.00}", disponible);
            }
            else
            {
                txtImporteAbono.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message + lblError.Text;
        }
    }
    protected void imbAceptarAbono_Click(object sender, ImageClickEventArgs e)
    {
        string referencia;
        string pagoActivo;
        string PagoEnUso;


        if (Session["PagoEnUsoAnticipo"]!=null)
            PagoEnUso = Session["PagoEnUsoAnticipo"].ToString();



        try
        {


            referencia = gvPedidos.SelectedRow.Cells[2].Text.TrimEnd();
            
            pagoActivo = Session["idCobroConsec"].ToString();
            //Primer detalle del Pago 
                  
            if (ds.Tables["CobroPedido"].Rows.Count == 0)
            {   
                //Valida y Actualiza Saldo
                if (ActualizaSaldo(referencia, Convert.ToDecimal(txtImporteAbono.Text), false))
                //Insertar Registro en Tabla
                {
                    //Crear Registro nuevo 
                    dtPagos = ds.Tables["CobroPedido"];

                    DataRow dr;
                    //dr = dtPagos.NewRow();
                    dr = ds.Tables["CobroPedido"].NewRow();
                    dr["IdPago"] = Session["idCobroConsec"].ToString();
                    dr["Pedido"] = gvPedidos.SelectedRow.Cells[3].Text.TrimEnd();//gvPedidos.SelectedRow.Cells[2].Text;
                    dr["Celula"] = gvPedidos.SelectedRow.Cells[4].Text;
                    dr["Anio"] = gvPedidos.SelectedRow.Cells[5].Text;
                    dr["Importe"] = Convert.ToDecimal(txtImporteAbono.Text);
                    dr["Impuesto"] = 0; //CHECK THIS
                    dr["Total"] = Convert.ToDecimal(txtImporteAbono.Text);
                    ds.Tables["CobroPedido"].Rows.Add(dr);
                    //Actualizo grid de Pagos
                    DataView vistaPago = new DataView(ds.Tables["CobroPedido"]);
                    vistaPago.RowFilter = "IdPago = " + Session["idCobroConsec"].ToString();
                    gvRelacionCobro.DataSource = vistaPago;
                    gvRelacionCobro.DataBind();


                 
                    //Limpio Controles de Captura
                    txtImporteAbono.Text = "";
                    txtImporteDocto.Text = "";
                    txtSaldoMovimiento.Text = "";

                    DataTable dtLiqAnticipo = ds.Tables["LiqPagoAnticipado"];

                    if (dtLiqAnticipo != null)
                    {
                        foreach (DataRow row in dtLiqAnticipo.Rows)
                    {
                        if (Session["PagoEnUsoAnticipo"].ToString() == row["Folio"].ToString() + row["AñoMovimiento"].ToString())
                        {
                            row.BeginEdit();
                            row["Pedidos"] = row["Pedidos"] + ","+ gvPedidos.SelectedRow.Cells[3].Text.TrimEnd();
                            row.EndEdit();
                        }
                    }

                        ds.Tables.Remove("LiqPagoAnticipado");
                        ds.Tables.Add(dtLiqAnticipo);

                        Session["dsLiquidacion"] = ds;
                    }

                }
            }
            //La tabla ya tiene contenido
            else
            {
                //Valida que no haya un Abono para el pedido seleccionado
                if (ValidaCaptura(referencia, pagoActivo))
                {
                    //Actualiza y Valida el Saldo, agrega fila a Tabla de Pagos post validacion
                    if (ActualizaSaldo(referencia, Convert.ToDecimal(txtImporteAbono.Text), false))
                    //Insertar Registro en Tabla
                    {
                 
                        DataRow dr;
                 
                        dr = ds.Tables["CobroPedido"].NewRow();
                        referencia = gvPedidos.SelectedRow.Cells[2].Text.TrimEnd();
                        dr["IdPago"] = Session["idCobroConsec"].ToString();
                        dr["Pedido"] = gvPedidos.SelectedRow.Cells[3].Text.TrimEnd();//gvPedidos.SelectedRow.Cells[2].Text;
                        dr["Celula"] = gvPedidos.SelectedRow.Cells[4].Text;
                        dr["Anio"] = gvPedidos.SelectedRow.Cells[5].Text;
                        dr["Importe"] = Convert.ToDecimal(txtImporteAbono.Text);
                        dr["Impuesto"] = 0; //CHECK THIS
                        dr["Total"] = Convert.ToDecimal(txtImporteAbono.Text);
                        //dtPagos.Rows.Add(dr);
                        ds.Tables["CobroPedido"].Rows.Add(dr);
                        //Actualizo grid de Pagos
                        DataView vistaPago = new DataView(ds.Tables["CobroPedido"]);
                        vistaPago.RowFilter = "IdPago = " + Session["idCobroConsec"].ToString();
                        gvRelacionCobro.DataSource = vistaPago;
                        gvRelacionCobro.DataBind();
                 
                        //Limpio Controles de Captura
                        txtImporteAbono.Text = "";
                        txtImporteDocto.Text = "";
                        txtSaldoMovimiento.Text = "";
                        ///
                        DataTable dtLiqAnticipo = ds.Tables["LiqPagoAnticipado"];

                        if (dtLiqAnticipo!=null)
                        {
                            foreach (DataRow row in dtLiqAnticipo.Rows)
                        {
                            if (Session["PagoEnUsoAnticipo"].ToString() == row["Folio"].ToString() + row["AñoMovimiento"].ToString())
                            {
                                row.BeginEdit();
                                row["Pedidos"] = row["Pedidos"] + "," + gvPedidos.SelectedRow.Cells[3].Text.TrimEnd();
                                row.EndEdit();
                            }
                        }

                                ds.Tables.Remove("LiqPagoAnticipado");
                                ds.Tables.Add(dtLiqAnticipo);

                                Session["dsLiquidacion"] = ds;
                        }
                    }
                    lblError.Text = "";
                }
                else { lblError.Text = "Ya existe un Abono para el pedido Seleccionado!"; }
            }
            lblDescuento.Visible = false;


          



        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void gvRelacionCobro_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow dr;
        string referencia;
        Decimal monto;
        GridViewRow row;
        try
        {

            for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
            {
                //Busco 
                
                if (ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString() == gvRelacionCobro.SelectedRow.Cells[0].Text)
                {
                    referencia = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();
                    monto = (Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"].ToString()));
                    dr = ds.Tables["CobroPedido"].Rows[i];
                    ds.Tables["CobroPedido"].Rows.Remove(dr);

                    DataView vistaPago = new DataView(ds.Tables["CobroPedido"]);
                    vistaPago.RowFilter = "IdPago = " + Session["idCobroConsec"].ToString();
                    gvRelacionCobro.DataSource = vistaPago;
                    gvRelacionCobro.DataBind();
                
                    ActualizaSaldo(referencia, monto, true);
                    CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imbRedirAbonos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            DataRow[] CobrosPedido = ds.Tables["CobroPedido"].Select("IdPago = '" + Session["idCobroConsec"].ToString() + "'");
            //DataRow[] CobrosPedido = ds.Tables["CobroPedido"].Select();
            if (CobrosPedido.Length > 0)

            
                Response.Redirect("GenerarPago.aspx");
            else
                lblError.Text = "Debe Capturar Abonos para el Pago";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imbCancelarAbonos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CancelarAbonos();
            CancelaRelacionPagoPedido();
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.replace('FormaPago.aspx');", true);

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
    {
        decimal disponible = Convert.ToDecimal(Session["ImporteOperacion"]);
        try
        {

            Decimal saldo;

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            
            //txtImporteDocto.Text = String.Format("{0:0.00}", gvPedidos.SelectedRow.Cells[7].Text.Replace("$", ""));
            txtImporteDocto.Text = String.Format("{0:0.00}", gvPedidos.Rows[row.RowIndex].Cells[7].Text.Replace("$", ""));
            txtSaldoMovimiento.Text = String.Format("{0:0.00}", gvPedidos.Rows[row.RowIndex].Cells[8].Text.Replace("$", ""));
            //Session["SaldoMtvo"]= Convert.ToDecimal(Session["ImporteOperacion"])- Convert.ToDecimal(txtImporteDocto.Text);

            //Valido si el pedido tiene descuento y solo le digo de donde va a tomar el valor
            if (gvPedidos.Rows[row.RowIndex].Cells[9].Text != "$0.00")
            {
                //Sacamos el saldo con el descuento 
                try
                {
                  saldo = Convert.ToDecimal(gvPedidos.Rows[row.RowIndex].Cells[7].Text.Replace("$", "")) - Convert.ToDecimal(gvPedidos.Rows[row.RowIndex].Cells[9].Text.Replace("$", ""));
                  lblDescuento.Text = "El pedido tiene un descuento por: " + String.Format("{0:0.00}", gvPedidos.Rows[row.RowIndex].Cells[9].Text);
                  lblDescuento.Visible = true;
                }
                catch (Exception ex)
                {
                  saldo = Convert.ToDecimal(gvPedidos.Rows[row.RowIndex].Cells[8].Text.Replace("$", ""));
                  lblDescuento.Visible = false;     
                }
            }
            else
            {
                saldo = Convert.ToDecimal(gvPedidos.Rows[row.RowIndex].Cells[8].Text.Replace("$", ""));
                lblDescuento.Visible = false;
            }

            //Si el disponible es mayor al monto total del pedido entonces pone el total en el txt
            //if (disponible >= Convert.ToDecimal(gvPedidos.SelectedRow.Cells[7].Text.Replace("$", "")))
            if (disponible >= saldo)
            {
                txtImporteAbono.Text = saldo.ToString();
            }
            else if (disponible > 0)
            {
                txtImporteAbono.Text = String.Format("{0:0.00}", disponible);
            }
            else
            {
                txtImporteAbono.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message + " Error";
        }
    }
    protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataRow dr;
        string referencia;
        Decimal monto;
        try
        {
            for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
            {
                //Busco 

                if (ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString() == gvRelacionCobro.SelectedRow.Cells[0].Text)
                {
                    referencia = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();
                    monto = (Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"].ToString()));
                    dr = ds.Tables["CobroPedido"].Rows[i];
                    ds.Tables["CobroPedido"].Rows.Remove(dr);
                    gvRelacionCobro.DataSource = ds.Tables["CobroPedido"];
                    gvRelacionCobro.DataBind();

                    ActualizaSaldo(referencia, monto, true);
                    CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        DataRow dr;
        string referencia;
        Decimal monto;
        GridViewRow row;
        try
        {
         ////   GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;

         //   for (int i = 0; i <= ds.Tables["CobroPedido"].Rows.Count - 1; i++)
         //   {
         //       //Busco 

         //       if (ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString() == gvRelacionCobro.Rows[row.RowIndex].Cells[0].Text)
         //       {
         //           referencia = ds.Tables["CobroPedido"].Rows[i]["Anio"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Celula"].ToString().TrimEnd() + ds.Tables["CobroPedido"].Rows[i]["Pedido"].ToString().TrimEnd();
         //           monto = (Convert.ToDecimal(ds.Tables["CobroPedido"].Rows[i]["Importe"].ToString()));
         //           dr = ds.Tables["CobroPedido"].Rows[i];
         //           ds.Tables["CobroPedido"].Rows.Remove(dr);
         //           gvRelacionCobro.DataSource = ds.Tables["CobroPedido"];
         //           gvRelacionCobro.DataBind();

         //           ActualizaSaldo(referencia, monto, true);
         //           CargaPedidos(ds.Tables["Pedidos"], idCliente, true);
         //           return;
         //       }
         //   }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
