// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosFoliosPendientes
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosFoliosPendientes : Datos
  {
    public DataTable ConsultaFoliosPendientes(DateTime FInicioRuta)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@FInicioRuta", (object) FInicioRuta)
      };
      try
      {
        this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaRutas", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public DataTable ConsultaRutasAutorizadasPorUsuario(DateTime FAsignacion, string Usuario)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@FAsignacion", (object) FAsignacion),
        new SqlParameter("@Usuario", (object) Usuario)
      };
      try
      {
        this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaAsignacionUsuario", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }
  }
}
