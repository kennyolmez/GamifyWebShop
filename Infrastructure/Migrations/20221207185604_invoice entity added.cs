using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class invoiceentityadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PendingInvoiceMailId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PendingInvoiceMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingInvoiceMails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_PendingInvoiceMailId",
                table: "ShoppingCartItems",
                column: "PendingInvoiceMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_PendingInvoiceMails_PendingInvoiceMailId",
                table: "ShoppingCartItems",
                column: "PendingInvoiceMailId",
                principalTable: "PendingInvoiceMails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_PendingInvoiceMails_PendingInvoiceMailId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "PendingInvoiceMails");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_PendingInvoiceMailId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "PendingInvoiceMailId",
                table: "ShoppingCartItems");
        }
    }
}
