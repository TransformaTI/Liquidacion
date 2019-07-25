// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Precio
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;

namespace SigametLiquidacion
{
    public class Precio
    {
        private byte _claseRuta;
        private DateTime _fecha;
        private bool _preciosMultiples;
        private int _ruta;
        private DataTable dtListaPrecios;//Lista de precios de acuerdo a la zona del camión
        private DataTable dtListaPreciosCompleto;//Lista de precios completos para cargar de acuerdo a la fecha de venta y descuento del cliente
        private DatosPrecio _datosPrecio;
        private Decimal _precioVigente;
        
        public Decimal PrecioVigente
        {
            get
            {
                return this._precioVigente;
            }
        }
        
        public Precio(byte ClaseRuta, DateTime Fecha, bool PreciosMultiples)
        {
            this._claseRuta = ClaseRuta;
            this._fecha = Fecha;
            this._preciosMultiples = PreciosMultiples;
            this.dtListaPrecios = new DataTable("ListaPrecios");//Precios por la zona del autotanque
            this._datosPrecio = new DatosPrecio();
            this._datosPrecio.ConsultarPrecios(ClaseRuta, Fecha, PreciosMultiples, this.dtListaPrecios);
        }
        
        public Precio(int Ruta, byte ClaseRuta, DateTime Fecha, bool PreciosMultiples)
        {
            this._ruta = Ruta;
            this._claseRuta = ClaseRuta;
            this._fecha = Fecha;
            this._preciosMultiples = PreciosMultiples;
            this.dtListaPrecios = new DataTable("ListaPrecios");
            this.dtListaPreciosCompleto = new DataTable("ListaPreciosCompleto");
            this._datosPrecio = new DatosPrecio();
            this._datosPrecio.ConsultarPrecios(Ruta, Fecha, PreciosMultiples, this.dtListaPrecios);
            this._datosPrecio.ConsultarPrecios(Fecha, this.dtListaPreciosCompleto);
        }

        public DataTable ListaCompletaPrecios
        {
            get
            {
                return this.dtListaPreciosCompleto;
            }
        }
        
        public DataTable ListaPrecios()
        {      
            DataTable dataTable = dtListaPrecios.Clone();




            if (dtListaPrecios.Rows.Count >0)
            {
                this._precioVigente = Convert.ToDecimal(this.dtListaPrecios.Compute("MAX(Precio)", "ClaseRuta = " + this._claseRuta.ToString()));
            
           

                    DataRow[] dr = dtListaPrecios.Select("precio="+ _precioVigente.ToString());

                    if (!this._preciosMultiples)
                    {
                        DataRow row = dataTable.NewRow();
                        row.BeginEdit();
                        row["Precio"] = (object) this._precioVigente;
                        row["ZonaEconomica"] = (object)dr[0]["ZonaEconomica"];
                        row.EndEdit();
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        foreach (DataRow dataRow in this.dtListaPrecios.Select("", "Precio DESC"))
                        {
                            DataRow row = dataTable.NewRow();
                            foreach (DataColumn dataColumn in (InternalDataCollectionBase)this.dtListaPrecios.Columns)
                            {
                                row[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
                            }
                            dataTable.Rows.Add(row);
                        }
                    }

            }
            return dataTable;
        }
    }
}
