using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApp.Data;
using Project.Common.Enums;

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170924065112_App_20170924145037")]
    partial class App_20170924145037
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Project.Entities.Alert.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommonId");

                    b.Property<DateTime>("NotifDate");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Project.Entities.Alert.UserNotification", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<long>("NotificationId");

                    b.Property<bool>("IsViewed");

                    b.HasKey("UserId", "NotificationId");

                    b.HasAlternateKey("NotificationId", "UserId");

                    b.ToTable("UserNotification");
                });

            modelBuilder.Entity("Project.Entities.Block.ItemBlock", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupId");

                    b.Property<string>("BackgroundUrl");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("ItemUrl");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Type");

                    b.HasKey("Id", "GroupId");

                    b.HasAlternateKey("GroupId", "Id");

                    b.ToTable("ItemBlocks");
                });

            modelBuilder.Entity("Project.Entities.Cart.MyCart", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<long>("ItemId");

                    b.Property<int>("Quantity");

                    b.Property<bool>("Selected");

                    b.HasKey("UserId", "ItemId");

                    b.HasAlternateKey("ItemId", "UserId");

                    b.ToTable("MyCart");
                });

            modelBuilder.Entity("Project.Entities.Category.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Project.Entities.Category.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("Project.Entities.Global.GlobalImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileLocation");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Sequence");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("GlobalImage");
                });

            modelBuilder.Entity("Project.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyAddress")
                        .IsRequired();

                    b.Property<string>("CompanyContact")
                        .IsRequired();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<decimal>("Discount");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool?>("IsRemove");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<decimal>("OtherFees");

                    b.Property<string>("OtherRemarks");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<decimal>("Tax");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Project.Entities.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("JoDate");

                    b.Property<int>("JobOrderStatusId");

                    b.Property<long?>("PurchaseOrderId");

                    b.Property<string>("Remarks");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("JobOrderStatusId");

                    b.HasIndex("PurchaseOrderId");

                    b.HasIndex("UserId");

                    b.ToTable("JobOrder");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderAction", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("NextStatusId");

                    b.Property<int>("StatusId");

                    b.Property<int>("ValidStatusId");

                    b.HasKey("Id");

                    b.HasIndex("NextStatusId");

                    b.HasIndex("StatusId");

                    b.HasIndex("ValidStatusId");

                    b.ToTable("JobOrderAction");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("ActionDate");

                    b.Property<string>("ActionUserId");

                    b.Property<long>("JobOrderId");

                    b.HasKey("Id");

                    b.HasIndex("ActionUserId");

                    b.HasIndex("JobOrderId");

                    b.ToTable("JobOrderHistory");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderItem", b =>
                {
                    b.Property<long>("JobOrderId");

                    b.Property<long>("ItemId");

                    b.Property<DateTime?>("DateTimeEnd");

                    b.Property<DateTime?>("DateTimeStart");

                    b.Property<int>("EstDay");

                    b.Property<int>("EstHour");

                    b.Property<int>("EstMinute");

                    b.Property<int>("Qty");

                    b.Property<int>("RejectQty");

                    b.Property<string>("Remarks");

                    b.Property<int>("StatusId");

                    b.HasKey("JobOrderId", "ItemId");

                    b.HasAlternateKey("ItemId", "JobOrderId");

                    b.HasIndex("StatusId");

                    b.ToTable("JobOrderItem");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderItemAction", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("NextStatusId");

                    b.Property<int>("StatusId");

                    b.Property<int>("ValidStatusId");

                    b.HasKey("Id");

                    b.HasIndex("NextStatusId");

                    b.HasIndex("StatusId");

                    b.HasIndex("ValidStatusId");

                    b.ToTable("JobOrderItemAction");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderItemStatus", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ClassIcon");

                    b.Property<string>("ClassName");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("JobOrderItemStatus");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderStatus", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ClassIcon");

                    b.Property<string>("ClassName");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("JobOrderStatus");
                });

            modelBuilder.Entity("Project.Entities.Product.ImageLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileLocation");

                    b.Property<string>("FileName");

                    b.Property<long?>("ItemId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ImageLocation");
                });

            modelBuilder.Entity("Project.Entities.Product.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode")
                        .IsRequired();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("NotForSale");

                    b.Property<string>("PartNo")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<bool>("PurPro");

                    b.Property<int>("Qty");

                    b.Property<int>("QtyNotification");

                    b.Property<bool>("Removed");

                    b.Property<int?>("SizeId");

                    b.Property<string>("StockNo")
                        .IsRequired();

                    b.Property<int>("SubCategoryId");

                    b.Property<decimal>("UnitCost");

                    b.Property<int>("UomId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SizeId");

                    b.HasIndex("SubCategoryId");

                    b.HasIndex("UomId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Project.Entities.Product.ItemHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("ActionDate");

                    b.Property<string>("ActionUserId");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");

                    b.HasIndex("ActionUserId");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemHistory");
                });

            modelBuilder.Entity("Project.Entities.Product.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("Project.Entities.Product.UnitOfMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UnitOfMeasure");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Discount");

                    b.Property<decimal>("OtherFees");

                    b.Property<string>("OtherRemarks");

                    b.Property<DateTime>("PoDate");

                    b.Property<int>("PurchaseOrderStatusId");

                    b.Property<string>("Remarks");

                    b.Property<decimal>("Tax");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseOrder");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderAction", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("NextStatusId");

                    b.Property<int>("PoStatusId");

                    b.Property<int>("ValidStatusId");

                    b.HasKey("Id");

                    b.HasIndex("NextStatusId");

                    b.HasIndex("PoStatusId");

                    b.HasIndex("ValidStatusId");

                    b.ToTable("PurchaseOrderAction");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("ActionDate");

                    b.Property<string>("ActionUserId");

                    b.Property<long>("PoId");

                    b.HasKey("Id");

                    b.HasIndex("ActionUserId");

                    b.HasIndex("PoId");

                    b.ToTable("PurchaseOrderHistory");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderItem", b =>
                {
                    b.Property<long>("PurchaseOrderId");

                    b.Property<long>("ItemId");

                    b.Property<decimal>("Discount");

                    b.Property<int>("JobQty");

                    b.Property<decimal>("Price");

                    b.Property<int>("Qty");

                    b.Property<string>("Remarks");

                    b.Property<decimal>("UnitCost");

                    b.HasKey("PurchaseOrderId", "ItemId");

                    b.HasAlternateKey("ItemId", "PurchaseOrderId");

                    b.ToTable("PurchaseOrderItem");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderStatus", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ClassIcon");

                    b.Property<string>("ClassName");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrderStatus");
                });

            modelBuilder.Entity("Project.Entities.SiteMap.SiteMap", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<string>("Area");

                    b.Property<string>("ClassIcon");

                    b.Property<string>("ClassName");

                    b.Property<string>("Controller");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsPublic");

                    b.Property<bool>("IsVisible");

                    b.Property<string>("Method");

                    b.Property<string>("ParentId");

                    b.Property<int>("Sequence");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("SiteMap");
                });

            modelBuilder.Entity("Project.Entities.SiteMap.SiteMapRole", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("SiteMapId");

                    b.HasKey("RoleId", "SiteMapId");

                    b.HasIndex("SiteMapId");

                    b.ToTable("SiteMapRole");
                });

            modelBuilder.Entity("Project.Entities.User.UserInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyContact");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Project.Entities.User.UserInfoHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("ActionDate");

                    b.Property<string>("ActionUserId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ActionUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInfoHistory");
                });

            modelBuilder.Entity("Project.Entities.UserReports.ReportDetail", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description");

                    b.Property<bool>("IsField");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.ToTable("ReportDetail");
                });

            modelBuilder.Entity("Project.Entities.UserReports.UserReport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsLock");

                    b.Property<int>("Month");

                    b.Property<string>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserReport");
                });

            modelBuilder.Entity("Project.Entities.UserReports.UserReportDetail", b =>
                {
                    b.Property<long>("UrId");

                    b.Property<int>("RdId");

                    b.Property<decimal>("Value");

                    b.HasKey("UrId", "RdId");

                    b.HasAlternateKey("RdId", "UrId");

                    b.ToTable("UserReportDetail");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Project.Entities.Identity.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Project.Entities.Identity.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Project.Entities.Identity.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Project.Entities.Identity.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.Identity.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.Alert.UserNotification", b =>
                {
                    b.HasOne("Project.Entities.Alert.Notification", "Notification")
                        .WithMany("UserNotifications")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.Cart.MyCart", b =>
                {
                    b.HasOne("Project.Entities.Product.Item", "Item")
                        .WithMany("MyCarts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.User.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.Category.SubCategory", b =>
                {
                    b.HasOne("Project.Entities.Category.Category", "Category")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrder", b =>
                {
                    b.HasOne("Project.Entities.JobOrder.JobOrderStatus", "JobOrderStatus")
                        .WithMany("JobOrders")
                        .HasForeignKey("JobOrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrder", "PurchaseOrder")
                        .WithMany()
                        .HasForeignKey("PurchaseOrderId");

                    b.HasOne("Project.Entities.User.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderAction", b =>
                {
                    b.HasOne("Project.Entities.JobOrder.JobOrderStatus", "NextStatus")
                        .WithMany("NextStatusActions")
                        .HasForeignKey("NextStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrderStatus", "Status")
                        .WithMany("StatusActions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrderStatus", "ValidStatus")
                        .WithMany("ValidStatusActions")
                        .HasForeignKey("ValidStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderHistory", b =>
                {
                    b.HasOne("Project.Entities.User.UserInfo", "ActionUser")
                        .WithMany("JobOrderHistories")
                        .HasForeignKey("ActionUserId");

                    b.HasOne("Project.Entities.JobOrder.JobOrder", "JobOrder")
                        .WithMany("History")
                        .HasForeignKey("JobOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderItem", b =>
                {
                    b.HasOne("Project.Entities.Product.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrder", "JobOrder")
                        .WithMany("JobOrderItems")
                        .HasForeignKey("JobOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrderItemStatus", "Status")
                        .WithMany("JobOrderItems")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.JobOrder.JobOrderItemAction", b =>
                {
                    b.HasOne("Project.Entities.JobOrder.JobOrderItemStatus", "NextStatus")
                        .WithMany("NextStatusActions")
                        .HasForeignKey("NextStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrderItemStatus", "Status")
                        .WithMany("StatusActions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.JobOrder.JobOrderItemStatus", "ValidStatus")
                        .WithMany("ValidStatusActions")
                        .HasForeignKey("ValidStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.Product.ImageLocation", b =>
                {
                    b.HasOne("Project.Entities.Product.Item", "Item")
                        .WithMany("Images")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("Project.Entities.Product.Item", b =>
                {
                    b.HasOne("Project.Entities.Category.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.Product.Size", "Size")
                        .WithMany("Items")
                        .HasForeignKey("SizeId");

                    b.HasOne("Project.Entities.Category.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.Product.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany("Items")
                        .HasForeignKey("UomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.Product.ItemHistory", b =>
                {
                    b.HasOne("Project.Entities.User.UserInfo", "ActionUser")
                        .WithMany("ItemHistories")
                        .HasForeignKey("ActionUserId");

                    b.HasOne("Project.Entities.Product.Item", "Item")
                        .WithMany("ItemHistories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrder", b =>
                {
                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrderStatus", "PurchaseOrderStatus")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("PurchaseOrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.User.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderAction", b =>
                {
                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrderStatus", "NextStatus")
                        .WithMany("NextStatusActions")
                        .HasForeignKey("NextStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrderStatus", "PoStatus")
                        .WithMany("PoStatusActions")
                        .HasForeignKey("PoStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrderStatus", "ValidStatus")
                        .WithMany("ValidStatusActions")
                        .HasForeignKey("ValidStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderHistory", b =>
                {
                    b.HasOne("Project.Entities.User.UserInfo", "ActionUser")
                        .WithMany("PurchaseOrderHistories")
                        .HasForeignKey("ActionUserId");

                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrder", "PurchaseOrder")
                        .WithMany("History")
                        .HasForeignKey("PoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.PurchaseOrder.PurchaseOrderItem", b =>
                {
                    b.HasOne("Project.Entities.Product.Item", "Item")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.PurchaseOrder.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.SiteMap.SiteMapRole", b =>
                {
                    b.HasOne("Project.Entities.Identity.Role", "Role")
                        .WithMany("SiteMapRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.SiteMap.SiteMap", "SiteMap")
                        .WithMany("SiteMapRoles")
                        .HasForeignKey("SiteMapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Entities.User.UserInfoHistory", b =>
                {
                    b.HasOne("Project.Entities.User.UserInfo", "ActionUser")
                        .WithMany("ActionUserInfoHistories")
                        .HasForeignKey("ActionUserId");

                    b.HasOne("Project.Entities.User.UserInfo", "User")
                        .WithMany("UserInfoHistories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project.Entities.UserReports.UserReport", b =>
                {
                    b.HasOne("Project.Entities.User.UserInfo", "User")
                        .WithMany("UserReport")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project.Entities.UserReports.UserReportDetail", b =>
                {
                    b.HasOne("Project.Entities.UserReports.ReportDetail", "ReportDetail")
                        .WithMany("UserReportDetail")
                        .HasForeignKey("RdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Entities.UserReports.UserReport", "UserReport")
                        .WithMany("UserReportDetail")
                        .HasForeignKey("UrId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
