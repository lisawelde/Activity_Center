using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeltExam.Migrations
{
    public partial class ChangedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_PlannerUserId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlannerUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlannerUserId",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlannerId",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Joins",
                columns: table => new
                {
                    JoinId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    PlanId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joins", x => x.JoinId);
                    table.ForeignKey(
                        name: "FK_Joins_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Joins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlannerId",
                table: "Plans",
                column: "PlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Joins_PlanId",
                table: "Joins",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Joins_UserId",
                table: "Joins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_PlannerId",
                table: "Plans",
                column: "PlannerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_PlannerId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "Joins");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlannerId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlannerId",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlannerUserId",
                table: "Plans",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    PlanId = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participants_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlannerUserId",
                table: "Plans",
                column: "PlannerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_PlanId",
                table: "Participants",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_PlannerUserId",
                table: "Plans",
                column: "PlannerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
