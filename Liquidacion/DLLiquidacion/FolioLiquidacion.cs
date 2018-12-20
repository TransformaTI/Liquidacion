// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.FolioLiquidacion
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using DocumentosBSR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace SigametLiquidacion
{
    [Serializable]
    public class FolioLiquidacion
    {
        private string _usuario;
        private short _añoAtt;
        private int _folio;
        private DateTime _fecha;
        private int _autoTanque;
        private short _celula;
        private short _ruta;
        private string _status;
        private double _totalizadorInicial;
        private double _totalizadorFinal;
        private double _diferenciaTotalizador;      
        private DataTable _dtListaPedido;
        private byte _claseRuta;
        public Tripulacion Tripulacion;
        private DatosFolio _datosFolio;
        private Catalogos _catalogos;
        private Parametros _parametros;
        private string _serieRemision;      
        private bool _preciosMultiples;
        private string _tipoLiquidacion;
        private string _statusLiquidacion;
        private bool _validarRemisionLiquidada;
        private bool _validarRemisionExistente;
        private bool _folioRemisionAutomaticoRuta;

        private List<Cliente> ListaClientes = new List<Cliente>();

        //26-05-2015 - parámetro usar serie
        private bool _remisionUsaSerie;
        
        private byte _longitudSerieNota;
        private byte _longitudFolioNota;
        private string _cadenaConexion;
        
        public DataTable ListaPedidos
        {
            get
            {
                return this._dtListaPedido;
            }
            set
            {
                this._dtListaPedido = value;
            }
        }
        
        public short AñoAtt
        {
            get
            {
                return this._añoAtt;
            }
            set
            {
                this._añoAtt = value;
            }
        }
      
        public int Folio
        {
            get
            {
                return this._folio;
            }
            set
            {
                this._folio = value;
            }
        }
            
        public DateTime Fecha
        {
            get
            {
                return this._fecha;
            }
        }
      
        public int AutoTanque
        {
            get
            {
                return this._autoTanque;
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
        
        public string Status
        {      
            get
            {
                return this._status;
            }
        }
        
        public double TotalizadorInicial
        {
            get
            {
                return this._totalizadorInicial;
            }
        }
        
        public double TotalizadorFinal
        {      
            get
            {
                return this._totalizadorFinal;
            }
        }
        
        public double DiferenciaTotalizador
        {
            get
            {
                return this._diferenciaTotalizador;
            }
        }
        
        public DataTable PedidosConciliados
        {
            get
            {
                return this.SuministrosPorTipo("CONCILIADO");
            }
        }
        
        public string SerieRemision
        {
            get
            {
                return this._serieRemision;
            }
        }
        
        public bool PreciosMultiples
        {
            get
            {
                return this._preciosMultiples;
            }
        }
        
        public byte ClaseRuta
        {
            get
            {
                return this._claseRuta;
            }
        }
        
        public string Usuario
        {
            set
            {
                this._usuario = value;
            }
        }
        
        public string TipoLiquidacion
        {
            get
            {
                return this._tipoLiquidacion;
            }
            set
            {
                this._tipoLiquidacion = value;
            }
        }
        
        public string StatusLiquidacion
        {
            get
            {
                return this._statusLiquidacion;
            }
        }
        
        public bool ValidarRemisionLiquidada
        {
            get
            {
                return this._validarRemisionLiquidada;
            }
            set
            {
                this._validarRemisionLiquidada = value;
            }
        }
        
        public bool ValidarRemisionExistente
        {
            get
            {
                return this._validarRemisionExistente;
            }
            set
            {
                this._validarRemisionExistente = value;
            }
        }
        
        public bool FolioRemisionAutomatico
        {
            get
            {
                return this._folioRemisionAutomaticoRuta;
            }
            set
            {
                this._folioRemisionAutomaticoRuta = value;
            }
        }
        
        public FolioLiquidacion(short AñoAtt, int Folio)
        {
            this._añoAtt = AñoAtt;
            this._folio = Folio;
            this.consultaDatosFolio();
            this.consultaDatosTripulacion();
            this._catalogos = new Catalogos();
            this._parametros = new Parametros((short) 1, (short) 1, (short) 22);
            this.initListaPedido();
        }
        
        public byte longitudSerieNota
        {
            get
            {
                return _longitudSerieNota;
            }
        }
        
        public byte longitudFolioNota
        {
            get
            {
                return _longitudFolioNota;
            }            
        }
        
        private void consultaDatosFolio()
        {
            this._datosFolio = new DatosFolio(this._añoAtt, this._folio);
            foreach (DataRow dataRow in this._datosFolio.ConsultaDatosFolio().Rows)
            {
                this._añoAtt = Convert.ToInt16(dataRow["AñoAtt"]);            
                this._folio = Convert.ToInt32(dataRow["Folio"]);
                this._fecha = Convert.ToDateTime(dataRow["FTerminoRuta"]);
                this._autoTanque = Convert.ToInt32(dataRow["AutoTanque"]);
                this._celula = Convert.ToInt16(dataRow["Celula"]);
                this._ruta = Convert.ToInt16(dataRow["Ruta"]);
                this._status = Convert.ToString(dataRow["StatusLogistica"]);
                if(dataRow["TotalizadorInicial"] == DBNull.Value)
                {
                    this._totalizadorInicial = 0;
                }
                else
                {
                    this._totalizadorInicial = Convert.ToDouble(dataRow["TotalizadorInicial"]);
                }

                if(dataRow["TotalizadorFinal"] == DBNull.Value)
                {
                    this._totalizadorFinal = 0;
                }
                else
                {
                    this._totalizadorFinal = Convert.ToDouble(dataRow["TotalizadorFinal"]);
                }
                
                this._diferenciaTotalizador = Convert.ToDouble(dataRow["LitrosLiquidados"]);
                this._serieRemision = Convert.ToString(dataRow["SerieRemision"]);
                this._preciosMultiples = Convert.ToBoolean(dataRow["MultiplesPrecios"]);
                this._claseRuta = Convert.ToByte(dataRow["ClaseRuta"]);
                this._statusLiquidacion = Convert.ToString(dataRow["StatusLiquidacion"]);
                this._validarRemisionExistente = Convert.ToBoolean(dataRow["ValidarRemisionExistente"]);
                this._validarRemisionLiquidada = Convert.ToBoolean(dataRow["ValidarRemisionCapturada"]);
                this._folioRemisionAutomaticoRuta = Convert.ToBoolean(dataRow["FolioRemisionAutomatico"]);
                //26-05-2015 - Usar serie para la remisión
                //TODO: Parametrizar
                this._remisionUsaSerie = Convert.ToBoolean(dataRow["UsarSerie"]);
                this._longitudSerieNota = Convert.ToByte(dataRow["LongitudSerie"]);
                this._longitudFolioNota = Convert.ToByte(dataRow["LongitudFolio"]);
            }
        }
        
        private void consultaDatosTripulacion()
        {
            this.Tripulacion = new Tripulacion(this._añoAtt, this._folio);
            this.Tripulacion.ConsultaTripulacion();
        }
        
        public Decimal ImporteCreditoOperador()
        {
            object obj = this._dtListaPedido.Compute("SUM(Importe)", "FormaPago = 9");
            if (obj.Equals((object)DBNull.Value))
            {
                return new Decimal(0);
            }
            return Convert.ToDecimal(obj);
        }
        
        private void initListaPedido()
        {
            this._dtListaPedido = new DataTable("ListaPedidos");
            this._dtListaPedido.Columns.Add("ID", Type.GetType("System.Int32"));
            this._dtListaPedido.Columns.Add("Status", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("PedidoReferencia", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("Celula", Type.GetType("System.Int16"));
            this._dtListaPedido.Columns.Add("AñoPed", Type.GetType("System.Int16"));
            this._dtListaPedido.Columns.Add("Pedido", Type.GetType("System.Int32"));
            this._dtListaPedido.Columns.Add("Cliente", Type.GetType("System.Int32"));
            this._dtListaPedido.Columns.Add("Nombre", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("Litros", Type.GetType("System.Double"));
            this._dtListaPedido.Columns.Add("Precio", Type.GetType("System.Decimal"));
            this._dtListaPedido.Columns.Add("Importe", Type.GetType("System.Decimal"));
            this._dtListaPedido.Columns.Add("FormaPago", Type.GetType("System.Byte"));
            this._dtListaPedido.Columns.Add("FormaPagoDescripcion", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("TipoPedido", Type.GetType("System.Byte"));
            this._dtListaPedido.Columns.Add("SerieRemision", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("Remision", Type.GetType("System.Int64"));
            //this._dtListaPedido.Columns.Add("Remision", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("FolioRemision", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("Origen", Type.GetType("System.Int32"));
            this._dtListaPedido.Columns.Add("ConsecutivoOrigen", Type.GetType("System.Int64"));
            this._dtListaPedido.Columns.Add("ObservacionesConciliacion", Type.GetType("System.String"));
            this._dtListaPedido.Columns.Add("Descuento", Type.GetType("System.Decimal"));
            this._dtListaPedido.Columns.Add("IdCRM", Type.GetType("System.Int64"));
            this._dtListaPedido.PrimaryKey = new DataColumn[1]
            {
                this._dtListaPedido.Columns["ID"]
            };
        }

        private void ConsultarDirecciones(int IDDireccionEntrega)
        {

            Cliente cliente = new Cliente(IDDireccionEntrega, (byte)7, this._parametros, _usuario, _cadenaConexion);
            cliente.FSuministro = Fecha;
            cliente.ConsultaDatosCliente();

            ListaClientes.Add(cliente);
        }

        public void ConsultaPedidos()
        {
            List<int> clientesDistintos = new List<int>();
            Cliente cliente = new Cliente(0,7);
            
            List< RTGMCore.DireccionEntrega > ListaDireccionesEntrega = new List<RTGMCore.DireccionEntrega>();

            this._datosFolio.Usuario = _usuario;
            
            this._datosFolio.ConsultaListaPedidos(this._añoAtt, this._folio, this._dtListaPedido);

            _cadenaConexion = cliente.ObtenCadenaConexion();

            clientesDistintos.Add(0);

            foreach (DataRow dataRow in (InternalDataCollectionBase)this._dtListaPedido.Rows)
            {
                int _clienteTemp = Convert.ToInt32(dataRow["Cliente"]);

                if (!clientesDistintos.Contains(_clienteTemp))
                {
                    clientesDistintos.Add(_clienteTemp);
                }
            }

            if (clientesDistintos.Count > 0)
            {
                System.Threading.Tasks.Parallel.ForEach(clientesDistintos, x => ConsultarDirecciones(x));
            }


            System.Web.HttpContext.Current.Session["ListaClientes"] = ListaClientes;


            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                int idCliente = Convert.ToInt32(dataRow["Cliente"]);
                                
                cliente=ListaClientes.FirstOrDefault(x => x.NumeroCliente== idCliente);
               
                
                dataRow["IdCRM"] = cliente.IdPedidoCRM;
                dataRow["Nombre"] = cliente.Nombre;
               
                if (!Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("DescuentoProntoPago"))))
                {
                    dataRow["Descuento"] = !cliente.Encontrado ? (object)0 : (object)(cliente.Descuento * Convert.ToDecimal(dataRow["Litros"]));
                    if ((int)Convert.ToInt16(this._parametros.ValorParametro("LiqPrecioNeto")) == 0)
                    {
                        Precio precio = new Precio(this._claseRuta, this._fecha, this._preciosMultiples);
                        DataTable dataTable = new DataTable();
                        Decimal num1 = ControlDeDescuento.Instance.PrecioAutorizado(precio.ListaPrecios(), cliente.Descuento, cliente.ZonaEconomica);
                        if (Convert.ToDecimal(dataRow["Precio"]) == num1)
                        {
                            dataRow["Descuento"] = (object)0;
                        }
                        else
                        {
                            dataRow["Descuento"] = (object)(cliente.Descuento * Convert.ToDecimal(dataRow["Litros"]));
                            Decimal num2 = cliente.Descuento * Convert.ToDecimal(dataRow["Litros"]);
                        }
                    }
                }
            }
        }

        public void AltaPedido(int Cliente, short Celula, short AñoPed, int NumeroPedido, string Nombre, string PedidoReferencia, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, byte TipoPedido, string Status, int Origen, int ConsecutivoOrigen, string FolioRemision, string ObservacionesConciliacion, Decimal Descuento, Int64 IdCRM=0)
        {
            int num = 0;
            if (this._dtListaPedido.Rows.Count > 0)
            {
                num = (int)this._dtListaPedido.Compute("MAX(ID)", (string)null) + 1;
            }
            DataRow row = this._dtListaPedido.NewRow();
            row["ID"] = (object)num;
            row["Celula"] = (object)Celula;
            row["AñoPed"] = (object)AñoPed;
            row["Pedido"] = (object)NumeroPedido;
            row["Cliente"] = (object)Cliente;
            row["Nombre"] = (object)Nombre;
            row["PedidoReferencia"] = (object)PedidoReferencia;
            row["Litros"] = (object)Litros;
            row["Precio"] = (object)Precio;
            row["Importe"] = (object)Importe;
            row["FormaPago"] = (object)FormaPago;
            row["TipoPedido"] = (object)TipoPedido;
            row["Status"] = (object)Status;
            row["Origen"] = (object)Origen;
            row["ConsecutivoOrigen"] = (object)ConsecutivoOrigen;
            row["FormaPagoDescripcion"] = (object)this._catalogos.ListaTipoCobro.Rows.Find((object)FormaPago)["TipoPago"].ToString().Trim();
            row["FolioRemision"] = (object)FolioRemision;
            row["ObservacionesConciliacion"] = (object)ObservacionesConciliacion;
            row["Descuento"] = (object)Descuento;
            row["IdCRM"] = (object)IdCRM;
            this._dtListaPedido.Rows.Add(row);
        }
        
        public void EdicionPedido(int SourceRow, int Cliente, string Nombre, string PedidoReferencia, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, string FolioRemision, Decimal Descuento)
        {
            DataRow dataRow = this.CurrentRow(SourceRow);
            dataRow.BeginEdit();      dataRow["Cliente"] = (object) Cliente;
            dataRow["Nombre"] = (object) Nombre;
            dataRow["PedidoReferencia"] = (object) PedidoReferencia;
            dataRow["Litros"] = (object) Litros;
            dataRow["Precio"] = (object) Precio;
            dataRow["Importe"] = (object) Importe;
            dataRow["FormaPago"] = (object) FormaPago;
            dataRow["FormaPagoDescripcion"] = (object) this._catalogos.ListaTipoCobro.Rows.Find((object) FormaPago)["TipoPago"].ToString().Trim();
            dataRow["FolioRemision"] = (object) FolioRemision;
            dataRow["Descuento"] = (object) Descuento;
            dataRow.EndEdit();
        }
        
        public void RecorridoLista(DataRow Source)
        {
            int num = this._dtListaPedido.Rows.IndexOf(Source);
            for (int index = 1; index <= num; ++index)
            {
                DataRow row = this._dtListaPedido.NewRow();
                row.ItemArray = this._dtListaPedido.Rows[0].ItemArray;
                this._dtListaPedido.Rows.Remove(this._dtListaPedido.Rows[0]);
                this._dtListaPedido.Rows.InsertAt(row, this._dtListaPedido.Rows.Count);
            }
        }
        
        public void EdicionPedidoNuevo(int SourceRow, int Cliente, string Nombre, string PedidoReferencia, short Celula, short AñoPed, int NumeroPedido, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, byte TipoPedido, string Status, string FolioRemision, Decimal Descuento)
        {
            DataRow dataRow = this.CurrentRow(SourceRow);
            dataRow.BeginEdit();
            dataRow["Cliente"] = (object) Cliente;
            dataRow["Nombre"] = (object) Nombre;
            dataRow["PedidoReferencia"] = (object) PedidoReferencia;
            dataRow["Litros"] = (object) Litros;
            dataRow["Precio"] = (object) Precio;
            dataRow["Importe"] = (object) Importe;
            dataRow["FormaPago"] = (object) FormaPago;
            dataRow["TipoPedido"] = (object) TipoPedido;
            dataRow["Status"] = (object) Status;
            dataRow["Celula"] = (object) Celula;
            dataRow["AñoPed"] = (object) AñoPed;
            dataRow["Pedido"] = (object) NumeroPedido;
            dataRow["FormaPagoDescripcion"] = (object) this._catalogos.ListaTipoCobro.Rows.Find((object) FormaPago)["TipoPago"].ToString().Trim();
            dataRow["FolioRemision"] = (object) FolioRemision;
            dataRow["Descuento"] = (object) Descuento;
            dataRow.EndEdit();
        }
        
        public void DesasignacionPedido(int SourceRow)
        {
            this._dtListaPedido.Rows.Remove(this.CurrentRow(SourceRow));
        }

        public void DescargaSuministros(TipoOperacionDescarga TipoDescarga)
        {
            ControlDeCredito.Instance.Parametros = this._parametros;
            List<int> clientesDistintos = new List<int>();

            Cliente Cliente2;
            ControlDeDescuento.Instance.Parametros = this._parametros;
            bool folioRemisionAutomatico = Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("FolioRemisionAutomatico")));
            Precio precio = Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("MultipesZonasEconomicas"))) ?
            new Precio((int)this._ruta, this._claseRuta, this._fecha, this._preciosMultiples) :
            new Precio(this._claseRuta, this._fecha, this._preciosMultiples);

            if (this._status == "LIQUIDADO" || this._status == "LIQCAJA")
            {
                return;
            }
            DataTable ListaPedidos = new DataTable("Suministros");
            if (TipoDescarga == TipoOperacionDescarga.Rampac)
            {
                ListaPedidos = this._datosFolio.DescargaRampac(this._añoAtt, this._folio);
            }
            if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("SumarizarSuministros"))))
            {
                ListaPedidos = this.AgruparSuministros(ListaPedidos);
            }

            foreach (DataRow dataRow in ListaPedidos.Rows)
            {
                int Cliente1 = (int)dataRow["Cliente"];

                Cliente2 = ListaClientes.FirstOrDefault(x => x.NumeroCliente == Cliente1);

                if (Cliente2 == null)
                {
                    if (!clientesDistintos.Contains(Cliente1))
                    {
                        clientesDistintos.Add(Cliente1);

                    }
                }

            }

            if (clientesDistintos.Count > 0)
            {
                System.Threading.Tasks.Parallel.ForEach(clientesDistintos, x => ConsultarDirecciones(x));
            }


            System.Web.HttpContext.Current.Session["ListaClientes"] = ListaClientes;

            foreach (DataRow dataRow in ListaPedidos.Rows)
            {
                int Cliente1 = (int)dataRow["Cliente"];
                int ConsecutivoOrigen = (int)dataRow["Consecutivo"];
                string FormaPago = (string)dataRow["FormaPago"];
                Decimal Precio = (Decimal)dataRow["Precio"];
                Decimal TotalPedido = (Decimal)dataRow["Importe"];


                //Cliente2.FSuministro = Fecha;//21-07-15 Consulta de precio de acuerdo a la zona económica del cliente.

                //Cliente2.ConsultaDatosCliente();


                Cliente2 = ListaClientes.FirstOrDefault(x => x.NumeroCliente == Cliente1);
                int folioRemision = 0;
                string folioRemisionCompleto = string.Empty;
                if (folioRemisionAutomatico && this._folioRemisionAutomaticoRuta)
                {
                    //TODO: Revisar como parametrizar remisión SGC
                    /*
                        * SerieDocumento.SeparaSerie(Convert.ToString(dataRow["FolioNota"]));
                        * //this._serieRemision = SerieDocumento.get_Serie();
                        * this._serieRemision = SerieDocumento.Serie;
                        * //num1 = SerieDocumento.get_FolioNota();
                        * folioRemision = SerieDocumento.FolioNota;
                    * */
                    string folioNotaRemision = Convert.ToString(dataRow["FolioNota"]);
                    //Temporal, para pedidos sin número de contrato
                    folioRemisionCompleto = folioNotaRemision;
                    //
                    string serieNotaRemision = folioNotaRemision.Substring(0, this.longitudSerieNota);
                    folioNotaRemision = folioNotaRemision.Remove(0, this.longitudSerieNota);
                    this._serieRemision = serieNotaRemision;
                    folioRemision = Convert.ToInt32(folioNotaRemision);
                }

                if (Cliente2 != null)
                {
                    if (Cliente2.Encontrado && Cliente2.NumeroCliente > 0)
                    //if (Cliente2.Encontrado)
                    {
                        string ObservacionesConciliacion = string.Empty;
                        bool creditoAutorizado = true;
                        if (!ControlDeCredito.Instance.AutorizacionCredito(ControlDeCredito.Instance.AsignarFormaPago(FormaPago, "CRÉDITO", Cliente2.TipoCreditoCliente), TotalPedido, ControlDeCredito.Instance.ResumenSaldoCliente(Cliente2.NumeroCliente, this._dtListaPedido), Cliente2, this.Tripulacion.LimiteCreditoDisponible(ControlDeCredito.Instance.ResumenSaldoTipoCobro((byte)9, this._dtListaPedido, "CONCILIADO"))))
                        {
                            creditoAutorizado = false;
                            ObservacionesConciliacion = "Crédito no autorizado";
                        }
                        if (!ControlDeDescuento.Instance.DescuentoAutorizado(Cliente2, (Decimal)dataRow["Precio"], precio.ListaPrecios()))
                        {
                            creditoAutorizado = false;
                            //22-07-2015 - Selección del precio de acuerdo a la zona económica del cliente.
                            creditoAutorizado = ((Decimal)dataRow["Precio"] == Cliente2.PrecioCliente);

                            if (!creditoAutorizado)
                            {
                                ObservacionesConciliacion = "Descuento no autorizado (Surtido a $ " + ((Decimal)dataRow["Precio"]).ToString("0.00") + "/lt )";
                            }
                        }
                        if (creditoAutorizado)
                        {
                            Pedido pedido = new Pedido(Cliente2.NumeroCliente);
                            bool pedidoActivo = false;
                            try
                            {
                                pedidoActivo = pedido.ConsultaPedidoActivo();
                            }
                            catch (Exception ex)
                            {
                            }
                            if (!pedidoActivo)
                            {
                                try
                                {
                                    pedido.AltaPedido(this._añoAtt, this._folio, Cliente2.Celula, Cliente2.Ruta, this._fecha, this._usuario, Cliente2.IdPedidoCRM);
                                    pedido.ConsultaPedidoActivo();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            pedido.AñoAtt = this._añoAtt;
                            pedido.FolioAtt = this._folio;
                            pedido.Litros = Convert.ToDouble(dataRow["Litros"]);
                            pedido.FechaSuministro = this._fecha;
                            pedido.Precio = Precio;
                            pedido.RutaSuministro = this._ruta;
                            pedido.AutoTanque = this._autoTanque;
                            pedido.FormaPago = ControlDeCredito.Instance.AsignarFormaPago(FormaPago, "CRÉDITO", Cliente2.TipoCreditoCliente);
                            pedido.TipoDescarga = TipoDescarga;
                            pedido.ConsecutivoOrigen = ConsecutivoOrigen;
                            if (folioRemision > 0)
                            {
                                pedido.SerieRemision = this._serieRemision;
                                pedido.FolioRemision = folioRemision;
                            }
                            Decimal num2 = new Decimal(0);
                            Decimal Descuento = !Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("DescuentoProntoPago"))) ? new Decimal(0) : Convert.ToDecimal(pedido.Litros) * Cliente2.Descuento;
                            if ((int)Convert.ToInt16(this._parametros.ValorParametro("LiqPrecioNeto")) == 0)
                            {
                                Descuento = !(pedido.Precio < precio.PrecioVigente) ? (!Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("DescuentoProntoPago"))) ? new Decimal(0) : Convert.ToDecimal(pedido.Litros) * Cliente2.Descuento) : new Decimal(0);
                            }
                            pedido.ImporteDescuentoAplicado = Convert.ToDecimal(pedido.Litros) * Cliente2.Descuento;
                            pedido.DescuentoAplicado = Descuento == new Decimal(0) && pedido.ImporteDescuentoAplicado > new Decimal(0);
                            try
                            {
                                pedido.LiquidaPedido();
                                this.AltaPedido(Cliente2.NumeroCliente, pedido.Celula, pedido.AñoPed, pedido.NumeroPedido, Cliente2.Nombre,
                                pedido.PedidoReferencia, pedido.Litros, pedido.Precio, pedido.Importe, pedido.FormaPago,
                                pedido.TipoPedido, "CONCILIADO", Convert.ToInt32((object)TipoDescarga), ConsecutivoOrigen, folioRemision > 0 ? folioRemision.ToString() : string.Empty,
                                string.Empty, Descuento, Cliente2.IdPedidoCRM);
                            }
                            catch (Exception ex)
                            {
                                Trace.Write((object)ex);
                            }
                        }
                        else
                        {
                            this.AltaPedido(Cliente1, (short)0, (short)0, 0, Cliente2.Nombre, string.Empty, Convert.ToDouble(dataRow["Litros"]), Precio, (Decimal)dataRow["Litros"] * Precio, ControlDeCredito.Instance.AsignarFormaPago(FormaPago, "CRÉDITO", Cliente2.TipoCreditoCliente), (byte)0, "ERROR", Convert.ToInt32((object)TipoDescarga), ConsecutivoOrigen, string.Empty, ObservacionesConciliacion, new Decimal(0));
                        }
                    }
                    else
                    {

                        this.AltaPedido(Cliente1, (short)0, (short)0, 0, string.Empty, string.Empty,
                        Convert.ToDouble(dataRow["Litros"]), Precio, (Decimal)dataRow["Litros"] * Precio, (byte)5, (byte)0,
                        "PENDIENTE", Convert.ToInt32((object)TipoDescarga), ConsecutivoOrigen,
                        folioRemision > 0 ? folioRemisionCompleto : string.Empty,
                        string.Empty, new Decimal(0));
                    }
                }
                else
                {

                    this.AltaPedido(Cliente1, (short)0, (short)0, 0, string.Empty, string.Empty,
                    Convert.ToDouble(dataRow["Litros"]), Precio, (Decimal)dataRow["Litros"] * Precio, (byte)5, (byte)0,
                    "PENDIENTE", Convert.ToInt32((object)TipoDescarga), ConsecutivoOrigen,
                    folioRemision > 0 ? folioRemisionCompleto : string.Empty,
                    string.Empty, new Decimal(0));
                }
            }
        }

        public DataRow CurrentRow(int Row)
        {
            return this._dtListaPedido.Rows.Find((object) Row);
        }
        
        private void ReordenarLista()
        {
            string str = this._parametros.ValorParametro("OrdenDescarga").ToString().ToUpper();
            DataView defaultView = this._dtListaPedido.DefaultView;
            switch (str)
            {
                case "HORAINICIO":
                    defaultView.Sort = "ConsecutivoOrigen";
                    break;
                case "LITROS":
                    defaultView.Sort = "Litros";
                    break;
                case null:
                    return;
                default:
                    return;
            }
            this._dtListaPedido = defaultView.ToTable();
            foreach (DataRow row in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                row.BeginEdit();
                row["ID"] = (object) (this._dtListaPedido.Rows.IndexOf(row) + 1);
                row.EndEdit();
            }
            this._dtListaPedido.PrimaryKey = new DataColumn[1]
            {
                this._dtListaPedido.Columns["ID"]
            };
        }
        
        public void ReordenarLista(string Columna)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = "";
            defaultView.Sort = Columna;
            this._dtListaPedido = defaultView.ToTable();
            foreach (DataRow row in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                row.BeginEdit();
                row["ID"] = (object) (this._dtListaPedido.Rows.IndexOf(row) + 1);
                row.EndEdit();
            }
            this._dtListaPedido.PrimaryKey = new DataColumn[1]
            {
                this._dtListaPedido.Columns["ID"]
            };
        }
        
        public DataView FiltrarLista(string FilterBy)
        {
            DataTable dataTable = new DataTable("Filtros");
            DataColumn column1 = new DataColumn("Value", Type.GetType("System.Object"));
            DataColumn column2 = new DataColumn("Key", Type.GetType("System.String"));
            dataTable.Columns.Add(column1);
            dataTable.Columns.Add(column2);
            dataTable.PrimaryKey = new DataColumn[1]
            {
                column1
            };
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = string.Empty;
            defaultView.Sort = FilterBy;
            foreach (DataRow dataRow in (InternalDataCollectionBase) defaultView.ToTable().Rows)
            {
                if (dataTable.Rows.Find(dataRow[FilterBy]) == null)
                {
                    DataRow row = dataTable.NewRow();
                    row.BeginEdit();
                    row["Value"] = dataRow[FilterBy];
                    row["Key"] = (object) FilterBy;
                    row.EndEdit();
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable.DefaultView;
        }
        
        public int AplicarFiltroPersonalizado(string FilterBy, object FilterKey, bool ExactWord)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = string.Empty;
            defaultView.Sort = FilterBy;
            foreach (DataRow dataRow in (InternalDataCollectionBase) defaultView.ToTable().Rows)
            {
                if (dataRow[FilterBy].ToString() == FilterKey.ToString() || ExactWord && dataRow[FilterBy].ToString().Contains(FilterKey.ToString()))
                {
                    return Convert.ToInt32(dataRow["ID"]);
                }
            }
            return -1;
        }
        
        public int AplicarFiltro(string FilterBy, object FilterKey)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = string.Empty;
            defaultView.Sort = FilterBy;
            foreach (DataRow dataRow in (InternalDataCollectionBase) defaultView.ToTable().Rows)
            {
                if (dataRow[FilterBy].ToString() == FilterKey.ToString())
                {
                    return Convert.ToInt32(dataRow["ID"]);
                }
            }
            return -1;
        }
        
        public void ConfigurarLista()
        {
            this.ReordenarLista();
            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                if (this._catalogos.ListaTipoCobro.Rows.Count > 0)
                {
                    dataRow.BeginEdit();
                    
                    dataRow["FormaPagoDescripcion"] = this._catalogos.ListaTipoCobro.Rows.Find(dataRow["FormaPago"])!=null?(object) this._catalogos.ListaTipoCobro.Rows.Find(dataRow["FormaPago"])["TipoPago"].ToString().Trim():"SIN DESCRIPCIÓN";
                    dataRow.EndEdit();
                }
            }
        }
        
        public DataTable AgruparSuministros(DataTable ListaPedidos)
        {
            DataTable dataTable = ListaPedidos.Clone();
            dataTable.Columns.Add("ListaSuministros", Type.GetType("System.Object"));
            foreach (DataRow dataRow1 in (InternalDataCollectionBase) ListaPedidos.Rows)
            {
                List<int> list = new List<int>();
                double num1 = 0.0;
                Decimal num2 = new Decimal(0);
                int num3 = Convert.ToInt32(dataRow1["Cliente"]);
                DataRow row = (DataRow) null;
                if (num3 > 0)
                {
                    foreach (DataRow dataRow2 in (InternalDataCollectionBase) dataTable.Rows)
                    {
                        if (Convert.ToInt32(dataRow2["Cliente"]) == num3)
                        {
                            row = dataRow2;
                            num1 = Convert.ToDouble(dataRow2["Litros"]);
                            num2 = Convert.ToDecimal(dataRow2["Importe"]);
                            break;
                        }
                    }
                }
                if (row == null)
                {
                    row = dataTable.NewRow();
                    row["ListaSuministros"] = (object) list;
                    dataTable.Rows.Add(row);
                }
                row.BeginEdit();
                foreach (DataColumn dataColumn in (InternalDataCollectionBase)ListaPedidos.Columns)
                {
                    row[dataColumn.ColumnName] = dataRow1[dataColumn.ColumnName];
                }
                row["Litros"] = (object) (num1 + Convert.ToDouble(dataRow1["Litros"]));
                row["Importe"] = (object) (num2 + Convert.ToDecimal(dataRow1["Importe"]));
                ((List<int>) row["ListaSuministros"]).Add(Convert.ToInt32(dataRow1["Consecutivo"]));
                row.EndEdit();
            }
            return dataTable;
        }
        
        public bool ConciliacionValida()
        {
            int count1 = this.SuministrosPorTipo("CONCILIADO").Rows.Count;
            int count2 = this.SuministrosPorTipo("PENDIENTE").Rows.Count;
            int count3 = this.SuministrosPorTipo("ERROR").Rows.Count;
            if (count2 == 0)
            {
                return count3 == 0;
            }
            return false;
        }
        
        public DataTable SuministrosPorTipo(string Filtro)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = "Status = '" + Filtro.Trim() + "'";
            return defaultView.ToTable();
        }
        
        public DataTable SuministrosPorFormaPago(string FormaPago)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = "FormaPagoDescripcion = '" + FormaPago.Trim() + "'";
            return defaultView.ToTable();
        }
        
        public DataTable SuministrosPorTipoPedido(byte TipoPedido)
        {
            DataView defaultView = this._dtListaPedido.DefaultView;
            defaultView.RowFilter = "TipoPedido = '" + TipoPedido.ToString() + "'";
            return defaultView.ToTable();
        }
        
        public double TotalLitros()
        {
            double num = 0.0;
            if (this._dtListaPedido.Rows.Count > 0)
            {
                num = Convert.ToDouble(_dtListaPedido.Compute("Sum(Litros)", "").ToString());
            }
            return num;
        }
        
        public bool ValidarCapturaRemisiones()
        {
            int num = 0;
            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                if (Convert.ToString(dataRow["FolioRemision"]).Trim().Length <= 0)
                {
                    ++num;
                }
            }
            return num <= 0;
        }
        
        public string CapturaRemisiones()
        {
            string str = string.Empty;
            int num1 = 0;
            int num2 = 0;
            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                if (Convert.ToString(dataRow["FolioRemision"]).Trim().Length > 0)
                {
                    ++num1;
                }
                else
                {
                    ++num2;
                }
            }
            if (num2 > 0)
            {
                str = "No puede cerrar la liquidación porque" + (object)'\r' + "no ha capturado números de remisión para" + (string)(object)'\r' + num2.ToString() + " pedidos.";
            }
            return str;
        }
        
        public DataTable ResumenOperaciones()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Tipo", Type.GetType("System.String"));
            dataTable.Columns.Add("Descripcion", Type.GetType("System.String"));
            dataTable.Columns.Add("Resumen", Type.GetType("System.String"));
            DataRow row1 = dataTable.NewRow();
            row1["Tipo"] = (object) "TODO";
            row1["Descripcion"] = (object) "Suministros";
            row1["Resumen"] = (object) this._dtListaPedido.Rows.Count.ToString();
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["Tipo"] = (object) "CONCILIADO";
            row2["Descripcion"] = (object) "Conciliados";
            row2["Resumen"] = (object) this.SuministrosPorTipo("CONCILIADO").Rows.Count;
            dataTable.Rows.Add(row2);
            DataRow row3 = dataTable.NewRow();
            row3["Tipo"] = (object) "NUEVO";
            row3["Descripcion"] = (object) "Por conciliar";
            row3["Resumen"] = (object) this.SuministrosPorTipo("NUEVO").Rows.Count;
            dataTable.Rows.Add(row3);
            DataRow row4 = dataTable.NewRow();
            row4["Tipo"] = (object) "PENDIENTE";
            row4["Descripcion"] = (object) "Pedidos pendientes";
            row4["Resumen"] = (object) "0";
            dataTable.Rows.Add(row4);
            return dataTable;
        }
        
        public DataTable ResumenBoletines()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Tipo", Type.GetType("System.String"));
            dataTable.Columns.Add("Descripcion", Type.GetType("System.String"));
            dataTable.Columns.Add("Resumen", Type.GetType("System.String"));
            DataRow row1 = dataTable.NewRow();
            row1["Tipo"] = (object) "PENDIENTE";
            row1["Descripcion"] = (object) "Pendientes";
            row1["Resumen"] = (object) "0";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["Tipo"] = (object) "ENVIADO";
            row2["Descripcion"] = (object) "Enviados";
            row2["Resumen"] = (object) "0";
            dataTable.Rows.Add(row2);
            return dataTable;
        }
        
        public DataTable PedidosContado()
        {
            DataTable dataTable = new DataTable("ListaPedidos");
            dataTable.Columns.Add("Litros", Type.GetType("System.Double"));
            dataTable.Columns.Add("PedidoReferencia", Type.GetType("System.String"));
            dataTable.Columns.Add("Cliente", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Total", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("Saldo", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("Celula", Type.GetType("System.Int16"));
            dataTable.Columns.Add("AñoPed", Type.GetType("System.Int16"));
            dataTable.Columns.Add("Pedido", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Descripcion", Type.GetType("System.String"));
            dataTable.Columns.Add("Descuento", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("IdCRM", Type.GetType("System.Int32"));
            dataTable.Columns.Add("ConsecutivoOrigen", Type.GetType("System.Int32"));

            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtListaPedido.Rows)
            {
                if ((int) Convert.ToByte(dataRow["FormaPago"]) == 5 && Convert.ToString(dataRow["Status"]) == "CONCILIADO")
                {
                    DataRow row = dataTable.NewRow();
                    row.BeginEdit();
                    row["Litros"] = dataRow["Litros"];
                    row["PedidoReferencia"] = dataRow["PedidoReferencia"];
                    row["Cliente"] = dataRow["Cliente"];
                    row["Total"] = dataRow["Importe"];
                    row["Saldo"] = dataRow["Importe"];
                    row["Celula"] = dataRow["Celula"];          
                    row["AñoPed"] = dataRow["AñoPed"];
                    row["Pedido"] = dataRow["Pedido"];
                    row["Descripcion"] = (object) ("Pedido: " + Convert.ToString(dataRow["PedidoReferencia"]) + " Cliente: " + Convert.ToString(dataRow["Cliente"]) + " " + Convert.ToString(dataRow["Nombre"]) + " Litros: " + Convert.ToString(dataRow["Litros"]));
                    row["Descuento"] = dataRow["Descuento"];
                    row["IdCRM"] = dataRow["IdCRM"];
                    row["ConsecutivoOrigen"] = dataRow["ConsecutivoOrigen"];

                    row.EndEdit();
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
        
        public bool LiquidacionIniciada(ref string Usuario, short AñoAtt, int Folio)
        {
            DataTable dataTable;
            try
            {
                dataTable = this._datosFolio.InicioLiquidacion(AñoAtt, Folio, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (dataTable.Rows.Count > 0)
            {
                string str1 = Convert.ToString(dataTable.Rows[0]["Status"]).Trim().ToUpper();
                string str2 = Convert.ToString(dataTable.Rows[0]["Usuario"]).Trim().ToUpper();
                if (str1 == "INICIADO" && str2 != Usuario.Trim().ToUpper())
                {
                    Usuario = str2;
                    return false;
                }
                if (str1 == "INICIADO" && str2 == Usuario.Trim().ToUpper())
                {
                    return true;
                }
                if (str1 == "TERMINADO")
                {
                    return this._datosFolio.ReinicioLiquidacion(AñoAtt, Folio, Usuario.Trim().ToUpper());
                }
            }
            return false;
        }
        
        public void TerminarCapturaLiquidacion()
        {
            try
            {
                this._datosFolio.TerminoLiquidacion(this._añoAtt, this._folio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void AltaInicioLiquidacionFolio()
        {
            try
            {
                this._datosFolio.AltaInicioLiquidacionFolio(this._añoAtt, this._folio, this._tipoLiquidacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable ResumenLiquidacionFinal(string Usuario)
        {
            DataTable dataTable1 = new DataTable("ResumenLiquidacionFinal");
            dataTable1.Columns.Add("ImporteCredito", Type.GetType("System.Decimal"));
            dataTable1.Columns.Add("LitrosCredito", Type.GetType("System.Double"));
            dataTable1.Columns.Add("ImporteContado", Type.GetType("System.Decimal"));
            dataTable1.Columns.Add("LitrosContado", Type.GetType("System.Double"));
            dataTable1.Columns.Add("TipoLiquidacion", Type.GetType("System.String"));
            dataTable1.Columns.Add("Usuario", Type.GetType("System.String"));
            dataTable1.Columns.Add("AñoAtt", Type.GetType("System.Int16"));
            dataTable1.Columns.Add("Folio", Type.GetType("System.Int32"));
            DataRow row = dataTable1.NewRow();
            row.BeginEdit();
            DataTable dataTable2 = this.SuministrosPorFormaPago("CONTADO");
            if (dataTable2.Rows.Count > 0)
            {
                row["ImporteContado"] = (object) Convert.ToDecimal(dataTable2.Compute("SUM(Importe)", ""));
                row["LitrosContado"] = (object) Convert.ToDouble(dataTable2.Compute("SUM(Litros)", ""));
            }
            else
            {
                row["ImporteContado"] = (object) 0;
                row["LitrosContado"] = (object) 0;
            }
            DataTable dataTable3 = this.SuministrosPorFormaPago("CREDITO");
            if (dataTable3.Rows.Count > 0)
            {
                row["ImporteCredito"] = (object) Convert.ToDecimal(dataTable3.Compute("SUM(Importe)", ""));
                row["LitrosCredito"] = (object) Convert.ToDouble(dataTable3.Compute("SUM(Litros)", ""));
            }
            else
            {
                row["ImporteCredito"] = (object) 0;
                row["LitrosCredito"] = (object) 0;
            }
            row["TipoLiquidacion"] = (object) "AUTOMATICA";
            row["Usuario"] = (object) Usuario;
            row["AñoAtt"] = (object) this._añoAtt;
            row["Folio"] = (object) this._folio;
            row.EndEdit();
            dataTable1.Rows.Add(row);
            return dataTable1;
        }
        
        public void CancelarCobros(short AñoAtt, int Folio)
        {
            try
            {
                this._datosFolio.CancelacionCobros(AñoAtt, Folio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void CancelarPedidos(short AñoAtt, int Folio)
        {
            try
            {
                this._datosFolio.CancelacionPedidos(AñoAtt, Folio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable ConsultaTotalCobros(short AñoAtt, int Folio)
        {
            return this._datosFolio.ConsultaTotalCobros(AñoAtt, Folio);
        }

        public bool ActualizarBloqueLiquidaciones(DataTable tablaDatos)
        {
            return this._datosFolio.ActualizarBloqueLiquidaciones(tablaDatos);
        }
    }
}
