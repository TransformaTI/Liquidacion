// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Seguridad
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class Seguridad
  {
    private DatosSeguridad _seguridad = new DatosSeguridad();
    private short _modulo;
    private string _usuario;
    private string _contrasenia;
    private bool _sesionIniciada;
    private DataTable dtUsuario;
    private DataTable dtOperaciones;
    private string _mensajeAcceso;

    public bool SesionIniciada
    {
      get
      {
        return this._sesionIniciada;
      }
    }

    public string MensajeAcceso
    {
      get
      {
        return this._mensajeAcceso;
      }
    }

     public  DataTable Usuario
        {
            get
      {
        return this.dtUsuario;
      }
        }


        public Seguridad(short Modulo, string Usuario, string Contrasenia)
    {
      this._modulo = Modulo;
      this._usuario = Usuario;
      this._contrasenia = Contrasenia;
      this.consultaUsuario();
      this.validaUsuario();
    }

    private void consultaUsuario()
    {
      try
      {
        this.dtUsuario = this._seguridad.ConsultaUsuario(this._usuario);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void consultaOperaciones()
    {
      try
      {
        this.dtOperaciones = this._seguridad.Privilegios(this._modulo, this._usuario);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void validaUsuario()
    {
      this._mensajeAcceso = "Usuario o contraseña incorrecta.";
      if (this.dtUsuario.Rows.Count <= 0 || !(this._contrasenia.Trim().ToUpper() == Convert.ToString(this.dtUsuario.Rows[0]["Clave"]).Trim().ToUpper()))
        return;
      this._sesionIniciada = this.TieneAcceso("ACCESO");
      if (this._sesionIniciada)
        return;
      this._mensajeAcceso = "No tiene acceso al módulo.";
    }

    public bool TieneAcceso(string Operacion)
    {
      if (this.dtOperaciones == null)
        this.consultaOperaciones();
      foreach (DataRow dataRow in (InternalDataCollectionBase) this.dtOperaciones.Rows)
      {
        if (Operacion.Trim().ToUpper() == Convert.ToString(dataRow["Nombre"]).Trim().ToUpper())
          return Convert.ToBoolean(dataRow["Habilitada"]);
      }
      this._mensajeAcceso = "La operación no existe en el módulo";
      return false;
    }
  }
}
