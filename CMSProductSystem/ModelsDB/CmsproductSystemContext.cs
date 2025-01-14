﻿using System;
using System.Collections.Generic;
using CMSProductSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CMSProductSystem.ModelsDB;

public partial class CmsproductSystemContext : DbContext
{
    public CmsproductSystemContext()
    {
    }

    public CmsproductSystemContext(DbContextOptions<CmsproductSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Proizvod> Proizvod { get; set; }
    public virtual DbSet<Kategorija> Kategorija { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CMSProductSystem;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });


        //modelBuilder.Entity<Proizvod>(entity =>
        //{
        //    entity.HasKey(e => e.ID)
        //        .HasName("PK__Proizvod__3767918544DA94EB");

        //    entity.ToTable("Proizvod");

        //    entity.Property(e => e.Naziv).HasMaxLength(40);

        //    entity.Property(e => e.Opis).HasMaxLength(100);

        //    entity.Property(e => e.Kolicina).HasMaxLength(20);

        //    entity.Property(e => e.Cijena).HasMaxLength(30);


        //    entity.HasOne(d => d.Category).WithMany(p => p.Product).HasForeignKey(d => d.Category.ID);

        //});

        //modelBuilder.Entity<Kategorija>(entity =>
        //{
        //    entity.HasKey(e => e.ID)
        //        .HasName("PK__Kategorija__CA99D1B4F3E2476D");

        //    entity.ToTable("Kategorija");

        //    entity.Property(e => e.Naziv).HasMaxLength(30);
        //});

        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Kategori__3214EC270907479B");

            entity.ToTable("Kategorija");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Naziv).HasMaxLength(30);
        });

        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Proizvod__3214EC27F73712F6");

            entity.ToTable("Proizvod");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.Cijena).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Naziv).HasMaxLength(30);
            entity.Property(e => e.Opis).HasMaxLength(100);
            entity.Property(e => e.Slika).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryID)
                .HasConstraintName("FK_Proizvod_Kategorija_CategoryID");
        });


        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
