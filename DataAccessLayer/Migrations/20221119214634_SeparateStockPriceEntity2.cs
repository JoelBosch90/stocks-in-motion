using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class SeparateStockPriceEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_DataSources_DataSourceId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_DataSourceId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "DataSourceId",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "StockPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Open = table.Column<int>(type: "integer", nullable: false),
                    High = table.Column<int>(type: "integer", nullable: false),
                    Low = table.Column<int>(type: "integer", nullable: false),
                    Close = table.Column<int>(type: "integer", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<long>(type: "bigint", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockPrices_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockPrices_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_SourceId",
                table: "StockPrices",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_StockId",
                table: "StockPrices",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPrices");

            migrationBuilder.AddColumn<long>(
                name: "DataSourceId",
                table: "Stocks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_DataSourceId",
                table: "Stocks",
                column: "DataSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_DataSources_DataSourceId",
                table: "Stocks",
                column: "DataSourceId",
                principalTable: "DataSources",
                principalColumn: "Id");
        }
    }
}
