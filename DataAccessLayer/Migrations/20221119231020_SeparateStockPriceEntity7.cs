using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class SeparateStockPriceEntity7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPrices_DataSources_DataSourceId",
                table: "StockPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPrices_Stocks_StockId",
                table: "StockPrices");

            migrationBuilder.DropIndex(
                name: "IX_StockPrices_DataSourceId",
                table: "StockPrices");

            migrationBuilder.DropIndex(
                name: "IX_StockPrices_StockId",
                table: "StockPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_DataSourceId",
                table: "StockPrices",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_StockId",
                table: "StockPrices",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPrices_DataSources_DataSourceId",
                table: "StockPrices",
                column: "DataSourceId",
                principalTable: "DataSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPrices_Stocks_StockId",
                table: "StockPrices",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
