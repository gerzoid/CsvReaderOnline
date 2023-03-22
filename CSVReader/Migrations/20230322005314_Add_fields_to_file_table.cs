﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSVReader.Migrations
{
    /// <inheritdoc />
    public partial class Add_fields_to_file_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnsCount",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeader",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RowsCount",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<char>(
                name: "Separator",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: ' ');
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnsCount",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "HasHeader",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "RowsCount",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Separator",
                table: "Files");
        }
    }
}
