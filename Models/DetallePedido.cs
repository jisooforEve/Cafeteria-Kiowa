using ProyectoDSI_Avance.Models;
using System.ComponentModel.DataAnnotations;
namespace ProyectoDSI_Avance.Models
{
    public class DetallePedido
    {
        [Key]
        public int id_detalle { get; set; }
        public int id_pedido { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public string personalizacion { get; set; }

        public Producto Producto { get; set; }
        public Pedido Pedido { get; set; }
    }
}
