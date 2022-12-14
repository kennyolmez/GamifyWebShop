using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changedrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PendingInvoiceMails_PendingInvoiceMailId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_PendingInvoiceMailId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "PendingInvoiceMails");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PendingInvoiceMailId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "PendingInvoiceMails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PendingInvoiceMailId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PendingInvoiceMailId",
                table: "OrderItems",
                column: "PendingInvoiceMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PendingInvoiceMails_PendingInvoiceMailId",
                table: "OrderItems",
                column: "PendingInvoiceMailId",
                principalTable: "PendingInvoiceMails",
                principalColumn: "Id");
        }
    }
}
