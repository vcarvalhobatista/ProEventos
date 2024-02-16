using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProEventos.Domain;

namespace ProEventos.Persistence.Context
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {

        }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        public DbSet<Palestrante> Palestrantes { get; set; }

        public DbSet<Lote> Lotes { get; set; }

        public DbSet<RedeSocial> RedeSocials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Associação de Many to Many
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            modelBuilder.Entity<Evento>()
            .HasMany(evento => evento.RedesSociais)
            .WithOne(redeSocial => redeSocial.Evento)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
            .HasMany(palestrante => palestrante.RedesSociais)
            .WithOne(redeSocial => redeSocial.Palestrante)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}