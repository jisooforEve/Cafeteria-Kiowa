using ProyectoDSI_Avance.Models;
using System.ComponentModel.DataAnnotations;
namespace ProyectoDSI_Avance.Models
{
    public class Tarjeta
    {
        [Key]
        public int id_tarjeta { get; set; }
        public string numero { get; set; }
        public string nip { get; set; }
        public string? nombre_alumno { get; set; }
        public string? matricula { get; set; }
        public string? telefono { get; set; }
        public decimal saldo { get; set; }
        public bool activa { get; set; }
    }
}
