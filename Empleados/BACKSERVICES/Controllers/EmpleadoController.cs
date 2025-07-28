using BACKSERVICES.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BACKSERVICES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        string connectionString =
              "Data Source=DACTPC\\SQLEXPRESS;" +
              "Initial Catalog=promtest;" +
              "User id=sa;" +
              "Password=casa123.;";

        // GET: api/<PlazasEmpleadoController>sql 
        [HttpGet]
        public JsonResult Get()
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@Id", 123),
                        //new SqlParameter("@Fecha", DateTime.Now)
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_select_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;

                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });
                        
                    }

                    db.Close();
                }
                srvResponse.success = true;
                srvResponse.mensaje = "";
                srvResponse.codigo = 200;
                srvResponse.data = rst;

            }
            catch (Exception ex) {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = 500;
            }
            
            return new JsonResult(srvResponse);

            //return new string[] { "value1", "value2" };
        }

        // GET api/<EmpleadoController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try
            {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@i_codigo", id),
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_select_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;
                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });
                    }

                    db.Close();
                }

                srvResponse.success = true;
                srvResponse.mensaje = "Datos Enncontrados.";
                srvResponse.codigo = 200;
                srvResponse.data = rst;

                if (rst.Count < 1) { srvResponse.codigo = 404; srvResponse.mensaje = "No Encontrado"; }

            }
            catch (Exception ex)
            {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = -1;
            }

            return new JsonResult(srvResponse);
        }

        // POST api/<EmpleadoController>
        [HttpPost]
        public JsonResult Post([FromBody] plazaEmpleadoModel empleado)
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@i_puesto", empleado.puesto),
                        new SqlParameter("@i_nombre", empleado.nombre),
                        new SqlParameter("@i_codigo_jefe", (empleado.codigo_jefe>0)?empleado.codigo_jefe:DBNull.Value),
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_insert_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;
                        srvResponse.mensaje = row["MENSAJE"].ToString();
                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });

                    }

                    db.Close();
                    srvResponse.success = true;
                    
                    srvResponse.codigo = 200;
                    srvResponse.data = rst;
                }
            }
            catch (Exception ex)
            {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = 500;
            }
            return new JsonResult(srvResponse);
        }

        // PUT api/<EmpleadoController>/5
        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] plazaEmpleadoModel empleado)
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try
            {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@i_codigo", id),
                        new SqlParameter("@i_puesto", empleado.puesto),
                        new SqlParameter("@i_nombre", empleado.nombre),
                        new SqlParameter("@i_codigo_jefe", (empleado.codigo_jefe>0)?empleado.codigo_jefe:DBNull.Value),
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_update_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;
                        srvResponse.mensaje = row["MENSAJE"].ToString();
                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });

                    }

                    db.Close();
                    srvResponse.success = true;
                    srvResponse.codigo = 200;
                    srvResponse.data = rst;
                    if (rst.Count < 1) { srvResponse.codigo = 404; srvResponse.mensaje = "No Encontrado"; }
                }
            }
            catch (Exception ex)
            {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = 500;
            }
            return new JsonResult(srvResponse);
        }

        // DELETE api/<EmpleadoController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try
            {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@i_codigo", id),
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_delete_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;
                        srvResponse.mensaje = row["MENSAJE"].ToString();
                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });

                    }

                    db.Close();
                    srvResponse.success = true;
                    srvResponse.codigo = 200;
                    srvResponse.data = rst;
                    if (srvResponse.mensaje.Contains("No se puede eliminar")) srvResponse.codigo = 406;

                }
            }
            catch (Exception ex)
            {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = 500;
            }
            return new JsonResult(srvResponse);
        }




        // GET api/<EmpleadoController>/5
        [HttpGet("{id}/Subordinados")]
        public JsonResult GetSubordinado(int id)
        {
            List<plazaEmpleadoModel> rst = new List<plazaEmpleadoModel>();
            responseModel srvResponse = new responseModel();
            try
            {

                using (var db = new DBAccess(connectionString))
                {
                    db.Open();
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@i_codigo_jefe", id),
                    };

                    // Ejecutar y obtener DataTable
                    DataTable result = db.ExecuteStoredProcedure("sp_select_plaza_empleado", parameters);

                    foreach (DataRow row in result.Rows)
                    {
                        int codigoTmp = 0;
                        rst.Add(new plazaEmpleadoModel
                        {
                            codigo = (int.TryParse(row["CODIGO"].ToString(), out codigoTmp)) ? codigoTmp : 0,
                            puesto = row["Puesto"].ToString(),
                            nombre = row["Nombre"].ToString(),
                            codigo_jefe = (int.TryParse(row["codigo_jefe"].ToString(), out codigoTmp)) ? codigoTmp : 0
                        });
                    }

                    db.Close();
                }

                srvResponse.success = true;
                srvResponse.mensaje = "Datos Enncontrados.";
                srvResponse.codigo = 200;
                srvResponse.data = rst;

                if (rst.Count < 1) { srvResponse.codigo = 404; srvResponse.mensaje = "No Encontrado"; }

            }
            catch (Exception ex)
            {
                srvResponse.success = false;
                srvResponse.mensaje = ex.Message;
                srvResponse.codigo = -1;
            }

            return new JsonResult(srvResponse);
        }

    }
}
