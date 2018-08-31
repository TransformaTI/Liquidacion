// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Cliente
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using RTGMGateway;
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
        private string _usuario;
        private string _urlGateway;
        private byte _modulo;
        private string _cadenaConexion;
        
         
        
        private DataTable _dtSaldosCliente;

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

        public DataTable SaldosCliente
        {
            get
            {
                return this._dtSaldosCliente;
            }
            set
            {
                this._dtSaldosCliente = value;
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
            Parametros _parametros = (Parametros)System.Web.HttpContext.Current.Session["parametros"] ;
            this._usuario = (string)System.Web.HttpContext.Current.Session["Usuario"]; 
            this._modulo = _parametros!=null?(byte) _parametros.Modulo: (byte)0;
            this._urlGateway = "";

            try
            {
                _urlGateway = (String)_parametros.ValorParametro("URLGateway");
            }
            catch (Exception ex)
            {

            }

            this._cliente = Cliente;
            this._claveCreditoAutorizado = ClaveCreditoAutorizado;
            DatosCliente datosCliente = new DatosCliente(Cliente);
            this._cadenaConexion = datosCliente.obtenerCadenaConexion();

        }
        
        public void ConsultaDatosCliente()
        {
            

            if (_urlGateway=="") {
                DatosCliente datosCliente = new DatosCliente(this._cliente, _fSuministro);
                try
                {
                    this.dtDatosCliente = datosCliente.ConsultaCliente();
                    this.asignacionDatosCliente(this.dtDatosCliente);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                this.asignacionDatosClienteGateway();
            }
            
        }

        public void ConsultaNombreCliente()
        {
            if (_urlGateway == "")
            {
                DatosCliente datosCliente = new DatosCliente(this._cliente);
                try
                {
                    _nombre = datosCliente.consultaNombreCliente(this._cliente);
               
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                this.asignacionNombreClienteGateway();
            }
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
        public RTGMCore.DireccionEntrega obtenDireccionEntrega(int Cliente)
        {
            RTGMCore.DireccionEntrega objDireccionEntega = new RTGMCore.DireccionEntrega();
            try
            {
                RTGMGateway.RTGMGateway objGateway = new RTGMGateway.RTGMGateway(_modulo, _cadenaConexion);
                objGateway.URLServicio = _urlGateway;
                SolicitudGateway objRequest = new SolicitudGateway
                {
                    IDCliente = Cliente
                };
                objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);
            }
            catch (Exception ex)
            {
                //throw ex;
                objDireccionEntega.Nombre = "Error "+ex.Message;
            }
            return objDireccionEntega;

        }

        public RTGMCore.CondicionesCredito obtenCondicionesCredito(int Cliente)
        {
            RTGMCore.CondicionesCredito objCondicionCredito;
            try
            {

                //string hola;
                //hola = Convert.ToString(Session["Usuario"]);

                RTGMGateway.RTGMGateway objGateway = new RTGMGateway.RTGMGateway(_modulo, _cadenaConexion);
                objGateway.URLServicio = _urlGateway;
                SolicitudGateway objRequest = new SolicitudGateway
                {
                    IDCliente = Cliente                   
                };
                objCondicionCredito = objGateway.buscarCondicionesCredito(objRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCondicionCredito;

        }

        private void asignacionNombreClienteGateway()
        {
            try
            {
                RTGMCore.DireccionEntrega objDireccionEntega = obtenDireccionEntrega(this._cliente);
               
                this._encontrado = true;
                this._nombre = objDireccionEntega.Nombre;   
            }
            catch (Exception ex)
            {
                this._nombre = "Error " + ex.Message;
                this._encontrado = true;

                //this._encontrado = false;
            }

        }



        private void asignacionDatosClienteGateway()
        {           
            try
            {

                RTGMCore.DireccionEntrega objDireccionEntega = obtenDireccionEntrega(this._cliente);
                RTGMCore.CondicionesCredito objCondicionCredito = obtenCondicionesCredito(this._cliente);

      
                this._encontrado = true;

                this._nombre = objDireccionEntega.Nombre!=null? objDireccionEntega.Nombre:string.Empty;
                this._direccion = objDireccionEntega.DireccionCompleta!=null?objDireccionEntega.DireccionCompleta:string.Empty;
                this._celula = objDireccionEntega.ZonaSuministro!=null? Convert.ToInt16(objDireccionEntega.ZonaSuministro.IDZona): Convert.ToInt16(0);
                this._ruta = objDireccionEntega.Ruta!=null? short.Parse(objDireccionEntega.Ruta.IDRuta.ToString()): short.Parse("0");
                this._tipoCartera = objDireccionEntega.CondicionesCredito!=null? Convert.ToByte(objDireccionEntega.CondicionesCredito.IDCartera): byte.Parse("0");                                   

                this._descripcionTipoCartera = objCondicionCredito!=null? objCondicionCredito.CarteraDescripcion:string.Empty;
                this._limiteCredito = objCondicionCredito!=null ? objCondicionCredito.LimiteCredito.Value : 0;
                this._saldo = objCondicionCredito!=null? objCondicionCredito.Saldo.Value:0;
                this._limiteDisponible = this._limiteCredito - this._saldo - this._saldoClienteMovimiento;
                this._tipoCreditoCliente = objDireccionEntega.CondicionesCredito!=null?Convert.ToByte(objDireccionEntega.CondicionesCredito.IDClasificacionCredito):byte.Parse("0");

                this._tipoCarteraCliente = objCondicionCredito != null ? objDireccionEntega.CondicionesCredito.CarteraDescripcion:string.Empty;
                this._creditoAutorizado = (int)this._tipoCartera == (int)this._claveCreditoAutorizado;
                this._limiteCreditoExcedido = !(this._limiteDisponible > new Decimal(0));
            


                try
                {
                    this._descuento = objDireccionEntega.Descuentos[0].ImporteDescuento;
                }
                catch
                {
                    this._descuento = 0;
                }

                try
                {
                    this._descripcionDescuento = objDireccionEntega.Descuentos[0].TipoDescuento;
                }
                catch
                {
                    this._descripcionDescuento = "";
                }

                try
                {
                    this._zonaEconomica = Convert.ToByte(objDireccionEntega.ZonaEconomica.IDZonaEconomomica);
                }
                catch 
                {

                    this._zonaEconomica = 0;
                }

                

                try
                {
                    this._precioCliente = objDireccionEntega.PrecioPorDefecto.ValorPrecio.Value;
                }
                catch
                {
                    this._precioCliente = 0;
                }

                    


            }
            catch (Exception ex)
            {
                this._nombre ="Error "+ex.Message;
                this._encontrado= true;

                //this._encontrado = false;
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

        public void  ConsultaSaldosAFavor(int cliente,string statusMovimiento, int folioMovimiento, int anioMovimiento)
        {
            DataTable saldoCliente = new DataTable();
            DatosCliente datosCliente = new DatosCliente(0,DateTime.Now);
            try
            {
                saldoCliente = datosCliente.ConsultaSaldosAFavor(cliente, statusMovimiento, folioMovimiento, anioMovimiento);
                if (saldoCliente.Rows.Count >0)
                {
                    this._nombre = Convert.ToString(saldoCliente.Rows[0]["Nombre"]);
                    //this._saldo = this._saldo = Convert.ToDecimal(saldoCliente.Rows[0]["Saldo"]);
                    SaldosCliente = saldoCliente;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
