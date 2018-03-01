using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using SigametLiquidacion;

/// <summary>
/// Summary description for DatosCliente
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class DatosCliente : System.Web.Services.WebService
{
    string nombreCliente;
    DatosRegistroPago _datos = new DatosRegistroPago();

    public DatosCliente()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod(EnableSession = true)]
    [WebMethod]
    public String GetCliente(int numCliente)
    {
        try
        {
            _datos.CargaCliente(numCliente);
            if (_datos.Cliente.Rows.Count > 0)
                nombreCliente = _datos.Cliente.Rows[0][1].ToString();
            else
                nombreCliente = "";
            
            return nombreCliente;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}


