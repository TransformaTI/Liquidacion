using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SigametLiquidacion;
using SigametLiquidacion.WebControls;




public partial class SeleccionRutaLiquidacionDina : System.Web.UI.Page
{

    DataTable dtCelulas = new DataTable();
    SeleccionRuta sr = new SeleccionRuta();
    Int16 celSelex;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Convert.ToBoolean(Session["Iniciada"]))
        {
            Response.Redirect("Login.aspx");
        }
		
        if (Page.IsPostBack)
        {
            CargaSeleccionRuta();
            if (ViewState["Celula"] != null)
            {
                CargaRutasCelula((Int16)(ViewState["Celula"]));
            }            
        }
        else
        {
            CargaSeleccionRuta();
        }
        if (Session["FechaAsignacion"] != null)
        {
            txtFAsignacion.Text = Session["FechaAsignacion"].ToString();
        }
		else
        {
            txtFAsignacion.Text = DateTime.Now.ToShortDateString();
        }		
    }

    //Genera ImageButton y LinkButton de cada una de las Celulas que tienen Rutas pendientes, dependiendo la fecha capturada y el usuario loggeado
    private void CargaSeleccionRuta()
    {
        TableRow tr;
        TableCell tc;

        try
        {
            if (txtFAsignacion.Text.Trim() == string.Empty)
            {
                return;
            }
            
            dtCelulas = sr.ListaCelulas(Convert.ToDateTime(txtFAsignacion.Text), Convert.ToString(Session["Usuario"]));
            Session["FechaAsignacion"] = txtFAsignacion.Text;
            
            foreach (DataRow dr in dtCelulas.Rows)
            {
                if (dr[0].ToString().Trim().ToUpper() == "CELULA")
                {
                    string id;
                    short celula;
                    string desc;
                    string desc2;

                    id = dr["TipoAsignacion"].ToString().TrimEnd() + dr["GrupoAsignado"].ToString().TrimEnd();
                    celula = Convert.ToInt16(dr["Celula"]);
                    desc = dr["TipoAsignacion"].ToString().TrimEnd() + " " + dr["GrupoAsignado"].ToString().TrimEnd();
                    desc2 = dr["Descripcion"].ToString().TrimEnd();
                    //Inserta ImageButton
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Controls.Add(AddControl(id, celula, 0, 0, 0, 0, desc, desc2, "", true, string.Empty, string.Empty,0));
                    tr.Cells.Add(tc);
                    tbCelulas.Rows.Add(tr);

                    //Inserta LinkButton
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tc.Controls.Add(AddControl(id, celula, 0, 0, 0, 0, desc, desc2, "", false, string.Empty, string.Empty,0));
                    tr.Cells.Add(tc);
                    tbCelulas.Rows.Add(tr);
                }
            }
        }
        catch (Exception ex)
        {
             throw ex;
        }
    }
    //Genera ImageButton y LinkButton de cada una de las Rutas pendientes pertenecientes a la Célula seleccionada
    private void CargaRutasCelula(int Celula)
    {
        DataTable dtRutas = new DataTable();
        Table tbPanel = new Table(); 
        Table tbInner;
        TableRow trInner;
        TableCell tcInner;
        TableRow tr;
        TableCell tc;
       
        try
        {
            if (txtFAsignacion.Text.Trim() == string.Empty)
            {
                return;
            }

            dtRutas = sr.ListaRutas(Convert.ToDateTime(txtFAsignacion.Text), Celula);
            tbRutas.Rows.Clear();
            tr = new TableRow();
            //tbRutas = null;
            foreach (DataRow dr in dtRutas.Rows)
            {
                string id;
                short celula;
                short ruta;
                short añoAtt;
                int folio;
                string desc;
                string desc2;
                string status;
                short autotanque=0;

                //Para determinar si es necesario descargar registros
                string formaLiquidacion;
                string statusLiquidacion;

                id = dr["AñoAtt"].ToString().TrimEnd() + dr["Folio"].ToString().Replace(" ", "");

                celula = Convert.ToInt16(dr["Celula"]);
                ruta = Convert.ToInt16(dr["Ruta"]);
                añoAtt = Convert.ToInt16(dr["AñoAtt"]);
                folio = Convert.ToInt32(dr["Folio"]);
                desc = dr["NombreRuta"].ToString().TrimEnd() + " Folio " + dr["AñoAtt"].ToString().TrimEnd() + " - " + dr["Folio"].ToString().Replace(" ", "");
                status = dr["statuslogistica"].ToString().TrimEnd();

                formaLiquidacion = dr["TipoLiquidacion"].ToString().Trim().ToUpper();

                statusLiquidacion = dr["StatusLiquidacion"].ToString().Trim().ToUpper();
                autotanque= Convert.ToInt16(dr["AutoTanque"]);

                tbInner = new Table();
                tc = new TableCell();
                trInner = new TableRow();
                tcInner = new TableCell();

                

                tcInner.Controls.Add(AddControl(id, celula, ruta, añoAtt, folio, 1, desc, string.Empty, status, true, formaLiquidacion,
                    statusLiquidacion, autotanque));

                tcInner.HorizontalAlign = HorizontalAlign.Center;
                trInner.Cells.Add(tcInner);
                tbInner.Rows.Add(trInner);

                trInner = new TableRow();
                tcInner = new TableCell();

                tcInner.Controls.Add(AddControl(id, celula, ruta, añoAtt, folio, 1, desc, string.Empty, status, false, formaLiquidacion,
                    statusLiquidacion, autotanque));


                tcInner.HorizontalAlign = HorizontalAlign.Center;
                trInner.Cells.Add(tcInner);
                tbInner.Rows.Add(trInner);

                tc.Controls.Add(tbInner);
                 
                tr.Cells.Add(tc);
                tbPanel.Rows.Add(tr);

                if (tr.Cells.Count == 5)
                {
                    tr = new TableRow();
                }
           }
            pnlFolios.Controls.Add(tbPanel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Genera Controles 
    private Control AddControl(string nomControl, short celula, short ruta, short añoAtt, int folio, short tipo,
        string descripcion, string descripcion2, string status, bool tipoControl, string FormaLiquidacion,
        string StatusLiquidacion, int autotanque)
    {
        LiquidacionImageButton ibl ;
        LiquidacionLinkButton lbl;
       
        try
        {
            //ImageButton
            if (tipoControl)
            {
                ibl = new LiquidacionImageButton();
                ibl.ID = "ibt" + nomControl;

                ibl.AñoAtt = añoAtt;
                ibl.Folio = folio;
                ibl.Tipo = tipo;
                ibl.Autotanque = autotanque;

                //Para determinar si se deben descargar registros de la tarjeta rampac
                ibl.FormaLiquidacion = FormaLiquidacion;

                ibl.StatusLiquidacion = StatusLiquidacion;

               // ibl.Autotanque = autotanque;


                switch (status)
                {
                    //Los ImageButtons de Celulas no llevan la descripcion
                    case "":
                        ibl.ImageUrl = "~/Images/CelulaBase.png";
                        ibl.AlternateText = descripcion;
                        ibl.Celula = celula;
                        break;
                    case "INICIO":
                        ibl.ImageUrl = "~/Images/ATRutaIniciada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    case "CIERRE":
                        ibl.ImageUrl = "~/Images/ATRutaCerrada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    case "LIQUIDADO":
                        ibl.ImageUrl = "~/Images/ATRutaLiquidada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    case "LIQCAJA":
                        ibl.ImageUrl = "~/Images/ATRutaValidada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    case "LIQINICIADA":
                        ibl.ImageUrl = "~/Images/ATLiquidacionIniciada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    case "NOASIGNADO":
                        ibl.ImageUrl = "~/Images/ATRutaNoAsignada.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                    default:
                        ibl.ImageUrl = "~/Images/ATRutaOtro.png";
                        ibl.AlternateText = descripcion + " Status : " + status;
                        ibl.Ruta = ruta;
                        break;
                   
                }
                ibl.Height = 93;
                ibl.Width = 150;

                ibl.Click += new ImageClickEventHandler(this.LinkorButton_Click);
                return ibl;
            }
            //LinkButton
            else
            {
                lbl = new LiquidacionLinkButton();
                lbl.ID = "lbl" + nomControl;
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Font.Bold = true;
                lbl.Font.Underline = false;

                lbl.Celula = celula;
                lbl.Ruta = ruta;
                lbl.AñoAtt = añoAtt;
                lbl.Folio = folio;
                lbl.Tipo = tipo;
                lbl.Autotanque = autotanque;
                




                lbl.Text = descripcion;

                //Para determinar si se deben descargar registros de la tarjeta rampac
                lbl.FormaLiquidacion = FormaLiquidacion;

                lbl.StatusLiquidacion = StatusLiquidacion;

                lbl.Click += new EventHandler(this.LinkorButton_Click);
                return lbl;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Evento para los Image y Link Buttons
    protected void LinkorButton_Click(object sender, EventArgs e)   
    {
        LiquidacionLinkButton bt;
        LiquidacionImageButton ibt;

        try
        {
            //Toma el tipo de Control que llamó el evento
            if (sender.GetType().Name == "LiquidacionLinkButton")
            {
                bt = (LiquidacionLinkButton)(sender);
                //Si es un control de Célula, generará los controles de cada Ruta
                if (bt.Tipo == 0)
                {
                    celSelex = bt.Celula;

                    pnlFolios.Controls.Clear();

                    CargaRutasCelula(celSelex);
                    ViewState["Celula"] = celSelex;
                }
                //Si es de un control de Ruta, llamará a la pantalla de Liquidación
                else if (bt.Tipo == 1)
                {
                    Session["AñoAtt"] = bt.AñoAtt;
                    Session["Folio"] = bt.Folio;
                    
                    if (bt.FormaLiquidacion == "AUTOMATICA")
                    {
                        updPnlSeleccionFormaLiquidacion.Update();
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        Response.Redirect("Liquidacion.aspx?FormaLiquidacion=MANUAL");
                    }
                }
            }
            else
            {
                ibt = (LiquidacionImageButton)(sender);
                //Si es un control de Célula, generará los controles de cada Ruta
                if (ibt.Tipo == 0)
                {
                    //celSelex = Convert.ToInt16(ibt.AlternateText.ToString().Substring(6));
                    celSelex = ibt.Celula;

                    pnlFolios.Controls.Clear();


                    CargaRutasCelula(celSelex);
                    ViewState["Celula"] = celSelex;

                }
                //Si es de un control de Ruta, llamará a la pantalla de Liquidación
                else if (ibt.Tipo == 1)
                {
                    Session["AñoAtt"] = ibt.AñoAtt;
                    Session["Folio"] = ibt.Folio;
                    Session["Ruta"] = ibt.Ruta; // mcc 2018 05 16
                    Session["Autotanque"] = ibt.Autotanque;// mcc 2018 05 16




                    if (ibt.FormaLiquidacion == "AUTOMATICA")
                    {
                        updPnlSeleccionFormaLiquidacion.Update();
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        Response.Redirect("Liquidacion.aspx?FormaLiquidacion=MANUAL");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Liquidacion.aspx");
    }

    protected void btnCargaRutas_Click(object sender, ImageClickEventArgs e)
    {

    }
}