using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class HistoricalDailyStocks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks");

            migrationBuilder.AlterColumn<long>(
                name: "SourceId",
                table: "Stocks",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks",
                column: "SourceId",
                principalTable: "DataSources",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks");

            migrationBuilder.AlterColumn<long>(
                name: "SourceId",
                table: "Stocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_DataSources_SourceId",
                table: "Stocks",
                column: "SourceId",
                principalTable: "DataSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
