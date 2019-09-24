using Microsoft.EntityFrameworkCore.Migrations;

namespace BeltExam.Migrations
{
    public partial class Duration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Plans",
                newName: "DurationAmount");

            migrationBuilder.AddColumn<string>(
                name: "DurationType",
                table: "Plans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DurationAmount",
                table: "Plans",
                newName: "Duration");
        }
    }
}
