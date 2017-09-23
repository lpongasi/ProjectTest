using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Migration_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_SizeId",
                table: "Item",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Size_SizeId",
                table: "Item",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Size_SizeId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SizeId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Size");
        }
    }
}
