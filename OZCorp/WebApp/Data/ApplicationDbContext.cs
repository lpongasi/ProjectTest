using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Entities.Alert;
using Project.Entities.Identity;
using Project.Entities.JobOrder;
using Project.Entities.PurchaseOrder;
using Project.Entities.SiteMap;
using Project.Entities.User;
using Project.Entities.Category;
using Project.Entities.Cart;
using Project.Entities.Global;
using Project.Entities.Product;
using Project.Entities.UserReports;
using Project.Entities.Block;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<JobOrder> JobOrder { get; set; }
        public DbSet<JobOrderItem> JobOrderItem { get; set; }
        public DbSet<JobOrderItemStatus> JobOrderItemStatus { get; set; }
        public DbSet<JobOrderItemAction> JobOrderItemAction { get; set; }
        public DbSet<JobOrderStatus> JobOrderStatus { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; }
        public DbSet<PurchaseOrderStatus> PurchaseOrderStatus { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ImageLocation> ImageLocations { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserInfoHistory> UserInfoHistories { get; set; }
        public DbSet<ItemHistory> ItemHistories { get; set; }
        public DbSet<JobOrderHistory> JobOrderHistories { get; set; }
        public DbSet<PurchaseOrderHistory> PurchaseOrderHistories { get; set; }
        public DbSet<MyCart> MyCart { get; set; }
        public DbSet<PurchaseOrderAction> PurchaseOrderActions { get; set; }
        public DbSet<JobOrderAction> JobOrderActions { get; set; }
        public DbSet<SiteMap> SiteMaps { get; set; }
        public DbSet<SiteMapRole> SiteMapRoles { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }
        public DbSet<UserReport> UserReport { get; set; }
        public DbSet<UserReportDetail> UserReportDetail { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }
        public DbSet<GlobalImage> GlobalImages { get; set; }
        public DbSet<ItemBlock> ItemBlocks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemBlock>().HasKey(key => new
            {
                key.Id,
                key.GroupId
            });
            builder.Entity<PurchaseOrderItem>().HasKey(key => new
            {
                key.PurchaseOrderId,
                key.ItemId
            });
            builder.Entity<JobOrderItem>().HasKey(key => new
            {
                key.JobOrderId,
                key.ItemId
            });
            builder.Entity<MyCart>().HasKey(key => new
            {
                key.UserId,
                key.ItemId
            });
            builder.Entity<SiteMapRole>().HasKey(key => new
            {
                key.RoleId,
                key.SiteMapId
            });
            builder.Entity<UserReportDetail>().HasKey(key => new
            {
                key.UrId,
                key.RdId
            });
            builder.Entity<UserNotification>().HasKey(key => new
            {
                key.UserId,
                key.NotificationId
            });

            builder.Entity<UserInfo>()
                   .HasMany(m => m.UserInfoHistories)
                   .WithOne(o => o.User)
                   .HasForeignKey(f => f.UserId)
                   .HasPrincipalKey(p => p.Id);
            builder.Entity<UserInfo>()
                   .HasMany(m => m.ActionUserInfoHistories)
                   .WithOne(o => o.ActionUser)
                   .HasForeignKey(f => f.ActionUserId)
                   .HasPrincipalKey(p => p.Id);

            builder.Entity<PurchaseOrderStatus>()
                   .HasMany(m => m.PoStatusActions)
                   .WithOne(o => o.PoStatus)
                   .HasForeignKey(f => f.PoStatusId)
                   .HasPrincipalKey(p => p.Id);


            builder.Entity<PurchaseOrderStatus>()
                   .HasMany(m => m.ValidStatusActions)
                   .WithOne(o => o.ValidStatus)
                   .HasForeignKey(f => f.ValidStatusId)
                   .HasPrincipalKey(p => p.Id);



            builder.Entity<PurchaseOrderStatus>()
                   .HasMany(m => m.NextStatusActions)
                   .WithOne(o => o.NextStatus)
                   .HasForeignKey(f => f.NextStatusId)
                   .HasPrincipalKey(p => p.Id);


            builder.Entity<JobOrderStatus>()
                   .HasMany(m => m.StatusActions)
                   .WithOne(o => o.Status)
                   .HasForeignKey(f => f.StatusId)
                   .HasPrincipalKey(p => p.Id);


            builder.Entity<JobOrderStatus>()
                   .HasMany(m => m.ValidStatusActions)
                   .WithOne(o => o.ValidStatus)
                   .HasForeignKey(f => f.ValidStatusId)
                   .HasPrincipalKey(p => p.Id);

            builder.Entity<JobOrderStatus>()
                   .HasMany(m => m.NextStatusActions)
                   .WithOne(o => o.NextStatus)
                   .HasForeignKey(f => f.NextStatusId)
                   .HasPrincipalKey(p => p.Id);





            builder.Entity<JobOrderItemStatus>()
                .HasMany(m => m.StatusActions)
                .WithOne(o => o.Status)
                .HasForeignKey(f => f.StatusId)
                .HasPrincipalKey(p => p.Id);


            builder.Entity<JobOrderItemStatus>()
                .HasMany(m => m.ValidStatusActions)
                .WithOne(o => o.ValidStatus)
                .HasForeignKey(f => f.ValidStatusId)
                .HasPrincipalKey(p => p.Id);

            builder.Entity<JobOrderItemStatus>()
                .HasMany(m => m.NextStatusActions)
                .WithOne(o => o.NextStatus)
                .HasForeignKey(f => f.NextStatusId)
                .HasPrincipalKey(p => p.Id);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        }
    }
}
