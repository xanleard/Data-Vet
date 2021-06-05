using Microsoft.EntityFrameworkCore.Migrations;

namespace VET.Database.Migrations.SqlServer.Migrations.Migrations
{
    public partial class MyVisitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Appointments");
        }
    }
}
