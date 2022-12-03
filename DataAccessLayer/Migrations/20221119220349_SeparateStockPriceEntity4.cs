using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class SeparateStockPriceEntity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPrices_DataSources_SourceId",
                table: "StockPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPrices_Stocks_StockId",
                table: "StockPrices");

            migrationBuilder.DropIndex(
                name: "IX_StockPrices_SourceId",
                table: "StockPrices");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "StockPrices");

            migrationBuilder.AlterColumn<long>(
                name: "StockId",
                table: "StockPrices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DataSourceId",
                table: "StockPrices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_DataSourceId",
                table: "StockPrices",
                column: "DataSourceId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DataSourceId",
                table: "StockPrices");

            migrationBuilder.AlterColumn<long>(
                name: "StockId",
                table: "StockPrices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "SourceId",
                table: "StockPrices",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_SourceId",
                table: "StockPrices",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPrices_DataSources_SourceId",
                table: "StockPrices",
                column: "SourceId",
                principalTable: "DataSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPrices_Stocks_StockId",
                table: "StockPrices",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
