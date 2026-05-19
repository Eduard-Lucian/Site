using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Destinatie> Destinatii { get; set; }
        public DbSet<Hotel> Hoteluri { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Facilitate> Facilitati { get; set; }
        public DbSet<Activitate> Activitati { get; set; }
        public DbSet<Inchirieri_Masini> Inchirieri_Masini { get; set; }
        public DbSet<Zbor> Zboruri { get; set; }
        public DbSet<Recenzie> Recenzii { get; set; }
        public DbSet<Rezervare> Rezervari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OBLIGATORIU: Păstrăm configurările de bază pentru Identity
            base.OnModelCreating(modelBuilder);

            // 1. Configurări precizie zecimale (Evită erorile de trunchiere a prețurilor)
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Pret).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PretBaza).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Rating).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Activitate>(entity =>
            {
                entity.ToTable("Activitati");
                entity.Property(e => e.Pret_pe_persoana).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Rezervare>(entity =>
            {
                entity.Property(e => e.PretTotal).HasColumnType("decimal(18,2)");
            });

            // =========================================================
            // 2. REZOLVAREA TEMEI 2: FLUENT API PENTRU RELAȚII (Fără Shadow Properties)
            // =========================================================

            // Regulă: Nu poți șterge o Destinație dacă are Activități legate de ea.
            modelBuilder.Entity<Activitate>()
                .HasOne(a => a.Destinatie)
                .WithMany(d => d.Activitati) // <-- Legătura explicită către colecția din Destinatie
                .HasForeignKey(a => a.DestinatieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Regulă: Nu poți șterge o Destinație dacă are Hoteluri legate de ea.
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Destinatie)
                .WithMany(d => d.Hoteluri) // <-- Legătura explicită către colecția din Destinatie
                .HasForeignKey(h => h.DestinatieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Regulă: Nu poți șterge o Destinație dacă are Zboruri legate de ea.
            modelBuilder.Entity<Zbor>()
                .HasOne(z => z.Destinatie)
                .WithMany(d => d.Zboruri) // <-- Legătura explicită către colecția din Destinatie
                .HasForeignKey(z => z.DestinatieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Regulă pentru Rezervări: 
            // Dacă un Utilizator (User) își șterge contul, i se șterg și rezervările (Cascade).
            modelBuilder.Entity<Rezervare>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rezervari)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}