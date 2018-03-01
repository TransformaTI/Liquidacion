// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosBoletin
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  internal class DatosBoletin : Datos
  {
    private DataTable dtListaBoletines;

    public DataTable ListaBoletines
    {
      get
      {
        return this.dtListaBoletines;
      }
    }

    public DatosBoletin()
    {
      this.dtListaBoletines = new DataTable();
      this._dataAccess.LoadData(this.dtListaBoletines, "exec spCCConsultaBoletinDia @Celula = 1, @FCompromiso = 'Sep 2 2008 12:00:00:000AM', @StatusBoletin = 'BOLETIN'", CommandType.Text, (SqlParameter[]) null, true);
    }
  }
}
