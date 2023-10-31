using System;
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

    public virtual DbSet<ReportPath> ReportPaths { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.IdAccess).HasName("PK_Access_FF93766616CDF460");

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
            entity.HasKey(e => e.IdAnimalType).HasName("PK_AnimalTy_2F24A3993AE8DA2B");

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
            entity.HasKey(e => e.IdBreed).HasName("PK_Breed_E07BCBB91BCC26AC");

            entity.ToTable("Breed");

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
                .HasConstraintName("FK_BreedidAnimalT_35BCFE0A");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("PK_City_814F31DE7B563306");

            entity.ToTable("City");

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
            entity.HasKey(e => e.IdComment).HasName("PK_Comment_0370297E9127B084");

            entity.ToTable("Comment");

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
                .HasConstraintName("FK_CommentidComme_36B12243");

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_CommentidPubli_37A5467C");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_CommentidUser_38996AB5");
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
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__10A03A116524DEDB");

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
            entity.HasKey(e => e.IdMovement).HasName("PK_Movement_5B3BB2F5E4A1C9F0");

            entity.ToTable("Movement");

            entity.Property(e => e.IdMovement).HasColumnName("idMovement");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdBuyer).HasColumnName("idBuyer");
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.IdSeller).HasColumnName("idSeller");

            entity.HasOne(d => d.IdBuyerNavigation).WithMany(p => p.MovementIdBuyerNavigations)
                .HasForeignKey(d => d.IdBuyer)
                .HasConstraintName("FK_MovementidBuye_398D8EEE");

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.Movements)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_MovementidPubl_3A81B327");

            entity.HasOne(d => d.IdSellerNavigation).WithMany(p => p.MovementIdSellerNavigations)
                .HasForeignKey(d => d.IdSeller)
                .HasConstraintName("FK_MovementidSell_3B75D760");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotifacation).HasName("PK_Notifica_C24D00C423C065BF");

            entity.ToTable("Notification");

            entity.Property(e => e.IdNotifacation).HasColumnName("idNotifacation");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_NotificatidUse_3C69FB99");
        });

        modelBuilder.Entity<Publication>(entity =>
        {
            entity.HasKey(e => e.IdPublication).HasName("PK_Publicat_ECEE91EED6DB5C2C");

            entity.ToTable("Publication");

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
                .HasConstraintName("FK_PublicatiidAni_3D5E1FD2");

            entity.HasOne(d => d.IdBreedNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdBreed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PublicatiidBre_3E52440B");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK_PublicatiidCit_3F466844");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publication_Gender");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_PublicatiidOwn_403A8C7D");
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
            entity.HasKey(e => e.IdRole).HasName("PK_Role_E5045C54775D45A0");

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
            entity.HasKey(e => e.IdUser).HasName("PK_User_3717C98256EB3AB3");

            entity.ToTable("User");

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
                .HasConstraintName("FK_UseridAccess_412EB0B6");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK_UseridCity_4222D4EF");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UseridRole_4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
