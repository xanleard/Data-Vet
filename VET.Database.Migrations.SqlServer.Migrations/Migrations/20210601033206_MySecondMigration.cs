using Microsoft.EntityFrameworkCore.Migrations;

namespace VET.Database.Migrations.SqlServer.Migrations.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplet",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VisitDetail",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplet",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "VisitDetail",
                table: "Appointments");
        }
    }
}
