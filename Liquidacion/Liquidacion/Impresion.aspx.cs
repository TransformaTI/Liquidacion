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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.IO;
using SigametLiquidacion;

public partial class Impresion : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
	
        try
        {
			ReportDocument RepDocLiq = new ReportDocument();
            RepDocLiq = (ReportDocument)(Session["Reporte"]);
            System.IO.MemoryStream m_stream = new System.IO.MemoryStream();
            ExportOptions expOptions = RepDocLiq.ExportOptions;
            expOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            PageMargins mar = new PageMargins(1,1,1,1);
            RepDocLiq.FormatEngine.PrintOptions.ApplyPageMargins(mar);
            RepDocLiq.FormatEngine.PrintOptions.PaperSize = PaperSize.PaperLetter;
            ExportRequestContext req = new ExportRequestContext();
            req.ExportInfo = expOptions;
            // Get the export stream
            m_stream = (System.IO.MemoryStream)RepDocLiq.FormatEngine.ExportToStream(req);
            m_stream.Position = 0;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(m_stream.ToArray());
			m_stream.Dispose();
            m_stream.Close();
            RepDocLiq.Dispose();
			RepDocLiq.Close();
			RepDocLiq = null;
			 ((ReportDocument)(Session["Reporte"])).Close();
            ((ReportDocument)(Session["Reporte"])).Dispose();
            Session["Reporte"] = null; 
			Session["dsLiquidacion"] = null;
			Response.End();
        }
        catch (Exception ex)
        {

        }
    }
}
