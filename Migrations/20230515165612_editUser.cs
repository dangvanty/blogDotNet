﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razor9_identity.Migrations
{
    /// <inheritdoc />
    public partial class editUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Users",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Users");
        }
    }
}
