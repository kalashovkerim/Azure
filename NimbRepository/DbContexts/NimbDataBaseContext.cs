
using Microsoft.EntityFrameworkCore;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;
using Microsoft.Extensions.Configuration;

namespace NimbRepository.DbContexts;

public partial class NimbDataBaseContext : DbContext
{
    public NimbDataBaseContext()
    {
    }

    public NimbDataBaseContext(DbContextOptions<NimbDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<GoodsSold> GoodsSolds { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();

        builder.AddJsonFile("configuration.json", reloadOnChange: true, optional: false);

        var config = builder.Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("SNimbDb"));

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07A33215E0");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
            entity.Property(e => e.PatronymicName).HasMaxLength(50);
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Goods__3214EC078921883C");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DateAdd).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PurchasePrice).HasColumnType("money");
            entity.Property(e => e.Rate).HasColumnType("money");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Goods)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goods__SupplierI__45F365D3");
        });

        modelBuilder.Entity<GoodsSold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsSol__3214EC0773059463");

            entity.ToTable("GoodsSold");

            entity.Property(e => e.Count).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.DateSold).HasColumnType("datetime");

            entity.HasOne(d => d.Goods).WithMany(p => p.GoodsSolds)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GoodsSold__Goods__46E78A0C");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07E75FDFB4");

            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E9C6E28C");

            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(30);
            entity.Property(e => e.PatronymicName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Status).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
