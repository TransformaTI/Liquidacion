// Decompiled with JetBrains decompiler
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
    private DataTable dtPagosConTarjeta = new DataTable(); // mcc 2018 05 10
    private DataTable dtAfiliaciones = new DataTable();
    private DataTable dtProveedores = new DataTable();
    private DataTable dtTipoVale = new DataTable();

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

     public DataTable PagosConTarjeta(int NumCliente,int Ruta, int Autotanque)
        {
            this._datos.CargaPagosConTarjeta(NumCliente,Ruta, Autotanque);
            this.dtPagosConTarjeta = this._datos.PagosConTarjeta;
            return this.dtPagosConTarjeta;

        }

    public DataTable Afiliaciones(int Ruta)
        {
            this._datos.CargaAfiliaciones(Ruta);
            this.dtAfiliaciones = this._datos.Afiliaciones;
            return this.dtAfiliaciones;
        }

    public DataTable Proveedores()
        {
            this._datos.CargaProveedores();
            this.dtProveedores = this._datos.Proveedores;
            return this.dtProveedores;
        }

        public DataTable TipoVale()
        {
            this._datos.CargaTipoVale();
            this.dtTipoVale = this._datos.TipoVale;
            return this.dtTipoVale;
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
