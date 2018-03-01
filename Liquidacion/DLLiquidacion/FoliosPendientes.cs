// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.FoliosPendientes
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  public class FoliosPendientes
  {
    private DataTable dtFoliosPendientes;
    private DatosFoliosPendientes _datos;
    private DataTable dtRutasAutorizadasUsuario;

    public FoliosPendientes()
    {
      this._datos = new DatosFoliosPendientes();
    }

    public DataTable ConsultaFoliosPendientes(DateTime FInicioRuta)
    {
      this.dtFoliosPendientes = this._datos.ConsultaFoliosPendientes(FInicioRuta);
      return this.dtFoliosPendientes;
    }

    public DataTable ConsultaRutasAutorizadasPorUsuario(string Usuario, DateTime FAsignacion)
    {
      this.dtRutasAutorizadasUsuario = this._datos.ConsultaRutasAutorizadasPorUsuario(FAsignacion, Usuario);
      this.dtRutasAutorizadasUsuario.PrimaryKey = new DataColumn[1]
      {
        this.dtRutasAutorizadasUsuario.Columns["GrupoAsignado"]
      };
      return this.dtRutasAutorizadasUsuario;
    }

    public bool RutaAsignada(object Ruta)
    {
      return this.dtRutasAutorizadasUsuario.Rows.Contains(Ruta);
    }

    public DataTable ConsultaCelulas()
    {
      DataView defaultView = this.dtFoliosPendientes.DefaultView;
      defaultView.Sort = "Celula ASC";
      return defaultView.ToTable(1 != 0, new string[2]
      {
        "Celula",
        "NombreCelula"
      });
    }

    public DataTable ConsultaRutas(short Celula)
    {
      DataView defaultView = this.dtFoliosPendientes.DefaultView;
      defaultView.RowFilter = "Celula = " + Celula.ToString();
      defaultView.Sort = "Ruta ASC";
      return defaultView.ToTable(1 != 0, new string[2]
      {
        "Ruta",
        "NombreRuta"
      });
    }

    public DataTable ConsultaAutoTanques(short Ruta)
    {
      DataView defaultView = this.dtFoliosPendientes.DefaultView;
      defaultView.RowFilter = "Ruta = " + Ruta.ToString();
      defaultView.Sort = "AutoTanque ASC";
      return defaultView.ToTable(1 != 0, "AutoTanque");
    }

    public DataTable ConsultaFolios(int AutoTanque)
    {
      DataView defaultView = this.dtFoliosPendientes.DefaultView;
      defaultView.RowFilter = "AutoTanque = " + AutoTanque.ToString();
      defaultView.Sort = "AñoAtt, Folio ASC";
      return defaultView.ToTable(1 != 0, new string[2]
      {
        "AñoAtt",
        "Folio"
      });
    }
  }
}
