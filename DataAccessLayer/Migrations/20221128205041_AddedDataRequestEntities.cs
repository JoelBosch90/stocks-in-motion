using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddedDataRequestEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataSourceId",
                table: "StockPrices",
                newName: "DataRequestId");

            migrationBuilder.CreateTable(
                name: "DataRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestString = table.Column<string>(type: "text", nullable: false),
                    ResponseCode = table.Column<int>(type: "integer", nullable: false),
                    DataSourceId = table.Column<long>(type: "bigint", nullable: false),
                    StockId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataRequests");

            migrationBuilder.RenameColumn(
                name: "DataRequestId",
                table: "StockPrices",
                newName: "DataSourceId");
        }
    }
}
