// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.ControlDeCredito
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  public sealed class ControlDeCredito
  {
    private static readonly ControlDeCredito instance = new ControlDeCredito();
    private Parametros _parametros;

    public static ControlDeCredito Instance
    {
      get
      {
        return ControlDeCredito.instance;
      }
    }

    public Parametros Parametros
    {
      set
      {
        this._parametros = value;
      }
    }

    private ControlDeCredito()
    {
    }

    public byte AsignarFormaPago(string FormaPago, string DescripcionFormaPagoCredito, byte TipoCreditoCliente)
    {
      if (FormaPago.Trim().ToUpper() == DescripcionFormaPagoCredito.Trim().ToUpper())
        return TipoCreditoCliente;
      return (byte) 5;
    }

    public bool AutorizacionCredito(byte TipoCobro, Decimal TotalPedido, Decimal SaldoClienteFolio, Cliente Cliente, Decimal DisponibleOperador)
    {
      return (int) TipoCobro == 5 || !Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("AutorizacionCredito"))) || (int) Cliente.TipoCartera == 7 && (!Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LimiteCredito"))) || !(Cliente.Saldo + SaldoClienteFolio + TotalPedido > Cliente.LimiteCredito)) && (!Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LimiteCreditoOperador"))) || (int) TipoCobro != 9 || !(DisponibleOperador - TotalPedido < new Decimal(0)));
    }

    public byte TipoCobroAsignado(byte TipoCobro, Decimal TotalPedido, Decimal SaldoClienteFolio, Cliente Cliente, Decimal DisponibleOperador)
    {
      byte num = (byte) 5;
      if ((int) TipoCobro == 5 || Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("AutorizacionCredito"))) && (this.AutorizacionCredito(TipoCobro, TotalPedido, SaldoClienteFolio, Cliente, DisponibleOperador) || Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("LimiteCredito"))) && this.AutorizacionCredito(TipoCobro, TotalPedido, SaldoClienteFolio, Cliente, DisponibleOperador)))
        return num;
      return Cliente.TipoCreditoCliente;
    }

    public Decimal ResumenSaldoCliente(int Cliente, DataTable Pedidos)
    {
      if (Pedidos.Columns.Contains("Importe") && Pedidos.Columns.Contains("Cliente") && Pedidos.Rows.Count > 0)
      {
        object obj = Pedidos.Compute("Sum (Importe)", "Cliente = " + Cliente.ToString());
        if (!(obj is DBNull))
          return Convert.ToDecimal(obj);
      }
      return new Decimal(0);
    }

    public Decimal ResumenSaldoTipoCobro(byte TipoCobro, DataTable Pedidos)
    {
      if (!Pedidos.Columns.Contains("Importe") || !Pedidos.Columns.Contains("Cliente") || Pedidos.Rows.Count <= 0)
        return new Decimal(0);
      object obj = Pedidos.Compute("Sum (Importe)", "FormaPago = " + (object) TipoCobro);
      if (obj != DBNull.Value)
        return Convert.ToDecimal(obj);
      return new Decimal(0);
    }

    public Decimal ResumenSaldoTipoCobro(byte TipoCobro, DataTable Pedidos, string Status)
    {
      Pedidos.DefaultView.RowFilter = "Status = 'CONCILIADO'";
      if (!Pedidos.Columns.Contains("Importe") || !Pedidos.Columns.Contains("Cliente") || Pedidos.Rows.Count <= 0)
        return new Decimal(0);
      object obj = Pedidos.DefaultView.ToTable().Compute("Sum (Importe)", "FormaPago = " + (object) TipoCobro);
      Pedidos.DefaultView.RowFilter = (string) null;
      if (obj != DBNull.Value)
        return Convert.ToDecimal(obj);
      return new Decimal(0);
    }
  }
}
