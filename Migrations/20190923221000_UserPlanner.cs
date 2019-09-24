using Microsoft.EntityFrameworkCore.Migrations;

namespace BeltExam.Migrations
{
    public partial class UserPlanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlannerUserId",
                table: "Plans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlannerUserId",
                table: "Plans",
                column: "PlannerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_PlannerUserId",
                table: "Plans",
                column: "PlannerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_PlannerUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlannerUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlannerUserId",
                table: "Plans");
        }
    }
}
