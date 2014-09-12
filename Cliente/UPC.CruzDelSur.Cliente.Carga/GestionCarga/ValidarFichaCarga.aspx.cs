﻿using System;
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

using UPC.CruzDelSur.Negocio.Modelo.Carga;
using UPC.CruzDelSur.Datos.Carga;
namespace CRUZDELSUR.UI.Web.GestionCarga
{
    public partial class ValidarFichaCarga : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                if (!String.IsNullOrEmpty(Context.Request.QueryString["idficha"]))
                {
                    hffichacarga.Value = Context.Request.QueryString["idficha"].ToString();

                    btnIngresarCodigo.OnClientClick = "javascript:OpenModalDialog('Validar.aspx?opt=2&idcarga=" + hffichacarga.Value + " ','null','400','800')";



                    BL_Carga oBL_Carga = new BL_Carga();






                    BE_Carga oBEMG_ES01_FichaCarga = oBL_Carga.f_ListadoUnoCarga(Int32.Parse(hffichacarga.Value));


                    lblEstadoPago.Text = oBEMG_ES01_FichaCarga.ESTADOPAGO;
                    lblClave.Text = oBEMG_ES01_FichaCarga.CLAVE_SEGURIDAD ;
                    lblNumeroFicha.Text = oBEMG_ES01_FichaCarga.FICHA;
                    lblImporteTotal.Text = oBEMG_ES01_FichaCarga.DBL_IMPORTETOTAL.ToString();







                    


                    BL_Programacion_Ruta oBL_Programacion_Ruta = new BL_Programacion_Ruta();


                    BE_Programacion_Ruta oBE_Programacion_Ruta = oBL_Programacion_Ruta.f_UnoProgramacion_Ruta(Int32.Parse(oBEMG_ES01_FichaCarga.CODIGO_PROGRAMACION_RUTA.ToString()));



                    lblAgenciaOrigen.Text = oBE_Programacion_Ruta.ORIGEN;
                    lblAgenciaDestino.Text = oBE_Programacion_Ruta.DESTINO;





                    BL_Cliente oBL_Cliente = new BL_Cliente();

                    BE_Cliente oBE_Cliente = oBL_Cliente.f_UnoCliente(oBEMG_ES01_FichaCarga.CLIENTE_ORIGEN);

                    lblRemitente.Text = String.Concat(oBE_Cliente.NOMBRES, " ", oBE_Cliente.APELLIDOS);


                    BE_Cliente oBE_Cliente2 = oBL_Cliente.f_UnoCliente(oBEMG_ES01_FichaCarga.CLIENTE_DESTINO);

                    lblDestinatario.Text = String.Concat(oBE_Cliente2.NOMBRES, " ", oBE_Cliente2.APELLIDOS);





                }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoFichaCarga.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoFichaCarga.aspx");
        }
    }
}