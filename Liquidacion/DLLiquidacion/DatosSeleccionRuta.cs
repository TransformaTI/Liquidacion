// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosSeleccionRuta
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosSeleccionRuta : Datos
  {
    private DataTable dtCelulas;
    private DataTable dtRutas;

    public DataTable Celulas
    {
      get
      {
        return this.dtCelulas;
      }
    }

    public DataTable Rutas
    {
      get
      {
        return this.dtRutas;
      }
    }

    public void CargaCelulas(DateTime FAsignacion, string Usuario)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@FAsignacion", (object) FAsignacion),
        new SqlParameter("@Usuario", (object) Usuario)
      };
      this.dtCelulas = new DataTable();
      this._dataAccess.LoadData(this.dtCelulas, "spLIQ2ConsultaAsignacionUsuario", CommandType.StoredProcedure, sqlParameterArray, true);
    }

    public void CargaRutas(DateTime FInicioRuta, int Celula)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@FInicioRuta", (object) FInicioRuta),
        new SqlParameter("@Celula", (object) Celula)
      };
      this.dtRutas = new DataTable();
      this._dataAccess.LoadData(this.dtRutas, "spLIQ2ConsultaRutas", CommandType.StoredProcedure, sqlParameterArray, true);
    }
  }
}
