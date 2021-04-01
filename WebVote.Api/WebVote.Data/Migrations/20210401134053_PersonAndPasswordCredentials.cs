using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVote.Data.Migrations
{
    public partial class PersonAndPasswordCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Role = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    IndividualTaxNumber = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordCredentials",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    Salt = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordCredentials", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PasswordCredentials_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordCredentials_Login",
                table: "PasswordCredentials",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_IndividualTaxNumber",
                table: "People",
                column: "IndividualTaxNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordCredentials");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
