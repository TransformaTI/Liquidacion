// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.AdministracionUsuarios
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class AdministracionUsuarios
  {
    private DatosAdministracionUsuario _datos = new DatosAdministracionUsuario();
    private DataTable dtRelacion = new DataTable();
    private DataTable dtLiquidacionActiva = new DataTable();
    private DataTable dtUsuarios;
    private DataTable dtCelulas;

    public DataTable ListaUsuarios()
    {
      this._datos.CargaUsuarios();
      this.dtUsuarios = this._datos.Usuarios;
      return this.dtUsuarios;
    }

    public DataTable ListaCelulas()
    {
      this._datos.CargaCelulas();
      this.dtCelulas = this._datos.Celulas;
      return this.dtCelulas;
    }

    public DataTable ListaRelacion(string FAsignacion, string Usuario)
    {
      this._datos.CargaRelacion(FAsignacion, Usuario);
      this.dtRelacion = this._datos.Relacion;
      return this.dtRelacion;
    }

    public DataTable ListaLiquidacionActiva(string Usuario, string Status)
    {
      this._datos.CargaLiquidacionActiva(Usuario, Status);
      this.dtLiquidacionActiva = this._datos.LiquidacionActiva;
      return this.dtLiquidacionActiva;
    }

    public void GuardaUsuarioCelula(DataTable dtUsuarioCelula)
    {
      this._datos.GuardaUsuarioCelula(dtUsuarioCelula);
    }

    public void EliminaUsuarioCelula(int asignacionLiquidacion, string Status, string FecModificacion)
    {
      this._datos.EliminaUsuarioCelula(asignacionLiquidacion, Status, FecModificacion);
    }

    public void ActualizaLiquidacionActiva(int Año, int Folio)
    {
      this._datos.ActualizaLiquidacionActiva(Año, Folio);
    }
  }
}
