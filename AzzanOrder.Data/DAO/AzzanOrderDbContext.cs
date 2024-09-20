using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;

namespace AzzanOrder.Data.DAO
{
    public class AzzanOrderDbContext : DbContext
    {
        public AzzanOrderDbContext()
        {
        }
        public AzzanOrderDbContext(DbContextOptions<AzzanOrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Feedback> Feedback { get; set; } 
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<MenuCategory> MenuCategory { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VNPay> VNPay { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<VoucherDetail> VoucherDetail { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyCnn");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCategory>()
                .HasKey(od => new { od.MenuId, od.CategoryId });
        }

    }
}
