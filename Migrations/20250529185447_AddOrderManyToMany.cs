using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfectioneryManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cakes_Orders_OrderEntityId",
                table: "Cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pastries_Orders_OrderEntityId",
                table: "Pastries");

            migrationBuilder.DropIndex(
                name: "IX_Pastries_OrderEntityId",
                table: "Pastries");

            migrationBuilder.DropIndex(
                name: "IX_Cakes_OrderEntityId",
                table: "Cakes");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Pastries");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Cakes");

            migrationBuilder.CreateTable(
                name: "OrderCakes",
                columns: table => new
                {
                    OrderEntityId = table.Column<int>(type: "integer", nullable: false),
                    OrderedCakesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCakes", x => new { x.OrderEntityId, x.OrderedCakesId });
                    table.ForeignKey(
                        name: "FK_OrderCakes_Cakes_OrderedCakesId",
                        column: x => x.OrderedCakesId,
                        principalTable: "Cakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCakes_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPastries",
                columns: table => new
                {
                    CakesId = table.Column<int>(type: "integer", nullable: false),
                    OrderEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPastries", x => new { x.CakesId, x.OrderEntityId });
                    table.ForeignKey(
                        name: "FK_OrderPastries_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPastries_Pastries_CakesId",
                        column: x => x.CakesId,
                        principalTable: "Pastries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCakes_OrderedCakesId",
                table: "OrderCakes",
                column: "OrderedCakesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPastries_OrderEntityId",
                table: "OrderPastries",
                column: "OrderEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCakes");

            migrationBuilder.DropTable(
                name: "OrderPastries");

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "Pastries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "Cakes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pastries_OrderEntityId",
                table: "Pastries",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_OrderEntityId",
                table: "Cakes",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cakes_Orders_OrderEntityId",
                table: "Cakes",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pastries_Orders_OrderEntityId",
                table: "Pastries",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
