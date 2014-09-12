using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;


using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UPC.CruzDelSur.Datos.Carga;
using UPC.CruzDelSur.Negocio.Modelo.Carga;
namespace CRUZDELSUR.UI.Web.GestionCarga
{
    public partial class ActualizarFichaCarga : System.Web.UI.Page
    {

        //ServletGestionCarga _servletGestionCarga = new ServletGestionCarga();


        protected void Page_Load(object sender, EventArgs e)
        {
            //    //if (!Page.IsPostBack)
            //    //{

            if (!String.IsNullOrEmpty(Context.Request.QueryString["idficha"]))
            {
                hffichacarga.Value = Context.Request.QueryString["idficha"].ToString();


                Session["idcarga"] = hffichacarga.Value;


                BL_Carga oBL_Carga = new BL_Carga();
                BE_Carga oBE_Carga = oBL_Carga.f_ListadoUnoCarga(Int32.Parse(hffichacarga.Value));


                txtFicha.Text = oBE_Carga.FICHA;
                txtObservacion.Text = oBE_Carga.OBSERVACION;
                lblClave.Text = oBE_Carga.CLAVE_SEGURIDAD;
                txtImporteTotal.Text = oBE_Carga.DBL_IMPORTETOTAL.ToString();
                txtPesoTotal.Text = oBE_Carga.DBL_PESOTOTAL.ToString();
                ddlTipoPago.SelectedValue = oBE_Carga.TIPO_PAGO.ToString();


                BL_DetalleCarga oBL_DetalleCarga = new BL_DetalleCarga();


                if (Session["ListaProducto"] == null)
                    Session["ListaProducto"] = oBL_DetalleCarga.f_ListaDetalleCarga(Int32.Parse(hffichacarga.Value));



                Session["idProgramacionRuta"] = oBE_Carga.CODIGO_PROGRAMACION_RUTA;
                Session["idRemitente"] = oBE_Carga.CLIENTE_ORIGEN;
                Session["idDestinatario"] = oBE_Carga.CLIENTE_DESTINO;
            }


            if (Session["idProgramacionRuta"] != null)
            {
                //CREAMOS LOS PARAMETROS

                BL_Programacion_Ruta oBL_Programacion_Ruta = new BL_Programacion_Ruta();

                BE_Programacion_Ruta oBE_Programacion_Ruta = oBL_Programacion_Ruta.f_UnoProgramacion_Ruta(Int32.Parse(Session["idProgramacionRuta"].ToString()));


                ddlAgenciaDestino.SelectedValue = oBE_Programacion_Ruta.CODIGO_AGENCIADESTINO.ToString();
                txtFechasalida.Text = oBE_Programacion_Ruta.FECHA_ORIGEN.Value.ToShortDateString();
                txtfechallegada.Text = oBE_Programacion_Ruta.FECHA_DESTINO.Value.ToShortDateString();
                MK_ProgramacionRuta_ID.Value = oBE_Programacion_Ruta.CODIGO_PROGRAMACION_RUTA.ToString();
                txtUnidadTransporte.Text = oBE_Programacion_Ruta.PLACA.ToString();
            }


            //    if (Session["clave"] != null)
            //    {
            //        lblClave.Text = Session["clave"].ToString();
            //    }

            if (Session["idRemitente"] != null)
            {
                BL_Cliente oBL_Cliente = new BL_Cliente();
                BE_Cliente oCliente = oBL_Cliente.f_UnoCliente(Session["idRemitente"].ToString());
                txtRemitente.Text = String.Concat(oCliente.NOMBRES, " ", oCliente.APELLIDOS);
                txtDniRemitente.Text = oCliente.DOCUMENTO;
                HFIdClienteRemi.Value = oCliente.DOCUMENTO;
            }
            if (Session["idDestinatario"] != null)
            {


                BL_Cliente oBL_Cliente = new BL_Cliente();
                BE_Cliente oCliente = oBL_Cliente.f_UnoCliente(Session["idDestinatario"].ToString());

                txtDestinatario.Text = String.Concat(oCliente.NOMBRES, " ", oCliente.APELLIDOS);
                txtDNIDestinatario.Text = oCliente.DOCUMENTO;

                HFIdClienteDest.Value = oCliente.DOCUMENTO;

            }



            List<BE_DetalleCarga> ListaProducto = new List<BE_DetalleCarga>();
            if (Session["ListaProducto"] != null)
            {
                ListaProducto = (List<BE_DetalleCarga>)Session["ListaProducto"];
            }
            else
            {
                gvProductos.DataSource = null;
                gvProductos.DataBind();
            }
            if (Session["idProducto"] != null)
            {
                BL_Producto oBL_Producto = new BL_Producto();
                BE_Producto oBE_Producto = oBL_Producto.f_ListadoUnoProducto(Int32.Parse(Session["idProducto"].ToString()));


                ListaProducto.RemoveAll(item => item.CODIGO_PRODUCTO == oBE_Producto.CODIGO_PRODUCTO);
                ListaProducto.RemoveAll(item => item.CODIGO_PRODUCTO == 0);

                BE_DetalleCarga oBE_DetalleCarga = new BE_DetalleCarga();
                oBE_DetalleCarga.CODIGO_PRODUCTO = oBE_Producto.CODIGO_PRODUCTO;
                oBE_DetalleCarga.DESCRIPCION = oBE_Producto.DESCRIPCION;
                oBE_DetalleCarga.PRECIO = oBE_Producto.PRECIO;
                ListaProducto.Add(oBE_DetalleCarga);

                Session["ListaProducto"] = ListaProducto;
            }
            if (Session["clave"] != null)
            {
                lblClave.Text = Session["clave"].ToString();
            }


            if (gvProductos.Rows.Count != ListaProducto.Count())
            {
                gvProductos.DataSource = ListaProducto;
                gvProductos.DataBind();
            }





            //    //}

            //}
            //protected void setInfo()
            //{
            //    ViewState["codigoFicha"] = txtFicha.Text;
            //}
            //protected void GetInfo()
            //{
            //    txtFicha.Text = ViewState["codigoFicha"].ToString();
        }
        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                List<BE_DetalleCarga> ListaProducto = new List<BE_DetalleCarga>();
                if (Session["ListaProducto"] != null)
                {
                    ListaProducto = (List<BE_DetalleCarga>)Session["ListaProducto"];
                    BL_Producto oBL_Producto = new BL_Producto();
                    BE_Producto oBE_Producto = oBL_Producto.f_ListadoUnoProducto(Int32.Parse(e.CommandArgument.ToString()));

                    ListaProducto.RemoveAll(item => item.CODIGO_PRODUCTO == oBE_Producto.CODIGO_PRODUCTO);

                    Session["ListaProducto"] = ListaProducto;
                    gvProductos.DataSource = ListaProducto;
                    gvProductos.DataBind();



                    Double importetotal = 0;
                    Double pesototal = 0;
                    foreach (GridViewRow row in gvProductos.Rows)
                    {
                        TextBox txtPeso = (TextBox)row.FindControl("txtPeso");

                        TextBox txtimporte = (TextBox)row.FindControl("txtimporte");
                        DropDownList ddlTipoCarga = (DropDownList)row.FindControl("ddlTipoCarga");

                        if (txtimporte.Text!="")
                        importetotal += double.Parse(txtimporte.Text.Replace("","0").ToString());
                        if (txtPeso.Text!="")
                        pesototal += double.Parse(txtPeso.Text.Replace("","0"));
                        
                    }
                    txtImporteTotal.Text = importetotal.ToString();
                    txtPesoTotal.Text  = pesototal.ToString();


                }
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            if (gvProductos.Rows.Count == 0)
            {
                this.Controls.Add(new LiteralControl("<script language='JavaScript'>alert('Debe Seleccionar Productos'); </script>"));
                return;
            }

