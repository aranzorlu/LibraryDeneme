﻿using LibraryDeneme.Shelfs;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace LibraryDeneme.Migrations
{
    /// <inheritdoc />
    public partial class Added_Shelfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppShelfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShelfName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ShelfType = table.Column<int>(type: "int", nullable: false),
                    ShelfBolum = table.Column<int>(type: "int",nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppShelfs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppShelfs_ShelfName",
                table: "AppShelfs",
                column: "ShelfName");
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "AppShelfs");

        }
    }
}
