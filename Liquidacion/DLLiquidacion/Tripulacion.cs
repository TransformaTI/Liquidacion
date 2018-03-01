// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Tripulacion
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class Tripulacion
  {
    private short _añoAtt;
    private int _folio;
    private int _codigoOperadorTitular;
    private string _nombreOperadorTitular;
    private Decimal _limiteCreditoOperador;
    private Decimal _saldoOperador;
    private DataTable _dtAsignacionCompleta;

    public int CodigoOperadorTitular
    {
      get
      {
        return this._codigoOperadorTitular;
      }
    }

    public string NombreOperadorTitular
    {
      get
      {
        return this._nombreOperadorTitular;
      }
    }

    public Decimal LimiteCreditoOperador
    {
      get
      {
        return this._limiteCreditoOperador;
      }
    }

    public Decimal SaldoOperador
    {
      get
      {
        return this._saldoOperador;
      }
    }

    public Tripulacion(short AñoAtt, int Folio)
    {
      this._añoAtt = AñoAtt;
      this._folio = Folio;
    }

    public Decimal LimiteCreditoDisponible(Decimal SaldoOperadorMovimiento)
    {
      Decimal num = this._limiteCreditoOperador - this._saldoOperador - SaldoOperadorMovimiento;
      if (num < new Decimal(0))
        num = new Decimal(0);
      return num;
    }

    public void ConsultaTripulacion()
    {
      this._dtAsignacionCompleta = new DatosTripulacion(this._añoAtt, this._folio).ConsultaDatosTripulacion();
      this.asignacionDatosTripulacion(this._dtAsignacionCompleta);
    }

    private void asignacionDatosTripulacion(DataTable DatosTripulacion)
    {
      foreach (DataRow dataRow in (InternalDataCollectionBase) DatosTripulacion.Rows)
      {
        if ((int) Convert.ToInt16(dataRow["CategoriaOperador"]) == 1 && ((int) Convert.ToInt16(dataRow["TipoAsignacionOperador"]) == 1 || (int) Convert.ToInt16(dataRow["TipoAsignacionOperador"]) == 2))
        {
          this._codigoOperadorTitular = Convert.ToInt32(dataRow["Operador"]);
          this._nombreOperadorTitular = Convert.ToString(dataRow["Nombre"]);
          this._limiteCreditoOperador = Convert.ToDecimal(dataRow["PrecioVigente"]) * Convert.ToDecimal(dataRow["MaxLitrosCredito"]);
          this._saldoOperador = Convert.ToDecimal(dataRow["SaldoOperadorImporte"]);
        }
      }
    }

    public string AsignacionCompleta()
    {
      return string.Empty;
    }
  }
}
