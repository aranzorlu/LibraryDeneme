using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDeneme.Migrations
{
    /// <inheritdoc />
    public partial class Added_ShelfId_To_Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShelfId",
                table: "AppBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_ShelfId",
                table: "AppBooks",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppShelfs_ShelfId",
                table: "AppBooks",
                column: "ShelfId",
                principalTable: "AppShelfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppShelfs_ShelfId",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_ShelfId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "AppBooks");
        }
    }
}
