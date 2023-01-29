using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Finance;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Storekeeper;


namespace NimbRepository.DbContexts;

public partial class NimbDbContext : DbContext
{
    public NimbDbContext()
    {
    }

    public NimbDbContext(DbContextOptions<NimbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientsNotReg> ClientsNotRegs { get; set; }

    public virtual DbSet<ClientsReg> ClientsRegs { get; set; }

    public virtual DbSet<CompaniesReg> CompaniesRegs { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();

        builder.AddJsonFile("configuration.json", reloadOnChange: true, optional: false);

        var config = builder.Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("NimbDb"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC0762F8F29F");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
            entity.Property(e => e.PatronymicName).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientsNotReg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientsN__3214EC0780B67A27");

            entity.ToTable("ClientsNotReg");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Goods).WithMany(p => p.ClientsNotRegs)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientsNo__Goods__52593CB8");
        });

        modelBuilder.Entity<ClientsReg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientsR__3214EC0700A7B8CB");

            entity.ToTable("ClientsReg");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsRegs)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientsRe__Clien__534D60F1");

            entity.HasOne(d => d.Goods).WithMany(p => p.ClientsRegs)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientsRe__Goods__5441852A");
        });

        modelBuilder.Entity<CompaniesReg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC07CE1505B0");

            entity.ToTable("CompaniesReg");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CompaniesRegs)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Companies__Compa__5535A963");

            entity.HasOne(d => d.Goods).WithMany(p => p.CompaniesRegs)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Companies__Goods__5629CD9C");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC07C585941A");

            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Goods__3214EC07FD53D51A");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DateAdd).HasColumnType("datetime");
            entity.Property(e => e.DateSold).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price)
                .HasDefaultValueSql("((0.0))")
                .HasColumnType("money");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Goods)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goods__SupplierI__5165187F");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07BE0A1297");

            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07460F8546");

            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
            entity.Property(e => e.PatronymicName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Status).HasColumnType("datetime");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wallet__3214EC07FAA5E578");

            entity.ToTable("Wallet");

            entity.Property(e => e.TotalSumSeller)
                .HasDefaultValueSql("((0.0))")
                .HasColumnType("money");
            entity.Property(e => e.TotalSumStorekeeper)
                .HasDefaultValueSql("((0.0))")
                .HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
