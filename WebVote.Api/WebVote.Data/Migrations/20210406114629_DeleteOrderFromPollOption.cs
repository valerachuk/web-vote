using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVote.Data.Migrations
{
    public partial class DeleteOrderFromPollOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PollOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PollOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
