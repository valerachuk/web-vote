using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVote.Data.Migrations
{
    public partial class AddedRegionToVoterVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "VoterVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VoterVotes_RegionId",
                table: "VoterVotes",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterVotes_Regions_RegionId",
                table: "VoterVotes",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterVotes_Regions_RegionId",
                table: "VoterVotes");

            migrationBuilder.DropIndex(
                name: "IX_VoterVotes_RegionId",
                table: "VoterVotes");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "VoterVotes");
        }
    }
}
