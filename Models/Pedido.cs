using System;
using ProyectoDSI_Avance.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSI_Avance.Models
{
    public class Pedido
    {
        [Key]
        public int id_pedido { get; set; }
        public string folio { get; set; }
        public DateTime fecha_hora { get; set; }
        public decimal total { get; set; }

        public List<DetallePedido> Detalles { get; set; }
    }
}
