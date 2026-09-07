using ArriendosApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ArriendosApi.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {
        }
        public DbSet<Edificio> Edificios{ get; set; }
        public DbSet<Inmueble> Inmuebles{ get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<CobroMensual> CobrosMensuales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Edificio>(entity=>
            {
                entity.ToTable("Edificio");
                entity.HasKey(e => e.IdEdificio);
            }
            );

            modelBuilder.Entity<Inmueble>(en =>
            {
                en.ToTable("Inmueble");
                en.HasKey(e => e.IdInmueble);
            }
            );
            modelBuilder.Entity<Inquilino>(entity =>
            {
                entity.ToTable("Inquilino");
                entity.HasKey(e => e.IdInquilino);
            }  
            );
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");
                entity.HasKey(e => e.IdContrato);
            });

            modelBuilder.Entity<CobroMensual>(entity =>
            {
                entity.ToTable("CobroMensual");
                entity.HasKey(e => e.IdCobro);
            });

        }
    }
}
