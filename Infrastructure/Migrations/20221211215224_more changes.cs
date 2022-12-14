using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class morechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PendingInvoiceMails_PendingInvoiceMailId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PendingInvoiceMailId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PendingInvoiceMailId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceMailId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_InvoiceMailId",
                table: "OrderItems",
                column: "InvoiceMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PendingInvoiceMails_InvoiceMailId",
                table: "OrderItems",
                column: "InvoiceMailId",
                principalTable: "PendingInvoiceMails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PendingInvoiceMails_InvoiceMailId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_InvoiceMailId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "InvoiceMailId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "PendingInvoiceMailId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PendingInvoiceMailId",
                table: "Orders",
                column: "PendingInvoiceMailId",
                unique: true,
                filter: "[PendingInvoiceMailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PendingInvoiceMails_PendingInvoiceMailId",
                table: "Orders",
                column: "PendingInvoiceMailId",
                principalTable: "PendingInvoiceMails",
                principalColumn: "Id");
        }
    }
}
