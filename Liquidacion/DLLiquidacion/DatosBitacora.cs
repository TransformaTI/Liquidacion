// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosBitacora
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  public class DatosBitacora : Datos
  {
    private DataTable dtBitacora;

    public DataTable Bitacora
    {
      get
      {
        return this.dtBitacora;
      }
    }

    public void CargaBitacora()
    {
      this.dtBitacora = new DataTable();
      this._dataAccess.LoadData(this.dtBitacora, "spLIQ2ConsultaBitacoraUsuario", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    public void GuardaRegistroBitacora(int año, int folio, string descripcion, string usuario, DateTime fecha)
    {
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        this._dataAccess.ModifyData("spLIQ2RegistraBitacora", CommandType.StoredProcedure, new SqlParameter[8]
        {
          new SqlParameter("@Año", (object) año),
          new SqlParameter("@Folio", (object) folio),
          new SqlParameter("@Descripcion", (object) descripcion),
          new SqlParameter("@usuario", (object) usuario),
          new SqlParameter("@fecha", (object) Convert.ToDateTime(fecha)),
          null,
          null,
          null
        });
        //this._dataAccess.get_Transaction().Commit();
        this._dataAccess.Transaction.Commit();
      }
      catch
      {
        //this._dataAccess.get_Transaction().Rollback();
        this._dataAccess.Transaction.Rollback();
        throw;
      }
    }
  }
}