            if (lblClave.Text.Trim().ToString().Length == 0)
            {
                this.Controls.Add(new LiteralControl("<script language='JavaScript'>alert('Debe Ingresar la clave de seguridad'); </script>"));
                return;
            }




            BE_Carga oBE_Carga = new BE_Carga();
            oBE_Carga.CODIGO_CARGA = Int32.Parse(hffichacarga.Value);

            List<BE_DetalleCarga> oListaDetalleFCargaDTO = new List<BE_DetalleCarga>();
            Double importetotal = 0;
            Double pesototal = 0;
            foreach (GridViewRow row in gvProductos.Rows)
            {
                BE_DetalleCarga oDetalleFCargaDTO = new BE_DetalleCarga();


                BE_DetalleCarga oBEMG_ES03_Producto = (BE_DetalleCarga)row.DataItem;


                TextBox txtCantidad = row.FindControl("txtCantidad") as TextBox;
                TextBox txtPeso = (TextBox)row.FindControl("txtPeso");

                TextBox txtimporte = (TextBox)row.FindControl("txtimporte");
                DropDownList ddlTipoCarga = (DropDownList)row.FindControl("ddlTipoCarga");

                importetotal += double.Parse(txtimporte.Text);
                pesototal += double.Parse(txtPeso.Text);



                oDetalleFCargaDTO.CANTIDAD = Int32.Parse(txtCantidad.Text);
                oDetalleFCargaDTO.DESCRIPCION = row.Cells[1].Text;
                oDetalleFCargaDTO.CODIGO_PRODUCTO = Int32.Parse(row.Cells[0].Text);
                oDetalleFCargaDTO.DBL_IMPORTE = double.Parse(txtimporte.Text);
                oDetalleFCargaDTO.DBL_PESO = double.Parse(txtPeso.Text);

                oDetalleFCargaDTO.TIPO_CARGA = Int32.Parse(ddlTipoCarga.SelectedValue);

                oDetalleFCargaDTO.CODIGO_CARGA = oBE_Carga.CODIGO_CARGA;
                oListaDetalleFCargaDTO.Add(oDetalleFCargaDTO);
            }







