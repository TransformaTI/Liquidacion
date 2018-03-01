using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace LiqRegistroPago.Clases
{
    public class CobranzaDocumento
    {
        private string _Documento;
        private double _Importe;

        #region Propiedades
        public string Documento
        {
            get
            {
                return _Documento;
            }
            set
            {
                _Documento = value;
            }
        }

        public double Importe
        {
            get
            {
                return _Importe;
            }
            set
            {
                _Importe = value;
            }
        }
        #endregion

    }

    public class DatosCobro
    {
        private Int16 _FormaPago;
        private decimal _MontoPago;
        private DataTable _Pedidos;
        private Int32 _Cobro;

        #region Propiedades
        public Int16 FormaPago
        {
            get
            {
                return _FormaPago;
            }
            set
            {
                _FormaPago = value;
            }
        }
        public decimal MontoPago
        {
            get
            {
                return _MontoPago;
            }
            set
            {
                _MontoPago = value;
            }
        }

        public DataTable Pedidos
        {
            get
            {
                return _Pedidos;
            }
            set
            {
                _Pedidos = value;
            }
        }

        public Int32 Cobro
        {
            get
            {
                return _Cobro;
            }
            set
            {
                _Cobro = value;
            }
        }
        #endregion



    }
}
