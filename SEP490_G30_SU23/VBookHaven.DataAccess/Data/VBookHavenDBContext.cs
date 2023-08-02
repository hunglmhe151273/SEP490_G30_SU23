using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using Microsoft.Extensions.Configuration;

namespace VBookHaven.DataAccess.Data;

public partial class VBookHavenDBContext : IdentityDbContext<IdentityUser>
{
    public VBookHavenDBContext()
    {
    }

    public VBookHavenDBContext(DbContextOptions<VBookHavenDBContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Exchange> Exchanges { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShippingInfo> ShippingInfos { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Stationery> Stationeries { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<PurchasePaymentHistory> PurchasePaymentHistories { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Activity Log");

            entity.ToTable("ActivityLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Action).HasMaxLength(30);
            entity.Property(e => e.ActivityDetails)
                .HasColumnType("ntext")
                .HasColumnName("Activity Details");
            entity.Property(e => e.ActivitySummary)
                .HasMaxLength(100)
                .HasColumnName("Activity Summary");
            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.ItemsId).HasMaxLength(30);
            entity.Property(e => e.TableName).HasMaxLength(50);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.Actor).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK_ActivityLog_Staff");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.AuthorName).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Book");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.Publisher).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithOne(p => p.Book)
                .HasForeignKey<Book>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Product");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Book_Author_Author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Book_Author_Book"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("Book_Author");
                    });
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.ProductId });

            entity.ToTable("CartDetail");

            entity.HasOne(d => d.Customer).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_Customer");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.Property(e => e.Image).HasMaxLength(150);
            entity.HasIndex(e => e.AccountId, "IX_Customer").IsUnique();
            entity.Property(e => e.DOB)
               .HasColumnType("date")
               .HasColumnName("DOB");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.HasOne(d => d.DefaultShippingInfo).WithMany(p => p.Customers)
               .HasForeignKey(d => d.DefaultShippingInfoId)
               .HasConstraintName("FK_Customer_ShippingInfo");

        });

        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.HasKey(e => e.ExchangeId).HasName("PK__Exchange__72E6008B8FB6C369");

            entity.ToTable("Exchange");

            entity.HasIndex(e => new { e.ProductId, e.BaseProductId }, "UC_Product_BaseProduct").IsUnique();

            entity.HasOne(d => d.BaseProduct).WithMany(p => p.ExchangeBaseProducts)
                .HasForeignKey(d => d.BaseProductId)
                .HasConstraintName("FK_Exchange_Product1");

            entity.HasOne(d => d.Product).WithMany(p => p.ExchangeProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Exchange_Product");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ImageName).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Image_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            //entity.Property(e => e.ShippingInfo).HasMaxLength(300);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.ShipAddress).HasMaxLength(300);
            entity.Property(e => e.Phone).HasMaxLength(10).IsFixedLength();
            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("OrderDetail");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Barcode, "UK_Product").IsUnique();

            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Weight).HasMaxLength(50);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(7, 0)");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_Product_SubCategory");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(15, 0)");

            entity.HasOne(d => d.Staff).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_PurchaseOrder_Staff");
           
            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_PurchaseOrder_Supplier");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseOrderId, e.ProductId }).HasName("PK_PurchaseOrderDetail_1");

            entity.ToTable("PurchaseOrderDetail");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(7, 0)");
            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_Product");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
        });
        modelBuilder.Entity<PurchasePaymentHistory>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("PurchasePaymentHistory");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(15, 0)");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_ID");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchasePaymentHistories)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK_PurchasePaymentHistory_PurchaseOrder");
            entity.HasOne(d => d.Staff).WithMany(p => p.PurchasePaymentHistories)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_PurchasePaymentHistory_Staff");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Review_Product");
        });

        modelBuilder.Entity<ShippingInfo>(entity =>
        {
            entity.HasKey(e => e.ShipInfoId);

            entity.ToTable("ShippingInfo");
            entity.HasIndex(e => e.CustomerId, "IX_ShippingInfo_CustomerId");
            entity.Property(e => e.CustomerName).IsRequired(false);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ShipAddress).HasMaxLength(200);
            entity.Property(e => e.ProvinceCode).IsRequired(false);
            entity.Property(e => e.DistrictCode).IsRequired(false);
            entity.Property(e => e.WardCode).IsRequired(false);
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(50);
            entity.HasOne(d => d.Customer).WithMany(p => p.ShippingInfos)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ShippingInfo_Customer");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasIndex(e => e.AccountId, "IX_Staff").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FullName).HasMaxLength(70);
            entity.Property(e => e.IdCard)
                .HasMaxLength(12)
                .IsFixedLength();
            entity.Property(e => e.Image).HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Stationery>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Other_1");

            entity.ToTable("Stationery");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.Origin).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithOne(p => p.Stationery)
                .HasForeignKey<Stationery>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stationery_Product");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Address)
                .HasMaxLength(150).IsRequired(false);
			entity.Property(e => e.ProvinceCode).IsRequired(false);
			entity.Property(e => e.DistrictCode).IsRequired(false);
            entity.Property(e => e.WardCode).IsRequired(false);
			entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.SupplierName).HasMaxLength(300);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
