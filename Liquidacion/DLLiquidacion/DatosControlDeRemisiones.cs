// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosControlDeRemisiones
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  internal class DatosControlDeRemisiones : Datos
  {
    public DataTable Remision(string SerieRemision, string Remision)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@Serie", (object) SerieRemision),
        new SqlParameter("@FolioNota", (object) Remision)
      };
      //this._dataAccess.set_QueryingTimeOut(60);
      this._dataAccess.QueryingTimeOut = 60;
      try
      {
        this._dataAccess.LoadData(dataTable, "spCCLIQValidaRemision", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public DataTable RemisionCapturada(string SerieRemision, string Remision)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@Serie", (object) SerieRemision),
        new SqlParameter("@FolioNota", (object) Remision)
      };
      //this._dataAccess.set_QueryingTimeOut(60);
      this._dataAccess.QueryingTimeOut = 60;
      try
      {
        this._dataAccess.LoadData(dataTable, "spCCLIQValidaRemisionCapturada", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public DataTable ValidarNota(string SerieRemision, string Remision)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@Serie", (object) SerieRemision),
        new SqlParameter("@FolioNota", (object) Remision)
      };
      //this._dataAccess.set_QueryingTimeOut(60);
      this._dataAccess.QueryingTimeOut = 60;
      try
      {
        this._dataAccess.LoadData(dataTable, "spCCLIQValidaNota", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }
  }
}
