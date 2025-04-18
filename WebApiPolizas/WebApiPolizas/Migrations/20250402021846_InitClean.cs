using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiPolizas.Migrations
{
    /// <inheritdoc />
    public partial class InitClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aseguradoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aseguradoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CedulaAsegurado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TipoPersona = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CedulaAsegurado);
                });

            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPoliza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPoliza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPoliza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPoliza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polizas",
                columns: table => new
                {
                    NumeroPoliza = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoPolizaId = table.Column<int>(type: "int", nullable: false),
                    CedulaAsegurado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MontoAsegurado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "date", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "date", nullable: false),
                    CoberturaId = table.Column<int>(type: "int", nullable: false),
                    EstadoPolizaId = table.Column<int>(type: "int", nullable: false),
                    Prima = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodo = table.Column<DateTime>(type: "date", nullable: false),
                    FechaInclusion = table.Column<DateTime>(type: "date", nullable: false),
                    AseguradoraId = table.Column<int>(type: "int", nullable: false),
                    AseguradoraId1 = table.Column<int>(type: "int", nullable: true),
                    ClienteCedulaAsegurado = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CoberturaId1 = table.Column<int>(type: "int", nullable: true),
                    EstadoPolizaId1 = table.Column<int>(type: "int", nullable: true),
                    TipoPolizaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polizas", x => x.NumeroPoliza);
                    table.ForeignKey(
                        name: "FK_Polizas_Aseguradoras_AseguradoraId",
                        column: x => x.AseguradoraId,
                        principalTable: "Aseguradoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polizas_Aseguradoras_AseguradoraId1",
                        column: x => x.AseguradoraId1,
                        principalTable: "Aseguradoras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Polizas_Clientes_CedulaAsegurado",
                        column: x => x.CedulaAsegurado,
                        principalTable: "Clientes",
                        principalColumn: "CedulaAsegurado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polizas_Clientes_ClienteCedulaAsegurado",
                        column: x => x.ClienteCedulaAsegurado,
                        principalTable: "Clientes",
                        principalColumn: "CedulaAsegurado");
                    table.ForeignKey(
                        name: "FK_Polizas_Coberturas_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polizas_Coberturas_CoberturaId1",
                        column: x => x.CoberturaId1,
                        principalTable: "Coberturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Polizas_EstadosPoliza_EstadoPolizaId",
                        column: x => x.EstadoPolizaId,
                        principalTable: "EstadosPoliza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polizas_EstadosPoliza_EstadoPolizaId1",
                        column: x => x.EstadoPolizaId1,
                        principalTable: "EstadosPoliza",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Polizas_TiposPoliza_TipoPolizaId",
                        column: x => x.TipoPolizaId,
                        principalTable: "TiposPoliza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polizas_TiposPoliza_TipoPolizaId1",
                        column: x => x.TipoPolizaId1,
                        principalTable: "TiposPoliza",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_AseguradoraId",
                table: "Polizas",
                column: "AseguradoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_AseguradoraId1",
                table: "Polizas",
                column: "AseguradoraId1");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_CedulaAsegurado",
                table: "Polizas",
                column: "CedulaAsegurado");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_ClienteCedulaAsegurado",
                table: "Polizas",
                column: "ClienteCedulaAsegurado");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_CoberturaId",
                table: "Polizas",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_CoberturaId1",
                table: "Polizas",
                column: "CoberturaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_EstadoPolizaId",
                table: "Polizas",
                column: "EstadoPolizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_EstadoPolizaId1",
                table: "Polizas",
                column: "EstadoPolizaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_TipoPolizaId",
                table: "Polizas",
                column: "TipoPolizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_TipoPolizaId1",
                table: "Polizas",
                column: "TipoPolizaId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polizas");

            migrationBuilder.DropTable(
                name: "Aseguradoras");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "EstadosPoliza");

            migrationBuilder.DropTable(
                name: "TiposPoliza");
        }
    }
}
