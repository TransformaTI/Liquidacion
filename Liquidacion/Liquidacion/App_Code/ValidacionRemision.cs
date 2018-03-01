using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

/// <summary>
/// Summary description for ValidacionRemision
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ValidacionRemision : System.Web.Services.WebService
{

    public ValidacionRemision()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod(EnableSession = true)]
    [WebMethod]
    public bool RemisionExistente(string Remision)
    {
        bool _retValue = false;

        DocumentosBSR.SerieDocumento.SeparaSerie(Remision);
        SigametLiquidacion.ControlDeRemisiones _remisiones = new SigametLiquidacion.ControlDeRemisiones();

        try
        {
            _retValue = _remisiones.RemisionExistente(DocumentosBSR.SerieDocumento.Serie,
                DocumentosBSR.SerieDocumento.FolioNota.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return _retValue;
    }
}

