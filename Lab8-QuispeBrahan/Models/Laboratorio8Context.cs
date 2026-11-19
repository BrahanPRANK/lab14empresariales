using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab8_QuispeBrahan.Models;

public partial class Laboratorio8Context : DbContext
{
    public Laboratorio8Context()
    {
    }

    public Laboratorio8Context(DbContextOptions<Laboratorio8Context> opciones)
        : base(opciones)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        modelo.Entity<Client>(entidad =>
        {
            entidad.HasKey(dato => dato.Clientid).HasName("clients_pkey");

            entidad.ToTable("clients");

            entidad.Property(dato => dato.Clientid).HasColumnName("clientid");
            entidad.Property(dato => dato.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entidad.Property(dato => dato.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelo.Entity<Order>(entidad =>
        {
            entidad.HasKey(dato => dato.Orderid).HasName("orders_pkey");

            entidad.ToTable("orders");

            entidad.Property(dato => dato.Orderid).HasColumnName("orderid");
            entidad.Property(dato => dato.Clientid).HasColumnName("clientid");
            entidad.Property(dato => dato.Orderdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");

            entidad.HasOne(detalle => detalle.Client).WithMany(principal => principal.Orders)
                .HasForeignKey(detalle => detalle.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_clientid_fkey");
        });

        modelo.Entity<Orderdetail>(entidad =>
        {
            entidad.HasKey(dato => dato.Orderdetailid).HasName("orderdetails_pkey");

            entidad.ToTable("orderdetails");

            entidad.Property(dato => dato.Orderdetailid).HasColumnName("orderdetailid");
            entidad.Property(dato => dato.Orderid).HasColumnName("orderid");
            entidad.Property(dato => dato.Productid).HasColumnName("productid");
            entidad.Property(dato => dato.Quantity).HasColumnName("quantity");

            entidad.HasOne(detalle => detalle.Order).WithMany(principal => principal.Orderdetails)
                .HasForeignKey(detalle => detalle.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_orderid_fkey");

            entidad.HasOne(detalle => detalle.Product).WithMany(principal => principal.Orderdetails)
                .HasForeignKey(detalle => detalle.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_productid_fkey");
        });

        modelo.Entity<Product>(entidad =>
        {
            entidad.HasKey(dato => dato.Productid).HasName("products_pkey");

            entidad.ToTable("products");

            entidad.Property(dato => dato.Productid).HasColumnName("productid");
            entidad.Property(dato => dato.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entidad.Property(dato => dato.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entidad.Property(dato => dato.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelo);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelo);
}
