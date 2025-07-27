namespace BACKSERVICES.Models
{
    public class responseModel
    {
        public bool success { get; set; }
        public int codigo { get; set; }
        public string mensaje { get; set; }
        public object data { get; set; }

        public responseModel() { 
            success = false;
            codigo = 0;
            mensaje = string.Empty;
        }
    }
}
