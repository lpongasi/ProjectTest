using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class App_20170924140931 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    GroupId = table.Column<string>(nullable: false),
                    BackgroundUrl = table.Column<string>(nullable: true),
                    ItemUrl = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBlocks", x => new { x.Id, x.GroupId });
                    table.UniqueConstraint("AK_ItemBlocks_GroupId_Id", x => new { x.GroupId, x.Id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBlocks");
        }
    }
}
