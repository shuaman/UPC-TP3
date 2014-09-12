using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.CruzDelSur.Negocio.Modelo.Carga;
namespace UPC.CruzDelSur.Datos.Carga.Interface
{
    public interface ICarga
    {
        int f_ActualizarEstadoCarga(String pVCH_ESTADO, String pINT_CODIGO_CARGA);
        int f_AgregarCarga(BE_Carga oBE_Carga);
        List<BE_Carga> f_ListadoCarga(String pOPT, String pNroDocumento);
        BE_Carga f_ListadoUnoCarga(Int32 pCODIGO_CARGA);

    }
}
