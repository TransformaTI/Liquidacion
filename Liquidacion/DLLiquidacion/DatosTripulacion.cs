// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosTripulacion
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  internal class DatosTripulacion : Datos
  {
    public DatosTripulacion(short AñoAtt, int Folio)
    {
      this._AñoAtt = AñoAtt;
      this._Folio = Folio;
    }

    public DataTable ConsultaDatosTripulacion()
    {
      return this.ConsultaDatosPorFolio("spLIQ2ConsultaTripulacion", CommandType.StoredProcedure);
    }
  }
}
