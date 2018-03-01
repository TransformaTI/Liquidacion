// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosAdministracionUsuario
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
  internal class DatosAdministracionUsuario : Datos
  {
    private DataTable dtRelacion;
    private DataTable dtLiquidacionActiva;
    private DataTable dtUsuarios;
    private DataTable dtCelulas;

    public DataTable Usuarios
    {
      get
      {
        return this.dtUsuarios;
      }
    }

    public DataTable Celulas
    {
      get
      {
        return this.dtCelulas;
      }
    }

    public DataTable Relacion
    {
      get
      {
        return this.dtRelacion;
      }
    }

    public DataTable LiquidacionActiva
    {
      get
      {
        return this.dtLiquidacionActiva;
      }
    }

    public void CargaRelacion(string FAsignacion, string Usuario)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@FAsignacion", (object) Convert.ToDateTime(FAsignacion)),
        new SqlParameter("@Usuario", (object) Usuario)
      };
      this.dtRelacion = new DataTable();
      this._dataAccess.LoadData(this.dtRelacion, "spLIQ2ConsultaAsignacionUsuario", CommandType.StoredProcedure, sqlParameterArray, true);
    }

    public void CargaLiquidacionActiva(string Usuario, string Status)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@Usuario", (object) Usuario),
        new SqlParameter("@Status", (object) Status)
      };
      this.dtLiquidacionActiva = new DataTable();
      this._dataAccess.LoadData(this.dtLiquidacionActiva, "spLIQ2ConsultaActivas", CommandType.StoredProcedure, sqlParameterArray, true);
    }

    public void CargaUsuarios()
    {
      this.dtUsuarios = new DataTable();
      this._dataAccess.LoadData(this.dtUsuarios, "spLIQ2ConsultaUsuarios", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    public void CargaCelulas()
    {
      this.dtCelulas = new DataTable();
      this._dataAccess.LoadData(this.dtCelulas, "spLIQ2ConsultaCelulas", CommandType.StoredProcedure, (SqlParameter[]) null, true);
    }

    public void ActualizaLiquidacionActiva(int Año, int Folio)
    {
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        this._dataAccess.ModifyData("spLIQ2AltaFinLiquidacion", CommandType.StoredProcedure, new SqlParameter[2]
        {
          new SqlParameter("@AñoAtt", (object) Año),
          new SqlParameter("@Folio", (object) Folio)
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

    public void GuardaUsuarioCelula(DataTable dtUsuarioCelula)
    {
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        for (int index = 0; index <= dtUsuarioCelula.Rows.Count - 1; ++index)
          this._dataAccess.ModifyData("spLIQ2RegistraAsignacionUsuario", CommandType.StoredProcedure, new SqlParameter[8]
          {
            new SqlParameter("@FAsignacion", (object) Convert.ToDateTime(dtUsuarioCelula.Rows[index]["FAsignacion"].ToString())),
            new SqlParameter("@Usuario", (object) dtUsuarioCelula.Rows[index]["Usuario"].ToString()),
            new SqlParameter("@Status", (object) dtUsuarioCelula.Rows[index]["Status"].ToString()),
            new SqlParameter("@FAlta", (object) Convert.ToDateTime(dtUsuarioCelula.Rows[index]["FAlta"].ToString())),
            new SqlParameter("@FModificacion", (object) Convert.ToDateTime(dtUsuarioCelula.Rows[index]["FModificacion"].ToString())),
            new SqlParameter("@UsuarioAsignacion", (object) dtUsuarioCelula.Rows[index]["UsuarioAsignacion"].ToString()),
            new SqlParameter("@TipoAsignacion", (object) dtUsuarioCelula.Rows[index]["TipoAsignacion"].ToString()),
            new SqlParameter("@GrupoAsignado", (object) dtUsuarioCelula.Rows[index]["GrupoAsignado"].ToString())
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

    public void EliminaUsuarioCelula(int asignacionLiquidacion, string Status, string FecModificacion)
    {
      try
      {
        this._dataAccess.OpenConnection();
        this._dataAccess.BeginTransaction();
        this._dataAccess.ModifyData("spLIQ2ModificaAsignacionUsuario", CommandType.StoredProcedure, new SqlParameter[3]
        {
          new SqlParameter("@AsignacionLiquidacion", (object) asignacionLiquidacion),
          new SqlParameter("@Status", (object) Status),
          new SqlParameter("@FModificacion", (object) FecModificacion)
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
