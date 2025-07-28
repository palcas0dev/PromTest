using System.Collections.Generic;

namespace FRONT.Models
{
    public class PlazaEmpleadoModel
    {
        public int codigo { get; set; }
        public string puesto { get; set; }
        public string nombre { get; set; }
        public int codigo_jefe { get; set; }
    }

    public class ArbolEmpleados
    {
        public int id { get; set; }
        public string text { get; set; }
        public int codigo_jefe { get; set; }
        public List<ArbolEmpleados> children { get; set; } = new List<ArbolEmpleados>();
        public ArbolEmpleados() { }
        public ArbolEmpleados(PlazaEmpleadoModel emp) {
            id = emp.codigo;
            text = emp.puesto + " :: " + emp.nombre;
            codigo_jefe = emp.codigo_jefe;
            children = new List<ArbolEmpleados>();
        }
    }
}