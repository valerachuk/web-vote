using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVote.Data.Migrations
{
    public partial class AddedCodeToRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Regions",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "Regions",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Regions_Code",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Regions");
        }
    }
}
