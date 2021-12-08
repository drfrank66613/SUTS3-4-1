using Microsoft.EntityFrameworkCore.Migrations;

namespace P5.Migrations
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

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                    table.ForeignKey(
                        name: "FK_Baskets_Customers_CustId",
                        column: x => x.CustId,
                        principalTable: "Customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_CustId",
                        column: x => x.CustId,
                        principalTable: "Customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustId",
                        column: x => x.CustId,
                        principalTable: "Customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketWithProducts",
                columns: table => new
                {
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketWithProducts", x => new { x.BasketId, x.ProdId });
                    table.ForeignKey(
                        name: "FK_BasketWithProducts_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketWithProducts_Products_ProdId",
                        column: x => x.ProdId,
                        principalTable: "Products",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleWithProducts",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleWithProducts", x => new { x.SaleId, x.ProdId });
                    table.ForeignKey(
                        name: "FK_SaleWithProducts_Products_ProdId",
                        column: x => x.ProdId,
                        principalTable: "Products",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleWithProducts_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustId", "CustName", "CustPassword" },
                values: new object[,]
                {
                    { 1, "Customer1", "123123" },
                    { 2, "Customer2", "123123" },
                    { 3, "Customer3", "123123" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProdId", "ProdName", "ProdPrice" },
                values: new object[,]
                {
                    { 1, "Car", 300.0 },
                    { 2, "Smartphone", 99.989999999999995 },
                    { 3, "Watch", 30.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustId",
                table: "Baskets",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketWithProducts_ProdId",
                table: "BasketWithProducts",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustId",
                table: "Feedbacks",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustId",
                table: "Sales",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleWithProducts_ProdId",
                table: "SaleWithProducts",
                column: "ProdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketWithProducts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "SaleWithProducts");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
