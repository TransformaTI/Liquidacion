// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosFolio
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using DocumentosBSR;
using RTGMGateway;
using SGDAC;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosFolio : Datos
  {

    private string _usuario;

        public string Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
            }
        }

        public DatosFolio(short AñoAtt, int Folio)
    {
      this._AñoAtt = AñoAtt;
      this._Folio = Folio;
    }

    public DataTable ConsultaDatosFolio()
    {
      return this.ConsultaDatosPorFolio("spLiq2ConsultaDatosFolio", CommandType.StoredProcedure);
    }

    //public RTGMCore.DireccionEntrega obtenDireccionEntrega(int Cliente)
    //{
    //    RTGMCore.DireccionEntrega objDireccionEntega = new RTGMCore.DireccionEntrega();
    //    try
    //    {


    //        RTGMGateway.RTGMGateway objGateway = new RTGMGateway.RTGMGateway();
    //        objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
    //        SolicitudGateway objRequest = new SolicitudGateway
    //        {
    //            Fuente = RTGMCore.Fuente.Sigamet,
    //            IDCliente = Cliente,

    //        };
    //        objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);
    //    }
    //    catch (Exception ex)
    //    {
    //       // throw ex;
    //    }
    //    return objDireccionEntega;

    //}

        public void ConsultaListaPedidos(short AñoAtt, int Folio, DataTable ListaPedidos)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };

      try
      {
        if (this._dataAccess==null)
         {
                    TextReader textReader = (TextReader)new StreamReader(HttpContext.Current.Server.MapPath("Conexion.txt"));
                _conexion = textReader.ReadLine();
                this._dataAccess = new DAC(new SqlConnection(_conexion));
                textReader.Close();
         };
          this._dataAccess.OpenConnection();       
            

        SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaPedidosFolio", CommandType.StoredProcedure, sqlParameterArray);
        int num = 0;
               
        while (sqlDataReader.Read())
        {
          ++num;
          DataRow row = ListaPedidos.NewRow();

          
            
          Cliente clienteTemp = new Cliente(int.Parse(sqlDataReader["Cliente"].ToString()), 1) ;
          clienteTemp.ConsultaNombreCliente();


//          objDireccionEntega = obtenDireccionEntrega();

          row["ID"] = (object) num;
          row["Cliente"] = sqlDataReader["Cliente"];
          row["Celula"] = sqlDataReader["Celula"];
          row["AñoPed"] = sqlDataReader["AñoPed"];
          row["Pedido"] = sqlDataReader["Pedido"];
          //row["Nombre"] = sqlDataReader["Nombre"];
          row["Nombre"] = clienteTemp.Nombre;
          row["PedidoReferencia"] = sqlDataReader["PedidoReferencia"];
          row["Litros"] = sqlDataReader["Litros"];
          row["Precio"] = sqlDataReader["Precio"];
          row["Importe"] = sqlDataReader["Importe"];
          row["FormaPago"] = sqlDataReader["FormaPago"];
          row["TipoPedido"] = sqlDataReader["TipoPedido"];
          row["Status"] = (object) "CONCILIADO";
          row["FolioRemision"] = sqlDataReader["FolioRemision"];
          row["ConsecutivoOrigen"] = sqlDataReader["ConsecutivoOrigen"];
          row["IdCRM"] = clienteTemp.IdPedidoCRM;


          ListaPedidos.Rows.Add(row);
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

    public DataTable DescargaRampac(short AñoAtt, int Folio)
    {
      DataTable dataTable = new DataTable("Suministros");
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio),
        new SqlParameter("@Carburacion", (object) false)
      };
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.LoadData(dataTable, "spLIQ2DescargaRampac", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this._dataAccess.CloseConnection();
      }
      return dataTable;
    }

    public DataTable InicioLiquidacion(short AñoAtt, int Folio, string Usuario)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio),
        new SqlParameter("@Usuario", (object) Usuario)
      };
      try
      {
        this._dataAccess.LoadData(dataTable, "spLIQ2InicioLiquidacion", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public bool ReinicioLiquidacion(short AñoAtt, int Folio, string Usuario)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio),
        new SqlParameter("@Usuario", (object) Usuario)
      };
      try
      {
        this._dataAccess.ModifyData("spLIQ2AltaRenicioLiquidacion", CommandType.StoredProcedure, sqlParameterArray);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void TerminoLiquidacion(short AñoAtt, int Folio)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        if (this._dataAccess == null)
          this.DataCompInitialize();
        this._dataAccess.ModifyData("spLIQ2AltaFinLiquidacion", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void AltaInicioLiquidacionFolio(short AñoAtt, int Folio, string TipoLiquidacion)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio),
        new SqlParameter("@TipoLiquidacion", (object) TipoLiquidacion)
      };
      try
      {
        if (this._dataAccess == null)
          this.DataCompInitialize();
        this._dataAccess.ModifyData("spLIQ2AutoTanqueTurnoInicioLiquidacion", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool ActualizarBloqueLiquidaciones(DataTable tablaDatos)
    {
      bool flag = false;
      if (tablaDatos.Rows.Count > 0)
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        foreach (DataRow dataRow in (InternalDataCollectionBase) tablaDatos.Rows)
        {
          try
          {
            SqlParameter[] sqlParameterArray = new SqlParameter[5]
            {
              new SqlParameter("@Celula", (object) int.Parse(dataRow["Celula"].ToString().Trim())),
              new SqlParameter("@AñoPed", (object) int.Parse(dataRow["AñoPed"].ToString().Trim())),
              new SqlParameter("@Pedido", (object) int.Parse(dataRow["Pedido"].ToString().Trim())),
              null,
              null
            };
            SerieDocumento.SeparaSerie(dataRow["FolioRemision"].ToString());
            //sqlParameterArray[3] = new SqlParameter("@SerieRemision", (object) SerieDocumento.get_Serie());
            sqlParameterArray[3] = new SqlParameter("@SerieRemision", (object)SerieDocumento.Serie);
            //sqlParameterArray[4] = new SqlParameter("@Remision", (object) SerieDocumento.get_FolioNota());
            sqlParameterArray[4] = new SqlParameter("@Remision", (object)SerieDocumento.FolioNota);
            this._dataAccess.ModifyData("spLIQ2ActualizaRemision", CommandType.StoredProcedure, sqlParameterArray);
          }
          catch (Exception ex)
          {
            flag = true;
            //this._dataAccess.get_Transaction().Rollback();
            this._dataAccess.Transaction.Rollback();
            throw ex;
          }
        }
        if (!flag)
          //this._dataAccess.get_Transaction().Commit();
          this._dataAccess.Transaction.Commit();
      }
      return !flag;
    }

    public void CancelacionCobros(short AñoAtt, int Folio)
    {
        DataTable dataTable = this.ConsultaCobrosCancelacion(AñoAtt, Folio);
        try
        {
            this._dataAccess.OpenConnection();
            this._dataAccess.BeginTransaction();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in (InternalDataCollectionBase) dataTable.Rows)
                {
                    this.EliminaMovimientoAConciliarCobro(Convert.ToInt16(dataRow["AñoCobro"]), Convert.ToInt32(dataRow["Cobro"]));
                    this.EliminaCobroPedido(Convert.ToInt16(dataRow["AñoCobro"]), Convert.ToInt32(dataRow["Cobro"]));
                    this.EliminaCobro(Convert.ToInt16(dataRow["AñoCobro"]), Convert.ToInt32(dataRow["Cobro"]));
                }
            }
            this.EliminaEficienciaNegativa(AñoAtt, Folio);
            this.ReactivaAutoTanqueTurno(AñoAtt, Folio);
            //this._dataAccess.get_Transaction().Commit();
            this._dataAccess.Transaction.Commit();
        }
        catch (Exception ex)
        {
            //this._dataAccess.get_Transaction().Rollback();
            this._dataAccess.Transaction.Rollback();
            throw ex;
        }
        finally
        {
            this._dataAccess.CloseConnection();
        }
    }

    public void EliminaMovimientoAConciliarCobro(short AñoCobro, int Cobro)
    {
        SqlParameter[] sqlParameterArray = new SqlParameter[2]
        {
            new SqlParameter("@AñoCobro", (object) AñoCobro),
            new SqlParameter("@Cobro", (object) Cobro)
        };
        try
        {
            this._dataAccess.ModifyData("spLIQ2EliminarMovimientoAConciliarCobro", CommandType.StoredProcedure, sqlParameterArray);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void EliminaCobroPedido(short AñoCobro, int Cobro)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoCobro", (object) AñoCobro),
        new SqlParameter("@Cobro", (object) Cobro)
      };
      try
      {
        this._dataAccess.ModifyData("spLIQ2EliminarCobroPedido", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void EliminaCobro(short AñoCobro, int Cobro)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoCobro", (object) AñoCobro),
        new SqlParameter("@Cobro", (object) Cobro)
      };
      try
      {
        this._dataAccess.ModifyData("spLIQ2EliminarCobro", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void EliminaEficienciaNegativa(short AñoAtt, int Folio)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        this._dataAccess.ModifyData("spLIQ2EliminarEficienciaNegativa", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ReactivaAutoTanqueTurno(short AñoAtt, int Folio)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        this._dataAccess.ModifyData("spCCLIQ2ReinicioAutotanqueturno", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable ConsultaCobrosCancelacion(short AñoAtt, int Folio)
    {
      DataTable dataTable = new DataTable("Cobro");
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        if (this._dataAccess == null)
          this.DataCompInitialize();
        this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaCobros", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public DataTable ConsultaTotalCobros(short AñoAtt, int Folio)
    {
      DataTable dataTable = new DataTable("Cobro");
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        if (this._dataAccess == null)
          this.DataCompInitialize();
        this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaTotalPagosTipo", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public void CancelacionPedidos(short AñoAtt, int Folio)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) AñoAtt),
        new SqlParameter("@Folio", (object) Folio)
      };
      try
      {
        this._dataAccess.ModifyData("sp_CancelacionLiquidacion", CommandType.StoredProcedure, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
