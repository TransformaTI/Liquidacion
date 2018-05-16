// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.WebControls.LiquidacionLinkButton
// Assembly: LiquidacionWebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79924F4F-595E-4945-BCBB-D690C4F56B60
// Assembly location: C:\Proyectos\SigametLiquidacion\LiquidacionWebControls.dll

using System.Web.UI.WebControls;

namespace SigametLiquidacion.WebControls
{
  public class LiquidacionLinkButton : LinkButton
  {
    private short _celula;
    private short _ruta;
    private short _añoAtt;
    private int _folio;
    private short _tipo;
    private string _formaLiquidacion;
    private string _statusLiquidacion;
    private int _autotanque;

    public short Celula
    {
      get
      {
        return this._celula;
      }
      set
      {
        this._celula = value;
      }
    }

    public short Ruta
    {
      get
      {
        return this._ruta;
      }
      set
      {
        this._ruta = value;
      }
    }

    public short AñoAtt
    {
      get
      {
        return this._añoAtt;
      }
      set
      {
        this._añoAtt = value;
      }
    }

    public int Folio
    {
      get
      {
        return this._folio;
      }
      set
      {
        this._folio = value;
      }
    }

    public short Tipo
    {
      get
      {
        return this._tipo;
      }
      set
      {
        this._tipo = value;
      }
    }

    public string FormaLiquidacion
    {
      get
      {
        return this._formaLiquidacion;
      }
      set
      {
        this._formaLiquidacion = value;
      }
    }

    public string StatusLiquidacion
    {
      get
      {
        return this._statusLiquidacion;
      }
      set
      {
        this._statusLiquidacion = value;
      }
    }
        /// <summary>
        /// 
        /// </summary>
        public int Autotanque
        {
            get { return this._autotanque; }
            set { _autotanque = value; }
        }


        protected override object SaveViewState()
    {
      this.EnsureChildControls();
      object[] objArray = new object[6];
      object obj = base.SaveViewState();
      objArray[0] = obj;
      objArray[1] = (object) this._celula;
      objArray[2] = (object) this._ruta;
      objArray[3] = (object) this._añoAtt;
      objArray[4] = (object) this._folio;
      objArray[5] = (object) this._tipo;
/*      objArray[6] = (object)this._autotanque*/;
            return (object) objArray;
    }

    protected override void LoadViewState(object savedState)
    {
      object[] objArray = (object[]) savedState;
      base.LoadViewState(objArray[0]);
      this._celula = (short) objArray[1];
      this._ruta = (short) objArray[2];
      this._añoAtt = (short) objArray[3];
      this._folio = (int) objArray[4];
      this._tipo = (short) objArray[5];
      //this._autotanque=(int)objArray[6];
            this.EnsureChildControls();
    }
  }
}
