using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGarage.Migrations
{
    /// <inheritdoc />
    public partial class MakeMotorcycleCustomerRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Customers_CustomerId",
                table: "Motorcycles");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Motorcycles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Customers_CustomerId",
                table: "Motorcycles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Customers_CustomerId",
                table: "Motorcycles");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Motorcycles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Customers_CustomerId",
                table: "Motorcycles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
