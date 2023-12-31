﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PerfectMatchBack.Models;

public partial class PetFectMatchContext : DbContext
{
    public PetFectMatchContext()
    {
    }

    public PetFectMatchContext(DbContextOptions<PetFectMatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<AnimalType> AnimalTypes { get; set; }

    public virtual DbSet<Breed> Breeds { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Publication> Publications { get; set; }
    public virtual DbSet<RecoverPass> RecoverPasses { get; set; }
    public virtual DbSet<ReportPath> ReportPaths { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name= Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.IdAccess).HasName("PK__Access__FF93766616CDF460");

            entity.ToTable("Access");

            entity.Property(e => e.IdAccess).HasColumnName("idAccess");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<AnimalType>(entity =>
        {
            entity.HasKey(e => e.IdAnimalType).HasName("PK__AnimalTy__2F24A3993AE8DA2B");

            entity.ToTable("AnimalType");

            entity.Property(e => e.IdAnimalType).HasColumnName("idAnimalType");
            entity.Property(e => e.AnimalTypeName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("animalTypeName");
        });

        modelBuilder.Entity<Breed>(entity =>
        {
            entity.HasKey(e => e.IdBreed).HasName("PK__Breed__E07BCBB91BCC26AC");

            entity.ToTable("Breed");

            entity.HasIndex(e => e.IdAnimalType, "IX_Breed_idAnimalType");

            entity.Property(e => e.IdBreed).HasColumnName("idBreed");
            entity.Property(e => e.BreedName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("breedName");
            entity.Property(e => e.IdAnimalType).HasColumnName("idAnimalType");

            entity.HasOne(d => d.IdAnimalTypeNavigation).WithMany(p => p.Breeds)
                .HasForeignKey(d => d.IdAnimalType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Breed__idAnimalT__35BCFE0A");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("PK__City__814F31DE7B563306");

            entity.ToTable("City");

            entity.HasIndex(e => e.IdDepartment, "IX_City_idDeparment");

            entity.Property(e => e.IdCity).HasColumnName("idCity");
            entity.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cityName");
            entity.Property(e => e.IdDepartment).HasColumnName("idDepartment");

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdDepartment)
                .HasConstraintName("FK_City_Deparment");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PK__Comment__0370297E9127B084");

            entity.ToTable("Comment");

            entity.HasIndex(e => e.IdCommentFk, "IX_Comment_idCommentFk");

            entity.HasIndex(e => e.IdPublication, "IX_Comment_idPublication");

            entity.HasIndex(e => e.IdUser, "IX_Comment_idUser");

            entity.Property(e => e.IdComment).HasColumnName("idComment");
            entity.Property(e => e.Comment1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.IdCommentFk).HasColumnName("idCommentFk");
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdCommentFkNavigation).WithMany(p => p.InverseIdCommentFkNavigation)
                .HasForeignKey(d => d.IdCommentFk)
                .HasConstraintName("FK__Comment__idComme__36B12243");

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK__Comment__idPubli__37A5467C");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Comment__idUser__38996AB5");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).HasName("PK_Deparment");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender);

            entity.ToTable("Gender");

            entity.Property(e => e.IdGender).HasColumnName("idGender");
            entity.Property(e => e.GenderName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genderName");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {

            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__10A03A1181F8494A");


            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.IdHistorialToken).HasColumnName("idHistorialToken");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("dateCreate");
            entity.Property(e => e.DateExpiration)
                .HasColumnType("datetime")
                .HasColumnName("dateExpiration");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IsActive)
                .HasComputedColumnSql("(case when [dateExpiration]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false)
                .HasColumnName("isActive");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Historial__idUse__29221CFB");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PK_Image1");

            entity.ToTable("Image");

            entity.HasIndex(e => e.IdPublication, "IX_Image_idPublication");

            entity.Property(e => e.IdImage).HasColumnName("idImage");
            entity.Property(e => e.DataImage)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.IdPublication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Image1_Publication");
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.IdMovement).HasName("PK__Movement__5B3BB2F5E4A1C9F0");

            entity.ToTable("Movement");

            entity.HasIndex(e => e.IdBuyer, "IX_Movement_idBuyer");

            entity.HasIndex(e => e.IdPublication, "IX_Movement_idPublication");

