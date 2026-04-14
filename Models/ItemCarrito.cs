using System.ComponentModel.DataAnnotations;

namespace ProyectoDSI_Avance.Models
{
    public class ItemCarrito
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Personalizacion { get; set; }
    }
}
