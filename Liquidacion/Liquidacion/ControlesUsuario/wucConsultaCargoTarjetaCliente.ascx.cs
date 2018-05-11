using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ControlesUsuario_wucConsultaCargoTarjetaClienta : System.Web.UI.UserControl
{
    public DataTable dtPagosContarjeta;
    protected void Page_Load(object sender, EventArgs e)
    {
        GrdPagosConTarjeta.DataSource = dtPagosContarjeta;
        GrdPagosConTarjeta.DataBind();
    }
}