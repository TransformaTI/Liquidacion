// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Datos
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using SGDAC;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace SigametLiquidacion
{
  [Serializable]
  public class Datos
  {
    protected short _AñoAtt;
    protected int _Folio;
    protected string _conexion;
   

        [NonSerialized]
    protected DAC _dataAccess;

        protected string Conexion
        {
            get
            {
                return _conexion;
            }

           
        }

        

        //public string Conexion
        //{
        //    get
        //    {
        //        return _conexion;
        //    }


        //}
        public void generaConexion(string conexion)
        {            
            _conexion = conexion;
            this._dataAccess = new DAC(new SqlConnection(_conexion));            
        }

        public Datos()
    {
           
            try
            {
                this.DataCompInitialize();
            }
            catch
            {
               
            }
      
    }

    public void DataCompInitialize()
    {
      TextReader textReader = (TextReader) new StreamReader(HttpContext.Current.Server.MapPath("Conexion.txt"));
     _conexion = textReader.ReadLine();
      this._dataAccess = new DAC(new SqlConnection(_conexion));
      textReader.Close();
    }

    protected DataTable ConsultaDatosPorFolio(string CommandText, CommandType CommandType)
    {
      DataTable dataTable = new DataTable();
      SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@AñoAtt", (object) this._AñoAtt),
        new SqlParameter("@Folio", (object) this._Folio)
      };
      try
      {
        this._dataAccess.LoadData(dataTable, CommandText, CommandType, sqlParameterArray, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return dataTable;
    }
  }
}
