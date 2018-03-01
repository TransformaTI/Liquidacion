// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.ElementoListaPedidos
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using SigametLiquidacion;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
    public class ElementoListaPedidos : CompositeControl
    {
        private DataSet dsConfiguracion = new DataSet();
        private int _rowID;
        private DataTable _dtConfig;
        private DataRow _drSource;
        private ImageButton btnShowContent;
        private Catalogos _catalogos;
        private Parametros _parametros;
        private string _imageURL;
        private string _configFile;
        
        public int RowID
        {
            get
            {
                return this._rowID;
            }
        }
        
        public DataTable DTConfig
        {
            get
            {
                return this._dtConfig;
            }
            set
            {
                this._dtConfig = value;
            }
        }
        
        public string ImageURL
        {
            get
            {
                return this._imageURL;
            }
            set
            {
                this._imageURL = value;
            }
        }
        
        public DataRow Source
        {
            get
            {
                return this._drSource;
            }
            set
            {
                this._drSource = value;
                this.DataBind();
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
        
        public event EventHandler Editar;
        
        protected virtual void OnClick(EventArgs e)
        {
            if (this.Editar == null)
            {
                return;
            }
            this.Editar((object) this, e);
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
        
        private void LoadSettings()
        {
            if (this._dtConfig != null)
            {
                return;
            }
            int num = (int) this.dsConfiguracion.ReadXml(this.MapPathSecure(this._configFile));
            this._dtConfig = this.dsConfiguracion.Tables[0];
        }

        private void WriteValueCell(string Value, string CSSClass)
        {
            Label label = new Label();
            label.Text = Value;
            this.Controls.Add((Control) new LiteralControl("<td  class='" + CSSClass + "'>"));
            this.Controls.Add((Control) label);
            this.Controls.Add((Control) new LiteralControl("</td>"));
        }
        
        private void OverSizedValueCell(string Value, string CSSClass, string DIVCSSClass)
        {
            Label label = new Label();
            label.Text = Value;
            this.Controls.Add((Control) new LiteralControl("<td  class='" + CSSClass + "'>"));
            this.Controls.Add((Control) new LiteralControl("<div class='" + DIVCSSClass + "'>"));
            this.Controls.Add((Control) label);
            this.Controls.Add((Control) new LiteralControl("</div>"));
            this.Controls.Add((Control) new LiteralControl("</td>"));
        }
        
        private void FormatValueCell(object Source, DataRow ConfigValues)
        {
            string str1 = string.Empty;
            string str2;
            switch (Convert.ToString(ConfigValues["Type"]).Trim().ToUpper())
            {
                case "FORMATTEDTEXT":
                    str2 = Convert.ToDouble(Source).ToString(Convert.ToString(ConfigValues["Format"]));
                    break;
                case "FORMATTEDDATE":
                    str2 = Convert.ToDateTime(Source).ToString(Convert.ToString(ConfigValues["Format"]));
                    break;
                default:
                    str2 = Convert.ToString(Source);
                    break;
            }
            string str3 = HttpUtility.HtmlEncode(str2.Trim());
            if (Convert.ToString(ConfigValues["DisplayStyle"]).Trim().ToUpper() == "OVERSIZEDFIELD")
            {
                this.OverSizedValueCell(str3, Convert.ToString(ConfigValues["ContentCssClass"]), Convert.ToString(ConfigValues["OversizedCssClass"]));
            }
            else
            {
                this.WriteValueCell(str3, Convert.ToString(ConfigValues["ContentCssClass"]));
            }
        }
        
        public override void DataBind()
        {
            this.LoadSettings();
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            if (this._dtConfig != null && this._dtConfig.Rows.Count > 0)
            {
                foreach (DataRow ConfigValues in (InternalDataCollectionBase) this._dtConfig.Rows)
                {
                    switch (Convert.ToString(ConfigValues["Type"]).Trim().ToUpper())
                    {
                        case "CONTROLBOX":
                            this.Controls.Add((Control) new LiteralControl("<td>"));
                            this.btnShowContent = new ImageButton();
                            this.btnShowContent.ImageUrl = this._imageURL;
                            this.Controls.Add((Control) this.btnShowContent);
                            this.Controls.Add((Control) new LiteralControl("</td>"));
                            continue;
                        case "ALTERNATINGICONBOX1":
                            this.Controls.Add((Control) new LiteralControl("<td>"));
                            Image image1 = new Image();
                            image1.Width = (Unit) 16;
                            image1.Height = (Unit) 16;
                        switch (Convert.ToString(this._drSource["STATUS"]))
                        {
                            case "CONCILIADO":
                                image1.ImageUrl = "~/Images/procesOK.bmp";
                                image1.AlternateText = "Conciliación correcta";
                                break;
                            case "PENDIENTE":
                                image1.ImageUrl = "~/Images/procesPending.bmp";
                                image1.AlternateText = "Conciliación pendiente";
                                break;
                            case "ERROR":
                                image1.ImageUrl = "~/Images/procesWrong.bmp";
                                image1.AlternateText = "Inconsistencia de datos:" + (object) '\r' + Convert.ToString(this._drSource["ObservacionesConciliacion"]);
                                break;
                        }
                        this.Controls.Add((Control) image1);
                        this.Controls.Add((Control) new LiteralControl("</td>"));
                        continue;
                        case "ALTERNATINGICONBOX2":
                            this.Controls.Add((Control) new LiteralControl("<td>"));
                            Image image2 = new Image();
                            image2.Width = (Unit) 16;
                            image2.Height = (Unit) 16;
                            if (Convert.ToString(this._drSource["FormaPagoDescripcion"]) == "CREDITO")
                            {
                                image2.ImageUrl = "~/Images/credit_card.gif";
                                image2.AlternateText = "Forma de pago: Crédito";
                            }
                            else
                            {
                                image2.ImageUrl = "~/Images/cash.gif";
                                image2.AlternateText = "Forma de pago: Contado";
                            }
                            this.Controls.Add((Control) image2);
                            this.Controls.Add((Control) new LiteralControl("</td>"));
                            continue;
                        default:
                            this.FormatValueCell(this._drSource[Convert.ToString(ConfigValues["DataField"])], ConfigValues);
                            continue;
                    }
                }
            }
            else
            {
                foreach (DataColumn index in (InternalDataCollectionBase)this._drSource.Table.Columns)
                {
                    this.WriteValueCell(this._drSource[index].ToString(), string.Empty);
                }
            }
            try
            {
                this._rowID = Convert.ToInt32(this._drSource["ID"]);
            }
            catch
            {
            }
            this.Controls.Add((Control) new LiteralControl("</tr>"));
            this.btnShowContent.Click += new ImageClickEventHandler(this.ClickImagen);
        }
        
        public void ClickImagen(object sender, ImageClickEventArgs e)
        {
            this.OnClick((EventArgs) null);
        }
        
        public void Remark()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType().Name == "Label")
                {
                    ((WebControl)control).Font.Bold = true;
                }
            }
            this.btnShowContent.ImageUrl = "~/Images/editing.png";
        }
        
        public void Unmark()
        {
            this.btnShowContent.ImageUrl = this._imageURL;
            foreach (Control control in this.Controls)
            {
                if (control.GetType().Name == "Label")
                {
                    ((WebControl)control).Font.Bold = false;
                }
            }
        }
    }
}
