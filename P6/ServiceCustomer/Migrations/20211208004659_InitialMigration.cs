using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCustomer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustId);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustId", "CustName", "CustPassword" },
                values: new object[] { 1, "Customer1", "123123" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustId", "CustName", "CustPassword" },
                values: new object[] { 2, "Customer2", "123123" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustId", "CustName", "CustPassword" },
                values: new object[] { 3, "Customer3", "123123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
