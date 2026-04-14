using Microsoft.EntityFrameworkCore;
using ProyectoDSI_Avance.Models;

namespace ProyectoDSI_Avance.Data
{
    public class KCafeteriaContext : DbContext
    {
        public KCafeteriaContext(DbContextOptions<KCafeteriaContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.id_producto);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.id_pedido);
        }

        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
    
}
