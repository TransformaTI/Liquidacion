// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosRegistroPago
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  public class DatosRegistroPago : Datos
  {
   #region variables
    private DataTable dtPedidos;
    private DataTable dtBancos;
    private DataTable dtCliente;
    private DataTable dtPromociones;
    private DataTable dtAutoTanque;
    private DataTable dtPagosConTarjeta;
        #endregion
        #region propiedades
        public DataTable Pedidos
    {
      get
      {
        return this.dtPedidos;
      }
    }

    public DataTable Bancos
    {
      get
      {
        return this.dtBancos;
      }
    }

    public DataTable Cliente
    {
      get
      {
        return this.dtCliente;
      }
    }

    public DataTable Promociones
    {
      get
      {
        return this.dtPromociones;
      }
    }

    public DataTable AutoTanque
    {
      get
      {
        return this.dtAutoTanque;
      }
    }

    public DataTable PagosConTarjeta
        {
            get
            {
                return this.dtPagosConTarjeta;
            }

        }
        #endregion


        public void CargaPedidos()
    {
      this.dtPedidos = new DataTable();
      this._dataAccess.LoadData(this.dtPedidos, "SELECT p.litros,p.pedidoreferencia,p.Cliente,isnull(p.total,0) as total,isnull(p.saldo,0) as saldo,p.celula,p.añoped,p.pedido,'Pedido: '+rtrim(convert(varchar,p.pedidoreferencia))+' Cliente: '+convert(varchar,p.cliente)+' '+substring(rtrim(c.nombre),0,20)+' Litros: '+convert(varchar,isnull(p.litros,0)) as Descripcion from pedido p inner join cliente c on (p.cliente = c.cliente) where AñoAtt = 2008 AND Folio = 45980 AND p.tipocobro = 5", CommandType.Text, (SqlParameter[]) null, true);
    }

    public void CargaBancos()
    {
      this.dtBancos = new DataTable();
      this._dataAccess.LoadData(this.dtBancos, "spLIQ2ConsultaBancos", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    public void CargaPromociones()
    {
      this.dtPromociones = new DataTable();
      this._dataAccess.LoadData(this.dtBancos, "spLIQ2ConsultaPromocionesVale", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    public void CargaCliente(int Cliente)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@Cliente", (object) Cliente)
      };
      this.dtCliente = new DataTable();
      this._dataAccess.LoadData(this.dtCliente, "spLIQ2ConsultaDatosCliente", CommandType.StoredProcedure, sqlParameterArray, true);
    }
        /// <summary>
        /// Devuelve datatable con pagos de tarjeta del cliente
        /// </summary>
        /// <param name="NumCliente"></param>
    public void CargaPagosConTarjeta(int NumCliente,int Ruta,int Autotanque)
    {
         SqlParameter[] sqlParameterArray = new SqlParameter[3]
         {
            new SqlParameter("@Cliente",NumCliente),
            new SqlParameter("@Ruta",Ruta),
            new SqlParameter("@Autotanque",Autotanque),

         };
            this.dtPagosConTarjeta = new DataTable();
            this._dataAccess.LoadData(this.dtPagosConTarjeta, "spCBConsultarCargoTarjetaCliente", CommandType.StoredProcedure, sqlParameterArray, true);
        }



        private void CobroEnEfectivo(string Usuario, DataTable dtPedidos, ref DataTable dtPago, ref DataTable dtDetallePago)
    {
      DataRow[] dataRowArray = dtPedidos.Select("Saldo > 0");
      DataRow row1 = (DataRow) null;
      Decimal num1 = new Decimal(0);
      int num2 = 1;
      if (dtPedidos != null && dtPedidos.Rows.Count > 0)
        num1 = Convert.ToDecimal(dtPedidos.Compute("SUM(Saldo)", (string) null));
      if (dtPago != null && dtPago.Rows.Count > 0)
      {
        num2 = Convert.ToInt32(dtPago.Compute("MAX(IdPago)", (string) null)) + 1;
      }
      else
      {
        dtPago = new DataTable();
        dtPago.Columns.Add("IdPago", Type.GetType("System.Int32"));
        dtPago.Columns.Add(new DataColumn("Referencia"));
        dtPago.Columns.Add(new DataColumn("NumeroCuenta"));
        dtPago.Columns.Add(new DataColumn("FechaCheque"));
        dtPago.Columns.Add(new DataColumn("Cliente"));
        dtPago.Columns.Add(new DataColumn("Banco"));
        dtPago.Columns.Add(new DataColumn("Importe"));
        dtPago.Columns.Add(new DataColumn("Impuesto"));
        dtPago.Columns.Add(new DataColumn("Total"));
        dtPago.Columns.Add(new DataColumn("Saldo"));
        dtPago.Columns.Add(new DataColumn("Observaciones"));
        dtPago.Columns.Add(new DataColumn("Status"));
        dtPago.Columns.Add(new DataColumn("FechaAlta"));
        dtPago.Columns.Add(new DataColumn("TipoCobro"));
        dtPago.Columns.Add(new DataColumn("Usuario"));
        dtPago.Columns.Add(new DataColumn("SaldoAFavor"));
        dtPago.Columns.Add(new DataColumn("TPV"));
        dtPago.Columns.Add(new DataColumn("FechaDeposito"));
        dtPago.Columns.Add(new DataColumn("BancoOrigen"));
        dtPago.Columns.Add(new DataColumn("NombreTipoCobro"));
        dtDetallePago = new DataTable();
        dtDetallePago.Columns.Add(new DataColumn("IdPago"));
        dtDetallePago.Columns.Add(new DataColumn("Pedido"));
        dtDetallePago.Columns.Add(new DataColumn("Celula"));
        dtDetallePago.Columns.Add(new DataColumn("Anio"));
        dtDetallePago.Columns.Add(new DataColumn("Importe"));
        dtDetallePago.Columns.Add(new DataColumn("Impuesto"));
        dtDetallePago.Columns.Add(new DataColumn("Total"));
      }
      foreach (DataRow dataRow in dataRowArray)
      {
        if (row1 == null)
        {
          row1 = dtPago.NewRow();
          row1.BeginEdit();
          row1["IdPago"] = (object) num2;
          row1["Referencia"] = (object) DBNull.Value;
          row1["NumeroCuenta"] = (object) DBNull.Value;
          row1["FechaCheque"] = (object) DateTime.Now.Date;
          row1["Cliente"] = (object) 0;
          row1["Banco"] = (object) 0;
          row1["Importe"] = (object) 0;
          row1["Impuesto"] = (object) 0;
          row1["Total"] = (object) num1;
          row1["Saldo"] = (object) 0;
          row1["Observaciones"] = (object) "";
          row1["Status"] = (object) "EMITIDO";
          row1["FechaAlta"] = (object) DateTime.Now.Date;
          row1["TipoCobro"] = (object) 5;
          row1["Usuario"] = (object) Usuario;
          row1["SaldoAFavor"] = (object) DBNull.Value;
          row1["TPV"] = (object) DBNull.Value;
          row1["FechaDeposito"] = (object) DateTime.Now.Date;
          row1["BancoOrigen"] = (object) 0;
          row1["NombreTipoCobro"] = (object) "EFECTIVO";
          row1.EndEdit();
          dtPago.Rows.Add(row1);
        }
        DataRow row2 = dtDetallePago.NewRow();
        row2.BeginEdit();
        row2["IdPago"] = (object) num2;
        row2["Pedido"] = dataRow["Pedido"];
        row2["Celula"] = dataRow["Celula"];
        row2["Anio"] = dataRow["AñoPed"];
        row2["Importe"] = dataRow["Total"];
        row2["Impuesto"] = (object) 0;
        row2["Total"] = dataRow["Saldo"];
        row2.EndEdit();
        dtDetallePago.Rows.Add(row2);
      }
    }

    private void CobroDescuentos(string Usuario, DataTable dtPedidos, ref DataTable dtPago, ref DataTable dtDetallePago)
    {
      DataRow[] dataRowArray = dtPedidos.Select("Descuento > 0", "Cliente ASC");
      string str = "";
      Decimal num1 = new Decimal(0);
      int num2 = 1;
      if (dtPago != null)
        num2 = dtPago.Rows.Count <= 0 ? 1 : Convert.ToInt32(dtPago.Compute("MAX(IdPago)", (string) null)) + 1;
      foreach (DataRow dataRow1 in dataRowArray)
      {
        if (Convert.ToDecimal(dataRow1["Saldo"]) >= Convert.ToDecimal(dataRow1["Descuento"]))
        {
          if (str == dataRow1["Cliente"].ToString())
          {
            foreach (DataRow dataRow2 in (InternalDataCollectionBase) dtPago.Rows)
            {
              if (str == dataRow2["Cliente"].ToString() && dataRow2["TipoCobro"].ToString() == "12")
              {
                dataRow2.BeginEdit();
                dataRow2["Total"] = (object) (Convert.ToDecimal(dataRow2["Total"]) + Convert.ToDecimal(dataRow1["Descuento"]));
                dataRow2.EndEdit();
                str = dataRow1["Cliente"].ToString();
              }
            }
          }
          else
          {
            if (dtPago != null && dtPago.Rows.Count > 0)
            {
              num2 = Convert.ToInt32(dtPago.Compute("MAX(IdPago)", (string) null)) + 1;
            }
            else
            {
              dtPago = new DataTable();
              dtPago.Columns.Add("IdPago", Type.GetType("System.Int32"));
              dtPago.Columns.Add(new DataColumn("Referencia"));
              dtPago.Columns.Add(new DataColumn("NumeroCuenta"));
              dtPago.Columns.Add(new DataColumn("FechaCheque"));
              dtPago.Columns.Add(new DataColumn("Cliente"));
              dtPago.Columns.Add(new DataColumn("Banco"));
              dtPago.Columns.Add(new DataColumn("Importe"));
              dtPago.Columns.Add(new DataColumn("Impuesto"));
              dtPago.Columns.Add(new DataColumn("Total"));
              dtPago.Columns.Add(new DataColumn("Saldo"));
              dtPago.Columns.Add(new DataColumn("Observaciones"));
              dtPago.Columns.Add(new DataColumn("Status"));
              dtPago.Columns.Add(new DataColumn("FechaAlta"));
              dtPago.Columns.Add(new DataColumn("TipoCobro"));
              dtPago.Columns.Add(new DataColumn("Usuario"));
              dtPago.Columns.Add(new DataColumn("SaldoAFavor"));
              dtPago.Columns.Add(new DataColumn("TPV"));
              dtPago.Columns.Add(new DataColumn("FechaDeposito"));
              dtPago.Columns.Add(new DataColumn("BancoOrigen"));
              dtPago.Columns.Add(new DataColumn("NombreTipoCobro"));
              dtDetallePago = new DataTable();
              dtDetallePago.Columns.Add(new DataColumn("IdPago"));
              dtDetallePago.Columns.Add(new DataColumn("Pedido"));
              dtDetallePago.Columns.Add(new DataColumn("Celula"));
              dtDetallePago.Columns.Add(new DataColumn("Anio"));
              dtDetallePago.Columns.Add(new DataColumn("Importe"));
              dtDetallePago.Columns.Add(new DataColumn("Impuesto"));
              dtDetallePago.Columns.Add(new DataColumn("Total"));
            }
            Decimal num3 = Convert.ToDecimal(dataRow1["Descuento"]);
            DataRow row = dtPago.NewRow();
            row.BeginEdit();
            row["IdPago"] = (object) num2;
            row["Referencia"] = (object) DBNull.Value;
            row["NumeroCuenta"] = (object) DBNull.Value;
            row["FechaCheque"] = (object) DateTime.Now.Date;
            row["Cliente"] = dataRow1["Cliente"];
            row["Banco"] = (object) 0;
            row["Importe"] = (object) 0;
            row["Impuesto"] = (object) 0;
            row["Total"] = (object) num3;
            row["Saldo"] = (object) 0;
            row["Observaciones"] = (object) "";
            row["Status"] = (object) "EMITIDO";
            row["FechaAlta"] = (object) DateTime.Now.Date;
            row["TipoCobro"] = (object) 12;
            row["Usuario"] = (object) Usuario;
            row["SaldoAFavor"] = (object) DBNull.Value;
            row["TPV"] = (object) DBNull.Value;
            row["FechaDeposito"] = (object) DateTime.Now.Date;
            row["BancoOrigen"] = (object) 0;
            row["NombreTipoCobro"] = (object) "DESCUENTO";
            row.EndEdit();
            dtPago.Rows.Add(row);
          }
          DataRow row1 = dtDetallePago.NewRow();
          row1.BeginEdit();
          row1["IdPago"] = (object) num2;
          row1["Pedido"] = dataRow1["Pedido"];
          row1["Celula"] = dataRow1["Celula"];
          row1["Anio"] = dataRow1["AñoPed"];
          row1["Importe"] = dataRow1["Descuento"];
          row1["Impuesto"] = (object) 0;
          row1["Total"] = dataRow1["Descuento"];
          row1.EndEdit();
          dtDetallePago.Rows.Add(row1);
          str = dataRow1["Cliente"].ToString();
          dataRow1.BeginEdit();
          dataRow1["Saldo"] = (object) (Convert.ToDecimal(dataRow1["Saldo"]) - Convert.ToDecimal(dataRow1["Descuento"]));
          dataRow1.EndEdit();
        }
      }
    }

    private void CancelaCobrosEfectivoDescuentos(ref DataTable dtPago, ref DataTable dtDetallePago)
    {
      try
      {
        foreach (DataRow row1 in dtPago.Select("NombreTipoCobro in ('EFECTIVO', 'DESCUENTO')"))
        {
          foreach (DataRow row2 in dtDetallePago.Select("IdPago = " + row1["IdPago"].ToString()))
            dtDetallePago.Rows.Remove(row2);
          dtPago.Rows.Remove(row1);
        }
      }
      catch
      {
      }
    }

    public void GuardaPagos(string Usuario, DataTable dtPedidos, DataTable dtPago, DataTable dtDetallePago, DataTable dtResumenLiquidacion)
    {
      try
      {
        this.CobroDescuentos(Usuario, dtPedidos, ref dtPago, ref dtDetallePago);
        this.CobroEnEfectivo(Usuario, dtPedidos, ref dtPago, ref dtDetallePago);
        this.ValidaStatusAutotanqueTurno((int) Convert.ToInt16(dtResumenLiquidacion.Rows[0]["AñoAtt"]), Convert.ToInt32(dtResumenLiquidacion.Rows[0]["Folio"]));
        if (!(this.dtAutoTanque.Rows[0]["StatusLogistica"].ToString().Trim() == "CIERRE"))
          return;
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        for (int index1 = 0; index1 <= dtPago.Rows.Count - 1; ++index1)
        {
          SqlParameter[] sqlParameterArray = new SqlParameter[16]
          {
            new SqlParameter("@NumeroCheque", (object) dtPago.Rows[index1]["Referencia"].ToString()),
            new SqlParameter("@Total", (object) Convert.ToDecimal(dtPago.Rows[index1]["Total"])),
            new SqlParameter("@Saldo", (object) Convert.ToDecimal(dtPago.Rows[index1]["Saldo"])),
            new SqlParameter("@NumeroCuenta", (object) dtPago.Rows[index1]["NumeroCuenta"].ToString()),
            new SqlParameter("@FCheque", (object) Convert.ToDateTime(dtPago.Rows[index1]["FechaCheque"].ToString())),
            new SqlParameter("@Cliente", (object) Convert.ToInt32(dtPago.Rows[index1]["Cliente"])),
            new SqlParameter("@Banco", (object) Convert.ToInt16(dtPago.Rows[index1]["Banco"].ToString())),
            new SqlParameter("@Observaciones", (object) dtPago.Rows[index1]["Observaciones"].ToString()),
            new SqlParameter("@Estatus", (object) "EMITIDO"),
            new SqlParameter("@TipoCobro", (object) dtPago.Rows[index1]["TipoCobro"].ToString()),
            new SqlParameter("@Usuario", (object) dtPago.Rows[index1]["Usuario"].ToString()),
            new SqlParameter("@SaldoAFavor", (object) dtPago.Rows[index1]["SaldoAFavor"].ToString()),
            new SqlParameter("@TPV", (object) dtPago.Rows[index1]["TPV"].ToString()),
            new SqlParameter("@BancoTarjeta", (object) Convert.ToInt16(dtPago.Rows[index1]["Banco"].ToString())),
            new SqlParameter("@AñoCobro", SqlDbType.SmallInt),
            null
          };
          sqlParameterArray[14].Direction = ParameterDirection.Output;
          sqlParameterArray[15] = new SqlParameter("@Cobro", SqlDbType.Int);
          sqlParameterArray[15].Direction = ParameterDirection.Output;
          this._dataAccess.ModifyData("spCCLiqAltaCobroLiquidacion", CommandType.StoredProcedure, sqlParameterArray);
          int num1 = Convert.ToInt32(sqlParameterArray[15].Value);
          int num2 = (int) Convert.ToInt16(sqlParameterArray[14].Value);
          for (int index2 = 0; index2 <= dtDetallePago.Rows.Count - 1; ++index2)
          {
            if (dtPago.Rows[index1]["IdPago"].ToString() == dtDetallePago.Rows[index2]["idPago"].ToString())
              this._dataAccess.ModifyData("spCobroPedidoAlta", CommandType.StoredProcedure, new SqlParameter[6]
              {
                new SqlParameter("@Celula", (object) Convert.ToInt16(dtDetallePago.Rows[index2]["Celula"])),
                new SqlParameter("@AnoCobro", (object) num2),
                new SqlParameter("@Cobro", (object) num1),
                new SqlParameter("@AnoPed", (object) Convert.ToInt16(dtDetallePago.Rows[index2]["Anio"])),
                new SqlParameter("@Pedido", (object) dtDetallePago.Rows[index2]["Pedido"].ToString()),
                new SqlParameter("@Total", (object) Convert.ToDecimal(dtDetallePago.Rows[index2]["Total"]))
              });
          }
        }
        this.ActualizaTerminado(dtResumenLiquidacion);
        //this._dataAccess.get_Transaction().Commit();
        this._dataAccess.Transaction.Commit();
      }
      catch
      {
        this.CancelaCobrosEfectivoDescuentos(ref dtPago, ref dtDetallePago);
        //this._dataAccess.get_Transaction().Rollback();
        this._dataAccess.Transaction.Rollback();
        throw;
      }
    }

    public void ActualizaTerminado(DataTable dtResumenLiquidacion)
    {
      try
      {
        for (int index = 0; index <= dtResumenLiquidacion.Rows.Count - 1; ++index)
        {
          this._dataAccess.ModifyData("spCCLIQ2ActualizacionFinalAutotanqueturno", CommandType.StoredProcedure, new SqlParameter[8]
          {
            new SqlParameter("@ImporteCredito", (object) Convert.ToDecimal(dtResumenLiquidacion.Rows[index]["ImporteCredito"])),
            new SqlParameter("@LitrosCredito", (object) Convert.ToDouble(dtResumenLiquidacion.Rows[index]["LitrosCredito"])),
            new SqlParameter("@ImporteContado", (object) Convert.ToDecimal(dtResumenLiquidacion.Rows[index]["ImporteContado"])),
            new SqlParameter("@LitrosContado", (object) Convert.ToDouble(dtResumenLiquidacion.Rows[index]["LitrosContado"])),
            new SqlParameter("@TipoLiquidacion", (object) dtResumenLiquidacion.Rows[index]["TipoLiquidacion"].ToString()),
            new SqlParameter("@Usuario", (object) dtResumenLiquidacion.Rows[index]["Usuario"].ToString()),
            new SqlParameter("@AñoAtt", (object) Convert.ToInt16(dtResumenLiquidacion.Rows[index]["AñoAtt"])),
            new SqlParameter("@Folio", (object) Convert.ToInt32(dtResumenLiquidacion.Rows[index]["Folio"]))
          });
          this._dataAccess.ModifyData("spCargoEficienciaAlta", CommandType.StoredProcedure, new SqlParameter[2]
          {
            new SqlParameter("@AñoAtt", (object) Convert.ToInt16(dtResumenLiquidacion.Rows[index]["AñoAtt"])),
            new SqlParameter("@Folio", (object) Convert.ToInt32(dtResumenLiquidacion.Rows[index]["Folio"]))
          });
          this._dataAccess.ModifyData("spRNRelacionaNotaPedidoPorFolio", CommandType.StoredProcedure, new SqlParameter[2]
          {
            new SqlParameter("@AñoAtt", (object) Convert.ToInt16(dtResumenLiquidacion.Rows[index]["AñoAtt"])),
            new SqlParameter("@Folio", (object) Convert.ToInt32(dtResumenLiquidacion.Rows[index]["Folio"]))
          });
        }
      }
      catch
      {
        throw;
      }
    }

    public void ValidaStatusAutotanqueTurno(int AñoAtt, int Folio)
    {
      try
      {
        SqlParameter[] sqlParameterArray = new SqlParameter[2]
        {
          new SqlParameter("@añoatt", (object) AñoAtt),
          new SqlParameter("@folio", (object) Folio)
        };
        this.dtAutoTanque = new DataTable();
        this._dataAccess.LoadData(this.dtAutoTanque, "spLIQ2ConsultaStatusLogistica", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
