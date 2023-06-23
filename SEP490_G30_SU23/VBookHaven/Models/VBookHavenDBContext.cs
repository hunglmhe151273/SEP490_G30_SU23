using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

public partial class VBookHavenDBContext : DbContext
{
    public VBookHavenDBContext()
    {
    }

    public VBookHavenDBContext(DbContextOptions<VBookHavenDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShippingInfo> ShippingInfos { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Stationery> Stationeries { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.InverseCreator).HasConstraintName("FK_Account_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.InverseEditor).HasConstraintName("FK_Account_Account_Edit");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts).HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.AuthorCreators).HasConstraintName("FK_Author_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.AuthorEditors).HasConstraintName("FK_Author_Account_Edit");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.Book)
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

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.CategoryCreators).HasConstraintName("FK_Category_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.CategoryEditors).HasConstraintName("FK_Category_Account_Edit");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Account).WithOne(p => p.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Account");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.ImageCreators).HasConstraintName("FK_Image_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.ImageEditors).HasConstraintName("FK_Image_Account_Edit");

            entity.HasOne(d => d.Product).WithMany(p => p.Images).HasConstraintName("FK_Image_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.OrderCreators).HasConstraintName("FK_Order_Account_Create");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Editor).WithMany(p => p.OrderEditors).HasConstraintName("FK_Order_Account_Edit");

            entity.HasOne(d => d.ShippingInfo).WithMany(p => p.Orders).HasConstraintName("FK_Order_ShippingInfo");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.ProductCreators).HasConstraintName("FK_Product_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.ProductEditors).HasConstraintName("FK_Product_Account_Edit");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products).HasConstraintName("FK_Product_SubCategory");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.PurchaseOrderCreators).HasConstraintName("FK_PurchaseOrder_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.PurchaseOrderEditors).HasConstraintName("FK_PurchaseOrder_Account_Edit");

            entity.HasOne(d => d.Staff).WithMany(p => p.PurchaseOrders).HasConstraintName("FK_PurchaseOrder_Staff");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders).HasConstraintName("FK_PurchaseOrder_Supplier");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseOrderId, e.ProductId }).HasName("PK_PurchaseOrderDetail_1");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_Product");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.ReviewAccounts).HasConstraintName("FK_Review_Account");

            entity.HasOne(d => d.Creator).WithMany(p => p.ReviewCreators).HasConstraintName("FK_Review_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.ReviewEditors).HasConstraintName("FK_Review_Account_Edit");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews).HasConstraintName("FK_Review_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.RoleCreators).HasConstraintName("FK_Role_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.RoleEditors).HasConstraintName("FK_Role_Account_Edit");
        });

        modelBuilder.Entity<ShippingInfo>(entity =>
        {
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Creator).WithMany(p => p.ShippingInfoCreators).HasConstraintName("FK_ShippingInfo_Account_Create");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShippingInfos).HasConstraintName("FK_ShippingInfo_Customer");

            entity.HasOne(d => d.Editor).WithMany(p => p.ShippingInfoEditors).HasConstraintName("FK_ShippingInfo_Account_Edit");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.IdCard).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Account).WithOne(p => p.Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Account");
        });

        modelBuilder.Entity<Stationery>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Other_1");

            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.Stationery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stationery_Product");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories).HasConstraintName("FK_SubCategory_Category");

            entity.HasOne(d => d.Creator).WithMany(p => p.SubCategoryCreators).HasConstraintName("FK_SubCategory_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.SubCategoryEditors).HasConstraintName("FK_SubCategory_Account_Edit");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasOne(d => d.Creator).WithMany(p => p.SupplierCreators).HasConstraintName("FK_Supplier_Account_Create");

            entity.HasOne(d => d.Editor).WithMany(p => p.SupplierEditors).HasConstraintName("FK_Supplier_Account_Edit");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
