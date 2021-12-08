using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceSale.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustId = table.Column<int>(type: "int", nullable: false),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
