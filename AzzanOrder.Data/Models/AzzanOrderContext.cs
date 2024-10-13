using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AzzanOrder.Data.DTO;

namespace AzzanOrder.Data.Models
{
    public partial class AzzanOrderContext : DbContext
    {
        public AzzanOrderContext()
        {
        }

        public AzzanOrderContext(DbContextOptions<AzzanOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<ItemCategory> ItemCategories { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; } = null!;

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
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Employee_Role");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Feedback_Member");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.ToTable("ItemCategory");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.HasKey(e => new { e.MenuItemId, e.ItemCategoryId });

                entity.ToTable("MenuCategory");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.MenuCategories)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuCategory_ItemCategory");

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.MenuCategories)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuCategory_MenuItem");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItem");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_MenuItem_Employee");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Notification_Employee");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Notification_Member");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Order_Member");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_Order_Table");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MenuItemId)
                    .HasConstraintName("FK_OrderDetail_MenuItem");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_Owner_Bank");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("Table");

                entity.Property(e => e.Qr).HasColumnName("QR");
            });

            modelBuilder.Entity<VoucherDetail>(entity =>
            {
                entity.ToTable("VoucherDetail");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasMany(d => d.ItemCategories)
                    .WithMany(p => p.VocherDetails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Voucher",
                        l => l.HasOne<ItemCategory>().WithMany().HasForeignKey("ItemCategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Voucher_ItemCategory"),
                        r => r.HasOne<VoucherDetail>().WithMany().HasForeignKey("VocherDetailId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Voucher_VoucherDetail"),
                        j =>
                        {
                            j.HasKey("VocherDetailId", "ItemCategoryId");

                            j.ToTable("Voucher");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<AzzanOrder.Data.DTO.LoginDTO>? LoginDTO { get; set; }
    }
}
