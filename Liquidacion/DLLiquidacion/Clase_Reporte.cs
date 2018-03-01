// Decompiled with JetBrains decompiler
// Type: Clase_Reporte
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SigametLiquidacion;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public class Clase_Reporte
{
  public ReportDocument RepDoc = new ReportDocument();
  private string _strReporte = "";
  private ArrayList _arrPar = new ArrayList();
  private string _strServidor = "";
  private string _strBase = "";
  private string _strUsuario = "";
  private string _strPW = "";
  private string _strError = "";
  private Datos datos = new Datos();

  public bool Hay_Error
  {
    get
    {
      return this._strError.Length > 0;
    }
  }

  public string Error
  {
    get
    {
      return this._strError;
    }
  }

  public Clase_Reporte(string Reporte, ArrayList Parametros, string Servidor, string Base, string Usuario, string PW)
  {
    if (File.Exists(Reporte))
    {
      try
      {
        this._strReporte = Reporte;
        this._arrPar = (ArrayList) Parametros.Clone();
        this._strServidor = Servidor;
        this._strBase = Base;
        this._strUsuario = Usuario;
        this._strPW = PW;
        try
        {
          this.RepDoc.FileName = Reporte;
        }
        catch
        {
        }
        this.RepDoc.Load(Reporte);
        string tableName = "";
        this.RepDoc.SetDatabaseLogon(Usuario, PW, Servidor, Base);
        foreach (Table table in (SCRCollection) this.RepDoc.Database.Tables)
        {
          TableLogOnInfo logOnInfo = table.LogOnInfo;
          logOnInfo.ConnectionInfo.ServerName = Servidor;
          logOnInfo.ConnectionInfo.DatabaseName = Base;
          logOnInfo.ConnectionInfo.UserID = Usuario;
          logOnInfo.ConnectionInfo.Password = PW;
          try
          {
            table.ApplyLogOnInfo(logOnInfo);
          }
          catch (Exception ex)
          {
            this._strError = ex.ToString();
          }
          tableName = "";
          tableName = table.Name.IndexOf(";") <= 0 ? table.Name : table.Name.Substring(0, table.Name.IndexOf(";"));
        }
        SqlCommand sqlCommand1 = new SqlCommand();
        string currentDirectory = Environment.CurrentDirectory;
        TextReader textReader = (TextReader) new StreamReader(HttpContext.Current.Server.MapPath("Conexion.txt"));
        sqlCommand1.Connection = new SqlConnection(textReader.ReadLine());
        sqlCommand1.CommandType = CommandType.StoredProcedure;
        sqlCommand1.CommandText = tableName;
        ParameterFieldDefinitions parameterFields1 = this.RepDoc.DataDefinition.ParameterFields;
        foreach (ParameterFieldDefinition parameterFieldDefinition1 in (SCRCollection) this.RepDoc.DataDefinition.ParameterFields)
        {
          try
          {
            if (this.Existe_Parametro(Parametros, parameterFieldDefinition1.Name))
            {
              ParameterFieldDefinition parameterFieldDefinition2 = parameterFields1[parameterFieldDefinition1.Name];
              ParameterValues currentValues = parameterFieldDefinition2.CurrentValues;
              ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
              string str = this.Leer_Valor_Parametro(Parametros, parameterFieldDefinition1.Name);
              parameterDiscreteValue.Value = (object) str;
              currentValues.Add((ParameterValue) parameterDiscreteValue);
              parameterFieldDefinition2.ApplyCurrentValues(currentValues);
              SqlParameter sqlParameter = new SqlParameter();
              sqlParameter.ParameterName = parameterFieldDefinition1.Name;
              sqlParameter.Value = (object) str;
              if (!sqlCommand1.Parameters.Contains(parameterFieldDefinition1.Name))
                sqlCommand1.Parameters.Add(sqlParameter);
            }
          }
          catch (Exception ex)
          {
            this._strError = ex.ToString();
          }
        }
        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter();
        sqlDataAdapter1.SelectCommand = sqlCommand1;
        DataTable dataTable1 = new DataTable(tableName);
        sqlDataAdapter1.Fill(dataTable1);
        this.RepDoc.SetDataSource(dataTable1);
        foreach (ReportDocument reportDocument in (ReadOnlyCollectionBase) this.RepDoc.Subreports)
        {
          if (reportDocument != null)
          {
            reportDocument.SetDatabaseLogon(Usuario, PW, Servidor, Base);
            foreach (Table table in (SCRCollection) reportDocument.Database.Tables)
            {
              TableLogOnInfo logOnInfo = table.LogOnInfo;
              logOnInfo.ConnectionInfo.ServerName = Servidor;
              logOnInfo.ConnectionInfo.DatabaseName = Base;
              logOnInfo.ConnectionInfo.UserID = Usuario;
              logOnInfo.ConnectionInfo.Password = PW;
              try
              {
                table.ApplyLogOnInfo(logOnInfo);
              }
              catch (Exception ex)
              {
                this._strError = ex.ToString();
              }
              tableName = "";
              tableName = table.Name.IndexOf(";") <= 0 ? table.Name : table.Name.Substring(0, table.Name.IndexOf(";"));
            }
            SqlCommand sqlCommand2 = new SqlCommand();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlCommand2.CommandText = tableName;
            ParameterFieldDefinitions parameterFields2 = reportDocument.DataDefinition.ParameterFields;
            foreach (ParameterFieldDefinition parameterFieldDefinition1 in (SCRCollection) reportDocument.DataDefinition.ParameterFields)
            {
              try
              {
                if (this.Existe_Parametro(Parametros, parameterFieldDefinition1.Name))
                {
                  ParameterFieldDefinition parameterFieldDefinition2 = parameterFields2[parameterFieldDefinition1.Name];
                  ParameterValues currentValues = parameterFieldDefinition2.CurrentValues;
                  ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
                  string str = this.Leer_Valor_Parametro(Parametros, parameterFieldDefinition1.Name);
                  parameterDiscreteValue.Value = (object) str;
                  currentValues.Add((ParameterValue) parameterDiscreteValue);
                  parameterFieldDefinition2.ApplyCurrentValues(currentValues);
                  SqlParameter sqlParameter = new SqlParameter();
                  sqlParameter.ParameterName = parameterFieldDefinition1.Name;
                  sqlParameter.Value = (object) str;
                  if (!sqlCommand2.Parameters.Contains(parameterFieldDefinition1.Name))
                    sqlCommand2.Parameters.Add(sqlParameter);
                }
              }
              catch (Exception ex)
              {
                this._strError = ex.ToString();
              }
            }
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter2.SelectCommand = sqlCommand2;
            DataTable dataTable2 = new DataTable(tableName);
            sqlDataAdapter2.Fill(dataTable2);
            try
            {
              reportDocument.SetDataSource(dataTable2);
            }
            catch (Exception ex)
            {
              this._strError = ex.ToString();
            }
          }
        }
      }
      catch (Exception ex)
      {
        this._strError = ex.ToString();
      }
    }
    else
      this._strError = "No existe el reporte en la ruta especificada";
  }

  public void Imprimir_Reporte()
  {
    try
    {
      //this.RepDoc.PrintToPrinter(1, true, 0, 0);
    }
    catch (Exception ex)
    {
      this._strError = ex.ToString();
    }
  }

  private string Leer_Valor_Parametro(ArrayList Par, string Nombre)
  {
    try
    {
      Nombre = Nombre.ToUpper();
      bool flag = false;
      string str1 = "";
      for (int index = 0; index < Par.Count && !flag; ++index)
      {
        string str2 = Par[index].ToString();
        if (str2.Trim().Length > 0)
        {
          int length = str2.LastIndexOf("=");
          if (length > 0 && str2.Substring(0, length).ToUpper() == Nombre)
          {
            str1 = str2.Substring(length + 1);
            flag = true;
          }
        }
      }
      return str1;
    }
    catch
    {
      return "";
    }
  }

  private string Leer_Nombre_Parametro(string par)
  {
    try
    {
      if (par.Trim().Length <= 0)
        return "";
      int length = par.LastIndexOf("=");
      if (length > 0)
        return par.Substring(0, length).ToUpper();
      return "";
    }
    catch
    {
      return "";
    }
  }

  private string Leer_Valor_Parametro(string par)
  {
    try
    {
      if (par.Trim().Length <= 0)
        return "";
      int num = par.LastIndexOf("=");
      if (num > 0)
        return par.Substring(num + 1);
      return "";
    }
    catch
    {
      return "";
    }
  }

  private bool Existe_Parametro(ArrayList Par, string Nombre)
  {
    try
    {
      Nombre = Nombre.ToUpper();
      bool flag = false;
      for (int index = 0; index < Par.Count && !flag; ++index)
      {
        string str = Par[index].ToString();
        if (str.Trim().Length > 0)
        {
          int length = str.LastIndexOf("=");
          if (length > 0 && str.Substring(0, length).ToUpper() == Nombre)
            flag = true;
        }
      }
      return flag;
    }
    catch
    {
      return false;
    }
  }

  private ReportDocument OpenSubreport(ReportDocument Reporte, string reportObjectName)
  {
    ReportDocument reportDocument = new ReportDocument();
    SubreportObject subreportObject = Reporte.ReportDefinition.ReportObjects[reportObjectName] as SubreportObject;
    if (subreportObject == null)
      return (ReportDocument) null;
    string subreportName = subreportObject.SubreportName;
    return Reporte.OpenSubreport(subreportName);
  }
}
