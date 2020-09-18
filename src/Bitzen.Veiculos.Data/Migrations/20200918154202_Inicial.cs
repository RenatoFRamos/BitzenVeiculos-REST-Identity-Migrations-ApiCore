using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bitzen.Veiculos.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Comissao = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oportunidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VeiculoId = table.Column<Guid>(nullable: false),
                    VendedorId = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Comissao = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataExpiracao = table.Column<DateTime>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    DataExclusao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oportunidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OportunidadesLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OportunidadeId = table.Column<Guid>(nullable: false),
                    VeiculoId = table.Column<Guid>(nullable: false),
                    VendedorId = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Comissao = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OportunidadesLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Placa = table.Column<string>(type: "varchar(7)", nullable: false),
                    Ano = table.Column<string>(type: "varchar(4)", nullable: false),
                    Modelo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Combustivel = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    DataExclusao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendedoresCargo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VendedorId = table.Column<Guid>(nullable: false),
                    CargoId = table.Column<Guid>(nullable: false),
                    Comissao = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedoresCargo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Oportunidades");

            migrationBuilder.DropTable(
                name: "OportunidadesLog");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "VendedoresCargo");
        }
    }
}
