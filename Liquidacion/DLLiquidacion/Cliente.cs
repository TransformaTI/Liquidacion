// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Cliente
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
    [Serializable]
    public class Cliente
    {
        private int _cliente;
        private DateTime _fSuministro;
        private string _nombre;
        private string _direccion;
        private short _celula;
        private short _ruta;
        private byte _tipoCreditoCliente;
        private byte _tipoCartera;
        private string _descripcionTipoCartera;
        private byte _claveCreditoAutorizado;
        private Decimal _limiteCredito;
        private Decimal _saldo;
        private Decimal _limiteDisponible;
        private Decimal _saldoClienteMovimiento;
        private string _tipoCarteraCliente;
        private bool _encontrado;
        private bool _creditoAutorizado;
        private bool _limiteCreditoExcedido;
        private Decimal _descuento;
        private string _descripcionDescuento;
        private byte _zonaEconomica;
        private DataTable dtDatosCliente;

        private decimal _precioCliente;
        
        public bool Encontrado
        {
            get
            {
                return this._encontrado;
            }
        }
        
        public int NumeroCliente
        {
            get
            {
                return this._cliente;
            }
        }
        
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
        }
        
        public string Direccion
        {
            get
            {
                return this._direccion;
            }
        }
        
        public short Celula
        {
            get
            {
                return this._celula;
            }
        }
        
        public short Ruta
        {
            get
            {
                return this._ruta;
            }
        }
        
        public byte TipoCartera
        {
            get
            {
                return this._tipoCartera;
            }
        }
        
        public string DescripcionTipoCartera
        {
            get
            {
                return this._descripcionTipoCartera;
            }
        }
        
        public Decimal LimiteCredito
        {
            get
            {
                return this._limiteCredito;
            }
        }
        
        public Decimal Saldo
        {
            get
            {
                return this._saldo;
            }
        }
        
        public Decimal SaldoClienteMovimiento
        {
            get
            {
                return this._saldoClienteMovimiento;
            }
            set
            {
                this._saldoClienteMovimiento = value;
            }
        }

        public Decimal Disponible
        {
            get
            {
                return this._limiteDisponible;
            }
        }
        
        public byte TipoCreditoCliente
        {
            get
            {
                return this._tipoCreditoCliente;
            }
        }
        
        public string ClasificacionCartera
        {
            get
            {
                return this._tipoCarteraCliente;
            }
        }
        
        public bool CreditoAutorizado
        {
            get
            {
                return this._creditoAutorizado;
            }
        }
        
        public bool LimiteCreditoExcedido
        {
            get
            {
                return this._limiteCreditoExcedido;
            }
        }
        
        public Decimal Descuento
        {
            get
            {
                return this._descuento;
            }
        }
        
        public string DescripcionDescuento
        {
            get
            {
                return this._descripcionDescuento;
            }
        }
        
        public byte ZonaEconomica
        {
            get
            {
                return this._zonaEconomica;
            }
            set
            {
                this._zonaEconomica = value;
            }
        }

        public DateTime FSuministro
        {
            get
            {
                return this._fSuministro;
            }
            set
            {
                this._fSuministro = value;
            }
        }
        
        //20-07-2015
        public decimal PrecioCliente
        {
            get
            {
                return _precioCliente;
            }
        }
                
        public Cliente(int Cliente, byte ClaveCreditoAutorizado)
        {
            this._cliente = Cliente;
            this._claveCreditoAutorizado = ClaveCreditoAutorizado;
        }
        
        public void ConsultaDatosCliente()
        {
            DatosCliente datosCliente = new DatosCliente(this._cliente, this._fSuministro);
            try
            {
                this.dtDatosCliente = datosCliente.ConsultaCliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.asignacionDatosCliente(this.dtDatosCliente);
        }
        
        private void asignacionDatosCliente(DataTable DatosCliente)
        {
            if (DatosCliente != null && DatosCliente.Rows.Count > 0)
            {
                foreach (DataRow dataRow in DatosCliente.Rows)
                {
                    this._encontrado = true;
                    this._nombre = Convert.ToString(dataRow["Nombre"]);
                    this._direccion = Convert.ToString(dataRow["DireccionCompleta"]);
                    this._celula = Convert.ToInt16(dataRow["Celula"]);
                    this._ruta = Convert.ToInt16(dataRow["Ruta"]);
                    this._tipoCartera = Convert.ToByte(dataRow["Cartera"]);
                    this._descripcionTipoCartera = Convert.ToString(dataRow["DescripcionCartera"]);
                    this._limiteCredito = Convert.ToDecimal(dataRow["MaxImporteCredito"]);
                    this._saldo = Convert.ToDecimal(dataRow["Saldo"]);
                    this._limiteDisponible = this._limiteCredito - this._saldo - this._saldoClienteMovimiento;
                    this._tipoCreditoCliente = Convert.ToByte(dataRow["TipoCreditoCliente"]);
                    this._tipoCarteraCliente = Convert.ToString(dataRow["ClasificacionCartera"]);
                    this._creditoAutorizado = (int)this._tipoCartera == (int)this._claveCreditoAutorizado;
                    this._limiteCreditoExcedido = !(this._limiteDisponible > new Decimal(0));
                    this._descuento = Convert.ToDecimal(dataRow["Descuento"]);
                    this._descripcionDescuento = Convert.ToString(dataRow["TipoDescuento"]);

                    this._zonaEconomica = dataRow["ZonaEconomica"] == DBNull.Value ? Convert.ToByte(0) : Convert.ToByte(dataRow["ZonaEconomica"]);//Se asignará automáticamente la zona económica 0 para clientes sin zona económica.

                    this._precioCliente = dataRow["Precio"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(dataRow["Precio"]);//Para consultar el precio del cliente de acuerdo a la zona económica.
                }
            }
            else
            {
                this._encontrado = false;
            }
        }
        
        public bool ClienteLiquidado(short AñoAtt, int Folio, int Cliente)
        {
            DatosCliente datosCliente = new DatosCliente(this._cliente, this._fSuministro);
            try
            {
                return datosCliente.ClienteLiquidado(AñoAtt, Folio, Cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
