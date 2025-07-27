namespace BACKSERVICES.Models
{
    public class plazaEmpleadoModel
    {
        public int codigo { get; set; }
        public required string puesto { get; set; }
        public required string nombre { get; set; }
        public int codigo_jefe { get; set; }
    }
}
