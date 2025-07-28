using FRONT.Clases;
using FRONT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiCall _apiService = new ApiCall();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> obtenerEmpleados() {
            responseModel objSerialized = new responseModel();
            try {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado", "GET", "", null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex) {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public async Task<JsonResult> obtenerEmpleado(int Id)
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado/"+Id.ToString(), "GET", "", null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public async Task<JsonResult> nuevoEmpleado(PlazaEmpleadoModel empleado)
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado", "POST", JsonConvert.SerializeObject(empleado), null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public async Task<JsonResult> actualizarEmpleado(PlazaEmpleadoModel empleado)
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado/"+empleado.codigo.ToString(), "PUT", JsonConvert.SerializeObject(empleado), null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public async Task<JsonResult> borrarEmpleado(int Id)
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado/" + Id.ToString(), "DELETE", "", null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public async Task<JsonResult> obtenerSubordinados(int Id)
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado/"+Id.ToString()+"/Subordinados", "GET", "", null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());
                objSerialized.data = objEmpleados;
            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public async Task<JsonResult> obtenerArbol()
        {
            responseModel objSerialized = new responseModel();
            try
            {
                var apiResponse = await _apiService.callApiJsonAsync("https://localhost:44367/api/Empleado", "GET", "", null);
                objSerialized = JsonConvert.DeserializeObject<responseModel>(apiResponse);
                var objEmpleados = JsonConvert.DeserializeObject<List<PlazaEmpleadoModel>>(objSerialized.data.ToString());

                objEmpleados = objEmpleados.OrderBy(x => x.codigo_jefe).ToList();
                objSerialized.data = ConstruirArbol(objEmpleados);

            }
            catch (Exception ex)
            {
                objSerialized.success = false;
                objSerialized.mensaje = "Error de Comunicacion con el Servicio.";
                objSerialized.codigo = 500;

            }
            return new JsonResult { Data = objSerialized, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private static List<ArbolEmpleados> ConstruirArbol(List<PlazaEmpleadoModel> empleados)
        {
            // Diccionario rápido para buscar por ID
            var diccionario = empleados.ToDictionary(e => e.codigo, e => new ArbolEmpleados(e));

            List<ArbolEmpleados> raices = new List<ArbolEmpleados>();

            foreach (var empleado in empleados)
            {
                if (empleado.codigo_jefe> 0)
                {
                    // Obtener jefe y agregar como subordinado
                    if (diccionario.TryGetValue(empleado.codigo_jefe, out ArbolEmpleados jefe))
                    {
                        jefe.children.Add(diccionario[empleado.codigo]);
                    }
                }
                else
                {
                    // Es un jefe sin superior (raíz del árbol)
                    raices.Add(diccionario[empleado.codigo]);
                }
            }

            return raices; // puede haber más de una raíz si hay varios jefes sin jefe
        }
    }

}