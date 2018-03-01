// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosCatalogos
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosCatalogos : Datos
  {
    private DataTable dtListaPrecios;
    private DataTable dtListaFormaDePago;
    private DataTable dtListaTipoPedido;

    public DataTable ListaPrecios
    {
      get
      {
        return this.dtListaPrecios;
      }
    }

    public DataTable ListaFormasPago
    {
      get
      {
        return this.dtListaFormaDePago;
      }
    }

    public DataTable ListaTipoPedido
    {
      get
      {
        return this.dtListaTipoPedido;
      }
    }

    public DatosCatalogos()
    {
      try
      {
        this.dtListaPrecios = new DataTable();
        this.cargaPrecios();
        this.dtListaFormaDePago = new DataTable();
        this.cargaFormasPago();
        this.dtListaFormaDePago.PrimaryKey = new DataColumn[1]
        {
          this.dtListaFormaDePago.Columns["TipoCobro"]
        };
        this.dtListaTipoPedido = new DataTable();
        this.cargaListaTipoPedido();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void cargaPrecios()
    {
      this._dataAccess.LoadData(this.dtListaPrecios, "spLIQ2ConsultaPrecioGLP", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    private void cargaFormasPago()
    {
      this._dataAccess.LoadData(this.dtListaFormaDePago, "spLIQ2ConsultaFormasPago", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    private void cargaListaTipoPedido()
    {
      this._dataAccess.LoadData(this.dtListaTipoPedido, "spLIQ2ConsultaTipoPedido", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }
  }
}
