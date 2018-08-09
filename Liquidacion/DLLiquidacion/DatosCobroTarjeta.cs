// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosPedido
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
    [Serializable]
    internal class DatosCobroTarjeta : Datos
    {
        private int _banco;
        private string _autorizacion;
        private string _tarjeta;

        public DatosCobroTarjeta(int Banco, string Autorizacion, string Tarjeta)
        {
            this._banco = Banco;
            this._autorizacion = Autorizacion;
            this._tarjeta = Tarjeta;
        }

     

       
        public bool consulta()
        {
            bool encontrado = false; 
       
            int num = 0;
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
            {
                new SqlParameter("@Banco", (object) this._banco),
                new SqlParameter("@Autorizacion", (object) this._autorizacion),
                new SqlParameter("@Tarjeta", (object) this._tarjeta)        
            };
            try
            {
                SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLiqConsultaCargoTarjeta", CommandType.StoredProcedure, sqlParameterArray);
                this._dataAccess.OpenConnection();
                encontrado = sqlDataReader.Read();
                
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._dataAccess.CloseConnection();
            }
            return encontrado;
        }
    }
}
