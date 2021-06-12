using Microsoft.EntityFrameworkCore.Migrations;

namespace VET.Database.Migrations.SqlServer.Migrations.Migrations
{
    public partial class MySecondMigrationContextUnificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoteThird",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteThird",
                table: "Appointments");
        }
    }
}
