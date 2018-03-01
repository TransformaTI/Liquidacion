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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Xml.Linq;
using System.Text;
using SigametLiquidacion;
using System.IO;

public partial class ReporteLiquidacion : System.Web.UI.Page
{

    ReportDocument RepDocLiq;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
			if(Page.IsPostBack)
			{
				//RepDocLiq.Close();
				//RepDocLiq.Dispose();
				Session["dsLiquidacion"] = null;
				Response.Redirect("SeleccionRutaLiquidacionDina.aspx");
				
			}
			else
			{
					loadReport();
			}   
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;   
        }
    }
    protected void imgImprimir_Click(object sender, ImageClickEventArgs e)
    {
        //System.IO.MemoryStream m_stream = new System.IO.MemoryStream();
        //ExportOptions expOptions = RepDocLiq.ExportOptions;
        //expOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //ExportRequestContext req = new ExportRequestContext();
        //req.ExportInfo = expOptions;
        //// Get the export stream
        //m_stream = (System.IO.MemoryStream)RepDocLiq.FormatEngine.ExportToStream(req);
        //m_stream.Position = 0;
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(m_stream.ToArray());
        //Response.End();
        //m_stream.Close(); 
       // ExportPdf();
        
        
    }
    protected void imgLiquidacion_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SeleccionRutaLiquidacionDina.aspx");
    }

    private void loadReport()
    {
        try
        {
            
            string strReporte = Request.PhysicalApplicationPath + "rptLiquidacion.rpt";
			//Response.Write(strReporte);
			RepDocLiq = new ReportDocument();
            if (File.Exists(strReporte))
            {
                try
                {
				
                    Parametros param = new Parametros(1, 1, 22);

                    string strServer = param.ValorParametro("Server").ToString();
                    string strDatabase = param.ValorParametro("Database").ToString();
                    string strUsuario = param.ValorParametro("Usuario").ToString();
                    string strPW = param.ValorParametro("Password").ToString();
                    string añoAtt;
                    string folio;
                    string strError;

                    //Parametros
                    añoAtt = Session["AñoAtt"].ToString();
                    folio = Session["Folio"].ToString();
                    ArrayList Par = new ArrayList();
                    //FIX THIS
                    Par.Add("@añoAtt=" + añoAtt);
                    Par.Add("@Folio=" + folio);
                    
                    Clase_Reporte Reporte = new Clase_Reporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
                    //Response.Write(añoAtt + " - " + folio + " - " + Reporte);
                    strError = "Clase";
                    RepDocLiq = Reporte.RepDoc;
                   // RepDoc.FileName;
                    RepDocLiq.PrintOptions.PrinterName = "";
                    
                    crviewRep.ReportSource = RepDocLiq;
                    crviewRep.Visible = true;
                    //crviewRep.Width = Unit.Percentage(98);
                    //crviewRep.Height = Unit.Percentage(98);
                    crviewRep.RefreshReport();
                    crviewRep.DisplayGroupTree = false;
                    crviewRep.HasPrintButton = false;
                    crviewRep.DisplayToolbar = false;
                    crviewRep.HasCrystalLogo = false;
                    crviewRep.HasDrillUpButton = false;
                    crviewRep.HasGotoPageButton = false;
                    crviewRep.HasPageNavigationButtons = false;
                    crviewRep.HasSearchButton = false;
                    crviewRep.HasZoomFactorList = false;
                    crviewRep.HasViewList = false;
                    crviewRep.HasToggleGroupTreeButton = false;
                    crviewRep.HasExportButton = false;
                    crviewRep.EnableViewState = true;
                    crviewRep.PrintMode = PrintMode.Pdf;
                    crviewRep.DataBind();
                    Session["Reporte"] = RepDocLiq;
					
            
                    if (Reporte.Hay_Error)
                    {
                        strError = Reporte.Error;
                        throw new Exception(strError);
                    }
                    
                                
                }
                catch (Exception ex)
                {
                    lblError.Text = "1. " + ex.Message + (char)13 +
					                "2. " + ex.InnerException + (char)13 +
									"3. " + ex.StackTrace;
                }
            }
            else
            {
                lblError.Text = "El archivo de Reporte no fue encontrado";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void crviewRep_Unload(object sender, EventArgs e)
    {
        //RepDocLiq.Close();
        //RepDocLiq.Dispose();
    }
   
    private void ExportPdf()
    {
        //Response.Redirect("Impresion.aspx");
        //System.IO.MemoryStream m_stream = new System.IO.MemoryStream();
        //ExportOptions expOptions = RepDocLiq.ExportOptions;
        //expOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //ExportRequestContext req = new ExportRequestContext();
        //req.ExportInfo = expOptions;
        //// Get the export stream
        //m_stream = (System.IO.MemoryStream)RepDocLiq.FormatEngine.ExportToStream(req);
        //m_stream.Position = 0;
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(m_stream.ToArray());
        //Response.End();
        //m_stream.Close(); 

     
    }
}
