using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PerfectMatchBack.Models;

public partial class PerFectMatchContext : DbContext
{
    public PerFectMatchContext()
    {
    }

    public PerFectMatchContext(DbContextOptions<PerFectMatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User__3717C98256EB3AB3");

            entity.ToTable("User");

            entity.HasIndex(e => e.IdAccess, "IX_User_idAccess");

            entity.HasIndex(e => e.IdCity, "IX_User_idCity");

            entity.HasIndex(e => e.IdRole, "IX_User_idRole");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birthDate");
            entity.Property(e => e.CodePay)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdAccess).HasColumnName("idAccess");
            entity.Property(e => e.IdCity).HasColumnName("idCity");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
