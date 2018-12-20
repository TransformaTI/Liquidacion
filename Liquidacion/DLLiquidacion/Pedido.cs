// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Pedido
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using DocumentosBSR;
using System;

namespace SigametLiquidacion
{
  [Serializable]
  public class Pedido
  {
    private short _celula;
    private short _añoPed;
    private int _pedido;
    private string _status;
    private int _cliente;
    private byte _formaPago;
    private double _litros;
    private Decimal _precio;
    private Decimal _importe;
    private Decimal _iva;
    private DateTime _fechaSuministro;
    private string _pedidoReferencia;
    private byte _tipoPedido;
    private short _añoAtt;
    private int _folioAtt;
    private short _rutaSuministro;
    private int _autoTanque;
    private string _serieRemision;
    private int _folioRemision;
    private string _tipoLiquidacion;
    private DatosPedido _datosPedido;
    private TipoOperacionDescarga _tipoDescarga;
    private int _consecutivoOrigen;
    private bool _descuentoAplicado;
    private Decimal _importeDescuentoAplicado;
    public string _factura;

    public bool DescuentoAplicado
    {
      get
      {
        return this._descuentoAplicado;
      }
      set
      {
        this._descuentoAplicado = value;
      }
    }

    public Decimal ImporteDescuentoAplicado
    {
      get
      {
        return this._importeDescuentoAplicado;
      }
      set
      {
        this._importeDescuentoAplicado = value;
      }
    }

    public short Celula
    {
      get
      {
        return this._celula;
      }
      set
      {
        this._celula = value;
      }
    }

    public short AñoPed
    {
      get
      {
        return this._añoPed;
      }
      set
      {
        this._añoPed = value;
      }
    }

    public int NumeroPedido
    {
      get
      {
        return this._pedido;
      }
      set
      {
        this._pedido = value;
      }
    }

    public string Status
    {
      get
      {
        return this._status;
      }
    }

    public byte TipoPedido
    {
      get
      {
        return this._tipoPedido;
      }
    }

    public int Cliente
    {
      get
      {
        return this._cliente;
      }
      set
      {
        this._cliente = value;
      }
    }

    public byte FormaPago
    {
      get
      {
        return this._formaPago;
      }
      set
      {
        this._formaPago = value;
      }
    }

    public double Litros
    {
      get
      {
        return this._litros;
      }
      set
      {
        this._litros = value;
        this.CalcularImporte();
      }
    }

    public Decimal Precio
    {
      get
      {
        return this._precio;
      }
      set
      {
        this._precio = value;
        this.CalcularImporte();
      }
    }

    public Decimal Importe
    {
      get
      {
        return this._importe;
      }
    }

    public Decimal IVA
    {
      get
      {
        return this._iva;
      }
      set
      {
        this._iva = value;
      }
    }

    public DateTime FechaSuministro
    {
      get
      {
        return this._fechaSuministro;
      }
      set
      {
        this._fechaSuministro = value;
      }
    }

