using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web;
using System.Diagnostics;

namespace SigametLiquidacion
{
    public class LogOperacion
    {
        string message = "";
        string Encabezado = "";
        public void EscribeLogOperacionRow(DataRow dr, Int64 Folio, int añoFolio, string OrigenInfo)
        {



            message += "|"+ string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += "|" + Folio.ToString();
            message += "|" + añoFolio.ToString();
            message += "|" + OrigenInfo.ToString();

            Encabezado += "|" + "FechaHora|Folio|AñoFolio|OrigenInfo";

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                Encabezado += "|" + dr.Table.Columns[i].ColumnName.ToString();

            }



            Encabezado += Environment.NewLine;

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {

                message += "|" + dr.ItemArray[i].ToString();

            }

            try
            {

                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/LogOperacion.txt")))
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/LogOperacion.txt");
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {

                        writer.WriteLine(message);
                        writer.Close();

                    }
                }
                else
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/LogOperacion.txt");
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(Encabezado);
                        writer.WriteLine(message);
                        writer.Close();

                    }


                }


            }
            catch (Exception e)
            {
                if (!EventLog.SourceExists("LiquidacionWeb"))
                {
                    EventLog.CreateEventSource("LiquidacionWeb", "Application");
                }
                EventLog.WriteEntry("LiquidacionWeb", e.Message,EventLogEntryType.Information);
            }
                                 


        }



    }
}
