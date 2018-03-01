// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.ResumenLiquidacion
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
  [ToolboxData("<{0}:ResumenLiquidacion runat=server></{0}:ResumenLiquidacion>")]
  public class ResumenLiquidacion : CompositeControl
  {
    private Label lblEncabezado = new Label();
    private Label lblTagConciliados = new Label();
    private Label lblConciliados = new Label();
    private Label lblTagPendientes = new Label();
    private Label lblPendientes = new Label();
    private Label lblTagInconsistentes = new Label();
    private Label lblInconsistentes = new Label();
    private Label lblEncabezadoPedidosPorTipo = new Label();
    private Label lblTagTelefonicos = new Label();
    private Label lblTelefonicos = new Label();
    private Label lblTagProgramados = new Label();
    private Label lblProgramados = new Label();
    private Label lblTagNotasBlancas = new Label();
    private Label lblNotasBlancas = new Label();
    private Label lblTagImporte = new Label();
    private Label lblTagLitros = new Label();
    private Label lblTagContado = new Label();
    private Label lblLitrosContado = new Label();
    private Label lblImporteContado = new Label();
    private Label lblTagCredito = new Label();
    private Label lblImporteCredito = new Label();
    private Label lblLitrosCredito = new Label();
    private Label lblTagOtros = new Label();
    private Label lblImporteOtros = new Label();
    private Label lblLitrosOtros = new Label();
    private Label lblTagTotal = new Label();
    private Label lblImporteTotal = new Label();
    private Label lblLitrosTotal = new Label();

    public int PedidosConciliados
    {
      set
      {
        this.lblConciliados.Text = value.ToString();
      }
    }

    public int PedidosPendientes
    {
      set
      {
        this.lblPendientes.Text = value.ToString();
      }
    }

    public int PedidosInconsistentes
    {
      set
      {
        this.lblInconsistentes.Text = value.ToString();
      }
    }

    public int PedidosTelefonicos
    {
      set
      {
        this.lblTelefonicos.Text = value.ToString();
      }
    }

    public int PedidosProgramados
    {
      set
      {
        this.lblProgramados.Text = value.ToString();
      }
    }

    public int PedidosNotaBlanca
    {
      set
      {
        this.lblNotasBlancas.Text = value.ToString();
      }
    }

    public double LitrosContado
    {
      set
      {
        this.lblLitrosContado.Text = value.ToString("0.00");
      }
    }

    public double LitrosCredito
    {
      set
      {
        this.lblLitrosCredito.Text = value.ToString("0.00");
      }
    }

    public double LitrosOtros
    {
      set
      {
        this.lblLitrosOtros.Text = value.ToString("0.00");
      }
    }

    public double LitrosTotal
    {
      set
      {
        this.lblLitrosTotal.Text = value.ToString("0.00");
      }
    }

    public Decimal ImporteContado
    {
      set
      {
        this.lblImporteContado.Text = value.ToString("$ 0.00");
      }
    }

    public Decimal ImporteCredito
    {
      set
      {
        this.lblImporteCredito.Text = value.ToString("$ 0.00");
      }
    }

    public Decimal ImporteOtros
    {
      set
      {
        this.lblImporteOtros.Text = value.ToString("$ 0.00");
      }
    }

    public Decimal ImporteTotal
    {
      set
      {
        this.lblImporteTotal.Text = value.ToString("$ 0.00");
      }
    }

    protected override void CreateChildControls()
    {
      this.Controls.Add((Control) new LiteralControl("<table align='right'>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td style='vertical-align: top'>"));
      this.Controls.Add((Control) new LiteralControl("<table cellspacing='0' border='2'>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td colspan='2' class='HeaderMainStyle'>"));
      this.lblEncabezado.Text = "Pedidos por status";
      this.Controls.Add((Control) this.lblEncabezado);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagConciliados.Text = "Conciliados:";
      this.Controls.Add((Control) this.lblTagConciliados);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblConciliados);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagPendientes.Text = "Pendientes:";
      this.Controls.Add((Control) this.lblTagPendientes);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblPendientes);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagInconsistentes.Text = "Inconsistencias:";
      this.Controls.Add((Control) this.lblTagInconsistentes);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblInconsistentes);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("</table>"));
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td style='vertical-align: top'>"));
      this.Controls.Add((Control) new LiteralControl("<table cellspacing='0' border='2'>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td colspan='2' class='HeaderMainStyle'>"));
      this.lblEncabezadoPedidosPorTipo.Text = "Pedidos por tipo";
      this.Controls.Add((Control) this.lblEncabezadoPedidosPorTipo);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagTelefonicos.Text = "Telefónicos:";
      this.Controls.Add((Control) this.lblTagTelefonicos);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblTelefonicos);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagProgramados.Text = "Programados:";
      this.Controls.Add((Control) this.lblTagProgramados);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblProgramados);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagNotasBlancas.Text = "Notas blancas:";
      this.Controls.Add((Control) this.lblTagNotasBlancas);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblNotasBlancas);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("</table>"));
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td style='vertical-align: top'>"));
      this.Controls.Add((Control) new LiteralControl("<table cellspacing='0' border='2'>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='HeaderMainStyle' style='width:100px' >"));
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='HeaderMainStyle' style='width:100px' >"));
      this.lblTagLitros.Text = "Litros";
      this.Controls.Add((Control) this.lblTagLitros);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='HeaderMainStyle' style='width:100px' >"));
      this.lblTagImporte.Text = "Importe";
      this.Controls.Add((Control) this.lblTagImporte);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagContado.Text = "Contado:";
      this.Controls.Add((Control) this.lblTagContado);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblLitrosContado);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblImporteContado);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagCredito.Text = "Crédito:";
      this.Controls.Add((Control) this.lblTagCredito);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblLitrosCredito);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblImporteCredito);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagOtros.Text = "Otros:";
      this.Controls.Add((Control) this.lblTagOtros);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblLitrosOtros);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblImporteOtros);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      this.Controls.Add((Control) new LiteralControl("<td class='leftTableLabel'>"));
      this.lblTagTotal.Text = "Total:";
      this.Controls.Add((Control) this.lblTagTotal);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblLitrosTotal);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("<td class='contentTableLabel'>"));
      this.Controls.Add((Control) this.lblImporteTotal);
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("</table>"));
      this.Controls.Add((Control) new LiteralControl("</td>"));
      this.Controls.Add((Control) new LiteralControl("</tr>"));
      this.Controls.Add((Control) new LiteralControl("</table>"));
    }
  }
}
