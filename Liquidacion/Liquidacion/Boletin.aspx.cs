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

using SigametLiquidacion;

public partial class Boletin : System.Web.UI.Page
{
    Boletines _boletin;

    private TreeNode AddChildNode(TreeNode Parent, string Caption, string Value, string NodeValue, TreeNodeSelectAction SelectAction)
    {
        TreeNode subChild = new TreeNode();
        subChild.Text = (Caption + " " + Value).Trim();
        subChild.SelectAction = SelectAction;
        if (NodeValue != null)
        {
            subChild.Value = NodeValue; //El separador del indicador de nodos hijos es el caracter ":", la cadena antes del caracter ":"
        }
        //indica la operación a realizar.
        Parent.ChildNodes.Add(subChild);
        return subChild;
    }

    private void cargaTreeView()
    {
        FoliosPendientes _foliosPendientes = new FoliosPendientes();

        _foliosPendientes.ConsultaFoliosPendientes(Convert.ToDateTime(txtFAsignacion.Text));//Cargar de calendario, usar el calendario estándar de javascript.
        _foliosPendientes.ConsultaRutasAutorizadasPorUsuario("JOGUDO", Convert.ToDateTime(txtFAsignacion.Text));//Cargar de variable de sesión y cargar de calendario
        //_foliosPendientes.ConsultaCelulas();
        tvRutas.Nodes.Clear();
        foreach (DataRow dr in _foliosPendientes.ConsultaCelulas().Rows)
        {
            TreeNode nodoCelula = new TreeNode();
            nodoCelula.Text = Convert.ToString(dr["NombreCelula"]);
            nodoCelula.SelectAction = TreeNodeSelectAction.Expand;
            tvRutas.Nodes.Add(nodoCelula);

            TreeNode nodoTodos = AddChildNode(nodoCelula, "Todos",
                string.Empty, null, TreeNodeSelectAction.Expand);

            TreeNode nodoTodosBoletin = AddChildNode(nodoTodos, "Boletín",
                string.Empty, null, TreeNodeSelectAction.Expand);

            TreeNode nodoTodosBoletinado = AddChildNode(nodoTodos, "Boletinado",
                string.Empty, null, TreeNodeSelectAction.Expand);
                       
            foreach (DataRow drRuta in _foliosPendientes.ConsultaRutas(Convert.ToInt16(dr["Celula"])).Rows)
            {
                if (_foliosPendientes.RutaAsignada(drRuta["Ruta"]))//Verificar si el usuario tiene esta ruta asignada,
                //o tiene derecho a ver todas las rutas
                {
                    TreeNode nodoRuta = AddChildNode(nodoCelula, string.Empty,
                        Convert.ToString(drRuta["NombreRuta"]), null, TreeNodeSelectAction.Expand);

                    TreeNode nodoBoletin = AddChildNode(nodoRuta, "Boletín",
                            Convert.ToString(drRuta["Ruta"]), null, TreeNodeSelectAction.Expand);

                    TreeNode nodoBoletinado = AddChildNode(nodoRuta, "Boletinado",
                        Convert.ToString(drRuta["Ruta"]), null, TreeNodeSelectAction.Expand);
                }
            }
        }
        tvRutas.CollapseAll();
    }

    protected void btnCargaRutas_Click(object sender, ImageClickEventArgs e)
    {
        cargaTreeView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _boletin = new Boletines();

        ListaPedidos1.DataSource = _boletin.ListaBoletines;

        //cargaTreeView();
    }

    protected void ListaPedidos1_Click(object sender, EventArgs e)
    {

    }
}