    public string PedidoReferencia
    {
      get
      {
        return this._pedidoReferencia;
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

    public int FolioAtt
    {
      get
      {
        return this._folioAtt;
      }
      set
      {
        this._folioAtt = value;
      }
    }

    public short RutaSuministro
    {
      get
      {
        return this._rutaSuministro;
      }
      set
      {
        this._rutaSuministro = value;
      }
    }

    public int AutoTanque
    {
      get
      {
        return this._autoTanque;
      }
      set
      {
        this._autoTanque = value;
      }
    }

    public string SerieRemision
    {
      get
      {
        return this._serieRemision;
      }
      set
      {
        this._serieRemision = value;
      }
    }

    public int FolioRemision
    {
      get
      {
        return this._folioRemision;
      }
      set
      {
        this._folioRemision = value;
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

    public TipoOperacionDescarga TipoDescarga
    {
      get
      {
        return this._tipoDescarga;
      }
      set
      {
        this._tipoDescarga = value;
      }
    }

    public int ConsecutivoOrigen
    {
      get
      {
        return this._consecutivoOrigen;
      }
      set
      {
        this._consecutivoOrigen = value;
      }
    }

    public string Factura
    {
      get
      {
        return this._factura;
      }
    }

    public Pedido(int Cliente)
    {
      this._cliente = Cliente;
      this._datosPedido = new DatosPedido(this._cliente);
    }

    public Pedido(short Celula, short AñoPed, int Pedido)
    {
      this._datosPedido = new DatosPedido(Celula, AñoPed, Pedido);
      this._datosPedido.ConsultaDatosPedidoLiquidado();
      this._celula = this._datosPedido.Celula;
      this._añoPed = this._datosPedido.AñoPed;
      this._pedido = this._datosPedido.Pedido;
      this._cliente = this._datosPedido.Cliente;
      this._pedidoReferencia = this._datosPedido.PedidoReferencia;
      this._litros = this._datosPedido.Litros;
      this._precio = this._datosPedido.Precio;
      this._importe = this._datosPedido.Importe;
      this._formaPago = this._datosPedido.TipoCobro;
      this._tipoPedido = this._datosPedido.TipoPedido;
      this._añoAtt = this._datosPedido.AñoAtt;
      this._folioAtt = this._datosPedido.Folio;
      this._rutaSuministro = this._datosPedido.RutaSuministro;
      this._iva = this._datosPedido.IVA;
      this._autoTanque = this._datosPedido.AutoTanque;
      this._fechaSuministro = this._datosPedido.FSuministro;
      this._serieRemision = this._datosPedido.SerieRemision;
      this._folioRemision = this._datosPedido.FolioNota;
    }

    public Pedido(string PedidoReferencia)
    {
      this._datosPedido = new DatosPedido(PedidoReferencia);
      this._datosPedido.ConsultaDatosPedidoLiquidadoPedidoReferencia();
      this._celula = this._datosPedido.Celula;
      this._añoPed = this._datosPedido.AñoPed;
      this._pedido = this._datosPedido.Pedido;
      this._cliente = this._datosPedido.Cliente;
      this._pedidoReferencia = this._datosPedido.PedidoReferencia;
      this._litros = this._datosPedido.Litros;
      this._precio = this._datosPedido.Precio;
      this._importe = this._datosPedido.Importe;
      this._formaPago = this._datosPedido.TipoCobro;
      this._tipoPedido = this._datosPedido.TipoPedido;
      this._añoAtt = this._datosPedido.AñoAtt;
      this._folioAtt = this._datosPedido.Folio;
      this._rutaSuministro = this._datosPedido.RutaSuministro;
      this._iva = this._datosPedido.IVA;
      this._autoTanque = this._datosPedido.AutoTanque;
      this._fechaSuministro = this._datosPedido.FSuministro;
      this._serieRemision = this._datosPedido.SerieRemision;
      this._folioRemision = this._datosPedido.FolioNota;
      this._factura = this._datosPedido.Factura;
    }

    public Pedido(string SerieRemision, int Remision)
    {
      this._datosPedido = new DatosPedido(SerieRemision, Remision);
      this._datosPedido.ConsultaDatosPedidoLiquidadoRemision();
      this._celula = this._datosPedido.Celula;
      this._añoPed = this._datosPedido.AñoPed;
      this._pedido = this._datosPedido.Pedido;
      this._cliente = this._datosPedido.Cliente;
      this._pedidoReferencia = this._datosPedido.PedidoReferencia;
      this._litros = this._datosPedido.Litros;
      this._precio = this._datosPedido.Precio;
      this._importe = this._datosPedido.Importe;
      this._formaPago = this._datosPedido.TipoCobro;
      this._tipoPedido = this._datosPedido.TipoPedido;
      this._añoAtt = this._datosPedido.AñoAtt;
      this._folioAtt = this._datosPedido.Folio;
      this._rutaSuministro = this._datosPedido.RutaSuministro;
      this._iva = this._datosPedido.IVA;
      this._autoTanque = this._datosPedido.AutoTanque;
      this._fechaSuministro = this._datosPedido.FSuministro;
      this._serieRemision = this._datosPedido.SerieRemision;
      this._folioRemision = this._datosPedido.FolioNota;
      this._factura = this._datosPedido.Factura;
    }

    public Pedido(TipoOperacionDescarga TipoDescarga, int Consecutivo, short AñoAtt, int Folio)
    {
      this._tipoDescarga = TipoDescarga;
      this._consecutivoOrigen = Consecutivo;
      this._añoAtt = AñoAtt;
      this._folioAtt = Folio;
      this._datosPedido = new DatosPedido(AñoAtt, Folio);
    }

    public int LiquidaPedido()
    {
      int num = 0;
      this._datosPedido.AñoAtt = this._añoAtt;
      this._datosPedido.Folio = this._folioAtt;
      this._datosPedido.AutoTanque = this._autoTanque;
      this._datosPedido.RutaSuministro = this._rutaSuministro;
      this._datosPedido.Litros = this._litros;
      this._datosPedido.Importe = this._importe;
      this._datosPedido.Precio = this._precio;
      this._datosPedido.IVA = this._iva;
      this._datosPedido.TipoCobro = this._formaPago;
      this._datosPedido.SerieRemision = this._serieRemision;
      this._datosPedido.FolioNota = this._folioRemision;
      this._datosPedido.TipoLiquidacion = this._tipoLiquidacion;
      this._datosPedido.FSuministro = this._fechaSuministro;
      this._datosPedido.TipoDescarga = this._tipoDescarga;
      this._datosPedido.ConsecutivoOrigen = this._consecutivoOrigen;
      this._datosPedido.DescuentoAplicado = this._descuentoAplicado;
      this._datosPedido.ImporteDescuentoAplicado = this._importeDescuentoAplicado;
      try
      {
        return this._datosPedido.LiquidaPedido();
      }
      catch (Exception ex)
      {
        num = -1;
        throw ex;
      }
    }

    public int ActualizaRemision()
    {
      int num = 0;
      this._datosPedido.SerieRemision = this._serieRemision;
      this._datosPedido.FolioNota = this._folioRemision;
      try
      {
        return this._datosPedido.ActualizaRemision();
      }
      catch (Exception ex)
      {
        num = -1;
        throw ex;
      }
    }

    public bool ConsultaPedidoActivo()
    {
      bool pedidoActivo;
      try
      {
        pedidoActivo = this._datosPedido.ConsultaDatosPedido();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this._celula = this._datosPedido.Celula;
      this._añoPed = this._datosPedido.AñoPed;
      this._pedido = this._datosPedido.Pedido;
      this._pedidoReferencia = this._datosPedido.PedidoReferencia;
      this._tipoPedido = this._datosPedido.TipoPedido;
      if (pedidoActivo)
      {
          this._serieRemision = this._datosPedido.SerieRemision.Trim();
          this._folioRemision = this._datosPedido.FolioNota;
      }
      return pedidoActivo;
    }

    public void ConsultaPedido()
    {
      this._datosPedido.ConsultaPedido();
      this._status = this._datosPedido.Status;
    }

    public void CalcularImporte()
    {
      this._importe = Convert.ToDecimal(this._litros) * this._precio;
    }

    public void ReinitDataComp()
    {
      this._datosPedido.DataCompInitialize();
    }

    public void AltaPedido(short AñoAtt, int Folio, short CelulaCliente, short RutaCliente, DateTime Fecha, string Usuario, int IdPedidoCRM)
    {
      try
      {
        this._datosPedido.AltaPedido(AñoAtt, Folio, CelulaCliente, RutaCliente, Fecha, Usuario, IdPedidoCRM);
        this._celula = this._datosPedido.Celula;
        this._añoPed = this._datosPedido.AñoPed;
        this._pedido = this._datosPedido.Pedido;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void DesasignaPedido()
    {
      try
      {
        if ((int) this.TipoPedido == 3)
          this._datosPedido.DesasignaPedido(this._añoAtt, this._folioAtt, this._celula, this._añoPed, this._pedido, TipoDesasignacionPedido.BorrarNotaBlanca);
        else
          this._datosPedido.DesasignaPedido(this._añoAtt, this._folioAtt, this._celula, this._añoPed, this._pedido, TipoDesasignacionPedido.DesasignarProgramado);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void PedidoActualizaRampac()
    {
      try
      {
        this._datosPedido.ActualizaRampac(this._tipoDescarga, this._consecutivoOrigen, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void SepararFolioRemision(string FolioRemision, byte LongitudSerie)
    {
        //TODO: Revisar como parametrizar remisión SGC
        /*
        SerieDocumento.SeparaSerie(FolioRemision);
        //this._folioRemision = SerieDocumento.get_FolioNota();
        this._folioRemision = SerieDocumento.FolioNota;
        //this._serieRemision = SerieDocumento.get_Serie();
        this._serieRemision = SerieDocumento.Serie;
        */
        string serieNotaRemision = FolioRemision.Substring(0, LongitudSerie);
        FolioRemision = FolioRemision.Remove(0, LongitudSerie);
        this._serieRemision = serieNotaRemision;
        this._folioRemision = Convert.ToInt32(FolioRemision);

    }

    public Decimal LimiteDisponibleOperador(short AñoAtt, int Folio)
    {
      return this._datosPedido.CreditoDisponibleOperador(AñoAtt, Folio);
    }
  }
}
