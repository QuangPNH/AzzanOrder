using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AzzanOrder.Data.Models
{
    public partial class OrderingAssistSystemContext : DbContext
    {
        public OrderingAssistSystemContext()
        {
        }

        public OrderingAssistSystemContext(DbContextOptions<OrderingAssistSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> Abouts { get; set; } = null!;
        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<ItemCategory> ItemCategories { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MemberVoucher> MemberVouchers { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server = NGUYENMINH; database = OrderingAssistSystem; user = sa; password = 123456;TrustServerCertificate=true;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>(entity =>
            {
                entity.ToTable("About");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Abouts)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_About_Owner");
            });

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

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Employee_Employee");

                entity.HasOne(d => d.Ower)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OwerId)
                    .HasConstraintName("FK_Employee_Owner");

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

            modelBuilder.Entity<MemberVoucher>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.VoucherDetailId });

                entity.ToTable("MemberVoucher");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberVouchers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberVoucher_Member");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.MemberVouchers)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_MemberVoucher_Order");

                entity.HasOne(d => d.VoucherDetail)
                    .WithMany(p => p.MemberVouchers)
                    .HasForeignKey(d => d.VoucherDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberVoucher_VoucherDetail");
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

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Promotion_Employee");
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

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasKey(e => new { e.VoucherDetailId, e.ItemCategoryId });

                entity.ToTable("Voucher");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_ItemCategory");

                entity.HasOne(d => d.VoucherDetail)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.VoucherDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_VoucherDetail");
            });

            modelBuilder.Entity<VoucherDetail>(entity =>
            {
                entity.ToTable("VoucherDetail");
               
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<AzzanOrder.Data.DTO.LoginDTO>? LoginDTO { get; set; }
    }
}
