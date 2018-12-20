// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosPedido
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosPedido : Datos
  {
    private short _celula;
    private short _añoPed;
    private int _pedido;
    private string _pedidoReferencia;
    private byte _tipoPedido;
    private int _cliente;
    private short _celulaCliente;
    private short _rutaCliente;
    private short _rutaSuministro;
    private int _autotanque;
    private double _litros;
    private Decimal _precio;
    private Decimal _importe;
    private Decimal _porcentajeIva;
    private Decimal _iva;
    private string _status;
    private DateTime _fecha;
    private byte _tipoCobro;
    private string _serieRemision;
    private int _folioNota;
    private string _tipoLiquidacion;
    private TipoOperacionDescarga _tipoDescarga;
    private int _consecutivoOrigen;
    private bool _descuentoAplicado;
    private Decimal _importeDescuentoAplicado;
    private string _factura;
    private int _idPedidoCRM;

    public bool DescuentoAplicado
    {
      get
      {
        return this._descuentoAplicado;
      }
      set
      {
        this._descuentoAplicado = value;
      }
    }

    public Decimal ImporteDescuentoAplicado
    {
      get
      {
        return this._importeDescuentoAplicado;
      }
      set
      {
        this._importeDescuentoAplicado = value;
      }
    }

    public int Cliente
    {
      get
      {
        return this._cliente;
      }
      set
      {
        this._cliente = value;
      }
    }

    public short CelulaCliente
    {
      get
      {
        return this._celulaCliente;
      }
      set
      {
        this._celulaCliente = value;
      }
    }

    public short RutaCliente
    {
      get
      {
        return this._rutaCliente;
      }
      set
      {
        this._rutaCliente = value;
      }
    }

    public short Celula
    {
      get
      {
        return this._celula;
      }
    }

    public short AñoPed
    {
      get
      {
        return this._añoPed;
      }
    }

    public int Pedido
    {
      get
      {
        return this._pedido;
      }
    }

    public string PedidoReferencia
    {
      get
      {
        return this._pedidoReferencia;
      }
    }

    public string Status
    {
      get
      {
        return this._status;
      }
      set
      {
        this._status = value;
      }
    }

    public byte TipoPedido
    {
      get
      {
        return this._tipoPedido;
      }
    }

    public short AñoAtt
    {
      get
      {
        return this._AñoAtt;
      }
      set
      {
        this._AñoAtt = value;
      }
    }

    public int Folio
    {
      get
      {
        return this._Folio;
      }
      set
      {
        this._Folio = value;
      }
    }

    public short RutaSuministro
    {
      get
      {
        return this._rutaSuministro;
      }
      set
      {
        this._rutaSuministro = value;
      }
    }

    public int AutoTanque
    {
      get
      {
        return this._autotanque;
      }
      set
      {
        this._autotanque = value;
      }
    }

    public double Litros
    {
      get
      {
        return this._litros;
      }
      set
      {
        this._litros = value;
      }
    }

    public Decimal Precio
    {
      get
      {
        return this._precio;
      }
      set
      {
        this._precio = value;
      }
    }

    public Decimal Importe
    {
      get
      {
        return this._importe;
      }
      set
      {
        this._importe = value;
      }
    }

    public Decimal PorcentajeIVA
    {
      get
      {
        return this._porcentajeIva;
      }
      set
      {
        this._porcentajeIva = value;
      }
    }

    public Decimal IVA
    {
      get
      {
        return this._iva;
      }
      set
      {
        this._iva = value;
      }
    }

    public DateTime FSuministro
    {
      get
      {
        return this._fecha;
      }
      set
      {
        this._fecha = value;
      }
    }

    public byte TipoCobro
    {
      get
      {
        return this._tipoCobro;
      }
      set
      {
        this._tipoCobro = value;
      }
    }

    public string SerieRemision
    {
      get
      {
        return this._serieRemision;
      }
      set
      {
        this._serieRemision = value;
      }
    }

    public int FolioNota
    {
      get
      {
        return this._folioNota;
      }
      set
      {
        this._folioNota = value;
      }
    }

    public string TipoLiquidacion
    {
      get
      {
        return this._tipoLiquidacion;
      }
      set
      {
        this._tipoLiquidacion = value;
      }
    }

    public TipoOperacionDescarga TipoDescarga
    {
      get
      {
        return this._tipoDescarga;
      }
      set
      {
        this._tipoDescarga = value;
      }
    }

    public int ConsecutivoOrigen
    {
      get
      {
        return this._consecutivoOrigen;
      }
      set
      {
        this._consecutivoOrigen = value;
      }
    }

    public string Factura
    {
      get
      {
        return this._factura;
      }
    }

        public int IdPedidoCRM
        {
            get
            {
                return _idPedidoCRM;
            }

            set
            {
                _idPedidoCRM = value;
            }
        }

        public DatosPedido(int Cliente)
    {
      this._cliente = Cliente;
    }

    public DatosPedido(short Celula, short AñoPed, int Pedido)
    {
      this._celula = Celula;
      this._añoPed = AñoPed;
      this._pedido = Pedido;
    }

    public DatosPedido(short AñoAtt, int Folio)
    {
      this._AñoAtt = AñoAtt;
      this._Folio = Folio;
    }

    public DatosPedido(string PedidoReferencia)
    {
      this._pedidoReferencia = PedidoReferencia;
    }

    public DatosPedido(string SerieRemision, int Remision)
    {
      this._serieRemision = SerieRemision;
      this._folioNota = Remision;
    }

    public int LiquidaPedido()
    {
      int num = 0;
      SqlParameter[] sqlParameterArray = new SqlParameter[21]
      {
        new SqlParameter("@AñoAtt", (object) this._AñoAtt),
        new SqlParameter("@Folio", (object) this._Folio),
        new SqlParameter("@Pedido", (object) this._pedido),
        new SqlParameter("@AñoPed", (object) this._añoPed),
        new SqlParameter("@Celula", (object) this._celula),
        new SqlParameter("@Cliente", (object) this._cliente),
        new SqlParameter("@Ruta", (object) this._rutaSuministro),
        new SqlParameter("@Litros", (object) this._litros),
        new SqlParameter("@Precio", (object) this._precio),
        new SqlParameter("@Importe", (object) this._importe),
        new SqlParameter("@Iva", (object) this._iva),
        new SqlParameter("@Autotanque", (object) this._autotanque),
        new SqlParameter("@Tipo", (object) this._tipoCobro),
        new SqlParameter("@Fecha", (object) this._fecha),
        new SqlParameter("@FolioNota", (object) this._folioNota),
        new SqlParameter("@SerieRemision", (object) this._serieRemision),
        new SqlParameter("@TipoLiquidacion", (object) this._tipoLiquidacion),
        new SqlParameter("@ConsecutivoOrigen", (object) this._consecutivoOrigen),
        //new SqlParameter("@DescuentoAplicado", (object) (bool) (this._descuentoAplicado ? 1 : 0)),
        new SqlParameter("@DescuentoAplicado", (object) (bool) (this._descuentoAplicado ? true : false)),
        new SqlParameter("@ImporteDescuentoAplicado", (object) this._importeDescuentoAplicado),
        new SqlParameter("@IdPedidoCRM", (object) this._idPedidoCRM)
      };
      try
      {
        if (this._tipoDescarga == TipoOperacionDescarga.Rampac || this._tipoDescarga == TipoOperacionDescarga.RIVDos)
        {
          this._dataAccess.OpenConnection();
          this._dataAccess.BeginTransaction();
        }
        num = this._dataAccess.ModifyData("spLIQ2LiquidacionPedido", CommandType.StoredProcedure, sqlParameterArray);
        if (this._tipoDescarga != TipoOperacionDescarga.Rampac)
        {
          if (this._tipoDescarga != TipoOperacionDescarga.RIVDos)
            goto label_10;
        }
        this.ActualizaRampac(this._tipoDescarga, this._consecutivoOrigen, true);
        //this._dataAccess.get_Transaction().Commit();
        this._dataAccess.Transaction.Commit();
      }
      catch (Exception ex)
      {
          if (this._tipoDescarga == TipoOperacionDescarga.Rampac || this._tipoDescarga == TipoOperacionDescarga.RIVDos)
          {
              //this._dataAccess.get_Transaction().Rollback();
              this._dataAccess.Transaction.Rollback();
          }
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
label_10:
      return num;
    }

    public int ActualizaRemision()
    {
      int num = 0;
      SqlParameter[] sqlParameterArray1 = new SqlParameter[5]
      {
        new SqlParameter("@Pedido", (object) this._pedido),
        new SqlParameter("@Celula", (object) this._celula),
        new SqlParameter("@AñoPed", (object) this._añoPed),
        new SqlParameter("@FolioNota", (object) this._folioNota),
        new SqlParameter("@SerieRemision", (object) this._serieRemision)
      };
      SqlParameter[] sqlParameterArray2 = new SqlParameter[3]
      {
        new SqlParameter("@PPedido", (object) this._pedido),
        new SqlParameter("@PCelula", (object) this._celula),
        new SqlParameter("@PAñoPed", (object) this._añoPed)
      };
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        num = this._dataAccess.ModifyData("spLIQ2ActualizacionNumeroDeRemision", CommandType.StoredProcedure, sqlParameterArray1);
        this._dataAccess.ModifyData("spRNRelacionaNotaPedidoUnitario", CommandType.StoredProcedure, sqlParameterArray2);
        //this._dataAccess.get_Transaction().Commit();
        this._dataAccess.Transaction.Commit();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
      return num;
    }

    public bool ConsultaDatosPedido()
    {
      bool flag = false;
      SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@Cliente", (object) this._cliente)
      };
      try
      {
        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedidoActivo", CommandType.StoredProcedure, sqlParameterArray);
        this._dataAccess.OpenConnection();
        while (sqlDataReader.Read())
        {
          this._celula = Convert.ToInt16(sqlDataReader["Celula"]);
          this._añoPed = Convert.ToInt16(sqlDataReader["AñoPed"]);
          this._pedido = Convert.ToInt32(sqlDataReader["Pedido"]);
          this._pedidoReferencia = Convert.ToString(sqlDataReader["PedidoReferencia"]);
          this._tipoPedido = Convert.ToByte(sqlDataReader["TipoPedido"]);
          this._serieRemision = sqlDataReader["Serie"] == DBNull.Value ? string.Empty : Convert.ToString(sqlDataReader["Serie"]);
          this._folioNota = sqlDataReader["FolioNota"] == DBNull.Value ? 0 : Convert.ToInt32(sqlDataReader["FolioNota"]);
          flag = true;
        }
        sqlDataReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
      return flag;
    }

    public void ConsultaDatosPedidoLiquidado()
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@Celula", (object) this._celula),
        new SqlParameter("@AñoPed", (object) this._añoPed),
        new SqlParameter("@Pedido", (object) this._pedido)
      };
      try
      {
        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedidoLiquidado", CommandType.StoredProcedure, sqlParameterArray);
        this._dataAccess.OpenConnection();
        while (sqlDataReader.Read())
        {
          this._celula = Convert.ToInt16(sqlDataReader["Celula"]);
          this._añoPed = Convert.ToInt16(sqlDataReader["AñoPed"]);
          this._pedido = Convert.ToInt32(sqlDataReader["Pedido"]);
          this._cliente = Convert.ToInt32(sqlDataReader["Cliente"]);
          this._pedidoReferencia = Convert.ToString(sqlDataReader["PedidoReferencia"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Importe"]);
          this._tipoCobro = Convert.ToByte(sqlDataReader["TipoCobro"]);
          this._tipoPedido = Convert.ToByte(sqlDataReader["TipoPedido"]);
          this._AñoAtt = Convert.ToInt16(sqlDataReader["AñoAtt"]);
          this._Folio = Convert.ToInt32(sqlDataReader["Folio"]);
          this._rutaSuministro = Convert.ToInt16(sqlDataReader["RutaSuministro"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Total"]);
          this._iva = Convert.ToDecimal(sqlDataReader["Impuesto"]);
          this._autotanque = Convert.ToInt32(sqlDataReader["Autotanque"]);
          this._fecha = Convert.ToDateTime(sqlDataReader["FSuministro"]);
          this._serieRemision = Convert.ToString(sqlDataReader["SerieRemision"]);
          this._folioNota = Convert.ToInt32(sqlDataReader["Remision"]);
          this._consecutivoOrigen = Convert.ToInt32(sqlDataReader["ConsecutivoOrigen"]);
        }
        sqlDataReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
    }

    public void ConsultaDatosPedidoLiquidadoRemision()
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@SerieRemision", (object) this._serieRemision),
        new SqlParameter("@Remision", (object) this._folioNota)
      };
      try
      {
        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedidoLiquidadoRemision", CommandType.StoredProcedure, sqlParameterArray);
        this._dataAccess.OpenConnection();
        while (sqlDataReader.Read())
        {
          this._celula = Convert.ToInt16(sqlDataReader["Celula"]);
          this._añoPed = Convert.ToInt16(sqlDataReader["AñoPed"]);
          this._pedido = Convert.ToInt32(sqlDataReader["Pedido"]);
          this._cliente = Convert.ToInt32(sqlDataReader["Cliente"]);
          this._pedidoReferencia = Convert.ToString(sqlDataReader["PedidoReferencia"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Importe"]);
          this._tipoCobro = Convert.ToByte(sqlDataReader["TipoCobro"]);
          this._tipoPedido = Convert.ToByte(sqlDataReader["TipoPedido"]);
          this._AñoAtt = Convert.ToInt16(sqlDataReader["AñoAtt"]);
          this._Folio = Convert.ToInt32(sqlDataReader["Folio"]);
          this._rutaSuministro = Convert.ToInt16(sqlDataReader["RutaSuministro"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Total"]);
          this._iva = Convert.ToDecimal(sqlDataReader["Impuesto"]);
          this._autotanque = Convert.ToInt32(sqlDataReader["Autotanque"]);
          this._fecha = Convert.ToDateTime(sqlDataReader["FSuministro"]);
          this._serieRemision = Convert.ToString(sqlDataReader["SerieRemision"]);
          this._folioNota = Convert.ToInt32(sqlDataReader["Remision"]);
          this._consecutivoOrigen = Convert.ToInt32(sqlDataReader["ConsecutivoOrigen"]);
          this._factura = Convert.ToString(sqlDataReader["Factura"]);
        }
        if(!sqlDataReader.HasRows) 
        {
            sqlDataReader.Close();
            throw new Exception("No se encontró pedido");
        }
        sqlDataReader.Close();

      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
    }

    public void ConsultaDatosPedidoLiquidadoPedidoReferencia()
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@PedidoReferencia", (object) this._pedidoReferencia)
      };
      try
      {
        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedidoLiquidadoPedidoReferencia", CommandType.StoredProcedure, sqlParameterArray);
        this._dataAccess.OpenConnection();
        while (sqlDataReader.Read())
        {
          this._celula = Convert.ToInt16(sqlDataReader["Celula"]);
          this._añoPed = Convert.ToInt16(sqlDataReader["AñoPed"]);
          this._pedido = Convert.ToInt32(sqlDataReader["Pedido"]);
          this._cliente = Convert.ToInt32(sqlDataReader["Cliente"]);
          this._pedidoReferencia = Convert.ToString(sqlDataReader["PedidoReferencia"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Importe"]);
          this._tipoCobro = Convert.ToByte(sqlDataReader["TipoCobro"]);
          this._tipoPedido = Convert.ToByte(sqlDataReader["TipoPedido"]);
          this._AñoAtt = Convert.ToInt16(sqlDataReader["AñoAtt"]);
          this._Folio = Convert.ToInt32(sqlDataReader["Folio"]);
          this._rutaSuministro = Convert.ToInt16(sqlDataReader["RutaSuministro"]);
          this._litros = Convert.ToDouble(sqlDataReader["Litros"]);
          this._precio = Convert.ToDecimal(sqlDataReader["Precio"]);
          this._importe = Convert.ToDecimal(sqlDataReader["Total"]);
          this._iva = Convert.ToDecimal(sqlDataReader["Impuesto"]);
          this._autotanque = Convert.ToInt32(sqlDataReader["Autotanque"]);
          this._fecha = Convert.ToDateTime(sqlDataReader["FSuministro"]);
          this._serieRemision = Convert.ToString(sqlDataReader["SerieRemision"]);
          this._folioNota = Convert.ToInt32(sqlDataReader["Remision"]);
          this._consecutivoOrigen = Convert.ToInt32(sqlDataReader["ConsecutivoOrigen"]);
          this._factura = Convert.ToString(sqlDataReader["Factura"]);
        }
       

        if(!sqlDataReader.HasRows) 
        {
            sqlDataReader.Close();
            throw new Exception("No se encontró pedido");
        }
         sqlDataReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
    }

    public void ConsultaPedido()
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@Celula", (object) this._celula),
        new SqlParameter("@AñoPed", (object) this._añoPed),
        new SqlParameter("@Pedido", (object) this._pedido)
      };
      try
      {
        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedido", CommandType.StoredProcedure, sqlParameterArray);
        while (sqlDataReader.Read())
          this._status = Convert.ToString(sqlDataReader["Status"]);
        sqlDataReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
    }

    public void AltaPedido(short AñoAtt, int Folio, short CelulaCliente, short RutaCliente, DateTime Fecha, string Usuario, int IdPedidoCRM)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[10]
      {
        new SqlParameter("@Cliente", (object) this._cliente),
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio),
        new SqlParameter("@Fecha", (object) Fecha),
        new SqlParameter("@RutaCliente", (object) RutaCliente),
        new SqlParameter("@Usuario", (object) Usuario),
        new SqlParameter("@Celula", (object) CelulaCliente),
        new SqlParameter("@Pedido", SqlDbType.Int),        
        null,
        new SqlParameter("@IdPedidoCRM", (object) IdPedidoCRM)
      };
      sqlParameterArray[7].Direction = ParameterDirection.Output;
      sqlParameterArray[8] = new SqlParameter("@AñoPed", SqlDbType.SmallInt);
      sqlParameterArray[8].Direction = ParameterDirection.Output;
      try
      {
        this._dataAccess.ModifyData("spLIQ2GeneraPedido", CommandType.StoredProcedure, sqlParameterArray);
        this._celula = Convert.ToInt16(sqlParameterArray[6].Value);
        this._pedido = Convert.ToInt32(sqlParameterArray[7].Value);
        this._añoPed = Convert.ToInt16(sqlParameterArray[8].Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
    }

    public int DesasignaPedido(short AñoAtt, int Folio, short Celula, short AñoPed, int Pedido, TipoDesasignacionPedido TipoDesasignacion)
    {
      int num = 0;
      SqlParameter[] sqlParameterArray = new SqlParameter[5]
      {
        new SqlParameter("@AñoAtt", (object) this._AñoAtt),
        new SqlParameter("@Folio", (object) this._Folio),
        new SqlParameter("@Pedido", (object) this._pedido),
        new SqlParameter("@AñoPed", (object) this._añoPed),
        new SqlParameter("@Celula", (object) this._celula)
      };
      try
      {
        num = TipoDesasignacion != TipoDesasignacionPedido.BorrarNotaBlanca ? this._dataAccess.ModifyData("spLIQ2DesasignaPedidoReactivar", CommandType.StoredProcedure, sqlParameterArray) : this._dataAccess.ModifyData("spLIQ2DesasignaPedidoBorrar", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
      return num;
    }

    public int ActualizaRampac(TipoOperacionDescarga TipoDescarga, int Consecutivo, bool Liquidado)
    {
      int num = 0;
      SqlParameter[] sqlParameterArray = new SqlParameter[4]
      {
        new SqlParameter("@AñoAtt", (object) this._AñoAtt),
        new SqlParameter("@Folio", (object) this._Folio),
        new SqlParameter("@Consecutivo", (object) Consecutivo),
        //new SqlParameter("@Liquidado", (object) (bool) (Liquidado ? 1 : 0))
        new SqlParameter("@Liquidado", (object) (bool) (Liquidado ? true : false))
      };
      try
      {
        if (TipoDescarga == TipoOperacionDescarga.Rampac)
          num = this._dataAccess.ModifyData("spLIQ2ActualizaRampac", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return num;
    }

    public Decimal CreditoDisponibleOperador(short AñoAtt, int Folio)
    {
      Decimal num = new Decimal(0);
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        return Convert.ToDecimal(this._dataAccess.LoadScalarData("spLIQ2ConsultaLimiteDisponibleOperador", CommandType.StoredProcedure, sqlParameterArray));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
