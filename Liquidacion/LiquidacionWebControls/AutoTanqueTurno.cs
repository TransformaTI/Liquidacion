// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.AutoTanqueTurno
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using SigametLiquidacion;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
  public class AutoTanqueTurno : CompositeControl
  {
    private Label lblTagFolio = new Label();
    private Label lblFolio = new Label();
    private Label lblTagAutotanque = new Label();
    private Label lblAutotanque = new Label();
    private Label lblTagCelula = new Label();
    private Label lblCelulaRuta = new Label();
    private Label lblTagOperador = new Label();
    private Label lblOperador = new Label();
    private Label lblTagLimiteCredito = new Label();
    private Label lblLimiteCreditoOperador = new Label();
    private Label lblTagTotalizador = new Label();
    private Label lblTotalizador = new Label();
    private ImageButton btnAgregar = new ImageButton();
    private ImageButton btnCierre = new ImageButton();
    private short _añoAtt;
    private int _folio;
    private Decimal _saldoOperadorMovimiento;
    private Decimal _limiteCreditoOperador;
    private string _urlCierreLiquidacion;
    private string _formaLiquidacion;
    private bool _operadorAsignado;
    private bool _validarRemisionLiquidada;
    private bool _validarRemisionExistente;
    private FolioLiquidacion _folioLiquidacion;
    private Tripulacion _tripulacion;

    private string _usuarioSistema;

    public short AñoAtt
    {
      get
      {
        if (this._folioLiquidacion == null)
          return Convert.ToInt16(0);
        return this._folioLiquidacion.AñoAtt;
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
        if (this._folioLiquidacion == null)
          return 0;
        return this._folioLiquidacion.Folio;
      }
      set
      {
        this._folio = value;
      }
    }

    public string URLCierreLiquidacion
    {
      get
      {
        return this._urlCierreLiquidacion;
      }
      set
      {
        this._urlCierreLiquidacion = value;
      }
    }

    public DataTable ListaPedidos
    {
      get
      {
        return this._folioLiquidacion.ListaPedidos;
      }
        set
        {
            this._folioLiquidacion.ListaPedidos = value;
        }
    }

    public short Celula
    {
      get
      {
        return this._folioLiquidacion.Celula;
      }
    }

    public short Ruta
    {
      get
      {
        return this._folioLiquidacion.Ruta;
      }
    }

    public int Autotanque
    {
      get
      {
        return this._folioLiquidacion.AutoTanque;
      }
    }

    public DateTime Fecha
    {
      get
      {
        return this._folioLiquidacion.Fecha;
      }
    }

    public string Status
    {
      get
      {
        return this._folioLiquidacion.Status;
      }
    }

    public string SerieRemision
    {
      get
      {
        return this._folioLiquidacion.SerieRemision;
      }
    }

    public bool PreciosMultiples
    {
      get
      {
        return this._folioLiquidacion.PreciosMultiples;
      }
    }

    public byte ClaseRuta
    {
      get
      {
        return this._folioLiquidacion.ClaseRuta;
      }
    }

    public DataTable ResumenOperaciones
    {
      get
      {
        return this._folioLiquidacion.ResumenOperaciones();
      }
    }

    public DataTable ResumenBoletines
    {
      get
      {
        return this._folioLiquidacion.ResumenBoletines();
      }
    }

    public string Usuario
    {
      set
      {
        this._folioLiquidacion.Usuario = value;
        this._usuarioSistema= value;
      }
    }

    public string FormaLiquidacion
    {
      get
      {
        return this._formaLiquidacion;
      }
      set
      {
        this._formaLiquidacion = value;
      }
    }

    public bool OperadorAsignado
    {
      get
      {
        return this._operadorAsignado;
      }
    }

    public bool ValidarRemisionLiquidada
    {
      get
      {
        return this._validarRemisionLiquidada;
      }
    }

    public bool ValidarRemisionExistente
    {
      get
      {
        return this._validarRemisionExistente;
      }
    }

    public DataTable PedidosContado
    {
      get
      {
        return this._folioLiquidacion.PedidosContado();
      }
    }

    public double LitrosLiquidados
    {
      get
      {
        return this._folioLiquidacion.DiferenciaTotalizador;
      }
    }

    public double TotalLitros
    {
      get
      {
        return this._folioLiquidacion.TotalLitros();
      }
    }

    public bool ConciliacionCompleta
    {
      get
      {
        return this._folioLiquidacion.ConciliacionValida();
      }
    }

    public bool CapturaRemisionCompleta
    {
      get
      {
        return this._folioLiquidacion.ValidarCapturaRemisiones();
      }
    }

    public byte LongitudSerie
    {
        get
        {
            return _folioLiquidacion.longitudSerieNota;
        }
    }

    public byte LongitudRemision
    {
        get
        {
            return _folioLiquidacion.longitudFolioNota;
        }
    }

      

        public event EventHandler CierreLiquidacion;

    protected virtual void OnCierreLiquidacion(EventArgs e)
    {
      if (this.CierreLiquidacion == null)
        return;
      this.CierreLiquidacion((object) this, e);
    }

    protected override object SaveViewState()
    {
      this.EnsureChildControls();
      object[] objArray = new object[4];
      object obj = base.SaveViewState();
      objArray[0] = obj;
      objArray[1] = (object) this._folioLiquidacion;
      objArray[2] = (object) this._tripulacion;
      objArray[3] = (object) this._formaLiquidacion;
      return (object) objArray;
    }

    protected override void LoadViewState(object savedState)
    {
      object[] objArray = (object[]) savedState;
      base.LoadViewState(objArray[0]);
      this._folioLiquidacion = (FolioLiquidacion) objArray[1];
      this._tripulacion = (Tripulacion) objArray[2];
      this._formaLiquidacion = (string) objArray[3];
      this.EnsureChildControls();
      this.asignarInformacionFolio();
    }

    protected override void CreateChildControls()
    {
      this.Controls.Add((Control) new LiteralControl("<table cellspacing='0'>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td colspan='6' class='EtiquetaCelulaRuta'>"));
      this.Controls.Add((Control) new LiteralControl("<div style=" + '\'' + "vertical-align:middle" + '\'' + ">"));
      this.Controls.Add((Control) this.lblCelulaRuta);
      this.Controls.Add((Control) new LiteralControl("<div>"));
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='TagLabel TagLabelFolio'>"));
      this.lblTagFolio.Text = "Folio:";
      this.Controls.Add((Control) this.lblTagFolio);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<TD class='InfoLabel LabelFolio'>"));
      this.Controls.Add((Control) this.lblFolio);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='TagLabel TagLabelAutotanque'>"));
      this.lblTagAutotanque.Text = "Autotanque:";
      this.Controls.Add((Control) this.lblTagAutotanque);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<TD class='InfoLabel LabelAutotanque'>"));
      this.Controls.Add((Control) this.lblAutotanque);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='TagLabel TagLabelTotalizador'>"));
      this.lblTagTotalizador.Text = "Totalizador";
      this.Controls.Add((Control) this.lblTagTotalizador);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<TD class='InfoLabel LabelTotalizador' colspan='3'>"));
      this.Controls.Add((Control) this.lblTotalizador);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='TagLabel TagLabelOperador'>"));
      this.lblTagOperador.Text = "Operador:";
      this.Controls.Add((Control) this.lblTagOperador);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<TD class='InfoLabel LabelOperador' colspan='3'>"));
      this.Controls.Add((Control) this.lblOperador);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='TagLabel TagLabelLimiteCredito'>"));
      this.lblTagLimiteCredito.Text = "Límite de crédito: ";
      this.Controls.Add((Control) this.lblTagLimiteCredito);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<TD class='InfoLabel LabelLimiteCredito'>"));
      this.Controls.Add((Control) this.lblLimiteCreditoOperador);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("</table>"));
    }

    public void CargaDatosFolio()
    {
      this._folioLiquidacion = new FolioLiquidacion(this._añoAtt, this._folio);
      this._tripulacion = new Tripulacion(this._añoAtt, this._folio);
      this._tripulacion.ConsultaTripulacion();
      this.asignarInformacionFolio();
    }

    private void asignarInformacionFolio()
    {
      if (this._folioLiquidacion == null || this._tripulacion == null)
        return;
      this.lblCelulaRuta.Text = "Liquidación para la ruta: " + this._folioLiquidacion.Ruta.ToString() + " célula: " + this._folioLiquidacion.Celula.ToString();
      this.lblFolio.Text = this._folioLiquidacion.AñoAtt.ToString() + " - " + this._folioLiquidacion.Folio.ToString();
      this.lblAutotanque.Text = this._folioLiquidacion.AutoTanque.ToString();
      this.lblTotalizador.Text = this._folioLiquidacion.DiferenciaTotalizador.ToString();
      if (this._tripulacion.CodigoOperadorTitular.ToString() != "0")
      {
        this.lblOperador.Text = this._tripulacion.CodigoOperadorTitular.ToString() + " - " + this._tripulacion.NombreOperadorTitular;
        this._operadorAsignado = true;
      }
      else
      {
        this.lblOperador.Text = "Operador no asignado. ¡Verifique!";
        this._operadorAsignado = false;
      }
      this._validarRemisionExistente = this._folioLiquidacion.ValidarRemisionExistente;
      this._validarRemisionLiquidada = this._folioLiquidacion.ValidarRemisionLiquidada;
      this.LimiteDeCredito();
    }

    public void LimiteDeCredito()
    {
      this._limiteCreditoOperador = this._tripulacion.LimiteCreditoOperador - this._tripulacion.SaldoOperador - this._saldoOperadorMovimiento;
      this.lblLimiteCreditoOperador.Text = this._tripulacion.LimiteCreditoOperador.ToString("C");
    }

    public void AltaPedido(int Cliente, short Celula, short AñoPed, int Pedido, string Nombre, string PedidoReferencia, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, byte TipoPedido, string Status, string FolioRemision, Decimal Descuento, int PedidoCRM)
    {
      this._folioLiquidacion.AltaPedido(Cliente, Celula, AñoPed, Pedido, Nombre, PedidoReferencia, Litros, Precio, Importe, FormaPago, TipoPedido, Status, 0, 0, FolioRemision, string.Empty, Descuento,PedidoCRM);
    }

    public void EdicionPedido(int SourceRow, int Cliente, string Nombre, string PedidoReferencia, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, string FolioRemision, Decimal Descuento)
    {
      this._folioLiquidacion.EdicionPedido(SourceRow, Cliente, Nombre, PedidoReferencia, Litros, Precio, Importe, FormaPago, FolioRemision, Descuento);
    }

    public void EdicionNuevoPedido(int SourceRow, int Cliente, string Nombre, string PedidoReferencia, short Celula, short AñoPed, int NumeroPedido, double Litros, Decimal Precio, Decimal Importe, byte FormaPago, byte TipoPedido, string Status, string FolioRemision, Decimal Descuento)
    {
      this._folioLiquidacion.EdicionPedidoNuevo(SourceRow, Cliente, Nombre, PedidoReferencia, Celula, AñoPed, NumeroPedido, Litros, Precio, Importe, FormaPago, TipoPedido, Status, FolioRemision, Descuento);
    }

    public void DesasignacionPedido(int SourceRow)
    {
      this._folioLiquidacion.DesasignacionPedido(SourceRow);
    }

    public void CargarListaPedidos()
    {
      this._folioLiquidacion.Usuario = _usuarioSistema;
      this._folioLiquidacion.ConsultaPedidos();
      if (this._folioLiquidacion.Status.Trim().ToUpper() == "CIERRE" && this.FormaLiquidacion == "AUTOMATICA")
        this._folioLiquidacion.DescargaSuministros(TipoOperacionDescarga.Rampac);
      this._folioLiquidacion.ConfigurarLista();
    }

    public void Click_CierreLiquidacion(object sender, ImageClickEventArgs e)
    {
      this.OnCierreLiquidacion((EventArgs) null);
    }

    public DataRow CurrentRow(int Row)
    {
      return this._folioLiquidacion.CurrentRow(Row);
    }

    public DataTable PedidosFiltrados(string Filtro)
    {
      return this._folioLiquidacion.SuministrosPorTipo(Filtro);
    }

    public bool LiquidacionIniciada(ref string Usuario, short AñoAtt, int Folio)
    {
      try
      {
        return this._folioLiquidacion.LiquidacionIniciada(ref Usuario, AñoAtt, Folio);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void FinalizarCapturaLiquidacion()
    {
      try
      {
        this._folioLiquidacion.TerminarCapturaLiquidacion();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void AltaInicioLiquidacionFolio()
    {
      if (!(this._folioLiquidacion.StatusLiquidacion != "INICIADA"))
        return;
      this._folioLiquidacion.TipoLiquidacion = this._formaLiquidacion;
      this._folioLiquidacion.AltaInicioLiquidacionFolio();
    }

    public string ValidaRemisiones()
    {
      return this._folioLiquidacion.CapturaRemisiones();
    }

    public DataTable SuministrosPorFormaDePago(string FormaPago)
    {
      return this._folioLiquidacion.SuministrosPorFormaPago(FormaPago);
    }

    public DataTable SuministrosPorTipoPedido(byte TipoPedido)
    {
      return this._folioLiquidacion.SuministrosPorTipoPedido(TipoPedido);
    }

    public DataTable ResumenLiquidacionFinal(string Usuario)
    {
      return this._folioLiquidacion.ResumenLiquidacionFinal(Usuario);
    }

    public void RecorridoListaPedidos(DataRow Source)
    {
      this._folioLiquidacion.RecorridoLista(Source);
    }

    public void ReordenarListaPedidos(string Column)
    {
      this._folioLiquidacion.ReordenarLista(Column);
    }

    public DataView Filter(string FilterBy)
    {
      return this._folioLiquidacion.FiltrarLista(FilterBy);
    }

    public int ApplyCustomFilter(string FilterBy, object FilterKey, bool ExactWord)
    {
      return this._folioLiquidacion.AplicarFiltroPersonalizado(FilterBy, FilterKey, ExactWord);
    }

    public int ApplyFilter(string FilterBy, object FilterKey)
    {
      return this._folioLiquidacion.AplicarFiltro(FilterBy, FilterKey);
    }
  }
}
