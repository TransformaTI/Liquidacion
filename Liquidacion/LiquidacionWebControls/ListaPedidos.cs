// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.ListaPedidos
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
    public class ListaPedidos : CompositeControl
    {
        private Panel pnlContainer = new Panel();
        private DataSet dsConfiguracion = new DataSet();
        public int ClickedRow;
        private Panel mainContainerPanel;
        private ElementoListaPedidos _currentRow;
        private DataTable _dataSource;
        private DataTable _dtConfiguracion;
        private string _imageButtonURL;
        private string _configFile;
        private TipoOperacionLista _tipoOperacion;
        private bool _remark;
        
        public string ContenedorUniqueID
        {
            get
            {
                return this.mainContainerPanel.UniqueID;
            }
        }
        
        public ElementoListaPedidos CurrentRow
        {
            get
            {
                return this._currentRow;
            }
        }
        
        public DataTable DataSource
        {
            set
            {
                this._dataSource = value;
                this.DataBind();
            }
        }
        
        public string ImageButtonURL
        {
            get
            {
                return this._imageButtonURL;
            }
            set
            {
                this._imageButtonURL = value;
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
        
        public TipoOperacionLista TipoOperacion
        {
            get
            {
                return this._tipoOperacion;
            }
            set
            {
                this._tipoOperacion = value;
            }
        }
        
        public bool Remark
        {
            set
            {
                this._remark = value;
            }
        }
        
        public event EventHandler CargarDatos;
        public event EventHandler Error;
        public event EventHandler Recargar;
        public event CommandEventHandler SortList;
        public event CommandEventHandler Filtrar;
        public event EventHandler ClickBtnClickMe;
        public event EventHandler EditarElemento;
        
        protected virtual void OnClick(EventArgs e)
        {
            if (this.CargarDatos == null)
            {
                return;
            }
            this.CargarDatos((object)this, e);
        }
        
        protected override object SaveViewState()
        {
            this.EnsureChildControls();
            object[] objArray = new object[2];
            object obj = base.SaveViewState();
            objArray[0] = obj;
            objArray[1] = (object) this._dataSource;
            return (object) objArray;
        }
        
        protected override void LoadViewState(object savedState)
        {
            object[] objArray = (object[]) savedState;
            base.LoadViewState(objArray[0]);
            this._dataSource = (DataTable) objArray[1];
            this.EnsureChildControls();
        }
        
        protected virtual void OnClickMe(EventArgs e)
        {
            if (this.ClickBtnClickMe == null)
            {
                return;
            }
            this.ClickBtnClickMe((object) this, e);
        }
        
        protected virtual void OnClickEditarElemento(EventArgs e)
        {
            if (this.EditarElemento == null)
            {
                return;
            }
            this.EditarElemento((object)this, e);
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
        
        private void LoadSettings()
        {
            if (this._dtConfiguracion != null)
            {
                return;
            }
            int num = (int) this.dsConfiguracion.ReadXml(this.MapPathSecure(this._configFile));
            this._dtConfiguracion = this.dsConfiguracion.Tables[0];
        }
        
        private void WriteHeader()
        {
            if (this._dtConfiguracion == null)
            {
                return;
            }
            this.Controls.Add((Control) new LiteralControl("<tr>"));
            foreach (DataRow row in (InternalDataCollectionBase) this._dtConfiguracion.Rows)
            {
                this.Controls.Add((Control) new LiteralControl("<td class='" + Convert.ToString(row["HeaderCssClass"]) + " '>"));
                if (this._dtConfiguracion.Rows.IndexOf(row) == 0)
                {
                    ImageButton imageButton = new ImageButton();
                    imageButton.ImageUrl = "~/images/update.png";
                    imageButton.Click += new ImageClickEventHandler(this.btnUpdate_Click);
                    this.Controls.Add((Control) imageButton);
                }
                LinkButton linkButton = new LinkButton();
                linkButton.CssClass = Convert.ToString(row["HeaderCssClass"]);
                linkButton.CommandName = "SORT";
                linkButton.CommandArgument = Convert.ToString(row["DataField"]);
                linkButton.Command += new CommandEventHandler(this.lnkHeader_Command);
                linkButton.Text = Convert.ToString(row["Caption"]);
                this.Controls.Add((Control) linkButton);
                if (Convert.ToString(row["DataField"]).Trim().Length > 0)
                {
                    ImageButton imageButton = new ImageButton();
                    imageButton.CommandName = Convert.ToString(row["Caption"]);
                    imageButton.CommandArgument = Convert.ToString(row["DataField"]);
                    imageButton.ImageUrl = "~/images/filter.png";
                    imageButton.Command += new CommandEventHandler(this.btnFilter_Command);
                    this.Controls.Add((Control) imageButton);
                }
                this.Controls.Add((Control) new LiteralControl("</td>"));
            }
            this.Controls.Add((Control) new LiteralControl("</tr>"));
        }
        
        private void btnFilter_Command(object sender, CommandEventArgs e)
        {
            this.Filtrar(sender, e);
        }
        
        protected void lnkHeader_Command(object sender, CommandEventArgs e)
        {
            this.SortList(sender, e);
        }
        
        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            this.Error(sender, (EventArgs) e);
        }
        
        private void AlternativeWriteHeader(Panel Container)
        {
            if (this._dtConfiguracion == null)
            {
                return;
            }
            Container.Controls.Add((Control) new LiteralControl("<tr>"));
            foreach (DataRow dataRow in (InternalDataCollectionBase) this._dtConfiguracion.Rows)
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
            if (this._dataSource != null && this._dataSource.Rows.Count > 0)
            {
                this.Controls.Add((Control) new LiteralControl("<tr>"));
                this.Controls.Add((Control) new LiteralControl("<td colspan=" + (object) this._dtConfiguracion.Columns.Count + ">"));
                this.mainContainerPanel = new Panel();
                this.mainContainerPanel.Height = (Unit) 400;
                this.mainContainerPanel.ScrollBars = ScrollBars.Vertical;
                this.mainContainerPanel.ID = "ContainerPedidos";
                this.Controls.Add((Control) this.mainContainerPanel);
                this.mainContainerPanel.Controls.Add((Control) new LiteralControl("<table>"));
                this.AlternativeWriteHeader(this.mainContainerPanel);
                foreach (DataRow dataRow in (InternalDataCollectionBase) this._dataSource.Rows)
                {
                    switch (this._tipoOperacion)
                    {
                        case TipoOperacionLista.Conciliacion:
                        case TipoOperacionLista.Boletin:
                        ElementoListaPedidos elementoListaPedidos = new ElementoListaPedidos();
                        elementoListaPedidos.DTConfig = this._dtConfiguracion;
                        elementoListaPedidos.ImageURL = this._imageButtonURL;
                        elementoListaPedidos.Source = dataRow;
                        elementoListaPedidos.Editar += new EventHandler(this.EditarElemento_Clic);
                        if (this._remark)
                        {
                            elementoListaPedidos.Remark();
                            this._remark = false;
                        }
                        this.mainContainerPanel.Controls.Add((Control) elementoListaPedidos);
                        continue;
                        default:
                            continue;
                    }
                }
                this.mainContainerPanel.Controls.Add((Control) new LiteralControl("</table>"));
            }
            this.Controls.Add((Control) new LiteralControl("</table>"));
        }
        
        public override void DataBind()
        {
            this.CargarDatos((object) this, (EventArgs) null);
            this.CreateChildControls();
        }

        public void EditarElemento_Clic(object sender, EventArgs e)
        {
            this.Restablecer();
            this._currentRow = (ElementoListaPedidos) sender;
            this.ClickedRow = this._currentRow.RowID;
            this.OnClickEditarElemento(e);
        }
        
        public void Restablecer()
        {
            try
            {
                foreach (Control control in this.mainContainerPanel.Controls)
                {
                    if (control.GetType().Name == "ElementoListaPedidos")
                    {
                        ((ElementoListaPedidos)control).Unmark();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error((object) this, (EventArgs) null);
            }
        }
        
        public int UltimaFila()
        {
            int num = 0;
            if (this._dataSource.Rows.Count > 0)
            {
                num = Convert.ToInt32(this._dataSource.Compute("MAX(ID)", (string)null));
            }
            return num;
        }
        
        public bool SiguientePedido(int Index)
        {
            if (Index <= 0)
            {
                return false;
            }
            ++Index;
            if (Index <= this.UltimaFila())
            {
                foreach (Control control in this.mainContainerPanel.Controls)
                {
                    if (control.GetType().Name == "ElementoListaPedidos" && ((ElementoListaPedidos) control).RowID == Index)
                    {
                        ((ElementoListaPedidos) control).ClickImagen((object) null, (ImageClickEventArgs) null);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
