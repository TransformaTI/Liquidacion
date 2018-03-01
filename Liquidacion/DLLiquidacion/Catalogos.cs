// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Catalogos
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
  [Serializable]
  public class Catalogos
  {
    private DatosCatalogos _datos;
    private DataTable dtListaPrecios;
    private DataTable dtListaFormaPago;
    private DataTable dtListaTipoPedido;

    public DataTable ListaPrecios
    {
      get
      {
        return this.dtListaPrecios;
      }
    }

    public DataTable ListaFormaPago
    {
      get
      {
        return this.dtListaFormaPago;
      }
    }

    public DataTable ListaTipoCobro
    {
      get
      {
        return this._datos.ListaFormasPago;
      }
    }

    public DataTable ListaTipoPedido
    {
      get
      {
        return this.dtListaTipoPedido;
      }
    }

    public Catalogos()
    {
      this._datos = new DatosCatalogos();
      this.configurarListaFormasPago();
      this.cargaListaTipoPedido();
      this.cargaListaPrecios();
    }

    private void configurarListaFormasPago()
    {
      this.dtListaFormaPago = this._datos.ListaFormasPago.DefaultView.ToTable("FormasPago", 1 != 0, "TipoPago");
    }

    private void cargaListaPrecios()
    {
      this.dtListaPrecios = this._datos.ListaPrecios.DefaultView.ToTable("ListaPrecios", 1 != 0, "Precio", "PorcentajeIva");
    }

    private void cargaListaTipoPedido()
    {
      DataColumn[] dataColumnArray = new DataColumn[1];
      this.dtListaTipoPedido = this._datos.ListaTipoPedido;
      dataColumnArray[0] = this.dtListaTipoPedido.Columns["TipoPedido"];
      this.dtListaTipoPedido.PrimaryKey = dataColumnArray;
    }
  }
}
