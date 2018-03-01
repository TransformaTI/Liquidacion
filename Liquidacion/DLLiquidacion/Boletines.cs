// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Boletines
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System.Data;

namespace SigametLiquidacion
{
  public class Boletines
  {
    private DatosBoletin _datos;

    public DataTable ListaBoletines
    {
      get
      {
        return this._datos.ListaBoletines;
      }
    }

    public Boletines()
    {
      this._datos = new DatosBoletin();
    }
  }
}
