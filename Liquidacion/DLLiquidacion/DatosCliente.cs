// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.DatosCliente
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace SigametLiquidacion
{
    [Serializable]
    internal class DatosCliente : Datos
    {
        private int _cliente;
        private DateTime _fSuministro;

        public DataTable ConsultaDatosCliente
        {
            get
            {
                return this.ConsultaCliente();
            }
        }

        public DatosCliente(int Cliente)
        {
            this._cliente = Cliente;
            
        }

        public DatosCliente(int Cliente, DateTime FSuministro)
        {
            this._cliente = Cliente;
            this._fSuministro = FSuministro;
        }


        public string consultaNombreCliente(int ClienteID)
        {
            string NombreCliente = "";
            DataTable dtNombreCliente = new DataTable();
            try
            {
                this._dataAccess.LoadData(dtNombreCliente, "SELECT NOMBRE FROM CLIENTE WHERE CLIENTE = " + ClienteID.ToString(), CommandType.Text, null, true);
                if (dtNombreCliente.Rows.Count > 0)
                {
                    NombreCliente = dtNombreCliente.Rows[0]["NOMBRE"].ToString();
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }


            return NombreCliente;
        }

        public string obtenerCadenaConexion()
        {
            return Conexion;
        }


        public DataTable ConsultaSaldosAFavor(int cliente,string statusMovimiento, int folioMovimiento, int anioMovimiento)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] sqlParameterArray = new SqlParameter[4]
            {
                new SqlParameter("@Cliente", cliente),
                new SqlParameter("@StatusMovimiento",statusMovimiento),
                new SqlParameter("@FolioMovimiento",folioMovimiento),
                new SqlParameter("@AñoMovimiento",anioMovimiento)
            };
            try
            {
                this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaSaldosAFavor", CommandType.StoredProcedure, sqlParameterArray, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;

        }
        

        public DataTable ConsultaCliente()
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
                new SqlParameter("@Cliente", this._cliente),
                new SqlParameter("@FSurtido", this._fSuministro)
            };
            try
            {
                this._dataAccess.LoadData(dataTable, "spLIQ2ConsultaDatosCliente", CommandType.StoredProcedure, sqlParameterArray, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public bool ClienteLiquidado(short AñoAtt, int Folio, int Cliente)
        {
            bool flag = false;
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
            {
                new SqlParameter("@AñoAtt", (object) AñoAtt),
                new SqlParameter("@Folio", (object) Folio),
                new SqlParameter("@Cliente", (object) Cliente)
      };
            try
            {
                SqlDataReader sqlDataReader = this._dataAccess.LoadData("spLIQ2ConsultaClienteLiquidado", CommandType.StoredProcedure, sqlParameterArray);
                this._dataAccess.OpenConnection();
                while (sqlDataReader.Read())
                    flag = true;
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
            return flag;
        }
    }
}
