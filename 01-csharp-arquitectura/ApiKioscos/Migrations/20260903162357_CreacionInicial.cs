using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DominioKioscos.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kioscos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    UltimoReporte = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MotivoFueraDeServicio = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kioscos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kioscos_Codigo",
                table: "Kioscos",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kioscos");
        }
    }
}