            entity.HasIndex(e => e.IdSeller, "IX_Movement_idSeller");

            entity.Property(e => e.IdMovement).HasColumnName("idMovement");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdBuyer).HasColumnName("idBuyer");
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.IdSeller).HasColumnName("idSeller");

            entity.HasOne(d => d.IdBuyerNavigation).WithMany(p => p.MovementIdBuyerNavigations)
                .HasForeignKey(d => d.IdBuyer)
                .HasConstraintName("FK__Movement__idBuye__398D8EEE");

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Movements)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK__Movement__idPubl__3A81B327");

            entity.HasOne(d => d.IdSellerNavigation).WithMany(p => p.MovementIdSellerNavigations)
                .HasForeignKey(d => d.IdSeller)
                .HasConstraintName("FK__Movement__idSell__3B75D760");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification).HasName("PK__Notifica__C24D00C423C065BF");

            entity.ToTable("Notification");

            entity.HasIndex(e => e.IdMovement, "IX_Notification_idUser");

            entity.Property(e => e.IdNotification).HasColumnName("idNotification");
            entity.Property(e => e.AccessLink)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("accessLink");
            entity.Property(e => e.IdMovement).HasColumnName("idMovement");
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IdUserFK).HasColumnName("idUserFK");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.TypeNotification).HasColumnName("typeNotification");

            entity.HasOne(d => d.IdMovementNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdMovement)
                .HasConstraintName("FK_Notification_Movement");
            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_Notification_Publication");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.NotificationIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Notification_User");

            entity.HasOne(d => d.IdUserFKNavigation).WithMany(p => p.NotificationIdUserFkNavigations)
                .HasForeignKey(d => d.IdUserFK)
                .HasConstraintName("FK__Notificat__idUse__3C69FB99");
        });
        modelBuilder.Entity<Publication>(entity =>
        {
            entity.HasKey(e => e.IdPublication).HasName("PK__Publicat__ECEE91EED6DB5C2C");

            entity.ToTable("Publication");

            entity.HasIndex(e => e.IdAnimalType, "IX_Publication_idAnimalType");

            entity.HasIndex(e => e.IdBreed, "IX_Publication_idBreed");

            entity.HasIndex(e => e.IdCity, "IX_Publication_idCity");

            entity.HasIndex(e => e.IdGender, "IX_Publication_idGender");

            entity.HasIndex(e => e.IdOwner, "IX_Publication_idOwner");

            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AnimalName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("animalName");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdAnimalType).HasColumnName("idAnimalType");
            entity.Property(e => e.IdBreed).HasColumnName("idBreed");
            entity.Property(e => e.IdCity).HasColumnName("idCity");
            entity.Property(e => e.IdGender).HasColumnName("idGender");
            entity.Property(e => e.IdOwner).HasColumnName("idOwner");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.IdAnimalTypeNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdAnimalType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Publicati__idAni__3D5E1FD2");

            entity.HasOne(d => d.IdBreedNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdBreed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Publicati__idBre__3E52440B");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK__Publicati__idCit__3F466844");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publication_Gender");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK__Publicati__idOwn__403A8C7D");
        });


        modelBuilder.Entity<RecoverPass>(entity =>
        {
            entity.HasKey(e => e.IdRecover);

            entity.ToTable("RecoverPass");

            entity.Property(e => e.IdRecover).HasColumnName("idRecover");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Token)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RecoverPasses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecoverPass_User");
        });
        modelBuilder.Entity<ReportPath>(entity =>
        {
            entity.HasKey(e => e.IdReport);

            entity.ToTable("ReportPath");

            entity.Property(e => e.Path)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReportName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__E5045C54775D45A0");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User__3717C98256EB3AB3");

            entity.ToTable("User");

            entity.HasIndex(e => e.IdAccess, "IX_User_idAccess");

            entity.HasIndex(e => e.IdCity, "IX_User_idCity");

            entity.HasIndex(e => e.IdRole, "IX_User_idRole");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.AccountDate).HasColumnType("date");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birthDate");
            entity.Property(e => e.CodePay)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
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

            entity.HasOne(d => d.IdAccessNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAccess)
                .HasConstraintName("FK__User__idAccess__412EB0B6");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK__User__idCity__4222D4EF");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__idRole__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
