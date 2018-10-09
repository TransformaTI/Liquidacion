// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.ControlDeDescuento
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  public sealed class ControlDeDescuento
  {
      private static readonly ControlDeDescuento instance = new ControlDeDescuento();
      private Parametros _parametros;

      public static ControlDeDescuento Instance
      {
          get
          {
              return ControlDeDescuento.instance;
          }
      }
      
      public Parametros Parametros
      {
          set
          {
              this._parametros = value;
          }
      }
      
      private ControlDeDescuento()
      {
      }
      
      public Decimal PrecioAutorizado(DataTable ListaPrecios, Decimal Descuento, byte ZonaEconomica)
      {
          Decimal precioMinimo = 0;
          if (ListaPrecios.Rows.Count > 0)
          {
              try
              {
                    if (ListaPrecios.Compute("MAX(Precio)", "ZonaEconomica = " + ZonaEconomica.ToString()) != System.DBNull.Value)
                    {
                        precioMinimo = Convert.ToDecimal(ListaPrecios.Compute("MAX(Precio)", "ZonaEconomica = " + ZonaEconomica.ToString()));
                    }
              }
              catch (Exception ex)
              {
                  precioMinimo = Convert.ToDecimal(ListaPrecios.Compute("MAX(Precio)", ""));
                  //throw new Exception("zona económica " + ZonaEconomica.ToString());
              }
          }
          return precioMinimo - Descuento;
      }
      
      public Decimal PrecioValido(DataTable ListaPrecios, Decimal Precio)
      {
          foreach (DataRow dataRow in ListaPrecios.Rows)
          {
              if (Convert.ToDecimal(dataRow["Precio"]) == Precio)
              {
                  return Precio;
              }
          }
          return Convert.ToDecimal(ListaPrecios.Compute("MAX(Precio)", ""));
      }
      
      public bool DescuentoAutorizado(Cliente Cliente, Decimal Precio, DataTable ListaPrecios)
      {
          if (Convert.ToBoolean(Convert.ToByte(this._parametros.ValorParametro("DescuentoPorPrecio"))))
          {
              return Precio >= this.PrecioAutorizado(ListaPrecios, Cliente.Descuento, Cliente.ZonaEconomica);
          }
          return true;
      }
  }
}
