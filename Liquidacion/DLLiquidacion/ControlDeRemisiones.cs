// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.ControlDeRemisiones
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  public class ControlDeRemisiones
  {
    public bool RemisionExistente(string SerieRemision, string Remision)
    {
      DatosControlDeRemisiones controlDeRemisiones = new DatosControlDeRemisiones();
      try
      {
        return controlDeRemisiones.Remision(SerieRemision, Remision).Rows.Count > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool RemisionExistente(int Celula, int AnioPed, int Pedido, string SerieRemision, string Remision)
    {
      DatosControlDeRemisiones controlDeRemisiones = new DatosControlDeRemisiones();
      bool flag = false;
      try
      {
        DataTable dataTable = controlDeRemisiones.RemisionCapturada(SerieRemision, Remision);
        if (dataTable.Rows.Count > 0)
          flag = Celula != Convert.ToInt32(dataTable.Rows[0]["Celula"]) || AnioPed != Convert.ToInt32(dataTable.Rows[0]["AñoPed"]) || Pedido != Convert.ToInt32(dataTable.Rows[0]["Pedido"]);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return flag;
    }

    public bool ValidarNota(string SerieRemision, string Remision)
    {
      DatosControlDeRemisiones controlDeRemisiones = new DatosControlDeRemisiones();
      try
      {
        return controlDeRemisiones.ValidarNota(SerieRemision, Remision).Rows.Count > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
