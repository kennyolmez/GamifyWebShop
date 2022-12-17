using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "DeliveryAddresses",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "DeliveryAddresses",
                newName: "AddressName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "DeliveryAddresses",
                newName: "StreetNumber");

            migrationBuilder.RenameColumn(
                name: "AddressName",
                table: "DeliveryAddresses",
                newName: "StreetName");
        }
    }
}
