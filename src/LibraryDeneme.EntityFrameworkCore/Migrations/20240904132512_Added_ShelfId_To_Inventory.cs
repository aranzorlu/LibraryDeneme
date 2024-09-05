using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDeneme.Migrations
{
    /// <inheritdoc />
    public partial class Added_ShelfId_To_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShelfId",
                table: "AppInventorys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppInventorys_ShelfId",
                table: "AppInventorys",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInventorys_AppShelfs_ShelfId",
                table: "AppInventorys",
                column: "ShelfId",
                principalTable: "AppShelfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInventorys_AppShelfs_ShelfId",
                table: "AppInventorys");

            migrationBuilder.DropIndex(
                name: "IX_AppInventorys_ShelfId",
                table: "AppInventorys");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "AppInventorys");
        }
    }
}
