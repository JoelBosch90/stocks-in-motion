using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class HistoricalDailyStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moment",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "DataSources");

            migrationBuilder.RenameColumn(
                name: "Ticker",
                table: "Stocks",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Stocks",
                newName: "Open");

            migrationBuilder.AddColumn<int>(
                name: "Close",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Close",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "High",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Low",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Stocks",
                newName: "Ticker");

            migrationBuilder.RenameColumn(
                name: "Open",
                table: "Stocks",
                newName: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "Moment",
                table: "Stocks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "DataSources",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }
    }
}
