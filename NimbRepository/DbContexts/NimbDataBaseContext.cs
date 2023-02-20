
using Microsoft.EntityFrameworkCore;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=NimbDataBase.mssql.somee.com;packet size=4096;user id=kerimkalash_SQLLogin_1;pwd=tvqlktet83;data source=NimbDataBase.mssql.somee.com;Encrypt=False;persist security info=False;initial catalog=NimbDataBase;");

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
