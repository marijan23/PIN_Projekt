using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIN_Projekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Smjers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predmets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmjerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predmets_Smjers_SmjerId",
                        column: x => x.SmjerId,
                        principalTable: "Smjers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ispits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocijena = table.Column<int>(type: "int", nullable: false),
                    BrojBodova = table.Column<int>(type: "int", nullable: false),
                    DatumPolaganja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ispits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ispits_Predmets_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ispits_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ispits_PredmetId",
                table: "Ispits",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ispits_StudentId",
                table: "Ispits",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Predmets_SmjerId",
                table: "Predmets",
                column: "SmjerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ispits");

            migrationBuilder.DropTable(
                name: "Predmets");

            migrationBuilder.DropTable(
                name: "Smjers");
        }
    }
}
