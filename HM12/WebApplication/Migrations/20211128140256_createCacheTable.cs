using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    public partial class createCacheTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputedExpressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    V1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    V2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Op = table.Column<byte>(type: "tinyint", nullable: false),
                    Res = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputedExpressions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputedExpressions");
        }
    }
}
