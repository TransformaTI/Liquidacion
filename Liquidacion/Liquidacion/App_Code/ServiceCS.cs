using System;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using SigametLiquidacion;

///<summary>
/// Summary description for ServiceCS
///</summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class ServiceCS : System.Web.Services.WebService
{
    [WebMethod(enableSession:true)]
    public CascadingDropDownNameValue[] GetBancoAfiliaciones(
       string knownCategoryValues)
    {

        string claveBanco;
        string nombreBanco="";

        try
        {
            claveBanco = Convert.ToString(Session["BancoTarjetaSeleccionado"]);
            nombreBanco = Convert.ToString(Session["NombreBancoTarjetaSeleccionado"]);

        }
        catch (Exception ex)
        {
            claveBanco = "";
        }
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>(); 

        if (claveBanco != "")
        {
            values.Add(new CascadingDropDownNameValue(nombreBanco, claveBanco));
            claveBanco = "";
        }
        else
        {
            RegistroPago rp2 = new RegistroPago();

            DataTable tbl = rp2.ListaBancosAfiliacion();

            
            values.Add(new CascadingDropDownNameValue(
                  "- Seleccione -", "0"));

            foreach (DataRow dr in tbl.Rows)
            {
                string Nombre = (string)dr["Nombre"];
                int Banco = Convert.ToInt16(dr["Banco"]);
                values.Add(new CascadingDropDownNameValue(
                  Nombre, Banco.ToString()));
            }

        }
        Session["BancoTarjetaSeleccionado"] = "";
        Session["NombreBancoTarjetaSeleccionado"] ="";

        return values.ToArray();
    }

    [WebMethod(enableSession: true)]
    public CascadingDropDownNameValue[] GetAfiliaciones(
        string knownCategoryValues)
    {
        string claveAfiliacion;
        string nombreBanco = "";

        try
        {
            claveAfiliacion = Convert.ToString(Session["AfiliacionSeleccionada"]);

        }
        catch (Exception ex)
        {
            claveAfiliacion = "";
        }

        StringDictionary kv =
       CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int iBanco;
        if (!kv.ContainsKey("Banco") || !Int32.TryParse(kv["Banco"], out iBanco))
        {
            return null;
        }
        RegistroPago rp2 = new RegistroPago();
        DataTable afiliacionesTotal;
        DataTable tbl = rp2.Afiliaciones(0).Clone();



        try
        {
            afiliacionesTotal = rp2.Afiliaciones(0);

            DataRow[] dtAfiliacionesTemp = afiliacionesTotal.Select("Banco =" + iBanco);

            foreach (DataRow fila in dtAfiliacionesTemp)
            {
                tbl.ImportRow(fila);
            }

        }
        catch (Exception ex)
        {

        }

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        if (claveAfiliacion == "")
        {
            values.Add(new CascadingDropDownNameValue("- Seleccione -", "0"));
        }

        foreach (DataRow dr in tbl.Rows)
        {
            if (claveAfiliacion == "")
            {
                string NumeroAfiliacion = (string)dr["NumeroAfiliacion"];
                int Afiliacion = (int)dr["Afiliacion"];
                values.Add(new CascadingDropDownNameValue(NumeroAfiliacion, Afiliacion.ToString()));
            }
            else
            {
                string NumeroAfiliacion = (string)dr["NumeroAfiliacion"];
                int Afiliacion = (int)dr["Afiliacion"];
                if (Afiliacion.ToString().Equals(claveAfiliacion))
                {
                    values.Add(new CascadingDropDownNameValue(NumeroAfiliacion, Afiliacion.ToString()));

                }
            }


        }
        return values.ToArray();
    }
}