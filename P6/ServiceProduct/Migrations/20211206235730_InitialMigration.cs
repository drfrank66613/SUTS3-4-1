using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceProduct.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProdId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProdId", "ProdName", "ProdPrice" },
                values: new object[] { 1, "Car", 300.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProdId", "ProdName", "ProdPrice" },
                values: new object[] { 2, "Smartphone", 99.989999999999995 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProdId", "ProdName", "ProdPrice" },
                values: new object[] { 3, "Watch", 30.5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
