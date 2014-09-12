using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;

using UPC.CruzDelSur.Datos.Carga;
using UPC.CruzDelSur.Negocio.Modelo.Carga;

namespace CRUZDELSUR.UI.Web.GestionCarga
{
    public partial class ConsultaProducto : System.Web.UI.Page
    {
        //ServletGestionCarga _servletGestionCarga = new ServletGestionCarga();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProdutos();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarProdutos();
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                Session["idProducto"] = e.CommandArgument;
                this.Controls.Add(new LiteralControl("<script language='JavaScript'>alert('Seleccionado'); CloseFormOK();</script>"));
            }
        }
        void CargarProdutos()
        {
            //CREAMOS LOS PARAMETROS
            BL_Producto oBL_Producto = new BL_Producto();

            gvProductos.DataSource = oBL_Producto.f_ListadoProducto(txtDescripcion.Text.Trim());
            gvProductos.DataBind();

            
            
        }
    }
}