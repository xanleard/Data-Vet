using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VET.Database.Migrations.SqlServer.Migrations.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ////migrationBuilder.CreateTable(
            ////    name: "Appointments",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        NoteFirst = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        NoteSeconds = table.Column<string>(type: "nvarchar(max)", nullable: true)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_Appointments", x => x.Id);
            ////    });

            ////migrationBuilder.CreateTable(
            ////    name: "Customers",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        IdentificationCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        Telephone1 = table.Column<int>(type: "int", nullable: false),
            ////        Telephone2 = table.Column<int>(type: "int", nullable: false),
            ////        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_Customers", x => x.Id);
            ////    });

            ////migrationBuilder.CreateTable(
            ////    name: "Sexes",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_Sexes", x => x.Id);
            ////    });

            ////migrationBuilder.CreateTable(
            ////    name: "TypeAnimals",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_TypeAnimals", x => x.Id);
            ////    });

            ////migrationBuilder.CreateTable(
            ////    name: "UnitMeasurements",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_UnitMeasurements", x => x.Id);
            ////    });

            ////migrationBuilder.CreateTable(
            ////    name: "Animals",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        Age = table.Column<int>(type: "int", nullable: false),
            ////        Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        SexId = table.Column<int>(type: "int", nullable: false),
            ////        Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            ////        UnitMeasurementId = table.Column<int>(type: "int", nullable: false),
            ////        Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        LastVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        TypeAnimalId = table.Column<int>(type: "int", nullable: false),
            ////        CustomerId = table.Column<int>(type: "int", nullable: false)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_Animals", x => x.Id);
            ////        table.ForeignKey(
            ////            name: "FK_Animals_Customers_CustomerId",
            ////            column: x => x.CustomerId,
            ////            principalTable: "Customers",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Restrict);
            ////        table.ForeignKey(
            ////            name: "FK_Animals_Sexes_SexId",
            ////            column: x => x.SexId,
            ////            principalTable: "Sexes",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Restrict);
            ////        table.ForeignKey(
            ////            name: "FK_Animals_TypeAnimals_TypeAnimalId",
            ////            column: x => x.TypeAnimalId,
            ////            principalTable: "TypeAnimals",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Restrict);
            ////        table.ForeignKey(
            ////            name: "FK_Animals_UnitMeasurements_UnitMeasurementId",
            ////            column: x => x.UnitMeasurementId,
            ////            principalTable: "UnitMeasurements",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Restrict);
            ////    });

            ////migrationBuilder.CreateIndex(
            ////    name: "IX_Animals_CustomerId",
            ////    table: "Animals",
            ////    column: "CustomerId");

            ////migrationBuilder.CreateIndex(
            ////    name: "IX_Animals_SexId",
            ////    table: "Animals",
            ////    column: "SexId");

            ////migrationBuilder.CreateIndex(
            ////    name: "IX_Animals_TypeAnimalId",
            ////    table: "Animals",
            ////    column: "TypeAnimalId");

            ////migrationBuilder.CreateIndex(
            ////    name: "IX_Animals_UnitMeasurementId",
            ////    table: "Animals",
            ////    column: "UnitMeasurementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "TypeAnimals");

            migrationBuilder.DropTable(
                name: "UnitMeasurements");
        }
    }
}
