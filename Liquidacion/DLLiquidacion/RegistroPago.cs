﻿// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.RegistroPago
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class RegistroPago
  {
    public double dbIVA = 0.15;
    private DataTable dtPedidos = new DataTable();
    private DataTable dtBancos = new DataTable();
    private DataTable dtCliente = new DataTable();
    private DataTable dtUsuarios = new DataTable();
    private DataTable dtCelulas = new DataTable();
    private DataTable dtPromociones = new DataTable();
    private DatosRegistroPago _datos = new DatosRegistroPago();

    public DataTable ListaPedidos()
    {
      this._datos.CargaPedidos();
      this.dtPedidos = this._datos.Pedidos;
      return this.dtPedidos;
    }

    public DataTable ListaBancos()
    {
      this._datos.CargaBancos();
      this.dtBancos = this._datos.Bancos;
      return this.dtBancos;
    }

    public DataTable DatosCliente(int Cliente)
    {
      this._datos.CargaCliente(Cliente);
      this.dtCliente = this._datos.Cliente;
      return this.dtCliente;
    }

    public DataTable Promociones()
    {
      this._datos.CargaPromociones();
      this.dtPromociones = this._datos.Promociones;
      return this.dtPromociones;
    }

    public void GuardaPagos(string Usuario, DataTable dtPedidos, DataTable dtPago, DataTable dtDetallePago, DataTable dtResumenLiquidacion)
    {
      this._datos.GuardaPagos(Usuario, dtPedidos, dtPago, dtDetallePago, dtResumenLiquidacion);
    }

    public enum TipoPago
    {
      tipoCheque = 3,
      tipoEfectivo = 5,
      tipoTarjeta = 6,
      tipoDescuento = 12,
      tipoVale = 16,
    }
  }
}