            oBE_Carga.CLAVE_SEGURIDAD = lblClave.Text;
            oBE_Carga.DESTINATARIO = txtDestinatario.Text;
            oBE_Carga.CLIENTE_DESTINO = HFIdClienteDest.Value;
            oBE_Carga.DESTINATARIO = txtDestinatario.Text;
            oBE_Carga.ESTADO = "Registrado";
            oBE_Carga.CODIGO_GUIA = null;
            oBE_Carga.ESTADOPAGO = ddlTipoPago.SelectedValue;
            oBE_Carga.FECHA_REGISTRO = DateTime.Now.Date;
            oBE_Carga.FICHA = txtFicha.Text;
            oBE_Carga.CLIENTE_ORIGEN = HFIdClienteRemi.Value;
            oBE_Carga.REMITENTE = txtRemitente.Text;
            oBE_Carga.DBL_IMPORTETOTAL = importetotal;
            oBE_Carga.OBSERVACION = txtObservacion.Text;
            oBE_Carga.DBL_PESOTOTAL = pesototal;
            oBE_Carga.CODIGO_PROGRAMACION_RUTA = int.Parse(MK_ProgramacionRuta_ID.Value);







            oBE_Carga.oDetalleCarga = oListaDetalleFCargaDTO;

            BL_Carga oBL_Carga = new BL_Carga();





            int resultado = oBL_Carga.f_AgregarCarga(oBE_Carga);




            this.Controls.Add(new LiteralControl("<script language='JavaScript'>alert('El Registro se realizo con exito'); window.location = 'ListadoFichaCarga.aspx'; </script>"));
            //Response.Redirect("ListadoFichaCarga.aspx");
        }

        protected void btnIngresarCodigo_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoFichaCarga.aspx");
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                BE_DetalleCarga oBEMG_ES03_Producto = (BE_DetalleCarga)e.Row.DataItem;


                TextBox txtCantidad = e.Row.FindControl("txtCantidad") as TextBox;
                TextBox txtimporte = e.Row.FindControl("txtimporte") as TextBox;
                Label lblPrecio = e.Row.FindControl("lblPrecio") as Label;

                TextBox txtPeso = e.Row.FindControl("txtPeso") as TextBox;

                if (lblPrecio != null)
                {

                    //lblPrecio.ID = String.Concat("precio-", oBEMG_ES03_Producto.CODIGO_PRODUCTO.ToString());

                }

                if (txtCantidad != null)
                {
                    //txtCantidad.ID = String.Concat("producto-", oBEMG_ES03_Producto.CODIGO_PRODUCTO.ToString());

                    txtCantidad.Attributes.Add("onChange", "CalcularImporteProducto('" + txtCantidad.ClientID + "','" + lblPrecio.ClientID + "','" + txtPeso.ClientID +  "','" + txtimporte.ClientID + "')");
                   


                }

                if (txtPeso != null)
                {

                    txtPeso.Attributes.Add("onChange", "CalcularImporteProducto('" + txtCantidad.ClientID + "','" + lblPrecio.ClientID + "','" + txtPeso.ClientID + "','" + txtimporte.ClientID + "');CalcularPesoTotal()");
                    //txtPeso.Attributes.Add("onChange", "");

                }


            }
        }

    }
}