using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVote.Data.Migrations
{
    public partial class RegistrationLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ByWhomId = table.Column<int>(type: "int", nullable: false),
                    ToWhomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationLog_People_ByWhomId",
                        column: x => x.ByWhomId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationLog_People_ToWhomId",
                        column: x => x.ToWhomId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationLog_ByWhomId",
                table: "RegistrationLog",
                column: "ByWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationLog_ToWhomId",
                table: "RegistrationLog",
                column: "ToWhomId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationLog");
        }
    }
}
