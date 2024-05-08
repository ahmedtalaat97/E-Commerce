using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Repository.Migrations
{
    public partial class UpdatePaymentIntitiesIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentIntent",
                table: "Orders",
                newName: "PaymentIntentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentIntentId",
                table: "Orders",
                newName: "PaymentIntent");
        }
    }
}
