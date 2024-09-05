using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDeneme.Migrations
{
    /// <inheritdoc />
    public partial class Added_BookId_To_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "AppInventorys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppInventorys_BookId",
                table: "AppInventorys",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInventorys_AppBooks_BookId",
                table: "AppInventorys",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInventorys_AppBooks_BookId",
                table: "AppInventorys");

            migrationBuilder.DropIndex(
                name: "IX_AppInventorys_BookId",
                table: "AppInventorys");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "AppInventorys");
        }
    }
}
