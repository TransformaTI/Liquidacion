// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosPrecio
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
    internal class DatosPrecio : Datos
    {
        public void ConsultarPrecios(byte ClaseRuta, DateTime Fecha, bool PreciosMultiples, DataTable ListaPrecios)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
            {
                new SqlParameter("@ClaseRuta", ClaseRuta),
                new SqlParameter("@Fecha", Fecha),
                new SqlParameter("@MultiPrecio", (bool) (PreciosMultiples ? true : false))
            };
            try
            {
                this._dataAccess.LoadData(ListaPrecios, "spLIQ2CargarPrecios", CommandType.StoredProcedure, sqlParameterArray, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultarPrecios(int Ruta, DateTime Fecha, bool PreciosMultiples, DataTable ListaPrecios)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
            {
                new SqlParameter("@Ruta", Ruta),
                new SqlParameter("@Fecha", Fecha),
                //new SqlParameter("@MultiPrecio", (object) (bool) (PreciosMultiples ? 1 : 0))
                new SqlParameter("@MultiPrecio", (bool) (PreciosMultiples ? true : false))
            };
            try
            {
                this._dataAccess.LoadData(ListaPrecios, "spLIQ2CargarPreciosPorRuta", CommandType.StoredProcedure, sqlParameterArray, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPrecios(DateTime Fecha, DataTable ListaPrecios)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
                new SqlParameter("@Fecha", Fecha)
            };
            try
            {
                this._dataAccess.LoadData(ListaPrecios, "spLIQ2CargarPreciosRI", CommandType.StoredProcedure, sqlParameterArray, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
