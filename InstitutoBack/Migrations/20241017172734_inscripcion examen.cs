using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstitutoBack.Migrations
{
    /// <inheritdoc />
    public partial class inscripcionexamen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inscripcionesexamenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    TurnoExamenId = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscripcionesexamenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inscripcionesexamenes_alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscripcionesexamenes_carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscripcionesexamenes_turnosexamenes_TurnoExamenId",
                        column: x => x.TurnoExamenId,
                        principalTable: "turnosexamenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detallesinscripcionesexamenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InscripcionExamenId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detallesinscripcionesexamenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detallesinscripcionesexamenes_inscripcionesexamenes_Inscripc~",
                        column: x => x.InscripcionExamenId,
                        principalTable: "inscripcionesexamenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detallesinscripcionesexamenes_materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "inscripciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2024, 10, 17, 14, 27, 33, 177, DateTimeKind.Local).AddTicks(178));

            migrationBuilder.CreateIndex(
                name: "IX_detallesinscripcionesexamenes_InscripcionExamenId",
                table: "detallesinscripcionesexamenes",
                column: "InscripcionExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_detallesinscripcionesexamenes_MateriaId",
                table: "detallesinscripcionesexamenes",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripcionesexamenes_AlumnoId",
                table: "inscripcionesexamenes",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripcionesexamenes_CarreraId",
                table: "inscripcionesexamenes",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripcionesexamenes_TurnoExamenId",
                table: "inscripcionesexamenes",
                column: "TurnoExamenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detallesinscripcionesexamenes");

            migrationBuilder.DropTable(
                name: "inscripcionesexamenes");

            migrationBuilder.UpdateData(
                table: "inscripciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2024, 10, 13, 11, 56, 51, 520, DateTimeKind.Local).AddTicks(368));
        }
    }
}
