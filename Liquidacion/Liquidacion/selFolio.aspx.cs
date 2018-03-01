using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class selFolio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        Session["AñoAtt"] = Convert.ToInt16(txtAñoAtt.Text);
        Session["Folio"] = Convert.ToInt32(txtFolio.Text);
        Response.Redirect("Liquidacion.aspx");
    }
}
