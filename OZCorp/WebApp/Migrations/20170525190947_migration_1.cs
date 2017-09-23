using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CommonId = table.Column<string>(nullable: true),
                    NotifDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CompanyAddress = table.Column<string>(nullable: false),
                    CompanyContact = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    IsRemove = table.Column<bool>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    OtherFees = table.Column<decimal>(nullable: false),
                    OtherRemarks = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Tax = table.Column<decimal>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ClassIcon = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ClassIcon = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ClassIcon = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteMap",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Action = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    ClassIcon = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    IsVisible = table.Column<bool>(nullable: false),
                    Method = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyContact = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsField = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Sequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    NotificationId = table.Column<long>(nullable: false),
                    IsViewed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => new { x.UserId, x.NotificationId });
                    table.UniqueConstraint("AK_UserNotification_NotificationId_UserId", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserNotification_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderItemAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NextStatusId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ValidStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderItemAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOrderItemAction_JobOrderItemStatus_NextStatusId",
                        column: x => x.NextStatusId,
                        principalTable: "JobOrderItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderItemAction_JobOrderItemStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JobOrderItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderItemAction_JobOrderItemStatus_ValidStatusId",
                        column: x => x.ValidStatusId,
                        principalTable: "JobOrderItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NextStatusId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ValidStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOrderAction_JobOrderStatus_NextStatusId",
                        column: x => x.NextStatusId,
                        principalTable: "JobOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderAction_JobOrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JobOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderAction_JobOrderStatus_ValidStatusId",
                        column: x => x.ValidStatusId,
                        principalTable: "JobOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NextStatusId = table.Column<int>(nullable: false),
                    PoStatusId = table.Column<int>(nullable: false),
                    ValidStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAction_PurchaseOrderStatus_NextStatusId",
                        column: x => x.NextStatusId,
                        principalTable: "PurchaseOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAction_PurchaseOrderStatus_PoStatusId",
                        column: x => x.PoStatusId,
                        principalTable: "PurchaseOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAction_PurchaseOrderStatus_ValidStatusId",
                        column: x => x.ValidStatusId,
                        principalTable: "PurchaseOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteMapRole",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    SiteMapId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMapRole", x => new { x.RoleId, x.SiteMapId });
                    table.ForeignKey(
                        name: "FK_SiteMapRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteMapRole_SiteMap_SiteMapId",
                        column: x => x.SiteMapId,
                        principalTable: "SiteMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Discount = table.Column<decimal>(nullable: false),
                    OtherFees = table.Column<decimal>(nullable: false),
                    OtherRemarks = table.Column<string>(nullable: true),
                    PoDate = table.Column<DateTime>(nullable: false),
                    PurchaseOrderStatusId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Tax = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseOrderStatus_PurchaseOrderStatusId",
                        column: x => x.PurchaseOrderStatusId,
                        principalTable: "PurchaseOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Action = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    ActionUserId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfoHistory_UserInfo_ActionUserId",
                        column: x => x.ActionUserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfoHistory_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserReport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    IsLock = table.Column<bool>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReport_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Barcode = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    NotForSale = table.Column<bool>(nullable: false),
                    PartNo = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PurPro = table.Column<bool>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    QtyNotification = table.Column<int>(nullable: false),
                    Removed = table.Column<bool>(nullable: false),
                    StockNo = table.Column<string>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false),
                    UnitCost = table.Column<decimal>(nullable: false),
                    UomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_UnitOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOrder",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    JoDate = table.Column<DateTime>(nullable: false),
                    JobOrderStatusId = table.Column<int>(nullable: false),
                    PurchaseOrderId = table.Column<long>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOrder_JobOrderStatus_JobOrderStatusId",
                        column: x => x.JobOrderStatusId,
                        principalTable: "JobOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrder_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOrder_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Action = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    ActionUserId = table.Column<string>(nullable: true),
                    PoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHistory_UserInfo_ActionUserId",
                        column: x => x.ActionUserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHistory_PurchaseOrder_PoId",
                        column: x => x.PoId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReportDetail",
                columns: table => new
                {
                    UrId = table.Column<long>(nullable: false),
                    RdId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReportDetail", x => new { x.UrId, x.RdId });
                    table.UniqueConstraint("AK_UserReportDetail_RdId_UrId", x => new { x.RdId, x.UrId });
                    table.ForeignKey(
                        name: "FK_UserReportDetail_ReportDetail_RdId",
                        column: x => x.RdId,
                        principalTable: "ReportDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReportDetail_UserReport_UrId",
                        column: x => x.UrId,
                        principalTable: "UserReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyCart",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Selected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCart", x => new { x.UserId, x.ItemId });
                    table.UniqueConstraint("AK_MyCart_ItemId_UserId", x => new { x.ItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MyCart_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyCart_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageLocation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    FileLocation = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageLocation_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Action = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    ActionUserId = table.Column<string>(nullable: true),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemHistory_UserInfo_ActionUserId",
                        column: x => x.ActionUserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemHistory_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    JobQty = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    UnitCost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItem", x => new { x.PurchaseOrderId, x.ItemId });
                    table.UniqueConstraint("AK_PurchaseOrderItem_ItemId_PurchaseOrderId", x => new { x.ItemId, x.PurchaseOrderId });
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Action = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    ActionUserId = table.Column<string>(nullable: true),
                    JobOrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOrderHistory_UserInfo_ActionUserId",
                        column: x => x.ActionUserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOrderHistory_JobOrder_JobOrderId",
                        column: x => x.JobOrderId,
                        principalTable: "JobOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOrderItem",
                columns: table => new
                {
                    JobOrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    DateTimeEnd = table.Column<DateTime>(nullable: true),
                    DateTimeStart = table.Column<DateTime>(nullable: true),
                    EstDay = table.Column<int>(nullable: false),
                    EstHour = table.Column<int>(nullable: false),
                    EstMinute = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    RejectQty = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrderItem", x => new { x.JobOrderId, x.ItemId });
                    table.UniqueConstraint("AK_JobOrderItem_ItemId_JobOrderId", x => new { x.ItemId, x.JobOrderId });
                    table.ForeignKey(
                        name: "FK_JobOrderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderItem_JobOrder_JobOrderId",
                        column: x => x.JobOrderId,
                        principalTable: "JobOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrderItem_JobOrderItemStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JobOrderItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOrder_JobOrderStatusId",
                table: "JobOrder",
                column: "JobOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrder_PurchaseOrderId",
                table: "JobOrder",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrder_UserId",
                table: "JobOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderAction_NextStatusId",
                table: "JobOrderAction",
                column: "NextStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderAction_StatusId",
                table: "JobOrderAction",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderAction_ValidStatusId",
                table: "JobOrderAction",
                column: "ValidStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderHistory_ActionUserId",
                table: "JobOrderHistory",
                column: "ActionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderHistory_JobOrderId",
                table: "JobOrderHistory",
                column: "JobOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderItem_StatusId",
                table: "JobOrderItem",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderItemAction_NextStatusId",
                table: "JobOrderItemAction",
                column: "NextStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderItemAction_StatusId",
                table: "JobOrderItemAction",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrderItemAction_ValidStatusId",
                table: "JobOrderItemAction",
                column: "ValidStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageLocation_ItemId",
                table: "ImageLocation",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SubCategoryId",
                table: "Item",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UomId",
                table: "Item",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistory_ActionUserId",
                table: "ItemHistory",
                column: "ActionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistory_ItemId",
                table: "ItemHistory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_PurchaseOrderStatusId",
                table: "PurchaseOrder",
                column: "PurchaseOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_UserId",
                table: "PurchaseOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAction_NextStatusId",
                table: "PurchaseOrderAction",
                column: "NextStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAction_PoStatusId",
                table: "PurchaseOrderAction",
                column: "PoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAction_ValidStatusId",
                table: "PurchaseOrderAction",
                column: "ValidStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHistory_ActionUserId",
                table: "PurchaseOrderHistory",
                column: "ActionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHistory_PoId",
                table: "PurchaseOrderHistory",
                column: "PoId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMapRole_SiteMapId",
                table: "SiteMapRole",
                column: "SiteMapId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoHistory_ActionUserId",
                table: "UserInfoHistory",
                column: "ActionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoHistory_UserId",
                table: "UserInfoHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReport_UserId",
                table: "UserReport",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "MyCart");

            migrationBuilder.DropTable(
                name: "JobOrderAction");

            migrationBuilder.DropTable(
                name: "JobOrderHistory");

            migrationBuilder.DropTable(
                name: "JobOrderItem");

            migrationBuilder.DropTable(
                name: "JobOrderItemAction");

            migrationBuilder.DropTable(
                name: "ImageLocation");

            migrationBuilder.DropTable(
                name: "ItemHistory");

            migrationBuilder.DropTable(
                name: "PurchaseOrderAction");

            migrationBuilder.DropTable(
                name: "PurchaseOrderHistory");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem");

            migrationBuilder.DropTable(
                name: "SiteMapRole");

            migrationBuilder.DropTable(
                name: "UserInfoHistory");

            migrationBuilder.DropTable(
                name: "UserReportDetail");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "JobOrder");

            migrationBuilder.DropTable(
                name: "JobOrderItemStatus");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "SiteMap");

            migrationBuilder.DropTable(
                name: "ReportDetail");

            migrationBuilder.DropTable(
                name: "UserReport");

            migrationBuilder.DropTable(
                name: "JobOrderStatus");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropTable(
                name: "PurchaseOrderStatus");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
