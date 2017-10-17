namespace SupportSaleAndWarehouseVer1._0.FrameWork
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WareHouseDbContext : DbContext
    {
        public WareHouseDbContext()
            : base("name=WareHouseDbContext")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ExportBill> ExportBills { get; set; }
        public virtual DbSet<ImportBill> ImportBills { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<WareHouse> WareHouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.ExportBills)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.IDClient);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Company)
                .HasForeignKey(e => e.IDCompany);

            modelBuilder.Entity<ExportBill>()
                .HasMany(e => e.ProductDetails)
                .WithOptional(e => e.ExportBill)
                .HasForeignKey(e => e.IDExBill);

            modelBuilder.Entity<ImportBill>()
                .HasMany(e => e.ProductDetails)
                .WithOptional(e => e.ImportBill)
                .HasForeignKey(e => e.IDImBill);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IDProduct);

            modelBuilder.Entity<WareHouse>()
                .HasMany(e => e.ExportBills)
                .WithOptional(e => e.WareHouse)
                .HasForeignKey(e => e.IDWareHouse);

            modelBuilder.Entity<WareHouse>()
                .HasMany(e => e.ImportBills)
                .WithOptional(e => e.WareHouse)
                .HasForeignKey(e => e.IDWareHouse);
        }
    }
}
