// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosParametros
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  internal class DatosParametros : Datos
  {
    private short _modulo;
    private short _corporativo;
    private short _sucursal;

    public DatosParametros(short Modulo, short Corporativo, short Sucursal, DataTable ListaParametros)
    {
      this._modulo = Modulo;
      this._corporativo = Corporativo;
      this._sucursal = Sucursal;
      try
      {
        this.cargaParametros(ListaParametros);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void cargaParametros(DataTable ListaParametros)
    {
      try
      {
        this._dataAccess.LoadData(ListaParametros, "spSEGParametrosModulo", CommandType.StoredProcedure, new SqlParameter[3]
        {
          new SqlParameter("@Modulo", (object) this._modulo),
          new SqlParameter("@Corporativo", (object) this._corporativo),
          new SqlParameter("@Sucursal", (object) this._sucursal)
        }, 1 != 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
