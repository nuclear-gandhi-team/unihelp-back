using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniHelp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AnswerIsCorrectBoolMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_AnswerVariants_CorrectAnswerId",
                table: "TestQuestions");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestions_CorrectAnswerId",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerId",
                table: "TestQuestions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AnswerVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AnswerVariants");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerId",
                table: "TestQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_CorrectAnswerId",
                table: "TestQuestions",
                column: "CorrectAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_AnswerVariants_CorrectAnswerId",
                table: "TestQuestions",
                column: "CorrectAnswerId",
                principalTable: "AnswerVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
