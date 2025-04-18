using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApiPolizas.Models;

public partial class PolizasDBContext : DbContext
{
    public PolizasDBContext() { }

    public PolizasDBContext(DbContextOptions<PolizasDBContext> options)
        : base(options) { }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }
    public virtual DbSet<TipoPoliza> TiposPoliza { get; set; }
    public virtual DbSet<EstadoPoliza> EstadosPoliza { get; set; }
    public virtual DbSet<Cobertura> Coberturas { get; set; }
    public virtual DbSet<Poliza> Polizas { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Configuracion> Configuracion { get; set; }
    public DbSet<PolizaFlat> PolizaFlat { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CedulaAsegurado);
            entity.Property(e => e.CedulaAsegurado).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrimerApellido).HasMaxLength(100);
            entity.Property(e => e.SegundoApellido).HasMaxLength(100);
            entity.Property(e => e.TipoPersona).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).HasMaxLength(50) ;
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity.HasKey(e => e.Clave);
            entity.Property(e => e.Valor).HasMaxLength(256);
        });

        modelBuilder.Entity<TipoPoliza>(entity =>
        {
            entity.HasKey(e => e.TipoPolizaId);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });
        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.HasKey(e => e.CoberturaId);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<PolizaFlat>().HasNoKey();

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.NumeroPoliza);
            entity.Property(e => e.NumeroPoliza).HasMaxLength(50);
            entity.Property(e => e.MontoAsegurado).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Prima).HasColumnType("decimal(18,2)");
            entity.Property(e => e.FechaVencimiento).HasColumnType("date");
            entity.Property(e => e.FechaEmision).HasColumnType("date");
            entity.Property(e => e.FechaInclusion).HasColumnType("date");
            entity.Property(e => e.Periodo).HasColumnType("date");

            entity.HasOne(p => p.Cliente)
                 .WithMany(c => c.Polizas)
                  .HasForeignKey(p => p.CedulaAsegurado)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Cobertura)
                .WithMany(c => c.Polizas)
                .HasForeignKey(p => p.CoberturaId)
                .OnDelete(DeleteBehavior.Restrict);

             entity.HasOne(p => p.TipoPoliza)
                 .WithMany(c => c.Polizas)
                 .HasForeignKey(p => p.TipoPolizaId)
                 .OnDelete(DeleteBehavior.Restrict);


             entity.HasOne(p => p.EstadoPoliza)
                 .WithMany(c => c.Polizas)
                 .HasForeignKey(p => p.EstadoPolizaId)
                 .OnDelete(DeleteBehavior.Restrict);

             entity.HasOne(p => p.Aseguradora)
                 .WithMany(c => c.Polizas)
                 .HasForeignKey(p => p.AseguradoraId)
                 .OnDelete(DeleteBehavior.Restrict);



        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
