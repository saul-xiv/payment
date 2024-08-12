using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentCodeChallenge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catEstadoPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catEstadoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "catRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_catRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "catRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    IdPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadProductos = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdUsuarioPaga = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuarioRecibe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoPagoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pago_Usuario_IdUsuarioPaga",
                        column: x => x.IdUsuarioPaga,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Pago_Usuario_IdUsuarioRecibe",
                        column: x => x.IdUsuarioRecibe,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Pago_catEstadoPago_EstadoPagoId",
                        column: x => x.EstadoPagoId,
                        principalTable: "catEstadoPago",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "catEstadoPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Registrado" },
                    { 2, "En_Proceso" },
                    { 3, "Pagado" },
                    { 4, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "catRol",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Contrasenia", "Correo", "IdRol", "Nombre" },
                values: new object[,]
                {
                    { new Guid("17fa39bc-5552-4c5f-ae43-9cb8bf428cc4"), "adminpassword", "admin@example.com", 1, "Admin" },
                    { new Guid("3fbfdc32-c0ae-4921-95ac-429d7cbd99b3"), "user2password", "user2@example.com", 2, "Jane Doe" },
                    { new Guid("6cbeba98-7b66-4121-95c6-da6865b7820f"), "user3password", "user3@example.com", 2, "John Smith" },
                    { new Guid("a786221c-5f34-45e4-a296-85ff88edfa73"), "user1password", "user1@example.com", 2, "John Doe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pago_EstadoPagoId",
                table: "Pago",
                column: "EstadoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdUsuarioPaga",
                table: "Pago",
                column: "IdUsuarioPaga");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdUsuarioRecibe",
                table: "Pago",
                column: "IdUsuarioRecibe");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "catEstadoPago");

            migrationBuilder.DropTable(
                name: "catRol");
        }
    }
}
