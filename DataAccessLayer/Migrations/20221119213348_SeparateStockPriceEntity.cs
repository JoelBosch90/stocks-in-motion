using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class SeparateStockPriceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Added",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Close",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "High",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Low",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Open",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Stocks",
                newName: "DataSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_SourceId",
                table: "Stocks",
                newName: "IX_Stocks_DataSourceId");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Stocks",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_DataSources_DataSourceId",
                table: "Stocks",
                column: "DataSourceId",
                principalTable: "DataSources",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_DataSources_DataSourceId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "DataSourceId",
                table: "Stocks",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_DataSourceId",
                table: "Stocks",
                newName: "IX_Stocks_SourceId");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Stocks",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Added",
                table: "Stocks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Close",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Stocks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "High",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Low",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Open",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks",
                column: "SourceId",
                principalTable: "DataSources",
                principalColumn: "Id");
        }
    }
}
