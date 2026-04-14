using ProyectoDSI_Avance.Models;
using System.ComponentModel.DataAnnotations;
namespace ProyectoDSI_Avance.Models
{
    public class Producto
    {
        [Key]
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int? id_proveedor { get; set; }
        public bool activo { get; set; }
    }
}
