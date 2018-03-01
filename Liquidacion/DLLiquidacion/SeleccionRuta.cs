// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.SeleccionRuta
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class SeleccionRuta
  {
    private DataTable dtCelulas = new DataTable();
    private DataTable dtRutas = new DataTable();
    private DatosSeleccionRuta _datos = new DatosSeleccionRuta();

    public DataTable ListaCelulas(DateTime Fasignacion, string Usuario)
    {
      this._datos.CargaCelulas(Fasignacion, Usuario);
      this.dtCelulas = this._datos.Celulas;
      return this.dtCelulas;
    }

    public DataTable ListaRutas(DateTime FInicioRuta, int Celula)
    {
      this._datos.CargaRutas(FInicioRuta, Celula);
      this.dtRutas = this._datos.Rutas;
      return this.dtRutas;
    }
  }
}
