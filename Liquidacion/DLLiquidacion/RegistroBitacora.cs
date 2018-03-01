// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.RegistroBitacora
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class RegistroBitacora
  {
    private DataTable dtBitacora = new DataTable();
    private DatosBitacora _datos = new DatosBitacora();
    private int _año;
    private int _folio;
    private string _descripcion;
    private string _usuario;
    private DateTime _fecha;

    public DataTable Bitacora()
    {
      this._datos.CargaBitacora();
      this.dtBitacora = this._datos.Bitacora;
      return this.dtBitacora;
    }

    public void GuardaBitacora(int año, int folio, string descripcion, string usuario, DateTime fecha)
    {
      this._datos.GuardaRegistroBitacora(año, folio, descripcion, usuario, fecha);
    }
  }
}
