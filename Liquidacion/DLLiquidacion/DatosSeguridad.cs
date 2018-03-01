// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosSeguridad
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosSeguridad : Datos
  {
    public DatosSeguridad(string conexion)
    {
    }

    public DatosSeguridad()
    {
    }

    public DataTable ConsultaUsuario(string Usuario)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@Usuario", SqlDbType.VarChar)
      };
      sqlParameterArray[0].Value = (object) Usuario;
      try
      {
        this._dataAccess.LoadData(dataTable, "spConsultaVwUsuario", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }

    public DataTable Privilegios(short Modulo, string Usuario)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@Modulo", SqlDbType.SmallInt),
        null
      };
      sqlParameterArray[0].Value = (object) Modulo;
      sqlParameterArray[1] = new SqlParameter("@Usuario", SqlDbType.VarChar);
      sqlParameterArray[1].Value = (object) Usuario;
      try
      {
        this._dataAccess.LoadData(dataTable, "spSEGPrivilegiosUsuario", CommandType.StoredProcedure, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }
  }
}
