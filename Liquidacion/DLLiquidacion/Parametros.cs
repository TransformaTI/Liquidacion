// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Parametros
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class Parametros
  {
    private short _corporativo;
    private short _sucursal;
    private short _modulo;
    private DataTable _parametros;
    [NonSerialized]
    private DatosParametros _datos;

        public short Modulo
        {
            get
            {
                return _modulo;
            }

            set
            {
                _modulo = value;
            }
        }

        public Parametros(short Corporativo, short Sucursal, short Modulo)
    {
      this._corporativo = Corporativo;
      this._sucursal = Sucursal;
      this._modulo = Modulo;
      this._parametros = new DataTable("Parametros");
      this._datos = new DatosParametros(this._modulo, this._corporativo, this._sucursal, this._parametros);
      this._parametros.PrimaryKey = new DataColumn[2]
      {
        this._parametros.Columns["Parametro"],
        this._parametros.Columns[""]
      };
    }

    public object ValorParametro(string Parametro)
    {
      return this._parametros.Rows.Find((object) Parametro)["Valor"];
    }
  }
}
