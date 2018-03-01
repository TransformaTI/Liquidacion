// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.EncabezadoListaPedidos
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
  public class EncabezadoListaPedidos : CompositeControl
  {
    private DataSet dsConfiguracion = new DataSet();
    private string _configFile;
    private DataTable _dtConfiguracion;

    public DataTable DTConfiguracion
    {
      get
      {
        return this._dtConfiguracion;
      }
    }

    public string ConfigFile
    {
      get
      {
        return this._configFile;
      }
      set
      {
        this._configFile = value;
      }
    }

    protected override object SaveViewState()
    {
      this.EnsureChildControls();
      object[] objArray = new object[1];
      object obj = base.SaveViewState();
      objArray[0] = obj;
      return (object) objArray;
    }

    protected override void LoadViewState(object savedState)
    {
      base.LoadViewState(((object[]) savedState)[0]);
      this.EnsureChildControls();
    }

    protected override void Render(HtmlTextWriter writer)
    {
      base.Render(writer);
    }

    private void LoadSettings()
    {
      if (this._dtConfiguracion != null)
        return;
      int num = (int) this.dsConfiguracion.ReadXml(this.MapPathSecure(this._configFile));
      this._dtConfiguracion = this.dsConfiguracion.Tables[0];
    }

    private void WriteHeader()
    {
      if (this._dtConfiguracion == null)
        return;
      this.Controls.Add((Control) new LiteralControl("<tr>"));
      foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtConfiguracion.Rows)
      {
        this.Controls.Add((Control) new LiteralControl("<td class='" + Convert.ToString(dataRow["HeaderCssClass"]) + " '>"));
        this.Controls.Add((Control) new LiteralControl(Convert.ToString(dataRow["Caption"])));
        this.Controls.Add((Control) new LiteralControl("</td>"));
      }
      this.Controls.Add((Control) new LiteralControl("</tr>"));
    }

    public void AlternativeWriteHeader(Panel Container)
    {
      if (this._dtConfiguracion == null)
        this.LoadSettings();
      Container.Controls.Add((Control) new LiteralControl("<tr>"));
      foreach (DataRow dataRow in (InternalDataCollectionBase) this.DTConfiguracion.Rows)
      {
        Container.Controls.Add((Control) new LiteralControl("<td class='" + Convert.ToString(dataRow["HeaderCssClass"]) + "' style='height:0px;background-image:none'>"));
        Container.Controls.Add((Control) new LiteralControl("</td>"));
      }
      Container.Controls.Add((Control) new LiteralControl("</tr>"));
    }

    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      this.LoadSettings();
      this.Controls.Add((Control) new LiteralControl("<table cellspacing='0'>"));
      this.WriteHeader();
      this.Controls.Add((Control) new LiteralControl("</table>"));
    }
  }
}
