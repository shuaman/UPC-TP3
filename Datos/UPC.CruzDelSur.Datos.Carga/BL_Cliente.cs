using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.CruzDelSur.Negocio.Modelo.Carga;

namespace UPC.CruzDelSur.Datos.Carga
{
    public class BL_Cliente
    {
        public List<BE_Cliente> f_ListadoCliente(String pNroDocumento)
        {
            List<BE_Cliente> lst = new List<BE_Cliente>();

            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@VCH_DOCUMENTO", SqlDbType.VarChar);
            param[0].Value = pNroDocumento;
            param[0].Direction = ParameterDirection.Input;


            DataSet ds = SqlHelper.ExecuteDataSet(Conexion.CadenaConexion, System.Data.CommandType.StoredProcedure, "SP_LISTARCLIENTE",param);
            int ColumnCount = ds.Tables.Count;
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //Object of the propery class
                BE_Cliente objCliente = new BE_Cliente();
                //asign values
                if (DBNull.Value != dr["VCH_DOCUMENTO"])
                objCliente.DOCUMENTO = dr["VCH_DOCUMENTO"].ToString();

                if (DBNull.Value != dr["VCH_NOMBRES"])
                objCliente.NOMBRES = dr["VCH_NOMBRES"].ToString();

                if (DBNull.Value != dr["VCH_APELLIDOS"])
                objCliente.APELLIDOS = dr["VCH_APELLIDOS"].ToString();

                if (DBNull.Value != dr["VCH_DIRECCION"])
                objCliente.DIRECCION= dr["VCH_DIRECCION"].ToString();

                if (DBNull.Value != dr["VCH_CORREO"])
                objCliente.CORREO= dr["VCH_CORREO"].ToString();

                if (DBNull.Value != dr["VCH_TELEFONO"])
                objCliente.TELEFONO= dr["VCH_TELEFONO"].ToString();

                if (DBNull.Value != dr["INT_UBIGEO"])
                objCliente.UBIGEO =  int.Parse(dr["INT_UBIGEO"].ToString());
                //add one row to the list
                lst.Add(objCliente);
            }
            return lst;
        }
        public BE_Cliente f_UnoCliente(String pDocumento)
        {
            BE_Cliente objCliente = new BE_Cliente();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VCH_DOCUMENTO", SqlDbType.VarChar);
            param[0].Value = pDocumento;
            param[0].Direction = ParameterDirection.Input;



            DataSet ds = SqlHelper.ExecuteDataSet(Conexion.CadenaConexion, System.Data.CommandType.StoredProcedure, "SP_LISTARUNOCLIENTE", param);
            int ColumnCount = ds.Tables.Count;
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //Object of the propery class
                
                //asign values
                if (DBNull.Value != dr["VCH_DOCUMENTO"])
                    objCliente.DOCUMENTO = dr["VCH_DOCUMENTO"].ToString();

                if (DBNull.Value != dr["VCH_NOMBRES"])
                    objCliente.NOMBRES = dr["VCH_NOMBRES"].ToString();

                if (DBNull.Value != dr["VCH_APELLIDOS"])
                    objCliente.APELLIDOS = dr["VCH_APELLIDOS"].ToString();

                if (DBNull.Value != dr["VCH_DIRECCION"])
                    objCliente.DIRECCION = dr["VCH_DIRECCION"].ToString();

                if (DBNull.Value != dr["VCH_CORREO"])
                    objCliente.CORREO = dr["VCH_CORREO"].ToString();

                if (DBNull.Value != dr["VCH_TELEFONO"])
                    objCliente.TELEFONO = dr["VCH_TELEFONO"].ToString();

                if (DBNull.Value != dr["INT_UBIGEO"])
                    objCliente.UBIGEO = int.Parse(dr["INT_UBIGEO"].ToString());
                //add one row to the list
                
            }
            return objCliente;
        }
    }
}
